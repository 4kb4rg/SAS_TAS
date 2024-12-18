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

Public Class BD_StdRpt_Manuring : Inherits Page

    Protected RptSelect As UserControl

    Dim objBD As New agri.BD.clsReport()
    Dim objBDSetup As New agri.BD.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstPlantYr As DropDownList
    Protected WithEvents lblErrYrPlant As Label

    Protected WithEvents PrintPrev As ImageButton

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intConfigsetting As Integer

    Dim strParam As String
    Dim strUserLoc As String

    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim dr As DataRow
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        Else

            lblErrYrPlant.Visible = False
            If Not Page.IsPostBack Then
                BindPlantYr()
            End If

        End If
    End Sub

    Protected Function GetActivePeriod(ByRef BGTPeriod As String) As String
        Dim strOppCd_GET As String = "BD_CLSSETUP_BGTPERIOD_GET"
        Dim dsperiod As New DataSet()

        strParam = "|||||" & objBDSetup.EnumPeriodStatus.Active & "|" & strLocation & "|"

        Try
            intErrNo = objBDSetup.mtdGetPeriodList(strOppCd_GET, strParam, dsperiod)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_MANURING_GET_BUDGETACTIVEPERIODS&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try
        If dsperiod.Tables(0).Rows.Count > 0 Then
            BGTPeriod = dsperiod.Tables(0).Rows(0).Item("BGTPeriod")
            Return dsperiod.Tables(0).Rows(0).Item("PeriodID")
        Else
            BGTPeriod = "No Active Period"
            Return ""
        End If
    End Function

    Sub BindPlantYr()
        Dim strOppCd_PlantYr_GET As String
        Dim objRptDsPlantYr As New DataSet()
        Dim objMapPath As String
        Dim intCnt As Integer

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfigsetting) = True Then
            strOppCd_PlantYr_GET = "BD_STDRPT_MANURING_PLANTYR_GET"
        Else
            strOppCd_PlantYr_GET = "BD_STDRPT_MANURING_SBLK_PLANTYR_GET"
        End If

        strParam = GetActivePeriod("") & "||AND BD.LocCode = '" & strLocation & "'"
        Try
            intErrNo = objBD.mtdGetReport_Manuring(strOppCd_PlantYr_GET, strParam, objRptDsPlantYr, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_MANURING_PLANTYR_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

        For intCnt = 0 To objRptDsPlantYr.Tables(0).Rows.Count - 1
            lstPlantYr.Items.Add(New ListItem(Trim(objRptDsPlantYr.Tables(0).Rows(intCnt).Item("YearPlanted")), Trim(objRptDsPlantYr.Tables(0).Rows(intCnt).Item("YearPlanted"))))
        Next

        dr = objRptDsPlantYr.Tables(0).NewRow()
        dr("YearPlanted") = "Select Planting Year"
        objRptDsPlantYr.Tables(0).Rows.InsertAt(dr, 0)

        lstPlantYr.DataSource = objRptDsPlantYr.Tables(0)
        lstPlantYr.DataValueField = "YearPlanted"
        lstPlantYr.DataBind()

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strYrPlanted As String

        Dim strddlPeriodID As String
        Dim strddlPeriodName As String
        Dim strRptID As String
        Dim strRptName As String
        Dim strDec As String

        Dim tempPeriod As DropDownList
        Dim tempRpt As DropDownList
        Dim tempDec As DropDownList
        Dim templblUL As Label
        Dim lblPeriod As Label

        tempPeriod = RptSelect.FindControl("lstPeriodName")
        strddlPeriodID = Trim(tempPeriod.SelectedItem.Value)
        strddlPeriodName = Trim(tempPeriod.SelectedItem.Text)
        tempRpt = RptSelect.FindControl("lstRptName")
        strRptID = Trim(tempRpt.SelectedItem.Value)
        strRptName = Trim(tempRpt.SelectedItem.Text)
        tempDec = RptSelect.FindControl("lstDecimal")
        strDec = Trim(tempDec.SelectedItem.Value)


        If Not lstPlantYr.SelectedIndex = 0 Then
            strYrPlanted = Trim(lstPlantYr.SelectedItem.Value)
        Else
            lblErrYrPlant.Visible = True
            Exit Sub
        End If

        If strddlPeriodID = 0 Then
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = True
            Exit Sub
        Else
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = False
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""BD_StdRpt_ManuringPreview.aspx?DDLPeriodID=" & strddlPeriodID & "&DDLPeriodName=" & strddlPeriodName & _
                       "&YrPlanted=" & strYrPlanted & "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
