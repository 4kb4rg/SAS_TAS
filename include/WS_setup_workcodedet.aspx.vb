
Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports System.Collections

Imports System.Data.SqlClient
Imports Microsoft.VisualBasic


Imports agri.WS.clsSetup
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap



Public Class WS_WorkCodeDet : Inherits Page

    Protected WithEvents txtWorkCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents dgWorkCodeDet As DataGrid
    Protected WithEvents workcode As HtmlInputHidden
    Protected WithEvents lblErrBlankWC As Label
    Protected WithEvents lblErrDupWC As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblWork As Label
    Protected WithEvents lblWorkDesc As Label
    Protected WithEvents rfvCode As RequiredFieldValidator
    Protected WithEvents lblServType As Label    
    Protected WithEvents lstServType As DropDownList
    Protected WithEvents lblAccCode As Label    
    Protected WithEvents lstAccCode As DropDownList
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents lstBlock As DropDownList
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblErrWorkDesc As Label
    Protected WithEvents lblErrServType As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblWorkCode As Label

    Protected WithEvents btnFindAccCode As HtmlInputButton

    Protected WithEvents SaveBtn As ImageButton 

    Protected objGL As New agri.GL.clsSetup()

    Dim objWS As New agri.WS.clsSetup()    
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim objWorkCodeDs As New Object()
    Dim objWorkCodeDetDs As New Object()
    Dim objLangCapDs As New Object()
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim strBlkTag As String
    Dim intConfigsetting As Integer
    Dim strLocType as String


    Dim strSortExpression As String = "WorkCode"

    Dim strServTypeCode As String = ""
    Dim strAccCode As String = ""
    Dim strBlkCode As String = ""

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
                        

            If Not IsPostBack Then
                lblWorkCode.Text = Trim(Request.QueryString("workcode"))
                
                BindBlockDropList("")

                If lblWorkCode.Text <> "" Then
                    onLoad_Display
                End If

                onLoad_BindButton()
                DisplayDropDown()

            End If
        End If
    End Sub



    Sub onload_GetLangCap()
        GetEntireLangCap()

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strBlkTag = GetCaption(objLangCap.EnumLangCap.Block)
            Else
                strBlkTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKISSUE_DET_GET_LANGCAP_COSTLEVEL&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_workcodedet.aspx")
        End Try

        lblWork.text = GetCaption(objLangCap.EnumLangCap.Work)
        lblWorkDesc.text = GetCaption(objLangCap.EnumLangCap.WorkDesc)
        lblTitle.text = UCase(lblWork.text)
        lblServType.Text = GetCaption(objLangCap.EnumLangCap.ServType)
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account)
        
        lblBlkTag.Text = strBlkTag & lblCode.Text 

        lblErrBlankWC.Visible = False

        lblErrBlankWC.text = lblPleaseEnter.text & lblWork.text & lblCode.text
        rfvCode.ErrorMessage = lblPleaseEnter.text & lblWork.text & lblCode.text

   
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_WORKDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_workcodedet.aspx")
        End Try

    End Sub

        

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function



    Sub onLoad_Display()
        Dim strOpCd_GetWorkCode As String = "WS_CLSSETUP_WORKCODE_LIST_GET"
        Dim strOpCd_GetWorkCodeDet As String = "WS_CLSSETUP_WORKCODE_DET_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        
        strParam = lblWorkCode.Text & "||||" & strSortExpression & "||" 

        Try
            intErrNo = objWS.mtdGetWorkCode(strOpCd_GetWorkCode, strParam, objWorkCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODEDET_GET_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objWorkCodeDs.Tables(0).Rows.Count > 0 Then
            objWorkCodeDs.Tables(0).Rows(0).Item(0) = Trim(objWorkCodeDs.Tables(0).Rows(0).Item(0))
            objWorkCodeDs.Tables(0).Rows(0).Item(1) = Trim(objWorkCodeDs.Tables(0).Rows(0).Item(1))
            objWorkCodeDs.Tables(0).Rows(0).Item(2) = Trim(objWorkCodeDs.Tables(0).Rows(0).Item(2))
            objWorkCodeDs.Tables(0).Rows(0).Item(3) = Trim(objWorkCodeDs.Tables(0).Rows(0).Item(3))
            objWorkCodeDs.Tables(0).Rows(0).Item(4) = Trim(objWorkCodeDs.Tables(0).Rows(0).Item(4))
            objWorkCodeDs.Tables(0).Rows(0).Item(5) = Trim(objWorkCodeDs.Tables(0).Rows(0).Item(5))
            objWorkCodeDs.Tables(0).Rows(0).Item(6) = Trim(objWorkCodeDs.Tables(0).Rows(0).Item(6))

            txtWorkCode.Text = objWorkCodeDs.Tables(0).Rows(0).Item(0)
            txtDescription.Text = objWorkCodeDs.Tables(0).Rows(0).Item(1)            
            
            lblStatus.Text = Trim(objWS.mtdGetStatus(objWorkCodeDs.Tables(0).Rows(0).Item(2)))
            lblDateCreated.Text = objGlobal.GetLongDate(objWorkCodeDs.Tables(0).Rows(0).Item(3))
            lblLastUpdate.Text = objGlobal.GetLongDate(objWorkCodeDs.Tables(0).Rows(0).Item(4))            
            lblUpdatedBy.Text = objWorkCodeDs.Tables(0).Rows(0).Item(6)

            strServTypeCode = objWorkCodeDs.Tables(0).Rows(0).Item(7)
            strAccCode = objWorkCodeDs.Tables(0).Rows(0).Item(8)
            strBlkCode = objWorkCodeDs.Tables(0).Rows(0).Item(9)
            

        End If

        Try
            intErrNo = objWS.mtdGetWorkCodeDet(strOpCd_GetWorkCodeDet, strParam, objWorkCodeDetDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODEDET_GET_WORKCODEDET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
                                         
    End Sub

    Sub onLoad_BindButton()
        txtWorkCode.enabled = False
        txtDescription.enabled = False
        UnDelBtn.visible = False
        DelBtn.visible = False
        btnFindAccCode.Visible = False  

        Select Case Trim(lblStatus.Text)
            Case objWS.mtdGetStatus(objWS.EnumStatus.Active)
                txtDescription.enabled = True
                lstServType.enabled = True

                If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseControlAccForWorkExp), intConfigSetting) = True Then
                    lstAccCode.Enabled = False
                    lstBlock.Enabled = False
                Else
                    lstAccCode.Enabled = True
                    lstBlock.Enabled = True
                    btnFindAccCode.Visible = True
                End If

                SaveBtn.Visible = True 
                DelBtn.visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objWS.mtdGetStatus(objWS.EnumStatus.Deleted)

                lstServType.enabled = False
                lstAccCode.enabled = False
                lstBlock.enabled = False
                
                SaveBtn.Visible = False 
                UnDelBtn.Visible = True
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
            Case Else
                txtWorkCode.enabled = True
                txtDescription.enabled = True
                lstServType.enabled = True
                lstAccCode.enabled = True
                lstBlock.enabled = True
                btnFindAccCode.Visible = True
        End Select
    End Sub


    Sub DisplayDropDown()
        Dim strOppCd_ServType_GET As String = "WS_CLSSETUP_SERVTYPE_LIST_GET"
        Dim strOppCd_AccountCode_GET As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        
        BindDropList(strOppCd_ServType_GET, "ServTypeCode", "ServTypeCode", objWS.EnumWorkShopMasterType.ServiceType, _
                       objGlobal.EnumModule.Workshop, objWS.EnumServiceTypeStatus.Active, lstServType)
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseControlAccForWorkExp), intConfigSetting) = True Then
            lstAccCode.Enabled = False
            lstBlock.Enabled = False
            btnFindAccCode.Visible = False

            GetWorkshopControlAccount()
        Else
            If Trim(lblStatus.Text) = objWS.mtdGetStatus(objWS.EnumStatus.Active) Then
                lstAccCode.Enabled = True
                lstBlock.Enabled = True
            End If
            
            BindDropList(strOppCd_AccountCode_GET, "ActCode", "AccCode", objGL.EnumGLMasterType.AccountCode, _
                        objGlobal.EnumModule.GeneralLedger, objGL.EnumAccountCodeStatus.Active, lstAccCode)
        End If
    End Sub


    Sub BindDropList(ByVal Oppcode As String, ByVal ItemKeyField As String, ByVal MasterKeyField As String, ByVal MasterType As Integer, _
    ByVal ModuleType As Integer, ByVal Status As Integer, ByVal BindToList As DropDownList)
        
        Dim strParam As String
        Dim dsForDropDown As DataSet
        Dim dsForInactiveItem As DataSet
        Dim SelectedIndex As Integer = -1
        Dim intCnt As Integer
        Dim TblAlias As String
        Dim DataTextField As String
        Dim drinsert As DataRow
        
        Dim intErrNo As Integer
        Dim ItemCode As String
        Dim dsStockItem As DataSet
        Dim stDisplay As String = ""
        Dim stSQLCondition As String = ""

        DataTextField = "Description"

        stDisplay = "Please Select "

        If ModuleType = objGlobal.EnumModule.Workshop Then
            Select Case MasterType
                Case objWS.EnumWorkShopMasterType.ServiceType
                    TblAlias = "A"                
                    strParam = "||" & Status & "||" & _
                                "ServTypeCode|" & _
                                "ASC|"     
                    stDisplay = stDisplay + lblServType.Text
            End Select
        ElseIf ModuleType = objGlobal.EnumModule.GeneralLedger Then
            Select Case MasterType
                Case objGL.EnumGLMasterType.AccountCode
                    TblAlias = "Acc"
                    stDisplay = stDisplay + lblAccCode.Text
            End Select
        End If

        If ModuleType = objGlobal.EnumModule.GeneralLedger And MasterType = objGL.EnumGLMasterType.AccountCode Then

            
            stSQLCondition = "AND " & TblAlias & ".Status = '" & Status & "'"
            stSQLCondition = stSQLCondition + " AND " & TblAlias & ".WSAccDistUse = '" & objGL.EnumWSAccDistUse.No & "'"
            strParam = "ORDER BY " & MasterKeyField & "|" & stSQLCondition
        End If 

        Try
            Select Case ModuleType
                Case objGlobal.EnumModule.Workshop
                    intErrNo = objWS.mtdGetServType(Oppcode, strParam, dsForDropDown)               
                Case objGlobal.EnumModule.GeneralLedger
                    intErrNo = objGL.mtdGetMasterList(Oppcode, strParam, MasterType, dsForDropDown)
            End Select
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ITEMTODROPDOWN&errmesg=" & lblErrMessage.Text & "&redirect=WS/Setup/WS_StockMaster.aspx")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item(0) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0))
            dsForDropDown.Tables(0).Rows(intCnt).Item(1) = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(0)) & " ( " & _
                                                          Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(1)) & " )"
            If Not ItemCode = "" Then
                If Trim(dsForDropDown.Tables(0).Rows(intCnt).Item(MasterKeyField)) = Trim(dsStockItem.Tables(0).Rows(0).Item(ItemKeyField)) Then
                    SelectedIndex = intCnt + 1
                End If
            End If
        Next intCnt

        SelectedIndex = 0
        If Trim(strServTypeCode) <> "" Then
            If ModuleType = objGlobal.EnumModule.Workshop And MasterType =  objWS.EnumWorkShopMasterType.ServiceType Then
                For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                    If dsForDropDown.Tables(0).Rows(intCnt).Item(MasterKeyField) = Trim(strServTypeCode) Then
                        SelectedIndex = intCnt + 1
                        Exit For
                    End If
                Next
            End If
        End If

        If Trim(strAccCode) <> "" Then
            If ModuleType = objGlobal.EnumModule.GeneralLedger And MasterType = objGL.EnumGLMasterType.AccountCode Then
                For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
                    If dsForDropDown.Tables(0).Rows(intCnt).Item(MasterKeyField) = Trim(strAccCode) Then
                        SelectedIndex = intCnt + 1
                        Exit For
                    End If
                Next
                
                BindBlockDropList(cstr(trim(strAccCode)) , trim(strBlkCode))
            End If            
            
        End If        


        drinsert = dsForDropDown.Tables(0).NewRow()
        drinsert(0) = " "
        drinsert(1) = stDisplay
        dsForDropDown.Tables(0).Rows.InsertAt(drinsert, 0)

        BindToList.DataSource = dsForDropDown.Tables(0)
        BindToList.DataValueField = MasterKeyField
        BindToList.DataTextField = DataTextField
        BindToList.DataBind()




        BindToList.SelectedIndex = SelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
        If Not dsForInactiveItem Is Nothing Then
            dsForInactiveItem = Nothing
        End If

    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "WS_CLSSETUP_WORKCODE_LIST_ADD"
        Dim strOpCd_Upd As String = "WS_CLSSETUP_WORKCODE_LIST_UPD"
        Dim strOpCd_Get As String = "WS_CLSSETUP_WORKCODE_LIST_GET"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strDescription As String
        Dim strChrgRate As String
        Dim strStatus As String
        Dim strParam As String = ""
        Dim stAccCode As String = ""
        Dim iAccType As Integer
        Dim iAccPurpose As Integer
        Dim iNurseryInd As Integer
        Dim strServiceType As String = Request.Form("lstServType")
        Dim strBlkCode As String = Request.Form("lstBlock")

        strDescription = txtDescription.Text

        lblErrBlankWC.Visible = False
        lblErrDupWC.Visible = False
        lblErrWorkDesc.Visible = False      
        lblErrServType.Visible = False      
        lblErrAccCode.Visible = False       
        lblErrBlock.Visible = False         

        Select case Trim(lblstatus.text)
            case objWS.mtdGetStatus(objWS.EnumStatus.Active)
             strStatus = objWS.EnumStatus.Active
            case objWS.mtdGetStatus(objWS.EnumStatus.Deleted)
             strStatus = objWS.EnumStatus.Deleted
        End Select
                
        
        If Trim(strDescription) = "" Then
            lblErrWorkDesc.Text = "Please enter " & lblWorkDesc.text &  "."
            lblErrWorkDesc.Visible = True
            Exit Sub
        End If
        If lstServType.SelectedIndex <= 0 Then
            lblErrServType.Text = "Please select a " & lblServType.text &  "."
            lblErrServType.Visible = True
            Exit Sub
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseControlAccForWorkExp), intConfigSetting) <> True Then
            If lstAccCode.SelectedIndex <= 0 Then
                lblErrAccCode.Text = "Please select a " & lblAccCode.text &  "."
                lblErrAccCode.Visible = True
                Exit Sub
            End If
        End If

        stAccCode = fnGetValueFromDropDownList(lstAccCode)
        
        GetAccountDetails(stAccCode, iAccType, iAccPurpose, iNurseryInd)

        If iAccType = objGLset.EnumAccountType.BalanceSheet Then  
            Select Case iNurseryInd
                    Case objGLset.EnumNurseryAccount.Yes                        
                        If lstBlock.SelectedIndex <= 0 Then
                            lblErrBlock.Text = "Please select a " & strBlkTag & lblCode.Text & "."
                            lblErrBlock.Visible = True
                            Exit Sub
                        End If
             End Select
        Else If iAccType = objGLset.EnumAccountType.ProfitAndLost
            Select Case iAccPurpose
                Case objGLset.EnumAccountPurpose.NonVehicle                         
                    If lstBlock.SelectedIndex <= 0 Then
                        lblErrBlock.Text = "Please select a " & strBlkTag & lblCode.Text & "."
                        lblErrBlock.Visible = True
                        Exit Sub
                    End If
            End Select
        End If

   
        If strCmdArgs = "Save" Then
            If lblWorkCode.Text = "" Then
                If Trim(txtWorkCode.Text) = "" Then
                    lblErrBlankWC.Visible = True
                    Exit Sub
                Else
                    strParam = txtWorkCode.Text & "||||" & strSortExpression & "||"

                    Try
                        intErrNo = objWS.mtdGetWorkCode(strOpCd_Get, strParam, objWorkCodeDs)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODEDET_GET_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try

                    If objWorkCodeDs.Tables(0).Rows.Count <> 0 Then
                        lblErrDupWC.Visible = True
                        Exit Sub                      
                    Else
                        strParam = Trim(txtWorkCode.Text) & "|" & _
                                   strDescription & "|" & _
                                   objWS.EnumStatus.Active & "|" & _
                                   strServiceType & "|" & _
                                   stAccCode & "|" & _
                                   strBlkCode & "|"
                       
                        Try
                            intErrNo = objWS.mtdUpdWorkCode(strOpCd_Add, _
                                                            strOpCd_Upd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            False)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODEDET_ADD_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try

                        lblWorkCode.Text = Trim(txtWorkCode.Text)
                    End If
                End If
            Else
                strParam = Trim(txtWorkCode.Text) & "|" & _
                           strDescription & "|" & _
                           strStatus & "|" & _
                           strServiceType & "|" & _
                           stAccCode & "|" & _
                           strBlkCode & "|"

                Try
                    intErrNo = objWS.mtdUpdWorkCode(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)                                           
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODEDET_UPD_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = Trim(txtWorkCode.Text) & "||" & objWs.EnumStatus.Deleted & "||||"

            Try
                intErrNo = objWS.mtdUpdWorkCode(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODEDET_DEL_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = Trim(txtWorkCode.Text) & "||" & objWs.EnumStatus.Active & "||||"

            Try
                intErrNo = objWS.mtdUpdWorkCode(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)                                            
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODEDET_UNDEL_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        End If

        If Trim(lblWorkCode.Text) <> ""
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("WS_WorkCodeList.aspx")
    End Sub


    Sub lstAccCode_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strAccCode As String = Trim(fnGetValueFromDropDownList(lstAccCode))
        Dim strBlkCode As String = Trim(fnGetValueFromDropDownList(lstBlock))


        lblErrAccCode.Visible = False
        lblErrBlock.Visible = False
        BindBlockDropList(strAccCode, strBlkCode)        
    End Sub


    Sub BindBlockDropList(ByVal pv_strAccCode As String, Optional ByVal pv_strBlkCode As String = "")        
        Dim dsForDropDown As DataSet
        Dim intSelectedIndex As Integer = 0
        Dim intCnt As Integer
        Dim strOpCdBlockList_Get As String
        Dim strParam As String
        Dim dr As DataRow
        Dim intErrNo As Integer

        Try
            If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_BLOCK_GET"
                strParam = pv_strAccCode & "|" & strLocation & "|" & objGLset.EnumBlockStatus.Active
            Else
                strOpCdBlockList_Get = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
                strParam = pv_strAccCode & "|" & strLocation & "|" & objGLset.EnumBlockStatus.Active
            End If
            intErrNo = objGLset.mtdGetAccountBlock(strOpCdBlockList_Get, _
                                                     strParam, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_ACCOUNT_BLOCK_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode"))
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode")) & " (" & Trim(dsForDropDown.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If dsForDropDown.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(pv_strBlkCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("Description") = lblSelect.Text & strBlkTag & lblCode.Text

        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)
        lstBlock.DataSource = dsForDropDown.Tables(0)
        lstBlock.DataValueField = "BlkCode"
        lstBlock.DataTextField = "Description"
        lstBlock.DataBind()
        lstBlock.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub GetWorkshopControlAccount()
            Dim objGLDs As New DataSet()
            Dim objTemp As New DataSet()
            Dim iSelectedIndex As Integer = 0
            Dim strOpCdGetAcc As String = "GL_CLSSETUP_ENTRYSETUP_TYPE_GET"
            Dim strOpCdGetAccDesc As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
            Dim intErrNo As Integer            
            Dim strParam As String = ""
            Dim stAccCode As String = ""
            Dim drinsert As DataRow

            strParam = objGlobal.EnumModule.Workshop & "|" & objGL.EnumEntryType.WSDRCTRLACC & "|" & strLocation            

            Try
                intErrNo = objGL.mtdGetEntrySetupDet(strOpCdGetAcc, _
                                                     strParam, _                                                     
                                                     objTemp )
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_SAVE&errmesg=" & Exp.ToString & "&redirect=")                
            End Try

            If objTemp.Tables(0).Rows.Count > 0 Then
                stAccCode = objTemp.Tables(0).Rows(0).Item("CRAccCode")                
                strParam = "Order By ACC.AccCode|And ACC.Status = '" & _
                           objGL.EnumAccountCodeStatus.Active & "' AND ACC.AccCode = '" & stAccCode & "'"
                Try
                    intErrNo = objGL.mtdGetMasterList(strOpCdGetAccDesc, _
                                                        strParam, _
                                                        objGL.EnumGLMasterType.AccountCode, _
                                                        objGLDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ENTRYSETUP_ACCOUNT_GET&errmesg=" & Exp.ToString & "&redirect=")
                End Try
            End If

            If objGLDs.Tables.Count > 0 Then
                iSelectedIndex = 1
            Else
               objGLDs = New DataSet()
               objGLDs.Tables.Add("GL_ENTRYSETUP")
               objGLDs.Tables(0).Columns.Add("AccCode")
               objGLDs.Tables(0).Columns.Add("_Description")
               iSelectedIndex = 0


            End If


            drinsert = objGLDs.Tables(0).NewRow()
            drinsert("AccCode") = ""
            drinsert("_Description") = "Please Select Account Code."
            objGLDs.Tables(0).Rows.InsertAt(drinsert, 0)

            lstAccCode.DataSource = objGLDs.Tables(0)
            lstAccCode.DataValueField = "AccCode"
            lstAccCode.DataTextField = "_Description"
            lstAccCode.DataBind()

            lstAccCode.SelectedIndex = iSelectedIndex           
    End Sub
    
    Sub GetAccountDetails(ByVal pv_strAccCode As String, _
                          ByRef pr_strAccType As Integer, _
                          ByRef pr_strAccPurpose As Integer, _
                          ByRef pr_strNurseryInd As Integer)

        Dim _objAccDs As New DataSet()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = pv_strAccCode
        Dim intErrNo As Integer

        Try
            intErrNo = objGLset.mtdGetAccount(strOpCd, _
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

    Function fnGetValueFromDropDownList(ByRef ddlObject As DropDownList) As String
        If Trim(Request.Form(ddlObject.ID)) <> "" Then
            fnGetValueFromDropDownList = Trim(Request.Form(ddlObject.ID))
        Else
            fnGetValueFromDropDownList = ddlObject.SelectedItem.Value
        End If
    End Function


End Class
