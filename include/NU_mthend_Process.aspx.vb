Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.NU.clsMthEnd
Imports agri.Admin.clsShare

Public Class NU_mthend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastProcessDate As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblErrNotClose As Label
    Protected WithEvents lblErrProcess As Label
    Protected WithEvents lblErrSeedlingsIssue As Label
    Protected WithEvents lblErrEntrySetup As Label

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEndDs As New agri.NU.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intNUAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        intNUAR = Session("SS_NUAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUMonthEnd), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrNotClose.Visible = False
            lblErrProcess.Visible = False
            lblErrSeedlingsIssue.Visible = False
            lblErrEntrySetup.Visible = False

            If Not Page.IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strParam As String = objGlobal.EnumModule.Nursery
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
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
        Dim strParam As String = objGlobal.EnumModule.Nursery
        Dim objResult As Integer

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_MTHEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NUMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=NU/mthend/NU_MthEnd_Process.aspx")
            End Try

            Session("SS_NUACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("NUAccMonth").Trim()
            Session("SS_NUACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("NUAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("NUAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("NUAccYear").Trim()
            onLoad_Display()
        Else
            Select Case objResult
                Case 0
                    lblErrNotClose.Visible = True
                Case 2
                    lblErrProcess.Visible = True
                Case 21
                    lblErrSeedlingsIssue.Visible = True
                Case 31
                    lblErrEntrySetup.Visible = True
            End Select
        End If        
    End Sub

End Class
