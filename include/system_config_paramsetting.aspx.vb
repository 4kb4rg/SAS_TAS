Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.PWSystem.clsConfig
Imports agri.PWSystem.clsLangCap



Public Class system_config_paramsetting : Inherits Page

    Protected WithEvents lblCentreControl As Label
    Protected WithEvents rbAutoEmp As RadioButton
    Protected WithEvents rbManualEmp As RadioButton
    Protected WithEvents rbBlock As RadioButton
    Protected WithEvents rbSubBlock As RadioButton
    Protected WithEvents rbBlock_Yield As RadioButton
    Protected WithEvents rbSubBlock_Yield As RadioButton
    Protected WithEvents rbVeh_MTD As RadioButton
    Protected WithEvents rbVeh_YTD As RadioButton
    Protected WithEvents rbVeh_12 As RadioButton
    Protected WithEvents rbVeh_Veh As RadioButton
    Protected WithEvents rbVeh_VehType As RadioButton
    Protected WithEvents cbAutoIncentive As CheckBox
    Protected WithEvents cbAutoLabourOverheadDist As CheckBox
    Protected WithEvents cbAutoYieldRate As CheckBox
    Protected WithEvents cbAutoResetPLAcc As CheckBox
    Protected WithEvents cbAutoResetBSAcc As CheckBox
    Protected WithEvents cbAutoAccRetainEarn As CheckBox
    Protected WithEvents rbGCDist_No As RadioButton
    Protected WithEvents rbGCDist_MthEnd As RadioButton
    Protected WithEvents rbGCDist_PreMth As RadioButton
    Protected WithEvents rb1Stage As RadioButton
    Protected WithEvents rb2Stage As RadioButton
    Protected WithEvents cbChargingToBlock As CheckBox
    Protected WithEvents lblVeh1 As Label
    Protected WithEvents lblVeh2 As Label
    Protected WithEvents lblBlkTag1 As Label
    Protected WithEvents lblBlkTag2 As Label
    Protected WithEvents lblSubBlkTag1 As Label
    Protected WithEvents lblSubBlkTag2 As Label
    Protected WithEvents lblChooseActual As Label
    Protected WithEvents rbCLBlock As RadioButton
    Protected WithEvents rbCLSubBlock As RadioButton
    Protected WithEvents cbInterEstateCharging As CheckBox
    Protected WithEvents cb5Distribution As CheckBox
    Protected WithEvents cbProportionGC As CheckBox
    Protected WithEvents cbAdjAccPeriod As CheckBox    
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents rbIntLabCharge_Actual As RadioButton
    Protected WithEvents rbIntLabCharge_Budget As RadioButton
    Protected WithEvents rbStfLabCharge_Actual As RadioButton
    Protected WithEvents rbStfLabCharge_Budget As RadioButton
    Protected WithEvents rbExtPtyLabCharge_Actual As RadioButton
    Protected WithEvents rbExtPtyLabCharge_Budget As RadioButton
    Protected WithEvents cbUseCtrlAcct As CheckBox
    Protected WithEvents txtWSCostDistMth As TextBox
    

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Data.DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")
		
	        strLocType = Session("SS_LOCTYPE")
	    
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADSystemConfig), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If Not IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        Dim strBlockTag As String
        Dim strSubBlockTag As String
        Dim strVehTypeTag As String

        GetEntireLangCap()
        lblVeh1.text = GetCaption(objLangCap.EnumLangCap.Vehicle)
        strBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        strSubBlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
        strVehTypeTag = GetCaption(objLangCap.EnumLangCap.VehType)
        lblVeh2.text = lblVeh1.text
        rbBlock.text = strBlockTag
        rbSubBlock.text = strSubBlockTag
        rbBlock_Yield.text = strBlockTag
        rbSubBlock_Yield.text = strSubBlockTag
        rbVeh_Veh.text = lblVeh1.text
        rbVeh_VehType.text = strVehTypeTag
        lblBlkTag1.Text = strBlockTag
        lblBlkTag2.Text = strBlockTag
        lblSubBlkTag1.Text = strSubBlockTag
        lblSubBlkTag2.Text = strSubBlockTag
        rbCLBlock.text = strBlockTag
        rbCLSubBlock.text = strSubBlockTag
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
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSPARAM_CONFIGSETTING_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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

    Sub Initialize ()
        rbAutoEmp.Checked = False
        rbManualEmp.Checked = False
        rbBlock.Checked  = False
        rbSubBlock.Checked = False
        rbBlock_Yield.Checked = False
        rbSubBlock_Yield.Checked = False
        rbVeh_MTD.Checked = False 
        rbVeh_YTD.Checked = False
        rbVeh_12.Checked = False
        rbVeh_Veh.Checked = False
        rbVeh_VehType.Checked = False
        cbAutoIncentive.Checked = False
        cbAutoLabourOverheadDist.Checked = False
        cbAutoYieldRate.Checked = False
        cbAutoResetPLAcc.Checked = False
        cbAutoResetBSAcc.Checked = False
        cbAutoAccRetainEarn.Checked = False 
        rbGCDist_No.Checked = False
        rbGCDist_MthEnd.Checked = False
        rbGCDist_PreMth.Checked = False
        rb1Stage.Checked = False
        rb2Stage.Checked = False
        cbChargingToBlock.Checked = False
        rbCLBlock.Checked = False
        rbCLSubBlock.Checked = False
        cbInterEstateCharging.Checked = False
        cb5Distribution.Checked = False
        cbProportionGC.Checked = False
        cbAdjAccPeriod.Checked = False
        rbIntLabCharge_Actual.Checked = False
        rbIntLabCharge_Budget.Checked = False
        rbStfLabCharge_Actual.Checked = False
        rbStfLabCharge_Budget.Checked = False
        rbExtPtyLabCharge_Actual.Checked= False
        rbExtPtyLabCharge_Budget.Checked = False
        cbUseCtrlAcct.Checked = False
    End Sub

    Sub onLoad_Display()
        Dim objConfigDS As New Dataset()
        Dim intErrNo As Integer
        Dim intConfigSetting As Integer
        Dim strOpCode_Config As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim strParam As String = ""

        Initialize()

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCode_Config, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                objConfigDS)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSPARAM_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intConfigSetting = objConfigDS.Tables(0).Rows(0).Item("ConfigSetting")
        

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl), intConfigSetting) Then
            lblCentreControl.Text = objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl)
        Else
            lblCentreControl.Text = 0
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            rbBlock.Checked = True
        Else
            rbSubBlock.Checked = True
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfigSetting) = True Then
            rbBlock_Yield.Checked = True
        Else
            rbSubBlock_Yield.Checked = True
        End If


       If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoEmpCode), intConfigSetting) = True Then
             rbManualEmp.Checked = False 
             rbAutoEmp.Checked = True   
        Else
             rbManualEmp.Checked = True 
             rbAutoEmp.Checked = False      
        End If


        Select Case Convert.ToInt16(objConfigDS.Tables(0).Rows(0).Item("VehDistMethod"))
            Case objSysCfg.EnumVehDistMethod.ByMTD
                    rbVeh_MTD.Checked = True
            Case objSysCfg.EnumVehDistMethod.ByYTD
                    rbVeh_YTD.Checked = True
            Case objSysCfg.EnumVehDistMethod.ByLast12Month
                    rbVeh_12.Checked = True
        End Select

        Select Case Convert.ToInt16(objConfigDS.Tables(0).Rows(0).Item("VehDistUse"))
            Case objSysCfg.EnumVehDistMethod.UsingVehicle
                    rbVeh_Veh.Checked = True
            Case Else
                    rbVeh_VehType.Checked = True
        End Select

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoIncentive), intConfigSetting) = True Then
            cbAutoIncentive.Checked = True
        Else
            cbAutoIncentive.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoLabourOverheadDist), intConfigSetting) = True Then
            cbAutoLabourOverheadDist.Checked = True
        Else
            cbAutoLabourOverheadDist.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoEstateYieldRate), intConfigSetting) = True Then
            cbAutoYieldRate.Checked = True
        Else
            cbAutoYieldRate.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoResetPLAcc), intConfigSetting) = True Then
            cbAutoResetPLAcc.Checked = True
        Else
            cbAutoResetPLAcc.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoResetBSAcc), intConfigSetting) = True Then
            cbAutoResetBSAcc.Checked = True
        Else
            cbAutoResetBSAcc.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoAccRetainEarning), intConfigSetting) = True Then
            cbAutoAccRetainEarn.Checked = True
        Else
            cbAutoAccRetainEarn.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.NoGCDistribute), intConfigSetting) = True Then
            rbGCDist_No.Checked = True

            cbProportionGC.Enabled = False
        Else
            rbGCDist_No.Checked = False

            cbProportionGC.Enabled = True
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.GCDistributionByMthEnd), intConfigSetting) = True Then
            rbGCDist_MthEnd.Checked = True
        Else
            rbGCDist_MthEnd.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.GCDistributeByPreceedMth), intConfigSetting) = True Then
            rbGCDist_PreMth.Checked = True
        Else
            rbGCDist_PreMth.Checked = False
        End If

        If (rbGCDist_No.Checked = False) And (objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.ProportionGCDistribute), intConfigSetting) = True) Then
            cbProportionGC.Checked = True
        Else
            cbProportionGC.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.OneStageNursery), intConfigSetting) = True Then
            rb1Stage.Checked = True
        Else
            rb2Stage.Checked = True
        End If


        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.ChargingToBlock), intConfigSetting) = True Then
            cbChargingToBlock.Checked = True
        Else
            cbChargingToBlock.Checked = False
        End If
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.DefaultChargeToBlock), intConfigSetting) = True Then
            rbCLBlock.Checked = True
            rbCLSubBlock.Checked = False
        Else
            rbCLBlock.Checked = False
            rbCLSubBlock.Checked = True
        End If
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.InterEstateCharging), intConfigSetting) = True Then
            cbInterEstateCharging.Checked = True
        Else
            cbInterEstateCharging.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.WAccDistribution5), intConfigSetting) = True Then
            cb5Distribution.Checked = True
        Else
            cb5Distribution.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseAdjPeriod), intConfigSetting) = True Then
            cbAdjAccPeriod.Checked = True
        Else
            cbAdjAccPeriod.Checked = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.InternalLabourCharge), intConfigSetting) = True Then
            rbIntLabCharge_Actual.Checked = True
            rbIntLabCharge_Budget.Checked = False
        Else
            rbIntLabCharge_Actual.Checked = False
            rbIntLabCharge_Budget.Checked = True
        End If
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.StaffLabourCharge), intConfigSetting) = True Then
            rbStfLabCharge_Actual.Checked = True
            rbStfLabCharge_Budget.Checked = False
        Else
            rbStfLabCharge_Actual.Checked = False
            rbStfLabCharge_Budget.Checked = True
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.ExternalPartyLabourCharge), intConfigSetting) = True Then
            rbExtPtyLabCharge_Actual.Checked = True
            rbExtPtyLabCharge_Budget.Checked = False
        Else
            rbExtPtyLabCharge_Actual.Checked = False
            rbExtPtyLabCharge_Budget.Checked = True
        End If

        txtWSCostDistMth.Text = objConfigDS.Tables(0).Rows(0).Item("WSDistCost")
        If Trim(txtWSCostDistMth.Text) = "" Then
            txtWSCostDistMth.Text = "0"        
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseControlAccForWorkExp), intConfigSetting) = True Then
            cbUseCtrlAcct.Checked = True
        Else
            cbUseCtrlAcct.Checked = False            
        End If

        lblChooseActual.Visible = False

        Switch_Control(objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl), intConfigSetting))

        objConfigDS = Nothing
    End Sub


    Sub SaveBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim intConfigSetting As Integer = lblCentreControl.Text
        Dim strOpCode_UpdCfg As String = "PWSYSTEM_CLSCONFIG_PARAM_UPD"
        Dim strParam As String
        Dim intVehDist As Integer = 0
        Dim intVehDistUse As Integer = 0
        Dim stWorkshopDistCode As String = ""
            
        If rbBlock.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel)
        End If

        If rbBlock_Yield.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel)
        End If

        If rbAutoEmp.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoEmpCode)
        End If

        If cbAutoIncentive.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoIncentive)
        End If

        If cbAutoLabourOverheadDist.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoLabourOverheadDist)
        End If

        If cbAutoYieldRate.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoEstateYieldRate)
        End If

        If cbAutoResetPLAcc.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoResetPLAcc)
        End If

        If cbAutoResetBSAcc.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoResetBSAcc)
        End If

        If cbAutoAccRetainEarn.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoAccRetainEarning)
        End If

        If rbGCDist_No.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.NoGCDistribute)
        End If

        If rbGCDist_MthEnd.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.GCDistributionByMthEnd)
        End If

        If rbGCDist_PreMth.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.GCDistributeByPreceedMth)
        End If

        If rb1Stage.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.OneStageNursery)
        End If

        If cbChargingToBlock.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.ChargingToBlock)
        End If
        
        If rbCLBlock.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.DefaultChargeToBlock)
        End If
        
        If cbInterEstateCharging.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.InterEstateCharging)
        End If

        If cb5Distribution.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.WAccDistribution5)
        End If

        If cbAdjAccPeriod.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseAdjPeriod)
        End If

        If cbProportionGC.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.ProportionGCDistribute)
        End If

        If rbVeh_MTD.Checked Then
            intVehDist = objSysCfg.EnumVehDistMethod.ByMTD
        ElseIf rbVeh_YTD.Checked Then
            intVehDist = objSysCfg.EnumVehDistMethod.ByYTD
        Else
            intVehDist = objSysCfg.EnumVehDistMethod.ByLast12Month
        End If

        If rbVeh_Veh.Checked Then
            intVehDistUse = objSysCfg.EnumVehDistMethod.UsingVehicle
        Else
            intVehDistUse = objSysCfg.EnumVehDistMethod.UsingVehicleType
        End If

         If rbIntLabCharge_Actual.Checked = False And _ 
            rbStfLabCharge_Actual.Checked = False And _
            rbExtPtyLabCharge_Actual.Checked = False Then
            lblChooseActual.Visible = True
            Exit Sub
         End If

        If rbIntLabCharge_Actual.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.InternalLabourCharge)
        End If    
        
        If rbStfLabCharge_Actual.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.StaffLabourCharge)
        End If 

        If rbExtPtyLabCharge_Actual.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.ExternalPartyLabourCharge)
        End If

        stWorkshopDistCode = CSTR(Trim(txtWSCostDistMth.Text))
        
        If cbUseCtrlAcct.Checked Then
            intConfigSetting += objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseControlAccForWorkExp)
        End If
        


        strParam = "|||" & intConfigSetting & "|" & intVehDist & "|" & intVehDistUse & "||||||||" & stWorkshopDistCode

        Try
            intErrNo = objSysCfg.mtdUpdConfigInfo("", _
                                                strOpCode_UpdCfg, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSPARAM_UPD_CONFIG&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        onLoad_Display()
    End Sub

    Sub Switch_Control(ByVal pv_blnIsCentre As Integer)
        Dim objAccDs As New Dataset()
        Dim objBlkDs As New Dataset()
        Dim strOpCd_Acc As String = "PWSYSTEM_CLSCONFIG_ACCOUNT_COUNT_GET"
        Dim strOpCd_Blk As String = "PWSYSTEM_CLSCONFIG_BLOCK_COUNT_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCd_Acc, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSPARAM_ACCOUNT_COUNT_GET&errmesg=" & Exp.ToString & "&redirect=system/config/syssetting.aspx")
        End Try

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCd_Blk, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSPARAM_ACCOUNT_COUNT_GET&errmesg=" & Exp.ToString & "&redirect=system/config/syssetting.aspx")
        End Try

        If pv_blnIsCentre = True
            SaveBtn.Visible = True
            rbAutoEmp.Enabled = True
            rbManualEmp.Enabled = True
            rbBlock.Enabled = True
            rbSubBlock.Enabled = True
            rbBlock_Yield.Enabled = True
            rbSubBlock_Yield.Enabled = True
            rbVeh_MTD.Enabled = True
            rbVeh_YTD.Enabled = True
            rbVeh_12.Enabled = True
            rbVeh_Veh.Enabled = True
            rbVeh_VehType.Enabled = True
            cbAutoIncentive.Enabled = True
            cbAutoLabourOverheadDist.Enabled = True
            cbAutoYieldRate.Enabled = True
            cbAutoResetPLAcc.Enabled = True
            cbAutoResetBSAcc.Enabled = True
            cbAutoAccRetainEarn.Enabled = True
            rbGCDist_No.Enabled = True
            rbGCDist_MthEnd.Enabled = True
            rbGCDist_PreMth.Enabled = True
            rb1Stage.Enabled = True
            rb2Stage.Enabled = True
            rbIntLabCharge_Actual.Enabled = True
            rbIntLabCharge_Budget.Enabled = True
            rbStfLabCharge_Actual.Enabled = True
            rbStfLabCharge_Budget.Enabled = True
            rbExtPtyLabCharge_Actual.Enabled = True
            rbExtPtyLabCharge_Budget.Enabled = True
            txtWSCostDistMth.Enabled = True            
            cbUseCtrlAcct.Enabled = True
            cbInterEstateCharging.Enabled = True
            cb5Distribution.enabled=true
            cbAdjAccPeriod.Enabled = True

            If objAccDs.Tables(0).Rows(0).Item("RecordCount") > 0 Then
                rbAutoEmp.Enabled = False
                rbManualEmp.Enabled = False
            Else
                rbAutoEmp.Enabled = True
                rbManualEmp.Enabled = True
            End If

            If objBlkDs.Tables(0).Rows(0).Item("RecordCount") > 0 Then
                rbBlock.Enabled = False
                rbSubBlock.Enabled = False
                rbBlock_Yield.Enabled = False
                rbSubBlock_Yield.Enabled = False
                rbVeh_MTD.Enabled = False
                rbVeh_YTD.Enabled = False
                rbVeh_12.Enabled = False
                rbVeh_Veh.Enabled = False
                rbVeh_VehType.Enabled = False
                cbAutoIncentive.Enabled = False
                cbAutoLabourOverheadDist.Enabled = False
                cbAutoYieldRate.Enabled = False
                cbAutoResetPLAcc.Enabled = False
                cbAutoResetBSAcc.Enabled = False
                cbAutoAccRetainEarn.Enabled = False
                rbGCDist_No.Enabled = False
                rbGCDist_MthEnd.Enabled = False
                rbGCDist_PreMth.Enabled = False
                cbProportionGC.Enabled = False
                rb1Stage.Enabled = False
                rb2Stage.Enabled = False
                rbIntLabCharge_Actual.Enabled = False
                rbIntLabCharge_Budget.Enabled = False
                rbStfLabCharge_Actual.Enabled = False
                rbStfLabCharge_Budget.Enabled = False
                rbExtPtyLabCharge_Actual.Enabled = False
                rbExtPtyLabCharge_Budget.Enabled = False
                txtWSCostDistMth.Enabled = False            
                cbUseCtrlAcct.Enabled = False
                cbInterEstateCharging.Enabled = False
                cb5Distribution.enabled=false
                cbAdjAccPeriod.Enabled = False
            Else
                rbBlock.Enabled = True
                rbSubBlock.Enabled = True
                rbBlock_Yield.Enabled = True
                rbSubBlock_Yield.Enabled = True
                rbVeh_MTD.Enabled = True
                rbVeh_YTD.Enabled = True
                rbVeh_12.Enabled = True
                rbVeh_Veh.Enabled = True
                rbVeh_VehType.Enabled = True
                cbAutoIncentive.Enabled = True
                cbAutoLabourOverheadDist.Enabled = True
                cbAutoYieldRate.Enabled = True
                cbAutoResetPLAcc.Enabled = True
                cbAutoResetBSAcc.Enabled = True
                cbAutoAccRetainEarn.Enabled = True
                rbGCDist_No.Enabled = True
                rbGCDist_MthEnd.Enabled = True
                rbGCDist_PreMth.Enabled = True
                rb1Stage.Enabled = True
                rb2Stage.Enabled = True
                rbIntLabCharge_Actual.Enabled = True
                rbIntLabCharge_Budget.Enabled = True
                rbStfLabCharge_Actual.Enabled = True
                rbStfLabCharge_Budget.Enabled = True
                rbExtPtyLabCharge_Actual.Enabled = True
                rbExtPtyLabCharge_Budget.Enabled = True
                txtWSCostDistMth.Enabled = True            
                cbUseCtrlAcct.Enabled = True
                cbInterEstateCharging.Enabled = True
                cb5Distribution.enabled=true
                cbAdjAccPeriod.Enabled = True
            End If

            If (rbBlock.Checked = True And rbBlock_Yield.Checked = True) Or _
                (rbSubBlock.Checked = True And rbSubBlock_Yield.Checked = True) Then
            Else
                cbAutoYieldRate.Enabled = False
                cbAutoYieldRate.Checked = False
            End If
        Else    
            SaveBtn.Visible = False        
            rbAutoEmp.Enabled = False
            rbManualEmp.Enabled = False
            rbBlock.Enabled = False
            rbSubBlock.Enabled = False
            rbBlock_Yield.Enabled = False
            rbSubBlock_Yield.Enabled = False
            rbVeh_MTD.Enabled = False
            rbVeh_YTD.Enabled = False
            rbVeh_12.Enabled = False
            rbVeh_Veh.Enabled = False
            rbVeh_VehType.Enabled = False
            cbAutoIncentive.Enabled = False
            cbAutoLabourOverheadDist.Enabled = False
            cbAutoYieldRate.Enabled = False
            cbAutoResetPLAcc.Enabled = False
            cbAutoResetBSAcc.Enabled = False
            cbAutoAccRetainEarn.Enabled = False
            rbGCDist_No.Enabled = False
            rbGCDist_MthEnd.Enabled = False
            rbGCDist_PreMth.Enabled = False
            cbProportionGC.Enabled = False
            rb1Stage.Enabled = False
            rb2Stage.Enabled = False
            rbIntLabCharge_Actual.Enabled = False
            rbIntLabCharge_Budget.Enabled = False
            rbStfLabCharge_Actual.Enabled = False
            rbStfLabCharge_Budget.Enabled = False
            rbExtPtyLabCharge_Actual.Enabled = False
            rbExtPtyLabCharge_Budget.Enabled = False
            txtWSCostDistMth.Enabled = False            
            cbUseCtrlAcct.Enabled = False
            cbInterEstateCharging.Enabled = False
            cb5Distribution.enabled=false
            cbAdjAccPeriod.Enabled = False
        End If

        If rbSubBlock.Enabled = True Then       
            cbChargingToBlock.Enabled = True
            If cbChargingToBlock.Checked = True Then
                rbCLBlock.Enabled = True
                rbCLSubBlock.Enabled = True
            Else
                rbCLBlock.Enabled = False
                rbCLSubBlock.Enabled = False
            End If
        Else
            cbChargingToBlock.Enabled = False
            rbCLBlock.Enabled = False
            rbCLSubBlock.Enabled = False
        End If
    End Sub

    Sub OnIndexChange_GCDist(Sender As Object, E As EventArgs)
        If rbGCDist_No.Checked = True Then
            cbProportionGC.Enabled = False
            cbProportionGC.Checked = False
        Else
            cbProportionGC.Enabled = True
        End If

        Switch_Control(objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl), lblCentreControl.Text))
    End Sub
    
    Sub OnIndexChanged_ResetChargingLevel(Sender As Object, E As EventArgs)
        If rbSubBlock.Enabled = True And rbSubBlock.Checked = True Then
            rbSubBlock_Yield.Checked = True
            cbChargingToBlock.Enabled = True
        Else
            rbBlock_Yield.Checked = True
            cbChargingToBlock.Enabled = False
            If rbSubBlock.Checked = False Then
                cbChargingToBlock.Checked = False
            End If
        End If
        Call OnIndexChanged_ResetChargingLevelDefault(Sender, E)
        Call OnIndexChanged_AnalysisForProd(Sender, E)
    End Sub
    
    Sub OnIndexChanged_ResetChargingLevelDefault(Sender As Object, E As EventArgs)
        If cbChargingToBlock.Enabled = True And cbChargingToBlock.Checked = True Then
            rbCLBlock.Enabled = True
            rbCLSubBlock.Enabled = True
        Else
            rbCLBlock.Enabled = False
            rbCLSubBlock.Enabled = False
            rbCLBlock.Checked = rbBlock.Checked
            rbCLSubBlock.Checked = rbSubBlock.Checked
        End If
    End Sub

    Sub OnIndexChanged_AnalysisForProd(Sender As Object, E As EventArgs)
        If (rbBlock.Enabled = True And rbBlock_Yield.Enabled = True And rbSubBlock.Enabled = True And rbSubBlock_Yield.Enabled = True) Then
            If (rbBlock.Checked = True And rbSubBlock_Yield.Checked = True) Then
                rbBlock_Yield.Checked = True
            End If

            If (rbBlock.Checked = True And rbBlock_Yield.Checked = True) Or _
                (rbSubBlock.Checked = True And rbSubBlock_Yield.Checked = True) Then
                cbAutoYieldRate.Enabled = True
            Else
                cbAutoYieldRate.Enabled = False
                cbAutoYieldRate.Checked = False
            End If
        End If
    End Sub
  


End Class
