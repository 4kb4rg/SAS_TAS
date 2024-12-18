
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
Imports Microsoft.VisualBasic


Imports agri.GL
Imports agri.GlobalHdl

Public Class GL_setup_COGS : Inherits Page

    Protected WithEvents lblErrMessage As Label

    Protected WithEvents ddlPanenDanPengumpulanAccFrom As DropDownList
    Protected WithEvents ddlPanenDanPengumpulanAccTo As DropDownList
    Protected WithEvents ddlPemeliharaanTMAccFrom As DropDownList
    Protected WithEvents ddlPemeliharaanTMAccTo As DropDownList
    Protected WithEvents ddlPemupukanTMAccFrom As DropDownList
    Protected WithEvents ddlPemupukanTMAccTo As DropDownList
    Protected WithEvents ddlPengolahanPabrikAccFrom As DropDownList
    Protected WithEvents ddlPengolahanPabrikAccTo As DropDownList
    Protected WithEvents ddlPemeliharaanPabrikAccFrom As DropDownList
    Protected WithEvents ddlPemeliharaanPabrikAccTo As DropDownList
    Protected WithEvents ddlPembelianTBSExternAccFrom As DropDownList
    Protected WithEvents ddlPembelianTBSExternAccTo As DropDownList
    Protected WithEvents ddlPemakaianInternAccFrom As DropDownList
    Protected WithEvents ddlPemakaianInternAccTo As DropDownList
    Protected WithEvents ddlSahamLangsungYangDijualAccFrom As DropDownList
    Protected WithEvents ddlSahamLangsungYangDijualAccTo As DropDownList
    Protected WithEvents ddlPenyusukTMAccFrom As DropDownList
    Protected WithEvents ddlPenyusukTMAccTo As DropDownList
    Protected WithEvents ddlPenyusukAKTAccFrom As DropDownList
    Protected WithEvents ddlPenyusukAKTAccTo As DropDownList
    Protected WithEvents ddlAlokasiSPenyusutanAccFrom As DropDownList
    Protected WithEvents ddlAlokasiSPenyusutanAccTo As DropDownList
    Protected WithEvents ddlKaryawanAccFrom As DropDownList
    Protected WithEvents ddlKaryawanAccTo As DropDownList
    Protected WithEvents ddlAdministrasiKantorAccFrom As DropDownList
    Protected WithEvents ddlAdministrasiKantorAccTo As DropDownList
    Protected WithEvents ddlPemerliharaanAccFrom As DropDownList
    Protected WithEvents ddlPemerliharaanAccTo As DropDownList
    Protected WithEvents ddlPengembanganKaryawanAccFrom As DropDownList
    Protected WithEvents ddlPengembanganKaryawanAccTo As DropDownList
    Protected WithEvents ddlPerjalananDinasAccFrom As DropDownList
    Protected WithEvents ddlPerjalananDinasAccTo As DropDownList
    Protected WithEvents ddlUnitLaboratoriumAccFrom As DropDownList
    Protected WithEvents ddlUnitLaboratoriumAccTo As DropDownList
    Protected WithEvents ddlRisetAccFrom As DropDownList
    Protected WithEvents ddlRisetAccTo As DropDownList
    Protected WithEvents ddlTransportasi1AccFrom As DropDownList
    Protected WithEvents ddlTransportasi1AccTo As DropDownList
    Protected WithEvents ddlTransportasi2AccFrom As DropDownList
    Protected WithEvents ddlTransportasi2AccTo As DropDownList
    Protected WithEvents ddlUmumLainyaAccFrom As DropDownList
    Protected WithEvents ddlUmumLainyaAccTo As DropDownList
    Protected WithEvents ddlOverheadYangDialokasiAccFrom As DropDownList
    Protected WithEvents ddlOverheadYangDialokasiAccTo As DropDownList
    Protected WithEvents ddlEliminasiPemakaianTBSInternAccFrom As DropDownList
    Protected WithEvents ddlEliminasiPemakaianTBSInternAccTo As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS1AccFrom As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS1AccTo As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS2AccFrom As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS2AccTo As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS3AccFrom As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS3AccTo As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS4AccFrom As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS4AccTo As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS5AccFrom As DropDownList
    Protected WithEvents ddlBiayaProduksiTBS5AccTo As DropDownList

    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents lblHasRecord As Label

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdmin As New agri.Admin.clsShare()

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objGLDs As New Object()
    Dim objGlobalAccDs As New Object()

    Dim objGlobalNurseryAccDs As New Object()
    Dim objGlobalAllAccDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim intModuleActivate As Integer

    Dim intConfigsetting As Integer
    Dim strBlkTag As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        intModuleActivate = Session("SS_MODULEACTIVATE")

        intConfigsetting = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(clsAccessRights.EnumGLAccessRights.GLEntrySetup), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            onload_GetLangCap()

            If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(clsGlobalHdl.EnumModule.GeneralLedger)) = True Then

                ddlPanenDanPengumpulanAccFrom.Enabled = True
                ddlPanenDanPengumpulanAccTo.Enabled = True
                ddlPemeliharaanTMAccFrom.Enabled = True
                ddlPemeliharaanTMAccTo.Enabled = True
                ddlPemupukanTMAccFrom.Enabled = True
                ddlPemupukanTMAccTo.Enabled = True
                ddlPengolahanPabrikAccFrom.Enabled = True
                ddlPengolahanPabrikAccTo.Enabled = True
                ddlPemeliharaanPabrikAccFrom.Enabled = True
                ddlPemeliharaanPabrikAccTo.Enabled = True
                ddlPembelianTBSExternAccFrom.Enabled = True
                ddlPembelianTBSExternAccTo.Enabled = True
                ddlPemakaianInternAccFrom.Enabled = True
                ddlPemakaianInternAccTo.Enabled = True
                ddlSahamLangsungYangDijualAccFrom.Enabled = True
                ddlSahamLangsungYangDijualAccTo.Enabled = True
                ddlPenyusukTMAccFrom.Enabled = True
                ddlPenyusukTMAccTo.Enabled = True
                ddlPenyusukAKTAccFrom.Enabled = True
                ddlPenyusukAKTAccTo.Enabled = True
                ddlAlokasiSPenyusutanAccFrom.Enabled = True
                ddlAlokasiSPenyusutanAccTo.Enabled = True
                ddlKaryawanAccFrom.Enabled = True
                ddlKaryawanAccTo.Enabled = True
                ddlAdministrasiKantorAccFrom.Enabled = True
                ddlAdministrasiKantorAccTo.Enabled = True
                ddlPemerliharaanAccFrom.Enabled = True
                ddlPemerliharaanAccTo.Enabled = True
                ddlPengembanganKaryawanAccFrom.Enabled = True
                ddlPengembanganKaryawanAccTo.Enabled = True
                ddlPerjalananDinasAccFrom.Enabled = True
                ddlPerjalananDinasAccTo.Enabled = True
                ddlUnitLaboratoriumAccFrom.Enabled = True
                ddlUnitLaboratoriumAccTo.Enabled = True
                ddlRisetAccFrom.Enabled = True
                ddlRisetAccTo.Enabled = True
                ddlTransportasi1AccFrom.Enabled = True
                ddlTransportasi1AccTo.Enabled = True
                ddlTransportasi2AccFrom.Enabled = True
                ddlTransportasi2AccTo.Enabled = True
                ddlUmumLainyaAccFrom.Enabled = True
                ddlUmumLainyaAccTo.Enabled = True
                ddlOverheadYangDialokasiAccFrom.Enabled = True
                ddlOverheadYangDialokasiAccTo.Enabled = True
                ddlEliminasiPemakaianTBSInternAccFrom.Enabled = True
                ddlEliminasiPemakaianTBSInternAccTo.Enabled = True
                ddlBiayaProduksiTBS1AccFrom.Enabled = True
                ddlBiayaProduksiTBS1AccTo.Enabled = True
                ddlBiayaProduksiTBS2AccFrom.Enabled = True
                ddlBiayaProduksiTBS2AccTo.Enabled = True
                ddlBiayaProduksiTBS3AccFrom.Enabled = True
                ddlBiayaProduksiTBS3AccTo.Enabled = True
                ddlBiayaProduksiTBS4AccFrom.Enabled = True
                ddlBiayaProduksiTBS4AccTo.Enabled = True
                ddlBiayaProduksiTBS5AccFrom.Enabled = True
                ddlBiayaProduksiTBS5AccTo.Enabled = True

                ddlPanenDanPengumpulanAccFrom.Visible = True
                ddlPanenDanPengumpulanAccTo.Visible = True
                ddlPemeliharaanTMAccFrom.Visible = True
                ddlPemeliharaanTMAccTo.Visible = True
                ddlPemupukanTMAccFrom.Visible = True
                ddlPemupukanTMAccTo.Visible = True
                ddlPengolahanPabrikAccFrom.Visible = True
                ddlPengolahanPabrikAccTo.Visible = True
                ddlPemeliharaanPabrikAccFrom.Visible = True
                ddlPemeliharaanPabrikAccTo.Visible = True
                ddlPembelianTBSExternAccFrom.Visible = True
                ddlPembelianTBSExternAccTo.Visible = True
                ddlPemakaianInternAccFrom.Visible = True
                ddlPemakaianInternAccTo.Visible = True
                ddlSahamLangsungYangDijualAccFrom.Visible = True
                ddlSahamLangsungYangDijualAccTo.Visible = True
                ddlPenyusukTMAccFrom.Visible = True
                ddlPenyusukTMAccTo.Visible = True
                ddlPenyusukAKTAccFrom.Visible = True
                ddlPenyusukAKTAccTo.Visible = True
                ddlAlokasiSPenyusutanAccFrom.Visible = True
                ddlAlokasiSPenyusutanAccTo.Visible = True
                ddlKaryawanAccFrom.Visible = True
                ddlKaryawanAccTo.Visible = True
                ddlAdministrasiKantorAccFrom.Visible = True
                ddlAdministrasiKantorAccTo.Visible = True
                ddlPemerliharaanAccFrom.Visible = True
                ddlPemerliharaanAccTo.Visible = True
                ddlPengembanganKaryawanAccFrom.Visible = True
                ddlPengembanganKaryawanAccTo.Visible = True
                ddlPerjalananDinasAccFrom.Visible = True
                ddlPerjalananDinasAccTo.Visible = True
                ddlUnitLaboratoriumAccFrom.Visible = True
                ddlUnitLaboratoriumAccTo.Visible = True
                ddlRisetAccFrom.Visible = True
                ddlRisetAccTo.Visible = True
                ddlTransportasi1AccFrom.Visible = True
                ddlTransportasi1AccTo.Visible = True
                ddlTransportasi2AccFrom.Visible = True
                ddlTransportasi2AccTo.Visible = True
                ddlUmumLainyaAccFrom.Visible = True
                ddlUmumLainyaAccTo.Visible = True
                ddlOverheadYangDialokasiAccFrom.Visible = True
                ddlOverheadYangDialokasiAccTo.Visible = True
                ddlEliminasiPemakaianTBSInternAccFrom.Visible = True
                ddlEliminasiPemakaianTBSInternAccTo.Visible = True
                ddlBiayaProduksiTBS1AccFrom.Visible = True
                ddlBiayaProduksiTBS1AccTo.Visible = True
                ddlBiayaProduksiTBS2AccFrom.Visible = True
                ddlBiayaProduksiTBS2AccTo.Visible = True
                ddlBiayaProduksiTBS3AccFrom.Visible = True
                ddlBiayaProduksiTBS3AccTo.Visible = True
                ddlBiayaProduksiTBS4AccFrom.Visible = True
                ddlBiayaProduksiTBS4AccTo.Visible = True
                ddlBiayaProduksiTBS5AccFrom.Visible = True
                ddlBiayaProduksiTBS5AccTo.Visible = True

            End If

            If Not IsPostBack Then
                BindGLAccDropDownList()
                onLoad_Display()
            End If
        End If
    End Sub

    Function BindAccount(ByVal pv_strAccCode As String, ByRef pv_intIndex As Integer) As DataSet
        Dim objAccDs As New Object()
        Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "'" & _
                                " And ACC.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "'" & _
                                " And ACC.NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "'"
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        If UCase(TypeName(objGlobalAccDs)) = "DATASET" Then
            For intCnt = 0 To objGlobalAccDs.Tables(0).Rows.Count - 1
                If objGlobalAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                    intSelectIndex = intCnt
                    Exit For
                End If
            Next
            pv_intIndex = intSelectIndex
            BindAccount = objGlobalAccDs
        Else
            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                        strParam, _
                                                        objGLSetup.EnumGLMasterType.AccountCode, _
                                                        objGlobalAccDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            For intCnt = 0 To objGlobalAccDs.Tables(0).Rows.Count - 1
                If objGlobalAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                    intSelectIndex = intCnt + 1
                    Exit For
                End If
            Next

            dr = objGlobalAccDs.Tables(0).NewRow()
            dr("AccCode") = ""
            dr("_Description") = "Select Account Code"
            objGlobalAccDs.Tables(0).Rows.InsertAt(dr, 0)
            pv_intIndex = intSelectIndex
            BindAccount = objGlobalAccDs
        End If
    End Function

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode

        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString & "&redirect=ws/setup/ws_workcodedet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                Exit For
            End If
        Next
    End Function

    Function BindAllAccount(ByVal pv_strAccCode As String, ByRef pv_intIndex As Integer) As DataSet
        Dim objAccDs As New Object()
        Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "'"

        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        If UCase(TypeName(objGlobalAllAccDs)) = "DATASET" Then
            For intCnt = 0 To objGlobalAllAccDs.Tables(0).Rows.Count - 1
                If objGlobalAllAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                    intSelectIndex = intCnt
                    Exit For
                End If
            Next
            pv_intIndex = intSelectIndex
            BindAllAccount = objGlobalAllAccDs
        Else
            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                        strParam, _
                                                        objGLSetup.EnumGLMasterType.AccountCode, _
                                                        objGlobalAllAccDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNTCODE_LIST_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            For intCnt = 0 To objGlobalAllAccDs.Tables(0).Rows.Count - 1
                If objGlobalAllAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                    intSelectIndex = intCnt + 1
                    Exit For
                End If
            Next

            dr = objGlobalAllAccDs.Tables(0).NewRow()
            dr("AccCode") = ""
            dr("_Description") = "Select Account Code"
            objGlobalAllAccDs.Tables(0).Rows.InsertAt(dr, 0)
            pv_intIndex = intSelectIndex
            BindAllAccount = objGlobalAllAccDs
        End If
    End Function

    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            pr_strAccType = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType"))
            pr_strAccPurpose = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose"))
            pr_strNurseryInd = Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd"))
        End If
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "GL_CLSSETUP_COGSSETUP_ADD"
        Dim strOpCd_Upd As String = "GL_CLSSETUP_COGSSETUP_UPD"
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = ""




        If strCmdArgs = "Save" Then
            strOpCd = strOpCd_Upd
            strParam = ddlPanenDanPengumpulanAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPanenDanPengumpulanAccTo.SelectedItem.Value & Chr(9) & _
         ddlPemeliharaanTMAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPemeliharaanTMAccTo.SelectedItem.Value & Chr(9) & _
         ddlPemupukanTMAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPemupukanTMAccTo.SelectedItem.Value & Chr(9) & _
         ddlPengolahanPabrikAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPengolahanPabrikAccTo.SelectedItem.Value & Chr(9) & _
         ddlPemeliharaanPabrikAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPemeliharaanPabrikAccTo.SelectedItem.Value & Chr(9) & _
         ddlPembelianTBSExternAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPembelianTBSExternAccTo.SelectedItem.Value & Chr(9) & _
         ddlPemakaianInternAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPemakaianInternAccTo.SelectedItem.Value & Chr(9) & _
         ddlSahamLangsungYangDijualAccFrom.SelectedItem.Value & Chr(9) & _
         ddlSahamLangsungYangDijualAccTo.SelectedItem.Value & Chr(9) & _
         ddlPenyusukTMAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPenyusukTMAccTo.SelectedItem.Value & Chr(9) & _
         ddlPenyusukAKTAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPenyusukAKTAccTo.SelectedItem.Value & Chr(9) & _
         ddlAlokasiSPenyusutanAccFrom.SelectedItem.Value & Chr(9) & _
         ddlAlokasiSPenyusutanAccTo.SelectedItem.Value & Chr(9) & _
         ddlKaryawanAccFrom.SelectedItem.Value & Chr(9) & _
         ddlKaryawanAccTo.SelectedItem.Value & Chr(9) & _
         ddlAdministrasiKantorAccFrom.SelectedItem.Value & Chr(9) & _
         ddlAdministrasiKantorAccTo.SelectedItem.Value & Chr(9) & _
         ddlPemerliharaanAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPemerliharaanAccTo.SelectedItem.Value & Chr(9) & _
         ddlPengembanganKaryawanAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPengembanganKaryawanAccTo.SelectedItem.Value & Chr(9) & _
         ddlPerjalananDinasAccFrom.SelectedItem.Value & Chr(9) & _
         ddlPerjalananDinasAccTo.SelectedItem.Value & Chr(9) & _
         ddlUnitLaboratoriumAccFrom.SelectedItem.Value & Chr(9) & _
         ddlUnitLaboratoriumAccTo.SelectedItem.Value & Chr(9) & _
         ddlRisetAccFrom.SelectedItem.Value & Chr(9) & _
         ddlRisetAccTo.SelectedItem.Value & Chr(9) & _
         ddlTransportasi1AccFrom.SelectedItem.Value & Chr(9) & _
         ddlTransportasi1AccTo.SelectedItem.Value & Chr(9) & _
         ddlTransportasi2AccFrom.SelectedItem.Value & Chr(9) & _
         ddlTransportasi2AccTo.SelectedItem.Value & Chr(9) & _
         ddlUmumLainyaAccFrom.SelectedItem.Value & Chr(9) & _
         ddlUmumLainyaAccTo.SelectedItem.Value & Chr(9) & _
         ddlOverheadYangDialokasiAccFrom.SelectedItem.Value & Chr(9) & _
         ddlOverheadYangDialokasiAccTo.SelectedItem.Value & Chr(9) & _
         ddlEliminasiPemakaianTBSInternAccFrom.SelectedItem.Value & Chr(9) & _
         ddlEliminasiPemakaianTBSInternAccTo.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS1AccFrom.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS1AccTo.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS2AccFrom.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS2AccTo.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS3AccFrom.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS3AccTo.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS4AccFrom.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS4AccTo.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS5AccFrom.SelectedItem.Value & Chr(9) & _
         ddlBiayaProduksiTBS5AccTo.SelectedItem.Value & Chr(9)
            Try
                intErrNo = objGLSetup.mtdCOGSSetup(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strOpCd, _
                                                    strParam, _
                                                    False, _
                                                    objGLDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_COGSSETUP_UPD&errmesg=" & Exp.ToString & "&redirect=GL/setup/GL_setup_COGS.aspx")
            End Try
        End If

        onLoad_Display()
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_COGSSETUP_GET"
        Dim intErrNo As Integer
        Dim intIndex As Integer
        Dim intCnt As Integer
        Dim strParam As String

        Dim objCOGSDs As New Object()
        Dim intCOGSDsIndex As Integer


        Try
            strParam = "|"
            intErrNo = objGLSetup.mtdCOGSSetupGet(strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strOpCd, _
                                                strParam, _
                                                True, _
                                                objCOGSDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_COGSSETUP_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If objCOGSDs.Tables(0).Rows.Count > 0 Then
            lblHasRecord.Text = True
            lblLastUpdate.Text = objGlobal.GetLongDate(objCOGSDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = Trim(objCOGSDs.Tables(0).Rows(0).Item("UserName"))

            ddlPanenDanPengumpulanAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PanenDanPengumpulanAccFrom")), intIndex)
            ddlPanenDanPengumpulanAccFrom.DataValueField = "AccCode"
            ddlPanenDanPengumpulanAccFrom.DataTextField = "_Description"
            ddlPanenDanPengumpulanAccFrom.DataBind()
            ddlPanenDanPengumpulanAccFrom.SelectedIndex = intIndex

            ddlPanenDanPengumpulanAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PanenDanPengumpulanAccTo")), intIndex)
            ddlPanenDanPengumpulanAccTo.DataValueField = "AccCode"
            ddlPanenDanPengumpulanAccTo.DataTextField = "_Description"
            ddlPanenDanPengumpulanAccTo.DataBind()
            ddlPanenDanPengumpulanAccTo.SelectedIndex = intIndex

            ddlPemeliharaanTMAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemeliharaanTMAccFrom")), intIndex)
            ddlPemeliharaanTMAccFrom.DataValueField = "AccCode"
            ddlPemeliharaanTMAccFrom.DataTextField = "_Description"
            ddlPemeliharaanTMAccFrom.DataBind()
            ddlPemeliharaanTMAccFrom.SelectedIndex = intIndex

            ddlPemeliharaanTMAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemeliharaanTMAccTo")), intIndex)
            ddlPemeliharaanTMAccTo.DataValueField = "AccCode"
            ddlPemeliharaanTMAccTo.DataTextField = "_Description"
            ddlPemeliharaanTMAccTo.DataBind()
            ddlPemeliharaanTMAccTo.SelectedIndex = intIndex

            ddlPemupukanTMAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemupukanTMAccFrom")), intIndex)
            ddlPemupukanTMAccFrom.DataValueField = "AccCode"
            ddlPemupukanTMAccFrom.DataTextField = "_Description"
            ddlPemupukanTMAccFrom.DataBind()
            ddlPemupukanTMAccFrom.SelectedIndex = intIndex

            ddlPemupukanTMAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemupukanTMAccTo")), intIndex)
            ddlPemupukanTMAccTo.DataValueField = "AccCode"
            ddlPemupukanTMAccTo.DataTextField = "_Description"
            ddlPemupukanTMAccTo.DataBind()
            ddlPemupukanTMAccTo.SelectedIndex = intIndex

            ddlPengolahanPabrikAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PengolahanPabrikAccFrom")), intIndex)
            ddlPengolahanPabrikAccFrom.DataValueField = "AccCode"
            ddlPengolahanPabrikAccFrom.DataTextField = "_Description"
            ddlPengolahanPabrikAccFrom.DataBind()
            ddlPengolahanPabrikAccFrom.SelectedIndex = intIndex

            ddlPengolahanPabrikAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PengolahanPabrikAccTo")), intIndex)
            ddlPengolahanPabrikAccTo.DataValueField = "AccCode"
            ddlPengolahanPabrikAccTo.DataTextField = "_Description"
            ddlPengolahanPabrikAccTo.DataBind()
            ddlPengolahanPabrikAccTo.SelectedIndex = intIndex

            ddlPemeliharaanPabrikAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemeliharaanPabrikAccFrom")), intIndex)
            ddlPemeliharaanPabrikAccFrom.DataValueField = "AccCode"
            ddlPemeliharaanPabrikAccFrom.DataTextField = "_Description"
            ddlPemeliharaanPabrikAccFrom.DataBind()
            ddlPemeliharaanPabrikAccFrom.SelectedIndex = intIndex

            ddlPemeliharaanPabrikAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemeliharaanPabrikAccTo")), intIndex)
            ddlPemeliharaanPabrikAccTo.DataValueField = "AccCode"
            ddlPemeliharaanPabrikAccTo.DataTextField = "_Description"
            ddlPemeliharaanPabrikAccTo.DataBind()
            ddlPemeliharaanPabrikAccTo.SelectedIndex = intIndex

            ddlPembelianTBSExternAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PembelianTBSExternAccFrom")), intIndex)
            ddlPembelianTBSExternAccFrom.DataValueField = "AccCode"
            ddlPembelianTBSExternAccFrom.DataTextField = "_Description"
            ddlPembelianTBSExternAccFrom.DataBind()
            ddlPembelianTBSExternAccFrom.SelectedIndex = intIndex

            ddlPembelianTBSExternAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PembelianTBSExternAccTo")), intIndex)
            ddlPembelianTBSExternAccTo.DataValueField = "AccCode"
            ddlPembelianTBSExternAccTo.DataTextField = "_Description"
            ddlPembelianTBSExternAccTo.DataBind()
            ddlPembelianTBSExternAccTo.SelectedIndex = intIndex

            ddlPemakaianInternAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemakaianInternAccFrom")), intIndex)
            ddlPemakaianInternAccFrom.DataValueField = "AccCode"
            ddlPemakaianInternAccFrom.DataTextField = "_Description"
            ddlPemakaianInternAccFrom.DataBind()
            ddlPemakaianInternAccFrom.SelectedIndex = intIndex

            ddlPemakaianInternAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemakaianInternAccTo")), intIndex)
            ddlPemakaianInternAccTo.DataValueField = "AccCode"
            ddlPemakaianInternAccTo.DataTextField = "_Description"
            ddlPemakaianInternAccTo.DataBind()
            ddlPemakaianInternAccTo.SelectedIndex = intIndex

            ddlSahamLangsungYangDijualAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("SahamLangsungYangDijualAccFrom")), intIndex)
            ddlSahamLangsungYangDijualAccFrom.DataValueField = "AccCode"
            ddlSahamLangsungYangDijualAccFrom.DataTextField = "_Description"
            ddlSahamLangsungYangDijualAccFrom.DataBind()
            ddlSahamLangsungYangDijualAccFrom.SelectedIndex = intIndex

            ddlSahamLangsungYangDijualAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("SahamLangsungYangDijualAccTo")), intIndex)
            ddlSahamLangsungYangDijualAccTo.DataValueField = "AccCode"
            ddlSahamLangsungYangDijualAccTo.DataTextField = "_Description"
            ddlSahamLangsungYangDijualAccTo.DataBind()
            ddlSahamLangsungYangDijualAccTo.SelectedIndex = intIndex

            ddlPenyusukTMAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PenyusukTMAccFrom")), intIndex)
            ddlPenyusukTMAccFrom.DataValueField = "AccCode"
            ddlPenyusukTMAccFrom.DataTextField = "_Description"
            ddlPenyusukTMAccFrom.DataBind()
            ddlPenyusukTMAccFrom.SelectedIndex = intIndex

            ddlPenyusukTMAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PenyusukTMAccTo")), intIndex)
            ddlPenyusukTMAccTo.DataValueField = "AccCode"
            ddlPenyusukTMAccTo.DataTextField = "_Description"
            ddlPenyusukTMAccTo.DataBind()
            ddlPenyusukTMAccTo.SelectedIndex = intIndex

            ddlPenyusukAKTAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PenyusukAKTAccFrom")), intIndex)
            ddlPenyusukAKTAccFrom.DataValueField = "AccCode"
            ddlPenyusukAKTAccFrom.DataTextField = "_Description"
            ddlPenyusukAKTAccFrom.DataBind()
            ddlPenyusukAKTAccFrom.SelectedIndex = intIndex

            ddlPenyusukAKTAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PenyusukAKTAccTo")), intIndex)
            ddlPenyusukAKTAccTo.DataValueField = "AccCode"
            ddlPenyusukAKTAccTo.DataTextField = "_Description"
            ddlPenyusukAKTAccTo.DataBind()
            ddlPenyusukAKTAccTo.SelectedIndex = intIndex

            ddlAlokasiSPenyusutanAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("AlokasiSPenyusutanAccFrom")), intIndex)
            ddlAlokasiSPenyusutanAccFrom.DataValueField = "AccCode"
            ddlAlokasiSPenyusutanAccFrom.DataTextField = "_Description"
            ddlAlokasiSPenyusutanAccFrom.DataBind()
            ddlAlokasiSPenyusutanAccFrom.SelectedIndex = intIndex

            ddlAlokasiSPenyusutanAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("AlokasiSPenyusutanAccTo")), intIndex)
            ddlAlokasiSPenyusutanAccTo.DataValueField = "AccCode"
            ddlAlokasiSPenyusutanAccTo.DataTextField = "_Description"
            ddlAlokasiSPenyusutanAccTo.DataBind()
            ddlAlokasiSPenyusutanAccTo.SelectedIndex = intIndex

            ddlKaryawanAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("KaryawanAccFrom")), intIndex)
            ddlKaryawanAccFrom.DataValueField = "AccCode"
            ddlKaryawanAccFrom.DataTextField = "_Description"
            ddlKaryawanAccFrom.DataBind()
            ddlKaryawanAccFrom.SelectedIndex = intIndex

            ddlKaryawanAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("KaryawanAccTo")), intIndex)
            ddlKaryawanAccTo.DataValueField = "AccCode"
            ddlKaryawanAccTo.DataTextField = "_Description"
            ddlKaryawanAccTo.DataBind()
            ddlKaryawanAccTo.SelectedIndex = intIndex

            ddlAdministrasiKantorAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("AdministrasiKantorAccFrom")), intIndex)
            ddlAdministrasiKantorAccFrom.DataValueField = "AccCode"
            ddlAdministrasiKantorAccFrom.DataTextField = "_Description"
            ddlAdministrasiKantorAccFrom.DataBind()
            ddlAdministrasiKantorAccFrom.SelectedIndex = intIndex

            ddlAdministrasiKantorAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("AdministrasiKantorAccTo")), intIndex)
            ddlAdministrasiKantorAccTo.DataValueField = "AccCode"
            ddlAdministrasiKantorAccTo.DataTextField = "_Description"
            ddlAdministrasiKantorAccTo.DataBind()
            ddlAdministrasiKantorAccTo.SelectedIndex = intIndex

            ddlPemerliharaanAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemerliharaanAccFrom")), intIndex)
            ddlPemerliharaanAccFrom.DataValueField = "AccCode"
            ddlPemerliharaanAccFrom.DataTextField = "_Description"
            ddlPemerliharaanAccFrom.DataBind()
            ddlPemerliharaanAccFrom.SelectedIndex = intIndex

            ddlPemerliharaanAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemerliharaanAccTo")), intIndex)
            ddlPemerliharaanAccTo.DataValueField = "AccCode"
            ddlPemerliharaanAccTo.DataTextField = "_Description"
            ddlPemerliharaanAccTo.DataBind()
            ddlPemerliharaanAccTo.SelectedIndex = intIndex

            ddlPengembanganKaryawanAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PengembanganKaryawanAccFrom")), intIndex)
            ddlPengembanganKaryawanAccFrom.DataValueField = "AccCode"
            ddlPengembanganKaryawanAccFrom.DataTextField = "_Description"
            ddlPengembanganKaryawanAccFrom.DataBind()
            ddlPengembanganKaryawanAccFrom.SelectedIndex = intIndex

            ddlPengembanganKaryawanAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PengembanganKaryawanAccTo")), intIndex)
            ddlPengembanganKaryawanAccTo.DataValueField = "AccCode"
            ddlPengembanganKaryawanAccTo.DataTextField = "_Description"
            ddlPengembanganKaryawanAccTo.DataBind()
            ddlPengembanganKaryawanAccTo.SelectedIndex = intIndex

            ddlPerjalananDinasAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PerjalananDinasAccFrom")), intIndex)
            ddlPerjalananDinasAccFrom.DataValueField = "AccCode"
            ddlPerjalananDinasAccFrom.DataTextField = "_Description"
            ddlPerjalananDinasAccFrom.DataBind()
            ddlPerjalananDinasAccFrom.SelectedIndex = intIndex

            ddlPerjalananDinasAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PerjalananDinasAccTo")), intIndex)
            ddlPerjalananDinasAccTo.DataValueField = "AccCode"
            ddlPerjalananDinasAccTo.DataTextField = "_Description"
            ddlPerjalananDinasAccTo.DataBind()
            ddlPerjalananDinasAccTo.SelectedIndex = intIndex

            ddlUnitLaboratoriumAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("UnitLaboratoriumAccFrom")), intIndex)
            ddlUnitLaboratoriumAccFrom.DataValueField = "AccCode"
            ddlUnitLaboratoriumAccFrom.DataTextField = "_Description"
            ddlUnitLaboratoriumAccFrom.DataBind()
            ddlUnitLaboratoriumAccFrom.SelectedIndex = intIndex

            ddlUnitLaboratoriumAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("UnitLaboratoriumAccTo")), intIndex)
            ddlUnitLaboratoriumAccTo.DataValueField = "AccCode"
            ddlUnitLaboratoriumAccTo.DataTextField = "_Description"
            ddlUnitLaboratoriumAccTo.DataBind()
            ddlUnitLaboratoriumAccTo.SelectedIndex = intIndex

            ddlRisetAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("RisetAccFrom")), intIndex)
            ddlRisetAccFrom.DataValueField = "AccCode"
            ddlRisetAccFrom.DataTextField = "_Description"
            ddlRisetAccFrom.DataBind()
            ddlRisetAccFrom.SelectedIndex = intIndex

            ddlRisetAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("RisetAccTo")), intIndex)
            ddlRisetAccTo.DataValueField = "AccCode"
            ddlRisetAccTo.DataTextField = "_Description"
            ddlRisetAccTo.DataBind()
            ddlRisetAccTo.SelectedIndex = intIndex

            ddlTransportasi1AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("Transportasi1AccFrom")), intIndex)
            ddlTransportasi1AccFrom.DataValueField = "AccCode"
            ddlTransportasi1AccFrom.DataTextField = "_Description"
            ddlTransportasi1AccFrom.DataBind()
            ddlTransportasi1AccFrom.SelectedIndex = intIndex

            ddlTransportasi1AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("Transportasi1AccTo")), intIndex)
            ddlTransportasi1AccTo.DataValueField = "AccCode"
            ddlTransportasi1AccTo.DataTextField = "_Description"
            ddlTransportasi1AccTo.DataBind()
            ddlTransportasi1AccTo.SelectedIndex = intIndex

            ddlTransportasi2AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("Transportasi2AccFrom")), intIndex)
            ddlTransportasi2AccFrom.DataValueField = "AccCode"
            ddlTransportasi2AccFrom.DataTextField = "_Description"
            ddlTransportasi2AccFrom.DataBind()
            ddlTransportasi2AccFrom.SelectedIndex = intIndex

            ddlTransportasi2AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("Transportasi2AccTo")), intIndex)
            ddlTransportasi2AccTo.DataValueField = "AccCode"
            ddlTransportasi2AccTo.DataTextField = "_Description"
            ddlTransportasi2AccTo.DataBind()
            ddlTransportasi2AccTo.SelectedIndex = intIndex

            ddlUmumLainyaAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("UmumLainyaAccFrom")), intIndex)
            ddlUmumLainyaAccFrom.DataValueField = "AccCode"
            ddlUmumLainyaAccFrom.DataTextField = "_Description"
            ddlUmumLainyaAccFrom.DataBind()
            ddlUmumLainyaAccFrom.SelectedIndex = intIndex

            ddlUmumLainyaAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("UmumLainyaAccTo")), intIndex)
            ddlUmumLainyaAccTo.DataValueField = "AccCode"
            ddlUmumLainyaAccTo.DataTextField = "_Description"
            ddlUmumLainyaAccTo.DataBind()
            ddlUmumLainyaAccTo.SelectedIndex = intIndex

            ddlOverheadYangDialokasiAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("OverheadYangDialokasiAccFrom")), intIndex)
            ddlOverheadYangDialokasiAccFrom.DataValueField = "AccCode"
            ddlOverheadYangDialokasiAccFrom.DataTextField = "_Description"
            ddlOverheadYangDialokasiAccFrom.DataBind()
            ddlOverheadYangDialokasiAccFrom.SelectedIndex = intIndex

            ddlOverheadYangDialokasiAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("OverheadYangDialokasiAccTo")), intIndex)
            ddlOverheadYangDialokasiAccTo.DataValueField = "AccCode"
            ddlOverheadYangDialokasiAccTo.DataTextField = "_Description"
            ddlOverheadYangDialokasiAccTo.DataBind()
            ddlOverheadYangDialokasiAccTo.SelectedIndex = intIndex

            ddlEliminasiPemakaianTBSInternAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("EliminasiPemakaianTBSInternAccFrom")), intIndex)
            ddlEliminasiPemakaianTBSInternAccFrom.DataValueField = "AccCode"
            ddlEliminasiPemakaianTBSInternAccFrom.DataTextField = "_Description"
            ddlEliminasiPemakaianTBSInternAccFrom.DataBind()
            ddlEliminasiPemakaianTBSInternAccFrom.SelectedIndex = intIndex

            ddlEliminasiPemakaianTBSInternAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("EliminasiPemakaianTBSInternAccTo")), intIndex)
            ddlEliminasiPemakaianTBSInternAccTo.DataValueField = "AccCode"
            ddlEliminasiPemakaianTBSInternAccTo.DataTextField = "_Description"
            ddlEliminasiPemakaianTBSInternAccTo.DataBind()
            ddlEliminasiPemakaianTBSInternAccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS1AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS1AccFrom")), intIndex)
            ddlBiayaProduksiTBS1AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS1AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS1AccFrom.DataBind()
            ddlBiayaProduksiTBS1AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS1AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS1AccTo")), intIndex)
            ddlBiayaProduksiTBS1AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS1AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS1AccTo.DataBind()
            ddlBiayaProduksiTBS1AccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS2AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS2AccFrom")), intIndex)
            ddlBiayaProduksiTBS2AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS2AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS2AccFrom.DataBind()
            ddlBiayaProduksiTBS2AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS2AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS2AccTo")), intIndex)
            ddlBiayaProduksiTBS2AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS2AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS2AccTo.DataBind()
            ddlBiayaProduksiTBS2AccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS3AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS3AccFrom")), intIndex)
            ddlBiayaProduksiTBS3AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS3AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS3AccFrom.DataBind()
            ddlBiayaProduksiTBS3AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS3AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS3AccTo")), intIndex)
            ddlBiayaProduksiTBS3AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS3AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS3AccTo.DataBind()
            ddlBiayaProduksiTBS3AccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS4AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS4AccFrom")), intIndex)
            ddlBiayaProduksiTBS4AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS4AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS4AccFrom.DataBind()
            ddlBiayaProduksiTBS4AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS4AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS4AccTo")), intIndex)
            ddlBiayaProduksiTBS4AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS4AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS4AccTo.DataBind()
            ddlBiayaProduksiTBS4AccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS5AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS5AccFrom")), intIndex)
            ddlBiayaProduksiTBS5AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS5AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS5AccFrom.DataBind()
            ddlBiayaProduksiTBS5AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS5AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BiayaProduksiTBS5AccTo")), intIndex)
            ddlBiayaProduksiTBS5AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS5AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS5AccTo.DataBind()
            ddlBiayaProduksiTBS5AccTo.SelectedIndex = intIndex
        Else
            objCOGSDs = BindAccount("", intIndex)

            ddlPanenDanPengumpulanAccFrom.DataSource = objCOGSDs
            ddlPanenDanPengumpulanAccFrom.DataValueField = "AccCode"
            ddlPanenDanPengumpulanAccFrom.DataTextField = "_Description"
            ddlPanenDanPengumpulanAccFrom.DataBind()
            ddlPanenDanPengumpulanAccFrom.SelectedIndex = intIndex

            ddlPanenDanPengumpulanAccTo.DataSource = objCOGSDs
            ddlPanenDanPengumpulanAccTo.DataValueField = "AccCode"
            ddlPanenDanPengumpulanAccTo.DataTextField = "_Description"
            ddlPanenDanPengumpulanAccTo.DataBind()
            ddlPanenDanPengumpulanAccTo.SelectedIndex = intIndex

            ddlPemeliharaanTMAccFrom.DataSource = objCOGSDs
            ddlPemeliharaanTMAccFrom.DataValueField = "AccCode"
            ddlPemeliharaanTMAccFrom.DataTextField = "_Description"
            ddlPemeliharaanTMAccFrom.DataBind()
            ddlPemeliharaanTMAccFrom.SelectedIndex = intIndex

            ddlPemeliharaanTMAccTo.DataSource = objCOGSDs
            ddlPemeliharaanTMAccTo.DataValueField = "AccCode"
            ddlPemeliharaanTMAccTo.DataTextField = "_Description"
            ddlPemeliharaanTMAccTo.DataBind()
            ddlPemeliharaanTMAccTo.SelectedIndex = intIndex

            ddlPemupukanTMAccFrom.DataSource = objCOGSDs
            ddlPemupukanTMAccFrom.DataValueField = "AccCode"
            ddlPemupukanTMAccFrom.DataTextField = "_Description"
            ddlPemupukanTMAccFrom.DataBind()
            ddlPemupukanTMAccFrom.SelectedIndex = intIndex

            ddlPemupukanTMAccTo.DataSource = objCOGSDs
            ddlPemupukanTMAccTo.DataValueField = "AccCode"
            ddlPemupukanTMAccTo.DataTextField = "_Description"
            ddlPemupukanTMAccTo.DataBind()
            ddlPemupukanTMAccTo.SelectedIndex = intIndex

            ddlPengolahanPabrikAccFrom.DataSource = objCOGSDs
            ddlPengolahanPabrikAccFrom.DataValueField = "AccCode"
            ddlPengolahanPabrikAccFrom.DataTextField = "_Description"
            ddlPengolahanPabrikAccFrom.DataBind()
            ddlPengolahanPabrikAccFrom.SelectedIndex = intIndex

            ddlPengolahanPabrikAccTo.DataSource = objCOGSDs
            ddlPengolahanPabrikAccTo.DataValueField = "AccCode"
            ddlPengolahanPabrikAccTo.DataTextField = "_Description"
            ddlPengolahanPabrikAccTo.DataBind()
            ddlPengolahanPabrikAccTo.SelectedIndex = intIndex

            ddlPemeliharaanPabrikAccFrom.DataSource = objCOGSDs
            ddlPemeliharaanPabrikAccFrom.DataValueField = "AccCode"
            ddlPemeliharaanPabrikAccFrom.DataTextField = "_Description"
            ddlPemeliharaanPabrikAccFrom.DataBind()
            ddlPemeliharaanPabrikAccFrom.SelectedIndex = intIndex

            ddlPemeliharaanPabrikAccTo.DataSource = objCOGSDs
            ddlPemeliharaanPabrikAccTo.DataValueField = "AccCode"
            ddlPemeliharaanPabrikAccTo.DataTextField = "_Description"
            ddlPemeliharaanPabrikAccTo.DataBind()
            ddlPemeliharaanPabrikAccTo.SelectedIndex = intIndex

            ddlPembelianTBSExternAccFrom.DataSource = objCOGSDs
            ddlPembelianTBSExternAccFrom.DataValueField = "AccCode"
            ddlPembelianTBSExternAccFrom.DataTextField = "_Description"
            ddlPembelianTBSExternAccFrom.DataBind()
            ddlPembelianTBSExternAccFrom.SelectedIndex = intIndex

            ddlPembelianTBSExternAccTo.DataSource = objCOGSDs
            ddlPembelianTBSExternAccTo.DataValueField = "AccCode"
            ddlPembelianTBSExternAccTo.DataTextField = "_Description"
            ddlPembelianTBSExternAccTo.DataBind()
            ddlPembelianTBSExternAccTo.SelectedIndex = intIndex

            ddlPemakaianInternAccFrom.DataSource = objCOGSDs
            ddlPemakaianInternAccFrom.DataValueField = "AccCode"
            ddlPemakaianInternAccFrom.DataTextField = "_Description"
            ddlPemakaianInternAccFrom.DataBind()
            ddlPemakaianInternAccFrom.SelectedIndex = intIndex

            ddlPemakaianInternAccTo.DataSource = objCOGSDs
            ddlPemakaianInternAccTo.DataValueField = "AccCode"
            ddlPemakaianInternAccTo.DataTextField = "_Description"
            ddlPemakaianInternAccTo.DataBind()
            ddlPemakaianInternAccTo.SelectedIndex = intIndex

            ddlSahamLangsungYangDijualAccFrom.DataSource = objCOGSDs
            ddlSahamLangsungYangDijualAccFrom.DataValueField = "AccCode"
            ddlSahamLangsungYangDijualAccFrom.DataTextField = "_Description"
            ddlSahamLangsungYangDijualAccFrom.DataBind()
            ddlSahamLangsungYangDijualAccFrom.SelectedIndex = intIndex

            ddlSahamLangsungYangDijualAccTo.DataSource = objCOGSDs
            ddlSahamLangsungYangDijualAccTo.DataValueField = "AccCode"
            ddlSahamLangsungYangDijualAccTo.DataTextField = "_Description"
            ddlSahamLangsungYangDijualAccTo.DataBind()
            ddlSahamLangsungYangDijualAccTo.SelectedIndex = intIndex

            ddlPenyusukTMAccFrom.DataSource = objCOGSDs
            ddlPenyusukTMAccFrom.DataValueField = "AccCode"
            ddlPenyusukTMAccFrom.DataTextField = "_Description"
            ddlPenyusukTMAccFrom.DataBind()
            ddlPenyusukTMAccFrom.SelectedIndex = intIndex

            ddlPenyusukTMAccTo.DataSource = objCOGSDs
            ddlPenyusukTMAccTo.DataValueField = "AccCode"
            ddlPenyusukTMAccTo.DataTextField = "_Description"
            ddlPenyusukTMAccTo.DataBind()
            ddlPenyusukTMAccTo.SelectedIndex = intIndex

            ddlPenyusukAKTAccFrom.DataSource = objCOGSDs
            ddlPenyusukAKTAccFrom.DataValueField = "AccCode"
            ddlPenyusukAKTAccFrom.DataTextField = "_Description"
            ddlPenyusukAKTAccFrom.DataBind()
            ddlPenyusukAKTAccFrom.SelectedIndex = intIndex

            ddlPenyusukAKTAccTo.DataSource = objCOGSDs
            ddlPenyusukAKTAccTo.DataValueField = "AccCode"
            ddlPenyusukAKTAccTo.DataTextField = "_Description"
            ddlPenyusukAKTAccTo.DataBind()
            ddlPenyusukAKTAccTo.SelectedIndex = intIndex

            ddlAlokasiSPenyusutanAccFrom.DataSource = objCOGSDs
            ddlAlokasiSPenyusutanAccFrom.DataValueField = "AccCode"
            ddlAlokasiSPenyusutanAccFrom.DataTextField = "_Description"
            ddlAlokasiSPenyusutanAccFrom.DataBind()
            ddlAlokasiSPenyusutanAccFrom.SelectedIndex = intIndex

            ddlAlokasiSPenyusutanAccTo.DataSource = objCOGSDs
            ddlAlokasiSPenyusutanAccTo.DataValueField = "AccCode"
            ddlAlokasiSPenyusutanAccTo.DataTextField = "_Description"
            ddlAlokasiSPenyusutanAccTo.DataBind()
            ddlAlokasiSPenyusutanAccTo.SelectedIndex = intIndex

            ddlKaryawanAccFrom.DataSource = objCOGSDs
            ddlKaryawanAccFrom.DataValueField = "AccCode"
            ddlKaryawanAccFrom.DataTextField = "_Description"
            ddlKaryawanAccFrom.DataBind()
            ddlKaryawanAccFrom.SelectedIndex = intIndex

            ddlKaryawanAccTo.DataSource = objCOGSDs
            ddlKaryawanAccTo.DataValueField = "AccCode"
            ddlKaryawanAccTo.DataTextField = "_Description"
            ddlKaryawanAccTo.DataBind()
            ddlKaryawanAccTo.SelectedIndex = intIndex

            ddlAdministrasiKantorAccFrom.DataSource = objCOGSDs
            ddlAdministrasiKantorAccFrom.DataValueField = "AccCode"
            ddlAdministrasiKantorAccFrom.DataTextField = "_Description"
            ddlAdministrasiKantorAccFrom.DataBind()
            ddlAdministrasiKantorAccFrom.SelectedIndex = intIndex

            ddlAdministrasiKantorAccTo.DataSource = objCOGSDs
            ddlAdministrasiKantorAccTo.DataValueField = "AccCode"
            ddlAdministrasiKantorAccTo.DataTextField = "_Description"
            ddlAdministrasiKantorAccTo.DataBind()
            ddlAdministrasiKantorAccTo.SelectedIndex = intIndex

            ddlPemerliharaanAccFrom.DataSource = objCOGSDs
            ddlPemerliharaanAccFrom.DataValueField = "AccCode"
            ddlPemerliharaanAccFrom.DataTextField = "_Description"
            ddlPemerliharaanAccFrom.DataBind()
            ddlPemerliharaanAccFrom.SelectedIndex = intIndex

            ddlPemerliharaanAccTo.DataSource = objCOGSDs
            ddlPemerliharaanAccTo.DataValueField = "AccCode"
            ddlPemerliharaanAccTo.DataTextField = "_Description"
            ddlPemerliharaanAccTo.DataBind()
            ddlPemerliharaanAccTo.SelectedIndex = intIndex

            ddlPengembanganKaryawanAccFrom.DataSource = objCOGSDs
            ddlPengembanganKaryawanAccFrom.DataValueField = "AccCode"
            ddlPengembanganKaryawanAccFrom.DataTextField = "_Description"
            ddlPengembanganKaryawanAccFrom.DataBind()
            ddlPengembanganKaryawanAccFrom.SelectedIndex = intIndex

            ddlPengembanganKaryawanAccTo.DataSource = objCOGSDs
            ddlPengembanganKaryawanAccTo.DataValueField = "AccCode"
            ddlPengembanganKaryawanAccTo.DataTextField = "_Description"
            ddlPengembanganKaryawanAccTo.DataBind()
            ddlPengembanganKaryawanAccTo.SelectedIndex = intIndex

            ddlPerjalananDinasAccFrom.DataSource = objCOGSDs
            ddlPerjalananDinasAccFrom.DataValueField = "AccCode"
            ddlPerjalananDinasAccFrom.DataTextField = "_Description"
            ddlPerjalananDinasAccFrom.DataBind()
            ddlPerjalananDinasAccFrom.SelectedIndex = intIndex

            ddlPerjalananDinasAccTo.DataSource = objCOGSDs
            ddlPerjalananDinasAccTo.DataValueField = "AccCode"
            ddlPerjalananDinasAccTo.DataTextField = "_Description"
            ddlPerjalananDinasAccTo.DataBind()
            ddlPerjalananDinasAccTo.SelectedIndex = intIndex

            ddlUnitLaboratoriumAccFrom.DataSource = objCOGSDs
            ddlUnitLaboratoriumAccFrom.DataValueField = "AccCode"
            ddlUnitLaboratoriumAccFrom.DataTextField = "_Description"
            ddlUnitLaboratoriumAccFrom.DataBind()
            ddlUnitLaboratoriumAccFrom.SelectedIndex = intIndex

            ddlUnitLaboratoriumAccTo.DataSource = objCOGSDs
            ddlUnitLaboratoriumAccTo.DataValueField = "AccCode"
            ddlUnitLaboratoriumAccTo.DataTextField = "_Description"
            ddlUnitLaboratoriumAccTo.DataBind()
            ddlUnitLaboratoriumAccTo.SelectedIndex = intIndex

            ddlRisetAccFrom.DataSource = objCOGSDs
            ddlRisetAccFrom.DataValueField = "AccCode"
            ddlRisetAccFrom.DataTextField = "_Description"
            ddlRisetAccFrom.DataBind()
            ddlRisetAccFrom.SelectedIndex = intIndex

            ddlRisetAccTo.DataSource = objCOGSDs
            ddlRisetAccTo.DataValueField = "AccCode"
            ddlRisetAccTo.DataTextField = "_Description"
            ddlRisetAccTo.DataBind()
            ddlRisetAccTo.SelectedIndex = intIndex

            ddlTransportasi1AccFrom.DataSource = objCOGSDs
            ddlTransportasi1AccFrom.DataValueField = "AccCode"
            ddlTransportasi1AccFrom.DataTextField = "_Description"
            ddlTransportasi1AccFrom.DataBind()
            ddlTransportasi1AccFrom.SelectedIndex = intIndex

            ddlTransportasi1AccTo.DataSource = objCOGSDs
            ddlTransportasi1AccTo.DataValueField = "AccCode"
            ddlTransportasi1AccTo.DataTextField = "_Description"
            ddlTransportasi1AccTo.DataBind()
            ddlTransportasi1AccTo.SelectedIndex = intIndex

            ddlTransportasi2AccFrom.DataSource = objCOGSDs
            ddlTransportasi2AccFrom.DataValueField = "AccCode"
            ddlTransportasi2AccFrom.DataTextField = "_Description"
            ddlTransportasi2AccFrom.DataBind()
            ddlTransportasi2AccFrom.SelectedIndex = intIndex

            ddlTransportasi2AccTo.DataSource = objCOGSDs
            ddlTransportasi2AccTo.DataValueField = "AccCode"
            ddlTransportasi2AccTo.DataTextField = "_Description"
            ddlTransportasi2AccTo.DataBind()
            ddlTransportasi2AccTo.SelectedIndex = intIndex

            ddlUmumLainyaAccFrom.DataSource = objCOGSDs
            ddlUmumLainyaAccFrom.DataValueField = "AccCode"
            ddlUmumLainyaAccFrom.DataTextField = "_Description"
            ddlUmumLainyaAccFrom.DataBind()
            ddlUmumLainyaAccFrom.SelectedIndex = intIndex

            ddlUmumLainyaAccTo.DataSource = objCOGSDs
            ddlUmumLainyaAccTo.DataValueField = "AccCode"
            ddlUmumLainyaAccTo.DataTextField = "_Description"
            ddlUmumLainyaAccTo.DataBind()
            ddlUmumLainyaAccTo.SelectedIndex = intIndex

            ddlOverheadYangDialokasiAccFrom.DataSource = objCOGSDs
            ddlOverheadYangDialokasiAccFrom.DataValueField = "AccCode"
            ddlOverheadYangDialokasiAccFrom.DataTextField = "_Description"
            ddlOverheadYangDialokasiAccFrom.DataBind()
            ddlOverheadYangDialokasiAccFrom.SelectedIndex = intIndex

            ddlOverheadYangDialokasiAccTo.DataSource = objCOGSDs
            ddlOverheadYangDialokasiAccTo.DataValueField = "AccCode"
            ddlOverheadYangDialokasiAccTo.DataTextField = "_Description"
            ddlOverheadYangDialokasiAccTo.DataBind()
            ddlOverheadYangDialokasiAccTo.SelectedIndex = intIndex

            ddlEliminasiPemakaianTBSInternAccFrom.DataSource = objCOGSDs
            ddlEliminasiPemakaianTBSInternAccFrom.DataValueField = "AccCode"
            ddlEliminasiPemakaianTBSInternAccFrom.DataTextField = "_Description"
            ddlEliminasiPemakaianTBSInternAccFrom.DataBind()
            ddlEliminasiPemakaianTBSInternAccFrom.SelectedIndex = intIndex

            ddlEliminasiPemakaianTBSInternAccTo.DataSource = objCOGSDs
            ddlEliminasiPemakaianTBSInternAccTo.DataValueField = "AccCode"
            ddlEliminasiPemakaianTBSInternAccTo.DataTextField = "_Description"
            ddlEliminasiPemakaianTBSInternAccTo.DataBind()
            ddlEliminasiPemakaianTBSInternAccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS1AccFrom.DataSource = objCOGSDs
            ddlBiayaProduksiTBS1AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS1AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS1AccFrom.DataBind()
            ddlBiayaProduksiTBS1AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS1AccTo.DataSource = objCOGSDs
            ddlBiayaProduksiTBS1AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS1AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS1AccTo.DataBind()
            ddlBiayaProduksiTBS1AccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS2AccFrom.DataSource = objCOGSDs
            ddlBiayaProduksiTBS2AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS2AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS2AccFrom.DataBind()
            ddlBiayaProduksiTBS2AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS2AccTo.DataSource = objCOGSDs
            ddlBiayaProduksiTBS2AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS2AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS2AccTo.DataBind()
            ddlBiayaProduksiTBS2AccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS3AccFrom.DataSource = objCOGSDs
            ddlBiayaProduksiTBS3AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS3AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS3AccFrom.DataBind()
            ddlBiayaProduksiTBS3AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS3AccTo.DataSource = objCOGSDs
            ddlBiayaProduksiTBS3AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS3AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS3AccTo.DataBind()
            ddlBiayaProduksiTBS3AccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS4AccFrom.DataSource = objCOGSDs
            ddlBiayaProduksiTBS4AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS4AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS4AccFrom.DataBind()
            ddlBiayaProduksiTBS4AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS4AccTo.DataSource = objCOGSDs
            ddlBiayaProduksiTBS4AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS4AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS4AccTo.DataBind()
            ddlBiayaProduksiTBS4AccTo.SelectedIndex = intIndex

            ddlBiayaProduksiTBS5AccFrom.DataSource = objCOGSDs
            ddlBiayaProduksiTBS5AccFrom.DataValueField = "AccCode"
            ddlBiayaProduksiTBS5AccFrom.DataTextField = "_Description"
            ddlBiayaProduksiTBS5AccFrom.DataBind()
            ddlBiayaProduksiTBS5AccFrom.SelectedIndex = intIndex

            ddlBiayaProduksiTBS5AccTo.DataSource = objCOGSDs
            ddlBiayaProduksiTBS5AccTo.DataValueField = "AccCode"
            ddlBiayaProduksiTBS5AccTo.DataTextField = "_Description"
            ddlBiayaProduksiTBS5AccTo.DataBind()
            ddlBiayaProduksiTBS5AccTo.SelectedIndex = intIndex


        End If
    End Sub

    Sub BindGLAccDropDownList(Optional ByVal pv_strCode As String = "")
        Dim strOpCode As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim objActDs As New DataSet
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                        strParam, _
                                                        objGLSetup.EnumGLMasterType.AccountCode, _
                                                        objActDs)


        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNTCODE_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objActDs.Tables(0).Rows(intCnt).Item("_Description") = objActDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("_Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select Account Code"
        objActDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPanenDanPengumpulanAccFrom.DataSource = objActDs.Tables(0)
        ddlPanenDanPengumpulanAccFrom.DataValueField = "AccCode"
        ddlPanenDanPengumpulanAccFrom.DataTextField = "_Description"
        ddlPanenDanPengumpulanAccFrom.DataBind()
        ddlPanenDanPengumpulanAccFrom.SelectedIndex = intSelectIndex

        ddlPanenDanPengumpulanAccTo.DataSource = objActDs.Tables(0)
        ddlPanenDanPengumpulanAccTo.DataValueField = "AccCode"
        ddlPanenDanPengumpulanAccTo.DataTextField = "_Description"
        ddlPanenDanPengumpulanAccTo.DataBind()
        ddlPanenDanPengumpulanAccTo.SelectedIndex = intSelectIndex

        ddlPemeliharaanTMAccFrom.DataSource = objActDs.Tables(0)
        ddlPemeliharaanTMAccFrom.DataValueField = "AccCode"
        ddlPemeliharaanTMAccFrom.DataTextField = "_Description"
        ddlPemeliharaanTMAccFrom.DataBind()
        ddlPemeliharaanTMAccFrom.SelectedIndex = intSelectIndex

        ddlPemeliharaanTMAccTo.DataSource = objActDs.Tables(0)
        ddlPemeliharaanTMAccTo.DataValueField = "AccCode"
        ddlPemeliharaanTMAccTo.DataTextField = "_Description"
        ddlPemeliharaanTMAccTo.DataBind()
        ddlPemeliharaanTMAccTo.SelectedIndex = intSelectIndex

        ddlPemupukanTMAccFrom.DataSource = objActDs.Tables(0)
        ddlPemupukanTMAccFrom.DataValueField = "AccCode"
        ddlPemupukanTMAccFrom.DataTextField = "_Description"
        ddlPemupukanTMAccFrom.DataBind()
        ddlPemupukanTMAccFrom.SelectedIndex = intSelectIndex

        ddlPemupukanTMAccTo.DataSource = objActDs.Tables(0)
        ddlPemupukanTMAccTo.DataValueField = "AccCode"
        ddlPemupukanTMAccTo.DataTextField = "_Description"
        ddlPemupukanTMAccTo.DataBind()
        ddlPemupukanTMAccTo.SelectedIndex = intSelectIndex

        ddlPengolahanPabrikAccFrom.DataSource = objActDs.Tables(0)
        ddlPengolahanPabrikAccFrom.DataValueField = "AccCode"
        ddlPengolahanPabrikAccFrom.DataTextField = "_Description"
        ddlPengolahanPabrikAccFrom.DataBind()
        ddlPengolahanPabrikAccFrom.SelectedIndex = intSelectIndex

        ddlPengolahanPabrikAccTo.DataSource = objActDs.Tables(0)
        ddlPengolahanPabrikAccTo.DataValueField = "AccCode"
        ddlPengolahanPabrikAccTo.DataTextField = "_Description"
        ddlPengolahanPabrikAccTo.DataBind()
        ddlPengolahanPabrikAccTo.SelectedIndex = intSelectIndex

        ddlPemeliharaanPabrikAccFrom.DataSource = objActDs.Tables(0)
        ddlPemeliharaanPabrikAccFrom.DataValueField = "AccCode"
        ddlPemeliharaanPabrikAccFrom.DataTextField = "_Description"
        ddlPemeliharaanPabrikAccFrom.DataBind()
        ddlPemeliharaanPabrikAccFrom.SelectedIndex = intSelectIndex

        ddlPemeliharaanPabrikAccTo.DataSource = objActDs.Tables(0)
        ddlPemeliharaanPabrikAccTo.DataValueField = "AccCode"
        ddlPemeliharaanPabrikAccTo.DataTextField = "_Description"
        ddlPemeliharaanPabrikAccTo.DataBind()
        ddlPemeliharaanPabrikAccTo.SelectedIndex = intSelectIndex

        ddlPembelianTBSExternAccFrom.DataSource = objActDs.Tables(0)
        ddlPembelianTBSExternAccFrom.DataValueField = "AccCode"
        ddlPembelianTBSExternAccFrom.DataTextField = "_Description"
        ddlPembelianTBSExternAccFrom.DataBind()
        ddlPembelianTBSExternAccFrom.SelectedIndex = intSelectIndex

        ddlPembelianTBSExternAccTo.DataSource = objActDs.Tables(0)
        ddlPembelianTBSExternAccTo.DataValueField = "AccCode"
        ddlPembelianTBSExternAccTo.DataTextField = "_Description"
        ddlPembelianTBSExternAccTo.DataBind()
        ddlPembelianTBSExternAccTo.SelectedIndex = intSelectIndex

        ddlPemakaianInternAccFrom.DataSource = objActDs.Tables(0)
        ddlPemakaianInternAccFrom.DataValueField = "AccCode"
        ddlPemakaianInternAccFrom.DataTextField = "_Description"
        ddlPemakaianInternAccFrom.DataBind()
        ddlPemakaianInternAccFrom.SelectedIndex = intSelectIndex

        ddlPemakaianInternAccTo.DataSource = objActDs.Tables(0)
        ddlPemakaianInternAccTo.DataValueField = "AccCode"
        ddlPemakaianInternAccTo.DataTextField = "_Description"
        ddlPemakaianInternAccTo.DataBind()
        ddlPemakaianInternAccTo.SelectedIndex = intSelectIndex

        ddlSahamLangsungYangDijualAccFrom.DataSource = objActDs.Tables(0)
        ddlSahamLangsungYangDijualAccFrom.DataValueField = "AccCode"
        ddlSahamLangsungYangDijualAccFrom.DataTextField = "_Description"
        ddlSahamLangsungYangDijualAccFrom.DataBind()
        ddlSahamLangsungYangDijualAccFrom.SelectedIndex = intSelectIndex

        ddlSahamLangsungYangDijualAccTo.DataSource = objActDs.Tables(0)
        ddlSahamLangsungYangDijualAccTo.DataValueField = "AccCode"
        ddlSahamLangsungYangDijualAccTo.DataTextField = "_Description"
        ddlSahamLangsungYangDijualAccTo.DataBind()
        ddlSahamLangsungYangDijualAccTo.SelectedIndex = intSelectIndex

        ddlPenyusukTMAccFrom.DataSource = objActDs.Tables(0)
        ddlPenyusukTMAccFrom.DataValueField = "AccCode"
        ddlPenyusukTMAccFrom.DataTextField = "_Description"
        ddlPenyusukTMAccFrom.DataBind()
        ddlPenyusukTMAccFrom.SelectedIndex = intSelectIndex

        ddlPenyusukTMAccTo.DataSource = objActDs.Tables(0)
        ddlPenyusukTMAccTo.DataValueField = "AccCode"
        ddlPenyusukTMAccTo.DataTextField = "_Description"
        ddlPenyusukTMAccTo.DataBind()
        ddlPenyusukTMAccTo.SelectedIndex = intSelectIndex

        ddlPenyusukAKTAccFrom.DataSource = objActDs.Tables(0)
        ddlPenyusukAKTAccFrom.DataValueField = "AccCode"
        ddlPenyusukAKTAccFrom.DataTextField = "_Description"
        ddlPenyusukAKTAccFrom.DataBind()
        ddlPenyusukAKTAccFrom.SelectedIndex = intSelectIndex

        ddlPenyusukAKTAccTo.DataSource = objActDs.Tables(0)
        ddlPenyusukAKTAccTo.DataValueField = "AccCode"
        ddlPenyusukAKTAccTo.DataTextField = "_Description"
        ddlPenyusukAKTAccTo.DataBind()
        ddlPenyusukAKTAccTo.SelectedIndex = intSelectIndex

        ddlAlokasiSPenyusutanAccFrom.DataSource = objActDs.Tables(0)
        ddlAlokasiSPenyusutanAccFrom.DataValueField = "AccCode"
        ddlAlokasiSPenyusutanAccFrom.DataTextField = "_Description"
        ddlAlokasiSPenyusutanAccFrom.DataBind()
        ddlAlokasiSPenyusutanAccFrom.SelectedIndex = intSelectIndex

        ddlAlokasiSPenyusutanAccTo.DataSource = objActDs.Tables(0)
        ddlAlokasiSPenyusutanAccTo.DataValueField = "AccCode"
        ddlAlokasiSPenyusutanAccTo.DataTextField = "_Description"
        ddlAlokasiSPenyusutanAccTo.DataBind()
        ddlAlokasiSPenyusutanAccTo.SelectedIndex = intSelectIndex

        ddlKaryawanAccFrom.DataSource = objActDs.Tables(0)
        ddlKaryawanAccFrom.DataValueField = "AccCode"
        ddlKaryawanAccFrom.DataTextField = "_Description"
        ddlKaryawanAccFrom.DataBind()
        ddlKaryawanAccFrom.SelectedIndex = intSelectIndex

        ddlKaryawanAccTo.DataSource = objActDs.Tables(0)
        ddlKaryawanAccTo.DataValueField = "AccCode"
        ddlKaryawanAccTo.DataTextField = "_Description"
        ddlKaryawanAccTo.DataBind()
        ddlKaryawanAccTo.SelectedIndex = intSelectIndex

        ddlAdministrasiKantorAccFrom.DataSource = objActDs.Tables(0)
        ddlAdministrasiKantorAccFrom.DataValueField = "AccCode"
        ddlAdministrasiKantorAccFrom.DataTextField = "_Description"
        ddlAdministrasiKantorAccFrom.DataBind()
        ddlAdministrasiKantorAccFrom.SelectedIndex = intSelectIndex

        ddlAdministrasiKantorAccTo.DataSource = objActDs.Tables(0)
        ddlAdministrasiKantorAccTo.DataValueField = "AccCode"
        ddlAdministrasiKantorAccTo.DataTextField = "_Description"
        ddlAdministrasiKantorAccTo.DataBind()
        ddlAdministrasiKantorAccTo.SelectedIndex = intSelectIndex

        ddlPemerliharaanAccFrom.DataSource = objActDs.Tables(0)
        ddlPemerliharaanAccFrom.DataValueField = "AccCode"
        ddlPemerliharaanAccFrom.DataTextField = "_Description"
        ddlPemerliharaanAccFrom.DataBind()
        ddlPemerliharaanAccFrom.SelectedIndex = intSelectIndex

        ddlPemerliharaanAccTo.DataSource = objActDs.Tables(0)
        ddlPemerliharaanAccTo.DataValueField = "AccCode"
        ddlPemerliharaanAccTo.DataTextField = "_Description"
        ddlPemerliharaanAccTo.DataBind()
        ddlPemerliharaanAccTo.SelectedIndex = intSelectIndex

        ddlPengembanganKaryawanAccFrom.DataSource = objActDs.Tables(0)
        ddlPengembanganKaryawanAccFrom.DataValueField = "AccCode"
        ddlPengembanganKaryawanAccFrom.DataTextField = "_Description"
        ddlPengembanganKaryawanAccFrom.DataBind()
        ddlPengembanganKaryawanAccFrom.SelectedIndex = intSelectIndex

        ddlPengembanganKaryawanAccTo.DataSource = objActDs.Tables(0)
        ddlPengembanganKaryawanAccTo.DataValueField = "AccCode"
        ddlPengembanganKaryawanAccTo.DataTextField = "_Description"
        ddlPengembanganKaryawanAccTo.DataBind()
        ddlPengembanganKaryawanAccTo.SelectedIndex = intSelectIndex

        ddlPerjalananDinasAccFrom.DataSource = objActDs.Tables(0)
        ddlPerjalananDinasAccFrom.DataValueField = "AccCode"
        ddlPerjalananDinasAccFrom.DataTextField = "_Description"
        ddlPerjalananDinasAccFrom.DataBind()
        ddlPerjalananDinasAccFrom.SelectedIndex = intSelectIndex

        ddlPerjalananDinasAccTo.DataSource = objActDs.Tables(0)
        ddlPerjalananDinasAccTo.DataValueField = "AccCode"
        ddlPerjalananDinasAccTo.DataTextField = "_Description"
        ddlPerjalananDinasAccTo.DataBind()
        ddlPerjalananDinasAccTo.SelectedIndex = intSelectIndex

        ddlUnitLaboratoriumAccFrom.DataSource = objActDs.Tables(0)
        ddlUnitLaboratoriumAccFrom.DataValueField = "AccCode"
        ddlUnitLaboratoriumAccFrom.DataTextField = "_Description"
        ddlUnitLaboratoriumAccFrom.DataBind()
        ddlUnitLaboratoriumAccFrom.SelectedIndex = intSelectIndex

        ddlUnitLaboratoriumAccTo.DataSource = objActDs.Tables(0)
        ddlUnitLaboratoriumAccTo.DataValueField = "AccCode"
        ddlUnitLaboratoriumAccTo.DataTextField = "_Description"
        ddlUnitLaboratoriumAccTo.DataBind()
        ddlUnitLaboratoriumAccTo.SelectedIndex = intSelectIndex

        ddlRisetAccFrom.DataSource = objActDs.Tables(0)
        ddlRisetAccFrom.DataValueField = "AccCode"
        ddlRisetAccFrom.DataTextField = "_Description"
        ddlRisetAccFrom.DataBind()
        ddlRisetAccFrom.SelectedIndex = intSelectIndex

        ddlRisetAccTo.DataSource = objActDs.Tables(0)
        ddlRisetAccTo.DataValueField = "AccCode"
        ddlRisetAccTo.DataTextField = "_Description"
        ddlRisetAccTo.DataBind()
        ddlRisetAccTo.SelectedIndex = intSelectIndex

        ddlTransportasi1AccFrom.DataSource = objActDs.Tables(0)
        ddlTransportasi1AccFrom.DataValueField = "AccCode"
        ddlTransportasi1AccFrom.DataTextField = "_Description"
        ddlTransportasi1AccFrom.DataBind()
        ddlTransportasi1AccFrom.SelectedIndex = intSelectIndex

        ddlTransportasi1AccTo.DataSource = objActDs.Tables(0)
        ddlTransportasi1AccTo.DataValueField = "AccCode"
        ddlTransportasi1AccTo.DataTextField = "_Description"
        ddlTransportasi1AccTo.DataBind()
        ddlTransportasi1AccTo.SelectedIndex = intSelectIndex

        ddlTransportasi2AccFrom.DataSource = objActDs.Tables(0)
        ddlTransportasi2AccFrom.DataValueField = "AccCode"
        ddlTransportasi2AccFrom.DataTextField = "_Description"
        ddlTransportasi2AccFrom.DataBind()
        ddlTransportasi2AccFrom.SelectedIndex = intSelectIndex

        ddlTransportasi2AccTo.DataSource = objActDs.Tables(0)
        ddlTransportasi2AccTo.DataValueField = "AccCode"
        ddlTransportasi2AccTo.DataTextField = "_Description"
        ddlTransportasi2AccTo.DataBind()
        ddlTransportasi2AccTo.SelectedIndex = intSelectIndex

        ddlUmumLainyaAccFrom.DataSource = objActDs.Tables(0)
        ddlUmumLainyaAccFrom.DataValueField = "AccCode"
        ddlUmumLainyaAccFrom.DataTextField = "_Description"
        ddlUmumLainyaAccFrom.DataBind()
        ddlUmumLainyaAccFrom.SelectedIndex = intSelectIndex

        ddlUmumLainyaAccTo.DataSource = objActDs.Tables(0)
        ddlUmumLainyaAccTo.DataValueField = "AccCode"
        ddlUmumLainyaAccTo.DataTextField = "_Description"
        ddlUmumLainyaAccTo.DataBind()
        ddlUmumLainyaAccTo.SelectedIndex = intSelectIndex

        ddlOverheadYangDialokasiAccFrom.DataSource = objActDs.Tables(0)
        ddlOverheadYangDialokasiAccFrom.DataValueField = "AccCode"
        ddlOverheadYangDialokasiAccFrom.DataTextField = "_Description"
        ddlOverheadYangDialokasiAccFrom.DataBind()
        ddlOverheadYangDialokasiAccFrom.SelectedIndex = intSelectIndex

        ddlOverheadYangDialokasiAccTo.DataSource = objActDs.Tables(0)
        ddlOverheadYangDialokasiAccTo.DataValueField = "AccCode"
        ddlOverheadYangDialokasiAccTo.DataTextField = "_Description"
        ddlOverheadYangDialokasiAccTo.DataBind()
        ddlOverheadYangDialokasiAccTo.SelectedIndex = intSelectIndex

        ddlEliminasiPemakaianTBSInternAccFrom.DataSource = objActDs.Tables(0)
        ddlEliminasiPemakaianTBSInternAccFrom.DataValueField = "AccCode"
        ddlEliminasiPemakaianTBSInternAccFrom.DataTextField = "_Description"
        ddlEliminasiPemakaianTBSInternAccFrom.DataBind()
        ddlEliminasiPemakaianTBSInternAccFrom.SelectedIndex = intSelectIndex

        ddlEliminasiPemakaianTBSInternAccTo.DataSource = objActDs.Tables(0)
        ddlEliminasiPemakaianTBSInternAccTo.DataValueField = "AccCode"
        ddlEliminasiPemakaianTBSInternAccTo.DataTextField = "_Description"
        ddlEliminasiPemakaianTBSInternAccTo.DataBind()
        ddlEliminasiPemakaianTBSInternAccTo.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS1AccFrom.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS1AccFrom.DataValueField = "AccCode"
        ddlBiayaProduksiTBS1AccFrom.DataTextField = "_Description"
        ddlBiayaProduksiTBS1AccFrom.DataBind()
        ddlBiayaProduksiTBS1AccFrom.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS1AccTo.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS1AccTo.DataValueField = "AccCode"
        ddlBiayaProduksiTBS1AccTo.DataTextField = "_Description"
        ddlBiayaProduksiTBS1AccTo.DataBind()
        ddlBiayaProduksiTBS1AccTo.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS2AccFrom.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS2AccFrom.DataValueField = "AccCode"
        ddlBiayaProduksiTBS2AccFrom.DataTextField = "_Description"
        ddlBiayaProduksiTBS2AccFrom.DataBind()
        ddlBiayaProduksiTBS2AccFrom.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS2AccTo.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS2AccTo.DataValueField = "AccCode"
        ddlBiayaProduksiTBS2AccTo.DataTextField = "_Description"
        ddlBiayaProduksiTBS2AccTo.DataBind()
        ddlBiayaProduksiTBS2AccTo.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS3AccFrom.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS3AccFrom.DataValueField = "AccCode"
        ddlBiayaProduksiTBS3AccFrom.DataTextField = "_Description"
        ddlBiayaProduksiTBS3AccFrom.DataBind()
        ddlBiayaProduksiTBS3AccFrom.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS3AccTo.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS3AccTo.DataValueField = "AccCode"
        ddlBiayaProduksiTBS3AccTo.DataTextField = "_Description"
        ddlBiayaProduksiTBS3AccTo.DataBind()
        ddlBiayaProduksiTBS3AccTo.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS4AccFrom.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS4AccFrom.DataValueField = "AccCode"
        ddlBiayaProduksiTBS4AccFrom.DataTextField = "_Description"
        ddlBiayaProduksiTBS4AccFrom.DataBind()
        ddlBiayaProduksiTBS4AccFrom.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS4AccTo.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS4AccTo.DataValueField = "AccCode"
        ddlBiayaProduksiTBS4AccTo.DataTextField = "_Description"
        ddlBiayaProduksiTBS4AccTo.DataBind()
        ddlBiayaProduksiTBS4AccTo.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS5AccFrom.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS5AccFrom.DataValueField = "AccCode"
        ddlBiayaProduksiTBS5AccFrom.DataTextField = "_Description"
        ddlBiayaProduksiTBS5AccFrom.DataBind()
        ddlBiayaProduksiTBS5AccFrom.SelectedIndex = intSelectIndex

        ddlBiayaProduksiTBS5AccTo.DataSource = objActDs.Tables(0)
        ddlBiayaProduksiTBS5AccTo.DataValueField = "AccCode"
        ddlBiayaProduksiTBS5AccTo.DataTextField = "_Description"
        ddlBiayaProduksiTBS5AccTo.DataBind()
        ddlBiayaProduksiTBS5AccTo.SelectedIndex = intSelectIndex

    End Sub

End Class
