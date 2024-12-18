Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services

Public Class AR_StdRpt_MutasiAccountReceivable : Inherits Page


    Dim TrMthYr As HtmlTableRow

    Dim objGL As New agri.GL.clsReport()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCapDs As New Object()
    Dim objAdmAcc As New agri.Admin.clsAccPeriod()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objPUSetup As New agri.PU.clsSetup()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim strParamName As String
    Dim strParamValue As String

    Dim dr As DataRow
    Dim intErrNo As Integer
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String


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
            onload_GetLangCap()
            If Not Page.IsPostBack Then
                BindAccMonthList(BindAccYearList(strLocation, strAccYear, True))
                BindAccMonthToList(BindAccYearList(strLocation, strAccYear, False))
                BindBuyerList("")
            End If
        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim htmltr As HtmlTableRow

        htmltr = RptSelect.FindControl("TrMthYr")
        htmltr.visible = False
    End Sub

    Sub BindAccMonthList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        ddlSrchAccMonthFrom.Items.Clear()
        For intCnt = 1 To pv_intMaxMonth
            ddlSrchAccMonthFrom.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strAccMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next
        ddlSrchAccMonthFrom.SelectedIndex = intSelIndex
    End Sub

    Function BindAccYearList(ByVal pv_strLocation As String, _
                             ByVal pv_strAccYear As String, _
                             ByVal pv_blnIsFrom As Boolean) As Integer
        Dim strOpCd_Max_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ALLLOC_MAXPERIOD_GET"
        Dim strOpCd_Dist_Get As String = "ADMIN_CLSACCPERIOD_CONFIG_ACCYEAR_DISTINCT_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intAccYear As Integer
        Dim intMaxPeriod As Integer
        Dim intCnt As Integer
        Dim intSelIndex As Integer
        Dim objAccCfg As New Dataset()

        If pv_strLocation = "" Then
            pv_strLocation = strLocation
        Else
            If Left(pv_strLocation, 3) = "','" Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Right(pv_strLocation, 3) = "','" Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 3)
            ElseIf Left(pv_strLocation, 1) = "," Then
                pv_strLocation = Right(pv_strLocation, Len(pv_strLocation) - 1)
            ElseIf Right(pv_strLocation, 1) = "," Then
                pv_strLocation = Left(pv_strLocation, Len(pv_strLocation) - 1)
            End If
        End If

        Try
            strParam = "||"
            intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Dist_Get, _
                                                    strCompany, _
                                                    pv_strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objAccCfg)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_CTRL_ACCCFG_DIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        intSelIndex = 0
        If pv_blnIsFrom = True Then
            ddlSrchAccYearFrom.Items.Clear()
        Else
            ddlSrchAccYearTo.Items.Clear()
        End If

        If objAccCfg.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objAccCfg.Tables(0).Rows.Count - 1
                If pv_blnIsFrom = True Then
                    ddlSrchAccYearFrom.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))
                Else
                    ddlSrchAccYearTo.Items.Add(objAccCfg.Tables(0).Rows(intCnt).Item("AccYear"))
                End If

                If objAccCfg.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                    intSelIndex = intCnt
                End If
            Next

            If pv_blnIsFrom = True Then
                ddlSrchAccYearFrom.SelectedIndex = intSelIndex
                intAccYear = ddlSrchAccYearFrom.SelectedItem.Value
            Else
                ddlSrchAccYearTo.SelectedIndex = intSelIndex
                intAccYear = ddlSrchAccYearTo.SelectedItem.Value
            End If

            Try
                strParam = "||" & intAccYear
                intErrNo = objAdmAcc.mtdGetAccPeriodCfg(strOpCd_Max_Get, _
                                                        strCompany, _
                                                        pv_strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        objAccCfg)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_CTRL_ACCCFG_MAX_GET&errmesg=" & Exp.ToString() & "&redirect=")
            End Try

            Try
                intMaxPeriod = Convert.ToInt16(objAccCfg.Tables(0).Rows(0).Item("MaxPeriod"))
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CT_STDRPT_CTLR_ACCCFG_MAXPERIOD&errmesg=System required period configuration to process your request. Please set period configuration for the year of " & Convert.ToString(intAccYear) & "&redirect=")
            End Try

        Else
            If pv_blnIsFrom = True Then
                ddlSrchAccYearFrom.Items.Add(strAccYear)
                ddlSrchAccYearFrom.SelectedIndex = intSelIndex
            Else
                ddlSrchAccYearTo.Items.Add(strAccYear)
                ddlSrchAccYearTo.SelectedIndex = intSelIndex
            End If
            intMaxPeriod = Convert.ToInt16(strAccMonth)
        End If

        objAccCfg = Nothing
        Return intMaxPeriod
    End Function


    Sub OnIndexChage_FromAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim hidUserLoc As HtmlInputHidden

        hidUserLoc = RptSelect.FindControl("hidUserLoc")
        BindAccMonthList(BindAccYearList(hidUserLoc.Value, ddlSrchAccYearFrom.SelectedItem.Value, True))
    End Sub

    Sub OnIndexChage_ToAccPeriod(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim hidUserLoc As HtmlInputHidden

        hidUserLoc = RptSelect.FindControl("hidUserLoc")
        BindAccMonthToList(BindAccYearList(hidUserLoc.Value, ddlSrchAccYearTo.SelectedItem.Value, False))
    End Sub

    Sub BindAccMonthToList(ByVal pv_intMaxMonth As Integer)
        Dim intCnt As Integer
        Dim intSelIndex As Integer = 0

        ddlSrchAccMonthTo.Items.Clear()
        For intCnt = 1 To pv_intMaxMonth
            ddlSrchAccMonthTo.Items.Add(intCnt)
            If intCnt = Convert.ToInt16(strAccMonth) Then
                intSelIndex = intCnt - 1
            End If
        Next
        ddlSrchAccMonthTo.SelectedIndex = intSelIndex
    End Sub


    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strddlAccMth As String
        Dim strddlAccYr As String
        Dim strRptId As String
        Dim strRptName As String
        Dim strUserLoc As String
        Dim strDec As String

        Dim strSrchSupplier As String
        Dim strSrchAccMonthFrom As String
        Dim strSrchAccYearFrom As String
        Dim strSrchAccMonthTo As String
        Dim strSrchAccYearTo As String
        Dim strSrchPeriode1 As String
        Dim strSrchPeriode2 As String

        Dim objSysCfgDs As New Object()

        Dim ddlist As DropDownList

        Dim tempUserLoc As HtmlInputHidden
        Dim templblUL As Label
        Dim strExportToExcel As String

        ddlist = RptSelect.FindControl("lstRptName")
        strRptId = Trim(ddlist.SelectedItem.value)

        ddlist = RptSelect.FindControl("lstRptName")
        strRptName = Trim(ddlist.SelectedItem.text)

        ddlist = RptSelect.FindControl("lstDecimal")
        strDec = Trim(ddlist.SelectedItem.value)

        tempUserLoc = RptSelect.FindControl("hidUserLoc")
        strUserLoc = Trim(tempUserLoc.Value)

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


        strSrchSupplier = Server.UrlEncode(Trim(RadCmbCustCode.SelectedValue))

        strSrchAccMonthFrom = Server.UrlEncode(Trim(ddlSrchAccMonthFrom.SelectedItem.value))
        strSrchAccYearFrom = Server.UrlEncode(Trim(ddlSrchAccYearFrom.SelectedItem.value))
        strSrchAccMonthTo = Server.UrlEncode(Trim(ddlSrchAccMonthTo.SelectedItem.value))
        strSrchAccYearTo = Server.UrlEncode(Trim(ddlSrchAccYearTo.SelectedItem.value))


        If Len(Trim(strSrchAccMonthFrom)) = 1 Then
            strSrchPeriode1 = strSrchAccYearFrom & "0" & strSrchAccMonthFrom
        Else
            strSrchPeriode1 = strSrchAccYearFrom & strSrchAccMonthFrom
        End If


        If Len(Trim(strSrchAccMonthTo)) = 1 Then
            strSrchPeriode2 = strSrchAccYearTo & "0" & strSrchAccMonthTo
        Else
            strSrchPeriode2 = strSrchAccYearTo & strSrchAccMonthTo
        End If

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        Response.Write("<Script Language=""JavaScript"">window.open(""AR_StdRpt_MutasiAccountReceivablePreview.aspx?Type=Print&CompName=" & strCompany & _
                       "&SelLocation=" & strUserLoc & _
                       "&DDLAccMth=" & strddlAccMth & _
                       "&DDLAccYr=" & strddlAccYr & _
                       "&RptId=" & strRptId & _
                       "&RptName=" & strRptName & _
                       "&Decimal=" & strDec & _
                       "&SrchLocation=" & strUserLoc & _
                       "&SrchSupplier=" & strSrchSupplier & _
                       "&SrchSplType=" & ddlSplType.SelectedItem.Value & _
                       "&SrchRptType=" & ddlRptType.SelectedItem.Value & _
                       "&SrchPeriod1=" & strSrchPeriode1 & _
                       "&SrchPeriod2=" & strSrchPeriode2 & _
                       "&ExportToExcel=" & strExportToExcel & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        'lblAccCode.text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_REPORTS_DETACCLEDGER_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=../en/reports/GL_StdRpt_Selection.aspx")
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

    Sub BindBuyerList(ByVal pv_strBuyerCode As String)

        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim objBuyerDs As New DataSet
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim dsSOType As New DataSet
        Dim intSelectedIndex As Integer

        If pv_strBuyerCode = "" Then
            sSQLKriteria = ""
        Else
            sSQLKriteria = "AND BP.BillPartyCode='" & pv_strBuyerCode & "'"
        End If

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
                                         objBuyerDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dr = objBuyerDs.Tables(0).NewRow()
        dr("BillPartyCode") = ""
        'dr("Name") = "Please Select Customer"
        objBuyerDs.Tables(0).Rows.InsertAt(dr, 0)

        RadCmbCustCode.DataSource = objBuyerDs.Tables(0)
        RadCmbCustCode.DataValueField = "BillPartyCode"
        RadCmbCustCode.DataTextField = "Name"
        RadCmbCustCode.DataBind()
        'RadCmbCustCode.SelectedIndex = intSelectedIndex
    End Sub
End Class
