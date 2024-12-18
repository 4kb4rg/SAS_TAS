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

Public Class BD_StdRpt_CropProd : Inherits Page

    Protected RptSelect As UserControl

    Dim objBD As New agri.BD.clsReport()
    Dim objBDSetup As New agri.BD.clsSetup
    Dim objLangCap As New agri.PWSystem.clsLangCap
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblBlkType As Label
    Protected WithEvents lblBlkGrp As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblErrYrPlant As Label

    Protected WithEvents lstPlantYr As DropDownList
    Protected WithEvents lstBlkType As DropDownList
    Protected WithEvents txtBlkGrp As TextBox
    Protected WithEvents txtBlkCode As TextBox
    Protected WithEvents txtSubBlkCode As TextBox

    Protected WithEvents PrintPrev As ImageButton
    Protected WithEvents TrBlkGrp As HtmlTableRow
    Protected WithEvents TrBlk As HtmlTableRow
    Protected WithEvents TrSubBlk As HtmlTableRow

    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strUserLoc As String
    Dim intConfigsetting As String

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
                onload_GetLangCap()
                BindPlantYr()
                BlkTypeList()
            End If

            If lstBlkType.SelectedItem.Value = "BlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = True
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "BlkGrp" Then
                TrBlkGrp.Visible = True
                TrBlk.Visible = False
                TrSubBlk.Visible = False
            ElseIf lstBlkType.SelectedItem.Value = "SubBlkCode" Then
                TrBlkGrp.Visible = False
                TrBlk.Visible = False
                TrSubBlk.Visible = True
            End If

        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblBlkType.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Type :"
        lblBlkGrp.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Group :"
        lblBlkCode.Text = GetCaption(objLangCap.EnumLangCap.Block) & " Code :"
        lblSubBlkCode.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & " Code :"
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
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_CROPPROD_CLSLANGCAP_BUSSTERM_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
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
        Dim dsperiod As New DataSet
        Dim strParam As String

        strParam = "|||||" & objBDSetup.EnumPeriodStatus.Active & "|" & strLocation & "|"
        Try
            intErrNo = objBDSetup.mtdGetPeriodList(strOppCd_GET, strParam, dsperiod)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_STDRPT_CROPPROD_GET_BUDGETACTIVEPERIODS&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
        End Try

        If dsperiod.Tables(0).Rows.Count > 0 Then
            BGTPeriod = dsperiod.Tables(0).Rows(0).Item("BGTPeriod") & " - (" & objBDSetup.mtdGetPeriodStatus(dsperiod.Tables(0).Rows(0).Item("Status")) & ")"
            Return dsperiod.Tables(0).Rows(0).Item("PeriodID")
        End If
    End Function

    Sub BindPlantYr()
        Dim strOppCd_PlantYr_GET As String = "BD_STDRPT_CROPPROD_PLANTYR_GET"
        Dim objRptDsPlantYr As New DataSet
        Dim strParam As String
        Dim objMapPath As String
        Dim intCnt As Integer

        strParam = GetActivePeriod("") & "|" & strLocation & "|"
        Try
            intErrNo = objBD.mtdGetReport_CropProd(strOppCd_PlantYr_GET, strParam, objRptDsPlantYr, objMapPath)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=BD_CROP_PROD_PLANTYR_GET&errmesg=" & lblErrMessage.Text & "&redirect=../en/reports/BD_StdRpt_Selection.aspx")
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

    Sub BlkTypeList()

        Dim strBlkGrp As String
        Dim strBlk As String
        Dim strSubBlk As String

        strBlkGrp = Left(lblBlkGrp.Text, Len(lblBlkGrp.Text) - 2)
        strBlk = Left(lblBlkCode.Text, Len(lblBlkCode.Text) - 2)
        strSubBlk = Left(lblSubBlkCode.Text, Len(lblSubBlkCode.Text) - 2)

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockYieldLevel), intConfigsetting) = True Then
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlkGrp, "BlkGrp"))
        Else
            lstBlkType.Items.Add(New ListItem(strSubBlk, "SubBlkCode"))
            lstBlkType.Items.Add(New ListItem(strBlk, "BlkCode"))
        End If

    End Sub

    Sub btnPrintPrev_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strYrPlanted As String
        Dim strBlkType As String
        Dim strBlkGrpCode As String
        Dim strBlkCode As String
        Dim strSubBlkCode As String

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
        strBlkType = Trim(lstBlkType.SelectedItem.Value)

        If txtBlkGrp.Text = "" Then
            strBlkGrpCode = ""
        Else
            strBlkGrpCode = Trim(txtBlkGrp.Text)
        End If

        If txtBlkCode.Text = "" Then
            strBlkCode = ""
        Else
            strBlkCode = Trim(txtBlkCode.Text)
        End If

        If txtSubBlkCode.Text = "" Then
            strSubBlkCode = ""
        Else
            strSubBlkCode = Trim(txtSubBlkCode.Text)
        End If

        If strddlPeriodID = 0 Then
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = True
            Exit Sub
        Else
            lblPeriod = RptSelect.FindControl("lblPeriod")
            lblPeriod.Visible = False
        End If

        strBlkGrpCode = Server.UrlEncode(strBlkGrpCode)
        strBlkCode = Server.UrlEncode(strBlkCode)
        strSubBlkCode = Server.UrlEncode(strSubBlkCode)

        Response.Write("<Script Language=""JavaScript"">window.open(""BD_StdRpt_CropProdPreview.aspx?DDLPeriodID=" & strddlPeriodID & "&DDLPeriodName=" & strddlPeriodName & _
                       "&lblBlkType=" & lblBlkType.Text & "&lblBlkGrp=" & lblBlkGrp.Text & "&lblBlkCode=" & lblBlkCode.Text & "&lblSubBlkCode=" & lblSubBlkCode.Text & _
                       "&YrPlanted=" & strYrPlanted & "&BlkType=" & strBlkType & "&BlkGrp=" & strBlkGrpCode & "&BlkCode=" & strBlkCode & "&SubBlkCode=" & strSubBlkCode & _
                       "&RptID=" & strRptID & "&RptName=" & strRptName & "&Decimal=" & strDec & """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

    End Sub

End Class
