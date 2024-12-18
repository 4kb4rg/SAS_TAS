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

Public Class IN_StdRpt_StockReturnListing : Inherits Page

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

    Protected WithEvents lblBillPartyCode As Label
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

    Protected WithEvents txtStockReturnIDFrom As TextBox
    Protected WithEvents txtStockReturnIDTo As TextBox
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
    Protected WithEvents lstBlkType As DropDownList
    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox
    Protected WithEvents lstStatus As DropDownList

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow

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
                onload_GetLangCap()
                BlkTypeList()
                BindStatus()

            End If

            If lstBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If

        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrFromTo As HtmlTableRow
        Dim ucTrMthYr As HtmlTableRow

        UCTrFromTo = RptSelect.FindControl("trfromto")
        UCTrFromTo.Visible = True

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & " Code"
        lblProdTypeCode.Text = GetCaption(objLangCap.EnumLangCap.ProdType) & " Code :"
        lblProdBrandCode.Text = GetCaption(objLangCap.EnumLangCap.ProdBrand) & " Code :"
        lblProdModelCode.Text = GetCaption(objLangCap.EnumLangCap.ProdModel) & " Code :"
        lblProdCatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdCat) & " Code :"
        lblProdMatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdMat) & " Code :"
        lblStkAnaCode.Text = GetCaption(objLangCap.EnumLangCap.StockAnalysis) & " Code :"
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account) & " Code :"
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType) & " Code :"
        lblVehCode.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & " Code :"
        lblVehExpCode.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & " Code :"
        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Type :"
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Group :"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code :"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code :"
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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

    Sub BlkTypeList()

        Dim strBlkGrp As String
        Dim strBlk As String
        Dim strSubBlk As String

        strBlkGrp = Left(lblBlkGrp.Text, Len(lblBlkGrp.Text) - 2)
        strBlk = Left(lblBlkCode.Text, Len(lblBlkCode.Text) - 2)
        strSubBlk = Left(lblSubBlkCode.Text, Len(lblSubBlkCode.Text) - 2)

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlkGrp, "BlkGrp"))
        Else
            lstBlkType.Items.Add(New ListItem(strSubBlk, "SubBlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
        End If

    End Sub

    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockReturnStatus(objINTrx.EnumStockReturnStatus.All), objINTrx.EnumStockReturnStatus.All))
        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockReturnStatus(objINTrx.EnumStockReturnStatus.Active), objINTrx.EnumStockReturnStatus.Active))
        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockReturnStatus(objINTrx.EnumStockReturnStatus.Confirmed), objINTrx.EnumStockReturnStatus.Confirmed))
        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockReturnStatus(objINTrx.EnumStockReturnStatus.Deleted), objINTrx.EnumStockReturnStatus.Deleted))
        lstStatus.Items.Add(New ListItem(objINTrx.mtdGetStockReturnStatus(objINTrx.EnumStockReturnStatus.Closed), objINTrx.EnumStockReturnStatus.Closed))
        lstStatus.SelectedIndex = 4

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStockReturnIDFrom As String
        Dim strStockReturnIDTo As String
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
        Dim strDateFrom As String
        Dim strDateTo As String
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
        Dim objDateFrom As String
        Dim objDateTo As String

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

        If txtStockReturnIDFrom.Text = "" Then
            strStockReturnIDFrom = ""
        Else
            strStockReturnIDFrom = Trim(txtStockReturnIDFrom.Text)
        End If

        If txtStockReturnIDTo.Text = "" Then
            strStockReturnIDTo = ""
        Else
            strStockReturnIDTo = Trim(txtStockReturnIDTo.Text)
        End If

        If txtProdType.Text = "" Then
            strProdType = ""
        Else
            strProdType = Trim(txtProdType.Text)
        End If

        If txtProdBrand.Text = "" Then
            strProdBrand = ""
        Else
            strProdBrand = Trim(txtProdBrand.Text)
        End If

        If txtProdModel.Text = "" Then
            strProdModel = ""
        Else
            strProdModel = Trim(txtProdModel.Text)
        End If

        If txtProdCat.Text = "" Then
            strProdCat = ""
        Else
            strProdCat = Trim(txtProdCat.Text)
        End If

        If txtProdMaterial.Text = "" Then
            strProdMat = ""
        Else
            strProdMat = Trim(txtProdMaterial.Text)
        End If

        If txtStkAna.Text = "" Then
            strStkAna = ""
        Else
            strStkAna = Trim(txtStkAna.Text)
        End If

        If txtItemCode.Text = "" Then
            strItemCode = ""
        Else
            strItemCode = Trim(txtItemCode.Text)
        End If

        If txtAccCode.Text = "" Then
            strAccCode = ""
        Else
            strAccCode = Trim(txtAccCode.Text)
        End If

        If txtVehType.Text = "" Then
            strVehTypeCode = ""
        Else
            strVehTypeCode = Trim(txtVehType.Text)
        End If

        If txtVehCode.Text = "" Then
            strVehCode = ""
        Else
            strVehCode = Trim(txtVehCode.Text)
        End If

        If txtVehExpCode.Text = "" Then
            strVehExpCode = ""
        Else
            strVehExpCode = Trim(txtVehExpCode.Text)
        End If

        strBlkType = Trim(lstBlkType.SelectedItem.Value)

        If txtBlkGrp.Text = "" Then
            strBlkGrpCode = ""
        Else
            strBlkGrpCode = Trim(txtBlkGrp.Text)
        End If

        If txtBlkCode.Text = "" Then
            strBlkCode = ""
        Else
            strBlkCode = Trim(txtBlkCode.Text)
        End If

        If txtSubBlkCode.Text = "" Then
            strSubBlkCode = ""
        Else
            strSubBlkCode = Trim(txtSubBlkCode.Text)
        End If


        strStatus = Trim(lstStatus.SelectedItem.Value)

        strStockReturnIDFrom = Server.UrlEncode(strStockReturnIDFrom)
        strStockReturnIDTo = Server.UrlEncode(strStockReturnIDTo)
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

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_STOCKRETURN_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/IN_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_StockReturnListingPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                               "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblBillPartyCode=" & lblBillPartyCode.Text & "&lblProdTypeCode=" & lblProdTypeCode.Text & _
                               "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & "&lblStkAnaCode=" & lblStkAnaCode.Text & _
                               "&lblAccCode=" & lblAccCode.Text & "&lblVehTypeCode=" & lblVehType.Text & "&lblVehCode=" & lblVehCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblBlkType=" & lblBlkType.Text & "&lblBlkGrp=" & lblBlkGrp.Text & _
                               "&lblBlkCode=" & lblBlkCode.Text & "&lblSubBlkCode=" & lblSubBlkCode.Text & "&lblLocation=" & lblLocation.Text & "&StockReturnIDFrom=" & strStockReturnIDFrom & "&StockReturnIDTo=" & strStockReturnIDTo & _
                               "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & "&AccCode=" & strAccCode & _
                               "&VehTypeCode=" & strVehTypeCode & "&VehCode=" & strVehCode & "&VehExpCode=" & strVehExpCode & "&BlkType=" & strBlkType & "&BlkGrp=" & strBlkGrpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & "&Status=" & strStatus & _
                               """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""IN_StdRpt_StockReturnListingPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                           "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblBillPartyCode=" & lblBillPartyCode.Text & "&lblProdTypeCode=" & lblProdTypeCode.Text & _
                           "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & "&lblStkAnaCode=" & lblStkAnaCode.Text & _
                           "&lblAccCode=" & lblAccCode.Text & "&lblVehTypeCode=" & lblVehType.Text & "&lblVehCode=" & lblVehCode.Text & "&lblVehExpCode=" & lblVehExpCode.Text & "&lblBlkType=" & lblBlkType.Text & "&lblBlkGrp=" & lblBlkGrp.Text & _
                           "&lblBlkCode=" & lblBlkCode.Text & "&lblSubBlkCode=" & lblSubBlkCode.Text & "&lblLocation=" & lblLocation.Text & "&StockReturnIDFrom=" & strStockReturnIDFrom & "&StockReturnIDTo=" & strStockReturnIDTo & _
                           "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & "&AccCode=" & strAccCode & _
                           "&VehTypeCode=" & strVehTypeCode & "&VehCode=" & strVehCode & "&VehExpCode=" & strVehExpCode & "&BlkType=" & strBlkType & "&BlkGrp=" & strBlkGrpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & "&Status=" & strStatus & _
                           """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
