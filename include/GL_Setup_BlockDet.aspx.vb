
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
Imports microsoft.VisualBasic.DateAndTime
Imports Microsoft.VisualBasic

Public Class GL_Setup_BlockDet : Inherits Page

    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents txtPlantDate As TextBox
    Protected WithEvents txtStartArea As TextBox
    Protected WithEvents txtTotalArea As TextBox
    Protected WithEvents txtBunchRatio As TextBox
    Protected WithEvents txtQuotaIncRate As TextBox
    Protected WithEvents txtQuota As TextBox
    
    Protected WithEvents txtTransferDate As TextBox
    Protected WithEvents txtPlantMaterial As TextBox
    Protected WithEvents txtHarvestStartDate As TextBox
    Protected WithEvents txtStdPerArea As TextBox
    Protected WithEvents txtInitialDate As TextBox
    Protected WithEvents txtTotalStand As TextBox

    Protected WithEvents ddlBlkGrp As DropDownList
    Protected WithEvents ddlTransferBlk As DropDownList
    Protected WithEvents ddlAreaUOM As DropDownList
    Protected WithEvents ddlYieldUOM As DropDownList
    Protected WithEvents ddlAreaAvgUOM As DropDownList
    Protected WithEvents ddlYieldAvgUOM As DropDownList
    Protected WithEvents ddlAccGrp As DropDownList
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents rbTypeMatureField As RadioButton
    Protected WithEvents rbTypeInMatureField As RadioButton
    Protected WithEvents rbTypeOff As RadioButton
    Protected WithEvents rbTypeNursery As RadioButton

    Protected WithEvents rbTypeMill As RadioButton

    Protected WithEvents TRStartArea as HtmlTableRow
    Protected WithEvents TRTotalArea as HtmlTableRow
    Protected WithEvents TRBunchRatio as HtmlTableRow
    Protected WithEvents TRDailyQuota as HtmlTableRow
    Protected WithEvents TRMaterialPlant as HtmlTableRow
    Protected WithEvents TRStdPerArea as HtmlTableRow
    Protected WithEvents TRTotalStand as HtmlTableRow
    Protected WithEvents TRGroupType as HtmlTableRow


    Protected WithEvents tblSelection As HtmlTable
    Protected WithEvents rbByHour As RadioButton
    Protected WithEvents rbByVolume As RadioButton

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblCreateLocCode As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrSelectBoth As Label
    Protected WithEvents lblErrNotSelect As Label
    Protected WithEvents lblErrPlantDate As Label  
    Protected WithEvents lblPlantDate As Label
    Protected WithEvents lblPlantDateFmt As Label
    Protected WithEvents lblTransferDate As Label
    Protected WithEvents lblTransferDateFmt As Label
    Protected WithEvents lblErrTransferDate As Label
    Protected WithEvents lblErrTransferBlk As Label
    Protected WithEvents lblErrTransferBlkDate As Label
    Protected WithEvents lblHarvStartDate As Label
    Protected WithEvents lblHarvStartDateFmt As Label
    Protected WithEvents lblErrPlantHarvDate As Label
    Protected WithEvents lblInitialDate As Label
    Protected WithEvents lblInitialDateFmt As Label
    Protected WithEvents lblErrTotalSize As Label

    Protected WithEvents lblLocType as Label

    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents btnSelDate As ImageButton
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents dgLineDet As DataGrid

    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblCreateLoc As Label
    Protected WithEvents lblBlkDesc As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblStartArea As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblTotalArea As Label
    Protected WithEvents lblQuotaInc As Label
    Protected WithEvents lblArea As Label
    Protected WithEvents lblYield As Label
    Protected WithEvents lblAreaAvg As Label
    Protected WithEvents lblYieldAvg As Label
    Protected WithEvents lblHarvestStartDate As Label
    Protected WithEvents lblStdPerArea As Label
    Protected WithEvents lblAccGrp As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblPlsSelectUOM As Label
    Protected WithEvents lblPlsSelectEither As Label
    Protected WithEvents lblPlsSelect As Label
    Protected WithEvents lblOr As Label
    Protected WithEvents lblOnly As Label
    Protected WithEvents lblErrLicSize As Label
    Protected WithEvents lblHidCostLevel As Label
    Protected WithEvents lblDateOfPlanting As Label
    Protected WithEvents lblBunchRatio As Label
    Protected WithEvents lblDailyQuota As Label
    Protected WithEvents lblTransDate As Label
    Protected WithEvents lblMeasuredBy As Label
    Protected WithEvents lblMaterialPlant As Label
    Protected WithEvents lblTotalStand as label
    Protected WithEvents lblinitialchgdate as label
    Protected WithEvents lblGroupType as label
    Protected WithEvents lblStationCap as label 
    Protected WithEvents txtStationCap as TextBox
    Protected WithEvents lblStdRunHour as label
    Protected WithEvents txtStdRunHour as TextBox

    Protected WithEvents btnSelPlantDate As Image
    Protected WithEvents btnSelTransferDate As Image
    Protected WithEvents btnSelHarvStartDate As Image
    Protected WithEvents btnSelInitialDate As Image

    Protected WithEvents rfvBlkCode As RequiredFieldValidator
    Protected WithEvents rfvBlkDesc As RequiredFieldValidator
    Protected WithEvents rfvBlkGrp As RequiredFieldValidator
    Protected WithEvents rfvAreaUOM As RequiredFieldValidator
    Protected WithEvents rfvYieldUOM As RequiredFieldValidator
    Protected WithEvents rfvAreaAvgUOM As RequiredFieldValidator
    Protected WithEvents rfvYieldAvgUOM As RequiredFieldValidator
    Protected hidRecStatus As HtmlInputHidden
    Protected hidOriBlkCode As HtmlInputHidden

    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objBDTrx As New agri.BD.clsTrx 
    Dim objLoc As New agri.Admin.clsLoc()

    Dim objBlkDs As New Object()
    Dim objBlkGrpDs As New Object()
    Dim objTransBlkDs As New Object()
    Dim objUOMDs As New Object()
    Dim objAccGrpDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkLnDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strDateFMt As String
    Dim strYieldLevel As String
    Dim strLocType as String
    Dim strLocLevel As String

    Dim strSelectedBlockCode As String = ""
    Dim intStatus As Integer
    Dim intConfig As Integer

    Protected WithEvents rbPlantedArea As RadioButton
    Protected WithEvents rbUnplantedArea As RadioButton
    Protected WithEvents rbLandClearing As RadioButton
    Protected WithEvents rbExtension As RadioButton

    Protected WithEvents ddlBlkTypeInRpt As DropDownList

    Protected WithEvents lblBlkMgrRpt As Label

    Protected WithEvents lblProcessCtrl As System.Web.UI.WebControls.Label
    Protected WithEvents rbProcessCtrlYes As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbProcessCtrlNo As System.Web.UI.WebControls.RadioButton

    Dim objAdminLoc As New agri.Admin.clsLoc()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strDateFMt = Session("SS_DATEFMT")
        intConfig = Session("SS_CONFIGSETTING")
        strYieldLevel = Session("SS_YIELDLEVEL")
        lblHidCostLevel.Text = Session("SS_COSTLEVEL")
        strLocType = Session("SS_LOCTYPE")
        strLocLevel = Session("SS_LOCLEVEL")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLBlk), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrSelectBoth.Visible = False
            lblErrNotSelect.Visible = False
            lblErrPlantDate.Visible = False 
            lblPlantDate.Visible = False
            lblPlantDateFmt.Visible = False
            lblTransferDate.Visible = False
            lblTransferDateFmt.Visible = False
            lblErrTransferDate.Visible = False
            lblErrTransferBlk.Visible = False
            lblErrTransferBlkDate.Visible = False
            lblHarvStartDate.Visible = False
            lblHarvStartDateFmt.Visible = False
            lblInitialDate.Visible = False
            lblInitialDateFmt.Visible = False
            lblErrLicSize.Visible = False
            lblErrTotalSize.Visible = False

            rbTypeNursery.Enabled = True

            strSelectedBlockCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()

            If Not IsPostBack Then
                If strSelectedBlockCode <> "" Then
                    tbcode.Value = strSelectedBlockCode
                    onLoad_Display()
                    onLoad_BindButton()
                    BindAccGrpCode()
                    BindAccount()
                    onLoad_LineDisplay()
                Else
                    BindBlockGroup("")
                    BindUOM("", "", "", "")
                    onLoad_BindButton()
                    BindAccGrpCode()
                    BindAccount()
                    BindBlkTypeInRpt()
                End If

                If Request.QueryString("tbcode") = "" Then
                    hidRecStatus.Value = "Unsaved"
                Else
                    hidRecStatus.Value = "Saved"
                    hidOriBlkCode.Value = Trim(Request.QueryString("tbcode"))
                End If
            Else
                If ddlAccount.SelectedIndex = 0 Then
                    BindAccount()
                End If

                If ddlAccGrp.SelectedIndex = 0 Then
                    BindAccGrpCode()
                End If
            End If
        End If
        'If Session("PW_EDITION") <> objSys.EnumVersionType.Enterprise Then
        '
        'End If

        If  strLocType = objLoc.EnumLocType.Mill then 
            If strLocLevel = objLoc.EnumLocLevel.HQ Then
                rbTypeOff.Checked = True
                rbTypeMill.Visible = False
                rbTypeInMatureField.Visible = False
                rbTypeMatureField.Visible = False
                rbTypeNursery.Visible = False
            Else
                rbTypeMill.Visible = True
                rbTypeMill.Checked = True
                ddlBlkTypeInRpt.SelectedValue = 1
            End If
        else 
            rbTypeMill.visible = false
            rbTypeMill.checked = false 
        end if 

        lblBunchRatio.Text = "Bunch Ratio : "
        lblDailyQuota.Text = "Daily Quota : "
        lblBlock.Text = "Transfer To " & lblBlock.Text & " : "
        lblStartArea.Text = lblStartArea.Text & " : "
        lblTransDate.Text = "Transfer Date : "
        lblQuotaInc.Text = lblQuotaInc.Text & " Rate : "
        lblMeasuredBy.Text = "Quota Is Measured By : "
        lblMaterialPlant.text = "Material Of Plant : "
        lblHarvestStartDate.Text = lblHarvestStartDate.Text & " : "
        lblStdPerArea.Text = lblStdPerArea.text & " : "
        lblTotalStand.text = "Total Stand : "
        lblinitialchgdate.Text = "Initial Charge Date : "
        lblGroupType.Text = "Group Type :* "
        lblBlkMgrRpt.Text = lblBlkMgrRpt.Text & " Type in Managerial Report:* "
        lblStationCap.Text = "Station Capacity : "
        lblStdRunHour.Text = "Standard Running Hour : "
        lblTotalArea.Text = lblTotalArea.Text & " : "

        lblProcessCtrl.Text = "Process Control : "

        EnableProperties()


    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_BLOCK_GET_BY_BLKCODE"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAreaUOM As String
        Dim strYieldUOM As String
        Dim strAreaAvgUOM As String
        Dim strYieldAvgUOM As String

        strParam = strSelectedBlockCode & "|||"

        Try
            intErrNo = objGLSetup.mtdGetBlock(strOpCd, _
                                              strLocation, _
                                              strParam, _
                                              objBlkDs, _
                                              True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_BLOCK_GET_BY_BLKCODE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        txtBlkCode.Text = Trim(objBlkDs.Tables(0).Rows(0).Item("BlkCode"))
        txtDescription.Text = Trim(objBlkDs.Tables(0).Rows(0).Item("Description"))
        txtPlantDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objBlkDs.Tables(0).Rows(0).Item("PlantingDate")))
        txtStartArea.Text = objBlkDs.Tables(0).Rows(0).Item("StartArea")
        txtTotalArea.Text = objBlkDs.Tables(0).Rows(0).Item("TotalArea")
        txtTransferDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objBlkDs.Tables(0).Rows(0).Item("TransferDate")))
        txtBunchRatio.Text = objBlkDs.Tables(0).Rows(0).Item("BunchRatio")    
        txtQuotaIncRate.Text = objBlkDs.Tables(0).Rows(0).Item("QuotaIncRate")
        txtQuota.Text = objBlkDs.Tables(0).Rows(0).Item("Quota")
        
        strAreaUOM = Trim(objBlkDs.Tables(0).Rows(0).Item("AreaUOM"))
        strYieldUOM = Trim(objBlkDs.Tables(0).Rows(0).Item("CropUOM"))
        strAreaAvgUOM = Trim(objBlkDs.Tables(0).Rows(0).Item("AreaAvgUOM"))
        strYieldAvgUOM = Trim(objBlkDs.Tables(0).Rows(0).Item("CropAvgUOM"))
        txtPlantMaterial.Text = Trim(objBlkDs.Tables(0).Rows(0).Item("PlantMaterial"))
        txtHarvestStartDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objBlkDs.Tables(0).Rows(0).Item("HarvestStartDate")))
        txtStdPerArea.Text = Trim(objBlkDs.Tables(0).Rows(0).Item("StdPerArea"))
        txtInitialDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objBlkDs.Tables(0).Rows(0).Item("InitialChargeDate"))) 
        txtTotalStand.Text = FormatNumber(objBlkDs.Tables(0).Rows(0).Item("TotalStand"), 0, True, False, False)
        txtStationCap.text = Trim(objBlkDs.Tables(0).Rows(0).Item("StationCap"))

        BindBlockGroup(Trim(objBlkDs.Tables(0).Rows(0).Item("BlkGrpCode")))
        BindTransferBlock(Trim(objBlkDs.Tables(0).Rows(0).Item("BlkCode")), Trim(objBlkDs.Tables(0).Rows(0).Item("TransferBlkCode")))
        BindUOM(strAreaUOM, strYieldUOM, strAreaAvgUOM, strYieldAvgUOM)

        lblHiddenSts.Text = Trim(objBlkDs.Tables(0).Rows(0).Item("Status"))
        intStatus = CInt(lblHiddenSts.Text)
        lblStatus.Text = objGLSetup.mtdGetBlockStatus(Trim(objBlkDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objBlkDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objBlkDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objBlkDs.Tables(0).Rows(0).Item("UserName"))
        lblCreateLocCode.Text = objBlkDs.Tables(0).Rows(0).Item("LocCode").Trim()
        Select Case CInt(objBlkDs.Tables(0).Rows(0).Item("BlkType"))
            Case objGLSetup.EnumBlockType.InMatureField
                rbTypeInMatureField.Checked = True
                rbTypeMatureField.Checked = False
                rbTypeOff.Checked = False
                rbTypeNursery.Checked = False
            Case objGLSetup.EnumBlockType.MatureField
                rbTypeInMatureField.Checked = False
                rbTypeMatureField.Checked = True
                rbTypeOff.Checked = False
                rbTypeNursery.Checked = False
            Case objGLSetup.EnumBlockType.Office
                rbTypeInMatureField.Checked = False
                rbTypeMatureField.Checked = False
                rbTypeOff.Checked = True
                rbTypeNursery.Checked = False
            Case objGLSetup.EnumBlockType.Nursery
                rbTypeInMatureField.Checked = False
                rbTypeMatureField.Checked = False
                rbTypeOff.Checked = False
                rbTypeNursery.Checked = True
        End Select

        Select Case CInt(objBlkDs.Tables(0).Rows(0).Item("QuotaMethod"))
            Case objHRTrx.EnumQuotaMethod.ByHour
                rbByHour.Checked = True
            Case Else
                rbByVolume.Checked = True
        End Select
        Select Case CInt(objBlkDs.Tables(0).Rows(0).Item("ProcessCtrl"))
            Case objGLSetup.EnumProcessControl.Yes
                rbProcessCtrlNo.Checked = False
                rbProcessCtrlYes.Checked = True
            Case objGLSetup.EnumProcessControl.No
                rbProcessCtrlNo.Checked = True
                rbProcessCtrlYes.Checked = False
        End Select

        Select Case CInt(objBlkDs.Tables(0).Rows(0).Item("GrpType"))
            Case objGLSetup.EnumGrpType.PlantedArea
                rbPlantedArea.Checked = True
                rbUnplantedArea.Checked = False
                rbLandClearing.Checked = False
                rbExtension.Checked = False
            Case objGLSetup.EnumGrpType.LandClearing
                rbPlantedArea.Checked = False
                rbUnplantedArea.Checked = False
                rbLandClearing.Checked = True
                rbExtension.Checked = False
            Case objGLSetup.EnumGrpType.UnplantedArea
                rbPlantedArea.Checked = False
                rbUnplantedArea.Checked = True
                rbLandClearing.Checked = False
                rbExtension.Checked = False
            Case objGLSetup.EnumGrpType.Extension
                rbPlantedArea.Checked = False
                rbUnplantedArea.Checked = False
                rbLandClearing.Checked = False
                rbExtension.Checked = True
        End Select

        BindBlkTypeInRpt(objBlkDs.Tables(0).Rows(0).Item("RptBlkType"))
    End Sub

    Sub BindAccGrpCode()
        Dim strOpCode As String = "GL_CLSSETUP_CHARTOFACCOUNT_ACCGRPCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSortOrder As String
        Dim strSearchExp As String

        strSortOrder = "order by AccGrpCode "

        If rbTypeNursery.Checked = True Or rbTypeInMatureField.Checked = True Then
            strSearchExp = "where Status = '" & objGLSetup.EnumBlockStatus.Active & "' " & _
                           "and accgrpcode in (select accgrpcode from gl_account where acctype = '" & objGLSetup.EnumAccountType.BalanceSheet & "' " & _
                           "and NurseryInd = '" & objGLSetup.EnumNurseryAccount.Yes & "' )" '--01062004
 	ElseIf rbTypeOff.Checked = True Then
            strSearchExp = "where Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' " & _
                                       "and accgrpcode in (select accgrpcode from gl_account where acctype IN ('" & objGLSetup.EnumAccountType.BalanceSheet & "', '" & objGLSetup.EnumAccountType.ProfitAndLost & "')) " & _
                                       "     "
        Else
            strSearchExp = "where Status = '" & objGLSetup.EnumBlockStatus.Active & "' " & _
                           "and accgrpcode in (select accgrpcode from gl_account where acctype = '" & objGLSetup.EnumAccountType.ProfitAndLost & "' " & _
                           "and NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "' and AccPurpose IN ('" & objGLSetup.EnumAccountPurpose.NonVehicle & "','" & objGLSetup.EnumAccountPurpose.Others & "') )"
        End If

        strParam = strSortOrder & "|" & strSearchExp

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   0, _
                                                   objAccGrpDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_ACCGRPCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        For intCnt = 0 To objAccGrpDs.Tables(0).Rows.Count - 1
            objAccGrpDs.Tables(0).Rows(intCnt).Item("AccGrpCode") = Trim(objAccGrpDs.Tables(0).Rows(intCnt).Item("AccGrpCode"))
            objAccGrpDs.Tables(0).Rows(intCnt).Item("Description") = objAccGrpDs.Tables(0).Rows(intCnt).Item("AccGrpCode") & " (" & Trim(objAccGrpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next

        dr = objAccGrpDs.Tables(0).NewRow()
        dr("AccGrpCode") = ""
        dr("Description") = "Select " & lblAccGrp.Text
        objAccGrpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccGrp.DataSource = objAccGrpDs.Tables(0)
        ddlAccGrp.DataValueField = "AccGrpCode"
        ddlAccGrp.DataTextField = "Description"
        ddlAccGrp.DataBind()
    End Sub


    Sub BindAccount()
        Dim strOpCd_ACC As String = "GL_CLSSETUP_BLOCK_ACCOUNT_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        If rbTypeNursery.Checked = True Or rbTypeInMatureField.Checked = True Then     
            strParam = strSelectedBlockCode & "|" & objGLSetup.EnumAccStatus.Active & "|" & _
                       objGLSetup.EnumAccountType.BalanceSheet & "|and NurseryInd = '" & objGLSetup.EnumNurseryAccount.Yes & "' "    '--01062004
        Elseif rbTypeOff.checked=true then
            strParam = strSelectedBlockCode & "|" & objGLSetup.EnumAccStatus.Active & "|" & _
                       objGLSetup.EnumAccountType.ProfitAndLost & "|and NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "' and AccPurpose IN ('"  & objGLSetup.EnumAccountPurpose.NonVehicle & "','" & objGLSetup.EnumAccountPurpose.Others & "') or (MedAccDistUse = '" & objGLSetup.EnumMedAccDist.Yes & "' or HousingAccDistUse = '" & objGLSetup.EnumHousingAccDist.Yes & "')"
        else        
            strParam = strSelectedBlockCode & "|" & objGLSetup.EnumAccStatus.Active & "|" & _
                       objGLSetup.EnumAccountType.ProfitAndLost & "|and NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "' and AccPurpose IN ('"  & objGLSetup.EnumAccountPurpose.NonVehicle & "','" & objGLSetup.EnumAccountPurpose.Others & "') and (medaccdistuse is null or housingaccdistuse is null or (medaccdistuse='" & objGLSetup.EnumMedAccDist.no & "' and housingaccdistuse='" & objGLSetup.EnumhousingAccDist.no & "'))"
                       
        End If


        Try
            intErrNo = objGLSetup.mtdGetBlock(strOpCd_ACC, _
                                              strLocation, _
                                              strParam, _
                                              objAccDs, _
                                              True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_ACCOUNT_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = objAccDs.Tables(0).Rows(intCnt).Item("AccCode") & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Select " & lblAccount.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "Description"
        ddlAccount.DataBind()

    End Sub

    Sub onLoad_LineDisplay()
        Dim strOpCd As String = "GL_CLSSETUP_BLOCK_ACCOUNTLINE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        If Trim(LCase(lblHidCostLevel.Text)) = "block" Then
            strParam = strSelectedBlockCode & "|" & objGLSetup.EnumAccStatus.Active & "||"
            Try
                intErrNo = objGLSetup.mtdGetBlock(strOpCd, _
                                                  strLocation, _
                                                  strParam, _
                                                  objBlkLnDs, _
                                                  True)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKLINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
            End Try
            dgLineDet.DataSource = objBlkLnDs.Tables(0)
            dgLineDet.DataBind()

            If ddlTransferBlk.SelectedItem.Value <> "" Or Convert.ToInt16(objBlkDs.Tables(0).Rows(0).Item("Status")) <> objGLSetup.EnumBlockStatus.Active Then
                For intCnt = 0 To dgLineDet.Items.Count - 1
                    lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                Next
            End If
        End If
    End Sub

    Sub onLoad_BindButton()
        txtBlkCode.Enabled = True
        txtDescription.Enabled = False
        txtPlantDate.Enabled = False
        txtStartArea.Enabled = False
        txtTotalArea.Enabled = False
        txtTransferDate.Enabled = False
        txtBunchRatio.Enabled = False
        txtQuota.Enabled = False
        rbByHour.Enabled = False
        rbByVolume.Enabled = False
        txtQuotaIncRate.Enabled = False
        txtPlantMaterial.Enabled = False
        txtHarvestStartDate.Enabled = False
        txtStdPerArea.Enabled = False
        rbTypeMatureField.Enabled = False
        rbTypeInMatureField.Enabled = False
        rbTypeOff.Enabled = False
        rbTypeNursery.Enabled = False
        ddlBlkGrp.Enabled = False
        ddlTransferBlk.Enabled = False
        ddlAreaUOM.Enabled = False
        ddlYieldUOM.Enabled = False
        ddlAreaAvgUOM.Enabled = False
        ddlYieldAvgUOM.Enabled = False
        tblSelection.Visible = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnSelPlantDate.Visible = False
        btnSelTransferDate.Visible = False
        btnSelHarvStartDate.Visible = False
        btnSelInitialDate.Visible = False
        txtInitialDate.Enabled = False
        txtTotalStand.Enabled = False
        rbPlantedArea.Enabled = False
        rbUnplantedArea.Enabled = False
        rbLandClearing.Enabled = False
        rbExtension.Enabled = False
        ddlBlkTypeInRpt.Enabled = False
        rbTypeMill.Enabled = False


        txtStationCap.Enabled = False
        txtStdRunHour.enabled = false 


        rbProcessCtrlNo.Enabled = False
        rbProcessCtrlYes.Enabled = False

        Select Case intStatus
            Case objGLSetup.EnumBlockStatus.Active, objGLSetup.EnumBlockStatus.Pending
                If Trim(LCase(lblHidCostLevel.Text)) = "block" Then 
                    If ddlTransferBlk.SelectedIndex = 0 Then 
                        txtDescription.Enabled = True
                        txtPlantDate.Enabled = True
                        txtStartArea.Enabled = True
                        txtTotalArea.Enabled = True
                        txtTransferDate.Enabled = True
                        txtBunchRatio.Enabled = True
                        txtQuota.Enabled = True
                        rbByHour.Enabled = True
                        rbByVolume.Enabled = True
                        rbTypeMill.Enabled = True
                        txtQuotaIncRate.Enabled = True
                        txtPlantMaterial.Enabled = True
                        txtHarvestStartDate.Enabled = True
                        txtStdPerArea.Enabled = True
                        txtInitialDate.Enabled = True
                        txtTotalStand.Enabled = True
                        ddlBlkGrp.Enabled = True
                        ddlTransferBlk.Enabled = True
                        ddlAreaUOM.Enabled = True
                        ddlYieldUOM.Enabled = True
                        ddlAreaAvgUOM.Enabled = True
                        ddlYieldAvgUOM.Enabled = True
                        SaveBtn.Visible = True
                        DelBtn.Visible = True
                        DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                        If Trim(LCase(lblHidCostLevel.Text)) = "block" Then
                            tblSelection.Visible = True
                        End If
                        btnSelPlantDate.Visible = True
                        btnSelTransferDate.Visible = True
                        btnSelHarvStartDate.Visible = True
                        btnSelInitialDate.Visible = True
                        rbPlantedArea.Enabled = True
                        rbUnplantedArea.Enabled = True
                        rbLandClearing.Enabled = True
                        rbExtension.Enabled = True
                        ddlBlkTypeInRpt.Enabled = True
                        txtStationCap.Enabled = True
                        txtStdRunHour.enabled = True 

                        rbProcessCtrlNo.Enabled = True
                        rbProcessCtrlYes.Enabled = True

                    Else
                        DelBtn.Visible = True
                        DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    End If
                End If

            Case objGLSetup.EnumBlockStatus.Deleted
                UnDelBtn.Visible = True

            Case Else
                txtBlkCode.Enabled = True
                txtDescription.Enabled = True
                txtPlantDate.Enabled = True
                txtStartArea.Enabled = True
                txtTotalArea.Enabled = True
                txtTransferDate.Enabled = True
                txtBunchRatio.Enabled = True
                txtPlantMaterial.Enabled = True
                txtHarvestStartDate.Enabled = True
                txtStdPerArea.Enabled = True
                rbTypeMatureField.Enabled = True
                rbTypeInMatureField.Enabled = True
                rbTypeOff.Enabled = True
                
                rbTypeMill.Enabled = True
                txtStationCap.Enabled = True
                txtStdRunHour.enabled = true 

                rbProcessCtrlNo.Enabled = True
                rbProcessCtrlYes.Enabled = True

                If Trim(LCase(lblHidCostLevel.Text)) = "block" Then
                    txtQuota.Enabled = True
                    rbByHour.Enabled = True
                    rbByVolume.Enabled = True
                    txtQuotaIncRate.Enabled = True
                    tblSelection.Visible = True
                End If

                'If Session("PW_EDITION") = objSys.EnumVersionType.Enterprise Then
                rbTypeNursery.Enabled = True
                'End If

                txtInitialDate.Enabled = True
                txtTotalStand.Enabled = True
                ddlBlkGrp.Enabled = True
                ddlAreaUOM.Enabled = True
                ddlYieldUOM.Enabled = True
                ddlAreaAvgUOM.Enabled = True
                ddlYieldAvgUOM.Enabled = True
                SaveBtn.Visible = True
                btnSelPlantDate.Visible = True
                btnSelTransferDate.Visible = True
                btnSelHarvStartDate.Visible = True
                btnSelInitialDate.Visible = True
                rbPlantedArea.Enabled = True
                rbUnplantedArea.Enabled = True
                rbLandClearing.Enabled = True
                rbExtension.Enabled = True
                ddlBlkTypeInRpt.Enabled = True
        End Select
    End Sub

    Sub BindBlockGroup(ByVal pv_strBlkGrpCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_BLOCKGROUP_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String

        strSort = "order by blk.BlkGrpCode"
        strSearch = "and blk.Status = '" & objGLSetup.EnumBlkGrpStatus.Active & "' and blk.LocCode = '" & strLocation & "'"

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.BlkGrp, _
                                                   objBlkGrpDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_BLOCKGROUP_GET&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        For intCnt = 0 To objBlkGrpDs.Tables(0).Rows.Count - 1
            objBlkGrpDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(objBlkGrpDs.Tables(0).Rows(intCnt).Item("BlkGrpCode"))
            objBlkGrpDs.Tables(0).Rows(intCnt).Item("Description") = objBlkGrpDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") & " (" & Trim(objBlkGrpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkGrpDs.Tables(0).Rows(intCnt).Item("BlkGrpCode") = Trim(pv_strBlkGrpCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objBlkGrpDs.Tables(0).NewRow()
        dr("BlkGrpCode") = ""
        dr("Description") = "Select " & lblBlkGrp.Text
        objBlkGrpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlkGrp.DataSource = objBlkGrpDs.Tables(0)
        ddlBlkGrp.DataValueField = "BlkGrpCode"
        ddlBlkGrp.DataTextField = "Description"
        ddlBlkGrp.DataBind()
        ddlBlkGrp.SelectedIndex = intSelectIndex
    End Sub

    Sub BindTransferBlock(ByVal pv_strBlockCode As String, ByVal pv_strTransferBlock As String)
        Dim strOpCode As String = "GL_CLSSETUP_BLOCK_TRANSFER_BLOCK_CODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String

        strParam = pv_strBlockCode & "|" & objGLSetup.EnumBlockStatus.Active & "||"

        Try
            intErrNo = objGLSetup.mtdGetBlock(strOpCode, _
                                              strLocation, _
                                              strParam, _
                                              objTransBlkDs, _
                                              True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_TRANSFERBLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        For intCnt = 0 To objTransBlkDs.Tables(0).Rows.Count - 1
            objTransBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objTransBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objTransBlkDs.Tables(0).Rows(intCnt).Item("Description") = objTransBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & Trim(objTransBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objTransBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strTransferBlock) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objTransBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select " & lblBlkCode.Text
        objTransBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTransferBlk.DataSource = objTransBlkDs.Tables(0)
        ddlTransferBlk.DataValueField = "BlkCode"
        ddlTransferBlk.DataTextField = "Description"
        ddlTransferBlk.DataBind()
        ddlTransferBlk.SelectedIndex = intSelectIndex
    End Sub


    Sub BindUOM(ByVal pv_strArea As String, ByVal pv_strYield As String, ByVal pv_strAreaAvg As String, ByVal pv_strYieldAvg As String)
        Dim strOpCode As String = "GL_CLSSETUP_SUBACTIVITY_UOM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intAreaIndex As Integer = 0
        Dim intYieldIndex As Integer = 0
        Dim intAreaAvgIndex As Integer = 0
        Dim intYieldAvgIndex As Integer = 0

        strParam = "" & "|" & "" & "|"

        Try
            intErrNo = objAdmin.mtdGetUOM(strOpCode, _
                                          strParam, _
                                          objUOMDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_UOMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        For intCnt = 0 To objUOMDs.Tables(0).Rows.Count - 1
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode"))
            objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc") = objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") & " (" & Trim(objUOMDs.Tables(0).Rows(intCnt).Item("UOMDesc")) & ")"
            If objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strArea) Then
                intAreaIndex = intCnt + 1
            End If
            If objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strYield) Then
                intYieldIndex = intCnt + 1
            End If
            If objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strAreaAvg) Then
                intAreaAvgIndex = intCnt + 1
            End If
            If objUOMDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(pv_strYieldAvg) Then
                intYieldAvgIndex = intCnt + 1
            End If
        Next

        dr = objUOMDs.Tables(0).NewRow()
        dr("UOMCode") = ""
        dr("UOMDesc") = "Select Unit of Measurement"
        objUOMDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAreaUOM.DataSource = objUOMDs.Tables(0)
        ddlAreaUOM.DataValueField = "UOMCode"
        ddlAreaUOM.DataTextField = "UOMDesc"
        ddlAreaUOM.DataBind()
        ddlAreaUOM.SelectedIndex = intAreaIndex

        ddlYieldUOM.DataSource = objUOMDs.Tables(0)
        ddlYieldUOM.DataValueField = "UOMCode"
        ddlYieldUOM.DataTextField = "UOMDesc"
        ddlYieldUOM.DataBind()
        ddlYieldUOM.SelectedIndex = intYieldIndex

        ddlAreaAvgUOM.DataSource = objUOMDs.Tables(0)
        ddlAreaAvgUOM.DataValueField = "UOMCode"
        ddlAreaAvgUOM.DataTextField = "UOMDesc"
        ddlAreaAvgUOM.DataBind()
        ddlAreaAvgUOM.SelectedIndex = intAreaAvgIndex

        ddlYieldAvgUOM.DataSource = objUOMDs.Tables(0)
        ddlYieldAvgUOM.DataValueField = "UOMCode"
        ddlYieldAvgUOM.DataTextField = "UOMDesc"
        ddlYieldAvgUOM.DataBind()
        ddlYieldAvgUOM.SelectedIndex = intYieldAvgIndex

    End Sub


    Sub InsertBlockRecord(ByRef blnExceedLicSize As Boolean, ByRef blnError As Boolean)
        Dim strOpCd_Upd As String = "GL_CLSSETUP_BLOCK_DETAILS_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_BLOCK_GET_BY_BLKCODE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_BLOCK_ADD"
        Dim strOpCd As String
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim objDateFormat As String

        Dim strValidPlantDate As String = ""
        Dim strValidTransDate As String = ""
        Dim strValidHarvStartDate As String = ""
        Dim strValidIniChargeDate As string = ""

        Dim strTransferDate As String
        Dim strInitialDate As String
        Dim strTransferBlk As String
        Dim dblStartArea As Double
        Dim dblTotalArea As Double
        Dim dblStdPerArea As Double
        Dim dblTotalStand As Double
        Dim intBlockType As Integer
        Dim dblBunchRatio As Double
        Dim dblQuota As Double
        Dim intQuotaMethod As Integer
        Dim dblQuotaIncRate As Double
        Dim decSubBlkAreaSum As Decimal = 0
        Dim intGrpType As Integer

        Dim strProcessCtrl As String


        If Not Trim(txtPlantDate.Text) = "" Then
            If objGlobal.mtdValidInputDate(strDateFMt, txtPlantDate.Text, objDateFormat, strValidPlantDate) = False Then
                lblPlantDateFmt.Text = objDateFormat & "."
                lblPlantDate.Visible = True
                lblPlantDateFmt.Visible = True
                blnError = True
                Exit Sub
            End If
        End If

        If txtTransferDate.Text <> "" And ddlTransferBlk.SelectedIndex = 0 Then
            lblErrTransferBlk.Visible = True
            blnError = True
            Exit Sub
        ElseIf txtTransferDate.Text = "" And ddlTransferBlk.SelectedIndex > 0 Then
            lblErrTransferBlkDate.Visible = True
            blnError = True
            Exit Sub
        End If

        If Not Trim(txtTransferDate.Text) = "" Then
            If objGlobal.mtdValidInputDate(strDateFMt, txtTransferDate.Text, objDateFormat, strValidTransDate) = False Then
                lblTransferDateFmt.Text = objDateFormat & "."
                lblTransferDate.Visible = True
                lblTransferDateFmt.Visible = True
                blnError = True
                Exit Sub
            End If
        End If


        If Not Trim(txtHarvestStartDate.Text) = "" Then
            If objGlobal.mtdValidInputDate(strDateFMt, txtHarvestStartDate.Text, objDateFormat, strValidHarvStartDate) = False Then
                lblHarvStartDateFmt.Text = objDateFormat & "."
                lblHarvStartDate.Visible = True
                lblHarvStartDateFmt.Visible = True
                blnError = True
                Exit Sub
            End If
        End If

        If Not Trim(txtInitialDate.Text) = "" Then
            If objGlobal.mtdValidInputDate(strDateFMt, txtInitialDate.Text, objDateFormat, strValidIniChargeDate) = False Then
                lblInitialDateFmt.Text = objDateFormat & "."
                lblInitialDate.Visible = True
                lblInitialDateFmt.Visible = True
                blnError = True
            End If
        End If

        If txtTransferDate.Text <> "" And txtInitialDate.Text <> "" Then
            If objGlobal.mtdValidInputDate(strDateFMt, txtTransferDate.Text, objDateFormat, strTransferDate) = True And _
                objGlobal.mtdValidInputDate(strDateFMt, txtInitialDate.Text, objDateFormat, strInitialDate) = True Then
                If DateDiff("d", strTransferDate, strInitialDate) > 0 Then
                    lblErrTransferDate.Visible = True
                    blnError = True
                    Exit Sub
                End If
            End If
        End If

        If rbTypeOff.Checked = True Then
            intBlockType = objGLSetup.EnumBlockType.Office
        ElseIf rbTypeMatureField.Checked = True Then
            intBlockType = objGLSetup.EnumBlockType.MatureField
        ElseIf rbTypeInMatureField.Checked = True Then
            intBlockType = objGLSetup.EnumBlockType.InMatureField
        ElseIf rbTypeNursery.Checked = True Then
            intBlockType = objGLSetup.EnumBlockType.Nursery
        End If

        If txtStartArea.Text = "" Then
            dblStartArea = 0.0
        Else
            dblStartArea = txtStartArea.Text
        End If

        If txtTotalArea.Text = "" Then
            dblTotalArea = 0.0
        Else
            dblTotalArea = txtTotalArea.Text
        End If

        If Trim(txtBunchRatio.Text) = "" Then
            dblBunchRatio = 1
        Else
            dblBunchRatio = txtBunchRatio.Text
        End If

        If Trim(txtQuota.Text) = "" Then
            dblQuota = 0
        Else
            dblQuota = txtQuota.Text
        End If

        If rbByHour.Checked Then
            intQuotaMethod = objHRTrx.EnumQuotaMethod.ByHour
        Else
            intQuotaMethod = objHRTrx.EnumQuotaMethod.ByVolume
        End If

        If Trim(txtQuotaIncRate.Text) = "" Then
            dblQuotaIncRate = 0
        Else
            dblQuotaIncRate = txtQuotaIncRate.Text
        End If


        If txtStdPerArea.Text = "" Then
            dblStdPerArea = 0.0
        Else
            dblStdPerArea = txtStdPerArea.Text
        End If

        If txtTotalStand.Text = "" Then
            dblTotalStand = 0.0
        Else
            dblTotalStand = txtTotalStand.Text
        End If
        If rbProcessCtrlYes.Checked Then
            strProcessCtrl = objGLSetup.EnumProcessControl.Yes
        Else
            strProcessCtrl = objGLSetup.EnumProcessControl.No
        End If


        strParam = Trim(txtBlkCode.Text) & "|||"
        Try
            intErrNo = objGLSetup.mtdGetBlock(strOpCd_Get, _
                                              strLocation, _
                                              strParam, _
                                              objBlkDs, _
                                              True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_GET_BY_BLOCKCODE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_block.aspx")
        End Try

        If objBlkDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else
            If strLocType = objAdminLoc.EnumLocType.Estate Then
                Try
                    objGLSetup.mtdGetTotalArea(strLocation, _
                                               Trim(txtBlkCode.Text), _
                                               " AND Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'", _
                                               False, _
                                               decSubBlkAreaSum)
                Catch Exp As Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx")
                End Try
                If Decimal.Compare(decSubBlkAreaSum, dblTotalArea) > 0 Then
                    lblErrTotalSize.Visible = True
                    Exit Sub
                End If
            End If
            strSelectedBlockCode = Trim(txtBlkCode.Text)
            strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)

            tbcode.Value = strSelectedBlockCode
            If intStatus = 0 Then
                strTransferBlk = ""
            Else
                strTransferBlk = Trim(ddlTransferBlk.SelectedItem.Value)
            End If

            If rbPlantedArea.Checked = True Then
                intGrpType = objGLSetup.EnumGrpType.PlantedArea
            ElseIf rbUnplantedArea.Checked = True Then
                intGrpType = objGLSetup.EnumGrpType.UnplantedArea
            ElseIf rbLandClearing.Checked = True Then
                intGrpType = objGLSetup.EnumGrpType.LandClearing
            ElseIf rbExtension.Checked = True Then
                intGrpType = objGLSetup.EnumGrpType.Extension
            End If

            If strLocType = objAdminLoc.EnumLocType.Mill Then
                dblTotalArea = 1
            End If
            strParam = Trim(txtBlkCode.Text) & Chr(9) & _
                       Trim(txtDescription.Text) & Chr(9) & _
                       ddlBlkGrp.SelectedItem.Value & Chr(9) & _
                       strValidPlantDate & Chr(9) & _
                       intBlockType & Chr(9) & _
                       dblStartArea & Chr(9) & _
                       dblTotalArea & Chr(9) & _
                       strTransferBlk & Chr(9) & _
                       strValidTransDate & Chr(9) & _
                       dblBunchRatio & Chr(9) & _
                       dblQuota & Chr(9) & _
                       intQuotaMethod & Chr(9) & _
                       dblQuotaIncRate & Chr(9) & _
                       ddlAreaUOM.SelectedItem.Value & Chr(9) & _
                       ddlYieldUOM.SelectedItem.Value & Chr(9) & _
                       ddlAreaAvgUOM.SelectedItem.Value & Chr(9) & _
                       ddlYieldAvgUOM.SelectedItem.Value & Chr(9) & _
                       txtPlantMaterial.Text.Trim & Chr(9) & _
                       dblStdPerArea & Chr(9) & _
                       strValidHarvStartDate & Chr(9) & _
                       objGLSetup.EnumBlockStatus.Active & Chr(9) & _
                       dblTotalStand & Chr(9) & _
                       strValidIniChargeDate & Chr(9) & _
                       intGrpType & Chr(9) & _
                       ddlBlkTypeInRpt.SelectedItem.Value & Chr(9) & _
                       IIf(txtStationCap.text = "", 0, txtStationCap.Text.Trim) & Chr(9) & _
                       IIf(txtStdRunHour.Text = "", 0, txtStdRunHour.Text.Trim) & Chr(9) & _
                       strProcessCtrl 
                       
            Try
                intErrNo = objGLSetup.mtdUpdBlock(strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  False, _
                                                  blnExceedLicSize)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx")
            End Try

            If blnExceedLicSize = True Then lblErrLicSize.Visible = True
        End If
    End Sub

    Sub OnTypeChanged(ByVal Sender As Object, ByVal E As EventArgs)
        BindAccGrpCode()
        BindAccount()
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "GL_CLSSETUP_BLOCK_UPD"
        Dim strOpCd_BlockDet_Upd As String = "BD_CLSTRX_BLOCK_OR_SUBBLOCK_DETAILS_UPD_SP" 
        Dim blnExceedLicSize As Boolean
        Dim blnError As Boolean = False
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim dblTotalArea As Double
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim blnIsInUse As Boolean = False

        If txtTotalArea.Text = "" Then
             dblTotalArea = 0
        Else
            dblTotalArea = txtTotalArea.Text
        End If

        If strLocType = objAdminLoc.EnumLocType.Mill Then
             dblTotalArea = 1
        End If
       
        If txtPlantDate.Text = "" Then
            If rbTypeInMatureField.Checked = True Or rbTypeMatureField.Checked = True Then
                lblErrPlantDate.Visible = True
                Exit Sub
            End If
        End If

        If strCmdArgs = "Save" Then
            If hidRecStatus.Value = "Unsaved" Then
                InsertBlockRecord(blnExceedLicSize, blnError)

                If blnError Then
                    Exit Sub
                Else
                    strParam = txtBlkCode.Text.Trim & "|"
                    Try
                        intErrNo = objBDTrx.mtdUpdBlockDetails(strOpCd_BlockDet_Upd, strCompany, strLocation, _
                                                               strUserId, strParam, strYieldLevel)
                    Catch exp As Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_DETAILS_UPD_BD_TRX&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx?tbcode=" & strSelectedBlockCode)
                    End Try
                End If
            Else
                If hidOriBlkCode.Value = txtBlkCode.Text.Trim Then
                    InsertBlockRecord(blnExceedLicSize, blnError)

                    If blnError Then
                        Exit Sub
                    Else
                        strParam = txtBlkCode.Text.Trim & "|"
                        Try
                            intErrNo = objBDTrx.mtdUpdBlockDetails(strOpCd_BlockDet_Upd, strCompany, strLocation, _
                                                                   strUserId, strParam, strYieldLevel)
                        Catch exp As Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_DETAILS_UPD_BD_TRX&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx?tbcode=" & strSelectedBlockCode)
                        End Try
                    End If
                Else
                    intErrNo = objGLSetup.mtdCheckSetupCodeUsage("BlkCode", hidOriBlkCode.Value, blnIsInUse)

                    If blnIsInUse = True Then
                        Response.Write("<script language='javascript'>alert('Transaction exists for Block Code " & hidOriBlkCode.Value & ". Editing this Block Code is not allowed.')</script>")
                    Else
                        intStatus = 0
                        intErrNo = objGLSetup.mtdDelPrevCode("BlkCode", hidOriBlkCode.Value)

                        InsertBlockRecord(blnExceedLicSize, blnError)

                        If blnError Then
                            Exit Sub
                        Else
                            strParam = txtBlkCode.Text.Trim & "|"
                            Try
                                intErrNo = objBDTrx.mtdUpdBlockDetails(strOpCd_BlockDet_Upd, strCompany, strLocation, _
                                                                       strUserId, strParam, strYieldLevel)
                            Catch exp As Exception
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_DETAILS_UPD_BD_TRX&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx?tbcode=" & strSelectedBlockCode)
                            End Try
                        End If
                    End If
                End If
            End If

            If blnIsInUse = False Then
                If lblErrDup.Visible = False Then
                    hidRecStatus.Value = "Saved"
                    hidOriBlkCode.Value = txtBlkCode.Text.Trim
                End If
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtBlkCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       objGLSetup.EnumBlockStatus.Deleted & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9)
            Try
                intErrNo = objGLSetup.mtdUpdBlock(strOpCd_Upd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  True, _
                                                  blnExceedLicSize)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_DETAILS_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx?tbcode=" & strSelectedBlockCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtBlkCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       dblTotalArea & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       objGLSetup.EnumBlockStatus.Active & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9)

            Try
                intErrNo = objGLSetup.mtdUpdBlock(strOpCd_Upd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  True, _
                                                  blnExceedLicSize)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_DETAILS_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx?tbcode=" & strSelectedBlockCode)
            End Try
        End If

        If blnExceedLicSize = True Then
            lblErrLicSize.Visible = True
        ElseIf tbcode.Value <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
            onLoad_BindButton()
        End If
    End Sub


    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_AddLine As String '= "GL_CLSSETUP_BLOCK_LINE_ADD" '--#12
        Dim strOpCode_UpdID As String = "GL_CLSSETUP_BLOCK_ACCOUNT_UPDATEID"
        Dim strOpCode_GetAccCode As String = "GL_CLSSETUP_BLOCK_ACCCODE_GET_BY_ACCGRPCODE"
        Dim strOpCode_DelAccCode As String = "GL_CLSSETUP_BLOCK_ACCCODE_DELETE_BY_ACCGRPCODE"
        Dim strParam As String
        Dim strAccCode As String
        Dim strAccGrpCode As String
        Dim intErrNo As Integer
        Dim blnExceedLicSize As Boolean
        Dim blnError As Boolean = False

        Try
            strAccCode = ddlAccount.SelectedItem.Value
        Catch Exp As Exception
            Exit Sub
        End Try

        Try
            strAccGrpCode = ddlAccGrp.SelectedItem.Value
        Catch Exp As Exception
            Exit Sub
        End Try

        If strAccGrpCode = "" And strAccCode = "" Then
            lblErrNotSelect.Visible = True
            Exit Sub
        elseIf strAccGrpCode <> "" And strAccCode <> "" Then
            lblErrSelectBoth.Visible = True
            Exit Sub
        ElseIf strAccGrpCode <> "" And strAccCode = "" Then 
            strOpCode_AddLine = "GL_CLSSETUP_BLOCK_LINE_ADD" 
        ElseIf strAccGrpCode = "" And strAccCode <> "" Then 
            strOpCode_AddLine = "GL_CLSSETUP_BLOCK_LINE_ACCCODE_ADD" 
        End If

        InsertBlockRecord(blnExceedLicSize, blnError)

        If blnError Then
            Exit Sub
        End If

        If strSelectedBlockCode = "" Then
            Exit Sub
        Else
            Try
                strParam = strSelectedBlockCode & "|" & strAccCode & "|" & strAccGrpCode
                If rbTypeNursery.Checked = True Or rbTypeInMatureField.Checked = True Then     
                    strParam = strParam & "| AND Status = '" & objGLSetup.EnumAccStatus.Active & "' AND AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "' AND NurseryInd = '" & objGLSetup.EnumNurseryAccount.Yes & "' " 
		 ElseIf rbTypeOff.Checked = True Then
                    strParam = strParam & "| AND LocCode = '" & Trim(strLocation) & "' AND Status = '" & objGLSetup.EnumAccStatus.Active & "' AND AccType IN ('" & objGLSetup.EnumAccountType.BalanceSheet & "', '" & objGLSetup.EnumAccountType.ProfitAndLost & "') "   
                Else
                    strParam = strParam & "| AND Status = '" & objGLSetup.EnumAccStatus.Active & "' AND AccType = '" & objGLSetup.EnumAccountType.ProfitAndLost & "' AND NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "' and AccPurpose IN ('"  & objGLSetup.EnumAccountPurpose.NonVehicle & "','" & objGLSetup.EnumAccountPurpose.Others & "') "
                End If
                intErrNo = objGLSetup.mtdUpdBlockLine(strOpCode_UpdID, _
                                                      strOpCode_AddLine, _
                                                      strOpCode_GetAccCode, _
                                                      strOpCode_DelAccCode, _
                                                      strCompany, _
                                                      strLocation, _
                                                      strUserId, _
                                                      strParam, _
                                                      objResult)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKLINE_ADD&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_blockdet.aspx?tbcode=" & strSelectedBlockCode)
            End Try
        End If
        If blnExceedLicSize = True Then
            lblErrLicSize.Visible = True
        ElseIf tbcode.Value <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
            BindAccGrpCode()
            BindAccount()
            onLoad_BindButton()
        End If

    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objResult As New Object()
        Dim strOpCode_DelLine As String = "GL_CLSSETUP_BLOCK_LINE_DELETE"
        Dim strOpCode_UpdID As String = "GL_CLSSETUP_BLOCK_ACCOUNT_UPDATEID"
        Dim strOpCode_GetAccCode As String = ""
        Dim strOpCode_DelAccCode As String = ""
        Dim strParam As String
        Dim lblDelText As Label
        Dim strAccCode As String
        Dim intErrNo As Integer

        dgLineDet.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLineDet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblCode")
        strAccCode = lblDelText.Text

        Try
            strParam = strSelectedBlockCode & "|" & strAccCode & "|" & "" & "|"
            intErrNo = objGLSetup.mtdUpdBlockLine(strOpCode_UpdID, _
                                                  strOpCode_DelLine, _
                                                  strOpCode_GetAccCode, _
                                                  strOpCode_DelAccCode, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strParam, _
                                                  objResult)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCKLINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_blockdet.aspx?tbcode=" & strSelectedBlockCode)
        End Try

        onLoad_Display()
        onLoad_LineDisplay()
        BindAccGrpCode()
        BindAccount()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_Block.aspx")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Block))
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblBlkDesc.Text = GetCaption(objLangCap.EnumLangCap.BlockDesc)
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp) & lblCode.Text
        lblStartArea.Text = GetCaption(objLangCap.EnumLangCap.StartArea)
        lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block)
        lblTotalArea.Text = GetCaption(objLangCap.EnumLangCap.TotalArea)
        lblQuotaInc.Text = GetCaption(objLangCap.EnumLangCap.QuotaIncentive)
        lblArea.Text = GetCaption(objLangCap.EnumLangCap.Area)
        lblYield.Text = GetCaption(objLangCap.EnumLangCap.Crop)
        lblAreaAvg.Text = GetCaption(objLangCap.EnumLangCap.AreaAvg)
        lblYieldAvg.Text = GetCaption(objLangCap.EnumLangCap.CropAvg)
        lblHarvestStartDate.Text = GetCaption(objLangCap.EnumLangCap.HarvestStartDate)
        lblStdPerArea.Text = GetCaption(objLangCap.EnumLangCap.StdPerArea)
        lblAccGrp.Text = GetCaption(objLangCap.EnumLangCap.AccGrp) & lblCode.Text
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblCreateLoc.Text = GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
        lblErrTransferBlk.Text = lblPlsSelect.Text & GetCaption(objLangCap.EnumLangCap.Block)
        lblDateOfPlanting.Text = GetCaption(objLangCap.EnumLangCap.DateOfPlanting)

        lblBlkMgrRpt.Text = GetCaption(objLangCap.EnumLangCap.Block)

        rfvBlkCode.ErrorMessage = "<br>" & lblPleaseEnter.Text & lblBlkCode.Text & "."
        rfvBlkDesc.ErrorMessage = lblPleaseEnter.Text & lblBlkDesc.Text & "."
        rfvBlkGrp.ErrorMessage = lblPlsSelect.Text & lblBlkGrp.Text & "."
        rfvAreaUOM.ErrorMessage = lblPlsSelectUOM.Text & lblArea.Text & "."
        rfvYieldUOM.Text = lblPlsSelectUOM.Text & lblYield.Text & "."
        rfvAreaAvgUOM.Text = lblPlsSelectUOM.Text & lblAreaAvg.Text & "."
        rfvYieldAvgUOM.Text = lblPlsSelectUOM.Text & lblYieldAvg.Text & "."

        lblErrSelectBoth.Text = lblPlsSelectEither.Text & lblAccGrp.Text & lblOr.Text & lblAccount.Text & lblOnly.Text
        lblErrNotSelect.Text = lblPlsSelect.Text & lblAccGrp.Text & lblOr.Text & lblAccount.Text & "."

        dgLineDet.Columns(0).HeaderText = lblAccount.Text
        dgLineDet.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.AccDesc)
        lblErrTotalSize.Text = "<br>Total Area cannot be less than the sum of Total Area from all active " & GetCaption(objLangCap.EnumLangCap.SubBlock) & "s that belong to this " & GetCaption(objLangCap.EnumLangCap.Block) & "."
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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLKDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                    If strLocType = objAdminLoc.EnumLocType.Mill then
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                    else
                        Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                    end if
                    Exit For
                End If
            Next
        End Function



    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Sub OnGrpTypeChanged(ByVal Sender As Object, ByVal E As EventArgs)
        BindBlkTypeInRpt()
    End Sub

    Sub BindBlkTypeInRpt(Optional ByVal pv_strRptBlkType As String = "")
        If rbPlantedArea.Checked = True Then
            BindBlkTypeInRptWithPlantedArea(pv_strRptBlkType)
        ElseIf rbUnplantedArea.Checked = True Then
            BindBlkTypeInRptWithUnplantedArea(pv_strRptBlkType)
        ElseIf rbLandClearing.Checked = True Then
            BindBlkTypeInRptWithLandClearing(pv_strRptBlkType)
        ElseIf rbExtension.Checked = True Then
            BindBlkTypeInRptWithExtension(pv_strRptBlkType)
        End If
    End Sub
    
    Sub BindBlkTypeInRptWithPlantedArea(Optional ByVal pv_strRptBlkType As String = "")
        ddlBlkTypeInRpt.Items.Clear
        ddlBlkTypeInRpt.Items.Add(New ListItem("Select Block Type in Managerial Report", ""))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetPlantedAreaBlkType(objGLSetup.EnumPlantedAreaBlkType.Mature), objGLSetup.EnumPlantedAreaBlkType.Mature))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetPlantedAreaBlkType(objGLSetup.EnumPlantedAreaBlkType.Immature), objGLSetup.EnumPlantedAreaBlkType.Immature))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetPlantedAreaBlkType(objGLSetup.EnumPlantedAreaBlkType.NewPlanting), objGLSetup.EnumPlantedAreaBlkType.NewPlanting))
        
        If Not Trim(pv_strRptBlkType) = "" Then
            ddlBlkTypeInRpt.SelectedValue = pv_strRptBlkType
        End If
    End Sub

    Sub BindBlkTypeInRptWithUnplantedArea(Optional ByVal pv_strRptBlkType As String = "")
        ddlBlkTypeInRpt.Items.Clear
        ddlBlkTypeInRpt.Items.Add(New ListItem("Select Block Type in Managerial Report", ""))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetUnplantedAreaBlkType(objGLSetup.EnumUnplantedAreaBlkType.Nursery), objGLSetup.EnumUnplantedAreaBlkType.Nursery))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetUnplantedAreaBlkType(objGLSetup.EnumUnplantedAreaBlkType.Mill), objGLSetup.EnumUnplantedAreaBlkType.Mill))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetUnplantedAreaBlkType(objGLSetup.EnumUnplantedAreaBlkType.InfrastructureArea), objGLSetup.EnumUnplantedAreaBlkType.InfrastructureArea))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetUnplantedAreaBlkType(objGLSetup.EnumUnplantedAreaBlkType.HillRiverValley), objGLSetup.EnumUnplantedAreaBlkType.HillRiverValley))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetUnplantedAreaBlkType(objGLSetup.EnumUnplantedAreaBlkType.Office), objGLSetup.EnumUnplantedAreaBlkType.Office))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetUnplantedAreaBlkType(objGLSetup.EnumUnplantedAreaBlkType.OthersUnplantedArea), objGLSetup.EnumUnplantedAreaBlkType.OthersUnplantedArea))

        If Not Trim(pv_strRptBlkType) = "" Then
            ddlBlkTypeInRpt.SelectedValue = pv_strRptBlkType
        End If
    End Sub

    Sub BindBlkTypeInRptWithLandClearing(Optional ByVal pv_strRptBlkType As String = "")
        ddlBlkTypeInRpt.Items.Clear
        ddlBlkTypeInRpt.Items.Add(New ListItem("Select Block Type in Managerial Report", ""))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetLandClearingBlkType(objGLSetup.EnumLandClearingBlkType.LandClearing), objGLSetup.EnumLandClearingBlkType.LandClearing))

        If Not Trim(pv_strRptBlkType) = "" Then
            ddlBlkTypeInRpt.SelectedValue = pv_strRptBlkType
        End If
    End Sub

    Sub BindBlkTypeInRptWithExtension(Optional ByVal pv_strRptBlkType As String = "")
        ddlBlkTypeInRpt.Items.Clear
        ddlBlkTypeInRpt.Items.Add(New ListItem("Select Block Type in Managerial Report", ""))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetExtensionBlkType(objGLSetup.EnumExtensionBlkType.ReserveArea), objGLSetup.EnumExtensionBlkType.ReserveArea))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetExtensionBlkType(objGLSetup.EnumExtensionBlkType.Occupation), objGLSetup.EnumExtensionBlkType.Occupation))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetExtensionBlkType(objGLSetup.EnumExtensionBlkType.Village), objGLSetup.EnumExtensionBlkType.Village))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetExtensionBlkType(objGLSetup.EnumExtensionBlkType.OthersExtension), objGLSetup.EnumExtensionBlkType.OthersExtension))

        If Not Trim(pv_strRptBlkType) = "" Then
            ddlBlkTypeInRpt.SelectedValue = pv_strRptBlkType
        End If
    End Sub

    Sub EnableProperties()
        If strLocType = objLoc.EnumLocType.Mill then

            btnSelHarvStartDate.visible = false
            btnSelInitialDate.visible = false 
            btnSelTransferDate.visible = false 


            TRStartArea.visible = false
            TRTotalArea.visible = false
            TRBunchRatio.visible = false
            TRDailyQuota.visible = false
            TRMaterialPlant.visible = false
            TRStdPerArea.visible = false
            TRTotalStand.visible = false
            TRGroupType.visible = false


            lblProcessCtrl.Visible = true


        else

            btnSelHarvStartDate.visible = true
            btnSelInitialDate.visible = true 
            btnSelTransferDate.visible = true 

            TRStartArea.visible = true
            TRTotalArea.visible = true
            TRBunchRatio.visible = true
            TRDailyQuota.visible = true
            TRMaterialPlant.visible = true
            TRStdPerArea.visible = true
            TRTotalStand.visible = true
            TRGroupType.visible = true

            lblStationCap.visible = false 
            txtStationCap.visible = false 
            lblStdRunHour.visible = false 
            txtStdRunHour.visible = false 

            lblProcessCtrl.Visible = false
        end if 

        rbProcessCtrlYes.Visible = lblProcessCtrl.Visible
        rbProcessCtrlNo.Visible = lblProcessCtrl.Visible
    End Sub


End Class
