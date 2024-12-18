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

Public Class PU_StdRpt_PengirimanBarangInternal : Inherits Page

    Protected RptSelect As UserControl

    Dim objPU As New agri.PU.clsReport()
    Dim objPUTrx As New agri.PU.clsTrx()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblPeriodeFrom As Label
    Protected WithEvents lblPeriodeFromFmt As Label
    Protected WithEvents lblPeriodeTo As Label
    Protected WithEvents lblPeriodeToFmt As Label

    Protected WithEvents lblLocation As Label

    Protected WithEvents txtPeriodeFrom As TextBox
    Protected WithEvents txtPeriodeTo As TextBox
    Protected WithEvents txtSender As TextBox
    Protected WithEvents txtExpedition As TextBox
    Protected WithEvents txtReceiver As TextBox
    Protected WithEvents txtRemarks As TextBox

    Protected WithEvents ddlDANoFrom As DropDownList
    Protected WithEvents ddlDANoTo As DropDownList

    Protected WithEvents cbExcel As CheckBox



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
    Dim intConfigsetting As Integer

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim tempDateFrom As TextBox
        Dim tempDateTo As TextBox
        Dim strLocationCode as String

        strCompany = Session("SS_COMPANY")
        strCompanyName = Session("SS_COMPANYNAME")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strUserName = Session("SS_USERNAME")
        strAccMonth = Session("SS_PUACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")
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
                BindDANoFrom("")
                BindDANoTo("")
            End If
        End If


    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim UCTrDocDateFromTo As HtmlTableRow
        Dim ucTrMthYr As HtmlTableRow

        UCTrDocDateFromTo = RptSelect.FindControl("TRDocDateFromTo")
        UCTrDocDateFromTo.Visible = true

        ucTrMthYr = RptSelect.FindControl("TrMthYr")
        ucTrMthYr.Visible = false

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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_STDRPT_DALIST_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/PU_StdRpt_Selection.aspx")
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

    Sub BindDANoFrom(ByVal pv_strLocCode as String)
        Dim strOpCd As String = "PU_CLSTRX_DA_GET"
        Dim objDADs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim intCnt As Integer = 0
        Dim dr As DataRow

        strParam = "|" & strLocation & "||||||A.DispAdvId||||"

        Try
            intErrNo = objPUTrx.mtdGetDA(strOpCd, strParam, objDADs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        'Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_LIST_GET"
        'Dim intErrNo As Integer
        'Dim strParam As String
        'Dim objDADs As New DataSet()
        'Dim dr As DataRow

        'strParam = "||||DispAdvId||" & strAccMonth & "|" & strAccYear & "|" & strLocation

        'Try
        '    intErrNo = objPUTrx.mtdGetDAInternalList(strOpCode, strParam, objDADs)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_INTrx_page.aspx")
        'End Try

        

        For intCnt = 0 To objDADs.Tables(0).Rows.Count - 1
            objDADs.Tables(0).Rows(intCnt).Item("DispAdvId") = Trim(objDADs.Tables(0).Rows(intCnt).Item("DispAdvId"))
            objDADs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objDADs.Tables(0).Rows(intCnt).Item("DispAdvId")) & " ( " & Trim(objDADs.Tables(0).Rows(intCnt).Item("LocCode")) & " ) "
        Next

        dr = objDADs.Tables(0).NewRow()
        dr("DispAdvId") = ""
        dr("LocCode") = "Please Select Dispatch Advice Number"
        objDADs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDANoFrom.DataSource = objDADs.Tables(0)
        ddlDANoFrom.DataValueField = "DispAdvId"
        ddlDANoFrom.DataTextField = "LocCode"
        ddlDANoFrom.DataBind()
    End Sub

    Sub BindDANoTo(ByVal pv_strLocCode As String)
        'Dim strOpCode As String = "PU_CLSTRX_DISPATCHADVICEINTERNAL_LIST_GET"
        'Dim intErrNo As Integer
        'Dim strParam As String
        'Dim objDADs As New DataSet()
        'Dim dr As DataRow

        'strParam = "||||DispAdvId||" & strAccMonth & "|" & strAccYear & "|" & strLocation

        'Try
        '    intErrNo = objPUTrx.mtdGetDAInternalList(strOpCode, strParam, objDADs)
        'Catch Exp As System.Exception
        '    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STOCKTRANSFERLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_INTrx_page.aspx")
        'End Try
        'For intCnt = 0 To objDADs.Tables(0).Rows.Count - 1
        '    objDADs.Tables(0).Rows(intCnt).Item("DispAdvId") = Trim(objDADs.Tables(0).Rows(intCnt).Item("DispAdvId"))
        '    objDADs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objDADs.Tables(0).Rows(intCnt).Item("DispAdvId")) & " ( " & Trim(objDADs.Tables(0).Rows(intCnt).Item("LocCode")) & " ) "
        'Next

        'dr = objDADs.Tables(0).NewRow()
        'dr("DispAdvId") = ""
        'dr("LocCode") = "Please Select Dispatch Advice Number"
        'objDADs.Tables(0).Rows.InsertAt(dr, 0)

        'ddlDANoTo.DataSource = objDADs.Tables(0)
        'ddlDANoTo.DataValueField = "DispAdvId"
        'ddlDANoTo.DataTextField = "LocCode"
        'ddlDANoTo.DataBind()

        Dim strOpCd As String = "PU_CLSTRX_DA_GET"
        Dim objDADs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim intCnt As Integer = 0
        Dim dr As DataRow

        strParam = "|" & strLocation & "||||||A.DispAdvId||||"

        Try
            intErrNo = objPUTrx.mtdGetDA(strOpCd, strParam, objDADs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PU_GET_DA&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_dalist.aspx")
        End Try

        For intCnt = 0 To objDADs.Tables(0).Rows.Count - 1
            objDADs.Tables(0).Rows(intCnt).Item("DispAdvId") = Trim(objDADs.Tables(0).Rows(intCnt).Item("DispAdvId"))
            objDADs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objDADs.Tables(0).Rows(intCnt).Item("DispAdvId")) & " ( " & Trim(objDADs.Tables(0).Rows(intCnt).Item("LocCode")) & " ) "
        Next

        dr = objDADs.Tables(0).NewRow()
        dr("DispAdvId") = ""
        dr("LocCode") = "Please Select Dispatch Advice Number"
        objDADs.Tables(0).Rows.InsertAt(dr, 0)

        ddlDANoTo.DataSource = objDADs.Tables(0)
        ddlDANoTo.DataValueField = "DispAdvId"
        ddlDANoTo.DataTextField = "LocCode"
        ddlDANoTo.DataBind()

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strDANoFrom As String
        Dim strDANoTo As String
        Dim strPeriodeFrom As String
        Dim strPeriodeTo As String
        Dim strSender As String
        Dim strExpedition As String
        Dim strReceiver As String
        Dim strRemarks As String
        Dim strUserLoc As String
        Dim strRptID as String
        Dim strRptName as String
        Dim strDec as String

        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim tempUserLoc As HtmlInputHidden
        Dim tempDocDateFrom As TextBox
        Dim tempDocDateTo As TextBox


        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDocDateFrom As String
        Dim objDocDateTo As String
        Dim strExportToExcel As String


        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)
        tempDocDateFrom = RptSelect.FindControl("txtDateFrom")
        strPeriodeFrom = Trim(tempDocDateFrom.Text)
        tempDocDateTo = RptSelect.FindControl("txtDateTo")
        strPeriodeTo = Trim(tempDocDateTo.Text)

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

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


        strDANoFrom = ddlDANoFrom.SelectedItem.Value
        strDANoTo = ddlDANoTo.SelectedItem.Value

        if strDANoFrom = "Please Select Dispatch Advice Number" then 
            strDANoFrom = ""
        end if 
        if strDANoTo = "Please Select Dispatch Advice Number" then 
            strDANoTo = ""
        end if 
    

        If txtSender.Text = "" Then
            strSender = ""
        Else
            strSender = Trim(txtSender.Text)
        End If

        If txtExpedition.Text = "" Then
            strExpedition = ""
        Else
            strExpedition = Trim(txtExpedition.Text)
        End If

        If txtReceiver.Text = "" Then
            strReceiver = ""
        Else
            strReceiver = Trim(txtReceiver.Text)
        End If

        If txtRemarks.Text = "" Then
            strRemarks = ""
        Else
            strRemarks = Trim(txtRemarks.Text)
        End If






        Response.Write("<Script Language=""JavaScript"">window.open(""PU_StdRpt_PengirimanBarangInternalPreview.aspx?CompName=" & strCompany & "&Location=" & strUserLoc & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & _
                           "&DANoFrom=" & strDANoFrom & "&DANoTo=" & strDANoTo & "&PeriodeFrom=" & strPeriodeFrom & "&PeriodeTo=" & strPeriodeTo & "&Sender=" & strSender & _
                           "&ExportToExcel=" & strExportToExcel & _
                           "&Expedition=" & strExpedition & "&Receiver=" & strReceiver & "&Remarks=" & strRemarks & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
