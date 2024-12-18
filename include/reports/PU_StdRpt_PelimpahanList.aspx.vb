
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

Public Class PU_StdRpt_PelimpahanList : Inherits Page

    Protected RptSelect As UserControl

    Dim objPU As New agri.PU.clsReport()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objAdm As New agri.Admin.clsLoc()


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

    Protected WithEvents txtSupplier As TextBox
    Protected WithEvents txtDocNoFrom As TextBox
    Protected WithEvents txtDocNoTo As TextBox
    Protected WithEvents txtProdType As TextBox
    Protected WithEvents txtProdBrand As TextBox
    Protected WithEvents txtProdModel As TextBox
    Protected WithEvents txtProdCat As TextBox
    Protected WithEvents txtProdMaterial As TextBox
    Protected WithEvents txtStkAna As TextBox
    Protected WithEvents txtItemCode As TextBox
    Protected WithEvents lstPelimpahanType As DropDownList
    Protected WithEvents lstStatus As DropDownList

    Protected WithEvents TRDocDateFromTo As HtmlTableRow
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

    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        lblDate.Visible = False
        lblDateFormat.Visible = False

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindPelimpahanType()
                BindStatus()

            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrDocDateFromTo As HtmlTableRow
        Dim ucTrMthYr As HtmlTableRow

        UCTrDocDateFromTo = RptSelect.FindControl("TRDocDateFromTo")
        UCTrDocDateFromTo.Visible = True

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_POLIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PU_StdRpt_Selection.aspx")
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


    Sub BindPelimpahanType()



        lstPelimpahanType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.All),objAdm.EnumLocLevel.All)) 
        lstPelimpahanType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.Estate),objAdm.EnumLocLevel.Estate)) 
        lstPelimpahanType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.Perwakilan),objAdm.EnumLocLevel.Perwakilan)) 
        lstPelimpahanType.Items.Add(New ListItem(objAdm.mtdGetLocLevel(objAdm.EnumLocLevel.HQ),objAdm.EnumLocLevel.HQ)) 


    End Sub

    Sub BindStatus()

        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.All), objPUTrx.EnumPOStatus.All))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Active), objPUTrx.EnumPOStatus.Active))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Cancelled), objPUTrx.EnumPOStatus.Cancelled))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Closed), objPUTrx.EnumPOStatus.Closed))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Confirmed), objPUTrx.EnumPOStatus.Confirmed))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Deleted), objPUTrx.EnumPOStatus.Deleted))
        lstStatus.Items.Add(New ListItem(objPUTrx.mtdGetPOStatus(objPUTrx.EnumPOStatus.Invoiced), objPUTrx.EnumPOStatus.Invoiced))
        lstStatus.SelectedIndex = 4

    End Sub





    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strSupplier As String
        Dim strDocNoFrom As String
        Dim strDocNoTo As String
        Dim strProdType As String
        Dim strProdBrand As String
        Dim strProdModel As String
        Dim strProdCat As String
        Dim strProdMat As String
        Dim strStkAna As String
        Dim strItemCode As String
        Dim strPelimpahanType As String
        Dim strStatus As String
        Dim strLnStatus As String

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strDocDateFrom As String
        Dim strDocDateTo As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempDocDateFrom As TextBox
        Dim tempDocDateTo As TextBox
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDocDateFrom As String
        Dim objDocDateTo As String

        tempAccMth = RptSelect.FindControl("lstAccMonth")
        strddlAccMth = Trim(tempAccMth.SelectedItem.Value)
        tempAccYr = RptSelect.FindControl("lstAccYear")
        strddlAccYr = Trim(tempAccYr.SelectedItem.Value)
        tempDocDateFrom = RptSelect.FindControl("txtDateFrom")
        strDocDateFrom = Trim(tempDocDateFrom.Text)
        tempDocDateTo = RptSelect.FindControl("txtDateTo")
        strDocDateTo = Trim(tempDocDateTo.Text)
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

        If txtSupplier.Text = "" Then
            strSupplier = ""
        Else
            strSupplier = Trim(txtSupplier.Text)
        End If

        If txtDocNoFrom.Text = "" Then
            strDocNoFrom = ""
        Else
            strDocNoFrom = Trim(txtDocNoFrom.Text)
        End If

        If txtDocNoTo.Text = "" Then
            strDocNoTo = ""
        Else
            strDocNoTo = Trim(txtDocNoTo.Text)
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

        strPelimpahanType = Trim(lstPelimpahanType.SelectedItem.Value)
        strStatus = Trim(lstStatus.SelectedItem.Value)

        strSupplier = Server.UrlEncode(strSupplier)
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_POLIST_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../../en/reports/PU_StdRpt_Selection.aspx")
        End Try

        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If Not (strDocDateFrom = "" And strDocDateTo = "") Then
            If objGlobal.mtdValidInputDate(strDateSetting, strDocDateFrom, objDateFormat, objDocDateFrom) = True And objGlobal.mtdValidInputDate(strDateSetting, strDocDateTo, objDateFormat, objDocDateTo) = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""PU_StdRpt_PelimpahanListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                               "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblProdTypeCode=" & lblProdTypeCode.Text & "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & _
                               "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & "&lblStkAnaCode=" & lblStkAnaCode.Text & "&lblLocation=" & lblLocation.Text & "&Supplier=" & strSupplier & _
                               "&DocNoFrom=" & strDocNoFrom & "&DocNoTo=" & strDocNoTo & "&DocDateFrom=" & objDocDateFrom & "&DocDateTo=" & objDocDateTo & "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & _
                               "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & "&PelimpahanType=" & strPelimpahanType & "&Status=" & strStatus & _
                               "&LnStatus=" & strLnStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
            Else
                lblDateFormat.Text = objDateFormat & "."
                lblDate.Visible = True
                lblDateFormat.Visible = True
            End If
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""PU_StdRpt_PelimpahanListPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                           "&DDLAccMth=" & strddlAccMth & "&DDLAccYr=" & strddlAccYr & "&lblProdTypeCode=" & lblProdTypeCode.Text & "&lblProdBrandCode=" & lblProdBrandCode.Text & "&lblProdModelCode=" & lblProdModelCode.Text & _
                           "&lblProdCatCode=" & lblProdCatCode.Text & "&lblProdMatCode=" & lblProdMatCode.Text & "&lblStkAnaCode=" & lblStkAnaCode.Text & "&lblLocation=" & lblLocation.Text & "&Supplier=" & strSupplier & _
                           "&DocNoFrom=" & strDocNoFrom & "&DocNoTo=" & strDocNoTo & "&DocDateFrom=" & objDocDateFrom & "&DocDateTo=" & objDocDateTo & "&ProdType=" & strProdType & "&ProdBrand=" & strProdBrand & _
                           "&ProdModel=" & strProdModel & "&ProdCat=" & strProdCat & "&ProdMat=" & strProdMat & "&StkAna=" & strStkAna & "&ItemCode=" & strItemCode & "&PelimpahanType=" & strPelimpahanType & "&Status=" & strStatus & _
                           "&LnStatus=" & strLnStatus & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

End Class
