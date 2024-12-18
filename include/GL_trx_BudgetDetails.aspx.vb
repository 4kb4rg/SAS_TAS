
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Globalization
Imports System.Math 


Public Class GL_trx_BudgetDetails : Inherits Page

    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblPleaseSelect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlGroupCOA As DropDownList
    Protected WithEvents lblErrAccount As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblBlock As Label
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblAccount As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblVehicle As Label
    Protected WithEvents ddlVehCode As DropDownList
    Protected WithEvents lblErrVehicle As Label
    Protected WithEvents lblLastUpdate As Label

    Protected WithEvents txtYearBudget As TextBox
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents txtAmount As TextBox
    Protected WithEvents lblErrAmount As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents lblValueError As Label

    Protected WithEvents txtRp1 As TextBox
    Protected WithEvents txtRp2 As TextBox
    Protected WithEvents txtRp3 As TextBox
    Protected WithEvents txtRp4 As TextBox
    Protected WithEvents txtRp5 As TextBox
    Protected WithEvents txtRp6 As TextBox
    Protected WithEvents txtRp7 As TextBox
    Protected WithEvents txtRp8 As TextBox
    Protected WithEvents txtRp9 As TextBox
    Protected WithEvents txtRp10 As TextBox
    Protected WithEvents txtRp11 As TextBox
    Protected WithEvents txtRp12 As TextBox

    Protected WithEvents txtFS1 As TextBox
    Protected WithEvents txtFS2 As TextBox
    Protected WithEvents txtFS3 As TextBox
    Protected WithEvents txtFS4 As TextBox
    Protected WithEvents txtFS5 As TextBox
    Protected WithEvents txtFS6 As TextBox
    Protected WithEvents txtFS7 As TextBox
    Protected WithEvents txtFS8 As TextBox
    Protected WithEvents txtFS9 As TextBox
    Protected WithEvents txtFS10 As TextBox
    Protected WithEvents txtFS11 As TextBox
    Protected WithEvents txtFS12 As TextBox

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnBack As ImageButton

    Protected WithEvents hidTrxID As HtmlInputHidden

    Dim objAccDs As New Dataset()
    Dim objBlkDs As New Dataset()
    Dim objVehDs As New Dataset()
  
    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intGLAR As Integer
   
    Dim intConfigSetting As Integer

    Dim strBdgYear As String
    Dim strBdgAcc As String
    Dim strLocType As String
    Dim BlockTag As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            lblErrBlock.Visible = False
            lblErrVehicle.Visible = False
            lblValueError.Visible = False

            If Not IsPostBack Then
                If Not Request.QueryString("TrxID") = "" Then
                    hidtrxID.value = Request.QueryString("TrxID")
                End If

                If Not hidtrxID.value = "" Then
                    DisplayData(hidtrxID.value)
                Else
                    BindAccount("")
                    BindBlock("", "")
                    BindVehicle("", "")
                    BindGroupCoa("")
                    EnableControl()
                    btnSave.Visible = True
                    btnDelete.Visible = False
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
       
        Try
            'If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            'lblBlock.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            'BlockTag = GetCaption(objLangCap.EnumLangCap.Block)
            'Else
            lblBlock.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
            BlockTag = GetCaption(objLangCap.EnumLangCap.SubBlock)
            'End If
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GRDET_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=pu/trx/PU_trx_GRList.aspx")
        End Try

        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehicle.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text

        lblErrAccount.Text = "<BR>" & "Please Select " & lblAccount.Text
        lblErrBlock.Text = "Please Select " & lblBlock.Text
        lblErrVehicle.Text = "Please Select " & lblVehicle.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_AssetAddDETAILS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetAddDetails.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Protected Function LoadData(ByVal vstrTrxID As String) As DataSet

        Dim strOpCode As String = "GL_CLSTRX_BUDGET_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        strParamName = "TRXID"
        strParamValue = vstrTrxID

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BUDGET_GET_DATA&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list.aspx")
        End Try

        Return objDataSet

    End Function

    Sub DisableControl()
       
        ddlAccount.Enabled = False
        ddlBlock.Enabled = False
        ddlVehCode.Enabled = False
        txtYearBudget.Enabled = False
        txtRp1.Enabled = False
        txtRp2.Enabled = False
        txtRp3.Enabled = False
        txtRp4.Enabled = False
        txtRp5.Enabled = False
        txtRp6.Enabled = False
        txtRp7.Enabled = False
        txtRp8.Enabled = False
        txtRp9.Enabled = False
        txtRp10.Enabled = False
        txtRp11.Enabled = False
        txtRp12.Enabled = False

        txtFS1.Enabled = False
        txtFS2.Enabled = False
        txtFS3.Enabled = False
        txtFS4.Enabled = False
        txtFS5.Enabled = False
        txtFS6.Enabled = False
        txtFS7.Enabled = False
        txtFS8.Enabled = False
        txtFS9.Enabled = False
        txtFS10.Enabled = False
        txtFS11.Enabled = False
        txtFS12.Enabled = False

        txtAmount.Enabled = False

    End Sub

    Sub EnableControl()
        ddlAccount.Enabled = True
        ddlBlock.Enabled = True
        ddlVehCode.Enabled = True
        txtYearBudget.Enabled = True

        txtRp1.Enabled = True
        txtRp2.Enabled = True
        txtRp3.Enabled = True
        txtRp4.Enabled = True
        txtRp5.Enabled = True
        txtRp6.Enabled = True
        txtRp7.Enabled = True
        txtRp8.Enabled = True
        txtRp9.Enabled = True
        txtRp10.Enabled = True
        txtRp11.Enabled = True
        txtRp12.Enabled = True

        txtFS1.Enabled = True
        txtFS2.Enabled = True
        txtFS3.Enabled = True
        txtFS4.Enabled = True
        txtFS5.Enabled = True
        txtFS6.Enabled = True
        txtFS7.Enabled = True
        txtFS8.Enabled = True
        txtFS9.Enabled = True
        txtFS10.Enabled = True
        txtFS11.Enabled = True
        txtFS12.Enabled = True

        txtAmount.Enabled = True

    End Sub

    Sub DisplayData(ByVal vstrTrxID As String)

        Dim dsTx As DataSet = LoadData(vstrTrxID)
        Dim strAccCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strGroupCoa As String

        If dsTx.Tables(0).Rows.Count > 0 Then

            lblStatus.Text = objGLSetup.mtdGetActSatus(Trim(dsTx.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.GetLongDate(dsTx.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsTx.Tables(0).Rows(0).Item("Username"))


            txtYearBudget.Text = Trim(dsTx.Tables(0).Rows(0).Item("AccYear"))
            txtAmount.Text = dsTx.Tables(0).Rows(0).Item("TotalAmount")

          
            txtRp1.Text = dsTx.Tables(0).Rows(0).Item("Rp1")
            txtRp2.Text = dsTx.Tables(0).Rows(0).Item("Rp2")
            txtRp3.Text = dsTx.Tables(0).Rows(0).Item("Rp3")
            txtRp4.Text = dsTx.Tables(0).Rows(0).Item("Rp4")
            txtRp5.Text = dsTx.Tables(0).Rows(0).Item("Rp5")
            txtRp6.Text = dsTx.Tables(0).Rows(0).Item("Rp6")
            txtRp7.Text = dsTx.Tables(0).Rows(0).Item("Rp7")
            txtRp8.Text = dsTx.Tables(0).Rows(0).Item("Rp8")
            txtRp9.Text = dsTx.Tables(0).Rows(0).Item("Rp9")
            txtRp10.Text = dsTx.Tables(0).Rows(0).Item("Rp10")
            txtRp11.Text = dsTx.Tables(0).Rows(0).Item("Rp11")
            txtRp12.Text = dsTx.Tables(0).Rows(0).Item("Rp12")

            txtFS1.Text = dsTx.Tables(0).Rows(0).Item("FS1")
            txtFS2.Text = dsTx.Tables(0).Rows(0).Item("FS2")
            txtFS3.Text = dsTx.Tables(0).Rows(0).Item("FS3")
            txtFS4.Text = dsTx.Tables(0).Rows(0).Item("FS4")
            txtFS5.Text = dsTx.Tables(0).Rows(0).Item("FS5")
            txtFS6.Text = dsTx.Tables(0).Rows(0).Item("FS6")
            txtFS7.Text = dsTx.Tables(0).Rows(0).Item("FS7")
            txtFS8.Text = dsTx.Tables(0).Rows(0).Item("FS8")
            txtFS9.Text = dsTx.Tables(0).Rows(0).Item("FS9")
            txtFS10.Text = dsTx.Tables(0).Rows(0).Item("FS10")
            txtFS11.Text = dsTx.Tables(0).Rows(0).Item("FS11")
            txtFS12.Text = dsTx.Tables(0).Rows(0).Item("FS12")

            strAccCode = Trim(dsTx.Tables(0).Rows(0).Item("AccCode"))
            strBlkCode = Trim(dsTx.Tables(0).Rows(0).Item("BlkCode"))
            strVehCode = Trim(dsTx.Tables(0).Rows(0).Item("VehCode"))
            strGroupCoa = Trim(dsTx.Tables(0).Rows(0).Item("GroupCoa"))
            BindAccount(strAccCode)
            BindBlock(strAccCode, strBlkCode)
            BindVehicle(strAccCode, strVehCode)
            BindGroupCoa(strGroupCoa)

            Select Case Trim(lblStatus.Text)
                Case objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.Active)
                    EnableControl()
                    btnSave.Visible = True
                    btnDelete.Visible = True
                    btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objGLSetup.mtdGetActSatus(objGLSetup.EnumActStatus.Deleted)
                    DisableControl()
                    btnSave.Visible = False
                    btnDelete.Visible = False
            End Select
        End If
    End Sub

    Sub BindAccount(ByVal pv_strAccCode As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "' --AND ACC.LOCCODE='" & strLocation & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = lblPleaseSelect.Text & lblAccount.Text
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "_Description"
        ddlAccount.DataBind()
        ddlAccount.SelectedIndex = intSelectedIndex
        'ddlAccount.AutoPostBack = True
    End Sub

    Sub BindGroupCoa(ByVal pv_strGroupCoa As String)
        Dim strOpCd As String = "GL_GROUP_COA_LIST_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer = 0

        strParamName = "LocCode"
        strParamValue = strLocation

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETADD_ACCCODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccDs.Tables(0).Rows.Count - 1
            If objAccDs.Tables(0).Rows(intCnt).Item("GroupCoa") = Trim(pv_strGroupCoa) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objAccDs.Tables(0).NewRow()
        dr("GroupCoa") = ""
        dr("GroupName") = "Please Select Group COA"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlGroupCOA.DataSource = objAccDs.Tables(0)
        ddlGroupCOA.DataValueField = "GroupCoa"
        ddlGroupCOA.DataTextField = "GroupName"
        ddlGroupCOA.DataBind()
        ddlGroupCOA.SelectedIndex = intSelectedIndex
    End Sub

    Sub onSelect_Account(ByVal Sender As Object, ByVal E As EventArgs)
        Dim blnIsBalanceSheet As Boolean
        Dim blnIsBlockRequire As Boolean
        Dim blnIsVehicleRequire As Boolean
        Dim blnIsOthers As Boolean
        Dim intNurseryInd As Integer

        GetAccountDetails(ddlAccount.SelectedItem.Value, blnIsBalanceSheet, blnIsBlockRequire, blnIsVehicleRequire, blnIsOthers, intNurseryInd)

        If Not blnIsBalanceSheet Then
            If blnIsBlockRequire Then
                BindBlock(ddlAccount.SelectedItem.Value, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
            Else
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
            End If
            If blnIsVehicleRequire Then
                BindVehicle(ddlAccount.SelectedItem.Value, ddlVehCode.SelectedItem.Value)
            End If
            'If blnIsOthers Then
            '    lblVehicleOption.Text = True
            '    BindVehicle("%", ddlVehCode.SelectedItem.Value)
            '    BindVehicleExpense(False, ddlVehExpCode.SelectedItem.Value)
            'Else
            '    lblVehicleOption.Text = False
            'End If
        Else
            If blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.Yes Then
                BindBlock(ddlAccount.SelectedItem.Value, ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
            ElseIf blnIsBalanceSheet And intNurseryInd = objGLSetup.EnumNurseryAccount.No Then
                BindBlock("", ddlBlock.SelectedItem.Value)
                BindVehicle("", ddlVehCode.SelectedItem.Value)
            End If
        End If
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
            pr_strNurseryInd = objGLSetup.EnumNurseryAccount.No
            intErrNo = objGLSetup.mtdGetAccount(strOpCd, _
                                                strParam, _
                                                _objAccDs, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_GET_ACCOUNT_DETAILS&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If _objAccDs.Tables(0).Rows.Count = 1 Then
            If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccType")) = objGLSetup.EnumAccountType.BalanceSheet Then
                pr_IsBalanceSheet = True
                If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("NurseryInd")) = objGLSetup.EnumNurseryAccount.Yes Then
                    pr_strNurseryInd = objGLSetup.EnumNurseryAccount.Yes
                End If
            End If
            If Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.NonVehicle Then
                pr_IsBlockRequire = True
            ElseIf Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.VehicleDistribution Then
                pr_IsVehicleRequire = True
            ElseIf Convert.ToInt16(_objAccDs.Tables(0).Rows(0).Item("AccPurpose")) = objGLSetup.EnumAccountPurpose.Others Then
                pr_IsBlockRequire = True
                pr_IsOthers = True
            End If
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
            
            strOpCd = "GL_CLSSETUP_ACCOUNT_SUBBLOCK_GET"
            strParam = pv_strAccCode & "|" & Session("SS_LOCATION") & "|" & objGLSetup.EnumSubBlockStatus.Active

            intErrNo = objGLSetup.mtdGetAccountBlock(strOpCd, _
                                                     strParam, _
                                                     objBlkDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & objBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = pv_strBlkCode.Trim() Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        dr("BlkCode") = ""
        dr("_Description") = lblPleaseSelect.Text & BlockTag & lblCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objVehDs.Tables(0).Rows.Count - 1
            objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = objVehDs.Tables(0).Rows(intCnt).Item("VehCode").Trim()
            objVehDs.Tables(0).Rows(intCnt).Item("Description") = objVehDs.Tables(0).Rows(intCnt).Item("VehCode") & " (" & objVehDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objVehDs.Tables(0).Rows(intCnt).Item("VehCode") = pv_strVehCode.Trim() Then
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

    
    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCode As String
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim objRslSet As New DataSet()

        'validasi 
        If Val(txtAmount.Text) <> Val(txtRp1.Text) + Val(txtRp2.Text) + Val(txtRp3.Text) + Val(txtRp4.Text) + _
                                  Val(txtRp5.Text) + Val(txtRp6.Text) + Val(txtRp7.Text) + Val(txtRp8.Text) + _
                                  Val(txtRp9.Text) + Val(txtRp10.Text) + Val(txtRp11.Text) + Val(txtRp12.Text) Then

            lblValueError.Visible = True
            lblValueError.Text = "Total Amount On Month Budget Not Same With Control Amount."
            Exit Sub
        End If

        If ddlBlock.Items.Count > 1 And ddlBlock.SelectedValue = "" Then
            lblErrBlock.Visible = True
            Exit Sub
        End If

        If ddlVehCode.Items.Count > 1 And ddlVehCode.SelectedValue = "" Then
            lblErrVehicle.Visible = True
            Exit Sub
        End If


        If trim(lblStatus.Text) <> "" Then
            strOpCode = "GL_CLSTRX_BUDGET_UPDATE"
        Else
            strOpCode = "GL_CLSTRX_BUDGET_ADD"
        End If

        strParamName = "TRXID|LOCCODE|ACCYEAR|ACCCODE|BLOKCODE|VEHCODE|AMOUNT|USERID|STATUS|GroupCoa|"
        strParamName = strParamName & "RP1|RP2|RP3|RP4|RP5|RP6|"
        strParamName = strParamName & "RP7|RP8|RP9|RP10|RP11|RP12|"
        strParamName = strParamName & "FS1|FS2|FS3|FS4|FS5|FS6|"
        strParamName = strParamName & "FS7|FS8|FS9|FS10|FS11|FS12"

        strParamValue = hidTrxID.Value & "|" & strLocation & "|" & txtYearBudget.Text & "|" & ddlAccount.SelectedValue & "|"
        strParamValue = strParamValue & ddlBlock.SelectedValue & "|" & ddlVehCode.SelectedValue & "|"
        strParamValue = strParamValue & txtAmount.Text & "|" & strUserId & "|" & objGLSetup.EnumActStatus.Active & "|" & ddlGroupCOA.SelectedValue & "|"
        strParamValue = strParamValue & txtRp1.Text & "|" & txtRp2.Text & "|" & txtRp3.Text & "|"
        strParamValue = strParamValue & txtRp4.Text & "|" & txtRp5.Text & "|" & txtRp6.Text & "|"
        strParamValue = strParamValue & txtRp7.Text & "|" & txtRp8.Text & "|" & txtRp9.Text & "|"
        strParamValue = strParamValue & txtRp10.Text & "|" & txtRp11.Text & "|" & txtRp12.Text & "|"

        strParamValue = strParamValue & txtFS1.Text & "|" & txtFS2.Text & "|" & txtFS3.Text & "|"
        strParamValue = strParamValue & txtFS4.Text & "|" & txtFS5.Text & "|" & txtFS6.Text & "|"
        strParamValue = strParamValue & txtFS7.Text & "|" & txtFS8.Text & "|" & txtFS9.Text & "|"
        strParamValue = strParamValue & txtFS10.Text & "|" & txtFS11.Text & "|" & txtFS12.Text

        Try

            If trim(lblStatus.Text) <> "" Then
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                       strParamName, _
                                                       strParamValue)

            Else

                intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objRslSet)

                hidTrxID.value = objRslSet.Tables(0).Rows(0).Item("ID")

            End If

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/GL_trx_Budget_list")

        Finally

            DisplayData(hidTrxID.value)

        End Try


    End Sub

   
    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCode As String = "GL_CLSTRX_BUDGET_DELETED"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

       
        strParamName = "TRXID|STATUS|USERID"
        strParamValue = hidTrxID.Value & "|" & objGLSetup.EnumActStatus.Deleted & "|" & strUserId
       

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                   strParamName, _
                                                   strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_BUDGET_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=gl/trx/gl_trx_budget_list")

        Finally

            DisplayData(hidTrxID.Value)

        End Try


    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Response.Redirect("gl_trx_budget_list.aspx")

    End Sub

End Class
