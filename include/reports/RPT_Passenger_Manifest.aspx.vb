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

Public Class RPT_Passenger_Manifest : Inherits Page

    Protected RptSelect As UserControl

    Dim objGL As New agri.GL.clsReport()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Protected WithEvents hidAccMonthPX As HtmlInputHidden
    Protected WithEvents hidAccYearPX As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents Find As HtmlInputButton
    Protected WithEvents Find2 As HtmlInputButton
    Protected WithEvents txtDate As TextBox
    Protected WithEvents txtDateTo As TextBox
    Protected WithEvents btnDate As Image
    Protected WithEvents btnDateTo As Image
    Protected WithEvents txtFlightNo As TextBox
    Protected WithEvents ddlAirline As DropDownList
    Protected WithEvents ddlAirlineType As DropDownList
    Protected WithEvents ddlFlightType As DropDownList
    Protected WithEvents ddlOrigin As DropDownList
    Protected WithEvents ddlDestination As DropDownList
    Protected WithEvents cbExcel As CheckBox
    Dim TrMthYr As HtmlTableRow
    Protected WithEvents PrintPrev As ImageButton

    Dim strDateFmt As String
    Dim strAcceptFormat As String

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer
    Dim dr As DataRow
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim tempActGrp As String

    Dim objLangCapDs As New Object()
    Dim strLocType As String

    Dim objDataGet As New Object()
    Dim strParamName As String
    Dim strParamValue As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                txtDate.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                txtDateTo.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                onload_GetLangCap()
                BindAirline("X")
                BindOrigin()
                BindDestination("X")
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.Visible = False
        htmltr = RptSelect.FindControl("TrCheckLoc")
        htmltr.Visible = False
        htmltr = RptSelect.FindControl("TrRadioLoc")
        htmltr.Visible = False

        If Page.IsPostBack Then
        End If
    End Sub

    Sub onload_GetLangCap()
        'GetEntireLangCap()
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


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String
        Dim strFlightNo As String
        Dim strAirline As String
        Dim strOrigin As String
        Dim strDestination As String
        Dim strDate As String = Date_Validation(txtDate.Text, False)
        Dim strDateTo As String = Date_Validation(txtDateTo.Text, False)
        Dim objSysCfgDs As New Object()
        Dim ddlist As DropDownList
        Dim tempUserLoc As HtmlInputHidden
        Dim strExportToExcel As String

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.Value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.Text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.Value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

        strFlightNo = Trim(txtFlightNo.Text)
        strAirline = Trim(ddlAirline.SelectedItem.Value)
        strOrigin = Trim(ddlOrigin.SelectedItem.Value)
        strDestination = Trim(ddlDestination.SelectedItem.Value)

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")


        Response.Write("<Script Language=""JavaScript"">window.open(""RPT_Passenger_Manifest_Preview.aspx?Type=Print&CompName=" & strCompany & _
                       "&Location=" & strUserLoc & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&strFlightNo=" & strFlightNo & _
                       "&strAirline=" & strAirline & _
                       "&DateFrom=" & strDate & _
                       "&DateTo=" & strDateTo & _
                       "&strOrigin=" & strOrigin & _
                       "&strDestination=" & strDestination & _
                       "&ExportToExcel=" & strExportToExcel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub BindAirline(Optional ByVal pv_strAirlineType As String = "")
        Dim strOpCd As String = "SETUP_GET_AIRLINE"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0

        strParamName = "STRSEARCH"
        strParamValue = IIf(pv_strAirlineType = "" Or pv_strAirlineType = "X", "", "WHERE AirLineType = '" & pv_strAirlineType & "'")

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataGet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_AIRLINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objDataGet.Tables(0).NewRow()
        dr("AirlineCode") = ""
        dr("Init_Descr") = "- ALL -"
        objDataGet.Tables(0).Rows.InsertAt(dr, 0)

        ddlAirline.DataSource = objDataGet.Tables(0)
        ddlAirline.DataValueField = "AirlineCode"
        ddlAirline.DataTextField = "Init_Descr"
        ddlAirline.DataBind()
        ddlAirline.SelectedIndex = intSelectedIndex

        If Not objDataGet Is Nothing Then
            objDataGet = Nothing
        End If
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

    Sub AirlineType_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BindAirline(ddlAirlineType.SelectedValue)
    End Sub

    Sub FlightType_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BindDestination(ddlFlightType.SelectedValue)
    End Sub

    Sub BindDestination(Optional ByVal pv_strFlightType As String = "")
        Dim strOpCd As String = "SETUP_GET_AIRPORT"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0

        strParamName = "STRSEARCH"
        strParamValue = IIf(pv_strFlightType = "" Or pv_strFlightType = "X", "AND A.CountryCode = 'ID'", IIf(pv_strFlightType = "D", "AND A.CountryCode = 'ID'", "AND A.CountryCode <> 'ID'"))

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataGet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_AIRLINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objDataGet.Tables(0).NewRow()
        dr("AirportCode") = ""
        dr("Init_Descr") = "- ALL -"
        objDataGet.Tables(0).Rows.InsertAt(dr, 0)

        ddlDestination.DataSource = objDataGet.Tables(0)
        ddlDestination.DataValueField = "AirportCode"
        ddlDestination.DataTextField = "Init_Descr"
        ddlDestination.DataBind()
        ddlDestination.SelectedIndex = intSelectedIndex

        If Not objDataGet Is Nothing Then
            objDataGet = Nothing
        End If
    End Sub

    Sub BindOrigin(Optional ByVal pv_strFlightType As String = "")
        Dim strOpCd As String = "SETUP_GET_AIRPORT"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0

        strParamName = "STRSEARCH"
        strParamValue = "AND A.CountryCode = 'ID'"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objDataGet)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_AIRLINE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objDataGet.Tables(0).NewRow()
        dr("AirportCode") = ""
        dr("Init_Descr") = "- ALL -"
        objDataGet.Tables(0).Rows.InsertAt(dr, 0)

        ddlOrigin.DataSource = objDataGet.Tables(0)
        ddlOrigin.DataValueField = "AirportCode"
        ddlOrigin.DataTextField = "Init_Descr"
        ddlOrigin.DataBind()
        ddlOrigin.SelectedIndex = intSelectedIndex

        If Not objDataGet Is Nothing Then
            objDataGet = Nothing
        End If
    End Sub
End Class
