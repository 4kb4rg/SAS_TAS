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

Public Class NU_StdRpt_MutasiBibit : Inherits Page

    Protected RptSelect As UserControl

    Dim objIN As New agri.IN.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objINSetup As New agri.IN.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

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
    Protected WithEvents lblLocation As Label

    Protected WithEvents txtProdType As TextBox
    Protected WithEvents txtProdBrand As TextBox
    Protected WithEvents txtProdModel As TextBox
    Protected WithEvents txtProdCat As TextBox
    Protected WithEvents txtProdMaterial As TextBox
    Protected WithEvents txtStkAna As TextBox
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents txtDocIDFrom As TextBox
    Protected WithEvents txtDocIDTo As TextBox
    Protected WithEvents lstTransType As DropDownList

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents chkInventoryBin As CheckBox
    Protected WithEvents ddlInventoryBin As DropDownList
    Protected WithEvents txtFromTrxDate As TextBox
    Protected WithEvents txtToTrxDate As TextBox
    Protected WithEvents ddlRptType As DropDownList

    Protected WithEvents cbExcel As CheckBox

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strWS As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")
        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindTransTypeList()
                BindInventoryBinLevel("")
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = False

    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
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

    Sub BindTransTypeList()
        Dim strText = "All"

        lstTransType.Items.Add(New ListItem(strText))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockTransfer) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.StockTransfer) & ")", objGlobal.EnumDocType.StockTransfer))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockAdjust) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.StockAdjust) & ")", objGlobal.EnumDocType.StockAdjust))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockIssue) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.StockIssue) & ")", objGlobal.EnumDocType.StockIssue))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReceive) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.StockReceive) & ")", objGlobal.EnumDocType.StockReceive))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturnAdvice) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.StockReturnAdvice) & ")", objGlobal.EnumDocType.StockReturnAdvice))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.StockReturn) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.StockReturn) & ")", objGlobal.EnumDocType.StockReturn))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReceive) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.GoodsReceive) & ")", objGlobal.EnumDocType.GoodsReceive))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.GoodsReturn) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.GoodsReturn) & ")", objGlobal.EnumDocType.GoodsReturn))
        lstTransType.Items.Add(New ListItem(objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.DispatchAdvice) & " (" & objGlobal.mtdGetDocName(objGlobal.EnumDocType.DispatchAdvice) & ")", objGlobal.EnumDocType.DispatchAdvice))
        lstTransType.SelectedIndex = -1
    End Sub

    Sub BindInventoryBinLevel(ByVal pv_strInvBin As String)
        Dim strText = "All"
        ddlInventoryBin.Items.Add(New ListItem(strText, "0"))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.HO), objINSetup.EnumInventoryBinLevel.HO))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Central), objINSetup.EnumInventoryBinLevel.Central))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.Other), objINSetup.EnumInventoryBinLevel.Other))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinI), objINSetup.EnumInventoryBinLevel.BinI))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinII), objINSetup.EnumInventoryBinLevel.BinII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIII), objINSetup.EnumInventoryBinLevel.BinIII))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinIV), objINSetup.EnumInventoryBinLevel.BinIV))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinV), objINSetup.EnumInventoryBinLevel.BinV))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVI), objINSetup.EnumInventoryBinLevel.BinVI))
        ddlInventoryBin.Items.Add(New ListItem(objINSetup.mtdGetInventoryBinLevel(objINSetup.EnumInventoryBinLevel.BinVII), objINSetup.EnumInventoryBinLevel.BinVII))

        ddlInventoryBin.SelectedIndex = -1
    End Sub

    'Sub chkInventoryBin_Checked(ByVal Sender As Object, ByVal E As EventArgs)
    '    If chkInventoryBin.Checked = True Then
    '        ddlInventoryBin.Enabled = False
    '    Else
    '        ddlInventoryBin.Enabled = True
    '    End If
    'End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strParam As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strDateSetting As String
        Dim objDateFrom As String
        Dim objDateTo As String
        Dim objDateFormat As String
        Dim objSysCfgDs As New Object()
        Dim strSDate_Trx As String
        Dim strEDate_Trx As String
        Dim strBin As String
        Dim strItemCode As String
        Dim strRptType As String

        Dim strExportToExcel As String

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
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

        strSDate_Trx = txtFromTrxDate.Text
        strEDate_Trx = txtToTrxDate.Text
        strBin = ddlInventoryBin.SelectedItem.Value
        strItemCode = txtItemCode.Text
        strRptType = ddlRptType.SelectedItem.Value

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=AR_STDRPT_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (Trim(strSDate_Trx) = "" And Trim(strEDate_Trx) = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strSDate_Trx, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strEDate_Trx, objDateFormat, objDateTo) = True Then
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
                Exit Sub
            End If
        End If

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        Response.Write("<Script Language=""JavaScript"">window.open(""NU_StdRpt_MutasiBibitPreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&RptID=" & strRptID & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&ItemCode=" & strItemCode & _
                       "&SDate_Trx=" & objDateFrom & _
                       "&EDate_Trx=" & objDateTo & _
                       "&RptType=" & strRptType & _
                       "&ExportToExcel=" & strExportToExcel & _
                       "&Bin=" & strBin & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
