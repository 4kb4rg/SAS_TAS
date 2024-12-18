
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
Imports Microsoft.VisualBasic.DateAndTime
Imports Microsoft.VisualBasic


Public Class GL_Setup_SubBlockDet : Inherits Page

    Protected WithEvents txtSubBlkCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents txtPlantDate As TextBox
    Protected WithEvents txtStartArea As TextBox
    Protected WithEvents txtTotalArea As TextBox
    Protected WithEvents txtBunchRatio As TextBox
    Protected WithEvents txtTransferDate As TextBox
    Protected WithEvents txtPlantMaterial As TextBox
    Protected WithEvents txtHarvestStartDate As TextBox
    Protected WithEvents txtStdPerArea As TextBox
    Protected WithEvents txtInitialDate As TextBox
    Protected WithEvents txtTotalStand As TextBox
    Protected WithEvents txtQuota As TextBox
    Protected WithEvents txtQuotaIncRate As TextBox
	Protected WithEvents txtprd As TextBox
	Protected WithEvents txtprd2 As TextBox

    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlTransferSubBlk As DropDownList
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
    Protected WithEvents rbByHour As RadioButton
    Protected WithEvents rbByVolume AS RadioButton
    Protected WithEvents tblSelection As HtmlTable

    Protected WithEvents rbTypeMill As RadioButton

    Protected WithEvents TRStartArea As HtmlTableRow
    Protected WithEvents TRTotalArea As HtmlTableRow
    Protected WithEvents TRDailyQuota As HtmlTableRow
    Protected WithEvents TRQuotaInc As HtmlTableRow
    Protected WithEvents TREstimatedBJR As HtmlTableRow
    Protected WithEvents TRMaterialPlant As HtmlTableRow
    Protected WithEvents TRStdPerArea As HtmlTableRow
    Protected WithEvents TRTotalStand As HtmlTableRow
    Protected WithEvents TRGroupType As HtmlTableRow
	Protected WithEvents TRHistoriBlk As HtmlTableRow
	

    Protected WithEvents txtEstimatedBJR As TextBox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
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
    Protected WithEvents lblErrTransferSubBlk As Label 
    Protected WithEvents lblErrTransferSubBlkDate As Label 
    Protected WithEvents lblHarvStartDate As Label
    Protected WithEvents lblHarvStartDateFmt As Label
    Protected WithEvents lblInitialDate As Label
    Protected WithEvents lblInitialDateFmt As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblPlsSelectUOM As Label
    Protected WithEvents lblPlsSelectEither As Label
    Protected WithEvents lblPlsSelect As Label
    Protected WithEvents lblOr As Label
    Protected WithEvents lblOnly As Label
    Protected WithEvents lblErrLicSize As Label
    Protected WithEvents lblHidCostLevel As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents lblErrTotalSize As Label
    Protected WithEvents lblDateOfPlanting As Label

    Protected WithEvents lblLocType as Label
    Protected WithEvents lblActHourMeter As Label
    Protected WithEvents lblExpHourMeter As Label
    Protected WithEvents txtActHourMeter As TextBox
    Protected WithEvents txtExpHourMeter As TextBox


    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents btnSelDate As ImageButton
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents dgLineDet As DataGrid
	Protected WithEvents dghist As DataGrid
	
	Protected WithEvents id7ddl As DropDownList

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblSubBlkType As Label
    Protected WithEvents lblSubBlkDesc As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblBlock2 As Label
    Protected WithEvents lblStartArea As Label
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

    Protected WithEvents btnSelPlantDate As Image
    Protected WithEvents btnSelTransferDate As Image
    Protected WithEvents btnSelHarvStartDate As Image
    Protected WithEvents btnSelInitialDate As Image

    Protected WithEvents rfvSubBlkCode As RequiredFieldValidator
    Protected WithEvents rfvSubBlkDesc As RequiredFieldValidator
    Protected WithEvents rfvBlk As RequiredFieldValidator
    Protected WithEvents rfvAreaUOM As RequiredFieldValidator  
    Protected WithEvents rfvYieldUOM As RequiredFieldValidator
    Protected WithEvents rfvAreaAvgUOM As RequiredFieldValidator
    Protected WithEvents rfvYieldAvgUOM As RequiredFieldValidator
    Protected hidRecStatus As HtmlInputHidden
    Protected hidOriSubBlkCode As HtmlInputHidden
	
	Protected WithEvents ddl1 As dropdownlist
	Protected WithEvents ddl2 As TextBox

	
    Dim objGLSetup As New agri.GL.clsSetup()
	 Dim ObjOk As New agri.GL.ClsTrx()
	
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objSys As New agri.PWSystem.clsConfig()
    'Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objBDTrx As New agri.BD.clsTrx 

    Protected objLoc As New agri.Admin.clsLoc()
	Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objSubBlkDs As New Object()
    Dim objBlkDs As New Object()
    Dim objTransSubBlkDs As New Object()
    Dim objUOMDs As New Object()
    Dim objAccGrpDs As New Object()
    Dim objAccDs As New Object()
    Dim objSubBlkLnDs As New Object()
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

    Dim strSelectedSubBlkCode As String = ""
    Dim intStatus As Integer
    Dim intConfig As Integer

    Dim strLocType as String

    Protected WithEvents lblBunchRatio As Label
    Protected WithEvents lblDailyQuota As Label
    Protected WithEvents lblTransDate As Label
    Protected WithEvents lblMeasuredBy As Label
    Protected WithEvents lblMaterialPlant As Label
    Protected WithEvents lblTotalStand as label
    Protected WithEvents lblinitialchgdate as label
    Protected WithEvents lblGroupType as label
    Protected WithEvents lblEstimatedBJR as label
    Protected WithEvents lblMachineCap as label 
    Protected WithEvents txtMachineCap as TextBox
    Protected WithEvents txtStdRunHour as TextBox
    Protected WithEvents lblStdRunHour as label
    Protected WithEvents lblCheckMill as HtmlInputHidden



    Protected WithEvents rbPlantedArea As RadioButton
    Protected WithEvents rbUnplantedArea As RadioButton
    Protected WithEvents rbLandClearing As RadioButton
    Protected WithEvents rbExtension As RadioButton

    Protected WithEvents ddlBlkTypeInRpt As DropDownList

    Protected WithEvents lblSubBlkMgrRpt As Label
    Protected WithEvents lblProcessCtrl As System.Web.UI.WebControls.Label
    Protected WithEvents rbProcessCtrlYes As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbProcessCtrlNo As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtUrea as TextBox
    Protected WithEvents txtRp as TextBox
    Protected WithEvents txtKclMop as TextBox
    Protected WithEvents txtKliserit as TextBox
    Protected WithEvents txtDolomit as TextBox
    Protected WithEvents txtHGFB as TextBox
    Protected WithEvents txtMillEff as TextBox
    Protected WithEvents txtJJ as TextBox

    Protected WithEvents TRSpacer As HtmlTableRow
    Protected WithEvents TRFert As HtmlTableRow
    Protected WithEvents TRFert1 As HtmlTableRow
    Protected WithEvents TRFert2 As HtmlTableRow
    Protected WithEvents TRFert3 As HtmlTableRow
    Protected WithEvents TRFert4 As HtmlTableRow

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_SELACCYEAR")
        intGLAR = Session("SS_GLAR")
        strDateFMt = Session("SS_DATEFMT")
        intConfig = Session("SS_CONFIGSETTING")
        strYieldLevel = Session("SS_YIELDLEVEL")
        lblHidCostLevel.Text = Session("SS_COSTLEVEL")
        strLocType = Session("SS_LOCTYPE")
        lblCheckMill.value = strLocType

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrSelectBoth.Visible = False
            lblErrNotSelect.Visible = False
            lblPlantDate.Visible = False
            lblPlantDateFmt.Visible = False
            lblTransferDate.Visible = False
            lblTransferDateFmt.Visible = False
            lblErrPlantDate.Visible = False 
            lblErrTransferDate.Visible = False 
            lblErrTransferSubBlk.Visible = False
            lblErrTransferSubBlkDate.Visible = False
            lblHarvStartDate.Visible = False
            lblHarvStartDateFmt.Visible = False
            lblErrLicSize.Visible = False
            lblErrTotalSize.Visible = False

            strSelectedSubBlkCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()
         
            rbTypeNursery.Enabled = True
			txtprd.Text = strAccYear
			txtprd2.Text = strAccYear
			 
            If Not IsPostBack Then
                If strSelectedSubBlkCode <> "" Then
                    tbcode.Value = strSelectedSubBlkCode
                    onLoad_Display()                    
                    BindAccGrpCode()
                    BindAccount()
                    onLoad_LineDisplay()
                    onLoad_BindButton()
					onLoad_LineHistori()
                Else
                    BindBlock("")
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
                    hidOriSubBlkCode.Value = Trim(Request.QueryString("tbcode"))
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

        'End If

        If strLocType = objLoc.EnumLocType.Mill Then
            rbTypeMill.visible = True
            rbTypeMill.checked = True
            ddlBlkTypeInRpt.SelectedValue = 1
        Else
            rbTypeMill.visible = False
            rbTypeMill.checked = False
        End If

        lblBunchRatio.Text = "Bunch Ratio : "
        lblDailyQuota.Text = "Daily Quota : "
        If strLocType = objLoc.EnumLocType.Mill Then
            lblBlock2.Text = lblBlock2.Text & " Categories : "
        Else
            lblBlock2.Text = "Transfer To " & lblBlock2.Text & " : "
        End If
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
        lblSubBlkMgrRpt.Text = lblSubBlkMgrRpt.Text & " Type in Managerial Report:* "
        lblMachineCap.Text = "Annual Processing Time (Hour) : " '"Machine Capacity : "
        lblProcessCtrl.Text = "Process Control : "


        lblStdRunHour.Text = "Standard Running Hour : "
        lblTotalArea.Text = lblTotalArea.Text & " : "
        lblEstimatedBJR.Text = "Estimated BJR : "

        lblActHourMeter.Text = "Hour Meter : "
        lblExpHourMeter.Text = "Estimate Hour Meter : "

        EnableProperties()

    End Sub


    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_BLOCK_GET_BY_SUBBLKCODE"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAreaUOM As String
        Dim strYieldUOM As String
        Dim strAreaAvgUOM As String
        Dim strYieldAvgUOM As String       
        
        strParam = strSelectedSubBlkCode & "|||"
        Try
            intErrNo = objGLSetup.mtdGetSubBlock(strOpCd, _
                                                 strLocation, _
                                                 strParam, _
                                                 objSubBlkDs, _
                                                 True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_GET_BY_SUBBLKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        txtSubBlkCode.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("SubBlkCode"))
        txtDescription.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("Description"))
        txtPlantDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objSubBlkDs.Tables(0).Rows(0).Item("PlantingDate")))
        txtStartArea.Text = CDbl(objSubBlkDs.Tables(0).Rows(0).Item("StartArea"))
        txtTotalArea.Text = CDbl(objSubBlkDs.Tables(0).Rows(0).Item("TotalArea"))
        txtTransferDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objSubBlkDs.Tables(0).Rows(0).Item("TransferDate")))
        txtBunchRatio.Text = objSubBlkDs.Tables(0).Rows(0).Item("BunchRatio")
        txtQuota.Text = objSubBlkDs.Tables(0).Rows(0).Item("Quota")
        txtQuotaIncRate.Text = objSubBlkDs.Tables(0).Rows(0).Item("QuotaIncRate")
        txtPlantMaterial.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("PlantMaterial"))
        txtHarvestStartDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objSubBlkDs.Tables(0).Rows(0).Item("HarvestStartDate")))
        txtStdPerArea.Text = CDbl(objSubBlkDs.Tables(0).Rows(0).Item("StdPerArea"))
        txtInitialDate.Text = objGlobal.GetShortDate(strDateFMt, Trim(objSubBlkDs.Tables(0).Rows(0).Item("InitialChargeDate")))
        txtTotalStand.Text = formatNumber(objSubBlkDs.Tables(0).Rows(0).Item("TotalStand"), 0, True, False, false)

        If Trim(txtTransferDate.Text) = "01/01/1900" Then
            txtTransferDate.Text = ""
        End If
        txtEstimatedBJR.Text = objSubBlkDs.Tables(0).Rows(0).Item("EstBJR")
        txtMachineCap.Text = formatNumber(objSubBlkDs.Tables(0).Rows(0).Item("MachineCap"), 0, True, False, false)
        txtStdRunHour.Text = formatNumber(objSubBlkDs.Tables(0).Rows(0).Item("StdRunHour"), 0, True, False, false)

        
        txtActHourMeter.Text = CDbl(objSubBlkDs.Tables(0).Rows(0).Item("ActHourMeter"))
        txtExpHourMeter.Text = CDbl(objSubBlkDs.Tables(0).Rows(0).Item("ExpHourMeter"))
        
        strAreaUOM = Trim(objSubBlkDs.Tables(0).Rows(0).Item("AreaUOM"))
        strYieldUOM = Trim(objSubBlkDs.Tables(0).Rows(0).Item("CropUOM"))
        strAreaAvgUOM = Trim(objSubBlkDs.Tables(0).Rows(0).Item("AreaAvgUOM"))
        strYieldAvgUOM = Trim(objSubBlkDs.Tables(0).Rows(0).Item("CropAvgUOM"))

        BindBlock(Trim(objSubBlkDs.Tables(0).Rows(0).Item("BlkCode")))
        BindTransferSubBlock(Trim(objSubBlkDs.Tables(0).Rows(0).Item("SubBlkCode")), Iif(IsDBNull(objSubBlkDs.Tables(0).Rows(0).Item("TransferBlkCode"))," ",objSubBlkDs.Tables(0).Rows(0).Item("TransferBlkCode"))) 
        BindUOM(strAreaUOM, strYieldUOM, strAreaAvgUOM, strYieldAvgUOM)

        lblHiddenSts.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("Status"))
        intStatus = CInt(lblHiddenSts.Text)

        lblStatus.Text = objGLSetup.mtdGetSubBlockStatus(Trim(objSubBlkDs.Tables(0).Rows(0).Item("Status")))

        lblDateCreated.Text = objGlobal.GetLongDate(objSubBlkDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objSubBlkDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("UserName"))

        txtUrea.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("Urea"))
        txtRP.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("RP"))
        txtKclMop.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("KCLMOP"))
        txtKliserit.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("Kliserit"))
        txtDolomit.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("Dolomit"))
        txtHGFB.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("HGFB"))
        txtMillEff.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("MillEff"))
        txtJJ.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("JJ"))
		ddl1.selectedvalue = Trim(objSubBlkDs.Tables(0).Rows(0).Item("Komoditi"))
		ddl2.Text = Trim(objSubBlkDs.Tables(0).Rows(0).Item("Bibit"))

        Select Case CInt(objSubBlkDs.Tables(0).Rows(0).Item("SubBlkType"))
            Case objGLSetup.EnumSubBlockType.InMatureField
                rbTypeInMatureField.Checked = True
                rbTypeMatureField.Checked = False
                rbTypeOff.Checked = False
                rbTypeNursery.Checked = False
            Case objGLSetup.EnumSubBlockType.MatureField
                rbTypeInMatureField.Checked = False
                rbTypeMatureField.Checked = True
                rbTypeOff.Checked = False
                rbTypeNursery.Checked = False
            Case objGLSetup.EnumSubBlockType.Office
                rbTypeInMatureField.Checked = False
                rbTypeMatureField.Checked = False
                rbTypeOff.Checked = True
                rbTypeNursery.Checked = False
            Case objGLSetup.EnumSubBlockType.Nursery
                rbTypeInMatureField.Checked = False
                rbTypeMatureField.Checked = False
                rbTypeOff.Checked = False
                rbTypeNursery.Checked = True
        End Select
        Select Case CInt(objSubBlkDs.Tables(0).Rows(0).Item("ProcessCtrl"))
            Case objGLSetup.EnumProcessControl.Yes
                rbProcessCtrlNo.Checked = False
                rbProcessCtrlYes.Checked = True
            Case objGLSetup.EnumProcessControl.No
                rbProcessCtrlNo.Checked = True
                rbProcessCtrlYes.Checked = False
        End Select

        Select Case CInt(objSubBlkDs.Tables(0).Rows(0).Item("QuotaMethod"))
            Case objHRTrx.EnumQuotaMethod.ByHour
                rbByHour.Checked = True
            Case Else
                rbByVolume.Checked = True
        End Select

        Select Case CInt(objSubBlkDs.Tables(0).Rows(0).Item("GrpType"))
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

        BindBlkTypeInRpt(objSubBlkDs.Tables(0).Rows(0).Item("RptBlkType"))
    End Sub

    Protected Function CheckDate(ByVal strDateField As String) As String



    End Function


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
            strSearchExp = "where Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' " & _
                           "and accgrpcode in (select accgrpcode from gl_account where acctype = '" & objGLSetup.EnumAccountType.BalanceSheet & "' " & _
                           "                    Or NurseryInd = '" & objGLSetup.EnumNurseryAccount.Yes & "')"  '--01062004
        ElseIf rbTypeOff.Checked = True Then
            strSearchExp = "where Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' " & _
                                       "and accgrpcode in (select accgrpcode from gl_account where acctype IN ('" & objGLSetup.EnumAccountType.BalanceSheet & "', '" & objGLSetup.EnumAccountType.ProfitAndLost & "')) " & _
                                       "     "
       Else
            strSearchExp = "where Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' " & _
                           "and accgrpcode in (select accgrpcode from gl_account where acctype = '" & objGLSetup.EnumAccountType.ProfitAndLost & "' " & _
                           "                    and NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "' and AccPurpose IN ('" & objGLSetup.EnumAccountPurpose.NonVehicle & "','" & objGLSetup.EnumAccountPurpose.Others & "') )"
        End If

	
		
        strParam = strSortOrder & "|" & strSearchExp

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   0, _
                                                   objAccGrpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_ACCGRPCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        Dim strOpCd_ACC As String = "GL_CLSSETUP_SUBBLOCK_ACCOUNT_LIST_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow

        If rbTypeNursery.Checked = True Or rbTypeInMatureField.Checked = True Then
            strParam = strSelectedSubBlkCode & "|" & objGLSetup.EnumAccStatus.Active & "|" & _
                       objGLSetup.EnumAccountType.BalanceSheet & "|Or NurseryInd = '" & objGLSetup.EnumNurseryAccount.Yes & "'"' and (MedAccDistUse = '" & objGLSetup.EnumMedAccDist.no & "' or HousingAccDistUse = '" & objGLSetup.EnumHousingAccDist.no & "')"
		Elseif rbTypeOff.checked=true then
            strParam = strSelectedSubBlkCode & "|" & objGLSetup.EnumAccStatus.Active & "|" & _
                       objGLSetup.EnumAccountType.BalanceSheet & "','" & objGLSetup.EnumAccountType.ProfitAndLost & "|and NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "' and (AccPurpose IN ('" & objGLSetup.EnumAccountPurpose.NonVehicle & "','" & objGLSetup.EnumAccountPurpose.Others & "') or (MedAccDistUse = '" & objGLSetup.EnumMedAccDist.Yes & "' or HousingAccDistUse = '" & objGLSetup.EnumHousingAccDist.Yes & "'))"
        Else
            strParam = strSelectedSubBlkCode & "|" & objGLSetup.EnumAccStatus.Active & "|" & _
                       objGLSetup.EnumAccountType.ProfitAndLost & "|and NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "' and (AccPurpose IN ('" & objGLSetup.EnumAccountPurpose.NonVehicle & "','" & objGLSetup.EnumAccountPurpose.Others & "')  and (medaccdistuse is null or housingaccdistuse is null or (medaccdistuse='" & objGLSetup.EnumMedAccDist.no & "' and housingaccdistuse='" & objGLSetup.EnumhousingAccDist.no & "')))"
        End If

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetSubBlock(strOpCd_ACC, _
                                                 strLocation, _
                                                 strParam, _
                                                 objAccDs, _
                                                 True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_ACCOUNT_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_ACCOUNTLINE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton

        If Trim(LCase(lblHidCostLevel.Text)) = "subblock" Then
            strParam = strSelectedSubBlkCode & "|" & objGLSetup.EnumAccStatus.Active & "||"
            Try
                intErrNo = objGLSetup.mtdGetSubBlock(strOpCd, _
                                                     strLocation, _
                                                     strParam, _
                                                     objSubBlkLnDs, _
                                                     True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCKLINE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
            dgLineDet.DataSource = objSubBlkLnDs.Tables(0)
            dgLineDet.DataBind()

            For intCnt = 0 To dgLineDet.Items.Count - 1
                Select Case CInt(objSubBlkDs.Tables(0).Rows(0).Item("Status"))
                    Case objGLSetup.EnumSubBlockStatus.Active, objGLSetup.EnumSubBlockStatus.Pending
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        If ddlTransferSubBlk.SelectedIndex = 0 Then  
                            lbButton.Visible = True
                        Else
                            lbButton.Visible = False
                        End If
                       lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    Case objGLSetup.EnumSubBlockStatus.Deleted
                        lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
                End Select
            Next
        End If

    End Sub

    Sub onLoad_BindButton()
        txtSubBlkCode.Enabled = True
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
        rbTypeMatureField.Enabled = True
        rbTypeInMatureField.Enabled = True
        rbTypeOff.Enabled = True
        rbTypeNursery.Enabled = True
        txtStdPerArea.Enabled = False
        ddlBlock.Enabled = False
        ddlTransferSubBlk.Enabled = False
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
        txtEstimatedBJR.Enabled = False

        rbTypeMill.Enabled = False
        txtMachineCap.Enabled = False
        txtStdRunHour.Enabled = False
        rbProcessCtrlYes.Enabled = False
        rbProcessCtrlNo.Enabled = False

        txtUrea.Enabled = False
        txtRP.Enabled = False
        txtKclMop.Enabled = False
        txtKliserit.Enabled = False
        txtDolomit.Enabled = False
        txtHGFB.Enabled = False
        txtMillEff.Enabled = False
        txtJJ.Enabled = False

        Select Case intStatus
            Case objGLSetup.EnumSubBlockStatus.Active, objGLSetup.EnumSubBlockStatus.Pending                
                If Trim(LCase(lblHidCostLevel.Text)) = "subblock" Then   
                  If ddlTransferSubBlk.SelectedIndex = 0 Then 
                    txtDescription.Enabled = True
                    txtPlantDate.Enabled = True
                    txtStartArea.Enabled = True
                    txtTotalArea.Enabled = True
                    txtTransferDate.Enabled = True
                    txtBunchRatio.Enabled = True
                    txtPlantMaterial.Enabled = True
                    txtHarvestStartDate.Enabled = True
                    txtInitialDate.Enabled = True
                    txtTotalStand.Enabled = True
                    txtStdPerArea.Enabled = True
                    txtEstimatedBJR.Enabled = True
                    ddlBlock.Enabled = True
                    ddlTransferSubBlk.Enabled = True
                    ddlAreaUOM.Enabled = True
                    ddlYieldUOM.Enabled = True
                    ddlAreaAvgUOM.Enabled = True
                    ddlYieldAvgUOM.Enabled = True
                    SaveBtn.Visible = True
                    DelBtn.Visible = True
                    DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    If Trim(LCase(lblHidCostLevel.Text)) = "subblock" Then
                        txtQuota.Enabled = True
                        rbByHour.Enabled = True
                        rbByVolume.Enabled = True
                        txtQuotaIncRate.Enabled = True
                        tblSelection.Visible = True
                    End If
                    if strLocType = objLoc.EnumLocType.Mill then 
                        btnSelHarvStartDate.visible = false
                        btnSelInitialDate.visible = false 
                        btnSelTransferDate.visible = false 
                        btnSelPlantDate.Visible = True
                    else
                        btnSelPlantDate.Visible = True
                        btnSelTransferDate.Visible = True
                        btnSelHarvStartDate.Visible = True
                        btnSelInitialDate.Visible = True
                    end if 
                    rbPlantedArea.Enabled = True
                    rbUnplantedArea.Enabled = True
                    rbLandClearing.Enabled = True
                    rbExtension.Enabled = True
                    ddlBlkTypeInRpt.Enabled = True
                    rbTypeMill.Enabled = True
                    txtMachineCap.Enabled = True
                    txtStdRunHour.Enabled = True
                    rbProcessCtrlYes.Enabled = True
                    rbProcessCtrlNo.Enabled = True

                    txtUrea.Enabled = True
                    txtRP.Enabled = True
                    txtKclMop.Enabled = True
                    txtKliserit.Enabled = True
                    txtDolomit.Enabled = True
                    txtHGFB.Enabled = True
                    txtMillEff.Enabled = True
                    txtJJ.Enabled = True
                Else
                    txtDescription.Enabled = True
                    txtPlantDate.Enabled = True
                    txtStartArea.Enabled = True
                    txtTotalArea.Enabled = True
                    txtTransferDate.Enabled = True
                    txtBunchRatio.Enabled = True
                    txtPlantMaterial.Enabled = True
                    txtHarvestStartDate.Enabled = True
                    txtInitialDate.Enabled = True
                    txtTotalStand.Enabled = True
                    txtStdPerArea.Enabled = True

                    txtEstimatedBJR.Enabled = True

                    ddlBlock.Enabled = True
                    ddlTransferSubBlk.Enabled = false
                    ddlAreaUOM.Enabled = True
                    ddlYieldUOM.Enabled = True
                    ddlAreaAvgUOM.Enabled = True
                    ddlYieldAvgUOM.Enabled = True
                    SaveBtn.Visible = True
                    DelBtn.Visible = True
                    DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    If Trim(LCase(lblHidCostLevel.Text)) = "subblock" Then
                        txtQuota.Enabled = True
                        rbByHour.Enabled = True
                        rbByVolume.Enabled = True
                        txtQuotaIncRate.Enabled = True
                        tblSelection.Visible = True
                    End If
                    if strLocType = objLoc.EnumLocType.Mill then 
                        btnSelHarvStartDate.visible = false
                        btnSelInitialDate.visible = false 
                        btnSelTransferDate.visible = false 
                        btnSelPlantDate.Visible = True
                    else
                        btnSelPlantDate.Visible = True
                        btnSelTransferDate.Visible = True
                        btnSelHarvStartDate.Visible = True
                        btnSelInitialDate.Visible = True
                    end if 

                        rbPlantedArea.Enabled = True
                        rbUnplantedArea.Enabled = True
                        rbLandClearing.Enabled = True
                        rbExtension.Enabled = True
                        ddlBlkTypeInRpt.Enabled = True

                        rbTypeMill.Enabled = True
                        txtMachineCap.Enabled = True
                        txtStdRunHour.Enabled = True
                        rbProcessCtrlYes.Enabled = True
                        rbProcessCtrlNo.Enabled = True

                        txtUrea.Enabled = True
                        txtRP.Enabled = True
                        txtKclMop.Enabled = True
                        txtKliserit.Enabled = True
                        txtDolomit.Enabled = True
                        txtHGFB.Enabled = True
                        txtMillEff.Enabled = True
                        txtJJ.Enabled = True
            
                    DelBtn.Visible = True
                    DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                End If
            end if
            txtSubBlkCode.Enabled = False

            Case objGLSetup.EnumSubBlockStatus.Deleted
                UnDelBtn.Visible = True

            Case Else                
                txtSubBlkCode.Enabled = True
                txtDescription.Enabled = True
                txtPlantDate.Enabled = True
                txtStartArea.Enabled = True
                txtTotalArea.Enabled = True
                txtTransferDate.Enabled = True
                txtBunchRatio.Enabled = True
                txtEstimatedBJR.Enabled = True
                txtPlantMaterial.Enabled = True
                txtHarvestStartDate.Enabled = True
                rbTypeMatureField.Enabled = True
                rbTypeInMatureField.Enabled = True
                rbTypeOff.Enabled = True

                If Trim(LCase(lblHidCostLevel.Text)) = "subblock" Then
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
                txtStdPerArea.Enabled = True
                ddlBlock.Enabled = True
                ddlAreaUOM.Enabled = True
                ddlYieldUOM.Enabled = True
                ddlAreaAvgUOM.Enabled = True
                ddlYieldAvgUOM.Enabled = True
                SaveBtn.Visible = True
                If Trim(LCase(lblHidCostLevel.Text)) = "subblock" Then
                    tblSelection.Visible = True
                End If
                If strLocType = objLoc.EnumLocType.Mill Then
                    btnSelPlantDate.Visible = True
                    btnSelTransferDate.Visible = False
                    btnSelHarvStartDate.Visible = False
                    btnSelInitialDate.Visible = False
                Else
                    btnSelPlantDate.Visible = True
                    btnSelTransferDate.Visible = True
                    btnSelHarvStartDate.Visible = True
                    btnSelInitialDate.Visible = True
                End If
                rbPlantedArea.Enabled = True
                rbUnplantedArea.Enabled = True
                rbLandClearing.Enabled = True
                rbExtension.Enabled = True
                ddlBlkTypeInRpt.Enabled = True
                rbTypeMill.Enabled = True
                txtMachineCap.Enabled = True
                txtStdRunHour.Enabled = True
                rbProcessCtrlYes.Enabled = True
                rbProcessCtrlNo.Enabled = True
                txtUrea.Enabled = True
                txtRP.Enabled = True
                txtKclMop.Enabled = True
                txtKliserit.Enabled = True
                txtDolomit.Enabled = True
                txtHGFB.Enabled = True
                txtMillEff.Enabled = True
                txtJJ.Enabled = True
        End Select
    End Sub


    Sub BindBlock(ByVal pv_strBlkCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_SUBBLOCK_BLOCKCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String

        strSort = "order by BlkCode"
        strSearch = "where Status = '" & objGLSetup.EnumBlockStatus.Active & "' and LocCode = '" & strLocation & "'"

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Block, _
                                                   objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_BLOCKCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & Trim(objBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = "Select " & lblBlock.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectIndex
    End Sub

    Sub BindTransferSubBlock(ByVal pv_strSubBlockCode As String, ByVal pv_strTransferBlock As String)
        Dim strOpCode As String = "GL_CLSSETUP_SUBBLOCK_TRANSFER_SUBBLOCK_CODE_GET"
        Dim strOppCd_GET As String = "GL_CLSSETUP_VEHICLETYPE_LIST_GET"     
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String

        If  strLocType = objLoc.EnumLocType.Mill Then 
            strSearch = " AND veh.Status = '" & objGLSetup.EnumVehTypeStatus.Active & "' "
            strParam = "|" & strSearch

            Try
                intErrNo = objGLSetup.mtdGetMasterList(strOppCd_GET, strParam, objGLSetup.EnumGLMasterType.VehType, objTransSubBlkDs)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLETYPE_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_VehicleType.aspx")
            End Try

            For intCnt = 0 To objTransSubBlkDs.Tables(0).Rows.Count - 1
                objTransSubBlkDs.Tables(0).Rows(intCnt).Item("VehTypeCode") = Trim(objTransSubBlkDs.Tables(0).Rows(intCnt).Item("VehTypeCode"))
                objTransSubBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objTransSubBlkDs.Tables(0).Rows(intCnt).Item("Description"))
                If objTransSubBlkDs.Tables(0).Rows(intCnt).Item("VehTypeCode") = Trim(pv_strTransferBlock) Then
                    intSelectIndex = intCnt + 1
                End If
            Next
            dr = objTransSubBlkDs.Tables(0).NewRow()
            dr("VehTypeCode") = ""
            dr("Description") = "Select Machine Category"
            objTransSubBlkDs.Tables(0).Rows.InsertAt(dr, 0)
            ddlTransferSubBlk.DataSource = objTransSubBlkDs.Tables(0)
            ddlTransferSubBlk.DataValueField = "VehTypeCode"
            ddlTransferSubBlk.DataTextField = "Description"
            ddlTransferSubBlk.DataBind()
            ddlTransferSubBlk.SelectedIndex = intSelectIndex
        Else
            strParam = pv_strSubBlockCode & "|" & objGLSetup.EnumSubBlockStatus.Active & "||"

            Try
                intErrNo = objGLSetup.mtdGetBlock(strOpCode, _
                                                  strLocation, _
                                                  strParam, _
                                                  objTransSubBlkDs, _
                                                  True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_TRANSFERSUBBLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            For intCnt = 0 To objTransSubBlkDs.Tables(0).Rows.Count - 1
                objTransSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objTransSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
                objTransSubBlkDs.Tables(0).Rows(intCnt).Item("Description") = objTransSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") & " (" & Trim(objTransSubBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
                If objTransSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(pv_strTransferBlock) Then
                    intSelectIndex = intCnt + 1
                End If
            Next                 
            dr = objTransSubBlkDs.Tables(0).NewRow()
            dr("SubBlkCode") = ""
            dr("Description") = "Select " & lblSubBlkCode.Text
            objTransSubBlkDs.Tables(0).Rows.InsertAt(dr, 0)
            ddlTransferSubBlk.DataSource = objTransSubBlkDs.Tables(0)
            ddlTransferSubBlk.DataValueField = "SubBlkCode"
            ddlTransferSubBlk.DataTextField = "Description"
            ddlTransferSubBlk.DataBind()
            ddlTransferSubBlk.SelectedIndex = intSelectIndex
        End If


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
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_UOMCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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


    Sub InsertSubBlockRecord(ByRef blnExceedLicSize As Boolean, ByRef blnError As Boolean)
        Dim strOpCd_Upd As String = "GL_CLSSETUP_SUBBLOCK_DETAILS_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_BLOCK_GET_BY_SUBBLKCODE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_SUBBLOCK_ADD"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strOpCd As String
        Dim strTransferBlk As String
        Dim dblStartArea As Double
        Dim dblTotalArea As Double
        Dim dblStdPerArea As Double
        Dim intSubBlkType As Integer
        Dim dblTotalStand As Double
        Dim dblBunchRatio As Double
        Dim dblQuota As Double
        Dim intQuotaMethod As Integer
        Dim dblQuotaIncRate As Double
        Dim objDateFormat As String
        Dim strTransferDate As String
        Dim strInitialDate As String

        Dim strValidPlantDate As String
        Dim strValidTransDate As String
        Dim strValidHarvStartDate As String
        Dim strValidIniChargeDate As String
        Dim decBlkTotalArea As Decimal = 0
        Dim decSubBlkAreaSum As Decimal = 0
        Dim intGrpType As Integer
        Dim dblEstimatedBJR as Double
        Dim strProcessCtrl As String
        Dim strUrea As String
        Dim strRP As String
        Dim strKclMop As String
        Dim strKliserit As String
        Dim strDolomit As String
        Dim strHGFB As String
        Dim strMillEff As String
        Dim strJJ As String
      
        If Not txtPlantDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFMt, txtPlantDate.Text, objDateFormat, strValidPlantDate) = False Then
                lblPlantDateFmt.Text = objDateFormat & "."
                lblPlantDate.Visible = True
                lblPlantDateFmt.Visible = True

                blnError = True
            End If
        End If

        If strLocType <> objLoc.EnumLocType.Mill Then

            If txtTransferDate.Text <> "" And ddlTransferSubBlk.SelectedIndex = 0 Then
                lblErrTransferSubBlk.Visible = True
                blnError = True
                Exit Sub
            ElseIf txtTransferDate.Text = "" And ddlTransferSubBlk.SelectedIndex > 0 Then
                lblErrTransferSubBlkDate.Visible = True
                blnError = True
                Exit Sub
            End If
        End If

        If Not txtTransferDate.Text = "" Then

            If objGlobal.mtdValidInputDate(strDateFMt, txtTransferDate.Text, objDateFormat, strValidTransDate) = False Then
                lblTransferDateFmt.Text = objDateFormat & "."
                lblTransferDate.Visible = True
                lblTransferDateFmt.Visible = True
                blnError = True
            End If
        End If

        If Not txtHarvestStartDate.Text = "" Then

            If objGlobal.mtdValidInputDate(strDateFMt, txtHarvestStartDate.Text, objDateFormat, strValidHarvStartDate) = False Then
                lblHarvStartDateFmt.Text = objDateFormat & "."
                lblHarvStartDate.Visible = True
                lblHarvStartDateFmt.Visible = True
                blnError = True
            End If
        End If

        If Not txtInitialDate.Text = "" Then

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
            intSubBlkType = objGLSetup.EnumSubBlockType.Office
        ElseIf rbTypeMatureField.Checked = True Then
            intSubBlkType = objGLSetup.EnumSubBlockType.MatureField
        ElseIf rbTypeInMatureField.Checked = True Then
            intSubBlkType = objGLSetup.EnumSubBlockType.InMatureField
        ElseIf rbTypeNursery.Checked = True Then
            intSubBlkType = objGLSetup.EnumSubBlockType.Nursery
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

        If txtBunchRatio.Text = "" Then
            dblBunchRatio = 1
        Else
            dblBunchRatio = txtBunchRatio.Text
        End If

        If Trim(txtQuota.Text) = "" Then
            dblQuota = 0
        Else
            dblQuota = txtQuota.Text
        End If

        If Trim(txtEstimatedBJR.Text) = "" Then
            dblEstimatedBJR = 0
        Else
            dblEstimatedBJR = txtEstimatedBJR.Text
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

        If rbProcessCtrlYes.Checked Then
            strProcessCtrl = objGLSetup.EnumProcessControl.Yes
        Else
            strProcessCtrl = objGLSetup.EnumProcessControl.No
        End If

        If txtUrea.Text = "" Then
            strUrea = "0"
        Else
            strUrea = txtUrea.Text
        End If
        If txtRP.Text = "" Then
            strRP = "0"
        Else
            strRP = txtRP.Text
        End If
        If txtKclMop.Text = "" Then
            strKclMop = "0"
        Else
            strKclMop = txtKclMop.Text
        End If
        If txtKliserit.Text = "" Then
            strKliserit = "0"
        Else
            strKliserit = txtKliserit.Text
        End If
        If txtDolomit.Text = "" Then
            strDolomit = "0"
        Else
            strDolomit = txtDolomit.Text
        End If
        If txtHGFB.Text = "" Then
            strHGFB = "0"
        Else
            strHGFB = txtHGFB.Text
        End If
        If txtMillEff.Text = "" Then
            strMillEff = "0"
        Else
            strMillEff = txtMillEff.Text
        End If
        If txtJJ.Text = "" Then
            strJJ = "0"
        Else
            strJJ = txtJJ.Text
        End If

        strParam = Trim(txtSubBlkCode.Text) & "|||"
        Try
            intErrNo = objGLSetup.mtdGetSubBlock(strOpCd_Get, _
                                                 strLocation, _
                                                 strParam, _
                                                 objSubBlkDs, _
                                                 True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_GET_BY_SUBBLOCKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objSubBlkDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDup.Visible = True
            Exit Sub
        Else        
            If strLocType = objAdminLoc.EnumLocType.Estate Then
                Try
                    intErrNo = objGLSetup.mtdGetTotalArea(strLocation, _
                                                         Trim(ddlBlock.SelectedItem.Value), _
                                                         "", _
                                                         True, _
                                                         decBlkTotalArea)
                    intErrNo = objGLSetup.mtdGetTotalArea(strLocation, _
                                                         Trim(ddlBlock.SelectedItem.Value), _
                                                         " AND SubBlkCode <> '" & Replace(Trim(txtSubBlkCode.Text), "'", "''") & "' AND Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'", _
                                                         False, _
                                                         decSubBlkAreaSum)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx")
                End Try
                decSubBlkAreaSum = Decimal.Add(decSubBlkAreaSum, dblTotalArea)               
                'If Decimal.Compare(decSubBlkAreaSum, decBlkTotalArea) > 0 Then
                '    lblErrTotalSize.Visible = True
                '    Exit Sub
                'End If
            End If

            strSelectedSubBlkCode = Trim(txtSubBlkCode.Text)
            strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
            tbcode.Value = strSelectedSubBlkCode
            If intStatus = 0 Then
                strTransferBlk = ""
            Else
                strTransferBlk = Trim(ddlTransferSubBlk.SelectedItem.Value)
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
            strParam = Trim(txtSubBlkCode.Text) & Chr(9) & _
                       Trim(txtDescription.Text) & Chr(9) & _
                       ddlBlock.SelectedItem.Value & Chr(9) & _
                       strValidPlantDate & Chr(9) & _
                       intSubBlkType & Chr(9) & _
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
                       iif(Trim(txtPlantMaterial.Text) = "", "0", Trim(txtPlantMaterial.Text))  & Chr(9) & _
                       dblStdPerArea & Chr(9) & _
                       strValidHarvStartDate & Chr(9) & _
                       objGLSetup.EnumSubBlockStatus.Active & Chr(9) & _
                       dblTotalStand & Chr(9) & _
                       strValidIniChargeDate & Chr(9) & _
                       intGrpType & Chr(9) & _
                       iif(ddlBlkTypeInRpt.SelectedItem.Value = "", 2, ddlBlkTypeInRpt.SelectedItem.Value)  & Chr(9) & _
                       dblEstimatedBJR & Chr(9) & _
                       iif(Trim(txtMachineCap.text) = "", 0, Trim(txtMachineCap.text)) & Chr(9) & _
                       iif(Trim(txtStdRunHour.text) = "", 0, Trim(txtStdRunHour.text)) & Chr(9) & _
                       strProcessCtrl & Chr(9) & _
                       strUrea & Chr(9) & _
                       strRP & Chr(9) & _
                       strKclMop & Chr(9) & _
                       strKliserit & Chr(9) & _
                       strDolomit & Chr(9) & _
                       strHGFB & Chr(9) & _
                       strMillEff & Chr(9) & _
                       strJJ & Chr(9) & _
                       iif(Trim(txtActHourMeter.text) = "", 0, Trim(txtActHourMeter.text)) & Chr(9) & _
                       iif(Trim(txtExpHourMeter.text) = "", 0, Trim(txtExpHourMeter.text)) & Chr(9) & _
					   ddl1.SelectedItem.Value & Chr(9) & _
					   ddl2.text.toUpper().trim() 
                       
                      
            Try
                intErrNo = objGLSetup.mtdUpdSubBlock(strOpCd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     False, _
                                                     blnExceedLicSize)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_subblockdet.aspx")
            End Try
            If blnExceedLicSize = True Then lblErrLicSize.Visible = True
        End If
		
		
    End Sub

    Sub OnTypeChanged(ByVal Sender As Object, ByVal E As EventArgs)
        BindAccGrpCode()
        BindAccount()
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Upd As String = "GL_CLSSETUP_SUBBLOCK_UPD"
        Dim strOpCd_Add As String = "GL_CLSSETUP_SUBBLOCK_ADD"
        Dim strOpCd_BlockDet_Upd As String = "BD_CLSTRX_BLOCK_OR_SUBBLOCK_DETAILS_UPD_SP" 
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim blnExceedLicSize As Boolean
        Dim blnError As Boolean = False
        Dim dblTotalArea As Double
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim blnIsInUse As Boolean = False
        Dim decBlkTotalArea As Decimal = 0
        Dim decSubBlkAreaSum As Decimal = 0

        If Trim(txtTotalArea.Text) = "" Then
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
                InsertSubBlockRecord(blnExceedLicSize, blnError)
                CopyBtnKlik(Sender,E)
                If blnError Then
                    Exit Sub
                Else
                    strParam = txtSubBlkCode.Text.Trim & "|"

                    Try
                        intErrNo = objBDTrx.mtdUpdBlockDetails(strOpCd_BlockDet_Upd, strCompany, strLocation, _
                                                            strUserId, strParam, strYieldLevel)
                    Catch exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_DETAILS_UPD_BD_TRX&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_subblockdet.aspx?tbcode=" & strSelectedSubBlkCode)
                    End Try
                End If
            Else 'hidRecStatus.Value = "Saved"
                If hidOriSubBlkCode.Value = txtSubBlkCode.Text.Trim Then
                    InsertSubBlockRecord(blnExceedLicSize, blnError)

                    If blnError Then
                        Exit Sub
                    Else
                        strParam = txtSubBlkCode.Text.Trim & "|"

                        Try
                            intErrNo = objBDTrx.mtdUpdBlockDetails(strOpCd_BlockDet_Upd, strCompany, strLocation, _
                                                                strUserId, strParam, strYieldLevel)
                        Catch exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_DETAILS_UPD_BD_TRX&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_subblockdet.aspx?tbcode=" & strSelectedSubBlkCode)
                        End Try
                    End If
                Else
                    intErrNo = objGLSetup.mtdCheckSetupCodeUsage("SubBlkCode", hidOriSubBlkCode.Value, blnIsInUse)

                    If blnIsInUse = True Then
                        Response.Write("<script language='javascript'>alert('Transaction exists for Sub Block Code " & hidOriSubBlkCode.Value & ". Editing this Sub Block Code is not allowed.')</script>")
                    Else
                        intStatus = 0
                        intErrNo = objGLSetup.mtdDelPrevCode("SubBlkCode", hidOriSubBlkCode.Value)
    
                        InsertSubBlockRecord(blnExceedLicSize, blnError)
                        If blnError Then
                            Exit Sub
                        Else
                            strParam = txtSubBlkCode.Text.Trim & "|"

                            Try
                                intErrNo = objBDTrx.mtdUpdBlockDetails(strOpCd_BlockDet_Upd, strCompany, strLocation, _
                                                                    strUserId, strParam, strYieldLevel)
                            Catch exp As System.Exception
                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_DETAILS_UPD_BD_TRX&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_subblockdet.aspx?tbcode=" & strSelectedSubBlkCode)
                            End Try
                        End If
                    End If
                End If                
            End If

            If blnIsInUse = False Then
                hidRecStatus.Value = "Saved"
                hidOriSubBlkCode.Value = txtSubBlkCode.Text.Trim
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtSubBlkCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       objGLSetup.EnumSubBlockStatus.Deleted & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9)
            Try
                intErrNo = objGLSetup.mtdUpdSubBlock(strOpCd_Upd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     True, _
                                                     blnExceedLicSize)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_DETAILS_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_subblockdet.aspx?tbcode=" & strSelectedSubBlkCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            If strLocType = objAdminLoc.EnumLocType.Estate Then
                Try
                    intErrNo = objGLSetup.mtdGetTotalArea(strLocation, _
                                                            Trim(ddlBlock.SelectedItem.Value), _
                                                            "", _
                                                            True, _
                                                            decBlkTotalArea)
                    intErrNo = objGLSetup.mtdGetTotalArea(strLocation, _
                                                            Trim(ddlBlock.SelectedItem.Value), _
                                                            " AND SubBlkCode <> '" & Replace(Trim(txtSubBlkCode.Text), "'", "''") & "' AND Status = '" & objGLSetup.EnumSubBlockStatus.Active & "'", _
                                                            False, _
                                                            decSubBlkAreaSum)
                Catch Exp As System.Exception
                    Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_BLOCK_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_BlockDet.aspx")
                End Try
               ' decSubBlkAreaSum = Decimal.Add(decSubBlkAreaSum, dblTotalArea)
               ' If Decimal.Compare(decSubBlkAreaSum, decBlkTotalArea) > 0 Then
               '     lblErrTotalSize.Visible = True
               '     Exit Sub
               ' End If
            End If

            strParam = Trim(txtSubBlkCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       dblTotalArea & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       objGLSetup.EnumSubBlockStatus.Active & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9)
            Try
                intErrNo = objGLSetup.mtdUpdSubBlock(strOpCd_Upd, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     True, _
                                                     blnExceedLicSize)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_DETAILS_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_subblockdet.aspx?tbcode=" & strSelectedSubBlkCode)
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
        Dim strOpCode_UpdID As String = "GL_CLSSETUP_SUBBLOCK_ACCOUNT_UPDATEID"
        Dim strOpCode_GetAccCode As String = "GL_CLSSETUP_BLOCK_ACCCODE_GET_BY_ACCGRPCODE"
        Dim strOpCode_DelAccCode As String = "GL_CLSSETUP_SUBBLOCK_ACCCODE_DELETE_BY_ACCGRPCODE"
        Dim strOpCode_AddLine As String 
        Dim strParam As String
        Dim strAccCode As String
        Dim strAccGrpCode As String
        Dim intErrNo As Integer
        Dim blnExceedLicSize As Boolean
        Dim blnError As Boolean = False

        Try
            strAccCode = ddlAccount.SelectedItem.Value
        Catch Exp As System.Exception
            Exit Sub
        End Try

        Try
            strAccGrpCode = ddlAccGrp.SelectedItem.Value
        Catch Exp As System.Exception
            Exit Sub
        End Try

        If strAccGrpCode = "" And strAccCode = "" Then
            lblErrNotSelect.Visible = True
            Exit Sub
        elseIf strAccGrpCode <> "" And strAccCode <> "" Then
            lblErrSelectBoth.Visible = True
            Exit Sub
        ElseIf strAccGrpCode <> "" And strAccCode = "" Then
            strOpCode_AddLine = "GL_CLSSETUP_SUBBLOCK_LINE_ADD"
        ElseIf strAccGrpCode = "" And strAccCode <> "" Then
            strOpCode_AddLine = "GL_CLSSETUP_SUBBLOCK_LINE_ACCCODE_ADD"
        End If


        'InsertSubBlockRecord(blnExceedLicSize, blnError)
        'If blnError Then
        '    Exit Sub
        'End If

        If strSelectedSubBlkCode = "" Then
            Exit Sub
        Else
            Try
                strParam = strSelectedSubBlkCode & "|" & strAccCode & "|" & strAccGrpCode
                If rbTypeNursery.Checked = True Or rbTypeInMatureField.Checked = True Then     
                    strParam = strParam & "| AND Status = '" & objGLSetup.EnumAccStatus.Active & "' AND AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "' Or NurseryInd = '" & objGLSetup.EnumNurseryAccount.Yes & "' and (MedAccDistUse = '" & objGLSetup.EnumMedAccDist.no & "' or HousingAccDistUse = '" & objGLSetup.EnumHousingAccDist.no & "')"
               else
                    strParam = strParam & "| AND Status = '" & objGLSetup.EnumAccStatus.Active & "' AND AccType = '" & objGLSetup.EnumAccountType.ProfitAndLost & "' AND NurseryInd = '" & objGLSetup.EnumNurseryAccount.No & "' and AccPurpose IN ('"  & objGLSetup.EnumAccountPurpose.NonVehicle & "','" & objGLSetup.EnumAccountPurpose.Others & "') and (MedAccDistUse = '" & objGLSetup.EnumMedAccDist.no & "' or HousingAccDistUse = '" & objGLSetup.EnumHousingAccDist.no & "')"
                End If
                intErrNo = objGLSetup.mtdUpdSubBlockLine(strOpCode_UpdID, _
                                                         strOpCode_AddLine, _
                                                         strOpCode_GetAccCode, _
                                                         strOpCode_DelAccCode, _
                                                         strCompany, _
                                                         strLocation, _
                                                         strUserId, _
                                                         strParam, _
                                                         objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCKLINE_ADD&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_subblockdet.aspx?tbcode=" & strSelectedSubBlkCode)
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
        Dim strOpCode_DelLine As String = "GL_CLSSETUP_SUBBLOCK_LINE_DELETE"
        Dim strOpCode_UpdID As String = "GL_CLSSETUP_SUBBLOCK_ACCOUNT_UPDATEID"
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
            strParam = strSelectedSubBlkCode & "|" & strAccCode & "|" & "" & "|"
            intErrNo = objGLSetup.mtdUpdSubBlockLine(strOpCode_UpdID, _
                                                     strOpCode_DelLine, _
                                                     strOpCode_GetAccCode, _
                                                     strOpCode_DelAccCode, _
                                                     strCompany, _
                                                     strLocation, _
                                                     strUserId, _
                                                     strParam, _
                                                     objResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCKLINE_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_subblockdet.aspx?tbcode=" & strSelectedSubBlkCode)
        End Try

        onLoad_Display()
        onLoad_LineDisplay()
        BindAccGrpCode()
        BindAccount()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_SubBlock.aspx")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.SubBlock))
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code"
        lblSubBlkType.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
        lblSubBlkDesc.Text = GetCaption(objLangCap.EnumLangCap.SubBlockDesc)
        lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        lblStartArea.Text = GetCaption(objLangCap.EnumLangCap.StartArea)
        lblBlock2.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)
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
        lblErrTransferSubBlk.Text = lblPlsSelect.Text & GetCaption(objLangCap.EnumLangCap.SubBlock)
        lblDateOfPlanting.Text = GetCaption(objLangCap.EnumLangCap.DateOfPlanting)

        lblSubBlkMgrRpt.Text = GetCaption(objLangCap.EnumLangCap.SubBlock)


        rfvSubBlkCode.ErrorMessage = "<br>" & lblPleaseEnter.Text & lblSubBlkCode.Text & "."
        rfvSubBlkDesc.ErrorMessage = lblPleaseEnter.Text & lblSubBlkDesc.Text & "."
        rfvBlk.ErrorMessage = lblPlsSelect.Text & lblBlock.Text & "."
        rfvAreaUOM.Text = lblPlsSelectUOM.Text & lblArea.Text & "."
        rfvYieldUOM.Text = lblPlsSelectUOM.Text & lblYield.Text & "."
        rfvAreaAvgUOM.Text = lblPlsSelectUOM.Text & lblAreaAvg.Text & "."
        rfvYieldAvgUOM.Text = lblPlsSelectUOM.Text & lblYieldAvg.Text & "."

        lblErrSelectBoth.Text = lblPlsSelectEither.Text & lblAccGrp.Text & lblOr.Text & lblAccount.Text & lblOnly.Text
        lblErrNotSelect.Text = lblPlsSelect.Text & lblAccGrp.Text & lblOr.Text & lblAccount.Text & "."

        dgLineDet.Columns(0).HeaderText = lblAccount.Text
        dgLineDet.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.AccDesc)
        lblErrTotalSize.Text = "<br>The sum of Total Area from all active " & GetCaption(objLangCap.EnumLangCap.SubBlock) & "s that belong to this " & GetCaption(objLangCap.EnumLangCap.Block) & " cannot be greater than the Total Area defined for this " & GetCaption(objLangCap.EnumLangCap.Block) & "."
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLKDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_SubBlockDet.aspx")
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
        ddlBlkTypeInRpt.Items.Add(New ListItem("Select Sub Block Type in Managerial Report", ""))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetPlantedAreaBlkType(objGLSetup.EnumPlantedAreaBlkType.Mature), objGLSetup.EnumPlantedAreaBlkType.Mature))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetPlantedAreaBlkType(objGLSetup.EnumPlantedAreaBlkType.Immature), objGLSetup.EnumPlantedAreaBlkType.Immature))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetPlantedAreaBlkType(objGLSetup.EnumPlantedAreaBlkType.NewPlanting), objGLSetup.EnumPlantedAreaBlkType.NewPlanting))
        
        If Not Trim(pv_strRptBlkType) = "" Then
            ddlBlkTypeInRpt.SelectedValue = pv_strRptBlkType
        End If
    End Sub

    Sub BindBlkTypeInRptWithUnplantedArea(Optional ByVal pv_strRptBlkType As String = "")
        ddlBlkTypeInRpt.Items.Clear
        ddlBlkTypeInRpt.Items.Add(New ListItem("Select Sub Block Type in Managerial Report", ""))
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
        ddlBlkTypeInRpt.Items.Add(New ListItem("Select Sub Block Type in Managerial Report", ""))
        ddlBlkTypeInRpt.Items.Add(New ListItem(objGLSetup.mtdGetLandClearingBlkType(objGLSetup.EnumLandClearingBlkType.LandClearing), objGLSetup.EnumLandClearingBlkType.LandClearing))

        If Not Trim(pv_strRptBlkType) = "" Then
            ddlBlkTypeInRpt.SelectedValue = pv_strRptBlkType
        End If
    End Sub

    Sub BindBlkTypeInRptWithExtension(Optional ByVal pv_strRptBlkType As String = "")
        ddlBlkTypeInRpt.Items.Clear
        ddlBlkTypeInRpt.Items.Add(New ListItem("Select Sub Block Type in Managerial Report", ""))
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
            TRDailyQuota.visible = false
            TRQuotaInc.visible = false
            TREstimatedBJR.visible = false 
            TRMaterialPlant.visible = false 
            TRStdPerArea.visible = false 
            TRTotalStand.visible = false 
            TRGroupType.visible = false 
			TRHistoriBlk.visible = false 

            lblMachineCap.visible = true
            txtMachineCap.visible = true 


            txtStdRunHour.visible = true
            lblStdRunHour.visible = true
            lblActHourMeter.Visible = True
            txtActHourMeter.Visible = True
            txtActHourMeter.Enabled = False
            lblExpHourMeter.Visible = True
            txtExpHourMeter.Visible = True

            TRSpacer.Visible =  false
            TRFert.Visible =  false
            TRFert1.Visible =  false
            TRFert2.Visible =  false
            TRFert3.Visible =  false
            TRFert4.Visible =  false
         else



            btnSelHarvStartDate.visible = true
            btnSelInitialDate.visible = true 
            btnSelTransferDate.visible = true 

            TRStartArea.visible = true
            TRTotalArea.visible = true 
            TRDailyQuota.visible = true
            TRQuotaInc.visible = true
            TREstimatedBJR.visible = true 
            TRMaterialPlant.visible = true 
            TRStdPerArea.visible = true 
            TRTotalStand.visible = true 
            TRGroupType.visible = true
			TRHistoriBlk.visible = true

            lblMachineCap.visible = false
            txtMachineCap.visible = false 


            txtStdRunHour.visible = false
            lblStdRunHour.visible = false
            lblActHourMeter.Visible = False
            txtActHourMeter.Visible = False
            lblExpHourMeter.Visible = False
            txtExpHourMeter.Visible = False

        end if 
        lblProcessCtrl.Visible = lblMachineCap.Visible
        rbProcessCtrlYes.Visible = lblProcessCtrl.Visible
        rbProcessCtrlNo.Visible = lblProcessCtrl.Visible

    End Sub
	
	Sub onLoad_LineHistori()
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_GET_HIST"
        Dim strParam As String
		Dim strValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
		Dim objResult As New Object()
    

        If Trim(LCase(lblHidCostLevel.Text)) = "subblock" Then
		    strParam = "BLK|LOC|YR"
            strValue = strSelectedSubBlkCode & "|" & strlocation & "|" & txtprd.Text
            Try
                intErrNo = ObjOk.mtdGetDataCommon(strOpCd,strParam,strValue,objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_GET_HIST&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
			
            dghist.DataSource = objResult.Tables(0)
            dghist.DataBind()

           
        End If
    End Sub
	
	Sub Bindhistblok(ByVal index As Integer)
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim ParamName As String = ""
        Dim ParamValue As String = ""
        Dim objDs As New Object()
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intselectIndex As Integer = 0

        Dim dr As DataRow

        ParamName = "STRSEARCH"
        ParamValue = "AND Sub.LocCode='" & strLocation & "' AND Sub.Status='1' ORDER By SubBlkCode"

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOpCd, ParamName, ParamValue, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
		
        dr = objDs.Tables(0).NewRow()
        dr("SubBlkCode") = ""
        dr("Description") = "Select BloK"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

		id7ddl = dghist.Items.Item(index).FindControl("id7ddl")
        id7ddl.DataSource = objDs.Tables(0)
        id7ddl.DataTextField = "Description"
        id7ddl.DataValueField = "SubBlkCode"
        id7ddl.DataBind()
     End Sub
	

	
	 Sub dghist_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim Updbutton As LinkButton
        Dim lblTemp As Label
        Dim ddlTemp As DropDownList

        dghist.EditItemIndex = CInt(E.Item.ItemIndex)

        onLoad_LineHistori()
        If CInt(E.Item.ItemIndex) >= dghist.Items.Count Then
            dghist.EditItemIndex = -1
            Exit Sub
        End If

		Bindhistblok(dghist.EditItemIndex)
    	
		lblTemp = dghist.Items.Item(CInt(E.Item.ItemIndex)).FindControl("id7")
        ddlTemp = dghist.Items.Item(CInt(E.Item.ItemIndex)).FindControl("id7ddl")
        If Not (lblTemp Is Nothing) Then
            ddlTemp.SelectedValue = lblTemp.Text
        End If
		
      
        
      
    End Sub

    Sub dghist_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dghist.EditItemIndex = -1
        onLoad_LineHistori()
    End Sub

    Sub dghist_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd As String = "GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_UPD_HIST"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer

        Dim EditText As TextBox
        Dim list As DropDownList

        Dim strBLK As String
        Dim strMN As String
        Dim strYR As String
        Dim strTA As String
        Dim strST As String
        Dim strTS As String
		Dim strTSb As String
		Dim strTB As String
		
		Dim strBJR As String
		Dim strBSS As String
		Dim strOB As String
		Dim strOB2 As String
		
		Dim strP1K As String
		Dim strP1R As String
		Dim strP2K As String
		Dim strP2R As String
		Dim strPRP As String
		
		Dim strANT1 As String
		Dim strANT2 As String
		Dim strANT3 As String
		Dim strANT4 As String
		Dim strANT5 As String
		
		Dim strsty As String
		Dim strBSSHA As String
		
		EditText = E.Item.FindControl("id1")
		strBLK = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id2")
		strMN= EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id3")
		strYR = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id4")
		strTA = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id5")
		strTS = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id5b")
		strTSb = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id6")
		strST = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id11")
		strBJR = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id12")
		strBSS = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id13")
		strOB = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id14")
		strOB2 = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id15")
		strP1K = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id16")
		strP1R = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id17")
		strP2K = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id18")
		strP2R = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("id19")
		strPRP = EditText.Text.Trim()
		
		EditText = E.Item.FindControl("idT1")
		strANT1 = EditText.Text.Trim()
		EditText = E.Item.FindControl("idT2")
		strANT2 = EditText.Text.Trim()
		EditText = E.Item.FindControl("idT3")
		strANT3 = EditText.Text.Trim()
		EditText = E.Item.FindControl("idT4")
		strANT4 = EditText.Text.Trim()
		EditText = E.Item.FindControl("idT5")
		strANT5 = EditText.Text.Trim()
		
		list = E.Item.FindControl("ddlapl")
		strsty = list.selecteditem.value.trim() 
		
		list = E.Item.FindControl("id7ddl")
		strTB = list.selecteditem.value.trim() 
		
		EditText = E.Item.FindControl("id20")
		strBSSHA = EditText.Text.Trim()
		
        ParamNama = "TA|ST|TS|TB|BLK|MN|YR|LOC|UI|BJR|BSS|OB|OB2|P1K|P1R|P2K|P2R|PRP|ANT1|ANT2|ANT3|ANT4|ANT5|STY|PS|BSSHA"
        ParamValue = strTA & "|" & strST & "|" & strTS & "|" & _
                     strTB & "|" & strBLK & "|" & strMN & "|" & _
					 strYR & "|" & strlocation & "|" & strUserId & "|" & _
					 iif(strBJR="","0",strbjr) & "|" & iif(strBSS="","0",strBSS) & "|" & iif(strOB="","0",strOB) & "|" & iif(strOB2="","0",strOB2) & "|" & _
					 iif(strP1K="","0",strP1K) & "|" & iif(strP1R="","0",strP1R) & "|" & iif(strP2K="","0",strP2K) & "|" & iif(strP2R="","0",strP2R) & "|" & _
					 iif(strPRP="","0",strPRP) & "|" & _
					 iif(strANT1="","0",strANT1) & "|" & iif(strANT2="","0",strANT2) & "|" & iif(strANT3="","0",strANT3) & "|" & iif(strANT4="","0",strANT4) & "|" & iif(strANT5="","0",strANT5) & "|" & _
					 strsty & "|" & _
					 iif(strTSb="","0",strTSb) & "|" & _ 
					 iif(strBSSHA="","0",strBSSHA) 

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOppCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_UPD_HIST&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dghist.EditItemIndex = -1
        onLoad_LineHistori()
    End Sub
	
	Sub SrcBtnKlik(ByVal Sender As Object, ByVal E As EventArgs)
		dghist.EditItemIndex = -1
		onLoad_LineHistori()
	End Sub
	
	Sub CopyBtnKlik (ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOppCd As String = "GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_GEN_HIST"
        Dim ParamNama As String
        Dim ParamValue As String
        Dim intErrNo As Integer
		
		if  txtprd2.Text.Trim() = "" 
			lblErrMessage.text = "Silakan isi periode sumber blok (copy dari periode)"
			lblErrMessage.visible = true
		end if
		
		if  txtprd.Text.Trim() = "" 
			lblErrMessage.text = "Silakan isi periode tujuan blok (search)"
			lblErrMessage.visible = true
		end if
		
		ParamNama = "YRCPY|YR|LOC|BLK|UI"
        ParamValue = txtprd2.Text.Trim() & "|" & txtprd.Text.Trim() & "|" & strlocation & "|" & _
                     txtSubBlkCode.text.trim() & "|" & strUserId

        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOppCd, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBBLOCK_BY_SUBBLOCK_GEN_HIST&errmesg=" & Exp.Message & "&redirect=")
        End Try
	
		dghist.EditItemIndex = -1
		onLoad_LineHistori()
    End Sub

End Class
