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
Imports agri.PWSystem
Imports agri.GL
Imports agri.PD
Imports agri.GlobalHdl

Public Class PD_trx_EstProdDet : Inherits Page

    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents ddlBlkCode As DropDownList
    Protected WithEvents txtDate As Textbox
    Protected WithEvents txtRefNo As Textbox
    Protected WithEvents txtHarvRate As Textbox
    Protected WithEvents txtRoundNo As Textbox
    Protected WithEvents txtTotalBunch As Textbox
    Protected WithEvents EstateYieldLNID As Textbox

    Protected WithEvents txtBunchesQuota As Textbox
    Protected WithEvents txtBunchesOverQuota As Textbox
    Protected WithEvents txtBunchesDeliverToMill As Textbox
    Protected WithEvents txtEstimatedABW As Textbox
    Protected WithEvents txtActualHA As Textbox
    Protected WithEvents ddlEmployee As DropDownList
    Protected WithEvents txtBunches As Textbox
    Dim dbUnit As Double = 1000

    Protected WithEvents txtWeight As Textbox
    Protected WithEvents txtDedWeight As Textbox
    Protected WithEvents txtabw As Label
    Protected WithEvents txtTotalDed As Textbox

    Protected WithEvents lblEstateYieldLNID As Label
    Protected WithEvents lblExceedBunchDelivered As Label
    Protected WithEvents lblNoHarvester As Label
    Protected WithEvents lblHarvestIntv As Label
    Protected WithEvents lblEstateYieldID As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents trHarvest As HtmlTableRow
    Protected WithEvents eyid As HtmlInputHidden
    Protected WithEvents btnFind1 As HtmlInputButton
    Protected WithEvents btnFind2 As HtmlInputButton
    Protected WithEvents btnFind3 As HtmlInputButton
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrBlk As Label
    Protected WithEvents lblErrAcc As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDate As Label
    Protected WithEvents lblErrDateMsg As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblAverageBacklog As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblErrEmployee As Label
    Protected WithEvents lblRoundPeriod As Label
    Protected WithEvents lblTotHarvester As Label
    Protected WithEvents lblHaMdAboveQuota As Label
    Protected WithEvents lblHaMdLessQuota As Label
    Protected WithEvents lblHaMdTotal As Label
    Protected WithEvents EventData as DataGrid

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objPMTrx As New agri.PM.clsSetup()
    Dim objHRTrx As New agri.HR.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim objEstProdDs As New Object()
    Dim objHAIntervalDs As New Object()
    Dim objBlkDs As New Object()    
    Dim objLangCapDs As New Object()
    Dim objDataSet As New DataSet() 

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFormat As String
    Dim intPDAR As Integer
    Dim intConfig As Integer
    Dim blnAutoIncentive As Boolean = False

    Dim strSelectedEstYldId As String = ""
    Dim intStatus As Integer
    
    Dim strOppCd_GET As string = "PD_CLSTRX_ESTATEYIELDLN_GET"
    Dim strOppCd_ADD  As string = "PD_CLSTRX_ESTATEYIELDLN_ADD"
    Dim strOppCd_UPD As string = "PD_CLSTRX_ESTATEYIELDLN_UPD"
    Dim strOppCd_SUM As string = "PD_CLSTRX_ESTATEYIELDLN_SUM"
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	

    Dim strOppCdMill_GET As String = "PM_CLSSETUP_MILL_LIST_GET"

    Protected WithEvents lblMillCode as Label
    Protected WithEvents ddlMillCode As DropDownList
    Protected WithEvents lblErrMillCode as Label
    Protected objPMSetup As New agri.PM.clsSetup()
    Dim strSelectedMillCode As String = ""    

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
        intPDAR = Session("SS_PDAR")
        intConfig = Session("SS_CONFIGSETTING")
	    
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrAcc.Visible = False
            lblErrBlk.Visible = False
            lblErrEmployee.Visible = False
            lblExceedBunchDelivered.Visible = False
            
            lblErrDate.Text = ""
            strSelectedEstYldId = Trim(IIf(Request.QueryString("eyid") <> "", Request.QueryString("eyid"), Request.Form("eyid")))
            intStatus = CInt(lblHiddenSts.Text)
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.AutoIncentive), intConfig) = True Then
                blnAutoIncentive = True
            End If
            If SortExpression.Text = "" Then
                SortExpression.Text = "Mill"
                sortcol.Text = "ASC"
            End If
            If Not IsPostBack Then

                If strSelectedEstYldId <> "" Then
                    eyid.Value = strSelectedEstYldId
                    onLoad_Display()
                Else
                    txtDate.Text = objGlobal.GetShortDate(strDateFormat, Now())
                    BindAccount("")
                    BindBlock("", "")
                    BindEmployee("")
                    onLoad_BindButton()
                End If
                BindGrid()      
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig) = True Then
                lblBlock.text = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlock.text = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPRODDET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=PD/trx/PD_trx_EstProdList.aspx")
        End Try

        lblAccount.text = GetCaption(objLangCap.EnumLangCap.Account)
        lblErrBlk.text = lblErrSelect.text & lblBlock.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPRODDET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PD/trx/PD_trx_EstProdList.aspx")
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


    Sub onLoad_BindButton()
        ddlAccCode.Enabled = False
        ddlBlkCode.Enabled = False
        ddlEmployee.Enabled = False
        txtDate.Enabled = False
        txtRefNo.Enabled = False
        txtHarvRate.Enabled = False
        txtRoundNo.Enabled = False
        txtBunches.Enabled = False
        txtWeight.Enabled =False
        txtDedWeight.Enabled =False
        SaveBtn.Visible = False
        btnNew.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind1.Disabled = False

        Select Case intStatus
            Case objPDTrx.EnumEstateYieldStatus.Active
                ddlAccCode.Enabled = True
                ddlBlkCode.Enabled = True
                ddlEmployee.Enabled = True
                txtDate.Enabled = True
                txtRefNo.Enabled = True
                txtHarvRate.Enabled = True
                txtRoundNo.Enabled = True
                txtBunches.Enabled = True
                txtWeight.Enabled = True
                txtDedWeight.Enabled = True
                SaveBtn.Visible = True
                btnNew.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPDTrx.EnumEstateYieldStatus.Deleted
                UnDelBtn.Visible = True
                btnNew.Visible = True
                btnFind1.Disabled = True
                

            Case objPDTrx.EnumEstateYieldStatus.Closed
                btnNew.Visible = True
                btnFind1.Disabled = True
            Case Else
                ddlAccCode.Enabled = True
                ddlBlkCode.Enabled = True
                ddlEmployee.Enabled = True
                txtDate.Enabled = True
                txtRefNo.Enabled = True
                txtHarvRate.Enabled = True
                txtRoundNo.Enabled = True
                txtBunches.Enabled = True
                txtWeight.Enabled = True
                txtDedWeight.Enabled = True
                SaveBtn.Visible = True
        End Select

        If blnAutoIncentive = True Then
            ddlAccCode.Enabled = True
            ddlEmployee.Enabled = True
            btnFind1.Disabled = True
            btnFind2.Disabled = False
            btnFind3.Disabled = False
        Else
            ddlAccCode.Enabled = False
            ddlEmployee.Enabled = False
            btnFind1.Disabled = False
            btnFind2.Disabled = True
            btnFind3.Disabled = True
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PD_CLSTRX_ESTATEYIELD_GET"
        Dim strParam As String = strSelectedEstYldId        
        Dim intErrNo As Integer

        Try
            intErrNo = objPDTrx.mtdGetEstateYield(strOpCd, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  strAccMonth, _
                                                  strAccYear, _
                                                  strParam, _
                                                  objEstProdDs, _
                                                  True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        lblEstateYieldID.Text = strSelectedEstYldId
        txtDate.Text = objGlobal.GetShortDate(strDateFormat, objEstProdDs.Tables(0).Rows(0).Item("YieldDate"))
        txtRefNo.Text = Trim(objEstProdDs.Tables(0).Rows(0).Item("RefNo"))        
        txtRoundNo.Text = objEstProdDs.Tables(0).Rows(0).Item("RoundNo")
        txtBunches.Text = objEstProdDs.Tables(0).Rows(0).Item("BunchNo") 
        txtHarvRate.Text = objGlobal.DisplayForEditCurrencyFormat(objEstProdDs.Tables(0).Rows(0).Item("HarvestRate"))
        txtabw.Text = objEstProdDs.Tables(0).Rows(0).Item("AveBunchWeight") * dbUnit
        lblNoHarvester.Text = Trim(objEstProdDs.Tables(0).Rows(0).Item("HarvestManDay"))
        txtBunchesQuota.Text = objEstProdDs.Tables(0).Rows(0).Item("BunchesQuota")
        txtBunchesOverQuota.Text = objEstProdDs.Tables(0).Rows(0).Item("BunchesOverQuota")
        txtBunchesDeliverToMill.Text = objEstProdDs.Tables(0).Rows(0).Item("TotalBunchesDelivered")
        txtEstimatedABW.Text = objEstProdDs.Tables(0).Rows(0).Item("EstimatedABW")
        txtActualHA.text = objEstProdDs.Tables(0).Rows(0).Item("ActualHarvestedArea")
        
        IF objEstProdDs.Tables(0).Rows(0).Item("RoundPeriod") > 0 Then
            lblRoundPeriod.Text =  objEstProdDs.Tables(0).Rows(0).Item("RoundPeriod") & IIF (objEstProdDs.Tables(0).Rows(0).Item("RoundPeriod") = 1 , " Day", " Days")
        else
            lblRoundPeriod.Text = 0
        End IF     
        HarvestInterval_Display(objEstProdDs.Tables(0).Rows(0).Item("RoundPeriod"))     
        lblTotHarvester.Text =  objGlobal.DisplayQuantityFormat(objEstProdDs.Tables(0).Rows(0).Item("TotalHarvester"))
        lblHaMdAboveQuota.Text =  objGlobal.DisplayQuantityFormat(objEstProdDs.Tables(0).Rows(0).Item("MandayAboveQuota"))
        lblHaMdLessQuota.Text =  objGlobal.DisplayQuantityFormat(objEstProdDs.Tables(0).Rows(0).Item("MandayLessQuota"))
        lblHaMdTotal.Text =  objGlobal.DisplayQuantityFormat(objEstProdDs.Tables(0).Rows(0).Item("TotalHarvesterManday"))       
        intStatus = CInt(Trim(objEstProdDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objEstProdDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objPDTrx.mtdGetEstateYieldStatus(Trim(objEstProdDs.Tables(0).Rows(0).Item("Status")))
        lblPeriod.Text = Trim(objEstProdDs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objEstProdDs.Tables(0).Rows(0).Item("AccYear"))
        lblDateCreated.Text = objGlobal.GetLongDate(objEstProdDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objEstProdDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objEstProdDs.Tables(0).Rows(0).Item("UserName"))
        lblAverageBacklog.Text = objGlobal.DisplayQuantityFormat(objEstProdDs.Tables(0).Rows(0).Item("AverageBacklog"))
        BindBlock(Trim(objEstProdDs.Tables(0).Rows(0).Item("AccCode")), Trim(objEstProdDs.Tables(0).Rows(0).Item("BlkCode")))

        txtWeight.Text =  objEstProdDs.Tables(0).Rows(0).Item("ActualWeight") * dbUnit
        txtDedWeight.Text = objEstProdDs.Tables(0).Rows(0).Item("DedWeight") * dbUnit

        onLoad_BindButton()
    End Sub
    Sub HarvestInterval_Display(ByVal pv_strRoundPeriod As String)

        Dim strOpCd As String = "PM_CLSSETUP_HAINTERVAL_GET"
        Dim strParam As String = "|" & " WHERE DayFrom <= " & pv_strRoundPeriod & " AND	DayTo >= " & pv_strRoundPeriod
        Dim intErrNo As Integer

        Try
            intErrNo = objPMTrx.mtdGetHaInterval(strOpCd, _
                                                  strParam, _
                                                  objHAIntervalDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        If objHAIntervalDs.Tables(0).Rows.Count > 0 Then
            lblHarvestIntv.Text =  objHAIntervalDs.Tables(0).Rows(0).Item("DayFrom") & " - " & objHAIntervalDs.Tables(0).Rows(0).Item("DayTo") & " Days"
        Else
            lblHarvestIntv.Text =  "0 Day"
        End If

    End Sub
    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim objAccDs As New Dataset()
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & _
                                 objGLSetup.EnumAccountCodeStatus.Active & _
                                 "' And ACC.AccType = '" & objGLSetup.EnumAccountType.ProfitAndLost & _
                                 "' And ACC.AccPurpose = '" & objGLSetup.EnumAccountPurpose.NonVehicle & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objAccDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objAccDs.Tables(0).Rows(intCnt).Item("AccCode")) & " (" & Trim(objAccDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = lblSelect.text & lblAccount.text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccCode.DataSource = objAccDs.Tables(0)
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex
        ddlAccCode.AutoPostBack = True
        objAccDs = Nothing
    End Sub


    Sub onSelect_Account(Sender As Object, E As EventArgs)
        Dim strAcc As String = Request.Form("ddlAccCode")
        Dim strBlk As String = Request.Form("ddlBlkCode")
        BindBlock(strAcc, strBlk)
    End Sub


    Sub BindBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0
        Try
            If blnAutoIncentive = True Then
                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig) = True Then
                    strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                    strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
                Else
                    strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                    strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active
                End If
                intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                        strParam, _
                                                        objBlkDs)
            Else            
                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig) = True Then
                    strOpCd = "GL_CLSTRX_BLOCK_LIST_GET"
                    strParam = "Order By GL.BlkCode ASC|And GL.LocCode = '" & Session("SS_LOCATION") & "' And GL.BlkType = '" & objGLSetup.EnumBlockType.MatureField & "' AND GL.Status = '" & objGLSetup.EnumBlockStatus.Active & "' And GL.TotalArea > 0 "
                    intMasterType = objGLSetup.EnumGLMasterType.Block
                Else
                    strOpCd = "GL_CLSTRX_SUBBLOCK_LIST_GET"
                    strParam = "Order By GL.SubBlkCode ASC|And GL.LocCode = '" & Session("SS_LOCATION") & "' And GL.SubBlkType = '" & objGLSetup.EnumSubBlockType.MatureField & "' AND GL.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' And GL.TotalArea > 0"
                    intMasterType = objGLSetup.EnumGLMasterType.SubBlock
                End If
                intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                    strParam, _
                                                    objGLSetup.EnumGLMasterType.Block, _
                                                    objBlkDs)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(objBlkDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.text & lblBlock.text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlkCode.DataSource = objBlkDs.Tables(0)
        ddlBlkCode.DataValueField = "BlkCode"
        ddlBlkCode.DataTextField = "Description"
        ddlBlkCode.DataBind()
        ddlBlkCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindEmployee(ByVal pv_strEmpId As String)
        Dim objEmpDs As New Dataset()
        Dim strOpCd As String = "HR_CLSSETUP_EMPID_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "|" & Convert.ToString(objHRTrx.EnumEmpStatus.Active) & "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objHRTrx.mtdGetEmpCode(strOpCd, strParam, objEmpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_EMP_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        For intCnt = 0 To objEmpDs.Tables(0).Rows.Count - 1
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode"))
            objEmpDs.Tables(0).Rows(intCnt).Item("EmpName") = Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode")) & " (" & Trim(objEmpDs.Tables(0).Rows(intCnt).Item("EmpName")) & ")"
            If objEmpDs.Tables(0).Rows(intCnt).Item("EmpCode") = Trim(pv_strEmpId) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objEmpDs.Tables(0).NewRow()
        dr("EmpCode") = ""
        dr("EmpName") = "Select one Employee Code"
        objEmpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlEmployee.DataSource = objEmpDs.Tables(0)
        ddlEmployee.DataValueField = "EmpCode"
        ddlEmployee.DataTextField = "EmpName"
        ddlEmployee.DataBind()
        ddlEmployee.SelectedIndex = intSelectedIndex
        objEmpDs = Nothing
    End Sub


    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PD_CLSTRX_ESTATEYIELD_ADD"
        Dim strOpCd_Upd As String = "PD_CLSTRX_ESTATEYIELD_UPD"
        Dim strOpCd_Update As String = "PD_CLSTRX_ROUNDPERIOD_UPD"
        Dim strRPParam As String
        Dim strOpCd_UpdateHATOTAL As String = "PD_CLSTRX_HAVESTER_TOTAL_UPD"
        Dim strOpCd_UpdateHAMANDAY_SP As String = "PD_CLSTRX_HAVESTER_MANDAY_UPD_SP"
        Dim strBlock As String
        Dim strOpCd_Get As String = "PD_CLSTRX_TOTALHADAY_GET"
        Dim strOpCd_Sts As String = "PD_CLSTRX_ESTATEYIELD_STATUS_UPD"
        Dim objEstateYieldId As String
        Dim strAcc As String = "" 'Request.Form("ddlAccCode")
        Dim strBlk As String = Request.Form("ddlBlkCode")
        Dim strEmp As String = "" 'Request.Form("ddlEmployee")
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim dblAmount As Double        
        Dim dblWeight As Double
        Dim dbBacklogBunch As Double
        Dim dbBacklogWeight As Double
        Dim dbProductionWeight As Double
        Dim dbTotalBunch As Double
        Dim backlogEvaluationEnum as String
        Dim dbAverageBacklog As Double
        Dim dbTotalHaDay As Double 
        Dim objTotHaDay as new Dataset
        Dim strTotHaDayParam as string
        Dim dblActualWeight As Double
        Dim dblDedWeight As Double

        If objGlobal.mtdValidInputDate(strDateFormat, _
                                                Trim(txtDate.Text), _
                                                objFormatDate, _
                                                objActualDate) = False Then
            lblErrDate.Visible = True
            lblErrDate.Text = lblErrDateMsg.Text & objFormatDate
            Exit Sub
        else            
            strBlock = trim(ddlBlkCode.SelectedValue)
            strRPParam = strBlock & "|" & _
                        objActualDate & "|" & _                    
                        objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig)  & "|" & _ 
                        Iif(objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig), "PR_LN.BlkCode", "PR_LN.SubBlkCode") & "|" 
        End If

        If strCmdArgs = "Save" Then
            If blnAutoIncentive = True And strAcc = "" Then
                lblErrAcc.Visible = True
                Exit sub
            ElseIf strBlk = "" Then
                lblErrBlk.Visible = True
                Exit Sub
            ElseIf objGlobal.mtdValidInputDate(strDateFormat, _
                                                Trim(txtDate.Text), _
                                                objFormatDate, _
                                                objActualDate) = False Then
                lblErrDate.Visible = True
                lblErrDate.Text = lblErrDateMsg.Text & objFormatDate
                Exit Sub
            ElseIf strEmp = "" And blnAutoIncentive = True Then
                lblErrEmployee.Visible = True
            End If
            dblActualWeight = txtWeight.Text / dbUnit
            dblDedWeight = txtDedWeight.Text / dbUnit
            dblWeight = (txtWeight.Text - txtDedWeight.Text) / dbUnit
            dblAmount = cdbl(dblWeight) * 1
            dbBacklogBunch = 0
            dbBacklogWeight = 0
            dbTotalBunch =  txtBunches.Text
            dbProductionWeight = dblWeight * dbUnit
            backlogEvaluationEnum = objPDTrx.EnumConformanceDay.Conformance

            
            strTotHaDayParam = "|" & "WHERE PR_ATTDTRX.LocCode = '" & strLocation &"' AND (MONTH(PR_ATTDTRX.AttDate) = MONTH('" & objActualDate &"')) " & _
                               " AND (YEAR(PR_ATTDTRX.AttDate) = YEAR('" & objActualDate &"')) AND (PR_ATTDTRX.AttDate <= '" & objActualDate &"') "
 
            Try
                intErrNo = objPDTrx.mtdGetTotalHarvesterDay(strOpCd_Get, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strTotHaDayParam, _
                                                            objTotHaDay)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_SAVE1&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/PD_trx_EstProdDet.aspx")
            End Try
            
            dbTotalHaDay = objTotHaDay.Tables(0).Rows(0).Item("iCount")



            dbAverageBacklog = 0



            strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.EstateProduction) & "|" & _
                        strSelectedEstYldId & "|" & _
                        Trim(Replace(txtRefNo.Text, "'", "''")) & "|" & _
                        strAcc & "|" & _
                        strBlk & "|" & _
                        objActualDate & "|" & _
                        "1" & "|" & _
                        "1" & "|" & _
                        strEmp & "|" & _
                        Trim(txtBunches.Text) & "|" & _
                        dblWeight & "|" & _
                        objGlobal.DBCurrencyFormat(dblAmount) & "|" & _
                        Trim(lblNoHarvester.Text) & "|" & _
                        objPDTrx.EnumEstateYieldStatus.Active & "|" & _
                        Trim(txtBunches.Text) & "|" & _
                        "0" & "|" & _
                        Trim(txtBunches.Text) & "|" & _
                        IIf(txtabw.Text = "", 0, objGlobal.DBDecimalFormat(txtabw.Text))  & "|" & _                        
                        dbTotalBunch & "|" & _                        
                        dbBacklogBunch & "|" & _                        
                        dbProductionWeight & "|" & _
                        dbProductionWeight & "|" & _
                        "0" & "|" & _
                        "0" & "|" & _      
                        objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig) & "|" & _                        
                        backlogEvaluationEnum & "|" & _   
                        dbAverageBacklog & "|" & dblActualWeight & "|" & dblDedWeight

            Try
                intErrNo = objPDTrx.mtdUpdEstateYield(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    False, _
                                                    objEstateYieldId)
            If intErrNo = 0 Then
                
                LoadData()    
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    UpdateEstateYieldLN
                End if
                If Session("SS_COSTLEVEL") <> "block" AND objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfig) then
                    intErrNo = objPDTrx.mtdUpdHarvesterBlock("PD_CLSTRX_HAVESTER_BLOCK_UPD", _
                                                            strLocation, _
                                                            strRPParam)
                End if

                intErrNo = objPDTrx.mtdUpdRoundPeriod(strOpCd_Update, _
                                                    strLocation, _
                                                    strRPParam)
                intErrNo = objPDTrx.mtdUpdHarvesterTotal(strOpCd_UpdateHATOTAL, _
                                                        strLocation, _
                                                        strRPParam)
                intErrNo = objPDTrx.mtdUpdHarvesterManday(strOpCd_UpdateHAMANDAY_SP, _
                                                        strLocation, _
                                                        strRPParam)
            End If
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_SAVE2&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/PD_trx_EstProdDet.aspx")
            End Try
            strSelectedEstYldId = objEstateYieldId
            eyid.Value = strSelectedEstYldId

        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedEstYldId & "|" & objPDTrx.EnumEstateYieldStatus.Deleted
            Try
                intErrNo = objPDTrx.mtdUpdEstateYield("", _
                                                    strOpCd_Sts, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    True, _
                                                    objEstateYieldId)
            If intErrNo = 0 Then
                intErrNo = objPDTrx.mtdUpdRoundPeriod(strOpCd_Update, _
                                                    strLocation, _
                                                    strRPParam)
            End If
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/PD_trx_EstProdDet.aspx?eyid=" & strSelectedEstYldId)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedEstYldId & "|" & objPDTrx.EnumEstateYieldStatus.Active
            Try
                intErrNo = objPDTrx.mtdUpdEstateYield("", _
                                                    strOpCd_Sts, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    True, _
                                                    objEstateYieldId)
            If intErrNo = 0 Then
                intErrNo = objPDTrx.mtdUpdRoundPeriod(strOpCd_Update, _
                                                    strLocation, _
                                                    strRPParam)
            End If
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_ESTPROD_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/PD_trx_EstProdDet.aspx?eyid=" & strSelectedEstYldId)
            End Try
        ElseIf strCmdArgs = "New" Then
            Response.Redirect("PD_trx_EstProdDet.aspx")
        End If

        If strSelectedEstYldId <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PD_trx_EstProdList.aspx")
    End Sub

    Sub BindGrid() 
        Dim dsData As DataSet       
        Dim Updbutton As LinkButton

        dsData = LoadData       
        EventData.DataSource = dsData        
        EventData.DataBind()
 
    End Sub 

    Protected Function LoadData() As DataSet
                
        Dim Desc As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer        
        Dim intErrNo As integer


        SearchStr = " EstateYieldID ='" & strSelectedEstYldId & "' AND LocCode='" & strLocation & "'"        

        sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objPDTrx.mtdGetEstateYieldLN(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_MACHINE&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineMaster.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            strSelectedMillCode = Trim(objDataSet.Tables(0).Rows(0).Item("Mill"))                   
        End If 
        Return objDataSet    

    End Function

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.text = e.SortExpression.ToString()
        sortcol.text = IIF(sortcol.text = "ASC", "DESC", "ASC")
        BindGrid() 
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim intSelectedMillCode As Integer
        Dim strMillCode As String = ""
        Dim strMill As String
        Dim EditLabel As Label

        EventData.EditItemIndex = CInt(e.Item.ItemIndex)
        BindGrid()
        EditLabel = E.Item.FindControl("lblMillCode")
        
        ddlMillCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ddlMillCode")      
        strMill = EditLabel.Text
        ddlMillCode.DataSource = BindMillCode(strMillCode, intSelectedMillCode, strMill)
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        
        Dim EditText As TextBox
        Dim EditLabel As Label
        Dim EstateYieldLNID As String
        Dim txtMill As String
        Dim strTotalWeight As String
        Dim strTotalDed As String
        Dim strParam As string
        Dim intErrNo As Integer
        Dim bIsExceedBunchDelivered As Boolean
        Dim List As Dropdownlist
        Dim strMillCode as String
        Dim lblMsg as label

        EditLabel = E.Item.FindControl("lblEstateYieldLNID")
        EstateYieldLNID = EditLabel.Text
        List = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ddlMillCode")
        strMillCode = List.SelectedItem.Value

        lblMsg = E.Item.FindControl("lblErrMillCode")
        If Trim(strMillCode) = "Please Select Mill Code" Then
           lblMsg.Visible = True
           Exit Sub 
        End If
    
        EditText = E.Item.FindControl("txtTotalBunch")
        strTotalWeight = EditText.Text
        EditText = E.Item.FindControl("txtTotalDed")
        strTotalDed = EditText.Text

        
        If lblEstateYieldID.Text = "" Then Exit Sub

        strParam = EstateYieldLNID & "|" & _
                   strSelectedEstYldId & "|" & _
                   strMillCode & "|" & _
                   "0|0|" & _
                   strTotalWeight & "|" & strTotalDed & "|" & _
                   txtWeight.Text & "|" & txtDedWeight.Text

        Try
            intErrNo = objPDTrx.mtdUpdEstateYieldLN(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_SUM, _
                                                    strCompany, _
                                                    strLocation, _                                                    
                                                    strParam, _
                                                    bIsExceedBunchDelivered)                                                    
                                                    
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_prodtype.aspx")
        End Try

        If bIsExceedBunchDelivered Then
            lblExceedBunchDelivered.Visible = True
        Else
            lblExceedBunchDelivered.Visible = False
            EventData.EditItemIndex = -1
            BindGrid()
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)     
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim EditLabel As Label
        Dim EstateYieldLNID As String
        Dim strParam As String 
        Dim strOppCd_DEL As string = "PD_CLSTRX_ESTATEYIELDLN_DEL"
        Dim intErrNo As integer
        
        EditLabel = E.Item.FindControl("lblEstateYieldLNID")
        strParam = EditLabel.Text
        
        Try
            intErrNo = objPDTrx.mtdDelEstateYieldLine(strOppCd_DEL, _
                                            strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_PRODTYPE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Setup/IN_prodtype.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
                
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateMill As RequiredFieldValidator
        Dim intSelectedMillCode As Integer
        Dim strMillCode As String = ""
    

        Button_Click(SaveBtn,e)
        dataSet = LoadData()

      
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("Mill") = ""
        newRow.Item("Total") = 0
        newRow.Item("TotalDed") = 0
        dataSet.Tables(0).Rows.Add(newRow)

        EventData.DataSource = dataSet

        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()

          
        ddlMillCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ddlMillCode")
        ddlMillCode.DataSource = BindMillCode(strMillCode, intSelectedMillCode, "")
        ddlMillCode.DataValueField = "MillCode"
        ddlMillCode.DataTextField = "MillCode"
        ddlMillCode.DataBind()
        ddlMillCode.SelectedIndex = intSelectedMillCode


    End Sub

    Protected Function BindMillCode(ByVal pv_strMillCode As String,  ByRef pr_intIndex As Integer, ByRef strMill As String) as DataSet
  
        Dim objMillDs As New Object()
        Dim strParam As String = ""
        Dim dr As DataRow
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strSelectedMillCode As String = ""
        
        strParam =  pv_strMillCode & "||1||" & _
                    "MillCode" & " " & SortCol.Text


        Try
            intErrNo = objPMSetup.mtdGetMillCode(strOppCdMill_GET, strParam, objMillDs)
        Catch Exp As System.Exception
            Response.Redirect("../include/mesg/ErrorMessage.aspx?errcode=PD_SETUP_ESTPRODDET_GET&errmesg=" & lblErrMessage.Text & "&redirect=PD/Setup/PD_trx_EstProdDet.aspx")
        End Try
        
        strSelectedMillCode = Trim(UCase(strMill))

        For intCnt = 0 To objMillDs.Tables(0).Rows.Count - 1
            objMillDs.Tables(0).Rows(intCnt).Item("MillCode") = objMillDs.Tables(0).Rows(intCnt).Item("MillCode").Trim()
            If UCase(objMillDs.Tables(0).Rows(intCnt).Item("MillCode")) = strSelectedMillCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        
        dr = objMillDs.Tables(0).NewRow()
        dr("MillCode") = "Please Select Mill Code"
        objMillDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlMillCode.DataSource = objMillDs.Tables(0)
        ddlMillCode.DataValueField = "MillCode"
        ddlMillCode.DataBind()
        ddlMillCode.SelectedIndex = intSelectedIndex
        objMillDs = Nothing

        Return objMillDs
    End Function

    Sub UpdateEstateYieldLN()
        
        Dim dgi As DataGridItem
        Dim updateList as String
        Dim strRPParam As String
        Dim intErrNo As Integer
        Dim strOpCd_Update As String = "PD_CLSTRX_ESTATEYIELDLNID_UPD"
        
        If strSelectedEstYldId = "" Then Exit Sub

        For Each dgi In EventData.Items
            Dim lbl As Label = dgi.FindControl("lblEstateYieldLNID")
            
            If Not lbl Is Nothing Then
                updateList = updateList & lbl.Text & ","
            End If
        Next

        strRPParam = Left(updateList, len(updateList) -1) & "|" & strSelectedEstYldId

        If trim(updateList) <> "" Then
            intErrNo = objPDTrx.mtdUpdEstateYieldLNID(strOpCd_Update, strRPParam)  
        End If
    End Sub 

End Class
