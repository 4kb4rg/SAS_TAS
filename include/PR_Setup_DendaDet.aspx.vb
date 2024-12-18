Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.PWSystem
Imports agri.Admin
Imports agri.PR
Imports agri.GL
Imports agri.GlobalHdl


Public Class PR_Setup_DendaDet : Inherits Page
    
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblUOM as Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents txtDendaCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtDendaRate As TextBox
    Protected WithEvents ddlAD As DropDownList
    Protected WithEvents ddlDendaType As DropDownList
    Protected WithEvents ddlDefAccCode As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents DendaCode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupDendaCode As Label
    Protected WithEvents lblErrAD As Label
    Protected WithEvents lblErrDendaType As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objADDs As New Object()
    Dim objADTypeDs As New Object()
    Dim objADLnDs As New Object()
    Dim objADGroupDs As New Object()
    Dim objDefAccDs As New Object()
    Dim objDefBlkDs As New Object()
    Dim objUOMDs As New Object()
    Dim objPayADDs As New Object()
    Dim objLocDs As New Object()
    Dim objLangCapDs As New Object()
    dim objVehTypeDs as New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelectedDendaCode As String = ""
    Dim intStatus As Integer
    Dim strLocType as String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRDenda), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupDendaCode.Visible = False
            lblErrAD.Visible = False
            lblErrDendaType.Visible = False
            
            strSelectedDendaCode = Trim(IIf(Request.QueryString("DendaCode") <> "", Request.QueryString("DendaCode"), Request.Form("DendaCode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedDendaCode <> "" Then
                    dendacode.Value = strSelectedDendaCode
                    onLoad_Display()
                Else
                    onLoad_BindDendaType("")
                    onLoad_BindADType("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblDesc.Text = GetCaption(objLangCap.EnumLangCap.RouteDesc)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/Setup/PR_setup_ADDet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


    Sub onLoad_BindButton()
        txtDendaCode.Enabled = False
        txtDesc.Enabled = False
        ddlAD.Enabled = False
        ddlDendaType.Enabled = False
        txtDendaRate.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objPRSetup.EnumDendaStatus.Active
                txtDesc.Enabled = True
                ddlAD.Enabled = True
                ddlDendaType.Enabled = True
                txtDendaRate.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPRSetup.EnumDendaStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtDendaCode.Enabled = True
                txtDesc.Enabled = True
                ddlAD.Enabled = True
                ddlDendaType.Enabled = True
                txtDendaRate.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_DENDA_LIST_GET"
        Dim strParam As String 
        Dim intErrNo As Integer        

        strParam = "|and D.DendaCode = '" & strSelectedDendaCode & "' and d.loccode = '" & Trim(strLocation) & "'"
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.Denda, _ 
                                           objADDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_DENDA_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objADDs.Tables(0).Rows.Count > 0 Then
            txtDendaCode.Text = objADDs.Tables(0).Rows(0).Item("DendaCode").Trim()
            txtDesc.Text = objADDs.Tables(0).Rows(0).Item("Description").Trim()
            intStatus = CInt(objADDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objADDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRSetup.mtdGetDendaStatus(objADDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objADDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objADDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objADDs.Tables(0).Rows(0).Item("UserName")
            txtDendaRate.Text = objADDs.Tables(0).Rows(0).Item("DendaRate") 
            onLoad_BindADType(objADDs.Tables(0).Rows(0).Item("ADCode").Trim())            
            onLoad_BindDendaType(objADDs.Tables(0).Rows(0).Item("DendaType").Trim())
            onLoad_BindButton()
        End If
    End Sub


    Sub onLoad_BindDendaType(ByVal pv_strDendaType As String)
        ddlDendaType.Items.Clear
        ddlDendaType.Items.Add(New ListItem("Select Denda Type", ""))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTerkontaminasi), objPRSetup.EnumPenaltyType.BrondolanTerkontaminasi))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG), objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG), objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTertinggalBlok), objPRSetup.EnumPenaltyType.BrondolanTertinggalBlok))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BrondolanTertinggalTPH), objPRSetup.EnumPenaltyType.BrondolanTertinggalTPH))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahDiperam), objPRSetup.EnumPenaltyType.BuahDiperam))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahMatangTerTinggal), objPRSetup.EnumPenaltyType.BuahMatangTerTinggal))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahMentah), objPRSetup.EnumPenaltyType.BuahMentah))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahTertinggalTPH), objPRSetup.EnumPenaltyType.BuahTertinggalTPH))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.BuahTidakLetakTPH), objPRSetup.EnumPenaltyType.BuahTidakLetakTPH))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.EmptyBunch), objPRSetup.EnumPenaltyType.EmptyBunch))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.LongStalk), objPRSetup.EnumPenaltyType.LongStalk))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.Others), objPRSetup.EnumPenaltyType.Others))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.OverPrunning), objPRSetup.EnumPenaltyType.OverPrunning))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.PanenMatahari), objPRSetup.EnumPenaltyType.PanenMatahari))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.PelepahSengkleh), objPRSetup.EnumPenaltyType.PelepahSengkleh))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.PelepahTidakSusun), objPRSetup.EnumPenaltyType.PelepahTidakSusun))
        ddlDendaType.Items.Add(New ListItem(objPRSetup.mtdGetPenaltyTypeStatus(objPRSetup.EnumPenaltyType.UnderRipe), objPRSetup.EnumPenaltyType.UnderRipe))

        If Trim(pv_strDendaType) = "" Then
            ddlDendaType.SelectedValue = ""
        Else
            ddlDendaType.SelectedValue = CInt(Trim(pv_strDendaType))
        End If
    End Sub


    Sub InsertDendaRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_DENDA_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_DENDA_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_DENDA_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = False

        If ddlDendaType.SelectedItem.Value = "" Then            
            lblErrDendaType.Visible = True
            Exit Sub
        ElseIf ddlDendaType.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        Else
            strParam = "|" & " AND D.DendaCode like '" & Trim(txtDendaCode.Text) & "%' and d.loccode = '" & Trim(strLocation) & "' "
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Get, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Denda, _
                                                objADDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DENDA_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/HR_setup_ADList.aspx")
            End Try

            If objADDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDupdendacode.Visible = True
            Else

                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                strSelectedDendaCode = Trim(txtDendaCode.Text)
                dendacode.Value = strSelectedDendaCode
                strParam = strSelectedDendaCode & "|" & _
                            Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                            ddlDendaType.SelectedItem.Value & "|" & _
                            ddlAD.SelectedItem.Value & "|" & _
                            Trim(txtDendaRate.Text) & "|" & _                                                        
                            objPRSetup.EnumDendaStatus.Active

                
                Try
                    intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strOpCd_Get, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objPRSetup.EnumPayrollMasterType.Denda, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DENDA_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/HR_setup_ADList.aspx")
                End Try
            End If
        End If
    End Sub

    Sub UpdateDendaRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_DENDA_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_DENDA_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_DENDA_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = True
        If ddlAD.SelectedItem.Value = "" Then            
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlDendaType.SelectedItem.Value = "" Then            
            lblErrDendaType.Visible = True
            Exit Sub
        Else
            strSelectedDendaCode = Trim(txtDendaCode.Text)
            dendacode.Value = strSelectedDendaCode
            strParam = strSelectedDendaCode & "|" & _
                        Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                        ddlDendaType.SelectedItem.Value & "|" & _
                        ddlAD.SelectedItem.Value & "|" & _
                        Trim(txtDendaRate.Text) & "|" & _                                                    
                        objPRSetup.EnumDendaStatus.Active
            
            Try
                intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Denda, _
                                                blnDupKey, _
                                                blnUpdate.Text)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DENDA_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/HR_setup_ADList.aspx")
            End Try            
        End If
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_AD_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_AD_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_AD_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_DENDA_STATUS_UPD"
        Dim strOpCd As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlAD.SelectedItem.Value = "" Then
            lblErrAD.Visible = True
            Exit Sub
        ElseIf ddlDendaType.SelectedItem.Value = "" Then
            lblErrDendaType.Visible = True            
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                Select Case intStatus
                    Case objPRSetup.EnumDendaStatus.Active
                        UpdateDendaRecord()
                    Case Else
                        InsertDendaRecord()
                    End Select
            ElseIf strCmdArgs = "Del" Then
                strParam = strSelectedDendaCode & "|" & objPRSetup.EnumDendaStatus.Deleted
                Try
                    intErrNo = objPRSetup.mtdUpdDenda(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DENDA_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_DendaDet.aspx?dendacode=" & strSelectedDendaCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = strSelectedDendaCode & "|" & objPRSetup.EnumDendaStatus.Active
                Try
                    intErrNo = objPRSetup.mtdUpdDenda(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_DENDA_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_DendaDet.aspx?dendacode=" & strSelectedDendaCode)
                End Try
            End If

            If strSelectedDendaCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_DendaList.aspx")
    End Sub
    

    Sub onLoad_BindADType(ByVal pv_strAD As String)
        Dim strOpCode As String = "PR_CLSSETUP_ADLIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCode, _
                                           strParam, _
                                           objADTypeDs, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_HARVINCENTIVE_AD_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try
        
        For intCnt = 0 To objADTypeDs.Tables(0).Rows.Count - 1
            objADTypeDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADTypeDs.Tables(0).Rows(intCnt).Item("ADCode"))
            objADTypeDs.Tables(0).Rows(intCnt).Item("Description") = objADTypeDs.Tables(0).Rows(intCnt).Item("ADCode") & " (" & Trim(objADTypeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objADTypeDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(pv_strAD) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objADTypeDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction Code"
        objADTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAD.DataSource = objADTypeDs.Tables(0)
        ddlAD.DataValueField = "ADCode"
        ddlAD.DataTextField = "Description"
        ddlAD.DataBind()
        ddlAD.SelectedIndex = intSelectIndex
    End Sub

    Sub OnChangeIndexAD(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strUOM As String = ""
        Dim strDendaType As String = ddlDendaType.SelectedItem.Value
          
        Select Case strDendaType
            Case objPRSetup.EnumPenaltyType.BuahMentah, _
                objPRSetup.EnumPenaltyType.UnderRipe, _
                objPRSetup.EnumPenaltyType.BuahMatangTerTinggal, _
                objPRSetup.EnumPenaltyType.BuahDiperam, _
                objPRSetup.EnumPenaltyType.PanenMatahari, _
                objPRSetup.EnumPenaltyType.PelepahSengkleh, _
                objPRSetup.EnumPenaltyType.PelepahTidakSusun, _
                objPRSetup.EnumPenaltyType.OverPrunning, _
                objPRSetup.EnumPenaltyType.BuahTidakLetakTPH, _
                objPRSetup.EnumPenaltyType.LongStalk 
                    strUOM = " Bunches"
            Case objPRSetup.EnumPenaltyType.BrondolanTerkontaminasi, _
                objPRSetup.EnumPenaltyType.BrondolanTertinggalBlok
                    strUOM = " per KG"                            
            Case objPRSetup.EnumPenaltyType.BuahTertinggalTPH, _
                objPRSetup.EnumPenaltyType.BrondolanTerkontaminasiKG, _
                objPRSetup.EnumPenaltyType.BrondolanTertinggalTPH, _
                objPRSetup.EnumPenaltyType.EmptyBunch, _
                objPRSetup.EnumPenaltyType.Others
                    strUOM = ""
            Case Else
                    strUOM = ""
        End Select
    End Sub


End Class
