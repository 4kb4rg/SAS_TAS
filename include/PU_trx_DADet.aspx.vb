Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class PU_DADet : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrOnHand As Label
    Protected WithEvents lblErrOnHold As Label
    Protected WithEvents lblErrQty As Label
    Protected WithEvents lblErrCost As Label
    Protected WithEvents lblErrQtyDisp As Label
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblErrVehExp As Label
    Protected WithEvents lblTo As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblSelectListLoc As Label
    Protected WithEvents lblPleaseSelectOne As Label
    Protected WithEvents lblLocation As Label
    Protected WithEvents lblPRLocation As Label

    Protected WithEvents lblAccount As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents lblVehExpense As Label

    Protected WithEvents lblDispAdvId As Label
    Protected WithEvents lblDispAdvType As Label
    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents ddlToLocCode As DropDownList
    Protected WithEvents lblToLocCode As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblHidStatus As Label
    'Protected WithEvents ddlSuppCode As DropDownList
    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtAccName As TextBox
    Protected WithEvents txtPIC As TextBox

    Protected WithEvents lblSuppCode As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblPrintDate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents ddlPOId As DropDownList
    Protected WithEvents txtPRRefId As TextBox
    Protected WithEvents ddlPRRefLocCode As DropDownList
    Protected WithEvents ddlItemCode As DropDownList
    Protected WithEvents lblErrItemCode As Label
    Protected WithEvents lblSelectedItemCode As Label
    Protected WithEvents lblSelectedGRID As Label
    Protected WithEvents lblQtyReceive As Label
    Protected WithEvents txtQty As TextBox
    Protected WithEvents txtCost As TextBox
    'Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents ddlVehExpCode As DropDownList
    Protected WithEvents dgDADet As DataGrid
    Protected WithEvents dgLine As DataGrid

    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents txtRemark As TextBox
    Protected WithEvents lblVehicleOption As Label
    Protected WithEvents lblDocType As Label
    Protected WithEvents tblFACode As HtmlTable

    Protected WithEvents btnSelETDLoc As Image
    Protected WithEvents btnSelETA As Image
    Protected WithEvents btnSelETAtoLoc As Image

    Protected WithEvents btnAdd As ImageButton
    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnConfirm As ImageButton
    Protected WithEvents btnPrint As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton
    Protected WithEvents btnCancel As ImageButton
    Protected WithEvents btnBack As ImageButton
    Protected WithEvents tblLine As HtmlTable
    Protected WithEvents tblDoc As HtmlTable
    Protected WithEvents tblAcc As HtmlTable
    Protected WithEvents hidItemType As HtmlInputHidden
    Protected WithEvents DispAdvId As HtmlInputHidden
    Protected WithEvents FindAcc As HtmlInputButton
    Protected WithEvents tblDoc1 As HtmlTable
    Protected WithEvents RowChargeLevel As HtmlTableRow
    Protected WithEvents RowPreBlk As HtmlTableRow
    Protected WithEvents RowBlk As HtmlTableRow
    Protected WithEvents lblPreBlockErr As Label
    Protected WithEvents lblPreBlkTag As Label
    Protected WithEvents ddlPreBlock As DropDownList
    Protected WithEvents ddlChargeLevel As DropDownList
    Protected WithEvents ddlDAIssue As DropDownList
    Protected WithEvents lblDAIssue As Label

    Protected WithEvents lblPBBKB As Label

    Protected WithEvents hidBlockCharge As HtmlInputHidden
    Protected WithEvents hidChargeLocCode As HtmlInputHidden

    Protected WithEvents lblErrOutStandingQty As Label
    Protected WithEvents lblErrPOID As Label
    Protected WithEvents lblOutStandingQtyMsg As Label
    Protected WithEvents lblOutStandingQty As Label
    Protected WithEvents lblIDQtyReceive As Label

    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents DDLDispCat As DropDownList
    Protected WithEvents lblInventoryBin As Label

    Protected WithEvents lblETDDeskripsi As Label
    Protected WithEvents lblETADeskripsi As Label
    Protected WithEvents lblETDLoc As Label
    Protected WithEvents lblETALoc As Label
    Protected WithEvents lblETAToLoc As Label

    Protected WithEvents txtETALocDeskripsi As TextBox
    Protected WithEvents txtETDLoc As TextBox
    Protected WithEvents txtETALoc As TextBox
    Protected WithEvents txtETAToLoc As TextBox

    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents txtDispAdvDate As TextBox
    Protected WithEvents btnNew As ImageButton

    Protected WithEvents txtAddNote As TextBox
    Protected WithEvents txtTransporter As TextBox
    Protected WithEvents txtAddress As TextBox


    Protected WithEvents lstStorage As Dropdownlist
    Protected WithEvents lblstoragemsg As Label
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents txtSuppName As TextBox
    Protected WithEvents btnEdited As ImageButton
    Protected WithEvents lblCurrentPeriod As Label
    Protected WithEvents btnSelDate As Image

    Protected WithEvents TrLink As HtmlTableRow
    Protected WithEvents lbViewJournal As LinkButton
    Protected WithEvents dgViewJournal As DataGrid
    Protected WithEvents lblTotalDB As Label
    Protected WithEvents lblTotalCR As Label
    Protected WithEvents lblTotalViewJournal As Label

    Dim PreBlockTag As String
    Dim BlockTag As String

    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objIN As New agri.IN.clsTrx()
    Protected objINSetup As New agri.IN.clsSetup()
    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objAdminSetup As New agri.Admin.clsLoc()
    Protected objSysCfg As New agri.PWSystem.clsConfig()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmShare As New agri.Admin.clsShare()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objINstp As New agri.IN.clsSetup()

    Dim objDALnDs As New Object()
    Dim objAccDs As New Object()
    Dim objBlkDs As New Object()
    Dim objVehDs As New Object()
    Dim objVehExpDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intFAAR As Integer
    Dim intConfigSetting As Integer
    Dim strLocLevel As String

    Dim strSelectedDispAdvId As String
    Dim strSelectedDAType As String
    Dim strSelectedSuppCode As String
    Dim strSelectedToLocCode As String
    Dim strSelectedPOId As String
    Dim strSelectedPRRefLocCode As String
    Dim strSelectedGoodsRcvLnId As String
    Dim strSelectedItemCode As String
    Dim strItemType As String

    Dim strPhyYear As String
    Dim strPhyMonth As String
    Dim strLastPhyYear As String


    Const ITEM_PART_SEPERATOR As String = " @ "
    Dim strLocType As String
    Dim strAcceptFormat As String
    Dim strPOLocCode As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strPhyYear = Session("SS_PHYYEAR")
        strPhyMonth = Session("SS_PHYMONTH")
        strLastPhyYear = Session("SS_LASTPHYYEAR")
        strLocType = Session("SS_LOCTYPE")
        intLevel = Session("SS_USRLEVEL")
        strLocLevel = Session("SS_LOCLEVEL")
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUDispatchAdvice), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblToLocCode.Visible = False
            lblSuppCode.Visible = False
            lblErrItemCode.Visible = False
            lblErrAccount.Visible = False
            lblPreBlockErr.Visible = False
            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblErrVehExp.Visible = False
            lblInventoryBin.Visible = False
            lblDate.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            btnAdd.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnAdd).ToString())
            btnNew.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnNew).ToString())
            btnSave.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnSave).ToString())
            btnConfirm.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnConfirm).ToString())
            btnPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnPrint).ToString())
            btnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnDelete).ToString())
            btnUnDelete.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnUnDelete).ToString())
            btnEdited.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnEdited).ToString())
            btnCancel.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnCancel).ToString())
            btnBack.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnBack).ToString())

            strSelectedDispAdvId = Trim(IIf(Request.QueryString("DispAdvId") = "", Request.Form("DispAdvId"), Request.QueryString("DispAdvId")))
            strSelectedDAType = Trim(IIf(Request.QueryString("DAType") = "", Request.Form("DAType"), Request.QueryString("DAType")))

            If strSelectedDAType = objPU.mtdGetGRNType(objPU.EnumGRNType.FixedAsset) Then
                tblFACode.Visible = True
            Else
                tblFACode.Visible = False
            End If

            onload_GetLangCap()
            If Not IsPostBack Then
                lSetDispCategory()
                BindChargeLevelDropDownList()
                
                If strLocLevel = "1" Then
                    BindInventoryBinLevel(objINstp.EnumInventoryBinLevel.Central)
                ElseIf strLocLevel = "3" Then
                    BindInventoryBinLevel(objINstp.EnumInventoryBinLevel.HO)
                Else
                    BindInventoryBinLevel("")
                End If
              
                lblETDDeskripsi.Text = strLocation
                If strSelectedDispAdvId <> "" Then

                    DispAdvId.Value = strSelectedDispAdvId
                    onLoad_Display(strSelectedDispAdvId)
                    onLoad_DisplayLn(strSelectedDispAdvId)
               
                    BindToLocCode("")
                    BindSupp("")
                    BindPO("")
                    BindLoc("")
                    ddlDAIssue.Enabled = False

                    If LEFT(TRIM(strSelectedDispAdvId), 3) = "BPB" Then
                        btnDelete.Visible = False
                        btnUnDelete.Visible = False
                        btnCancel.Visible = False
                        btnPrint.Visible = False
                        btnConfirm.Visible = False
                        btnEdited.Visible = False
                    End If

                Else
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False
                    btnCancel.Visible = False
                    btnPrint.Visible = False
                    btnConfirm.Visible = False
                    btnEdited.Visible = False
                    If strSelectedDAType <> "" Then
                        lblDispAdvType.Text = strSelectedDAType
                        If lblDispAdvType.Text = objPU.EnumDAType.Stock Then
                            lblDocType.Text = "Stock / Workshop"
                        Else
                            lblDocType.Text = objPU.mtdGetDAType(CInt(strSelectedDAType))
                        End If
                    End If

                    txtDispAdvDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    txtETDLoc.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    txtETALoc.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    txtETAToLoc.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                    BindStorage("")    
                    BindToLocCode(strSelectedDispAdvId)
                    BindSupp("")
                    BindPO("")
                    BindLoc("")
                    BindPOIssued("")
                End If


                FindAcc.Visible = False
                txtAccCode.Enabled = False
                ddlBlock.Enabled = False
                ddlPreBlock.Enabled = False
                ddlChargeLevel.Enabled = False
                ddlVehCode.Enabled = False
                ddlVehExpCode.Enabled = False

                BindAccount("")
                BindPreBlock("", "")
                BindBlock("", "")
                BindVehicle("", "")
                BindVehicleExpense(True, "")
                TrLink.Visible = True
                txtQty.Text = 0
                txtCost.Text = 0
            End If
        End If
        'btnCancel.Visible = False


        lblErrOutStandingQty.Visible = False
        lblErrPOID.Visible = False
        lblOutStandingQtyMsg.Visible = False
        lblOutStandingQty.Visible = False

    End Sub

    Sub BindChargeLevelDropDownList()
        ddlChargeLevel.Items.Add(New ListItem(PreBlockTag, objLangCap.EnumLangCap.Block))
        ddlChargeLevel.Items.Add(New ListItem(BlockTag, objLangCap.EnumLangCap.SubBlock))
        ddlChargeLevel.SelectedIndex = Session("SS_BLOCK_CHARGE_DEFAULT")
        RowChargeLevel.Visible = Session("SS_BLOCK_CHARGE_VISIBLE")
        ToggleChargeLevel()
    End Sub
    
    Sub ddlChargeLevel_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ToggleChargeLevel()
    End Sub
    
    Sub ToggleChargeLevel()
        If ddlChargeLevel.selectedIndex = 0 Then
            RowBlk.Visible = False
            RowPreBlk.Visible = True
            hidBlockCharge.value = "yes"
        Else
            RowBlk.Visible = True
            RowPreBlk.Visible = False
            hidBlockCharge.value = ""
        End If
    End Sub

    Sub lSetDispCategory()
        DDLDispCat.Items.Clear()
        DDLDispCat.Items.Add("Select Category")
        DDLDispCat.Items.Add(New ListItem("Darat", "1"))
        DDLDispCat.Items.Add(New ListItem("Laut", "2"))
        DDLDispCat.Items.Add(New ListItem("Udara", "3"))
        DDLDispCat.Items.Add(New ListItem("Hand carry/titip", "4"))
    End Sub

  Sub BindStorage(ByVal pv_strcode As String)

        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer


        sSQLKriteria = "Select StorageCode,Description From IN_STORAGE Where LocCode='" & strLocation & "'"


        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_NEW_GRLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        For intCnt = 0 To objdsST.Tables(0).Rows.Count - 1
            objdsST.Tables(0).Rows(intCnt).Item("StorageCode") = Trim(objdsST.Tables(0).Rows(intCnt).Item("StorageCode"))
            objdsST.Tables(0).Rows(intCnt).Item("Description") = objdsST.Tables(0).Rows(intCnt).Item("StorageCode") & " (" & Trim(objdsST.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objdsST.Tables(0).Rows(intCnt).Item("StorageCode") = Trim(pv_strcode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objdsST.Tables(0).NewRow()
        dr("StorageCode") = ""
        dr("Description") = "Please Select Storage"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        lstStorage.DataSource = objdsST.Tables(0)
        lstStorage.DataValueField = "StorageCode"
        lstStorage.DataTextField = "Description"
        lstStorage.DataBind()
        lstStorage.SelectedIndex = intSelectedIndex

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
                BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_DADET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_DAList.aspx")
        End Try

        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblPRLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpense.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text

        'dgDADet.Columns(7).HeaderText = "PR Ref. " & lblPRLocation.Text
        dgDADet.Columns(7).HeaderText = lblAccount.Text
        dgDADet.Columns(8).HeaderText = lblBlock.Text
        dgDADet.Columns(9).HeaderText = lblVehicle.Text
        dgDADet.Columns(10).HeaderText = lblVehExpense.Text

        lblToLocCode.Text = lblPleaseSelect.Text & lblLocation.Text
        lblErrAccount.Text = "<BR>" & lblPleaseSelectOne.Text & lblAccount.Text
        lblErrBlock.Text = lblPleaseSelectOne.Text & lblBlock.Text
        lblErrVehicle.Text = lblPleaseSelectOne.Text & lblVehicle.Text
        lblErrVehExp.Text = lblPleaseSelectOne.Text & lblVehExpense.Text
        PreBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        lblPreBlkTag.Text = PreBlockTag & lblCode.Text & " : "
        lblPreBlockErr.Text = lblPleaseSelectOne.Text & PreBlockTag & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_DADET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/PU_trx_DAList.aspx")
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

    Sub onLoad_Display(ByVal pv_strDispAdvId As String)
        Dim strOpCd As String = "PU_CLSTRX_DA_GET"
        Dim objDADs As New Object()
        Dim intErrNo As Integer
		
		Dim strParam As String = pv_strDispAdvId & "|||||||A.DispAdvId||||"
        Dim intCnt As Integer = 0

        Try
            intErrNo = objPU.mtdGetDA(strOpCd, strParam, objDADs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        If objDADs.Tables(0).Rows.Count > 0 Then
            lblDispAdvId.Text = pv_strDispAdvId
            lblAccPeriod.Text = Trim(objDADs.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(objDADs.Tables(0).Rows(0).Item("AccYear"))
            lblDispAdvType.Text = Trim(objDADs.Tables(0).Rows(0).Item("DispAdvType")) 

            If objDADs.Tables(0).Rows(0).Item("DispAdvType") = objPU.EnumDAType.Stock Then
                lblDocType.Text = "Stock / Workshop"
            Else
                lblDocType.Text = objPU.mtdGetDAType(objDADs.Tables(0).Rows(0).Item("DispAdvType")) 
            End If

            strSelectedToLocCode = Trim(objDADs.Tables(0).Rows(0).Item("ToLocCode"))
            'strSelectedSuppCode = Trim(objDADs.Tables(0).Rows(0).Item("SupplierCode"))
            lblStatus.Text = objPU.mtdGetDAStatus(Trim(objDADs.Tables(0).Rows(0).Item("Status")))
            lblHidStatus.Text = Trim(objDADs.Tables(0).Rows(0).Item("Status"))
            lblCreateDate.Text = objGlobal.GetLongDate(objDADs.Tables(0).Rows(0).Item("CreateDate"))
            lblUpdateDate.Text = objGlobal.GetLongDate(objDADs.Tables(0).Rows(0).Item("UpdateDate"))
            lblPrintDate.Text = objGlobal.GetLongDate(objDADs.Tables(0).Rows(0).Item("PrintDate"))
            lblUpdateBy.Text = objDADs.Tables(0).Rows(0).Item("UserName")
            txtRemark.Text = Trim(objDADs.Tables(0).Rows(0).Item("Remark"))
            txtDispAdvDate.Text = Date_Validation(objDADs.Tables(0).Rows(0).Item("CreateDate"), True)
            txtTransporter.Text = Trim(objDADs.Tables(0).Rows(0).Item("Transporter"))
            lblCurrentPeriod.Text = Trim(objDADs.Tables(0).Rows(0).Item("AccYear")) & Trim(objDADs.Tables(0).Rows(0).Item("AccMonth"))
            'txtSuppCode.Text = Trim(objDADs.Tables(0).Rows(0).Item("SupplierCode"))
            'txtSuppName.Text = Trim(objDADs.Tables(0).Rows(0).Item("SupplierName"))
            txtPIC.Text = Trim(objDADs.Tables(0).Rows(0).Item("PIC"))
            txtETDLoc.Text = Date_Validation(objDADs.Tables(0).Rows(0).Item("ETDLocDate"), True)
            txtETALoc.Text = Date_Validation(objDADs.Tables(0).Rows(0).Item("ETAToLocToDate"), True)
            txtETAToLoc.Text = Date_Validation(objDADs.Tables(0).Rows(0).Item("ETASecLocToDate"), True)
            txtETALocDeskripsi.Text = Trim(objDADs.Tables(0).Rows(0).Item("ETASecLocDesc"))
            txtAddress.Text = Trim(objDADs.Tables(0).Rows(0).Item("Dispaddress"))


            If Trim(objDADs.Tables(0).Rows(0).Item("DispCat")) = "1" Then
                DDLDispCat.SelectedIndex = 1
            ElseIf Trim(objDADs.Tables(0).Rows(0).Item("DispCat")) = "2" Then
                DDLDispCat.SelectedIndex = 2
            ElseIf Trim(objDADs.Tables(0).Rows(0).Item("DispCat")) = "3" Then
                DDLDispCat.SelectedIndex = 3
            ElseIf Trim(objDADs.Tables(0).Rows(0).Item("DispCat")) = "4" Then
                DDLDispCat.SelectedIndex = 4
            End If

            BindPOIssued(Trim(objDADs.Tables(0).Rows(0).Item("DALoc")))
            BindPO(strSelectedSuppCode)
            BindInventoryBinLevel(Trim(objDADs.Tables(0).Rows(0).Item("Bin")))

            If lblDispAdvType.Text = objPU.EnumDAType.DirectCharge Then
                txtAccCode.Text = BIndCoaLocCode(strSelectedToLocCode)
            Else
                txtAccCode.Text = ""
            End If

            Select Case Trim(objDADs.Tables(0).Rows(0).Item("Status"))
                Case objPU.EnumDAStatus.Active
                    ddlToLocCode.Enabled = True
                    txtSuppCode.Enabled = False
                    tblLine.Visible = True
                    tblDoc.Visible = True
                    tblDoc1.Visible = True

                    If strSelectedDAType = objPU.mtdGetGRNType(objPU.EnumGRNType.FixedAsset) Then
                        tblFACode.Visible = True
                    Else
                        tblFACode.Visible = False
                    End If

                    ddlPOId.Enabled = True
                    txtPRRefId.Enabled = True
                    ddlPRRefLocCode.Enabled = True
                    ddlItemCode.Enabled = True
                    txtQty.Enabled = True
                    txtCost.Enabled = False
                    btnAdd.Visible = True
                    FindAcc.Visible = True
                    btnSave.Visible = True
                    btnConfirm.Visible = True
                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    btnUnDelete.Visible = False
                    btnCancel.Visible = False
                    btnPrint.Visible = True
                    txtRemark.Enabled = True
                    txtTransporter.Enabled = True
                    btnEdited.Visible = False

                Case objPU.EnumDAStatus.Confirmed
                    txtDispAdvDate.Enabled = False
                    ddlToLocCode.Enabled = False
                    txtSuppCode.Enabled = False
                    tblLine.Visible = False
                    tblDoc.Visible = False
                    tblDoc1.Visible = False
                    tblFACode.Visible = False
                    tblAcc.Visible = False
                    ddlPOId.Enabled = False
                    txtPRRefId.Enabled = False
                    ddlPRRefLocCode.Enabled = False
                    ddlItemCode.Enabled = False
                    txtQty.Enabled = False
                    txtCost.Enabled = False
                    btnAdd.Visible = False
                    FindAcc.Visible = False
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False
                    btnPrint.Visible = True
                    txtRemark.Enabled = False
                    ddlInventoryBin.Enabled = False
                    btnSelDate.Visible = False
                    txtTransporter.Enabled = False
                    btnEdited.Visible = True
                    btnCancel.Visible = True
                    btnCancel.Attributes("onclick") = "javascript:return ConfirmAction('cancel');"

                Case objPU.EnumDAStatus.Deleted
                    txtDispAdvDate.Enabled = False
                    ddlToLocCode.Enabled = False
                    txtSuppCode.Enabled = False
                    tblLine.Visible = False
                    tblDoc.Visible = False
                    tblDoc1.Visible = False
                    tblFACode.Visible = False
                    tblAcc.Visible = False
                    ddlPOId.Enabled = False
                    txtPRRefId.Enabled = False
                    ddlPRRefLocCode.Enabled = False
                    ddlItemCode.Enabled = False
                    txtQty.Enabled = False
                    txtCost.Enabled = False
                    btnAdd.Visible = False
                    FindAcc.Visible = False
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                    btnUnDelete.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
                    btnUnDelete.Visible = True
                    btnCancel.Visible = False
                    btnPrint.Visible = False
                    txtRemark.Enabled = False
                    ddlInventoryBin.Enabled = False
                    btnSelDate.Visible = False
                    txtTransporter.Enabled = False
                    btnEdited.Visible = False

                Case objPU.EnumDAStatus.Cancelled
                    txtDispAdvDate.Enabled = False
                    ddlToLocCode.Enabled = False
                    txtSuppCode.Enabled = False
                    tblLine.Visible = False
                    tblDoc.Visible = False
                    tblDoc1.Visible = False
                    tblFACode.Visible = False
                    tblAcc.Visible = False
                    ddlPOId.Enabled = False
                    txtPRRefId.Enabled = False
                    ddlPRRefLocCode.Enabled = False
                    ddlItemCode.Enabled = False
                    txtQty.Enabled = False
                    txtCost.Enabled = False
                    btnAdd.Visible = False
                    FindAcc.Visible = False
                    btnSave.Visible = False
                    btnConfirm.Visible = False
                    btnDelete.Visible = False
                    btnUnDelete.Visible = False
                    btnCancel.Visible = False
                    btnPrint.Visible = False
                    ddlInventoryBin.Enabled = False
                    btnSelDate.Visible = False
                    txtTransporter.Enabled = False
                    btnEdited.Visible = False
            End Select
        End If
    End Sub

    Sub onLoad_DisplayLn(ByVal pv_strDispAdvId As String)
        Dim strOpCd As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strParam As String = pv_strDispAdvId
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim dblTotalAmount As Double
        Dim UpdButton As LinkButton
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton
        Dim strStorageCode As string

        Try
            intErrNo = objPU.mtdGetDALn(strOpCd, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strAccMonth, _
                                        strAccYear, _
                                        strParam, _
                                        objDALnDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_DALn&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        lstStorage.Enabled=true
        For intCnt = 0 To objDALnDs.Tables(0).Rows.Count - 1
            lstStorage.Enabled=false

            objDALnDs.Tables(0).Rows(intCnt).Item("DispAdvLnId") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("DispAdvLnId"))
            objDALnDs.Tables(0).Rows(intCnt).Item("DispAdvId") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("DispAdvId"))
            objDALnDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId"))
            objDALnDs.Tables(0).Rows(intCnt).Item("GoodsRcvId") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("GoodsRcvId"))
            objDALnDs.Tables(0).Rows(intCnt).Item("PRID") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("PRID"))
            objDALnDs.Tables(0).Rows(intCnt).Item("PRLocCode") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("PRLocCode"))
            objDALnDs.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("ItemCode"))
            objDALnDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("ItemCode")) & " (" & _
                                                                   Trim(objDALnDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            objDALnDs.Tables(0).Rows(intCnt).Item("PurchaseUOM") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("PurchaseUOM"))
            objDALnDs.Tables(0).Rows(intCnt).Item("QtyDisp") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("QtyDisp"))
            objDALnDs.Tables(0).Rows(intCnt).Item("Cost") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("Cost"))
            objDALnDs.Tables(0).Rows(intCnt).Item("Amount") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("Amount"))
            objDALnDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("AccCode"))
            objDALnDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("BlkCode"))
            objDALnDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("VehCode"))
            objDALnDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
            objDALnDs.Tables(0).Rows(intCnt).Item("POLnId") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("POLnId"))
            objDALnDs.Tables(0).Rows(intCnt).Item("POId") = Trim(objDALnDs.Tables(0).Rows(intCnt).Item("POId"))
            strStorageCode=Trim(objDALnDs.Tables(0).Rows(intCnt).Item("StorageCode"))

            dblTotalAmount += objDALnDs.Tables(0).Rows(intCnt).Item("Amount")
        Next intCnt

        BindStorage(strStorageCode)
        dgDADet.DataSource = objDALnDs.Tables(0)
        dgDADet.DataBind()

        lblTotalAmount.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(dblTotalAmount, 2), 2)

        For intCnt = 0 To objDALnDs.Tables(0).Rows.Count - 1
            DelButton = dgDADet.Items.Item(intCnt).FindControl("Delete")
            EdtButton = dgDADet.Items.Item(intCnt).FindControl("Edit")
            UpdButton = dgDADet.Items.Item(intCnt).FindControl("Update")
            CanButton = dgDADet.Items.Item(intCnt).FindControl("Cancel")

            CType(dgDADet.Items.Item(intCnt).FindControl("lblNoUrut"), Label).Text = intCnt + 1

            Select Case Trim(lblStatus.Text)
                Case objPU.mtdGetDAStatus(objPU.EnumDAStatus.Active)
                    DelButton.Visible = True
                    DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    EdtButton.Visible = False
                    UpdButton.Visible = False
                    CanButton.Visible = False
                Case objPU.mtdGetDAStatus(objPU.EnumDAStatus.Confirmed)
                    DelButton.Visible = False
                    DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                    EdtButton.Visible = True
                    UpdButton.Visible = False
                    CanButton.Visible = False
                Case objPU.mtdGetDAStatus(objPU.EnumDAStatus.Deleted), objPU.mtdGetDAStatus(objPU.EnumDAStatus.Cancelled)
                    DelButton.Visible = False
                    EdtButton.Visible = False
                    UpdButton.Visible = False
                    CanButton.Visible = False
            End Select
        Next intCnt

        If objDALnDs.Tables(0).Rows.Count > 0 Then
            TrLink.Visible = True
        Else
            TrLink.Visible = False
        End If
    End Sub

    Function BIndCoaLocCode(ByVal pLoc As String) As String
        Dim objItemDs As New Object()
        Dim strOpCode As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim nValue As String = ""
        Dim sSQLKriteria As String = "AND A.LocCode='" & pLoc & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        nValue = ""
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objItemDs.Tables(0).Rows.Count > 0 Then
            nValue = Trim(objItemDs.Tables(0).Rows(0).Item("AccCode"))
        End If

        Return nValue
    End Function

    Sub BindToLocCode(ByVal pv_strPRId As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objToLocCodeDs As New Object()
        Dim strToLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strToLocCode = IIf(pv_strPRId = "", "", strSelectedToLocCode)
        strParam = strToLocCode & "|" & objAdminSetup.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objToLocCodeDs, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objToLocCodeDs.Tables(0).Rows.Count - 1
            With objToLocCodeDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = strSelectedToLocCode Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objToLocCodeDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblLocation.Text
        objToLocCodeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlToLocCode.DataSource = objToLocCodeDs.Tables(0)
        ddlToLocCode.DataValueField = "LocCode"
        ddlToLocCode.DataTextField = "Description"
        ddlToLocCode.DataBind()
        ddlToLocCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindSupp(ByVal pv_strGoodsRcvId As String)
        'Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_GET"
        'Dim objSuppDs As New Object()
        'Dim strSuppCode As String
        'Dim strParam As String = ""
        'Dim intCnt As Integer = 0
        'Dim intErrNo As Integer
        'Dim dr As DataRow
        'Dim intSelectedIndex As Integer
        'Dim strSuppType As String = objPUSetup.EnumSupplierType.Internal & "','" & objPUSetup.EnumSupplierType.External & "','" & objPUSetup.EnumSupplierType.Associate & "','" & objPUSetup.EnumSupplierType.Contractor

        'strSuppCode = IIf(pv_strGoodsRcvId = "", "", strSelectedSuppCode)
        'strParam = strSuppCode & "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
        ''strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
        'strParam = strParam & "|" & IIf(Session("SS_COACENTRALIZED") = "1", "", " SupplierCode LIKE '%" & Trim(strLocation) & "%'") ', " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
        'strParam = strParam & "|" & strSuppType

        'Try
        '    intErrNo = objPUSetup.mtdGetSupplier(strOpCd, strParam, objSuppDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_GRList.aspx")
        'End Try

        'For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
        '    objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
        '    objSuppDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode")) & " (" & Trim(objSuppDs.Tables(0).Rows(intCnt).Item("Name")) & ")"

        '    If objSuppDs.Tables(0).Rows(intCnt).Item("SupplierCode") = strSelectedSuppCode Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next intCnt

        'dr = objSuppDs.Tables(0).NewRow()
        'dr("SupplierCode") = ""
        'dr("Name") = "Please select Supplier Code"
        'objSuppDs.Tables(0).Rows.InsertAt(dr, 0)

        'ddlSuppCode.DataSource = objSuppDs.Tables(0)
        'ddlSuppCode.DataValueField = "SupplierCode"
        'ddlSuppCode.DataTextField = "Name"
        'ddlSuppCode.DataBind()
        'ddlSuppCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub SuppIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'strSelectedSuppCode = ddlSuppCode.SelectedItem.Value
        'BindPO(ddlSuppCode.SelectedItem.Value)
    End Sub

    Sub BindPO(ByVal pv_strSuppCode As String)
        'Dim strOpCd As String = "PU_CLSTRX_DA_PO_ID_GET"
        Dim strOpCd As String
        Dim objPODs As New Object()
        Dim strParam As String
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        'strParam = pv_strSuppCode & "|" & _
        '           objPU.EnumGRStatus.Confirmed & "|" & _
        '           lblDispAdvType.Text & "|" & _
        '           strLocation


        'Try
        '    intErrNo = objPU.mtdGetPOID(strOpCd, strParam, objPODs)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_POID&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        'End Try

        If lblDispAdvType.Text = objPU.EnumDAType.Stock Then
            strOpCd = "PU_CLSTRX_DISPADV_PO_ID_GET"
        Else
            strOpCd = "PU_CLSTRX_DISPADV_PO_ID_GET_DC"
        End If

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "STATUS|POTYPE|LOCCODE|DISPADVID"
        strParamValue = "('" & objPU.EnumGRStatus.Confirmed & "','" & objPU.EnumGRStatus.Closed & "')" & _
                        "|" & lblDispAdvType.Text & _
                        "|" & strLocation & _
                        "|" & lblDispAdvId.Text
        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objPODs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
        End Try

        For intCnt = 0 To objPODs.Tables(0).Rows.Count - 1
            objPODs.Tables(0).Rows(intCnt).Item("POId") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId"))
            objPODs.Tables(0).Rows(intCnt).Item("DispPOId") = Trim(objPODs.Tables(0).Rows(intCnt).Item("POId")) & ",  PR ID : " & Trim(objPODs.Tables(0).Rows(intCnt).Item("PRID")) & ",  Supplier : (" & Trim(objPODs.Tables(0).Rows(intCnt).Item("Name")) & ")"
            objPODs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objPODs.Tables(0).Rows(intCnt).Item("LocCode"))

            If objPODs.Tables(0).Rows(intCnt).Item("POId") = strSelectedPOId Then
                intSelectedIndex = intCnt + 1
                
                lblPRLocation.Text = objPODs.Tables(0).Rows(intCnt).Item("LocCode")
                strPOLocCode = lblPRLocation.Text
            Else
                lblPRLocation.Text = ""
            End If
        Next intCnt

        dr = objPODs.Tables(0).NewRow()
        dr("POId") = ""
        dr("DispPOId") = "Please select Purchase Order ID"
        objPODs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPOId.DataSource = objPODs.Tables(0)
        ddlPOId.DataValueField = "POId"
        ddlPOId.DataTextField = "DispPOId"
        ddlPOId.DataBind()
        ddlPOId.SelectedIndex = intSelectedIndex
        strSelectedPOId = ddlPOId.SelectedItem.Value

        If ddlPOId.SelectedItem.Value = "" Then
            BindItemCode("")
        Else
            BindPOItem(strSelectedPOId)
        End If
    End Sub

    Sub IndexChangeCategory(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nValCat As String = ""
        nValCat = DDLDispCat.SelectedItem.Value
        If nValCat = "1" Then
            txtETDLoc.Enabled = False
            txtETALoc.Enabled = False
            txtETAToLoc.Enabled = False

            btnSelETDLoc.Enabled = False
            btnSelETA.Enabled = False
            btnSelETDLoc.Enabled = False

            txtETDLoc.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
            txtETALoc.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
            txtETAToLoc.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)

            txtETDLoc.BackColor = Drawing.Color.LightGray
            txtETALoc.BackColor = Drawing.Color.LightGray
            txtETAToLoc.BackColor = Drawing.Color.LightGray

        ElseIf nValCat = "2" Then
            txtETDLoc.Enabled = True
            txtETALoc.Enabled = True
            txtETAToLoc.Enabled = True

            txtETDLoc.BackColor = Drawing.Color.LightGreen
            txtETALoc.BackColor = Drawing.Color.LightGreen
            txtETAToLoc.BackColor = Drawing.Color.LightGreen
        Else
            txtETDLoc.BackColor = Drawing.Color.White
            txtETALoc.BackColor = Drawing.Color.White
            txtETAToLoc.BackColor = Drawing.Color.White
        End If
    End Sub

    Sub DispLocAddressIndex(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim objItemDs As New Object()
        Dim strOpCode As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim sSQLKriteria As String = "AND A.LocCode='" & ddlToLocCode.SelectedItem.Value & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objItemDs.Tables(0).Rows.Count > 0 Then
            txtAddress.Text = Trim(objItemDs.Tables(0).Rows(0).Item("Address"))
            If lblDispAdvType.Text = objPU.EnumDAType.DirectCharge Then
                txtAccCode.Text = Trim(objItemDs.Tables(0).Rows(0).Item("AccCode"))
            Else
                txtAccCode.Text = ""
            End If
        End If
    End Sub

    Sub POIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim POType As String

        strSelectedPOId = ddlPOId.SelectedItem.Value
     
        If strSelectedPOId = "" Then
            
            BindItemCode("")
            txtPRRefId.Enabled = True
            ddlPRRefLocCode.Enabled = True
            ddlPOId.Enabled = True
            lblPRLocation.Text = ""
            lblIDQtyReceive.Text = ""
        Else

            BindPOItem(strSelectedPOId)
            txtPRRefId.Enabled = False
            ddlPRRefLocCode.Enabled = False
            lblPRLocation.Text = strPOLocCode
            POType = GetPOType(strSelectedPOId)

            If POType = objPU.EnumPOType.FixedAsset And objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumModuleActivation.FixAsset), intFAAR) = True Then
                tblFACode.Visible = True
                BindFACode(ddlItemCode.SelectedItem.Value)
            End If

        End If
        FindAcc.Visible = False
        txtAccCode.Enabled = False
        ddlBlock.Enabled = False
        ddlPreBlock.Enabled = False
        ddlChargeLevel.Enabled = False
        ddlVehCode.Enabled = False
        ddlVehExpCode.Enabled = False

        txtPRRefId.Text = ""
        BindLoc("")
        BindAccount("")
        BindPreBlock("", "")
        BindBlock("", "")
        BindVehicle("", "")
        BindVehicleExpense(True, "")

    End Sub

    Sub LocIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
 
        If strSelectedPOId = "" Then
            BindItemCode("")
        Else
            BindPOItem(strSelectedPOId)
        End If
        
        
        If ddlPRRefLocCode.SelectedItem.Value <> "" Then
            strSelectedPOId = ddlPOID.SelectedItem.Value
            strSelectedPRRefLocCode = ddlPRRefLocCode.SelectedItem.Value
            strSelectedItemCode = ddlItemCode.SelectedItem.Value
            ddlPOId.Enabled = False
            lblPRLocation.Text = ""
            
        Else
            ddlPOId.Enabled = True
            txtPRRefId.Enabled = False
            ddlPRRefLocCode.Enabled = False
        End If
        
    End Sub
    Sub FAItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtQty.Enabled = False
        txtCost.Enabled = False
    End Sub

    Function GetPOType(ByVal strSelectedPOId As String) As String

        Dim strOpCd_GRPOType As String = "PU_CLSTRX_GR_POTYPE_GET"
        Dim objPOType As New Object()
        Dim intErrNo As Integer
        Dim POType As String

        Try
            intErrNo = objPU.mtdGetPOType(strOpCd_GRPOType, strSelectedPOId, objPOType)
            POType = Trim(objPOType.Tables(0).Rows(0).Item("POType"))
            Return POType
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_POType&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

    End Function

    Sub BindFACode(ByVal pv_strItemCode As String)

    End Sub

    Sub BindPOItem(ByVal pv_strPOId As String)
        'Dim strOpCd As String = "PU_CLSTRX_DA_GR_LINE_GET"
        Dim strOpCd As String
        Dim objPOItemDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)

        'strParam = pv_strPOId & "|" & objPU.EnumGRStatus.Confirmed
        'Try
        '    intErrNo = objPU.mtdGetDAGRLine(strOpCd, _
        '                                    strCompany, _
        '                                    strLocation, _
        '                                    strUserId, _
        '                                    strAccMonth, _
        '                                    strAccYear, _
        '                                    strParam, _
        '                                    objPOItemDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_GRItem&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        'End Try
        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        If lblDispAdvType.Text = objPU.EnumDAType.Stock Then
            If (CInt(strAccYear) * 100) + CInt(strAccMonth) < 201201 Then
                strOpCd = "PU_CLSTRX_DISPADV_GR_LINE_GET"
            Else
                strOpCd = "PU_CLSTRX_DISPADV_GR_LINE_GET_NEW"
            End If
        Else
            strOpCd = "PU_CLSTRX_DISPADV_GR_LINE_GET_DC"
        End If

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "STATUS|POID|LOCCODE|DISPADVID"
        strParamValue = "('" & objPU.EnumGRStatus.Confirmed & "','" & objPU.EnumGRStatus.Closed & "')" & _
                        "|" & pv_strPOId & _
                        "|" & strLocation & _
                        "|" & lblDispAdvId.Text

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objPOItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_ID&errmesg=" & lblErrMessage.Text & "&redirect=PU/trx/PU_trx_DAInternalList.aspx.aspx")
        End Try

        lblSelectedItemCode.Text = ""
        lblSelectedGRID.Text = ""
        lblQtyReceive.Text = ""
        txtPRRefId.Text = ""
        txtQty.Text = ""
        txtCost.Text = ""
        lblIDQtyReceive.Text = ""

        For intCnt = 0 To objPOItemDs.Tables(0).Rows.Count - 1

            If lblDispAdvType.Text = objPU.EnumDAType.Stock Then
                If objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo") <> "" Then


                    objPOItemDs.Tables(0).Rows(intCnt).Item("Description") = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo") & " (" & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("ItemLocCode") & ", " & _
                                                                             "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("Cost"), 2) & ", " & _
                                                                             objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty"), 5) & ", " & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("ReceiveUOM")
                    
                    objPOItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId") = objPOItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId") & ITEM_PART_SEPERATOR & _
                                                                              objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & ITEM_PART_SEPERATOR & _
                                                                              objPOItemDs.Tables(0).Rows(intCnt).Item("PartNo")
                Else


                    objPOItemDs.Tables(0).Rows(intCnt).Item("Description") = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("ItemLocCode") & ", " & _
                                                                             "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("Cost"), 2) & ", " & _
                                                                             objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty"), 5) & ", " & _
                                                                             objPOItemDs.Tables(0).Rows(intCnt).Item("ReceiveUOM")
                End If
            Else


                objPOItemDs.Tables(0).Rows(intCnt).Item("Description") = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode") & " (" & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("Description") & "), " & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("ItemLocCode") & ", " & _
                                                                         "Rp. " & objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("Cost"), 2) & ", " & _
                                                                         objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty"), 5) & ", " & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("ReceiveUOM") & ", " & _
                                                                         objPOItemDs.Tables(0).Rows(intCnt).Item("AdditionalNote")
            End If

            If objPOItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvLnId") = strSelectedGoodsRcvLnId Then
                intSelectedIndex = intCnt + 1

                lblSelectedItemCode.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemCode")
                lblSelectedGRID.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("GoodsRcvId")
                txtPRRefId.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemPR")
                strSelectedPRRefLocCode = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemLocCode")
                lblQtyReceive.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty")
                lblIDQtyReceive.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty"), 5)
                txtQty.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("OutStandingQty")
                txtCost.Text = FormatNumber(objPOItemDs.Tables(0).Rows(intCnt).Item("Cost"), 2, -2, -2, 0)
                strItemType = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemType")
                hidItemType.Value = objPOItemDs.Tables(0).Rows(intCnt).Item("ItemType")
                lblPBBKB.Text = objPOItemDs.Tables(0).Rows(intCnt).Item("PBBKB")

                BindLoc("")
                BindAccount(objPOItemDs.Tables(0).Rows(intCnt).Item("AccCode"))
                BindPreBlock(objPOItemDs.Tables(0).Rows(intCnt).Item("AccCode"), objPOItemDs.Tables(0).Rows(intCnt).Item("BlkCode"))
                BindBlock(objPOItemDs.Tables(0).Rows(intCnt).Item("AccCode"), objPOItemDs.Tables(0).Rows(intCnt).Item("BlkCode"))
                BindVehicle(objPOItemDs.Tables(0).Rows(intCnt).Item("AccCode"), objPOItemDs.Tables(0).Rows(intCnt).Item("VehCode"))
                If Not Trim(objPOItemDs.Tables(0).Rows(0).Item("AccPurpose")) = "" Then
                    If Trim(objPOItemDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
                        BindVehicleExpense(True, objPOItemDs.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
                    ElseIf Trim(objPOItemDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Or _
                           Trim(objPOItemDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
                        BindVehicleExpense(False, objPOItemDs.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
                    End If
                End If
            End If
        Next intCnt

        dr = objPOItemDs.Tables(0).NewRow()
        dr("GoodsRcvLnId") = ""
        dr("Description") = "Please select Item Code"
        objPOItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = objPOItemDs.Tables(0)
        ddlItemCode.DataValueField = "GoodsRcvLnId"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex

        FindAcc.Visible = False
        txtAccCode.Enabled = False
        ddlBlock.Enabled = False
        ddlPreBlock.Enabled = False
        ddlChargeLevel.Enabled = False
        ddlVehCode.Enabled = False
        ddlVehExpCode.Enabled = False
    End Sub

    Sub BindItemCode(ByVal pv_strPOId As String)
        Dim strOpCd As String = "PU_CLSTRX_DA_INVITEM_GET"
        Dim objItemDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParam = objINSetup.EnumInventoryItemType.DirectCharge & "|" & strLocation & "|" & _
                   objINSetup.EnumStockItemStatus.Active & "|itm.ItemCode|" & _
                   lblDispAdvId.Text

        Try
            intErrNo = objPU.mtdGetDADCItem(strOpCd, strParam, objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_Item&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        lblSelectedItemCode.Text = ""
        lblSelectedGRID.Text = ""
        lblQtyReceive.Text = ""
        txtQty.Text = ""
        txtCost.Text = ""

        For intCnt = 0 To objItemDs.Tables(0).Rows.Count - 1
            objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("ItemCode"))
            objItemDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("LocCode"))

            objItemDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("ItemCode")) & ", (" & _
                                                                   Trim(objItemDs.Tables(0).Rows(intCnt).Item("Description")) & "), " & _
                                                                   "Rp. " & objGlobal.GetIDDecimalSeparator(objItemDs.Tables(0).Rows(intCnt).Item("LatestCost")) & ", " & _
                                                                   objGlobal.GetIDDecimalSeparator_FreeDigit(objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"), 5) & ", " & _
                                                                   Trim(objItemDs.Tables(0).Rows(intCnt).Item("UOMCode"))
            objItemDs.Tables(0).Rows(intCnt).Item("UOMCode") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("UOMCode"))
            objItemDs.Tables(0).Rows(intCnt).Item("LatestCost") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("LatestCost"))
            objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand") = Trim(objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"))

            If objItemDs.Tables(0).Rows(intCnt).Item("ItemCode") = strSelectedItemCode Then
                intSelectedIndex = intCnt + 1

                lblSelectedItemCode.Text = Trim(objItemDs.Tables(0).Rows(intCnt).Item("ItemCode"))
                lblQtyReceive.Text = Trim(objItemDs.Tables(0).Rows(intCnt).Item("QtyOnHand"))
                strItemType = Trim(objItemDs.Tables(0).Rows(intCnt).Item("ItemType"))
                hidItemType.Value = objItemDs.Tables(0).Rows(intCnt).Item("ItemType")
            End If
        Next intCnt

        If lblDispAdvType.Text <> objINSetup.EnumInventoryItemType.DirectCharge Then
            objItemDs.Tables(0).Clear()
        End If

        dr = objItemDs.Tables(0).NewRow()
        dr("ItemCode") = ""
        dr("Description") = "Please select Item Code"
        objItemDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlItemCode.DataSource = objItemDs.Tables(0)
        ddlItemCode.DataValueField = "ItemCode"
        ddlItemCode.DataTextField = "Description"
        ddlItemCode.DataBind()
        ddlItemCode.SelectedIndex = intSelectedIndex

        txtAccCode.Enabled = True
        ddlBlock.Enabled = True
        ddlPreBlock.Enabled = True
        ddlChargeLevel.Enabled = True
        ddlVehCode.Enabled = True
        ddlVehExpCode.Enabled = True
    End Sub

    Sub ItemIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strSelectedPOId = ddlPOId.SelectedItem.Value
        strSelectedGoodsRcvLnId = ddlItemCode.SelectedItem.Value
        strSelectedItemCode = ddlItemCode.SelectedItem.Value
        If strSelectedPOId = "" Then
            BindItemCode("")
        Else
            BindPOItem(strSelectedPOId)
        End If
        txtQty.Enabled = True
        txtCost.Enabled = False
    End Sub

    Sub BindLoc(ByVal pv_strPRId As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strPRRefLocCode = IIf(pv_strPRId = "", "", strSelectedPRRefLocCode)
        strParam = strPRRefLocCode & "|" & objAdminSetup.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            With objLocDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = strSelectedPRRefLocCode Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = lblSelectListLoc.Text & lblLocation.Text
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPRRefLocCode.DataSource = objLocDs.Tables(0)
        ddlPRRefLocCode.DataValueField = "LocCode"
        ddlPRRefLocCode.DataTextField = "Description"
        ddlPRRefLocCode.DataBind()
        ddlPRRefLocCode.SelectedIndex = intSelectedIndex
        strSelectedPRRefLocCode = ddlPRRefLocCode.SelectedItem.Value
    End Sub

    Sub BindAccount(ByVal pv_strAccCode As String)
        'Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        'Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        'Dim intErrNo As Integer
        'Dim intCnt As Integer
        'Dim dr As DataRow
        'Dim intSelectedIndex As Integer = 0

        'strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        'Try
        '    intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
        '                                           strParam, _
        '                                           objGLSetup.EnumGLMasterType.AccountCode, _
        '                                           objAccDs)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        'End Try

        'For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
        '    If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
        '        intSelectedIndex = intCnt + 1
        '        Exit For
        '    End If
        'Next

        'dr = objAccDs.Tables(0).NewRow()
        'dr("AccCode") = ""
        'dr("_Description") = lblPleaseSelect.Text & lblAccount.Text & lblCode.Text
        'objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        'ddlAccount.DataSource = objAccDs.Tables(0)
        'ddlAccount.DataValueField = "AccCode"
        'ddlAccount.DataTextField = "_Description"
        'ddlAccount.DataBind()
        'ddlAccount.SelectedIndex = intSelectedIndex
        'ddlAccount.AutoPostBack = True
    End Sub

    Sub onSelect_Account(ByVal Sender As Object, ByVal E As EventArgs)
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim intNurseryInd As Integer

        GetAccountDetails(txtAccCode.Text, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindPreBlock(txtAccCode.Text, ddlPreBlock.SelectedItem.Value)
                BindBlock(txtAccCode.Text, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            Else
                BindPreBlock("", ddlPreBlock.SelectedItem.Value)
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            End If
            If blnIsVehicleRequire Then
                BindVehicle(txtAccCode.Text, ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(False, ddlVehExpCode.SelectedItem.Value)
            End If
            If blnIsOthers Then
                lblVehicleOption.Text = True
                BindVehicle("%", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(False, ddlVehExpCode.SelectedItem.Value)
            Else
                lblVehicleOption.Text = False
            End If
        Else
            If blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                BindPreBlock(txtAccCode.Text, ddlPreBlock.SelectedItem.Value)
                BindBlock(txtAccCode.Text, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            ElseIf blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.No Then
                BindPreBlock("", ddlPreBlock.SelectedItem.Value)
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
                BindVehicleExpense(True, ddlVehExpCode.SelectedItem.Value)
            End If
        End If

    End Sub

    Sub OnSelectItem(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim CPCellCode As TableCell = e.Item.Cells(0)
        Dim CPCellName As TableCell = e.Item.Cells(1)
        Dim CPCellTerm As TableCell = e.Item.Cells(2)
        Dim CPCellPPN As TableCell = e.Item.Cells(3)

        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If

        txtSuppCode.Text = CPCellCode.Text.Trim
        txtSuppName.Text = CPCellName.Text.Trim
        BindPO(txtSuppCode.Text)

        txtQty.Text = 0
        txtCost.Text = 0

        dgLine.DataSource = Nothing
        dgLine.Visible = False
    End Sub


    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_IsBalanceSheet As Boolean, _
                          ByRef pr_IsBlockRequire As Boolean, _
                          ByRef pr_IsVehicleRequire As Boolean, _
                          ByRef pr_IsOthers As Boolean, _
                          ByRef pr_strNurseryInd As Integer)

        Dim _objAccDs As New Object
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            pr_IsBalanceSheet = False
            pr_IsBlockRequire = False
            pr_IsVehicleRequire = False
            pr_IsOthers = False
            pr_strNurseryInd = False

            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_GET_ACCOUNT_DETAILS&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType")) = objGLSetup.EnumAccountType.BalanceSheet Then
                pr_IsBalanceSheet = True
                If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd")) = objGLSetup.EnumNurseryAccount.Yes Then
                    pr_strNurseryInd = objGLSetup.EnumNurseryAccount.Yes
                End If
            End If
            If CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
                pr_IsBlockRequire = True
            ElseIf CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                pr_IsVehicleRequire = True
            ElseIf CInt(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
                pr_IsBlockRequire = True
                pr_IsOthers = True
            End If
        End If
    End Sub

    Sub BindPreBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strOpCd = "GL_CLSSETUP_ACCOUNT_PREBLOCK_GET"
        intSelectedIndex = 0
        Try
            strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
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
        dr("Description") = lblPleaseSelect.Text & PreBlockTag & lblCode.Text

        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlPreBlock.DataSource = objBlkDs.Tables(0)
        ddlPreBlock.DataValueField = "BlkCode"
        ddlPreBlock.DataTextField = "Description"
        ddlPreBlock.DataBind()
        ddlPreBlock.SelectedIndex = intSelectedIndex

        If Not objBlkDs Is Nothing Then
            objBlkDs = Nothing
        End If
    End Sub

    Sub BindBlock(ByVal pv_strAccCode As String, ByVal pv_strBlkCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
                strOpCd = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumBlockStatus.Active
            Else
                strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active
            End If
            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblPleaseSelect.Text & lblBlock.Text & lblCode.Text
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "_Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicle(ByVal pv_strAccCode As String, ByVal pv_strVehCode As String)
        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            strOpCd = "GL_CLSSETUP_VEH_LIST_GET"
            strParam = "|AccCode = '" & pv_strAccCode & "' AND LocCode = '" & Session("SS_LOCATION") & "' AND Status = '" & objGLSetup.EnumVehicleStatus.Active & "'"
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.Vehicle, _
                                                   objVehDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode"))
            objVehDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objVehDs.Tables(0).Rows(intCnt).Item("VehCode")) & " (" & Trim(objVehDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = Trim(pv_strVehCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objVehDs.Tables(0).NewRow()
        dr("VehCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblVehicle.Text
        objVehDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehCode.DataSource = objVehDs.Tables(0)
        ddlVehCode.DataValueField = "VehCode"
        ddlVehCode.DataTextField = "Description"
        ddlVehCode.DataBind()
        ddlVehCode.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindVehicleExpense(ByVal pv_IsBlankList As Boolean, ByVal pv_strVehExpCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_VEHEXPENSE_LIST_GET"
        Dim strParam As String = "Order By VehExpenseCode ASC| And Veh.Status = '" & objGLSetup.EnumVehExpenseStatus.Active & "' "
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            If pv_IsBlankList Then
                strParam += "And Veh.VehExpensecode = ''"
            End If
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehicleExpense, _
                                                   objVehExpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEHEXPENSE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objVehExpDs.Tables(0).Rows.Count - 1
            objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode"))
            objVehExpDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode")) & " (" & Trim(objVehExpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objVehExpDs.Tables(0).Rows(intCnt).Item("VehExpenseCode") = Trim(pv_strVehExpCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objVehExpDs.Tables(0).NewRow()
        dr("VehExpenseCode") = ""
        dr("Description") = lblPleaseSelect.Text & lblVehExpense.Text
        objVehExpDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehExpCode.DataSource = objVehExpDs.Tables(0)
        ddlVehExpCode.DataValueField = "VehExpenseCode"
        ddlVehExpCode.DataTextField = "Description"
        ddlVehExpCode.DataBind()
        ddlVehExpCode.SelectedIndex = intSelectedIndex
    End Sub

    Protected Function LoadData(ByVal pName As String) As DataSet
        Dim strOpCode As String = "IN_CLSTRX_SUPPLIER_SEARCH"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strSortExp As String
        Dim objItemDs As New Object()

        strSortExp = " ORDER BY SupplierCode ASC"
        strParamName = "SEARCH|LOCCODE|SORTEXP"
        strParamValue = "%" & pName & "%||" & strSortExp

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objItemDs

    End Function

    Sub BindSup1(ByVal pName As String)
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData(pName)
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)

        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLine.DataBind()

    End Sub

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
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

    Sub BtnSup1Close_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.DataSource = Nothing
        dgLine.Visible = False
    End Sub

    Sub BtnSup1Find_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtSuppName.Text.Trim = "" Then
            lblSuppCode.Visible = True
            Exit Sub
        Else
            lblSuppCode.Visible = False
        End If

        dgLine.Visible = True
        BindSup1(txtSuppName.Text)
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCodeGLSubBlkByBlk As String = "GL_CLSSETUP_SUBBLOCK_BY_BLOCK_GET"
        Dim strParamList As String
        Dim strOpCd_AddDA As String = "PU_CLSTRX_DA_ADD"
        Dim strOpCd_AddDALn As String = "PU_CLSTRX_DA_LINE_ADD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_GetDALn As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim objDAId As Object
        Dim objDALnId As Object
        Dim strSuppCode As String = txtSuppCode.Text
        Dim strItemCode As String = ""
        Dim strPRRefLoc As String = ddlPRRefLocCode.SelectedItem.Value
        Dim strToLocCode as string = ddlToLocCode.SelectedItem.Value
        Dim strParam As String = ""
        Dim strParamLn As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strDAId As String
        Dim intPOIDInd As Integer
        Dim intItemCode As Integer
        Dim dblAmount As Double
        Dim dblTotalAmount As Double
        Dim intErrorCheck As Integer
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim strDALnID As String = ""
        Dim arrDALnID As Array
        Dim intNurseryInd As Integer
    
        Dim strPreBlk As String = Request.Form("ddlPreBlock") 
        Dim strBlk As String = Request.Form("ddlBlock")

        Dim strOppCd_RDP As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"
        Dim strOppCd_GetID As String = "IN_CLSTRX_PURREQ_GETID"
        Dim strOppCd As String = "IN_CLSTRX_PURREQ_MOVEID"
        Dim strOppCd_Back As String = "IN_CLSTRX_PURREQ_BACKID"

        Dim strDeptCode As String = ddlDAIssue.SelectedItem.Value
        Dim strNewIDFormat As String
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "DA"
        Dim strHistYear As String = ""
        Dim strRDP As String
        Dim objCompDs As New Object
        Dim blnIsDetail As Boolean = True
        Dim objPRDs As Object
        Dim strAddNote As String = Trim(txtAddNote.Text)
        Dim strTransporter As String = Trim(txtTransporter.Text)
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim strDateETD As String = Date_Validation(txtETDLoc.Text, False)
        Dim strDateETALoc As String = Date_Validation(txtETALoc.Text, False)
        Dim strDateETASecLoc As String = Date_Validation(txtETAToLoc.Text, False)

        Dim indDate As String = ""
        Dim strPOIssued As String

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If
        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        If lstStorage.SelectedItem.Value = "" Then
            lblstoragemsg.Visible = True
            Exit Sub
        else
            lblstoragemsg.Visible = False
        End If

        If strToLocCode = "" Then
            lblToLocCode.Visible = True
            Exit Sub
        End If

        If DDLDispCat.SelectedIndex = 0 Then
            lblSuppCode.Visible = True
            lblSuppCode.Text = "Please Select Dispatch Category"
            DDLDispCat.Focus()
            Exit Sub
        Else
            lblSuppCode.Visible = False
        End If

        If txtPIC.Text = "" Then
            lblSuppCode.Visible = True
            lblSuppCode.Text = "Please Input PIC"
            txtPIC.Focus()
            Exit Sub
        Else
            lblSuppCode.Visible = False
        End If

        If ddlItemCode.SelectedItem.Value.Trim = "" Then
            lblErrItemCode.Visible = True
            Exit Sub
        Else
            arrDALnID = Split(ddlItemCode.SelectedItem.Value.Trim, ITEM_PART_SEPERATOR)
            If UBound(arrDALnID) <> -1 Then
                strDALnID = arrDALnID(0)
            End If
        End If

        strDAId = IIf(lblDispAdvId.Text = "", "", lblDispAdvId.Text)

        GetAccountDetails(txtAccCode.Text, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)

        If blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
            If ddlChargeLevel.SelectedIndex = 0 Then
                If strPreBlk = "" Then
                    lblPreBlockErr.Visible = True
                    Exit Sub
                End If
            Else
                If strBlk = "" Then
                    lblErrBlock.Visible = True
                    Exit Sub
                End If
            End If
        End If

        If Not blnIsBalanceSheet Then
            If hidItemType.Value = objINSetup.EnumInventoryItemType.DirectCharge Then
                If txtAccCode.Text = "" Then
                    lblErrAccount.Visible = True
                    Exit Sub
                ElseIf ddlBlock.SelectedItem.Value = "" And ddlChargeLevel.SelectedIndex = 1 And blnIsBlockRequire = True Then
                    lblErrBlock.Visible = True
                    Exit Sub
                ElseIf ddlPreBlock.SelectedItem.Value = "" And ddlChargeLevel.SelectedIndex = 0 And blnIsBlockRequire = True Then
                    lblPreBlockErr.Visible = True
                    Exit Sub
                ElseIf ddlVehCode.SelectedItem.Value = "" And blnIsVehicleRequire = True Then
                    lblErrVehicle.Visible = True
                    Exit Sub
                ElseIf ddlVehExpCode.SelectedItem.Value = "" And blnIsVehicleRequire = True Then
                    lblErrVehExp.Visible = True
                    Exit Sub
                ElseIf ddlVehCode.SelectedItem.Value <> "" And ddlVehExpCode.SelectedItem.Value = "" And lblVehicleOption.Text = True Then
                    lblErrVehExp.Visible = True
                    Exit Sub
                ElseIf ddlVehCode.SelectedItem.Value = "" And ddlVehExpCode.SelectedItem.Value <> "" And lblVehicleOption.Text = True Then
                    lblErrVehicle.Visible = True
                    Exit Sub
                End If
            End If
        End If


        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
        End If

        strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'DA'" & "|"
        Try
            intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GetID, _
                                                   strParam, _
                                                   objIN.EnumPurReqDocType.StockPR, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strLocation, _
                                                   objPRDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try


        If objPRDs.Tables(0).Rows.Count > 0 Then
            strNewYear = ""
        Else
            strHistYear = Right(strLastPhyYear, 2)
            strNewYear = "1"
        End If


        If Trim(strDeptCode) = "0" Then
            strDeptCode = "JKT"
        End If
        If Trim(strDeptCode) = "1" Then
            strDeptCode = "PKU"
        End If
        If Trim(strDeptCode) = "2" Then
            strDeptCode = "LMP"
        End If
        If Trim(strDeptCode) = "3" Then
            strDeptCode = "PLM"
        End If
        If Trim(strDeptCode) = "4" Then
            strDeptCode = "BKL"
        End If
        If Trim(strDeptCode) = "5" Then
            strDeptCode = "LOK"
        End If

        'If Month(strDate) < strAccMonth And Year(strDate) <= strAccYear Then
        '    lblDate.Visible = True
        '    lblDate.Text = "Invalid transaction date."
        '    Exit Sub
        'End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)

        'If Session("SS_LOCLEVEL") <> objAdminLoc.EnumLocLevel.Perwakilan Then
        '    strPOIssued = "R"
        '    strNewIDFormat = "BPB" & "/" & strCompany & "/" & strToLocCode & "/" & Trim(lblDispAdvType.Text) & Trim(strPOIssued) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'ElseIf Session("SS_LOCLEVEL") <> objAdminLoc.EnumLocLevel.HQ Then
        '    strNewIDFormat = "BPB" & "/" & strCompany & "/" & strToLocCode & "/" & Trim(lblDispAdvType.Text) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End If

        Dim nDispCat As String = ""

        If DDLDispCat.selectedIndex = 1 Then
            nDispCat = "D"
        ElseIf DDLDispCat.selectedIndex = 2 Then
            nDispCat = "L"
        ElseIf DDLDispCat.SelectedIndex = 3 Then
            nDispCat = "U"
        ElseIf DDLDispCat.SelectedIndex = 4 Then
            nDispCat = "H"
        End If

        If lblDispAdvType.Text.Trim = "1" Then
            strNewIDFormat = "SPB" & "/" & strCompany & "/" & strLocation & "/" & Trim(nDispCat) & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"
        Else
            strNewIDFormat = "SPK" & "/" & strCompany & "/" & strLocation & "/" & Trim(nDispCat) & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/" & strAccYear & "/"
        End If

        If ddlPOId.SelectedItem.Value <> "" Then
            If CDbl(txtQty.Text) > 0 And (CDbl(txtQty.Text) <= CDbl(lblQtyReceive.Text)) Then
                lblErrQty.Visible = False
                If CDbl(lblPBBKB.Text) > 0 Then
                    dblAmount = FormatNumber((CDbl(txtQty.Text) * CDbl(txtCost.Text)) + (CDbl(txtQty.Text) * CDbl(txtCost.Text)) * CDbl(lblPBBKB.Text) / 100, 0)
                    dblTotalAmount += dblAmount
                Else
                    dblAmount = FormatNumber((CDbl(txtQty.Text) * CDbl(txtCost.Text)), 0)
                    dblTotalAmount += dblAmount
                End If


                strParamLn = strDAId & "|" & _
                             strDALnID & "|" & _
                             txtPRRefId.Text & "|" & _
                             strPRRefLoc & "|" & _
                             lblSelectedItemCode.Text & "|" & _
                             txtQty.Text & "|" & _
                             txtCost.Text & "|" & _
                             dblAmount & "|" & _
                             txtAccCode.Text & "|" & _
                             IIf(ddlChargeLevel.SelectedIndex = 0, ddlPreBlock.SelectedItem.Value, ddlBlock.SelectedItem.Value) & "|" & _
                             ddlVehCode.SelectedItem.Value & "|" & _
                             ddlVehExpCode.SelectedItem.Value & "|" & _
                             lblSelectedGRID.Text & "|" & _
                             strNewIDFormat & "|" & _
                             strNewYear & "|" & _
                             strTranPrefix & "|" & _
                             strHistYear & "|" & _
                             Right(strPhyYear, 2) & "|" & _
                             Replace(strAddNote, "'", "''")

            Else
                lblErrQty.Visible = True
                Exit Sub
            End If
        Else
            If CDbl(txtQty.Text) > 0 Then
                lblErrQty.Visible = False
                dblAmount = FormatNumber((CDbl(txtQty.Text) * CDbl(txtCost.Text)), 0)
                dblTotalAmount += dblAmount

                strParamLn = strDAId & "||" & _
                             txtPRRefId.Text & "|" & _
                             strPRRefLoc & "|" & _
                             lblSelectedItemCode.Text & "|" & _
                             txtQty.Text & "|" & _
                             txtCost.Text & "|" & _
                             dblAmount & "|" & _
                             txtAccCode.Text & "|" & _
                             IIf(ddlChargeLevel.SelectedIndex = 0, ddlPreBlock.SelectedItem.Value, ddlBlock.SelectedItem.Value) & "|" & _
                             ddlVehCode.SelectedItem.Value & "|" & _
                             ddlVehExpCode.SelectedItem.Value & "||" & _
                             strNewIDFormat & "|" & _
                             strNewYear & "|" & _
                             strTranPrefix & "|" & _
                             strHistYear & "|" & _
                             Right(strPhyYear, 2) & "|" & _
                             strAddNote
            Else
                lblErrQty.Visible = True
                Exit Sub
            End If
        End If

        strParam = strDAId & "|" & _
                   strLocation & "|" & _
                   ddlToLocCode.SelectedItem.Value & "|" & _
                   lblDispAdvType.Text & "|" & _
                   txtSuppCode.Text & "|" & _
                   dblTotalAmount & "|" & _
                   txtRemark.Text & "|" & _
                   objPU.EnumDAStatus.Active & "|" & _
                   strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                   ddlDAIssue.SelectedItem.Value & "|" & _
                   ddlInventoryBin.SelectedItem.Value & "|" & _
                   strDate & "|" & _
                   strTransporter & "|" & _
                   txtPIC.Text & "|" & _
                   DDLDispCat.SelectedItem.Value & "|" & _
                   strDateETD & "|" & _
                   strDateETALoc & "|" & _
                   strDateETASecLoc & "|" & _
                   txtETALocDeskripsi.Text & "|" & _
                   txtAddress.Text

        Try
            If ddlChargeLevel.SelectedIndex = 0 And ddlPreBlock.Enabled = True And RowPreBlk.Visible = True Then
                strParamList = Session("SS_LOCATION") & "|" & _
                                       txtAccCode.Text.Trim & "|" & _
                                       ddlPreBlock.SelectedItem.Value.Trim & "|" & _
                                       objGLSetup.EnumBlockStatus.Active
                intErrNo = objPU.mtdAddDALnByBlock(strOpCodeGLSubBlkByBlk, _
                                                   ddlPreBlock.SelectedItem.Value.Trim & "|" & txtAccCode.Text, _
                                                   strOpCd_AddDA, _
                                                   strOpCd_AddDALn, _
                                                   strOpCd_GetItem, _
                                                   strOpCd_UpdItem, _
                                                   strOpCd_GetDALn, _
                                                   strOpCd_UpdDA, _
                                                   strOppCd, _
                                                   strOppCd_Back, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strParam, _
                                                   strParamLn, _
                                                   intErrorCheck, _
                                                   strLocType, _
                                                   objDAId, _
                                                   objDALnId, _
                                                   objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdvice), _
                                                   objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdviceLn))

            Else
                intErrNo = objPU.mtdAddDALn(strOpCd_AddDA, _
                                            strOpCd_AddDALn, _
                                            strOpCd_GetItem, _
                                            strOpCd_UpdItem, _
                                            strOpCd_GetDALn, _
                                            strOpCd_UpdDA, _
                                            strOppCd, _
                                            strOppCd_Back, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            strParamLn, _
                                            intErrorCheck, _
                                            objDAId, _
                                            objDALnId, _
                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdvice), _
                                            objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdviceLn))
            End If
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_NEW_DALN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
            End If
        End Try

        strSelectedDispAdvId = objDAId

  '''---------UPDATE STORAGE SELECT - SUPAYA TIDAK MENGUBAH DLL
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim strParamName As string
        Dim strParamValue As string

        sSQLKriteria = "UPDATE PU_DISPADVLN SET StorageCode='" & lstStorage.SelectedItem.Value & "' Where DispAdvId='" & strSelectedDispAdvId & "'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_NEW_GRLN&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList.aspx")
        End Try

        ''---------------------

        
        strSelectedPOId = ddlPOId.SelectedItem.Value
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
        BindPO(strSelectedSuppCode)
        BindAccount("")
        BindPreBlock("", "")
        BindBlock("", "")
        BindVehicle("", "")
        BindVehicleExpense(True, "")
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_DelDALn As String = "PU_CLSTRX_DA_LINE_DEL"
        Dim strOpCd_GetDALn As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdGRLn As String = "PU_CLSTRX_DA_GR_LINE_DETAILS_UPD"
        Dim strParam As String = ""
        Dim DispAdvLnIdCell As TableCell = E.Item.Cells(0)
        Dim GoodsRcvLnIdCell As TableCell = E.Item.Cells(1)
        Dim ItemCell As TableCell = E.Item.Cells(2)
        Dim QtyDispCell As TableCell = E.Item.Cells(3)
        Dim strDispAdvLnId As String = DispAdvLnIdCell.Text
        Dim strGoodsRcvLnId As String = GoodsRcvLnIdCell.Text
        Dim strItemCode As String = ItemCell.Text
        Dim strQtyDisp As String = QtyDispCell.Text
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If strGoodsRcvLnId = "&nbsp;" Then
            strGoodsRcvLnId = ""
        End If

        'UPDATE PU_GOODSRCVLN
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "DISPADVID|GOODSRCVLNID|ITEMCODE|DISPQTY"

        strParamValue = lblDispAdvId.Text & "|" & _
                   strGoodsRcvLnId & "|" & _
                   strItemCode & "|" & _
                   strQtyDisp

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd_UpdGRLn, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try



        strParam = lblDispAdvId.Text & "|" & _
                   strDispAdvLnId & "|" & _
                   strGoodsRcvLnId & "|" & _
                   strItemCode & "|" & _
                   strQtyDisp

        Try
            intErrNo = objPU.mtdDelDALn(strOpCd_DelDALn, _
                                        strOpCd_GetDALn, _
                                        strOpCd_UpdDA, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_DALN&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_DAList.aspx")
            End If
        End Try


        



        strSelectedDispAdvId = lblDispAdvId.Text
        strSelectedPOId = ddlPOId.SelectedItem.Value
        onLoad_DisplayLn(strSelectedDispAdvId)
        strSelectedSuppCode = txtSuppCode.Text
        BindPO(strSelectedSuppCode)
        BindAccount("")
        BindPreBlock("", "")
        BindBlock("", "")
        BindVehicle("", "")
        BindVehicleExpense(True, "")
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_AddDA As String = "PU_CLSTRX_DA_ADD"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim objDAId As Object
        Dim strToLocCode As String = ddlToLocCode.SelectedItem.Value
        Dim strDispAdvType As String = lblDispAdvType.Text
        Dim strSuppCode As String = txtSuppCode.Text

        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer

        Dim strOppCd_RDP As String = "ADMIN_CLSCOMP_COMPANY_DETAILS_GET"
        Dim strOppCd_GetID As String = "IN_CLSTRX_PURREQ_GETID"
        Dim strOppCd As String = "IN_CLSTRX_PURREQ_MOVEID"
        Dim strOppCd_Back As String = "IN_CLSTRX_PURREQ_BACKID"

        Dim strDeptCode As String = ddlDAIssue.SelectedItem.Value
        Dim strNewIDFormat As String
        Dim strNewYear As String = ""
        Dim strTranPrefix As String = "DA"
        Dim strHistYear As String = ""
        Dim strRDP As String
        Dim objCompDs As New Object
        Dim blnIsDetail As Boolean = True
        Dim objPRDs As Object
        Dim strTransporter As String = Trim(txtTransporter.Text)
        Dim intStatus As Integer
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim strDateETD As String = Date_Validation(txtETDLoc.Text, False)
        Dim strDateETALoc As String = Date_Validation(txtETALoc.Text, False)
        Dim strDateETASecLoc As String = Date_Validation(txtETAToLoc.Text, False)
        Dim indDate As String = ""

        If Len(lblDispAdvId.Text) = 0 Then
            UserMsgBox(Me, "No Record Found !!!")
            Exit Sub
        End If

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        If CheckDate(txtETDLoc.Text.Trim(), indDate) = False Then
            lblETDLoc.Visible = True
            lblETDLoc.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        If CheckDate(txtETALoc.Text.Trim(), indDate) = False Then
            lblETALoc.Visible = True
            lblETALoc.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        If CheckDate(txtETAToLoc.Text.Trim(), indDate) = False Then
            lblETAToLoc.Visible = True
            lblETAToLoc.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If


        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        'If strSelectedDispAdvId = "" Then
        '    Exit Sub
        'End If

        'If strToLocCode = "" Then
        '    lblToLocCode.Visible = True
        '    Exit Sub
        'End If

        If DDLDispCat.SelectedItem.Value = "0" Then
            lblSuppCode.Visible = True
            lblSuppCode.Text = "Please Select Dispatch Category"
            DDLDispCat.Focus()
            Exit Sub
        Else
            lblSuppCode.Visible = False
        End If

        If txtPIC.Text = "" Then
            lblSuppCode.Visible = True
            lblSuppCode.Text = "Please Input PIC"
            txtPIC.Focus()
            Exit Sub
        Else
            lblSuppCode.Visible = False
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If

        If Len(strPhyMonth) = 1 Then
            strPhyMonth = "0" & strPhyMonth
        End If


        strParam = "where phyyear = '" & Right(Trim(strPhyYear), 2) & "' and tran_prefix = 'DA'" & "|"
        Try
            intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GetID, _
                                                   strParam, _
                                                   objIN.EnumPurReqDocType.StockPR, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strLocation, _
                                                   objPRDs)


        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_DISPLAYPR&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If objPRDs.Tables(0).Rows.Count > 0 Then
            strNewYear = ""
        Else
            strHistYear = Right(strLastPhyYear, 2)
            strNewYear = "1"
        End If

        'If Trim(strDeptCode) = "0" Then
        '    strDeptCode = "JKT"
        'End If
        'If Trim(strDeptCode) = "1" Then
        '    strDeptCode = "PKU"
        'End If
        'If Trim(strDeptCode) = "2" Then
        '    strDeptCode = "LMP"
        'End If
        'If Trim(strDeptCode) = "3" Then
        '    strDeptCode = "PLM"
        'End If
        'If Trim(strDeptCode) = "4" Then
        '    strDeptCode = "BKL"
        'End If
        'If Trim(strDeptCode) = "5" Then
        '    strDeptCode = "LOK"
        'End If

        If lblCurrentPeriod.Text <> "" Then
            If Month(strDate) = Mid(lblCurrentPeriod.Text, 5) And Year(strDate) = Mid(lblCurrentPeriod.Text, 1, 4) Then
            Else
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        End If

        strAccYear = Year(strDate)
        strAccMonth = Month(strDate)
        'Select Case strCompany
        '    Case "SAM", "MIL"
        '        strNewIDFormat = "SPBi" & "-" & Left(Trim(strDeptCode), 3) & "/" & strToLocCode & "/" & Right(strAccYear, 2) & "/" & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        '    Case Else
        '        strNewIDFormat = "BPB" & "/" & strCompany & "/" & strToLocCode & "/" & Trim(lblDispAdvType.Text) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"
        'End Select
        'strNewIDFormat = "BPB" & "/" & strCompany & "/" & strToLocCode & "/" & Trim(lblDispAdvType.Text) & "/" & strAccYear & IIf(Len(Trim(strAccMonth)) = 1, "0" & strAccMonth, strAccMonth) & "/"

        intStatus = CInt(Trim(lblHidStatus.Text))
        If intStatus = objPU.EnumDAStatus.Active Then
            strParam = lblDispAdvId.Text & "|" & _
                  strLocation & "|" & _
                  strToLocCode & "|" & _
                  strDispAdvType & "|" & _
                  strSuppCode & "|" & _
                  strRemark & "|" & _
                  objPU.EnumDAStatus.Active & "|" & _
                  strNewIDFormat & "|" & strNewYear & "|" & strTranPrefix & "|" & strHistYear & "|" & Right(strPhyYear, 2) & "|" & _
                  ddlDAIssue.SelectedItem.Value & "|" & _
                  ddlInventoryBin.SelectedItem.Value & "|" & _
                  strTransporter & "|" & _
                  txtPIC.Text & "|" & _
                  DDLDispCat.SelectedItem.Value & "|" & _
                  strDateETD & "|" & _
                  strDateETALoc & "|" & _
                  strDateETASecLoc & "|" & _
                  txtETALocDeskripsi.Text & "|" & _
                  txtAddress.Text & "|" & _
                  strDate

        ElseIf intStatus = objPU.EnumDAStatus.Confirmed Then
            strParam = lblDispAdvId.Text & "|" & _
                  strLocation & "|" & _
                  strToLocCode & "|||" & _
                  strRemark & "|" & _
                  objPU.EnumDAStatus.Confirmed & "||||||" & _
                  ddlDAIssue.SelectedItem.Value & "|" & _
                  ddlInventoryBin.SelectedItem.Value & "|" & _
                  strTransporter & "|" & _
                  txtPIC.Text & "|" & _
                  DDLDispCat.SelectedItem.Value & "|" & _
                  strDateETD & "|" & _
                  strDateETALoc & "|" & _
                  strDateETASecLoc & "|" & _
                  txtETALocDeskripsi.Text & "|" & _
                  txtAddress.Text

            '"2000-01-01" & "|" & _
            '"2000-01-01" & "|" & _
            '"2000-01-01" & "|" & _
        End If

        Try

            intErrNo = objPU.mtdUpdDA(strOpCd_AddDA, _
                                      strOpCd_UpdDA, _
                                      strOppCd, _
                                      strOppCd_Back, _
                                      strCompany, _
                                      strLocation, _
                                      strUserId, _
                                      strAccMonth, _
                                      strAccYear, _
                                      strParam, _
                                      objDAId, _
                                      objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdvice))
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_SAVE_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        If Len(lblDispAdvId.Text) > 0 Then
            UserMsgBox(Me, "Save Complete !!!")
            Exit Sub
        End If

        strSelectedDispAdvId = IIf(lblDispAdvId.Text = "", objDAId, lblDispAdvId.Text)
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
        BindPO(strSelectedSuppCode)
        BindPOItem(strSelectedDispAdvId)
    End Sub

    Sub btnConfirm_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetDALn As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim strOpCd_UpdGRLn As String = "PU_CLSTRX_GR_LINE_UPD"
        Dim objDAId As Object
        Dim strToLocCode As String = ddlToLocCode.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
		
		Dim strOpCd As String = "PU_CLSTRX_DA_GR_LINE_GET"
		Dim objDALnDs As New Object()
		Dim objOutStandingDs As New Object()
		Dim inCnt As Integer
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        Dim arrParam As Array
        arrParam = Split(lblAccPeriod.Text, "/")
        If Month(strDate) <> arrParam(0) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        ElseIf Year(strDate) <> arrParam(1) Then
            lblDate.Visible = True
            lblDate.Text = "Invalid transaction date."
            Exit Sub
        End If

        If ddlInventoryBin.SelectedItem.Value = 0 Then
            lblInventoryBin.Visible = True
            Exit Sub
        End If


        If txtPIC.Text = "" Then
            lblSuppCode.Visible = True
            lblSuppCode.Text = "Please Input PIC"
            txtPIC.Focus()
            Exit Sub
        Else
            lblSuppCode.Visible = False
        End If

		strParam = TRIM(lblDispAdvId.Text)
        Try
            intErrNo = objPU.mtdGetDALn(strOpCd_GetDALn, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objDALnDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_DALn&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try
		
		For inCnt = 0 To objDALnDs.Tables(0).Rows.Count - 1
			strParam = objDALnDs.Tables(0).Rows(inCnt).Item("POId") & "|" & _
						objDALnDs.Tables(0).Rows(inCnt).Item("ItemCode") & "|" & _
						objPU.EnumGRStatus.Confirmed
			Try
				intErrNo = objPU.mtdGetDAItemOutStandingQty(strOpCd, _
												strCompany, _
												strLocation, _
												strUserId, _
												strAccMonth, _
												strAccYear, _
												strParam, _
												objOutStandingDs)
			Catch Exp As System.Exception
				Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_OUTSTANDING_QTY&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
			End Try

			If objOutStandingDs.Tables(0).Rows.Count > 0 Then
                If objDALnDs.Tables(0).Rows(inCnt).Item("QtyDisp") > objOutStandingDs.Tables(0).Rows(0).Item("OutstandingQty") And (Trim(objDALnDs.Tables(0).Rows(inCnt).Item("GoodsRcvLnID")) = Trim(objOutStandingDs.Tables(0).Rows(0).Item("GoodsRcvLnID"))) Then
                    lblErrOutStandingQty.Visible = True
                    lblErrPOID.Text = objDALnDs.Tables(0).Rows(inCnt).Item("POId") & objDALnDs.Tables(0).Rows(inCnt).Item("ItemCode")
                    lblErrPOID.Visible = True
                    lblOutStandingQtyMsg.Visible = True
                    lblOutStandingQty.Text = objOutStandingDs.Tables(0).Rows(0).Item("OutstandingQty")
                    lblOutStandingQty.Visible = True
                    Exit Sub
                End If
			End If
		Next inCnt
		

        strParam = lblDispAdvId.Text & "|" & _
                   strLocation & "|" & _
                   strToLocCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumDAStatus.Confirmed


        Try
            intErrNo = objPU.mtdUpdDALn(strOpCd_GetDALn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdDA, _
                                        strOpCd_UpdGRLn, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CONFIRM_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
            End If
        End Try

        If intErrorCheck = objPU.EnumPUErrorType.NoError Then
            Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""

            strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

            strParamValue = Trim(strLocation) & _
                            "|" & "PU_DISPADV" & _
                            "|" & "PU_DISPADVLN" & _
                            "|" & "DISPADVID" & _
                            "|" & Trim(lblDispAdvId.Text) & _
                            "|" & "QTYDISP" & _
                            "|" & ddlInventoryBin.SelectedItem.Value & _
                            "|" & "-" & _
                            "|" & Trim(strUserId)

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
            End Try
        End If

        strSelectedDispAdvId = lblDispAdvId.Text
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
        BindPO(strSelectedSuppCode)
        BindPOItem(strSelectedDispAdvId)
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetDALn As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim strOpCd_UpdGRLn As String = "PU_CLSTRX_GR_LINE_UPD"
        Dim objDAId As Object
        Dim strToLocCode As String = ddlToLocCode.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strParam = lblDispAdvId.Text & "|" & _
                   strLocation & "|" & _
                   strToLocCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumDAStatus.Deleted

        Try
            intErrNo = objPU.mtdUpdDALn(strOpCd_GetDALn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdDA, _
                                        strOpCd_UpdGRLn, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DELETE_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
            End If
        End Try

        strSelectedDispAdvId = lblDispAdvId.Text
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
        BindPO(strSelectedSuppCode)
        BindPOItem(strSelectedDispAdvId)
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetDALn As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim strOpCd_UpdGRLn As String = "PU_CLSTRX_GR_LINE_UPD"
        Dim objDAId As Object
        Dim strToLocCode As String = ddlToLocCode.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If
        strParam = lblDispAdvId.Text & "|" & _
                   strLocation & "|" & _
                   strToLocCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumDAStatus.Active

        Try
            intErrNo = objPU.mtdUpdDALn(strOpCd_GetDALn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdDA, _
                                        strOpCd_UpdGRLn, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_UNDELETE_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
            End If
        End Try

        strSelectedDispAdvId = lblDispAdvId.Text
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
        BindPO(strSelectedSuppCode)
        BindPOItem(strSelectedDispAdvId)
    End Sub

    Sub btnCancel_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GetDALn As String = "PU_CLSTRX_DA_LINE_GET"
        Dim strOpCd_GetPRLn As String = "PU_CLSTRX_GR_PR_LINE_GET"
        Dim strOpCd_UpdPRLn As String = "PU_CLSTRX_PR_LINE_UPD"
        Dim strOpCd_GetItem As String = "IN_CLSSETUP_STOCKITEM_LIST_GET"
        Dim strOpCd_UpdItem As String = "PU_CLSTRX_INVITEM_DETAILS_UPD"
        Dim strOpCd_UpdDA As String = "PU_CLSTRX_DA_UPD"
        Dim strOpCd_UpdGRLn As String = "PU_CLSTRX_GR_LINE_UPD"
        Dim objDAId As Object
        Dim strToLocCode As String = ddlToLocCode.SelectedItem.Value
        Dim strRemark As String = txtRemark.Text
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intErrorCheck As Integer
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strParam = lblDispAdvId.Text & "|" & _
                   strLocation & "|" & _
                   strToLocCode & "|" & _
                   strRemark & "|" & _
                   objPU.EnumDAStatus.Cancelled

        Try
            intErrNo = objPU.mtdUpdDALn(strOpCd_GetDALn, _
                                        strOpCd_GetPRLn, _
                                        strOpCd_UpdPRLn, _
                                        strOpCd_GetItem, _
                                        strOpCd_UpdItem, _
                                        strOpCd_UpdDA, _
                                        strOpCd_UpdGRLn, _
                                        strCompany, _
                                        strLocation, _
                                        strUserId, _
                                        strParam, _
                                        intErrorCheck)
            Select Case intErrorCheck
                Case -1
                    lblErrOnHand.Visible = True
                Case -2
                    lblErrOnHold.Visible = True
            End Select
        Catch Exp As System.Exception
            If intErrNo <> -5 Then
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_CANCEL_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
            End If
        End Try

        Dim strOpCode_ItemUpd As String = "IN_CLSTRX_ITEM_DETAIL_UPD"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "LOCCODE|TABLEA|TABLEB|FIELDID|TRXID|FIELDQTY|BIN|SIGN|UPDATEID"

        strParamValue = Trim(strLocation) & _
                        "|" & "PU_DISPADV" & _
                        "|" & "PU_DISPADVLN" & _
                        "|" & "DISPADVID" & _
                        "|" & Trim(lblDispAdvId.Text) & _
                        "|" & "QTYDISP" & _
                        "|" & ddlInventoryBin.SelectedItem.Value & _
                        "|" & "+" & _
                        "|" & Trim(strUserId)

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCode_ItemUpd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        strSelectedDispAdvId = lblDispAdvId.Text
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
        BindPO(strSelectedSuppCode)
        BindPOItem(strSelectedDispAdvId)
    End Sub


    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCodePrint As String = "ADMIN_SHARE_UPD_PRINTDATE"
        Dim strUpdString As String = ""
        Dim strStatus As String
        Dim intStatus As Integer
        Dim strSortLine As String
        Dim strPrintDate As String
        Dim strTable As String
        Dim intErrNo As Integer

        strUpdString = "where DispAdvId = '" & lblDispAdvId.Text & "'"
        strStatus = Trim(lblStatus.Text)
        intStatus = CInt(Trim(lblHidStatus.Text))
        strPrintDate = Trim(lblPrintDate.Text)
        strSortLine = "PU_DISPADVLN.GoodsRcvLnID"
        strTable = "PU_DISPADV"

        If intStatus = objPU.EnumDAStatus.Confirmed Then
            If strPrintDate = "" Then
                Try
                    intErrNo = objAdmShare.mtdUpdPrintDate(strOpCodePrint, _
                                                           strUpdString, _
                                                           strTable, _
                                                           strCompany, _
                                                           strLocation, _
                                                           strUserId)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_DA_DETAILS_UPD_PRINTDATE&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
                End Try
            Else
                strStatus = strStatus & " (re-printed)"
            End If
        End If

        onLoad_Display(lblDispAdvId.Text)
        onLoad_DisplayLn(lblDispAdvId.Text)

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_DADet.aspx?strDispAdvId=" & lblDispAdvId.Text & _
                       "&strPrintDate=" & strPrintDate & _
                       "&strStatus=" & strStatus & _
                       "&strSortLine=" & strSortLine & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_trx_DAList.aspx")
    End Sub

    Sub BindPOIssued(ByVal pv_strPOIssued As String)
        ddlDAIssue.Items.Clear
        ddlDAIssue.Items.Add(New ListItem("Select DA Issued Location", ""))
        ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.JKT), objPU.EnumPOIssued.JKT))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PKU), objPU.EnumPOIssued.PKU))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LMP), objPU.EnumPOIssued.LMP))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PLM), objPU.EnumPOIssued.PLM))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.BKL), objPU.EnumPOIssued.BKL))
        ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.LOK), objPU.EnumPOIssued.LOK))
        ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.MDN), objPU.EnumPOIssued.MDN))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.JMB), objPU.EnumPOIssued.JMB))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PDG), objPU.EnumPOIssued.PDG))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.ACH), objPU.EnumPOIssued.ACH))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PON), objPU.EnumPOIssued.PON))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.SAM), objPU.EnumPOIssued.SAM))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.PLK), objPU.EnumPOIssued.PLK))
        'ddlDAIssue.Items.Add(New ListItem(objPU.mtdGetPOIssued(objPU.EnumPOIssued.BJR), objPU.EnumPOIssued.BJR))

        If Trim(pv_strPOIssued) <> "" Then
            With ddlDAIssue
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strPOIssued)))
            End With
        End If
    End Sub

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        Dim strText = "Please select Inventory Bin"
        
        'ddlInventoryBin.Items.Clear()
        ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.HO), objINSetup.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Central), objINSetup.EnumInventoryBinLevel.Central))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Other), objINSetup.EnumInventoryBinLevel.Other))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinI), objINSetup.EnumInventoryBinLevel.BinI))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinII), objINSetup.EnumInventoryBinLevel.BinII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIII), objINSetup.EnumInventoryBinLevel.BinIII))
        ' ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIV), objINSetup.EnumInventoryBinLevel.BinIV))

        If Not Trim(pv_strInvBin) = "" Then
            With ddlInventoryBin
                .SelectedIndex = .Items.IndexOf(.Items.FindByValue(Trim(pv_strInvBin)))
            End With
        Else
            ddlInventoryBin.SelectedIndex = -1
        End If
    End Sub

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GR_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_DAList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

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

    Sub btnNew_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("PU_trx_DADet.aspx?DAType=" & lblDispAdvType.Text)
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_PurReqLn_DEL As String = "IN_CLSTRX_PURREQLN_DEL"
        Dim strDAID As String = lblDispAdvId.Text.Trim
        Dim UpdButton As LinkButton
        Dim EditAddNoteText As Label
        Dim EditAddNote As TextBox
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        dgDADet.EditItemIndex = CInt(E.Item.ItemIndex)

        EditAddNoteText = dgDADet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAddNote")
        EditAddNoteText.Visible = False
        EditAddNote = dgDADet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstAddNote")
        EditAddNote.Text = EditAddNoteText.Text
        EditAddNote.Visible = True

        UpdButton = dgDADet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Edit")
        UpdButton.Visible = False
        UpdButton = dgDADet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        UpdButton.Visible = True
        UpdButton = dgDADet.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Cancel")
        UpdButton.Visible = True
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd As String = "PU_CLSTRX_DA_LINE_UPD"
        Dim strDAID As String = lblDispAdvId.Text.Trim
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strAddNote As String
        Dim strLnId As String
        Dim EditText As TextBox
        Dim EditItem As Label
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim indDate As String = ""

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        EditText = E.Item.FindControl("lstAddNote")
        strAddNote = EditText.Text
        EditItem = E.Item.FindControl("lblDispAdvLnId")
        strLnId = EditItem.Text

        strParamName = "UPDATESTR"
        strParamValue = "Set AdditionalNote = '" & Trim(strAddNote) & "' " & _
                        "where DispAdvId = '" & strDAID & "' And DispAdvLnId = '" & strLnId & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_DAList.aspx")
        End Try

        strSelectedDispAdvId = lblDispAdvId.Text
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
        BindPO(strSelectedSuppCode)
        BindPOItem(strSelectedDispAdvId)
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd As String = "PU_CLSTRX_DA_LINE_UPD"
        Dim strDAID As String = lblDispAdvId.Text.Trim
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strAddNote As String
        Dim strLnId As String
        Dim EditText As TextBox

        strSelectedDispAdvId = lblDispAdvId.Text
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
        BindPO(strSelectedSuppCode)
        BindPOItem(strSelectedDispAdvId)
    End Sub

    Sub btnEdited_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpcode_Edit As String = "PU_CLSTRX_DA_UPD"
        Dim strDate As String = Date_Validation(txtDispAdvDate.Text, False)
        Dim indDate As String = ""
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer

        If CheckDate(txtDispAdvDate.Text.Trim(), indDate) = False Then
            lblDate.Visible = True
            lblFmt.Visible = True
            lblDate.Text = "<br>Date Entered should be in the format"
            Exit Sub
        End If

        Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblDate.Visible = True
                lblDate.Text = "Invalid transaction date."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod And intLevel < 2 Then
                lblDate.Visible = True
                lblDate.Text = "This period already locked."
                Exit Sub
            End If
        End If

        If intLevel < 3 Then
            UserMsgBox(Me, "Acsess Denied !!!")
            Exit Sub
        End If
     
        strParamName = "STRUPDATE"
        strParamValue = "SET Status='1' Where DispAdvID='" & lblDispAdvId.Text & "'"

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpcode_Edit, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try



        onLoad_Display(lblDispAdvId.Text.Trim)
        onLoad_DisplayLn(lblDispAdvId.Text.Trim)
        BindPO(lblDispAdvId.Text.Trim)
        BindPOItem(lblDispAdvId.Text.Trim)

        txtDispAdvDate.Enabled = True
        btnSelDate.Visible = True
        txtRemark.Enabled = True
        txtRemark.Enabled = True
        txtTransporter.Enabled = True
        btnSave.Visible = True
        btnEdited.Visible = False
    End Sub

    Private Sub lbViewJournal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbViewJournal.Click
        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_JOURNAL_PREDICTION"
        Dim arrPeriod As Array

        arrPeriod = Split(lblAccPeriod.Text, "/")

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID|TRXID"
        strParamValue = strLocation & "|" & arrPeriod(0) & _
                        "|" & arrPeriod(1) & "|" & _
                        Session("SS_USERID") & "|" & Trim(lblDispAdvId.Text)

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DAYEND_PROCESS&errmesg=" & Exp.Message.ToString & "&redirect=")
        End Try

        If dsResult.Tables(0).Rows.Count > 0 Then

            Dim TotalDB As Double
            Dim TotalCR As Double
            Dim intCnt As Integer
            For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
                TotalDB += dsResult.Tables(0).Rows(intCnt).Item("AmountDB")
                TotalCR += dsResult.Tables(0).Rows(intCnt).Item("AmountCR")
            Next
            lblTotalDB.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalDB, 2), 2)
            lblTotalCR.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TotalCR, 2), 2)

            dgViewJournal.DataSource = Nothing
            dgViewJournal.DataSource = dsResult.Tables(0)
            dgViewJournal.DataBind()

            lblTotalDB.Visible = True
            lblTotalCR.Visible = True
            lblTotalViewJournal.Visible = True
            lblTotalViewJournal.Text = "Total Amount : "
        End If

        strSelectedDispAdvId = lblDispAdvId.Text
        onLoad_Display(strSelectedDispAdvId)
        onLoad_DisplayLn(strSelectedDispAdvId)
    End Sub

    Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub
End Class
