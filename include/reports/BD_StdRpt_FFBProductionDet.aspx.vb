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

Public Class BD_StdRpt_FFBProductionDet : Inherits Page

    Protected RptSelect As UserControl

    Dim objBD As New agri.BD.clsReport()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objBDSetup As New agri.BD.clsSetup()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrMsg As Label
    Protected WithEvents ddlYear As DropDownList
    
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
                BindYearList()
            End If

        End If
    End Sub

    Protected Overloads Sub OnPreRender(ByVal Source As Object, ByVal E As EventArgs) Handles MyBase.PreRender
        Dim ucPeriod As HtmlTableRow

        ucPeriod = RptSelect.FindControl("TrPeriod")
        ucPeriod.Visible = False
    End Sub

    Sub BindYearList()
        Dim strOpCd As String = "BD_STDRPT_FFB_PRODUCTION_DET_YEAR_GET"
        Dim objYearDs As DataSet
        
        Try
            intErrNo = objBD.mtdGetFFBYearList(strOpCd, strLocation, objYearDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_TRX_RECEIPT_BINDBANKCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=BI/trx/BI_trx_ReceiptList.aspx")
        End Try

        For intCnt = 0 To objYearDs.Tables(0).Rows.Count - 1
            objYearDs.Tables(0).Rows(intCnt).Item("txtYear") = objYearDs.Tables(0).Rows(intCnt).Item("AccYear")
        Next

        dr = objYearDs.Tables(0).NewRow()
        dr("AccYear") = ""
        dr("txtYear") = "Select a year"
        objYearDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlYear.DataSource = objYearDs.Tables(0)
        ddlYear.DataValueField = "AccYear"
        ddlYear.DataTextField = "txtYear"
        ddlYear.DataBind()
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
        Dim strYear As String
        
        Dim strRptID As String
        Dim strRptName As String
        Dim strDec As String

        
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)


        
        
        strYear = ddlYear.selectedItem.Value
        
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)




        lblErrMsg.Visible = False
        
        Response.Write("<Script Language=""JavaScript"">window.open(""BD_StdRpt_FFBProductionDetPreview.aspx?Year=" & strYear & _
                       "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & "&lblLocationTag=" & lblLocationTag.Text & _
                       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        
    End Sub

End Class
