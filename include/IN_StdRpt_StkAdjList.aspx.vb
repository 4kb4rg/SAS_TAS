Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports System.Xml
Imports System.Web.Services

Public Class IN_StdRpt_StkAdjList : Inherits Page

    Protected RptSelect As UserControl

    Dim objIN As New agri.IN.clsReport()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblProdTypeCode As Label
    Protected WithEvents lblProdBrandCode As Label
    Protected WithEvents lblProdModelCode As Label
    Protected WithEvents lblProdCatCode As Label
    Protected WithEvents lblProdMatCode As Label
    Protected WithEvents lblStkAnaCode As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblVehType As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtStkAdjIDFrom As TextBox
    Protected WithEvents txtStkAdjIDTo As TextBox
    Protected WithEvents txtAdjRefNo As TextBox
    Protected WithEvents txtProdType As TextBox
    Protected WithEvents txtProdBrand As TextBox
    Protected WithEvents txtProdModel As TextBox
    Protected WithEvents txtProdCat As TextBox
    Protected WithEvents txtProdMaterial As TextBox
    Protected WithEvents txtStkAna As TextBox
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents txtAccCode As TextBox
    Protected WithEvents txtVehType As TextBox
    Protected WithEvents txtVehCode As TextBox
    Protected WithEvents txtVehExpCode As TextBox
    Protected WithEvents ddlBlkType As DropDownList
    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox
    Protected WithEvents ddlAdjType As DropDownList
    Protected WithEvents ddlTransType As DropDownList
    Protected WithEvents ddlStatus As DropDownList

    Protected WithEvents ibPrintPreview As ImageButton
    Protected WithEvents trBlkGrp As HtmlTableRow
    Protected WithEvents trBlk As HtmlTableRow
    Protected WithEvents trSubBlk As HtmlTableRow

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                GetLangCap()
                BindAdjustmentTypeDropDownList()
                BindTransactionTypeDropDownList()
                BindBlockTypeDropDownList()
                BindStatusDropDownList()
            End If

            SetBlockAccessibilityByBlockType()
        End If
    End Sub

    Sub GetLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAccCode As String
        Dim strPreBlkCode As String
        Dim strBlkCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_TRX_STOCKADJ_DET_GET_LANGCAP&errmesg=&redirect=IN/trx/IN_trx_StockAdj_list.aspx")
        End Try

        lblProdTypeCode.Text = GetCaption(objLangCap.EnumLangCap.ProdType) & " Code"
        lblProdBrandCode.Text = GetCaption(objLangCap.EnumLangCap.ProdBrand) & " Code"
        lblProdModelCode.Text = GetCaption(objLangCap.EnumLangCap.ProdModel) & " Code"
        lblProdCatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdCat) & " Code"
        lblProdMatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdMat) & " Code"
        lblStkAnaCode.Text = GetCaption(objLangCap.EnumLangCap.StockAnalysis) & " Code"
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType) & " Code"
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"
        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Type"
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.BlockGrp)
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code"
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)
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
    
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrFromTo As HtmlTableRow
        Dim ucTrMthYr As HtmlTableRow

        ucTrFromTo = RptSelect.FindControl("trFromTo")
        ucTrFromTo.Visible = True

        ucTrMthYr = RptSelect.FindControl("trMthYr")
        ucTrMthYr.Visible = True
    End Sub
    
    Sub SetBlockAccessibilityByBlockType()
        If ddlBlkType.SelectedItem.Value = "BlkCode" Then
            trBlkGrp.Visible = False
            trBlk.Visible = True
            trSubBlk.Visible = False
        ElseIf ddlBlkType.SelectedItem.Value = "BlkGrp" Then
            trBlkGrp.Visible = True
            trBlk.Visible = False
            trSubBlk.Visible = False
        ElseIf ddlBlkType.SelectedItem.Value = "SubBlkCode" Then
            trBlkGrp.Visible = False
            trBlk.Visible = False
            trSubBlk.Visible = True
        End If
    End Sub
    
    Sub BindAdjustmentTypeDropDownList()
        ddlAdjType.Items.Clear()
        ddlAdjType.Items.Add(New ListItem(objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.All), objINTrx.EnumAdjustmentType.All))
        ddlAdjType.Items.Add(New ListItem(objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.Quantity), objINTrx.EnumAdjustmentType.Quantity))
        ddlAdjType.Items.Add(New ListItem(objINTrx.mtdGetAdjustmentType(objINTrx.EnumAdjustmentType.TotalCost), objINTrx.EnumAdjustmentType.TotalCost))
        ddlAdjType.SelectedIndex = 0
    End Sub

    Sub BindTransactionTypeDropDownList()
        ddlTransType.Items.Clear()
        ddlTransType.Items.Add(New ListItem(objINTrx.mtdGetTransactionType(objINTrx.EnumTransactionType.All), objINTrx.EnumTransactionType.All))
        ddlTransType.Items.Add(New ListItem(objINTrx.mtdGetTransactionType(objINTrx.EnumTransactionType.NewValue), objINTrx.EnumTransactionType.NewValue))
        ddlTransType.Items.Add(New ListItem(objINTrx.mtdGetTransactionType(objINTrx.EnumTransactionType.Difference), objINTrx.EnumTransactionType.Difference))
        ddlTransType.SelectedIndex = 0
    End Sub
    
    Sub BindBlockTypeDropDownList()
        ddlBlkType.Items.Clear()
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            ddlBlkType.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.BlockGrp), "BlkGrp"))
            ddlBlkType.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.Block) & " Code", "BlkCode"))
        Else
            ddlBlkType.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.Block) & " Code", "BlkCode"))
            ddlBlkType.Items.Add(New ListItem(GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code", "SubBlkCode"))
        End If
        ddlBlkType.SelectedIndex = 1
    End Sub
    
    Sub BindStatusDropDownList()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem(objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.All), objINTrx.EnumStockAdjustStatus.All))
        ddlStatus.Items.Add(New ListItem(objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Active), objINTrx.EnumStockAdjustStatus.Active))
        ddlStatus.Items.Add(New ListItem(objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Closed), objINTrx.EnumStockAdjustStatus.Closed))
        ddlStatus.Items.Add(New ListItem(objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Confirmed), objINTrx.EnumStockAdjustStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objINTrx.mtdGetStockAdjustStatus(objINTrx.EnumStockAdjustStatus.Deleted), objINTrx.EnumStockAdjustStatus.Deleted))
        ddlStatus.SelectedIndex = 3
    End Sub
    
    Sub ddlBlkType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetBlockAccessibilityByBlockType
    End Sub
    
    Sub ibPrintPreview_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strStkAdjIDFrom As String
        Dim strStkAdjIDTo As String
        Dim strAdjType As String
        Dim strTransType As String
        Dim strRefNo As String
        Dim strProdType As String
        Dim strProdBrand As String
        Dim strProdModel As String
        Dim strProdCat As String
        Dim strProdMat As String
        Dim strStkAna As String
        Dim strItemCode As String
        Dim strAccCode As String
        Dim strVehTypeCode As String
        Dim strVehCode As String
        Dim strVehExpCode As String
        Dim strBlkType As String
        Dim strBlkGrpCode As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String
        Dim strStatus As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempDateFrom = RptSelect.FindControl("txtDateFrom")
        strDateFrom = Trim(tempDateFrom.Text)
        tempDateTo = RptSelect.FindControl("txtDateTo")
        strDateTo = Trim(tempDateTo.Text)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)

        If strUserLoc = "" Then
            templblUL = RptSelect.FindControl("lblUserLoc")
            templblUL.Visible = True
            Exit Sub
        Else
            If Left(strUserLoc, 3) = "','" Then
                strUserLoc = Right(strUserLoc, Len(strUserLoc) - 3)
            ElseIf Right(strUserLoc, 3) = "','" Then
                strUserLoc = Left(strUserLoc, Len(strUserLoc) - 3)
            End If
        End If
        
        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_STKADJ_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If Not (objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, strDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, strDateTo) = True) Then
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If
        
        strStkAdjIDFrom = txtStkAdjIDFrom.Text.Trim()
        strStkAdjIDTo = txtStkAdjIDTo.Text.Trim()
        strAdjType = ddlAdjType.SelectedItem.Value.Trim()
        strTransType = ddlTransType.SelectedItem.Value.Trim()
        strRefNo = txtAdjRefNo.Text.Trim()
        strProdType = txtProdType.Text.Trim()
        strProdBrand = txtProdBrand.Text.Trim()
        strProdModel = txtProdModel.Text.Trim()
        strProdCat = txtProdCat.Text.Trim()
        strProdMat = txtProdMaterial.Text.Trim()
        strStkAna = txtStkAna.Text.Trim()
        strItemCode = txtItemCode.Text.Trim()
        strAccCode = txtAccCode.Text.Trim()
        strVehTypeCode = txtVehType.Text.Trim()
        strVehCode = txtVehCode.Text.Trim()
        strVehExpCode = txtVehExpCode.Text.Trim()
        strBlkType = ddlBlkType.SelectedItem.Value.Trim()
        strBlkGrpCode = txtBlkGrp.Text.Trim()
        strBlkCode = txtBlkCode.Text.Trim()
        strSubBlkCode = txtSubBlkCode.Text.Trim()
        strStatus = ddlStatus.SelectedItem.Value.Trim()
        
        strDateFrom = Server.UrlEncode(strDateFrom)
        strDateTo = Server.UrlEncode(strDateTo)
        strStkAdjIDFrom = Server.UrlEncode(strStkAdjIDFrom)
        strStkAdjIDTo = Server.UrlEncode(strStkAdjIDTo)
        strAdjType = Server.UrlEncode(strAdjType)
        strTransType = Server.UrlEncode(strTransType)
        strRefNo = Server.UrlEncode(strRefNo)
        strProdType = Server.UrlEncode(strProdType)
        strProdBrand = Server.UrlEncode(strProdBrand)
        strProdModel = Server.UrlEncode(strProdModel)
        strProdCat = Server.UrlEncode(strProdCat)
        strProdMat = Server.UrlEncode(strProdMat)
        strStkAna = Server.UrlEncode(strStkAna)
        strItemCode = Server.UrlEncode(strItemCode)
        strAccCode = Server.UrlEncode(strAccCode)
        strVehTypeCode = Server.UrlEncode(strVehTypeCode)
        strVehCode = Server.UrlEncode(strVehCode)
        strVehExpCode = Server.UrlEncode(strVehExpCode)
        strBlkGrpCode = Server.UrlEncode(strBlkGrpCode)
        strBlkCode = Server.UrlEncode(strBlkCode)
        strSubBlkCode = Server.UrlEncode(strSubBlkCode)
        
        Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_StkAdjListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                        "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & _
                        "&StkAdjIDFrom=" & strStkAdjIDFrom & "&StkAdjIDTo=" & strStkAdjIDTo & "&AdjType=" & strAdjType & "&TransType=" & strTransType & "&RefNo=" & strRefNo & "&Status=" & strStatus & _
                        "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & _
                        "&AccCode=" & strAccCode & "&VehTypeCode=" & strVehTypeCode & "&VehCode=" & strVehCode & "&VehExpCode=" & strVehExpCode & "&BlkType=" & strBlkType & "&BlkGrp=" & strBlkGrpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & _
                        "&lblProdTypeCode=" & lblProdTypeCode.Text & "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & "&lblStkAnaCode=" & lblStkAnaCode.Text & _
                        "&lblAccCode=" & lblAccCode.Text & "&lblVehTypeCode=" & lblVehType.Text & "&lblVehCode=" & lblVehCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblBlkType=" & lblBlkType.Text & "&lblBlkGrp=" & lblBlkGrp.Text & _
                        "&lblBlkCode=" & lblBlkCode.Text & "&lblSubBlkCode=" & lblSubBlkCode.Text & "&lblLocation=" & lblLocation.Text & _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
