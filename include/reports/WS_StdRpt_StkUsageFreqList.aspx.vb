Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class WS_StdRpt_StkUsageFreqList : Inherits Page

    Protected RptSelect As UserControl

    Dim objIN As New agri.IN.clsReport()
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objINTrx As New agri.IN.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblIss As Label
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
    Protected WithEvents txtLastIssDateFrom As TextBox
    Protected WithEvents txtLastIssDateTo As TextBox
    Protected WithEvents lstOrderBy As DropDownList
    Protected WithEvents lstSortBy As DropDownList
    Protected WithEvents rbIssAll As RadioButton
    Protected WithEvents rbIss As RadioButton
    Protected WithEvents rbNIss As RadioButton

    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strLocType as String


    Dim intErrNo As Integer

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
        lblIss.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindSortByList()
            End If
        End If
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PWSYSTEM_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WS_StdRpt_Selection.aspx")
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

    Sub BindSortByList()
        lstSortBy.Items.Add(New ListItem("Item Code", "ItemCode"))
        lstSortBy.Items.Add(New ListItem("No of Days", "NoOfDays"))
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strProdType As String
        Dim strProdBrand As String
        Dim strProdModel As String
        Dim strProdCat As String
        Dim strProdMat As String
        Dim strStkAna As String
        Dim strItemCode As String
        Dim strLastIssDateFrom As String
        Dim strLastIssDateTo As String
        Dim strOrderBy As String
        Dim strSortBy As String
        Dim strIss As String

        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

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

        strLastIssDateFrom = txtLastIssDateFrom.Text
        strLastIssDateTo = txtLastIssDateTo.Text
        strOrderBy = Trim(lstOrderBy.SelectedItem.Value)
        strSortBy = Trim(lstSortBy.SelectedItem.Value)

        If rbIssAll.Checked Then
            strIss = rbIssAll.Text
        ElseIf rbIss.Checked Then
            strIss = rbIss.Text
        ElseIf rbNIss.Checked Then
            strIss = rbNIss.Text
        End If

        If strIss = "Issued Before" And strLastIssDateFrom <> "" And strLastIssDateTo <> "" Then
            lblIss.Visible = True
            Exit Sub
        End If

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
            Response.Redirect("../mesg/ErrorMessage.aspx?errcode=IN_STDRPT_STKUSGFREQ_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WS_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strLastIssDateFrom = "" And strLastIssDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strLastIssDateFrom, objDateFormat, objDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strLastIssDateTo, objDateFormat, objDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_StkUsageFreqListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                               "&Decimal=" & strDec & "&lblProdTypeCode=" & lblProdTypeCode.Text & "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&lblProdCatCode=" & lblProdCatCode.Text & _
                               "&lblProdMatCode=" & lblProdMatCode.Text & "&lblStkAnaCode=" & lblStkAnaCode.Text & "&lblLocation=" & lblLocation.Text & "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & _
                               "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & "&SortBy=" & strSortBy & "&OrderBy=" & strOrderBy & "&Issue=" & strIss & "&LastIssDateFrom=" & objDateFrom & _
                               "&LastIssDateTo=" & objDateTo & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""WS_StdRpt_StkUsageFreqListPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                           "&Decimal=" & strDec & "&lblProdTypeCode=" & lblProdTypeCode.Text & "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & "&lblProdCatCode=" & lblProdCatCode.Text & _
                           "&lblProdMatCode=" & lblProdMatCode.Text & "&lblStkAnaCode=" & lblStkAnaCode.Text & "&lblLocation=" & lblLocation.Text & "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & "&ProdModel=" & strProdModel & _
                           "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & "&SortBy=" & strSortBy & "&OrderBy=" & strOrderBy & "&Issue=" & strIss & "&LastIssDateFrom=" & objDateFrom & _
                           "&LastIssDateTo=" & objDateTo & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If

    End Sub

End Class
