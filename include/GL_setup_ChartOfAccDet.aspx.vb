
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


Public Class GL_Setup_ChartOfAccDet : Inherits Page

    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlAccType As DropDownList
    Protected WithEvents ddlAccGrpCode As DropDownList
    Protected WithEvents ddl01 As DropDownList
    Protected WithEvents ddl02 As DropDownList
    Protected WithEvents ddl03 As DropDownList
    Protected WithEvents ddl04 As DropDownList
    Protected WithEvents rdPurpose As RadioButtonList
    Protected WithEvents rdMethod As RadioButtonList
    Protected WithEvents lblMethod As Label
    Protected WithEvents cbNursery As CheckBox
    Protected WithEvents txtFinAccCode As TextBox
    Protected WithEvents lbl01 As Label
    Protected WithEvents lbl02 As Label
    Protected WithEvents lbl03 As Label
    Protected WithEvents lbl04 As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents tbcode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrAccCode As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents lblErrAccType As Label
    Protected WithEvents lblErrAccGrpCode As Label
    Protected WithEvents lblErrLen As Label
    Protected WithEvents lblErr01 As Label
    Protected WithEvents lblErr02 As Label
    Protected WithEvents lblErr03 As Label
    Protected WithEvents lblErr04 As Label
    Protected WithEvents lblErrValidate As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblAccDesc As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblAccGrp As Label
    Protected WithEvents lblFinAccCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblAccClass As Label
    Protected WithEvents lblActivity As Label
    Protected WithEvents lblSubActivity As Label
    Protected WithEvents lblExpense As Label
    Protected WithEvents validateDesc As RequiredFieldValidator
    Protected WithEvents rowpurpose As HtmlTableRow
    Protected WithEvents rowNurseryUse As HtmlTableRow
    Protected WithEvents lblDdlActivity As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblEnter As Label
    Protected WithEvents lblType As Label
    Protected WithEvents lblAnd As Label
    Protected WithEvents lblCodeShouldBeIn As Label
    Protected WithEvents lblCharacter As Label
    Protected WithEvents cbWSAccDist As CheckBox
    Protected WithEvents cbMedAccDist As CheckBox
    Protected WithEvents cbHousingAccDist As CheckBox
    Protected WithEvents rowMedicalUse As HtmlTableRow
    Protected WithEvents rowHousingUse As HtmlTableRow

    Protected WithEvents lblErrCOAGeneral As Label

    Protected WithEvents ddlCOALevel As DropDownList
    Protected WithEvents ddlCOAGeneral As DropDownList
    Protected WithEvents ddlSaldoNormal As DropDownList
    Protected WithEvents ddlGrpCashflow As DropDownList


    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAdmin As New agri.Admin.clsUOM()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim objGLTrx As New agri.GL.clsTrx()

    Dim objAccDs As New Object()
    Dim objAccGrpDs As New Object()
    Dim objActDs As New Object()
    Dim objAccClsDs As New Object()
    Dim objConfigDs As New Object()
    Dim objSubActDs As New Object()
    Dim objExpenseDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strSelectedAccCode As String = ""
    Dim intStatus As Integer
    Dim intAccCodeLen As Integer = 0
    Dim strAccStructure As String = ""

    Dim intAccClsLen As Integer = 0
    Dim intActLen As Integer = 0
    Dim intSubActLen As Integer = 0
    Dim intExpenseLen As Integer = 0
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim intConfigValue As Integer
    Dim intLocLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")
        intLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDup.Visible = False
            lblErrAccCode.Visible = False
            lblErrAccType.Visible = False
            lblErrAccGrpCode.Visible = False
            lblErr01.Visible = False
            lblErr02.Visible = False
            lblErr03.Visible = False
            lblErr04.Visible = False
            lblErrValidate.Visible = False
            lblErrCOAGeneral.Visible = False

            strSelectedAccCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            intStatus = CInt(lblHiddenSts.Text)
            onload_GetLangCap()
            If Not IsPostBack Then
                If strSelectedAccCode <> "" Then
                    tbcode.Value = strSelectedAccCode
                    onLoad_Display()
                    Change_AccType()
                Else
                    BindAccType("")
                    BindAccGrpCode("")
                    BindCOAGeneral("")
                    GetConfigInfo(strAccStructure, intAccCodeLen, intAccClsLen, intActLen, intSubActLen, intExpenseLen)
                    BuildAccStructure(strAccStructure, "", "", "", "")
                    onLoad_BindButton()
                End If
            End If

            If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
                SaveBtn.Visible = True
            Else
                SaveBtn.Visible = True
            End If

            
            If intStatus = 1 Then
                DelBtn.Visible = True
                UnDelBtn.Visible = False
            Else
                DelBtn.Visible = False
                UnDelBtn.Visible = True
            End If
        End If
    End Sub

    Sub CallChange_AccType(ByVal Sender As Object, ByVal E As EventArgs)
        Change_AccType()
    End Sub

    Sub Change_AccType()
        If ddlAccType.SelectedItem.Value.Trim <> "" Then
            If ddlAccType.SelectedItem.Value.Trim = objGLSetup.EnumAccountType.BalanceSheet Then
                rowpurpose.Visible = False
                rowNurseryUse.Visible = True    
                if  rdPurpose.SelectedItem.Value = "1" and cbWSAccDist.checked=false then
                    enableMedHousingCheckbox(false)
                    setMedHousingCheckbox(false)     
                end if
                If intStatus = objGLSetup.EnumAccStatus.Active Then     
                    cbNursery.Enabled = True
                End If                                                  
            Else
                rowpurpose.Visible = True
                rowNurseryUse.Visible = False   
                cbNursery.Checked = False
                if  rdPurpose.SelectedItem.Value = "1" and cbWSAccDist.checked=false then
                    enableMedHousingCheckbox(true)
                end if
            End If

        End If

    End Sub

    Sub Change_Method(ByVal Sender As Object, ByVal E As EventArgs)
        If rdMethod.SelectedItem.Value = "1" Then
            ddl01.Enabled = False
            ddl02.Enabled = False
            ddl03.Enabled = False
            ddl04.Enabled = False
            txtAccCode.Enabled = True
        Else
            txtAccCode.Enabled = False
            ddl01.Enabled = True
            ddl02.Enabled = True
            ddl03.Enabled = True
            ddl04.Enabled = True
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strParam As String = strSelectedAccCode
        Dim intErrNo As Integer
        Dim strAccClsCode As String
        Dim strActCode As String
        Dim strSubActCode As String
        Dim strExpenseCode As String
        Dim intConfigSetting As Integer
        Dim strOpCode_Config As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"

        intAccCodeLen = 0

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_SUBACTIVITY_LIST_GET_BY_SUBACTCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        rdMethod.Items(0).Selected = True
        txtAccCode.Text = Trim(objAccDs.Tables(0).Rows(0).Item("AccCode"))
        txtDescription.Text = Trim(objAccDs.Tables(0).Rows(0).Item("Description"))
        txtFinAccCode.Text = Trim(objAccDs.Tables(0).Rows(0).Item("FinAccCode"))

        intStatus = CInt(Trim(objAccDs.Tables(0).Rows(0).Item("Status")))
        lblHiddenSts.Text = Trim(objAccDs.Tables(0).Rows(0).Item("Status"))
        lblStatus.Text = objGLSetup.mtdGetAccStatus(Trim(objAccDs.Tables(0).Rows(0).Item("Status")))
        lblDateCreated.Text = objGlobal.GetLongDate(objAccDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objAccDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = Trim(objAccDs.Tables(0).Rows(0).Item("UserName"))

        BindAccType(Trim(objAccDs.Tables(0).Rows(0).Item("AccType")))
        BindAccGrpCode(Trim(objAccDs.Tables(0).Rows(0).Item("AccGrpCode")))

        BindCOAGeneral(Trim(objAccDs.Tables(0).Rows(0).Item("COAGeneral")))

        strAccClsCode = Trim(objAccDs.Tables(0).Rows(0).Item("AccclsCode"))
        strActCode = Trim(objAccDs.Tables(0).Rows(0).Item("ActCode"))
        strSubActCode = Trim(objAccDs.Tables(0).Rows(0).Item("SubActCode"))
        strExpenseCode = Trim(objAccDs.Tables(0).Rows(0).Item("ExpenseCode"))
        ddlCOALevel.SelectedValue = Trim(objAccDs.Tables(0).Rows(0).Item("COALevel"))
        ddlSaldoNormal.SelectedValue = Trim(objAccDs.Tables(0).Rows(0).Item("SaldoNormal"))
        ddlGrpCashflow.SelectedValue = Trim(objAccDs.Tables(0).Rows(0).Item("GrpCashflow"))

        If objAccDs.Tables(0).Rows(0).Item("NurseryInd") = objGLSetup.EnumNurseryAccount.Yes Then
            cbNursery.Checked = True
        Else
            cbNursery.Checked = False
        End If

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCode_Config, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                objConfigDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SYSPARAM_GET_CONFIG&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intConfigSetting = objConfigDs.Tables(0).Rows(0).Item("ConfigSetting")

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseControlAccForWorkExp), intConfigSetting) = True Then
            cbWSAccDist.Enabled = False

        Else
            If intStatus = objGLSetup.EnumAccStatus.Active Then
                cbWSAccDist.Enabled = True
            End If

            If objAccDs.Tables(0).Rows(0).Item("WSAccDistUse") = objGLSetup.EnumWSAccDistUse.Yes Then
                cbWSAccDist.Checked = True
                enableMedHousingCheckbox(False)
                setMedHousingCheckbox(False)
            Else
                cbWSAccDist.Checked = False

            End If
        End If
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.WAccDistribution5), intConfigSetting) = False Then
            rowMedicalUse.Visible = False
            rowHousingUse.Visible = False
        Else
            rowMedicalUse.Visible = True
            rowHousingUse.Visible = True

            If intStatus = objGLSetup.EnumAccStatus.Active Then
                enableMedHousingCheckbox(True)
            End If
            If Not IsDBNull(objAccDs.Tables(0).Rows(0).Item("MedAccDistUse")) Then
                If objAccDs.Tables(0).Rows(0).Item("MedAccDistUse") = objGLSetup.EnumMedAccDist.Yes Then
                    cbMedAccDist.Checked = True
                Else
                    cbMedAccDist.Checked = False
                End If
            Else
                cbMedAccDist.Checked = False
            End If
            If Not IsDBNull(objAccDs.Tables(0).Rows(0).Item("HousingAccDistUse")) Then
                If objAccDs.Tables(0).Rows(0).Item("HousingAccDistUse") = objGLSetup.EnumHousingAccDist.Yes Then
                    cbHousingAccDist.Checked = True
                    enableMedHousingCheckbox(False)
                Else
                    cbHousingAccDist.Checked = False
                End If
            Else
                cbHousingAccDist.Checked = False
            End If
        End If

        lblErrCOAGeneral.Visible = False


        rdPurpose.Items(0).Selected = IIf(CInt(Trim(objAccDs.Tables(0).Rows(0).Item("AccPurpose"))) = objGLSetup.EnumAccountPurpose.NonVehicle, True, False)
        rdPurpose.Items(1).Selected = IIf(CInt(Trim(objAccDs.Tables(0).Rows(0).Item("AccPurpose"))) = objGLSetup.EnumAccountPurpose.VehicleDistribution, True, False)
        onLoad_BindButton()

    End Sub

    Sub onLoad_BindButton()
        Dim intErrNo As Integer
        Dim strOpCode_Config As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim intConfigSetting As Integer

        txtAccCode.Enabled = False
        txtDescription.Enabled = False
        ddlAccType.Enabled = False
        ddlAccGrpCode.Enabled = False
        ddl01.Visible = False
        ddl01.Enabled = False
        ddl02.Visible = False
        ddl02.Enabled = False
        ddl03.Visible = False
        ddl03.Enabled = False
        ddl04.Visible = False
        ddl04.Enabled = False
        lbl01.Visible = False
        lbl02.Visible = False
        lbl03.Visible = False
        lbl04.Visible = False
        rdPurpose.Enabled = False
        rdMethod.Visible = False
        lblMethod.Visible = False
        txtFinAccCode.Enabled = False
        cbWSAccDist.Enabled = False
        cbNursery.Enabled = False


        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        Select Case intStatus
            Case objGLSetup.EnumAccStatus.Active
                txtDescription.Enabled = True
                ddlAccType.Enabled = True
                ddlAccGrpCode.Enabled = True
                rdPurpose.Enabled = True
                txtFinAccCode.Enabled = True
                cbWSAccDist.Enabled = True 
                cbNursery.Enabled = True

                if cbWSAccDist.checked=false then
                    cbMedAccDist.Enabled = True 
                    cbHousingAccDist.Enabled = True
                end if

                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objGLSetup.EnumAccStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtAccCode.Enabled = True
                txtDescription.Enabled = True
                ddlAccType.Enabled = True
                ddlAccGrpCode.Enabled = True
                ddl01.Visible = True
                ddl02.Visible = True
                ddl03.Visible = True
                ddl04.Visible = True
                lbl01.Visible = True
                lbl02.Visible = True
                lbl03.Visible = True
                lbl04.Visible = True
                rdPurpose.Enabled = True
                rdPurpose.Items(0).Selected = True
                rdMethod.Visible = True
                rdMethod.Items(0).Selected = True
                lblMethod.Visible = True
                txtFinAccCode.Enabled = True
                cbWSAccDist.Enabled = True 
                cbNursery.Enabled = True

                cbMedAccDist.Enabled = True 
                cbHousingAccDist.Enabled = True
                SaveBtn.Visible = True
        End Select

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
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.CentreControl), intConfigSetting) = True Then
            intConfigValue = 1
        Else
            intConfigValue = 0
            rdMethod.Items(0).Selected = True
            rdMethod.Enabled = False
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.UseControlAccForWorkExp), intConfigSetting) = True Then
            cbWSAccDist.Enabled = False
        Else
            If intStatus = objGLSetup.EnumAccStatus.Active Then
                cbWSAccDist.Enabled = True
            End If
        End If

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.WAccDistribution5), intConfigSetting) = true Then
            rowMedicalUse.visible=true
            rowHousingUse.visible=true
        Else
            rowMedicalUse.visible=false
            rowHousingUse.visible=false
            If intStatus = objGLSetup.EnumAccStatus.Active Then   
                cbMedAccDist.Enabled = True  
                cbHousingAccDist.Enabled = True  
            End If                                                
        End If
    End Sub

    Sub onload_GetLangCap()

        GetEntireLangCap()

        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Account))
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblAccDesc.Text = GetCaption(objLangCap.EnumLangCap.AccDesc)
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account)
        lblAccGrp.Text = GetCaption(objLangCap.EnumLangCap.AccGrp)
        lblFinAccCode.Text = GetCaption(objLangCap.EnumLangCap.FinAccCode)
        lblAccClass.Text = GetCaption(objLangCap.EnumLangCap.AccClass)
        lblActivity.Text = GetCaption(objLangCap.EnumLangCap.Activity)
        lblSubActivity.Text = GetCaption(objLangCap.EnumLangCap.SubAct)
        lblExpense.Text = GetCaption(objLangCap.EnumLangCap.Expense) & lblCode.Text

        validateDesc.ErrorMessage = lblPleaseEnter.Text & lblAccDesc.Text & "."
        lblErrAccType.Text = lblPleaseEnter.Text & lblAccount.Text & lblType.Text & "."
        lblErrAccGrpCode.Text = lblPleaseEnter.Text & lblAccGrp.Text & "."

        rdMethod.Items(0).Text = lblEnter.Text & lblAccCode.Text
        rdMethod.Items(1).Text = lblSelect.Text & lblAccClass.Text & ", " & lblActivity.Text & ", " & lblSubActivity.Text & lblAnd.Text & lblExpense.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_ChartOfAcc.aspx")
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

    Sub BindAccType(ByVal pv_strAccType As String)
        Dim intCounter As Integer
        Dim blnIsUpdate As Boolean
        Dim strSelect As String

        strSelect = lblSelect.Text & lblAccount.Text & lblType.Text
        If ddlAccType.Items.Count < 1 Then
            ddlAccType.Items.Add(New ListItem(strSelect, ""))
            ddlAccType.Items.Add(New ListItem(objGLSetup.mtdGetAccountType(objGLSetup.EnumAccountType.BalanceSheet), objGLSetup.EnumAccountType.BalanceSheet))
            ddlAccType.Items.Add(New ListItem(objGLSetup.mtdGetAccountType(objGLSetup.EnumAccountType.ProfitAndLost), objGLSetup.EnumAccountType.ProfitAndLost))
        End If

        For intCounter = 0 To ddlAccType.Items.Count
            If ddlAccType.Items(intCounter).Value = pv_strAccType Then
                ddlAccType.SelectedIndex = intCounter
                Exit For
            End If
        Next
    End Sub

    Sub BindAccGrpCode(ByVal pv_strAccGrpCode As String)
        Dim strOpCode As String = "GL_CLSSETUP_CHARTOFACCOUNT_ACCGRPCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSortOrder As String
        Dim strSearchExp As String

        strSortOrder = "order by AccGrpCode "
        strSearchExp = "where Status = '" & objGLSetup.EnumAccGrpStatus.Active & "' "

        strParam = strSortOrder & "|" & strSearchExp

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   0, _
                                                   objAccGrpDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_ACCGRPCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccGrpDs.Tables(0).Rows.Count - 1
            objAccGrpDs.Tables(0).Rows(intCnt).Item("AccGrpCode") = Trim(objAccGrpDs.Tables(0).Rows(intCnt).Item("AccGrpCode"))
            objAccGrpDs.Tables(0).Rows(intCnt).Item("Description") = objAccGrpDs.Tables(0).Rows(intCnt).Item("AccGrpCode") & " (" & Trim(objAccGrpDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccGrpDs.Tables(0).Rows(intCnt).Item("AccGrpCode") = Trim(pv_strAccGrpCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objAccGrpDs.Tables(0).NewRow()
        dr("AccGrpCode") = ""
        dr("Description") = lblSelect.Text & lblAccGrp.Text

        objAccGrpDs.Tables(0).Rows.InsertAt(dr, 0)
        ddlAccGrpCode.DataSource = objAccGrpDs.Tables(0)
        ddlAccGrpCode.DataValueField = "AccGrpCode"
        ddlAccGrpCode.DataTextField = "Description"
        ddlAccGrpCode.DataBind()
        ddlAccGrpCode.SelectedIndex = intSelectIndex
    End Sub


    Sub BindCOAGeneral(ByVal pv_strCOAGeneral As String)
      
        Dim strOpCode As String = "GL_CLSSETUP_COAGENERAL_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim dsForDropDown As DataSet
        Dim intErrNo As Integer
        Dim intSelectIndex As Integer = 0
        Dim intCnt As Integer
        Dim dr As DataRow

        strParamName = "LOCCODE"
        'strParamValue = strLocation
		strParamValue = IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                     strParamName, _
                                                     strParamValue, _
                                                     dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_ACCGRPCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            dsForDropDown.Tables(0).Rows(intCnt).Item("Code") = dsForDropDown.Tables(0).Rows(intCnt).Item("Code").Trim()
            dsForDropDown.Tables(0).Rows(intCnt).Item("Description") = dsForDropDown.Tables(0).Rows(intCnt).Item("Description").Trim()

            If dsForDropDown.Tables(0).Rows(intCnt).Item("Code") = Trim(pv_strCOAGeneral) Then
                intSelectIndex = intCnt + 1
            End If


        Next intCnt

        dr = dsForDropDown.Tables(0).NewRow()
        dr("Code") = ""
        dr("Description") = "None"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlCOAGeneral.DataSource = dsForDropDown.Tables(0)
        ddlCOAGeneral.DataValueField = "Code"
        ddlCOAGeneral.DataTextField = "Description"
        ddlCOAGeneral.DataBind()

        ddlCOAGeneral.SelectedIndex = intSelectIndex

    End Sub


    Function BindActCode(ByVal pv_strActCode As String, ByRef pr_intSelectIndex As Integer) As DataSet
        Dim strOpCode As String = "GL_CLSSETUP_CHARTOFACCOUNT_ACTCODE_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSortOrder As String
        Dim strSearchExp As String

        strSortOrder = "order by ActCode "
        strSearchExp = "where Status = '" & objGLSetup.EnumActStatus.Active & "' "

        strParam = strSortOrder & "|" & strSearchExp
        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   0, _
                                                   objActDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_ACTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objActDs.Tables(0).Rows.Count - 1
            objActDs.Tables(0).Rows(intCnt).Item("ActCode") = Trim(objActDs.Tables(0).Rows(intCnt).Item("ActCode"))
            objActDs.Tables(0).Rows(intCnt).Item("Description") = objActDs.Tables(0).Rows(intCnt).Item("ActCode") & " (" & Trim(objActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objActDs.Tables(0).Rows(intCnt).Item("ActCode") = Trim(pv_strActCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objActDs.Tables(0).NewRow()
        dr("ActCode") = ""
        dr("Description") = lblSelect.Text & lblActivity.Text
        objActDs.Tables(0).Rows.InsertAt(dr, 0)
        pr_intSelectIndex = intSelectIndex
        Return objActDs

    End Function

    Function BindAccClsCode(ByVal pv_strAccClsCode As String, ByRef pr_intSelectIndex As Integer) As DataSet
        Dim strOpCode As String = "GL_CLSSETUP_CHARTOFACCOUNT_ACCCLSCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSortOrder As String
        Dim strSearchExp As String

        strSortOrder = "order by AccClsCode "
        strSearchExp = "where Status = '" & objGLSetup.EnumAccClsStatus.Active & "' "

        strParam = strSortOrder & "|" & strSearchExp

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                 strParam, _
                                                 0, _
                                                 objAccClsDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_ACCCLSCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccClsDs.Tables(0).Rows.Count - 1
            objAccClsDs.Tables(0).Rows(intCnt).Item("AccClsCode") = Trim(objAccClsDs.Tables(0).Rows(intCnt).Item("AccClsCode"))
            objAccClsDs.Tables(0).Rows(intCnt).Item("Description") = objAccClsDs.Tables(0).Rows(intCnt).Item("AccClsCode") & " (" & Trim(objAccClsDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objAccClsDs.Tables(0).Rows(intCnt).Item("AccClsCode") = Trim(pv_strAccClsCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objAccClsDs.Tables(0).NewRow()
        dr("AccClsCode") = ""
        dr("Description") = lblSelect.Text & lblAccClass.Text
        objAccClsDs.Tables(0).Rows.InsertAt(dr, 0)
        pr_intSelectIndex = intSelectIndex
        Return objAccClsDs

    End Function


    Function BindSubActCode(ByVal pv_strActCode As String, ByVal pv_strSubActCode As String, ByRef pr_intSelectIndex As Integer) As DataSet
        Dim strOpCode As String = "GL_CLSSETUP_CHARTOFACCOUNT_SUBACTCODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSortOrder As String
        Dim strSearchExp As String

        strSortOrder = "order by SubActCode "

        If pv_strActCode = "" Then
            strSearchExp = "where Status = '" & objGLSetup.EnumSubActStatus.Active & "' "
        Else
            strSearchExp = "where Status = '" & objGLSetup.EnumSubActStatus.Active & _
                           "' and ActCode = '" & pv_strActCode & _
                           "' or ActCode = ''"
        End If

        strParam = strSortOrder & "|" & strSearchExp

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   0, _
                                                   objSubActDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_SUBACTCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objSubActDs.Tables(0).Rows.Count - 1
            objSubActDs.Tables(0).Rows(intCnt).Item("SubActCode") = Trim(objSubActDs.Tables(0).Rows(intCnt).Item("SubActCode"))
            objSubActDs.Tables(0).Rows(intCnt).Item("Description") = objSubActDs.Tables(0).Rows(intCnt).Item("SubActCode") & " (" & Trim(objSubActDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objSubActDs.Tables(0).Rows(intCnt).Item("SubActCode") = Trim(pv_strSubActCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objSubActDs.Tables(0).NewRow()
        dr("SubActCode") = ""
        dr("Description") = lblSelect.Text & lblSubActivity.Text
        objSubActDs.Tables(0).Rows.InsertAt(dr, 0)
        pr_intSelectIndex = intSelectIndex
        Return objSubActDs

    End Function

    Function BindExpenseCode(ByVal pv_strExpenseCode As String, ByRef pr_intSelectIndex As Integer) As DataSet
        Dim strOpCode As String = "GL_CLSSETUP_CHARTOFACCOUNT_EXPENSECODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSortOrder As String
        Dim strSearchExp As String

        strSortOrder = "order by ExpenseCode "
        strSearchExp = "where Status = '" & objGLSetup.EnumExpCodeStatus.Active & "' "

        strParam = strSortOrder & "|" & strSearchExp

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   0, _
                                                   objExpenseDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_EXPENSECODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objExpenseDs.Tables(0).Rows.Count - 1
            objExpenseDs.Tables(0).Rows(intCnt).Item("ExpenseCode") = Trim(objExpenseDs.Tables(0).Rows(intCnt).Item("ExpenseCode"))
            objExpenseDs.Tables(0).Rows(intCnt).Item("Description") = objExpenseDs.Tables(0).Rows(intCnt).Item("ExpenseCode") & " (" & Trim(objExpenseDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objExpenseDs.Tables(0).Rows(intCnt).Item("ExpenseCode") = Trim(pv_strExpenseCode) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objExpenseDs.Tables(0).NewRow()
        dr("ExpenseCode") = ""
        dr("Description") = lblSelect.Text & lblExpense.Text
        objExpenseDs.Tables(0).Rows.InsertAt(dr, 0)
        pr_intSelectIndex = intSelectIndex
        Return objExpenseDs

    End Function

    Sub GetConfigInfo(ByRef pr_strAccStructure, ByRef pr_intAccCodeLen, ByRef pr_intAccClsLen, ByRef pr_intActLen, ByRef pr_intSubActLen, ByRef pr_intExpenseLen)
        Dim strOpCd As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim intErrNo As Integer
        

        Try
            intErrNo = objSys.mtdGetConfigInfo(strOpCd, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               objConfigDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_GETCONFIGINFO&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_ActivityDet.aspx")
        End Try

        pr_intAccCodeLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("AccCodeLen")))
        txtAccCode.MaxLength = pr_intAccCodeLen
        pr_strAccStructure = Trim(objConfigDs.Tables(0).Rows(0).Item("AccStructure"))

        pr_intAccClsLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("AccClassLen")))
        pr_intActLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("ActLen")))
        pr_intSubActLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("SubActLen")))
        pr_intExpenseLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("ExpenseLen")))
    End Sub

    Sub BuildAccStructure(ByVal pv_strAccStructure, ByVal pv_strAccClsCode, ByVal pv_strActCode, ByVal pv_strSubActCode, ByVal pv_strExpenseCode)
        Dim arrAccStructure(10) As String
        Dim intSelectIndex As Integer
        Dim intCount As Integer
        Dim arrLabelText(30) As String

        arrAccStructure = Split(pv_strAccStructure, Chr(9))
        For intCount = 0 To UBound(arrAccStructure)
            Select Case CInt(arrAccStructure(intCount))
                Case objSys.EnumChartOfAccount.AccountClass
                    arrLabelText(intCount) = lblAccClass.Text & lblCode.Text & " :*"
                Case objSys.EnumChartOfAccount.Activity
                    arrLabelText(intCount) = lblActivity.Text & lblCode.Text & " :*"
                Case objSys.EnumChartOfAccount.SubActivity
                    arrLabelText(intCount) = lblSubActivity.Text & lblCode.Text & " :*"
                Case objSys.EnumChartOfAccount.Expense
                    arrLabelText(intCount) = lblExpense.Text & " :*"
            End Select
        Next
        lbl01.Text = arrLabelText(0)
        lbl02.Text = arrLabelText(1)
        lbl03.Text = arrLabelText(2)
        lbl04.Text = arrLabelText(3)

        Select Case CInt(arrAccStructure(0))
            Case objSys.EnumChartOfAccount.AccountClass
                ddl01.DataSource = BindAccClsCode(pv_strAccClsCode, intSelectIndex)
                ddl01.DataValueField = "AccClsCode"
                ddl01.DataTextField = "Description"
                ddl01.DataBind()
                lblErr01.Text = lblPleaseSelect.Text & lblAccClass.Text & "."
            Case objSys.EnumChartOfAccount.Activity
                ddl01.DataSource = BindActCode(pv_strActCode, intSelectIndex)
                ddl01.DataValueField = "ActCode"
                ddl01.DataTextField = "Description"
                ddl01.DataBind()
                ddl01.AutoPostBack = True
                lblErr01.Text = lblPleaseSelect.Text & lblActivity.Text & "."
                lblDdlActivity.Text = "ddl01"
            Case objSys.EnumChartOfAccount.SubActivity
                ddl01.DataSource = BindSubActCode("", pv_strSubActCode, intSelectIndex)
                ddl01.DataValueField = "SubActCode"
                ddl01.DataTextField = "Description"
                ddl01.DataBind()
                lblErr01.Text = lblPleaseSelect.Text & lblSubActivity.Text & "."
            Case objSys.EnumChartOfAccount.Expense
                ddl01.DataSource = BindExpenseCode(pv_strExpenseCode, intSelectIndex)
                ddl01.DataValueField = "ExpenseCode"
                ddl01.DataTextField = "Description"
                ddl01.DataBind()
                lblErr01.Text = lblPleaseSelect.Text & lblExpense.Text & "."
        End Select

        Select Case CInt(arrAccStructure(1))
            Case objSys.EnumChartOfAccount.AccountClass
                ddl02.DataSource = BindAccClsCode(pv_strAccClsCode, intSelectIndex)
                ddl02.DataValueField = "AccClsCode"
                ddl02.DataTextField = "Description"
                ddl02.DataBind()
                lblErr02.Text = lblPleaseSelect.Text & lblAccClass.Text & "."
            Case objSys.EnumChartOfAccount.Activity
                ddl02.DataSource = BindActCode(pv_strActCode, intSelectIndex)
                ddl02.DataValueField = "ActCode"
                ddl02.DataTextField = "Description"
                ddl02.DataBind()
                ddl02.AutoPostBack = True
                lblErr02.Text = lblPleaseSelect.Text & lblActivity.Text & "."
                lblDdlActivity.Text = "ddl02"
            Case objSys.EnumChartOfAccount.SubActivity
                ddl02.DataSource = BindSubActCode("", pv_strSubActCode, intSelectIndex)
                ddl02.DataValueField = "SubActCode"
                ddl02.DataTextField = "Description"
                ddl02.DataBind()
                lblErr02.Text = lblPleaseSelect.Text & lblSubActivity.Text & "."
            Case objSys.EnumChartOfAccount.Expense
                ddl02.DataSource = BindExpenseCode(pv_strExpenseCode, intSelectIndex)
                ddl02.DataValueField = "ExpenseCode"
                ddl02.DataTextField = "Description"
                ddl02.DataBind()
                lblErr02.Text = lblPleaseSelect.Text & lblExpense.Text & "."
        End Select

        Select Case CInt(arrAccStructure(2))
            Case objSys.EnumChartOfAccount.AccountClass
                ddl03.DataSource = BindAccClsCode(pv_strAccClsCode, intSelectIndex)
                ddl03.DataValueField = "AccClsCode"
                ddl03.DataTextField = "Description"
                ddl03.DataBind()
                lblErr03.Text = lblPleaseSelect.Text & lblAccClass.Text & "."
            Case objSys.EnumChartOfAccount.Activity
                ddl03.DataSource = BindActCode(pv_strActCode, intSelectIndex)
                ddl03.DataValueField = "ActCode"
                ddl03.DataTextField = "Description"
                ddl03.DataBind()
                ddl03.AutoPostBack = True
                lblErr03.Text = lblPleaseSelect.Text & lblActivity.Text & "."
                lblDdlActivity.Text = "ddl03"
            Case objSys.EnumChartOfAccount.SubActivity
                ddl03.DataSource = BindSubActCode("", pv_strSubActCode, intSelectIndex)
                ddl03.DataValueField = "SubActCode"
                ddl03.DataTextField = "Description"
                ddl03.DataBind()
                lblErr03.Text = lblPleaseSelect.Text & lblSubActivity.Text & "."
            Case objSys.EnumChartOfAccount.Expense
                ddl03.DataSource = BindExpenseCode(pv_strExpenseCode, intSelectIndex)
                ddl03.DataValueField = "ExpenseCode"
                ddl03.DataTextField = "Description"
                ddl03.DataBind()
                lblErr03.Text = lblPleaseSelect.Text & lblExpense.Text & "."
        End Select

        Select Case CInt(arrAccStructure(3))
            Case objSys.EnumChartOfAccount.AccountClass
                ddl04.DataSource = BindAccClsCode(pv_strAccClsCode, intSelectIndex)
                ddl04.DataValueField = "AccClsCode"
                ddl04.DataTextField = "Description"
                ddl04.DataBind()
                lblErr04.Text = lblPleaseSelect.Text & lblAccClass.Text & "."
            Case objSys.EnumChartOfAccount.Activity
                ddl04.DataSource = BindActCode(pv_strActCode, intSelectIndex)
                ddl04.DataValueField = "ActCode"
                ddl04.DataTextField = "Description"
                ddl04.DataBind()
                ddl04.AutoPostBack = True
                lblErr04.Text = lblPleaseSelect.Text & lblActivity.Text & "."
                lblDdlActivity.Text = "ddl04"
            Case objSys.EnumChartOfAccount.SubActivity
                ddl04.DataSource = BindSubActCode("", pv_strSubActCode, intSelectIndex)
                ddl04.DataValueField = "SubActCode"
                ddl04.DataTextField = "Description"
                ddl04.DataBind()
                lblErr04.Text = lblPleaseSelect.Text & lblSubActivity.Text & "."
            Case objSys.EnumChartOfAccount.Expense
                ddl04.DataSource = BindExpenseCode(pv_strExpenseCode, intSelectIndex)
                ddl04.DataValueField = "ExpenseCode"
                ddl04.DataTextField = "Description"
                ddl04.DataBind()
                lblErr04.Text = lblPleaseSelect.Text & lblExpense.Text & "."
        End Select

    End Sub

    Sub BuildParam(ByVal pv_strAccStructure, ByRef pr_strAccClsCode, ByRef pr_strActCode, ByRef pr_strSubActCode, ByRef pr_strExpenseCode)
        Dim arrAccStructure(10) As String

        arrAccStructure = Split(pv_strAccStructure, Chr(9))
        Select Case CInt(arrAccStructure(0))
            Case objSys.EnumChartOfAccount.AccountClass
                pr_strAccClsCode = ddl01.SelectedItem.Value
            Case objSys.EnumChartOfAccount.Activity
                pr_strActCode = ddl01.SelectedItem.Value
            Case objSys.EnumChartOfAccount.SubActivity
                pr_strSubActCode = ddl01.SelectedItem.Value
            Case objSys.EnumChartOfAccount.Expense
                pr_strExpenseCode = ddl01.SelectedItem.Value
        End Select

        Select Case CInt(arrAccStructure(1))
            Case objSys.EnumChartOfAccount.AccountClass
                pr_strAccClsCode = ddl02.SelectedItem.Value
            Case objSys.EnumChartOfAccount.Activity
                pr_strActCode = ddl02.SelectedItem.Value
            Case objSys.EnumChartOfAccount.SubActivity
                pr_strSubActCode = ddl02.SelectedItem.Value
            Case objSys.EnumChartOfAccount.Expense
                pr_strExpenseCode = ddl02.SelectedItem.Value
        End Select

        Select Case CInt(arrAccStructure(2))
            Case objSys.EnumChartOfAccount.AccountClass
                pr_strAccClsCode = ddl03.SelectedItem.Value
            Case objSys.EnumChartOfAccount.Activity
                pr_strActCode = ddl03.SelectedItem.Value
            Case objSys.EnumChartOfAccount.SubActivity
                pr_strSubActCode = ddl03.SelectedItem.Value
            Case objSys.EnumChartOfAccount.Expense
                pr_strExpenseCode = ddl03.SelectedItem.Value
        End Select

        Select Case CInt(arrAccStructure(3))
            Case objSys.EnumChartOfAccount.AccountClass
                pr_strAccClsCode = ddl04.SelectedItem.Value
            Case objSys.EnumChartOfAccount.Activity
                pr_strActCode = ddl04.SelectedItem.Value
            Case objSys.EnumChartOfAccount.SubActivity
                pr_strSubActCode = ddl04.SelectedItem.Value
            Case objSys.EnumChartOfAccount.Expense
                pr_strExpenseCode = ddl04.SelectedItem.Value
        End Select

    End Sub

    Sub LoadSubActivity(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arrAccStructure(10) As String
        Dim counter As Integer
        Dim strActCode As String

        GetConfigInfo(strAccStructure, intAccCodeLen, intAccClsLen, intActLen, intSubActLen, intExpenseLen)
        arrAccStructure = Split(strAccStructure, Chr(9))


        For counter = 0 To UBound(arrAccStructure)
            If CInt(arrAccStructure(counter)) = objSys.EnumChartOfAccount.Activity Then
                If counter = 0 Then
                    strActCode = ddl01.SelectedItem.Value
                    Exit For
                ElseIf counter = 1 Then
                    strActCode = ddl02.SelectedItem.Value
                    Exit For
                ElseIf counter = 2 Then
                    strActCode = ddl03.SelectedItem.Value
                    Exit For
                ElseIf counter = 3 Then
                    strActCode = ddl04.SelectedItem.Value
                    Exit For
                End If
            End If
        Next

        If Trim(sender.Id) = Trim(lblDdlActivity.Text) Then
            For counter = 0 To UBound(arrAccStructure)
                If CInt(arrAccStructure(counter)) = objSys.EnumChartOfAccount.SubActivity Then
                    If counter = 0 Then
                        ddl01.DataSource = BindSubActCode(strActCode, "", 0)
                        ddl01.DataValueField = "SubActCode"
                        ddl01.DataTextField = "Description"
                        ddl01.DataBind()
                        lblErr01.Text = lblPleaseSelect.Text & lblSubActivity.Text & "."
                        Exit For
                    ElseIf counter = 1 Then
                        ddl02.DataSource = BindSubActCode(strActCode, "", 0)
                        ddl02.DataValueField = "SubActCode"
                        ddl02.DataTextField = "Description"
                        ddl02.DataBind()
                        lblErr02.Text = lblPleaseSelect.Text & lblSubActivity.Text & "."
                        Exit For
                    ElseIf counter = 2 Then

                        ddl03.DataSource = BindSubActCode(strActCode, "", 0)
                        ddl03.DataValueField = "SubActCode"
                        ddl03.DataTextField = "Description"
                        ddl03.DataBind()
                        lblErr03.Text = lblPleaseSelect.Text & lblSubActivity.Text & "."
                        Exit For
                    ElseIf counter = 3 Then
                        ddl04.DataSource = BindSubActCode(strActCode, "", 0)
                        ddl04.DataValueField = "SubActCode"
                        ddl04.DataTextField = "Description"
                        ddl04.DataBind()
                        lblErr04.Text = lblPleaseSelect.Text & lblSubActivity.Text & "."
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub

    Sub Button_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCd_Upd As String = "GL_CLSSETUP_CHARTOFACCOUNT_LIST_UPD"
        Dim strOpCd_Get As String = "GL_CLSSETUP_CHARTOFACCOUNT_LIST_GET" '"GL_CLSSETUP_CHARTOFACCOUNT_GET_BY_ACCCODE"
        Dim strOpCd_Add As String = "GL_CLSSETUP_CHARTOFACCOUNT_ADD"

        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        Dim strAccClsCode As String = ""
        Dim strActCode As String = ""
        Dim strSubActCode As String = ""
        Dim strExpenseCode As String = ""
        Dim blnValidate As String = ""
        Dim strErrMessage As String = ""
        Dim intNurseryInd As Integer
        Dim intWSAccDistUseInd As Integer
        Dim intMedAccDistUseInd As Integer
        Dim intHousingAccDistUseInd As Integer
        Dim strParamName As String
        Dim strParamValue As String

        If rdMethod.SelectedItem.Value = "1" Then
            If txtAccCode.Text = "" Then
                lblErrAccCode.Visible = True
                Exit Sub
            End If

            If ddlAccType.SelectedItem.Value = "" Then
                lblErrAccType.Visible = True
                Exit Sub
            End If

            If ddlAccGrpCode.SelectedItem.Value = "" Then
                lblErrAccGrpCode.Visible = True
                Exit Sub
            End If
        Else
            If ddlAccType.SelectedItem.Value = "" Then
                lblErrAccType.Visible = True
                Exit Sub
            End If

            If ddlAccGrpCode.SelectedItem.Value = "" Then
                lblErrAccGrpCode.Visible = True
                Exit Sub
            End If

            If ddl01.SelectedItem.Value = "" Then
                lblErr01.Visible = True
                Exit Sub
            End If

            If ddl02.SelectedItem.Value = "" Then
                lblErr02.Visible = True
                Exit Sub
            End If

            If ddl03.SelectedItem.Value = "" Then
                lblErr03.Visible = True
                Exit Sub
            End If

            If ddl04.SelectedItem.Value = "" Then
                lblErr04.Visible = True
                Exit Sub
            End If
        End If


        If ddlCOALevel.SelectedItem.Value = "2" And ddlCOAGeneral.SelectedItem.Value = "" Then
            'wajib memilih coa general
            lblErrCOAGeneral.Visible = True
            Exit Sub
        End If

        If strCmdArgs = "Save" Then
            GetConfigInfo(strAccStructure, intAccCodeLen, intAccClsLen, intActLen, intSubActLen, intExpenseLen)
            If rdMethod.SelectedItem.Value = "1" Then
                If intConfigValue = 1 Then
                    If txtAccCode.Text.Length = intAccCodeLen Then
                        lblErrLen.Visible = False
                    Else
                        lblErrLen.Text = "<br>" & lblAccount.Text & lblCodeShouldBeIn.Text & intAccCodeLen & lblCharacter.Text
                        lblErrLen.Visible = True
                        Exit Sub
                    End If
                End If


                strParamName = "STRSEARCH"
                strParamValue = " AND AccCode = '" & Trim(txtAccCode.Text) & "' " & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

                Try
                    intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get, _
                                                             strParamName, _
                                                             strParamValue, _
                                                             objAccDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_GET_BY_ACCCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                'strParam = Trim(txtAccCode.Text) & "||||acc.AccCode||" & IIf(Session("SS_COACENTRALIZED") = "1", "1", strLocation) & "|" & Trim(ddlCOAGeneral.SelectedItem.Value)
                'Try
                '    intErrNo = objGLSetup.mtdGetAccount(strOpCd_Get, _
                '                                        strParam, _
                '                                        objAccDs, _
                '                                        False)
                'Catch Exp As System.Exception
                '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_GET_BY_ACCCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
                'End Try

                If objAccDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                    lblErrDup.Visible = True
                    Exit Sub
                End If
            Else
                BuildParam(strAccStructure, strAccClsCode, strActCode, strSubActCode, strExpenseCode)
            End If

            strSelectedAccCode = Trim(txtAccCode.Text)
            blnIsUpdate = IIf(intStatus = 0, False, True)
            tbcode.Value = strSelectedAccCode
            intNurseryInd = IIf(cbNursery.Checked = True, objGLSetup.EnumNurseryAccount.Yes, objGLSetup.EnumNurseryAccount.No)

            intWSAccDistUseInd = IIf(cbWSAccDist.Checked = True, objGLSetup.EnumWSAccDistUse.Yes, objGLSetup.EnumWSAccDistUse.No)

            intMedAccDistUseInd = IIf(cbMedAccDist.Checked = True, objGLSetup.EnumMedAccDist.Yes, objGLSetup.EnumMedAccDist.No)
            intHousingAccDistUseInd = IIf(cbHousingAccDist.Checked = True, objGLSetup.EnumHousingAccDist.Yes, objGLSetup.EnumHousingAccDist.No)

            strParam = Trim(txtAccCode.Text) & Chr(9) & _
                        Trim(txtDescription.Text) & Chr(9) & _
                        ddlAccType.SelectedItem.Value & Chr(9) & _
                        ddlAccGrpCode.SelectedItem.Value & Chr(9) & _
                        strAccClsCode & Chr(9) & _
                        strActCode & Chr(9) & _
                        strSubActCode & Chr(9) & _
                        strExpenseCode & Chr(9) & _
                        rdPurpose.SelectedItem.Value & Chr(9) & _
                        intNurseryInd & Chr(9) & _
                        Trim(txtFinAccCode.Text) & Chr(9) & _
                        "0" & Chr(9) & "0" & Chr(9) & _
                        objGLSetup.EnumAccStatus.Active & Chr(9) & _
                        intWSAccDistUseInd & Chr(9) & _
                        intMedAccDistUseInd & Chr(9) & _
                        intHousingAccDistUseInd & Chr(9) & _
                        intConfigValue & Chr(9) & _
                        IIf(Session("SS_COACENTRALIZED") = "1", "", strLocation) & Chr(9) & _
                        ddlCOALevel.SelectedItem.Value & Chr(9) & _
                        ddlCOAGeneral.SelectedItem.Value & Chr(9) & _
                        ddlSaldoNormal.SelectedItem.Value & Chr(9) & _
                        ddlGrpCashflow.SelectedItem.Value

            Try
                intErrNo = objGLSetup.mtdUpdAccount(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    strAccStructure, _
                                                    intAccClsLen, _
                                                    intActLen, _
                                                    intSubActLen, _
                                                    intExpenseLen, _
                                                    strErrMessage, _
                                                    strSelectedAccCode, _
                                                    blnIsUpdate)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_SAVE&errmesg=" & Exp.ToString() & "&redirect=gl/setup/GL_setup_ChartOfAccDet.aspx")
            End Try

            If strErrMessage <> "" Then
                lblErrValidate.Text = "<br>" & strErrMessage
                lblErrValidate.Visible = True
                Exit Sub
            End If

            'If blnIsUpdate = False Then
            '    Response.Redirect("../Setup/GL_Setup_FSList.aspx")
            'End If

        ElseIf strCmdArgs = "Del" Then
            intWSAccDistUseInd = IIf(cbWSAccDist.Checked = True, objGLSetup.EnumWSAccDistUse.Yes, objGLSetup.EnumWSAccDistUse.No)
            intMedAccDistUseInd = IIf(cbMedAccDist.Checked = True, objGLSetup.EnumMedAccDist.Yes, objGLSetup.EnumMedAccDist.No)
            intHousingAccDistUseInd = IIf(cbHousingAccDist.Checked = True, objGLSetup.EnumHousingAccDist.Yes, objGLSetup.EnumHousingAccDist.No)
            strParam = Trim(txtAccCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumAccStatus.Deleted & Chr(9) _
                       & intWSAccDistUseInd & Chr(9) & intMedAccDistUseInd & Chr(9) & intHousingAccDistUseInd & Chr(9) & _
                       intConfigValue & Chr(9) & _
                       IIf(Session("SS_COACENTRALIZED") = "1", "", strLocation) & Chr(9) & _
                       ddlCOALevel.SelectedItem.Value & Chr(9) & _
                       ddlCOAGeneral.SelectedItem.Value & Chr(9) & _
                       ddlSaldoNormal.SelectedItem.Value & Chr(9) & _
                       ddlGrpCashflow.SelectedItem.Value
            Try
                intErrNo = objGLSetup.mtdUpdAccount(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    strAccStructure, _
                                                    intAccClsLen, _
                                                    intActLen, _
                                                    intSubActLen, _
                                                    intExpenseLen, _
                                                    strErrMessage, _
                                                    "", _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_DEL&errmesg=" & Exp.ToString() & "&redirect=gl/setup/GL_setup_ChartOfAccDet.aspx?tbcode=" & strSelectedAccCode)
            End Try
        ElseIf strCmdArgs = "UnDel" Then
            intWSAccDistUseInd = IIf(cbWSAccDist.Checked = True, objGLSetup.EnumWSAccDistUse.Yes, objGLSetup.EnumWSAccDistUse.No)
            intMedAccDistUseInd = IIf(cbMedAccDist.Checked = True, objGLSetup.EnumMedAccDist.Yes, objGLSetup.EnumMedAccDist.No)
            intHousingAccDistUseInd = IIf(cbHousingAccDist.Checked = True, objGLSetup.EnumHousingAccDist.Yes, objGLSetup.EnumHousingAccDist.No)
            strParam = Trim(txtAccCode.Text) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumAccStatus.Active & Chr(9) _
                       & intWSAccDistUseInd & Chr(9) & intMedAccDistUseInd & Chr(9) & intHousingAccDistUseInd & Chr(9) & _
                       intConfigValue & Chr(9) & _
                       IIf(Session("SS_COACENTRALIZED") = "1", "", strLocation) & Chr(9) & _
                       ddlCOALevel.SelectedItem.Value & Chr(9) & _
                       ddlCOAGeneral.SelectedItem.Value & Chr(9) & _
                       ddlSaldoNormal.SelectedItem.Value & Chr(9) & _
                       ddlGrpCashflow.SelectedItem.Value
            Try
                intErrNo = objGLSetup.mtdUpdAccount(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    strAccStructure, _
                                                    intAccClsLen, _
                                                    intActLen, _
                                                    intSubActLen, _
                                                    intExpenseLen, _
                                                    strErrMessage, _
                                                    "", _
                                                    True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_UNDEL&errmesg=" & Exp.ToString() & "&redirect=gl/setup/GL_setup_ChartOfAccDet.aspx?tbcode=" & strSelectedAccCode)
            End Try
        End If
        If strSelectedAccCode <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_ChartOfAcc.aspx")
    End Sub
    sub setMedHousingCheckbox(byval arg as boolean)
        cbMedAccDist.checked=arg
        cbhousingaccDist.checked=arg
    end sub

    sub enableMedHousingCheckbox(byval arg as boolean)
        cbMedAccDist.enabled=arg
        cbhousingaccDist.enabled=arg
    end sub

    Sub Change_Purpose(ByVal Sender As Object, ByVal E As EventArgs)
        if ddlAccType.SelectedIndex <= 0 then
            If rdPurpose.SelectedItem.Value = "1" and cbWSAccDist.checked = false Then
                enableMedHousingCheckbox(true)
            else
                enableMedHousingCheckbox(false)
                setMedHousingCheckbox(false)
            end if
        else
            If rdPurpose.SelectedItem.Value = "1" and _
            ddlAccType.SelectedItem.Value.Trim <> objGLSetup.EnumAccountType.BalanceSheet and _
            cbWSAccDist.checked = false Then

                enableMedHousingCheckbox(true)
            else
                enableMedHousingCheckbox(false)
                setMedHousingCheckbox(false)
            end if
        end if
    end sub

    Sub Change_WSAccDist(ByVal Sender As Object, ByVal E As EventArgs)

        If ddlAccType.SelectedIndex <= 0 Then

            If cbWSAccDist.checked = False And rdPurpose.SelectedItem.Value = "1" Then
                enableMedHousingCheckbox(True)
            Else
                enableMedHousingCheckbox(False)
                setMedHousingCheckbox(False)
            End If
        Else

            If cbWSAccDist.checked = False And _
            ddlAccType.SelectedItem.Value.Trim <> objGLSetup.EnumAccountType.BalanceSheet And _
            rdPurpose.SelectedItem.Value = "1" Then
                enableMedHousingCheckbox(True)
            Else

                enableMedHousingCheckbox(False)
                setMedHousingCheckbox(False)
            End If
        End If
    End Sub

    sub Change_MedAccDist(ByVal Sender As Object, ByVal E As EventArgs)
        if cbMedAccDist.checked then
            cbHousingAccDist.checked=false
            cbHousingAccDist.enabled=false
        else
            cbHousingAccDist.enabled=true
        end if
    end sub

   sub Change_HousingAccDist(ByVal Sender As Object, ByVal E As EventArgs)
        if cbHousingAccDist.checked then
            cbMedAccDist.checked=false
            cbMedAccDist.enabled=false
        else
            cbMedAccDist.enabled=true
        end if
    end sub



End Class
