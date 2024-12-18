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

Public Class BD_StdRpt_FFBReceivedList : Inherits Page

    Protected RptSelect As UserControl

    Dim objBD As New agri.BD.clsReport()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objBDSetup As New agri.BD.clsSetup()
    Dim objBDTrx As New agri.BD.clsTrx()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrMsg As Label
    Protected WithEvents txtMonthFrom As TextBox
    Protected WithEvents txtMonthTo As TextBox
    Protected WithEvents txtUpdatedDateFrom As TextBox
    Protected WithEvents txtUpdatedDateTo As TextBox
    Protected WithEvents txtUpdatedBy As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents lblLocationTag As Label

    Protected WithEvents PrintPrev As ImageButton

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim strParam As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else
            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindStatusList()
            End If

        End If
    End Sub
    
    Sub BindStatusList()
        ddlStatus.Items.Add(New ListItem(objBDTrx.mtdGetFFBReceiveStatus(objBDTrx.EnumFFBReceiveStatus.All), objBDTrx.EnumFFBReceiveStatus.All))
        ddlStatus.Items.Add(New ListItem(objBDTrx.mtdGetFFBReceiveStatus(objBDTrx.EnumFFBReceiveStatus.Active), objBDTrx.EnumFFBReceiveStatus.Active))
        ddlStatus.Items.Add(New ListItem(objBDTrx.mtdGetFFBReceiveStatus(objBDTrx.EnumFFBReceiveStatus.Budgeted), objBDTrx.EnumFFBReceiveStatus.Budgeted))
    End Sub
    
    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucPeriod As HtmlTableRow

        ucPeriod = RptSelect.FindControl("TrPeriod")
        ucPeriod.Visible = False
    End Sub
    
    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocationTag.Text = GetCaption(objLangCap.EnumLangCap.Location)
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_NURSERYACTIVITYDIST_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If Trim(strLocType) = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

    Protected Function GetActivePeriod(ByRef BGTPeriod As String) As String
        Dim strOppCd_GET As String = "BD_CLSSETUP_BGTPERIOD_GET"
        Dim dsperiod As New DataSet()

        strParam = "|||||" & objBDSetup.EnumPeriodStatus.Active & "|" & strLocation & "|"

        Try
            intErrNo = objBDSetup.mtdGetPeriodList(strOppCd_GET, strParam, dsperiod)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_FERTUSG_GET_BUDGETACTIVEPERIODS&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try
        If dsperiod.Tables(0).Rows.Count > 0 Then
            BGTPeriod = dsperiod.Tables(0).Rows(0).Item("BGTPeriod")
            Return dsperiod.Tables(0).Rows(0).Item("PeriodID")
        Else
            BGTPeriod = "No Active Period"
            Return ""
        End If
    End Function

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strParam As String
        Dim strDateSetting As String
        Dim objSysCfgDs As New Object()
        Dim objDateFormat As New Object()
        Dim objDateFrom As String
        Dim objDateTo As String
        
        Dim strMonthFrom As String
        Dim strMonthTo As String
        Dim strUpdatedDateFrom As String
        Dim strUpdatedDateTo As String
        Dim strUpdatedBy As String
        Dim strStatus As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strDec As String

        
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim dt1 As Date, dt2 As Date
        Dim D As Integer, M As Integer, Y As Integer
        Dim D2 As Integer, M2 As Integer, Y2 As Integer
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)


        
        
        strMonthFrom = txtMonthFrom.Text
        strMonthTo = txtMonthTo.Text
        strUpdatedDateFrom = txtUpdatedDateFrom.Text
        strUpdatedDateTo = txtUpdatedDateTo.Text
        strUpdatedBy = txtUpdatedBy.Text
        strStatus = ddlStatus.SelectedItem.Value

        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)




        lblErrMsg.Visible = False
        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=IN_STDRPT_FUELISSUE_GET_CONFIG_DATE&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/IN_StdRpt_Selection.aspx")
        End Try
        strDateSetting = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If strMonthFrom <>"" And strMonthTo<>"" Then
            M = CInt(strMonthFrom.SubString(0, 2))
            Y = CInt(strMonthFrom.SubString(3, 4))
            M2 = CInt(strMonthTo.SubString(0, 2))
            Y2 = CInt(strMonthTo.SubString(3, 4))
            If Y2<Y Or (Y2=Y And M2<M) Then
                lblErrMsg.Text = "Period From must be earlier than Period To."
                lblErrMsg.Visible = True
                Exit Sub
            End If
        End If

        If strUpdatedDateFrom<>"" Then
            D = CInt(strUpdatedDateFrom.SubString(0, 2))
            M = CInt(strUpdatedDateFrom.SubString(3, 2))
            Y = CInt(strUpdatedDateFrom.SubString(6, 4))
            If objGlobal.mtdValidInputDate(strDateSetting, strUpdatedDateFrom, objDateFormat, objDateFrom) = False Then
                lblErrMsg.Text = objDateFormat & "."
                lblErrMsg.Visible = True
                Exit Sub
            End If
        End If
        
        If strUpdatedDateTo<>"" Then
            D2 = CInt(strUpdatedDateTo.SubString(0, 2))
            M2 = CInt(strUpdatedDateTo.SubString(3, 2))
            Y2 = CInt(strUpdatedDateTo.SubString(6, 4))
            If objGlobal.mtdValidInputDate(strDateSetting, strUpdatedDateTo, objDateFormat, objDateTo) = False Then
                lblErrMsg.Text = objDateFormat & "."
                lblErrMsg.Visible = True
                Exit Sub
            End If
        End If
        
        If strUpdatedDateFrom<>"" And strUpdatedDateTo<>"" Then
            If Y2<Y Or (Y2=Y And M2<M) Or (Y2=Y And M2=M And D2<D) Then
                lblErrMsg.Text = "Updated Date From Must be earlier than Updated Date To."
                lblErrMsg.Visible = True
                Exit Sub
            End If
        End If
        Response.Write("<Script Language=""JavaScript"">window.open(""BD_StdRpt_FFBReceivedListPreview.aspx?MonthFrom=" & strMonthFrom & "&MonthTo=" & strMonthTo & "&UpdatedDateFrom=" & objDateFrom & "&UpdatedDateTo=" & objDateTo & _
                       "&UpdatedBy=" & strUpdatedBy & "&Status=" & strStatus & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocationTag=" & lblLocationTag.Text &  _
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        
    End Sub

End Class
