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

Public Class WM_StdRpt_Daily_FFBRcv_BySupp : Inherits Page

    Protected RptSelect As UserControl

    Dim objWM As New agri.WM.clsReport()
    Dim objWMTrx As New agri.WM.clsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents cbExcel As CheckBox

    Protected WithEvents lblLocation As Label
    Protected WithEvents txtSupplier As TextBox
    Protected WithEvents lstSuppType As DropDownList
    Protected WithEvents lstRptType As DropDownList
    Protected WithEvents lblErrDateInMsg As Label

    Protected WithEvents srchDateIn As TextBox
    Protected WithEvents srchDateTo As TextBox
    Protected WithEvents btnSelDateFrom As Image
    Protected WithEvents btnSelDateTo As Image
    Protected WithEvents lblTo As Label
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCapDs As New Object()

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
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                srchDateIn.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                srchDateTo.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                onload_GetLangCap()
                BindSuppType()
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
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Sub BindSuppType()
        lstSuppType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.All), objPUSetup.EnumSupplierType.All))
        lstSuppType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Associate), objPUSetup.EnumSupplierType.Associate))
        lstSuppType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Contractor), objPUSetup.EnumSupplierType.Contractor))
        lstSuppType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.External), objPUSetup.EnumSupplierType.External))
        lstSuppType.Items.Add(New ListItem(objPUSetup.mtdGetSupplierType(objPUSetup.EnumSupplierType.Internal), objPUSetup.EnumSupplierType.Internal))
    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strSuppCode As String
        Dim strSuppType As String

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
        Dim strRptType As String = ""
        Dim indDate As String = ""
        Dim strExportToExcel As String = ""
        Dim strDate As String = Date_Validation(srchDateIn.Text, False)
        Dim strDateTo As String = Date_Validation(srchDateTo.Text, False)


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
    

        strRptType = lstRptType.SelectedItem.Value


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

        If srchDateIn.Text.Trim = "" Or srchDateTo.Text.Trim = "" Then
            lblErrDateInMsg.Visible = True
            Exit Sub
        Else
            lblErrDateInMsg.Visible = False
        End If

        If CheckDate(srchDateIn.Text.Trim(), indDate) = False Then
            lblErrDateInMsg.Visible = True
            Exit Sub
        End If

        If CheckDate(srchDateTo.Text.Trim(), indDate) = False Then
            lblErrDateInMsg.Visible = True
            Exit Sub
        End If


        strSuppCode = IIf(Trim(txtSupplier.Text) = "", "", Trim(txtSupplier.Text))
        strSuppType = Trim(lstSuppType.SelectedItem.Value)

        'strSuppCode = Server.UrlEncode(strSuppCode)

        If cbExcel.Checked = True Then
            strExportToExcel = "1"
        Else
            strExportToExcel = "0"
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""WM_StdRpt_Daily_FFBRcv_By_Supplier_ReportPreview.aspx?Type=Print&CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & _
                       "&strddlAccMth=" & strddlAccMth & _
                       "&strddlAccYr=" & strddlAccYr & _
                       "&Decimal=" & strDec & _
                       "&Rptype=" & strRptType & _
                       "&lblLocation=" & lblLocation.Text & _
                       "&DateFr=" & strDate & _
                       "&DateTo=" & strDateTo & _
                       "&ExportToExcel=" & strExportToExcel & _
                       "&SuppCode=" & strSuppCode & "&SuppType=" & strSuppType & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Function CheckDate(ByVal pv_strDate As String, ByRef pr_strDate As String) As Boolean
        Dim strDateFormatCode As String = Session("SS_DATEFMT")
        Dim strDateFormat As String

        pr_strDate = ""
        CheckDate = True
        If Not pv_strDate = "" Then
            If objGlobal.mtdValidInputDate(strDateFormatCode, pv_strDate, strDateFormat, pr_strDate) = False Then
                'lblFmt.Text = strDateFormat
                pr_strDate = ""
                CheckDate = False
            End If
        End If
    End Function

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
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

