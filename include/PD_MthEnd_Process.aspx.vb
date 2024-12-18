
Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PD.clsMthEnd
Imports agri.PM.clsMthEnd
Imports agri.Admin.clsShare
Imports agri.PWSystem.clsConfig

Public Class PD_mthend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastProcessDate As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblErrNotClose As Label
    Protected WithEvents lblErrProcess As Label
    Protected WithEvents lblErrPlantingRate As Label
    Protected WithEvents lblErrPlantingYear As Label

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEnd As New agri.PD.clsMthEnd()
    Dim objPMMthEnd As New agri.PM.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intModuleActivate As Integer
    Dim intPDAR As Integer
    Dim intPMAR As Integer
    Dim PDMthEndClosed As Boolean
    Dim PMMthEndClosed As Boolean
    Dim strCostLevel As String
    Dim strYieldLevel As String
    Dim strAutoYieldRate AS String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        intModuleActivate = Session("SS_MODULEACTIVATE")
        intPDAR = Session("SS_PDAR")
        intPMAR = Session("SS_PMAR")
        strCostLevel = Session("SS_COSTLEVEL")
        strYieldLevel = Session("SS_YIELDLEVEL")
        strAutoYieldRate = Session("SS_AUTO_ESTYIELLDRATE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDMonthEnd), intPDAR) = False And _
               objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrNotClose.Visible = False
            lblErrProcess.Visible = False
            lblErrPlantingRate.Visible = False
            lblErrPlantingYear.Visible = False
            If Not Page.IsPostBack Then
                    onLoad_Display()
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strParam As String = Convert.ToString(objGlobal.EnumModule.Production)
        Dim objResult As New Dataset()


        Try 
            intErrNo = objAdminShare.mtdMonthEnd(strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strAccMonth, _
                                                 strAccYear, _
                                                 strOpCd, _
                                                 strParam, _
                                                 True, _
                                                 objResult)
        Catch Exp As System.Exception 
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If objResult.Tables(0).Rows.Count > 0 Then
            lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
            lblStatus.Text = objAdminShare.mtdGetMtdEndClose(Convert.ToInt32(objResult.Tables(0).Rows(0).Item("CloseInd")))
            lblAccPeriod.Text = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim() & "/" & objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()

            If (Convert.ToInt32(objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()) = Convert.ToInt32(strAccMonth)) And _
               (Convert.ToInt32(objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()) = Convert.ToInt32(strAccYear)) And _
               (Convert.ToInt32(objResult.Tables(0).Rows(0).Item("CloseInd")) = objAdminShare.EnumMthEndClose.No) Then
                btnProceed.Visible = True
            Else
                btnProceed.Visible = False
            End If
        Else
            lblStatus.Text = ""
            lblLastProcessDate.Text = ""
            lblAccPeriod.Text = strAccMonth & "/" & strAccYear
            btnProceed.Visible = True
        End If
        objResult = Nothing
    End Sub

    Sub btnProceed_Click(Sender As Object, E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strParam As String
        Dim objResult As Integer
        Dim objSysLocDs As New Dataset()
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"

        If objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.Production), intModuleActivate) = True Then
            Try 
                strParam = Convert.ToString(objGlobal.EnumModule.Production) & "|" & _
                            strCostLevel & "|" & _
                            strYieldLevel & "|" & _
                            strAutoYieldRate

                intErrNo = objMthEnd.mtdMonthEndProcess(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        objResult)
            Catch Exp As System.Exception 
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_MTHEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
            End Try
        End If

        If (objResult = 1) Then
            If (objAR.mtdHasAccessRights(objAR.mtdGetModuleActivation(objAR.EnumModuleActivation.MillProduction), intModuleActivate) = True) Then
                Try 
                    strParam = Convert.ToString(objGlobal.EnumModule.MillProduction)
                    intErrNo = objPMMthEnd.mtdMonthEndProcess(strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strAccMonth, _
                                                            strAccYear, _
                                                            strOpCd, _
                                                            strParam, _
                                                            objResult)
                Catch Exp As System.Exception 
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_MTHEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
                End Try
            End If

            If objResult = 1 Then 
                strParam = strCompany & "|" & strLocation & "|" & strUserId
                Try
                    intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        objSysLocDs, _
                                                        strParam)
                Catch Exp As System.Exception 
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PDMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=PD/mthend/PD_MthEnd_Process.aspx")
                End Try

                Session("SS_PDACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PDAccMonth").Trim()
                Session("SS_PDACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PDAccYear").Trim()
                Session("SS_PMACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PMAccMonth").Trim()
                Session("SS_PMACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PMAccYear").Trim()
                strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("PDAccMonth").Trim()
                strAccYear = objSysLocDs.Tables(0).Rows(0).Item("PDAccYear").Trim()
                onLoad_Display()
            Else
                Select Case objResult
                    Case 0
                        lblErrNotClose.Visible = True
                    Case 2
                        lblErrProcess.Visible = True
                End Select
            End If
        Else
            Select Case objResult
                Case 0
                    lblErrNotClose.Visible = True
                Case 2
                    lblErrProcess.Visible = True
                Case 3
                    lblErrPlantingRate.Visible = True
                Case 4
                    lblErrPlantingYear.Visible = True
            End Select
        End If
        objSysLocDs = Nothing
    End Sub


End Class
