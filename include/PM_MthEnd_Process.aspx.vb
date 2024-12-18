
Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PM.clsMthEnd
Imports agri.PD.clsMthEnd
Imports agri.Admin.clsShare


Public Class PM_mthend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastProcessDate As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblErrNotClose As Label
    Protected WithEvents lblErrProcess As Label

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEndDs As New agri.PM.clsMthEnd()
    Dim objPDMthEnd As New agri.PD.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPMAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        intPMAR = Session("SS_PMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMonthEnd), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrNotClose.Visible = False
            lblErrProcess.Visible = False

            If Not Page.IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strParam As String = objGlobal.EnumModule.MillProduction
        Dim objResult As New Object()

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If objResult.Tables(0).Rows.Count > 0 Then
            lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
            lblStatus.Text = objAdminShare.mtdGetMtdEndClose(CInt(objResult.Tables(0).Rows(0).Item("CloseInd")))
            lblAccPeriod.Text = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim() & "/" & objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
            If (CInt(objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()) = CInt(strAccMonth)) And _
               (CInt(objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()) = CInt(strAccYear)) And _
               (CInt(objResult.Tables(0).Rows(0).Item("CloseInd")) = objAdminShare.EnumMthEndClose.No) Then
                btnProceed.Visible = True
            Else
                btnProceed.Visible = False
            End If
        Else
            btnProceed.Visible = True
            lblStatus.Text = ""
            lblLastProcessDate.Text = ""
            lblAccPeriod.Text = strAccMonth & "/" & strAccYear
        End If        
    End Sub

    Sub btnProceed_Click(Sender As Object, E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objSysLocDs As New Dataset()
        Dim strParam As String = objGlobal.EnumModule.MillProduction
        Dim objResult As Integer

        'Simon , Remark, production MonthEnd Will process separatly depend on Location Type
	'If ProductionMonthEndClose() = False
        '    Exit Sub
        'End If

        Try 
            intErrNo = objMthEndDs.mtdMonthEndProcess(strCompany, _
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PMMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=PM/mthend/PM_MthEnd_Process.aspx")
            End Try

            Session("SS_PMACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PMAccMonth").Trim()
            Session("SS_PMACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PMAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("PMAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("PMAccYear").Trim()
            onLoad_Display()
        Else
            Select Case objResult
                Case 0
                    lblErrNotClose.Visible = True
                Case 2
                    lblErrProcess.Visible = True
            End Select
        End If        
    End Sub

    Function ProductionMonthEndClose() As Boolean
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objSysLocDs As New Dataset()
        Dim strParam As String = objGlobal.EnumModule.MillProduction
        Dim objResult As Integer

        Try 
            intErrNo = objPDMthEnd.mtdMonthEndProcess(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    Session("SS_PDACCMONTH"), _
                                                    Session("SS_PDACCYEAR"), _
                                                    strParam, _
                                                    objResult)
        Catch Exp As System.Exception 
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_MTHEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If objResult = 1 Then 
            ProductionMonthEndClose = True
        Else
            ProductionMonthEndClose = False
            Select Case objResult
                Case 0
                    lblErrNotClose.Visible = True
                Case 2
                    lblErrProcess.Visible = True
            End Select
        End If        
    End Function

End Class
