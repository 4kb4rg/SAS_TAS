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

Public Class CT_StdRpt_CanteenReturnListing : Inherits Page

    Protected RptSelect As UserControl

    Dim objCT As New agri.CT.clsReport()
    Dim objCTTrx As New agri.CT.clsTrx()
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
    Protected WithEvents lblBillPartyCode As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblVehExpCode As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtCanteenReturnIDFrom As TextBox
    Protected WithEvents txtCanteenReturnIDTo As TextBox
    Protected WithEvents txtProdType As TextBox
    Protected WithEvents txtProdBrand As TextBox
    Protected WithEvents txtProdModel As TextBox
    Protected WithEvents txtProdCat As TextBox
    Protected WithEvents txtProdMaterial As TextBox
    Protected WithEvents txtStkAna As TextBox
    Protected WithEvents txtEmpCode As TextBox
    Protected WithEvents txtBillPartyCode As TextBox
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents ddlStatus As DropDownList

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim strDateSetting As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatus()
            End If
        End If
    End Sub


    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrFromTo As HtmlTableRow
        Dim ucTrMthYr As HtmlTableRow

        UCTrFromTo = RptSelect.FindControl("TrFromTo")
        UCTrFromTo.Visible = True

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblProdTypeCode.Text = GetCaption(objLangCap.EnumLangCap.ProdType) & " Code"
        lblProdBrandCode.Text = GetCaption(objLangCap.EnumLangCap.ProdBrand) & " Code"
        lblProdModelCode.Text = GetCaption(objLangCap.EnumLangCap.ProdModel) & " Code"
        lblProdCatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdCat) & " Code"
        lblProdMatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdMat) & " Code"
        lblStkAnaCode.Text = GetCaption(objLangCap.EnumLangCap.StockAnalysis) & " Code"
        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & " Code"
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code"
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code"
        Else
            lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code"
        End If
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code"
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code"
        lblLocation.Text = GetCaption(objLangCap.EnumLangCap.Location)

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_STKISS_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/CT_StdRpt_Selection.aspx")
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
	                'Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
	                Exit For
	            End If
	        Next
	 End Function
	


    Sub BindStatus()
        ddlStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockReturnStatus(objCTTrx.EnumStockReturnStatus.All), objCTTrx.EnumStockReturnStatus.All))
        ddlStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockReturnStatus(objCTTrx.EnumStockReturnStatus.Active), objCTTrx.EnumStockReturnStatus.Active))
        ddlStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockReturnStatus(objCTTrx.EnumStockReturnStatus.Closed), objCTTrx.EnumStockReturnStatus.Closed))
        ddlStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockReturnStatus(objCTTrx.EnumStockReturnStatus.Confirmed), objCTTrx.EnumStockReturnStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockReturnStatus(objCTTrx.EnumStockReturnStatus.Deleted), objCTTrx.EnumStockReturnStatus.Deleted))
        ddlStatus.SelectedIndex = 3
    End Sub
    
    Public Sub GetDateSetting()
        Dim intErrNo As Integer
        Dim strOpCode As String
        Dim objSysCfgDs As New Object()
        
        strOpCode = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strOpCode, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CT_CANTEEN_RETURN_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/CT_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strCanteenReturnIDFrom As String
        Dim strCanteenReturnIDTo As String
        Dim strProdType As String
        Dim strProdBrand As String
        Dim strProdModel As String
        Dim strProdCat As String
        Dim strProdMat As String
        Dim strStkAna As String
        Dim strEmpCode As String
        Dim strBillPartyCode As String
        Dim strItemCode As String
        Dim strStatus As String

        Dim strAccMonth As String
        Dim strAccYear As String
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDecimal As String

        Dim tempddl As DropDownList
        Dim temptxt As TextBox
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim objDateFormat As String
        Dim objDateFrom As String
        Dim objDateTo As String

        tempddl = RptSelect.FindControl("lstAccMonth")
        strAccMonth = Trim(tempddl.SelectedItem.Value)
        tempddl = RptSelect.FindControl("lstAccYear")
        strAccYear = Trim(tempddl.SelectedItem.Value)
        temptxt = RptSelect.FindControl("txtDateFrom")
        strDateFrom = Trim(temptxt.Text)
        temptxt = RptSelect.FindControl("txtDateTo")
        strDateTo = Trim(temptxt.Text)
        tempddl = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempddl.SelectedItem.Value)
        strRptName = Trim(tempddl.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempddl = RptSelect.FindControl("lstDecimal")
        strDecimal = Trim(tempddl.SelectedItem.Value)

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
        
        If strDateFrom = "" Or strDateTo = "" Then
            strDateFrom = ""
            strDateTo = ""
            lblDate.Visible = False
            lblDateFormat.Visible = False
        Else
            GetDateSetting()
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = False Or objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = False Then
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            Else
                strDateFrom = objDateFrom
                strDateTo = objDateTo
                lblDate.Visible = False
                lblDateFormat.Visible = False
            End If
        End If
        strRptID = Server.UrlEncode(Trim(strRptID))
        strRptName = Server.UrlEncode(Trim(strRptName))
        strUserLoc = Server.UrlEncode(Trim(strUserLoc))
        strAccMonth = Server.UrlEncode(Trim(strAccMonth))
        strAccYear = Server.UrlEncode(Trim(strAccYear))
        strDateFrom = Server.UrlEncode(Trim(strDateFrom))
        strDateTo = Server.UrlEncode(Trim(strDateTo))
        strStatus = Server.UrlEncode(Trim(ddlStatus.SelectedItem.Value))

        strCanteenReturnIDFrom = Server.UrlEncode(Trim(txtCanteenReturnIDFrom.Text))
        strCanteenReturnIDTo = Server.UrlEncode(Trim(txtCanteenReturnIDTo.Text))
        strProdType = Server.UrlEncode(Trim(txtProdType.Text))
        strProdBrand = Server.UrlEncode(Trim(txtProdBrand.Text))
        strProdModel = Server.UrlEncode(Trim(txtProdModel.Text))
        strProdCat = Server.UrlEncode(Trim(txtProdCat.Text))
        strProdMat = Server.UrlEncode(Trim(txtProdMaterial.Text))
        strStkAna = Server.UrlEncode(Trim(txtStkAna.Text))
        strEmpCode = Server.UrlEncode(Trim(txtEmpCode.Text))
        strBillPartyCode = Server.UrlEncode(Trim(txtBillPartyCode.Text))
        strItemCode = Server.UrlEncode(Trim(txtItemCode.Text))

        Response.Write("<Script Language=""JavaScript"">window.open(""CT_StdRpt_CanteenReturnListingPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDecimal & _
                       "&DateFrom=" & strDateFrom & "&DateTo=" & strDateTo & "&AccMonth=" & strAccMonth & "&AccYear=" & strAccYear & "&lblLocation=" & lblLocation.Text & "&lblBlkCode=" & lblBlkCode.Text & "&lblBillPartyCode=" & lblBillPartyCode.Text & _
                       "&lblProdTypeCode=" & lblProdTypeCode.Text & "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & _
                       "&lblStkAnaCode=" & lblStkAnaCode.Text & "&lblAccCode=" & lblAccCode.Text & "&lblVehCode=" & lblVehCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & _
                       "&CanteenReturnIDFrom=" & strCanteenReturnIDFrom & "&CanteenReturnIDTo=" & strCanteenReturnIDTo & _
                       "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & _
                       "&EmpCode=" & strEmpCode & "&BillPartyCode=" & strBillPartyCode & "&ItemCode=" & strItemCode & "&Status=" & strStatus & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
