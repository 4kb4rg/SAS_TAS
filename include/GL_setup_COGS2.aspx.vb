
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

Public Class GL_setup_COGS2 : Inherits Page

    Protected WithEvents lblErrMessage As Label 

    Protected WithEvents ddlPersediaanAwalCrudePalmOilAccFrom As DropDownList
    Protected WithEvents ddlPersediaanAwalCrudePalmOilAccTo As DropDownList
    Protected WithEvents ddlPersediaanAwalIntiSawitAccFrom As DropDownList
    Protected WithEvents ddlPersediaanAwalIntiSawitAccTo As DropDownList
    Protected WithEvents ddlPemakaianBarangJadiCrudePalmOilAccFrom As DropDownList
    Protected WithEvents ddlPemakaianBarangJadiCrudePalmOilAccTo As DropDownList
    Protected WithEvents ddlPemakaianBarangJadiIntiSawitAccFrom As DropDownList
    Protected WithEvents ddlPemakaianBarangJadiIntiSawitAccTo As DropDownList
    Protected WithEvents ddlBahanLangsungYangDijualCrudePalmOilAccFrom As DropDownList
    Protected WithEvents ddlBahanLangsungYangDijualCrudePalmOilAccTo As DropDownList
    Protected WithEvents ddlBahanLangsungYangDijualIntiSawitAccFrom As DropDownList
    Protected WithEvents ddlBahanLangsungYangDijualIntiSawitAccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualIntiSawit1AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualIntiSawit2AccTo As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokProdukUTKDijualIntiSawit3AccTo As DropDownList
    Protected WithEvents ddlPersediaanAkhirCrudePalmOilAccFrom As DropDownList
    Protected WithEvents ddlPersediaanAkhirCrudePalmOilAccTo As DropDownList
    Protected WithEvents ddlPersediaanAkhirIntiSawitAccFrom As DropDownList
    Protected WithEvents ddlPersediaanAkhirIntiSawitAccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS1AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS1AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS2AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS2AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS3AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS3AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS4AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS4AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS5AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanTBS5AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil1AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil1AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil2AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil2AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil3AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil3AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil4AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil4AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil5AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil5AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil6AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil6AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil7AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil7AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil8AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanCrudePalmOil8AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanIntiSawit1AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanIntiSawit1AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanIntiSawit2AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanIntiSawit2AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanIntiSawit3AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanIntiSawit3AccTo As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanIntiSawit4AccFrom As DropDownList
    Protected WithEvents ddlBebanPokokPenjualanIntiSawit4AccTo As DropDownList

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

    Sub Page_Load(Sender As Object, E As EventArgs)
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

            If objAdmin.mtdModuleCheck(intModuleActivate, objGlobal.mtdGetModuleCode(objGlobal.EnumModule.GeneralLedger)) = True Then
             
                ddlPersediaanAwalCrudePalmOilAccFrom.Enabled = True
                ddlPersediaanAwalCrudePalmOilAccTo.Enabled = True
                ddlPersediaanAwalIntiSawitAccFrom.Enabled = True
                ddlPersediaanAwalIntiSawitAccTo.Enabled = True
                ddlPemakaianBarangJadiCrudePalmOilAccFrom.Enabled = True
                ddlPemakaianBarangJadiCrudePalmOilAccTo.Enabled = True
                ddlPemakaianBarangJadiIntiSawitAccFrom.Enabled = True
                ddlPemakaianBarangJadiIntiSawitAccTo.Enabled = True
                ddlBahanLangsungYangDijualCrudePalmOilAccFrom.Enabled = True
                ddlBahanLangsungYangDijualCrudePalmOilAccTo.Enabled = True
                ddlBahanLangsungYangDijualIntiSawitAccFrom.Enabled = True
                ddlBahanLangsungYangDijualIntiSawitAccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.Enabled = True
                ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.Enabled = True
                ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.Enabled = True
                ddlPersediaanAkhirCrudePalmOilAccFrom.Enabled = True
                ddlPersediaanAkhirCrudePalmOilAccTo.Enabled = True
                ddlPersediaanAkhirIntiSawitAccFrom.Enabled = True
                ddlPersediaanAkhirIntiSawitAccTo.Enabled = True
                ddlBebanPokokPenjualanTBS1AccFrom.Enabled = True
                ddlBebanPokokPenjualanTBS1AccTo.Enabled = True
                ddlBebanPokokPenjualanTBS2AccFrom.Enabled = True
                ddlBebanPokokPenjualanTBS2AccTo.Enabled = True
                ddlBebanPokokPenjualanTBS3AccFrom.Enabled = True
                ddlBebanPokokPenjualanTBS3AccTo.Enabled = True
                ddlBebanPokokPenjualanTBS4AccFrom.Enabled = True
                ddlBebanPokokPenjualanTBS4AccTo.Enabled = True
                ddlBebanPokokPenjualanTBS5AccFrom.Enabled = True
                ddlBebanPokokPenjualanTBS5AccTo.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil1AccFrom.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil1AccTo.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil2AccFrom.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil2AccTo.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil3AccFrom.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil3AccTo.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil4AccFrom.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil4AccTo.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil5AccFrom.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil5AccTo.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil6AccFrom.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil6AccTo.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil7AccFrom.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil7AccTo.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil8AccFrom.Enabled = True
                ddlBebanPokokPenjualanCrudePalmOil8AccTo.Enabled = True
                ddlBebanPokokPenjualanIntiSawit1AccFrom.Enabled = True
                ddlBebanPokokPenjualanIntiSawit1AccTo.Enabled = True
                ddlBebanPokokPenjualanIntiSawit2AccFrom.Enabled = True
                ddlBebanPokokPenjualanIntiSawit2AccTo.Enabled = True
                ddlBebanPokokPenjualanIntiSawit3AccFrom.Enabled = True
                ddlBebanPokokPenjualanIntiSawit3AccTo.Enabled = True
                ddlBebanPokokPenjualanIntiSawit4AccFrom.Enabled = True
                ddlBebanPokokPenjualanIntiSawit4AccTo.Enabled = True

				ddlPersediaanAwalCrudePalmOilAccFrom.Visible = True
				ddlPersediaanAwalCrudePalmOilAccTo.Visible = True
				ddlPersediaanAwalIntiSawitAccFrom.Visible = True
				ddlPersediaanAwalIntiSawitAccTo.Visible = True
				ddlPemakaianBarangJadiCrudePalmOilAccFrom.Visible = True
				ddlPemakaianBarangJadiCrudePalmOilAccTo.Visible = True
				ddlPemakaianBarangJadiIntiSawitAccFrom.Visible = True
				ddlPemakaianBarangJadiIntiSawitAccTo.Visible = True
				ddlBahanLangsungYangDijualCrudePalmOilAccFrom.Visible = True
				ddlBahanLangsungYangDijualCrudePalmOilAccTo.Visible = True
				ddlBahanLangsungYangDijualIntiSawitAccFrom.Visible = True
				ddlBahanLangsungYangDijualIntiSawitAccTo.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.Visible = True
				ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.Visible = True
				ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.Visible = True
				ddlPersediaanAkhirCrudePalmOilAccFrom.Visible = True
				ddlPersediaanAkhirCrudePalmOilAccTo.Visible = True
				ddlPersediaanAkhirIntiSawitAccFrom.Visible = True
				ddlPersediaanAkhirIntiSawitAccTo.Visible = True
				ddlBebanPokokPenjualanTBS1AccFrom.Visible = True
				ddlBebanPokokPenjualanTBS1AccTo.Visible = True
				ddlBebanPokokPenjualanTBS2AccFrom.Visible = True
				ddlBebanPokokPenjualanTBS2AccTo.Visible = True
				ddlBebanPokokPenjualanTBS3AccFrom.Visible = True
				ddlBebanPokokPenjualanTBS3AccTo.Visible = True
				ddlBebanPokokPenjualanTBS4AccFrom.Visible = True
				ddlBebanPokokPenjualanTBS4AccTo.Visible = True
				ddlBebanPokokPenjualanTBS5AccFrom.Visible = True
				ddlBebanPokokPenjualanTBS5AccTo.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil1AccFrom.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil1AccTo.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil2AccFrom.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil2AccTo.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil3AccFrom.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil3AccTo.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil4AccFrom.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil4AccTo.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil5AccFrom.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil5AccTo.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil6AccFrom.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil6AccTo.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil7AccFrom.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil7AccTo.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil8AccFrom.Visible = True
				ddlBebanPokokPenjualanCrudePalmOil8AccTo.Visible = True
				ddlBebanPokokPenjualanIntiSawit1AccFrom.Visible = True
				ddlBebanPokokPenjualanIntiSawit1AccTo.Visible = True
				ddlBebanPokokPenjualanIntiSawit2AccFrom.Visible = True
				ddlBebanPokokPenjualanIntiSawit2AccTo.Visible = True
				ddlBebanPokokPenjualanIntiSawit3AccFrom.Visible = True
				ddlBebanPokokPenjualanIntiSawit3AccTo.Visible = True
				ddlBebanPokokPenjualanIntiSawit4AccFrom.Visible = True
				ddlBebanPokokPenjualanIntiSawit4AccTo.Visible = True

            End If

            If Not IsPostBack Then
                BindGLAccDropDownList() 
                onLoad_Display()                
            End If
        End If
    End Sub

    Function BindAccount(ByVal pv_strAccCode As String, ByRef pv_intIndex As Integer) As Dataset
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=ws/setup/ws_workcodedet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        GetCaption = ""
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                Exit For
            End If
        Next
    End Function

    Function BindAllAccount(ByVal pv_strAccCode As String, ByRef pv_intIndex As Integer) As Dataset
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=")
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

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "GL_CLSSETUP_COGSSETUP2_ADD"
        Dim strOpCd_Upd As String = "GL_CLSSETUP_COGSSETUP2_UPD"
        Dim strOpCd As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim strParam As String = ""




        If strCmdArgs = "Save" Then
            strOpCd = strOpCd_Upd
            strParam =  ddlPersediaanAwalCrudePalmOilAccFrom.SelectedItem.Value & Chr(9) & _
			            ddlPersediaanAwalCrudePalmOilAccTo.SelectedItem.Value & Chr(9) & _
			            ddlPersediaanAwalIntiSawitAccFrom.SelectedItem.Value & Chr(9) & _
			            ddlPersediaanAwalIntiSawitAccTo.SelectedItem.Value & Chr(9) & _
			            ddlPemakaianBarangJadiCrudePalmOilAccFrom.SelectedItem.Value & Chr(9) & _
			            ddlPemakaianBarangJadiCrudePalmOilAccTo.SelectedItem.Value & Chr(9) & _
			            ddlPemakaianBarangJadiIntiSawitAccFrom.SelectedItem.Value & Chr(9) & _
			            ddlPemakaianBarangJadiIntiSawitAccTo.SelectedItem.Value & Chr(9) & _
			            ddlBahanLangsungYangDijualCrudePalmOilAccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBahanLangsungYangDijualCrudePalmOilAccTo.SelectedItem.Value & Chr(9) & _
			            ddlBahanLangsungYangDijualIntiSawitAccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBahanLangsungYangDijualIntiSawitAccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.SelectedItem.Value & Chr(9) & _
			            ddlPersediaanAkhirCrudePalmOilAccFrom.SelectedItem.Value & Chr(9) & _
			            ddlPersediaanAkhirCrudePalmOilAccTo.SelectedItem.Value & Chr(9) & _
			            ddlPersediaanAkhirIntiSawitAccFrom.SelectedItem.Value & Chr(9) & _
			            ddlPersediaanAkhirIntiSawitAccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS1AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS1AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS2AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS2AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS3AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS3AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS4AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS4AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS5AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanTBS5AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil1AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil1AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil2AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil2AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil3AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil3AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil4AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil4AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil5AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil5AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil6AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil6AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil7AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil7AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil8AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanCrudePalmOil8AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanIntiSawit1AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanIntiSawit1AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanIntiSawit2AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanIntiSawit2AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanIntiSawit3AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanIntiSawit3AccTo.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanIntiSawit4AccFrom.SelectedItem.Value & Chr(9) & _
			            ddlBebanPokokPenjualanIntiSawit4AccTo.SelectedItem.Value & Chr(9)
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_COGSSETUP2_UPD&errmesg=" & Exp.ToString & "&redirect=GL/setup/GL_setup_COGS.aspx")
            End Try
        End If

        onLoad_Display()
    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_COGSSETUP_GET"
        Dim intErrNo As Integer
        Dim intIndex As Integer
        Dim strParam As String

        Dim objCOGSDs As New Object()

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

            ddlPersediaanAwalCrudePalmOilAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PersediaanAwalCrudePalmOilAccFrom")), intIndex)
			ddlPersediaanAwalCrudePalmOilAccFrom.DataValueField = "AccCode"
			ddlPersediaanAwalCrudePalmOilAccFrom.DataTextField = "_Description"
			ddlPersediaanAwalCrudePalmOilAccFrom.DataBind()
			ddlPersediaanAwalCrudePalmOilAccFrom.SelectedIndex = intIndex
			
			ddlPersediaanAwalCrudePalmOilAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PersediaanAwalCrudePalmOilAccTo")), intIndex)
			ddlPersediaanAwalCrudePalmOilAccTo.DataValueField = "AccCode"
			ddlPersediaanAwalCrudePalmOilAccTo.DataTextField = "_Description"
			ddlPersediaanAwalCrudePalmOilAccTo.DataBind()
			ddlPersediaanAwalCrudePalmOilAccTo.SelectedIndex = intIndex
			
			ddlPersediaanAwalIntiSawitAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PersediaanAwalIntiSawitAccFrom")), intIndex)
			ddlPersediaanAwalIntiSawitAccFrom.DataValueField = "AccCode"
			ddlPersediaanAwalIntiSawitAccFrom.DataTextField = "_Description"
			ddlPersediaanAwalIntiSawitAccFrom.DataBind()
			ddlPersediaanAwalIntiSawitAccFrom.SelectedIndex = intIndex
			
			ddlPersediaanAwalIntiSawitAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PersediaanAwalIntiSawitAccTo")), intIndex)
			ddlPersediaanAwalIntiSawitAccTo.DataValueField = "AccCode"
			ddlPersediaanAwalIntiSawitAccTo.DataTextField = "_Description"
			ddlPersediaanAwalIntiSawitAccTo.DataBind()
			ddlPersediaanAwalIntiSawitAccTo.SelectedIndex = intIndex
			
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemakaianBarangJadiCrudePalmOilAccFrom")), intIndex)
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataValueField = "AccCode"
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataTextField = "_Description"
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataBind()
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.SelectedIndex = intIndex
			
			ddlPemakaianBarangJadiCrudePalmOilAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemakaianBarangJadiCrudePalmOilAccTo")), intIndex)
			ddlPemakaianBarangJadiCrudePalmOilAccTo.DataValueField = "AccCode"
			ddlPemakaianBarangJadiCrudePalmOilAccTo.DataTextField = "_Description"
			ddlPemakaianBarangJadiCrudePalmOilAccTo.DataBind()
			ddlPemakaianBarangJadiCrudePalmOilAccTo.SelectedIndex = intIndex
			
			ddlPemakaianBarangJadiIntiSawitAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemakaianBarangJadiIntiSawitAccFrom")), intIndex)
			ddlPemakaianBarangJadiIntiSawitAccFrom.DataValueField = "AccCode"
			ddlPemakaianBarangJadiIntiSawitAccFrom.DataTextField = "_Description"
			ddlPemakaianBarangJadiIntiSawitAccFrom.DataBind()
			ddlPemakaianBarangJadiIntiSawitAccFrom.SelectedIndex = intIndex
			
			ddlPemakaianBarangJadiIntiSawitAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PemakaianBarangJadiIntiSawitAccTo")), intIndex)
			ddlPemakaianBarangJadiIntiSawitAccTo.DataValueField = "AccCode"
			ddlPemakaianBarangJadiIntiSawitAccTo.DataTextField = "_Description"
			ddlPemakaianBarangJadiIntiSawitAccTo.DataBind()
			ddlPemakaianBarangJadiIntiSawitAccTo.SelectedIndex = intIndex
			
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BahanLangsungYangDijualCrudePalmOilAccFrom")), intIndex)
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataValueField = "AccCode"
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataTextField = "_Description"
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataBind()
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.SelectedIndex = intIndex
			
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BahanLangsungYangDijualCrudePalmOilAccTo")), intIndex)
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataValueField = "AccCode"
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataTextField = "_Description"
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataBind()
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.SelectedIndex = intIndex
			
			ddlBahanLangsungYangDijualIntiSawitAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BahanLangsungYangDijualIntiSawitAccFrom")), intIndex)
			ddlBahanLangsungYangDijualIntiSawitAccFrom.DataValueField = "AccCode"
			ddlBahanLangsungYangDijualIntiSawitAccFrom.DataTextField = "_Description"
			ddlBahanLangsungYangDijualIntiSawitAccFrom.DataBind()
			ddlBahanLangsungYangDijualIntiSawitAccFrom.SelectedIndex = intIndex
			
			ddlBahanLangsungYangDijualIntiSawitAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BahanLangsungYangDijualIntiSawitAccTo")), intIndex)
			ddlBahanLangsungYangDijualIntiSawitAccTo.DataValueField = "AccCode"
			ddlBahanLangsungYangDijualIntiSawitAccTo.DataTextField = "_Description"
			ddlBahanLangsungYangDijualIntiSawitAccTo.DataBind()
			ddlBahanLangsungYangDijualIntiSawitAccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil1AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil1AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil2AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil2AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil3AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil3AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil4AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil4AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil5AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil5AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil6AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil6AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil7AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualCrudePalmOil7AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualIntiSawit1AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualIntiSawit1AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualIntiSawit2AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualIntiSawit2AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualIntiSawit3AccFrom")), intIndex)
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokProdukUTKDijualIntiSawit3AccTo")), intIndex)
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.SelectedIndex = intIndex
			
			ddlPersediaanAkhirCrudePalmOilAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PersediaanAkhirCrudePalmOilAccFrom")), intIndex)
			ddlPersediaanAkhirCrudePalmOilAccFrom.DataValueField = "AccCode"
			ddlPersediaanAkhirCrudePalmOilAccFrom.DataTextField = "_Description"
			ddlPersediaanAkhirCrudePalmOilAccFrom.DataBind()
			ddlPersediaanAkhirCrudePalmOilAccFrom.SelectedIndex = intIndex
			
			ddlPersediaanAkhirCrudePalmOilAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PersediaanAkhirCrudePalmOilAccTo")), intIndex)
			ddlPersediaanAkhirCrudePalmOilAccTo.DataValueField = "AccCode"
			ddlPersediaanAkhirCrudePalmOilAccTo.DataTextField = "_Description"
			ddlPersediaanAkhirCrudePalmOilAccTo.DataBind()
			ddlPersediaanAkhirCrudePalmOilAccTo.SelectedIndex = intIndex
			
			ddlPersediaanAkhirIntiSawitAccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PersediaanAkhirIntiSawitAccFrom")), intIndex)
			ddlPersediaanAkhirIntiSawitAccFrom.DataValueField = "AccCode"
			ddlPersediaanAkhirIntiSawitAccFrom.DataTextField = "_Description"
			ddlPersediaanAkhirIntiSawitAccFrom.DataBind()
			ddlPersediaanAkhirIntiSawitAccFrom.SelectedIndex = intIndex
			
			ddlPersediaanAkhirIntiSawitAccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("PersediaanAkhirIntiSawitAccTo")), intIndex)
			ddlPersediaanAkhirIntiSawitAccTo.DataValueField = "AccCode"
			ddlPersediaanAkhirIntiSawitAccTo.DataTextField = "_Description"
			ddlPersediaanAkhirIntiSawitAccTo.DataBind()
			ddlPersediaanAkhirIntiSawitAccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS1AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS1AccFrom")), intIndex)
			ddlBebanPokokPenjualanTBS1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS1AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS1AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS1AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS1AccTo")), intIndex)
			ddlBebanPokokPenjualanTBS1AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS1AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS1AccTo.DataBind()
			ddlBebanPokokPenjualanTBS1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS2AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS2AccFrom")), intIndex)
			ddlBebanPokokPenjualanTBS2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS2AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS2AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS2AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS2AccTo")), intIndex)
			ddlBebanPokokPenjualanTBS2AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS2AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS2AccTo.DataBind()
			ddlBebanPokokPenjualanTBS2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS3AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS3AccFrom")), intIndex)
			ddlBebanPokokPenjualanTBS3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS3AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS3AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS3AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS3AccTo")), intIndex)
			ddlBebanPokokPenjualanTBS3AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS3AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS3AccTo.DataBind()
			ddlBebanPokokPenjualanTBS3AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS4AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS4AccFrom")), intIndex)
			ddlBebanPokokPenjualanTBS4AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS4AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS4AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS4AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS4AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS4AccTo")), intIndex)
			ddlBebanPokokPenjualanTBS4AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS4AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS4AccTo.DataBind()
			ddlBebanPokokPenjualanTBS4AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS5AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS5AccFrom")), intIndex)
			ddlBebanPokokPenjualanTBS5AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS5AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS5AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS5AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS5AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanTBS5AccTo")), intIndex)
			ddlBebanPokokPenjualanTBS5AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS5AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS5AccTo.DataBind()
			ddlBebanPokokPenjualanTBS5AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil1AccFrom")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil1AccTo")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil2AccFrom")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil2AccTo")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil3AccFrom")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil3AccTo")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil4AccFrom")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil4AccTo")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil5AccFrom")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil5AccTo")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil6AccFrom")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil6AccTo")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil7AccFrom")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil7AccTo")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil8AccFrom")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanCrudePalmOil8AccTo")), intIndex)
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit1AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanIntiSawit1AccFrom")), intIndex)
			ddlBebanPokokPenjualanIntiSawit1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit1AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit1AccFrom.DataBind()
			ddlBebanPokokPenjualanIntiSawit1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit1AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanIntiSawit1AccTo")), intIndex)
			ddlBebanPokokPenjualanIntiSawit1AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit1AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit1AccTo.DataBind()
			ddlBebanPokokPenjualanIntiSawit1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit2AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanIntiSawit2AccFrom")), intIndex)
			ddlBebanPokokPenjualanIntiSawit2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit2AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit2AccFrom.DataBind()
			ddlBebanPokokPenjualanIntiSawit2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit2AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanIntiSawit2AccTo")), intIndex)
			ddlBebanPokokPenjualanIntiSawit2AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit2AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit2AccTo.DataBind()
			ddlBebanPokokPenjualanIntiSawit2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit3AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanIntiSawit3AccFrom")), intIndex)
			ddlBebanPokokPenjualanIntiSawit3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit3AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit3AccFrom.DataBind()
			ddlBebanPokokPenjualanIntiSawit3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit3AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanIntiSawit3AccTo")), intIndex)
			ddlBebanPokokPenjualanIntiSawit3AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit3AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit3AccTo.DataBind()
			ddlBebanPokokPenjualanIntiSawit3AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit4AccFrom.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanIntiSawit4AccFrom")), intIndex)
			ddlBebanPokokPenjualanIntiSawit4AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit4AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit4AccFrom.DataBind()
			ddlBebanPokokPenjualanIntiSawit4AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit4AccTo.DataSource = BindAccount(Trim(objCOGSDs.Tables(0).Rows(0).Item("BebanPokokPenjualanIntiSawit4AccTo")), intIndex)
			ddlBebanPokokPenjualanIntiSawit4AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit4AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit4AccTo.DataBind()
			ddlBebanPokokPenjualanIntiSawit4AccTo.SelectedIndex = intIndex
        ELSE
           objCOGSDs = BindAccount("", intIndex)

			ddlPersediaanAwalCrudePalmOilAccFrom.DataSource = objCOGSDs
			ddlPersediaanAwalCrudePalmOilAccFrom.DataValueField = "AccCode"
			ddlPersediaanAwalCrudePalmOilAccFrom.DataTextField = "_Description"
			ddlPersediaanAwalCrudePalmOilAccFrom.DataBind()
			ddlPersediaanAwalCrudePalmOilAccFrom.SelectedIndex = intIndex
			
			ddlPersediaanAwalCrudePalmOilAccTo.DataSource = objCOGSDs
			ddlPersediaanAwalCrudePalmOilAccTo.DataValueField = "AccCode"
			ddlPersediaanAwalCrudePalmOilAccTo.DataTextField = "_Description"
			ddlPersediaanAwalCrudePalmOilAccTo.DataBind()
			ddlPersediaanAwalCrudePalmOilAccTo.SelectedIndex = intIndex
			
			ddlPersediaanAwalIntiSawitAccFrom.DataSource = objCOGSDs
			ddlPersediaanAwalIntiSawitAccFrom.DataValueField = "AccCode"
			ddlPersediaanAwalIntiSawitAccFrom.DataTextField = "_Description"
			ddlPersediaanAwalIntiSawitAccFrom.DataBind()
			ddlPersediaanAwalIntiSawitAccFrom.SelectedIndex = intIndex
			
			ddlPersediaanAwalIntiSawitAccTo.DataSource = objCOGSDs
			ddlPersediaanAwalIntiSawitAccTo.DataValueField = "AccCode"
			ddlPersediaanAwalIntiSawitAccTo.DataTextField = "_Description"
			ddlPersediaanAwalIntiSawitAccTo.DataBind()
			ddlPersediaanAwalIntiSawitAccTo.SelectedIndex = intIndex
			
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataSource = objCOGSDs
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataValueField = "AccCode"
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataTextField = "_Description"
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataBind()
			ddlPemakaianBarangJadiCrudePalmOilAccFrom.SelectedIndex = intIndex
			
			ddlPemakaianBarangJadiCrudePalmOilAccTo.DataSource = objCOGSDs
			ddlPemakaianBarangJadiCrudePalmOilAccTo.DataValueField = "AccCode"
			ddlPemakaianBarangJadiCrudePalmOilAccTo.DataTextField = "_Description"
			ddlPemakaianBarangJadiCrudePalmOilAccTo.DataBind()
			ddlPemakaianBarangJadiCrudePalmOilAccTo.SelectedIndex = intIndex
			
			ddlPemakaianBarangJadiIntiSawitAccFrom.DataSource = objCOGSDs
			ddlPemakaianBarangJadiIntiSawitAccFrom.DataValueField = "AccCode"
			ddlPemakaianBarangJadiIntiSawitAccFrom.DataTextField = "_Description"
			ddlPemakaianBarangJadiIntiSawitAccFrom.DataBind()
			ddlPemakaianBarangJadiIntiSawitAccFrom.SelectedIndex = intIndex
			
			ddlPemakaianBarangJadiIntiSawitAccTo.DataSource = objCOGSDs
			ddlPemakaianBarangJadiIntiSawitAccTo.DataValueField = "AccCode"
			ddlPemakaianBarangJadiIntiSawitAccTo.DataTextField = "_Description"
			ddlPemakaianBarangJadiIntiSawitAccTo.DataBind()
			ddlPemakaianBarangJadiIntiSawitAccTo.SelectedIndex = intIndex
			
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataSource = objCOGSDs
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataValueField = "AccCode"
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataTextField = "_Description"
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataBind()
			ddlBahanLangsungYangDijualCrudePalmOilAccFrom.SelectedIndex = intIndex
			
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataSource = objCOGSDs
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataValueField = "AccCode"
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataTextField = "_Description"
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataBind()
			ddlBahanLangsungYangDijualCrudePalmOilAccTo.SelectedIndex = intIndex
			
			ddlBahanLangsungYangDijualIntiSawitAccFrom.DataSource = objCOGSDs
			ddlBahanLangsungYangDijualIntiSawitAccFrom.DataValueField = "AccCode"
			ddlBahanLangsungYangDijualIntiSawitAccFrom.DataTextField = "_Description"
			ddlBahanLangsungYangDijualIntiSawitAccFrom.DataBind()
			ddlBahanLangsungYangDijualIntiSawitAccFrom.SelectedIndex = intIndex
			
			ddlBahanLangsungYangDijualIntiSawitAccTo.DataSource = objCOGSDs
			ddlBahanLangsungYangDijualIntiSawitAccTo.DataValueField = "AccCode"
			ddlBahanLangsungYangDijualIntiSawitAccTo.DataTextField = "_Description"
			ddlBahanLangsungYangDijualIntiSawitAccTo.DataBind()
			ddlBahanLangsungYangDijualIntiSawitAccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataSource = objCOGSDs
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataValueField = "AccCode"
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataTextField = "_Description"
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataBind()
			ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.SelectedIndex = intIndex
			
			ddlPersediaanAkhirCrudePalmOilAccFrom.DataSource = objCOGSDs
			ddlPersediaanAkhirCrudePalmOilAccFrom.DataValueField = "AccCode"
			ddlPersediaanAkhirCrudePalmOilAccFrom.DataTextField = "_Description"
			ddlPersediaanAkhirCrudePalmOilAccFrom.DataBind()
			ddlPersediaanAkhirCrudePalmOilAccFrom.SelectedIndex = intIndex
			
			ddlPersediaanAkhirCrudePalmOilAccTo.DataSource = objCOGSDs
			ddlPersediaanAkhirCrudePalmOilAccTo.DataValueField = "AccCode"
			ddlPersediaanAkhirCrudePalmOilAccTo.DataTextField = "_Description"
			ddlPersediaanAkhirCrudePalmOilAccTo.DataBind()
			ddlPersediaanAkhirCrudePalmOilAccTo.SelectedIndex = intIndex
			
			ddlPersediaanAkhirIntiSawitAccFrom.DataSource = objCOGSDs
			ddlPersediaanAkhirIntiSawitAccFrom.DataValueField = "AccCode"
			ddlPersediaanAkhirIntiSawitAccFrom.DataTextField = "_Description"
			ddlPersediaanAkhirIntiSawitAccFrom.DataBind()
			ddlPersediaanAkhirIntiSawitAccFrom.SelectedIndex = intIndex
			
			ddlPersediaanAkhirIntiSawitAccTo.DataSource = objCOGSDs
			ddlPersediaanAkhirIntiSawitAccTo.DataValueField = "AccCode"
			ddlPersediaanAkhirIntiSawitAccTo.DataTextField = "_Description"
			ddlPersediaanAkhirIntiSawitAccTo.DataBind()
			ddlPersediaanAkhirIntiSawitAccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS1AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS1AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS1AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS1AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS1AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS1AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS1AccTo.DataBind()
			ddlBebanPokokPenjualanTBS1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS2AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS2AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS2AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS2AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS2AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS2AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS2AccTo.DataBind()
			ddlBebanPokokPenjualanTBS2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS3AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS3AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS3AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS3AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS3AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS3AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS3AccTo.DataBind()
			ddlBebanPokokPenjualanTBS3AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS4AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS4AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS4AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS4AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS4AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS4AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS4AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS4AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS4AccTo.DataBind()
			ddlBebanPokokPenjualanTBS4AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS5AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS5AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS5AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS5AccFrom.DataBind()
			ddlBebanPokokPenjualanTBS5AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanTBS5AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanTBS5AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanTBS5AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanTBS5AccTo.DataBind()
			ddlBebanPokokPenjualanTBS5AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil3AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil4AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil4AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil5AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil5AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil6AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil6AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil7AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil7AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil8AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataBind()
			ddlBebanPokokPenjualanCrudePalmOil8AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit1AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanIntiSawit1AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit1AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit1AccFrom.DataBind()
			ddlBebanPokokPenjualanIntiSawit1AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit1AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanIntiSawit1AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit1AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit1AccTo.DataBind()
			ddlBebanPokokPenjualanIntiSawit1AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit2AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanIntiSawit2AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit2AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit2AccFrom.DataBind()
			ddlBebanPokokPenjualanIntiSawit2AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit2AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanIntiSawit2AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit2AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit2AccTo.DataBind()
			ddlBebanPokokPenjualanIntiSawit2AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit3AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanIntiSawit3AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit3AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit3AccFrom.DataBind()
			ddlBebanPokokPenjualanIntiSawit3AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit3AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanIntiSawit3AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit3AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit3AccTo.DataBind()
			ddlBebanPokokPenjualanIntiSawit3AccTo.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit4AccFrom.DataSource = objCOGSDs
			ddlBebanPokokPenjualanIntiSawit4AccFrom.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit4AccFrom.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit4AccFrom.DataBind()
			ddlBebanPokokPenjualanIntiSawit4AccFrom.SelectedIndex = intIndex
			
			ddlBebanPokokPenjualanIntiSawit4AccTo.DataSource = objCOGSDs
			ddlBebanPokokPenjualanIntiSawit4AccTo.DataValueField = "AccCode"
			ddlBebanPokokPenjualanIntiSawit4AccTo.DataTextField = "_Description"
			ddlBebanPokokPenjualanIntiSawit4AccTo.DataBind()
			ddlBebanPokokPenjualanIntiSawit4AccTo.SelectedIndex = intIndex

        END If
    END Sub

    
    Sub BindGLAccDropDownList(Optional ByVal pv_strCode as String = "")
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

        ddlPersediaanAwalCrudePalmOilAccFrom.DataSource = objActDs.Tables(0)
        ddlPersediaanAwalCrudePalmOilAccFrom.DataValueField = "AccCode"
        ddlPersediaanAwalCrudePalmOilAccFrom.DataTextField = "_Description"
        ddlPersediaanAwalCrudePalmOilAccFrom.DataBind()
        ddlPersediaanAwalCrudePalmOilAccFrom.SelectedIndex = intSelectIndex

        ddlPersediaanAwalCrudePalmOilAccTo.DataSource = objActDs.Tables(0)
        ddlPersediaanAwalCrudePalmOilAccTo.DataValueField = "AccCode"
        ddlPersediaanAwalCrudePalmOilAccTo.DataTextField = "_Description"
        ddlPersediaanAwalCrudePalmOilAccTo.DataBind()
        ddlPersediaanAwalCrudePalmOilAccTo.SelectedIndex = intSelectIndex

        ddlPersediaanAwalIntiSawitAccFrom.DataSource = objActDs.Tables(0)
        ddlPersediaanAwalIntiSawitAccFrom.DataValueField = "AccCode"
        ddlPersediaanAwalIntiSawitAccFrom.DataTextField = "_Description"
        ddlPersediaanAwalIntiSawitAccFrom.DataBind()
        ddlPersediaanAwalIntiSawitAccFrom.SelectedIndex = intSelectIndex

        ddlPersediaanAwalIntiSawitAccTo.DataSource = objActDs.Tables(0)
        ddlPersediaanAwalIntiSawitAccTo.DataValueField = "AccCode"
        ddlPersediaanAwalIntiSawitAccTo.DataTextField = "_Description"
        ddlPersediaanAwalIntiSawitAccTo.DataBind()
        ddlPersediaanAwalIntiSawitAccTo.SelectedIndex = intSelectIndex

        ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataSource = objActDs.Tables(0)
        ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataValueField = "AccCode"
        ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataTextField = "_Description"
        ddlPemakaianBarangJadiCrudePalmOilAccFrom.DataBind()
        ddlPemakaianBarangJadiCrudePalmOilAccFrom.SelectedIndex = intSelectIndex

        ddlPemakaianBarangJadiCrudePalmOilAccTo.DataSource = objActDs.Tables(0)
        ddlPemakaianBarangJadiCrudePalmOilAccTo.DataValueField = "AccCode"
        ddlPemakaianBarangJadiCrudePalmOilAccTo.DataTextField = "_Description"
        ddlPemakaianBarangJadiCrudePalmOilAccTo.DataBind()
        ddlPemakaianBarangJadiCrudePalmOilAccTo.SelectedIndex = intSelectIndex

        ddlPemakaianBarangJadiIntiSawitAccFrom.DataSource = objActDs.Tables(0)
        ddlPemakaianBarangJadiIntiSawitAccFrom.DataValueField = "AccCode"
        ddlPemakaianBarangJadiIntiSawitAccFrom.DataTextField = "_Description"
        ddlPemakaianBarangJadiIntiSawitAccFrom.DataBind()
        ddlPemakaianBarangJadiIntiSawitAccFrom.SelectedIndex = intSelectIndex

        ddlPemakaianBarangJadiIntiSawitAccTo.DataSource = objActDs.Tables(0)
        ddlPemakaianBarangJadiIntiSawitAccTo.DataValueField = "AccCode"
        ddlPemakaianBarangJadiIntiSawitAccTo.DataTextField = "_Description"
        ddlPemakaianBarangJadiIntiSawitAccTo.DataBind()
        ddlPemakaianBarangJadiIntiSawitAccTo.SelectedIndex = intSelectIndex

        ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataSource = objActDs.Tables(0)
        ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataValueField = "AccCode"
        ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataTextField = "_Description"
        ddlBahanLangsungYangDijualCrudePalmOilAccFrom.DataBind()
        ddlBahanLangsungYangDijualCrudePalmOilAccFrom.SelectedIndex = intSelectIndex

        ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataSource = objActDs.Tables(0)
        ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataValueField = "AccCode"
        ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataTextField = "_Description"
        ddlBahanLangsungYangDijualCrudePalmOilAccTo.DataBind()
        ddlBahanLangsungYangDijualCrudePalmOilAccTo.SelectedIndex = intSelectIndex

        ddlBahanLangsungYangDijualIntiSawitAccFrom.DataSource = objActDs.Tables(0)
        ddlBahanLangsungYangDijualIntiSawitAccFrom.DataValueField = "AccCode"
        ddlBahanLangsungYangDijualIntiSawitAccFrom.DataTextField = "_Description"
        ddlBahanLangsungYangDijualIntiSawitAccFrom.DataBind()
        ddlBahanLangsungYangDijualIntiSawitAccFrom.SelectedIndex = intSelectIndex

        ddlBahanLangsungYangDijualIntiSawitAccTo.DataSource = objActDs.Tables(0)
        ddlBahanLangsungYangDijualIntiSawitAccTo.DataValueField = "AccCode"
        ddlBahanLangsungYangDijualIntiSawitAccTo.DataTextField = "_Description"
        ddlBahanLangsungYangDijualIntiSawitAccTo.DataBind()
        ddlBahanLangsungYangDijualIntiSawitAccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil1AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil2AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil3AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil4AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil5AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil6AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualCrudePalmOil7AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualIntiSawit1AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualIntiSawit1AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualIntiSawit2AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualIntiSawit2AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.DataBind()
        ddlBebanPokokProdukUTKDijualIntiSawit3AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataValueField = "AccCode"
        ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataTextField = "_Description"
        ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.DataBind()
        ddlBebanPokokProdukUTKDijualIntiSawit3AccTo.SelectedIndex = intSelectIndex

        ddlPersediaanAkhirCrudePalmOilAccFrom.DataSource = objActDs.Tables(0)
        ddlPersediaanAkhirCrudePalmOilAccFrom.DataValueField = "AccCode"
        ddlPersediaanAkhirCrudePalmOilAccFrom.DataTextField = "_Description"
        ddlPersediaanAkhirCrudePalmOilAccFrom.DataBind()
        ddlPersediaanAkhirCrudePalmOilAccFrom.SelectedIndex = intSelectIndex

        ddlPersediaanAkhirCrudePalmOilAccTo.DataSource = objActDs.Tables(0)
        ddlPersediaanAkhirCrudePalmOilAccTo.DataValueField = "AccCode"
        ddlPersediaanAkhirCrudePalmOilAccTo.DataTextField = "_Description"
        ddlPersediaanAkhirCrudePalmOilAccTo.DataBind()
        ddlPersediaanAkhirCrudePalmOilAccTo.SelectedIndex = intSelectIndex

        ddlPersediaanAkhirIntiSawitAccFrom.DataSource = objActDs.Tables(0)
        ddlPersediaanAkhirIntiSawitAccFrom.DataValueField = "AccCode"
        ddlPersediaanAkhirIntiSawitAccFrom.DataTextField = "_Description"
        ddlPersediaanAkhirIntiSawitAccFrom.DataBind()
        ddlPersediaanAkhirIntiSawitAccFrom.SelectedIndex = intSelectIndex

        ddlPersediaanAkhirIntiSawitAccTo.DataSource = objActDs.Tables(0)
        ddlPersediaanAkhirIntiSawitAccTo.DataValueField = "AccCode"
        ddlPersediaanAkhirIntiSawitAccTo.DataTextField = "_Description"
        ddlPersediaanAkhirIntiSawitAccTo.DataBind()
        ddlPersediaanAkhirIntiSawitAccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS1AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS1AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS1AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS1AccFrom.DataBind()
        ddlBebanPokokPenjualanTBS1AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS1AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS1AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS1AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS1AccTo.DataBind()
        ddlBebanPokokPenjualanTBS1AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS2AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS2AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS2AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS2AccFrom.DataBind()
        ddlBebanPokokPenjualanTBS2AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS2AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS2AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS2AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS2AccTo.DataBind()
        ddlBebanPokokPenjualanTBS2AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS3AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS3AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS3AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS3AccFrom.DataBind()
        ddlBebanPokokPenjualanTBS3AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS3AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS3AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS3AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS3AccTo.DataBind()
        ddlBebanPokokPenjualanTBS3AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS4AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS4AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS4AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS4AccFrom.DataBind()
        ddlBebanPokokPenjualanTBS4AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS4AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS4AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS4AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS4AccTo.DataBind()
        ddlBebanPokokPenjualanTBS4AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS5AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS5AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS5AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS5AccFrom.DataBind()
        ddlBebanPokokPenjualanTBS5AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanTBS5AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanTBS5AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanTBS5AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanTBS5AccTo.DataBind()
        ddlBebanPokokPenjualanTBS5AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil1AccFrom.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil1AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil1AccTo.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil1AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil2AccFrom.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil2AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil2AccTo.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil2AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil3AccFrom.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil3AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil3AccTo.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil3AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil4AccFrom.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil4AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil4AccTo.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil4AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil5AccFrom.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil5AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil5AccTo.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil5AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil6AccFrom.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil6AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil6AccTo.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil6AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil7AccFrom.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil7AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil7AccTo.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil7AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil8AccFrom.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil8AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanCrudePalmOil8AccTo.DataBind()
        ddlBebanPokokPenjualanCrudePalmOil8AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanIntiSawit1AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanIntiSawit1AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanIntiSawit1AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanIntiSawit1AccFrom.DataBind()
        ddlBebanPokokPenjualanIntiSawit1AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanIntiSawit1AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanIntiSawit1AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanIntiSawit1AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanIntiSawit1AccTo.DataBind()
        ddlBebanPokokPenjualanIntiSawit1AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanIntiSawit2AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanIntiSawit2AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanIntiSawit2AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanIntiSawit2AccFrom.DataBind()
        ddlBebanPokokPenjualanIntiSawit2AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanIntiSawit2AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanIntiSawit2AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanIntiSawit2AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanIntiSawit2AccTo.DataBind()
        ddlBebanPokokPenjualanIntiSawit2AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanIntiSawit3AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanIntiSawit3AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanIntiSawit3AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanIntiSawit3AccFrom.DataBind()
        ddlBebanPokokPenjualanIntiSawit3AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanIntiSawit3AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanIntiSawit3AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanIntiSawit3AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanIntiSawit3AccTo.DataBind()
        ddlBebanPokokPenjualanIntiSawit3AccTo.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanIntiSawit4AccFrom.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanIntiSawit4AccFrom.DataValueField = "AccCode"
        ddlBebanPokokPenjualanIntiSawit4AccFrom.DataTextField = "_Description"
        ddlBebanPokokPenjualanIntiSawit4AccFrom.DataBind()
        ddlBebanPokokPenjualanIntiSawit4AccFrom.SelectedIndex = intSelectIndex

        ddlBebanPokokPenjualanIntiSawit4AccTo.DataSource = objActDs.Tables(0)
        ddlBebanPokokPenjualanIntiSawit4AccTo.DataValueField = "AccCode"
        ddlBebanPokokPenjualanIntiSawit4AccTo.DataTextField = "_Description"
        ddlBebanPokokPenjualanIntiSawit4AccTo.DataBind()
        ddlBebanPokokPenjualanIntiSawit4AccTo.SelectedIndex = intSelectIndex

    End Sub
  

End Class
