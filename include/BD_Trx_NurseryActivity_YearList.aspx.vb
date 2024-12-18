
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class BD_NurseryActivity_Year : Inherits Page

    Protected WithEvents YearList As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblPeriodErr As Label
    Protected WithEvents lblLocCode As Label
    Protected WithEvents lblBgtPeriod As Label
    Protected WithEvents lblTotAmtFig As Label
    Protected WithEvents lblBlockErr As Label
    Protected WithEvents BlockTag As Label
    Protected WithEvents lblLocTag As Label
    Protected WithEvents lblNoOf As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblBgtStatus As Label 
    Protected WithEvents lblErrNurserySeed As Label

    Dim objBDTrx As New agri.BD.clsTrx()
    Dim objBDSetup As New agri.BD.clsSetup()
    Dim objGL As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String

    Dim objDataSet As DataSet
    Dim objLangCapDs As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strValidateCode As String
    Dim strvalidateDesc As String
    Dim intConfigsetting As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "OriBlkCode"
                SortCol.Text = "ASC"
            End If

            If Not Page.IsPostBack Then
                onload_GetLangCap()
                BindGrid()
            End If
        End If
        lblPeriodErr.Visible = False
        lblErrNurserySeed.Visible = False
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblTitle.Text = "NURSERY ACTIVITY"
        lblLocTag.Text = GetCaption(objLangCap.EnumLangCap.Location) & lblCode.Text
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            BlockTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            YearList.Columns(0).Visible = False
            YearList.Columns(1).HeaderText = BlockTag.Text
        Else
            BlockTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
            YearList.Columns(0).HeaderText = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            YearList.Columns(1).HeaderText = BlockTag.Text
        End If
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BD_TRX_NURSERYACTIVITYYEARLIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_trx_NurseryActivity_YearList.aspx")
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


    Sub BindGrid()
        Dim Period As String

        YearList.DataSource = LoadData()
        YearList.DataBind()
        lblLocCode.Text = strLocation
        GetActivePeriod(Period)
        lblBgtPeriod.Text = Period
        If lblBgtStatus.Text.Trim = objBDSetup.EnumPeriodStatus.Addvote Then 
            YearList.Columns(0).Visible = False
        End If


    End Sub

    Protected Function LoadData() As DataSet

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            strOppCd_GET = "BD_CLSTRX_NURSERYACTIVITY_BLOCK_GET"
            strParam = objGL.EnumBlockType.Nursery & "|" & objGL.EnumBlockStatus.Active & "|" & strLocation & "|"
        Else
            strOppCd_GET = "BD_CLSTRX_NURSERYACTIVITY_SUBBLOCK_GET"
            strParam = objGL.EnumSubBlockType.Nursery & "|" & objGL.EnumSubBlockStatus.Active & "|" & strLocation & "|"
        End If

        Try
            intErrNo = objBDTrx.mtdGetNurseryActivityYear(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_NURSERYACTIVITY_YEARLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_trx_NurseryActivity_YearList.aspx")
        End Try

        Return objDataSet

        If objDataSet.Tables(0).Rows.Count - 1 = 0 Then
            lblErrNurserySeed.Visible = True
        End If
    End Function

    Protected Function GetActivePeriod(ByRef BGTPeriod As String) As String
        Dim strOppCd_GET As String = "BD_CLSSETUP_BGTPERIOD_GET"
        Dim dsperiod As New DataSet()

        strParam = "|||||" & objBDSetup.EnumPeriodStatus.Active & "|" & strLocation & "|"
        Try
            intErrNo = objBDSetup.mtdGetPeriodList(strOppCd_GET, strParam, dsperiod)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_TRX_NURSERYACTYEARLIST_GET_BUDGETACTIVEPERIODS&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_trx_NurseryActivity_YearList.aspx")
        End Try

        If dsperiod.Tables(0).Rows.Count > 0 Then
            BGTPeriod = dsperiod.Tables(0).Rows(0).Item("BGTPeriod") & " - (" & objBDSetup.mtdGetPeriodStatus(dsperiod.Tables(0).Rows(0).Item("Status")) & ")"
            lblBgtStatus.Text = dsperiod.Tables(0).Rows(0).Item("Status") 
            Return dsperiod.Tables(0).Rows(0).Item("PeriodID")
        Else
            BGTPeriod = "No Active Period"
            Response.Redirect("../../BD/Setup/BD_setup_Periods.aspx")
        End If
    End Function

    Sub btnBlockYear_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim strYear As String = CType(sender, LinkButton).CommandArgument
        Dim strSubBlk As String = CType(sender, LinkButton).CommandName
        Dim strBlk As String = CType(sender, LinkButton).Text
        Dim strOppCd_NurseryActivity_Format_GET As String = "BD_CLSTRX_NURSERYACTIVITY_FORMAT_GET"
        Dim strOppCd_NurseryActivitySetup_GET As String = "BD_CLSSETUP_NURSERYACTIVITY_FORMAT_GET"
        Dim strOppCd_NurseryActivity_ADD As String = "BD_CLSTRX_NURSERYACTIVITY_ADD"
        Dim strOppCd_NurseryActivity_UPD As String = "BD_CLSTRX_NURSERYACTIVITY_UPD"
        Dim strOppCd_NurseryActivity_CostPerSeed_SUM As String = "BD_CLSTRX_NURSERYACTIVITY_COSTPERSEED_SUM"
        Dim strOppCd_Formula_GET As String = "BD_CLSTRX_CALCNAFORMULA_GET"
        Dim strOppCd_SubBlock_Get As String = "BD_CLSTRX_NURSERYACTIVITY_SUBBLOCK_GET"
        Dim intError As Integer
        Dim strParam As String
        Dim strCropType As String
        Dim strBlkStatus As String

        strParam = GetActivePeriod("") & "||" & strSubBlk.Trim & "|" & strBlk.Trim & "|||||||"
        Try
            intErrNo = objBDTrx.mtdUpdNurseryActivity(strOppCd_NurseryActivity_Format_GET, _
                                                    strOppCd_NurseryActivity_ADD, _
                                                    strOppCd_NurseryActivitySetup_GET, _
                                                    strOppCd_NurseryActivity_UPD, _
                                                    strOppCd_Formula_GET, _
                                                    strOppCd_SubBlock_Get, _
                                                    strOppCd_NurseryActivity_CostPerSeed_SUM, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objBDSetup.EnumOperation.Add, _
                                                    intError, _
                                                    True)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_TRX_NURSERYACTIVITY_BLK_ADD&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_trx_NurseryActivity_YearList.aspx")
        End Try

        If intError = objBDTrx.EnumErrorType.NoActivePeriod Then
            lblPeriodErr.Visible = True
        ElseIf intError = objBDTrx.EnumErrorType.BlockErr Then
            lblBlockErr.Visible = True
        Else
            Response.Redirect("../../BD/Trx/BD_trx_NurseryActivity_Det_DistByBlock.aspx?blk=" & strBlk.Trim & "&subblk=" & strSubBlk.Trim & "&yr=" & strYear.Trim)
        End If
    End Sub

    Sub btnSubBlockYear_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim strYear As String = CType(sender, LinkButton).CommandArgument
        Dim strBlk As String = CType(sender, LinkButton).CommandName
        Dim strSubBlk As String = CType(sender, LinkButton).Text

        Dim strOppCd_NurseryActivity_Format_GET As String = "BD_CLSTRX_NURSERYACTIVITY_FORMAT_GET"
        Dim strOppCd_NurseryActivitySetup_GET As String = "BD_CLSSETUP_NURSERYACTIVITY_FORMAT_GET"
        Dim strOppCd_NurseryActivity_ADD As String = "BD_CLSTRX_NURSERYACTIVITY_ADD"
        Dim strOppCd_NurseryActivity_UPD As String = "BD_CLSTRX_NURSERYACTIVITY_UPD"
        Dim strOppCd_NurseryActivity_CostPerSeed_SUM As String = "BD_CLSTRX_NURSERYACTIVITY_COSTPERSEED_SUM"
        Dim strOppCd_Formula_GET As String = "BD_CLSTRX_CALCNAFORMULA_GET"
        Dim strOppCd_SubBlock_Get As String = "BD_CLSTRX_NURSERYACTIVITY_SUBBLOCK_GET"
        Dim intError As Integer
        Dim strParam As String
        Dim strCropType As String
        Dim strBlkStatus As String

        strParam = GetActivePeriod("") & "||" & strSubBlk.Trim & "||||||||"
        Try
            intErrNo = objBDTrx.mtdUpdNurseryActivity(strOppCd_NurseryActivity_Format_GET, _
                                                    strOppCd_NurseryActivity_ADD, _
                                                    strOppCd_NurseryActivitySetup_GET, _
                                                    strOppCd_NurseryActivity_UPD, _
                                                    strOppCd_Formula_GET, _
                                                    strOppCd_SubBlock_Get, _
                                                    strOppCd_NurseryActivity_CostPerSeed_SUM, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objBDSetup.EnumOperation.Add, _
                                                    intError, _
                                                    False)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=BD_TRX_NURSERYACTIVITY_SBLK_ADD&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_trx_NurseryActivity_YearList.aspx")
        End Try

        If intError = objBDTrx.EnumErrorType.NoActivePeriod Then
            lblPeriodErr.Visible = True
        ElseIf intError = objBDTrx.EnumErrorType.BlockErr Then
            lblBlockErr.Visible = True
        Else
            Response.Redirect("../../BD/Trx/BD_Trx_NurseryActivity_Details.aspx?blk=" & strBlk.Trim & "&subblk=" & strSubBlk.Trim & "&yr=" & strYear.Trim)
        End If
    End Sub

End Class
