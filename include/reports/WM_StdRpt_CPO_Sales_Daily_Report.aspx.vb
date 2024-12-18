Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports System.Xml
Imports System.Web.Services

Public Class WM_StdRpt_CPO_Sales_Daily_Report : Inherits Page

    Protected RptSelect As UserControl

    Dim objWM As New agri.WM.clsReport()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim strAcceptFormat As String
    Dim objWMSetup As New agri.WM.clsSetup()

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label

    Protected WithEvents lblLocation As Label
    Protected WithEvents txtSupplier As TextBox
    Protected WithEvents lstProdCode As DropDownList
    Protected WithEvents lstRptType As DropDownList
    Protected WithEvents lstBillParty As DropDownList
    Protected WithEvents lstTransport As DropDownList

    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents txtContractNo As TextBox

    Dim objBuyerDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strCompanyName As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strUserName As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String

    Dim strLocType As String
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()

                lstRptType.Items.Clear()
                lstRptType.Items.Add(New ListItem("Detail", "1"))
                lstRptType.Items.Add(New ListItem("Summary", "2"))
                BindProduct()
                BindSupplier("")
                BindTransporterList("")
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucTrMthYr As HtmlTableRow
        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = True
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=WM_STDRPT_DAILYFFBRCV_BYSUPP_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/WM_StdRpt_Selection.aspx")
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

    Sub BindProduct()
        Dim strOpCode As String = "WM_CLSSETUP_PRODUCT_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objBankDs As New DataSet()
        Dim intSelectedBankIndex As Integer = 0
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "STRSEARCH"
        strParamValue = "Order BY ProductName ASC"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objBankDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ID_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        'For intCnt = 0 To objBankDs.Tables(0).Rows.Count - 1
        '    If Trim(objBankDs.Tables(0).Rows(intCnt).Item("BankCode")) = Trim(pv_strBankCode) Then
        '        intSelectedBankIndex = intCnt + 1
        '        Exit For
        '    End If
        'Next intCnt

        Dim dr As DataRow
        dr = objBankDs.Tables(0).NewRow()
        dr("GrpCode") = ""
        dr("ProductName") = "Please select Product"
        objBankDs.Tables(0).Rows.InsertAt(dr, 0)

        lstProdCode.DataSource = objBankDs.Tables(0)
        lstProdCode.DataValueField = "GrpCode"
        lstProdCode.DataTextField = "ProductName"
        lstProdCode.DataBind()
        lstProdCode.SelectedIndex = intSelectedBankIndex
    End Sub

    Sub BindSupplier(ByVal pv_strBuyerCode As String)
        Dim strParam As String
        Dim strOpCdGet As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        strParam = "" & "|" & _
                   "" & "|" & _
                   objGLSetup.EnumBillPartyStatus.Active & "|" & _
                   "" & "|" & _
                   "BP.Name" & "|" & _
                   "ASC" & "|"
        Try
            intErrNo = objGLSetup.mtdGetBillParty(strOpCdGet, strParam, objBuyerDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_BUYERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=CM/Trx/CM_Trx_ContractRegList.aspx")
        End Try

        If objBuyerDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBuyerDs.Tables(0).Rows.Count - 1
                objBuyerDs.Tables(0).Rows(intCnt).Item("BillPartyCode") = Trim(objBuyerDs.Tables(0).Rows(intCnt).Item("BillPartyCode"))                
                objBuyerDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objBuyerDs.Tables(0).Rows(intCnt).Item("NamePPN"))
                If objBuyerDs.Tables(0).Rows(intCnt).Item("BillPartyCode") = pv_strBuyerCode Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objBuyerDs.Tables(0).NewRow()
        dr("BillPartyCode") = ""
        'dr("Name") = lblSelect.Text & lblBillParty.Text
        objBuyerDs.Tables(0).Rows.InsertAt(dr, 0)

        lstBillParty.DataSource = objBuyerDs.Tables(0)
        lstBillParty.DataValueField = "BillPartyCode"
        lstBillParty.DataTextField = "Name"
        lstBillParty.DataBind()
        lstBillParty.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindTransporterList(ByVal pv_strBuyerCode As String)
        Dim strParam As String
        Dim strOpCdGet As String = "WM_CLSSETUP_TRANSPORTER_GET"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer

        strParam = "||" & objWMSetup.EnumTransporterStatus.Active & "|||TransporterCode|"
        Try
            intErrNo = objWMSetup.mtdGetTransporter(strOpCdGet, strParam, objBuyerDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_TRANSPORTER_DROPDOWNLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        If objBuyerDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objBuyerDs.Tables(0).Rows.Count - 1
                objBuyerDs.Tables(0).Rows(intCnt).Item("TransporterCode") = Trim(objBuyerDs.Tables(0).Rows(intCnt).Item("TransporterCode"))
                objBuyerDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objBuyerDs.Tables(0).Rows(intCnt).Item("TransporterCode")) & " (" & Trim(objBuyerDs.Tables(0).Rows(intCnt).Item("Name")) & ")"
                If objBuyerDs.Tables(0).Rows(intCnt).Item("TransporterCode") = pv_strBuyerCode Then
                    intSelectedIndex = intCnt + 1
                End If
            Next
        End If

        dr = objBuyerDs.Tables(0).NewRow()
        dr("TransporterCode") = ""
        dr("Name") = "Please Select Transporter Code"
        objBuyerDs.Tables(0).Rows.InsertAt(dr, 0)

        lstTransport.DataSource = objBuyerDs.Tables(0)
        lstTransport.DataValueField = "TransporterCode"
        lstTransport.DataTextField = "Name"
        lstTransport.DataBind()
        lstTransport.SelectedIndex = intSelectedIndex
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strCustCode As String
        Dim strProdCode As String


        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim tempAccMth As DropDownList
        Dim tempAccYr As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden

        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)

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

        strCustCode = lstBillParty.SelectedItem.Value
        strProdCode = Trim(lstProdCode.SelectedItem.Value)

        Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_CPO_Sales_Daily_Preview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&strddlAccMth=" & strddlAccMth & _
                       "&strddlAccYr=" & strddlAccYr & _
                       "&Decimal=" & strDec & _
                       "&lblLocation=" & lblLocation.Text & _
                       "&RptType=" & lstRptType.SelectedItem.Value & _
                       "&CustCode=" & strCustCode & _
                       "&ProdCode=" & strProdCode & _
                       "&DateFrom=" & strDate & _
                       "&DateTo=" & strDateTo & _
                       "&ContractNo=" & txtContractNo.Text & _
                       "&Transporter=" & lstTransport.SelectedItem.Value & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Function blnValidEndStartDate(ByVal pv_strEndDate As String, ByVal pv_strStartDate As String) As Boolean
        blnValidEndStartDate = False
        If CDate(pv_strStartDate) <= CDate(pv_strEndDate) Then
            blnValidEndStartDate = True
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DAILY_GET&errmesg=" & Exp.Message & "&redirect=CB_StdRpt_BankTransaction.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

End Class

