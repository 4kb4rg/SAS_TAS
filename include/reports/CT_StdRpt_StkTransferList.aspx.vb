
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

Public Class CT_StdRpt_StkTransferList : Inherits Page

    Protected RptSelect As UserControl

    Dim objCTTrx As New agri.CT.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    
    Protected WithEvents lblToLocation As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblProdTypeCode As Label
    Protected WithEvents lblProdBrandCode As Label
    Protected WithEvents lblProdModelCode As Label
    Protected WithEvents lblProdCatCode As Label
    Protected WithEvents lblProdMatCode As Label
    Protected WithEvents lblStkAnaCode As Label
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtStkTransferIdFrom As TextBox
    Protected WithEvents txtStkTransferIdTo As TextBox    
    Protected WithEvents txtToLocation As TextBox    
    Protected WithEvents txtProdType As TextBox
    Protected WithEvents txtProdBrand As TextBox
    Protected WithEvents txtProdModel As TextBox
    Protected WithEvents txtProdCat As TextBox
    Protected WithEvents txtProdMaterial As TextBox
    Protected WithEvents txtStkAna As TextBox
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents lstStatus As DropDownList

    Protected WithEvents PrintPrev As ImageButton

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
                BindStatus()
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
        lblToLocation.Text = GetCaption(objLangCap.EnumLangCap.Location) & " Code :"
        lblProdTypeCode.Text = GetCaption(objLangCap.EnumLangCap.ProdType) & " Code :"
        lblProdBrandCode.Text = GetCaption(objLangCap.EnumLangCap.ProdBrand) & " Code :"
        lblProdModelCode.Text = GetCaption(objLangCap.EnumLangCap.ProdModel) & " Code :"
        lblProdCatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdCat) & " Code :"
        lblProdMatCode.Text = GetCaption(objLangCap.EnumLangCap.ProdMat) & " Code :"
        lblStkAnaCode.Text = GetCaption(objLangCap.EnumLangCap.StockAnalysis) & " Code :"
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & Exp.ToString() & "&redirect=../en/reports/CT_StdRpt_Selection.aspx")
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
        lstStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockTransferStatus(objCTTrx.EnumStockTransferStatus.All), objCTTrx.EnumStockTransferStatus.All))
        lstStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockTransferStatus(objCTTrx.EnumStockTransferStatus.Active), objCTTrx.EnumStockTransferStatus.Active))
        lstStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockTransferStatus(objCTTrx.EnumStockTransferStatus.Confirmed), objCTTrx.EnumStockTransferStatus.Confirmed))
        lstStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockTransferStatus(objCTTrx.EnumStockTransferStatus.Cancelled), objCTTrx.EnumStockTransferStatus.Cancelled))
        lstStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockTransferStatus(objCTTrx.EnumStockTransferStatus.DbNote), objCTTrx.EnumStockTransferStatus.DbNote))
        lstStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockTransferStatus(objCTTrx.EnumStockTransferStatus.Deleted), objCTTrx.EnumStockTransferStatus.Deleted))
        lstStatus.Items.Add(New ListItem(objCTTrx.mtdGetStockTransferStatus(objCTTrx.EnumStockTransferStatus.Closed), objCTTrx.EnumStockTransferStatus.Closed))
        lstStatus.SelectedIndex = 2
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStkTransferIdFrom As String
        Dim strStkTransferIdTo As String
        Dim strToLocation As String
        Dim strProdType As String
        Dim strProdBrand As String
        Dim strProdModel As String
        Dim strProdCat As String
        Dim strProdMat As String
        Dim strStkAna As String
        Dim strItemCode As String
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

        If txtStkTransferIdFrom.Text = "" Then
            strStkTransferIdFrom = ""
        Else
            strStkTransferIdFrom = Trim(txtStkTransferIdFrom.Text)
        End If

        If txtStkTransferIdTo.Text = "" Then
            strStkTransferIdTo = ""
        Else
            strStkTransferIdTo = Trim(txtStkTransferIdTo.Text)
        End If

        If txtToLocation.Text = "" Then
            strToLocation = ""
        Else
            strToLocation = Trim(txtToLocation.Text)
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

        strStatus = Trim(lstStatus.SelectedItem.Value)

        strStkTransferIdFrom = Server.UrlEncode(strStkTransferIdFrom)
        strStkTransferIdTo = Server.UrlEncode(strStkTransferIdTo)
        strProdType = Server.UrlEncode(strProdType)
        strProdBrand = Server.UrlEncode(strProdBrand)
        strProdModel = Server.UrlEncode(strProdModel)
        strProdCat = Server.UrlEncode(strProdCat)
        strProdMat = Server.UrlEncode(strProdMat)
        strStkAna = Server.UrlEncode(strStkAna)
        strItemCode = Server.UrlEncode(strItemCode)

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_STKTRANSFER_GET_CONFIG_DATE&errmesg=" & Exp.ToString() & "&redirect=../en/reports/CT_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDateFrom = "" And strDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""CT_StdRpt_StkTransferListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                            "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & _
                            "&lblProdTypeCode=" & lblProdTypeCode.Text & "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & _
                            "&lblStkAnaCode=" & lblStkAnaCode.Text & "&StkTransferIDFrom=" & strStkTransferIdFrom & "&StkTransferIDTo=" & strStkTransferIdTo & "&ToLocation=" & strToLocation & _
                            "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & _
                            "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""CT_StdRpt_StkTransferListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                           "&Decimal=" & strDec & "&lblLocation=" & lblLocation.Text & "&DateFrom=" & objDateFrom & "&DateTo=" & objDateTo & "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & _
                           "&lblProdTypeCode=" & lblProdTypeCode.Text & "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & _
                           "&lblStkAnaCode=" & lblStkAnaCode.Text & "&StkTransferIDFrom=" & strStkTransferIdFrom & "&StkTransferIDTo=" & strStkTransferIdTo & "&ToLocation=" & strToLocation & _
                           "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & _
                           "&Status=" & strStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If

    End Sub

End Class
