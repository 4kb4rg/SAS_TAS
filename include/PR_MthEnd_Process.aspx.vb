Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.PR.clsMthEnd
Imports agri.Admin.clsShare
Imports agri.PWSystem.clsConfig


Public Class PR_mthend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastProcessDate As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblErrNotClose As Label
    Protected WithEvents lblErrProcess As Label
    Protected WithEvents lblErrAttendance As Label
    Protected WithEvents lblErrBank As Label
    Protected WithEvents lblErrCOABlank as Label

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEnd As New agri.PR.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPRAR As Long
    Dim objLangCapDs As New DataSet()
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intPRAR = Session("SS_PRAR")
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMonthEnd), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            GetEntireLangCap()
            lblErrNotClose.Visible = False
            lblErrProcess.Visible = False
            lblErrAttendance.Visible = False
            lblErrBank.Visible = False
            If Not Page.IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strParam As String = objGlobal.EnumModule.Payroll
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=")
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


    Sub btnProceed_Click(Sender As Object, E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objSysLocDs As New Dataset()
        Dim strParam As String = objGlobal.EnumModule.Payroll 
        Dim objResult As Integer
        Dim strChargeLevel As String        

        Dim colParam As New Collection
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Account), "MS_COA")
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), Session("SS_CONFIGSETTING")) = True Then            
            colParam.Add(GetCaption(objLangCap.EnumLangCap.Block), "MS_BLOCK")
            strChargeLevel =objLangCap.EnumLangCap.Block
        Else            
            colParam.Add(GetCaption(objLangCap.EnumLangCap.SubBlock), "MS_BLOCK")
            strChargeLevel = objLangCap.EnumLangCap.SubBlock
        End If
        colParam.Add(GetCaption(objLangCap.EnumLangCap.Vehicle), "MS_VEHICLE")
        colParam.Add(GetCaption(objLangCap.EnumLangCap.VehExpense), "MS_VEHEXP")
        strParam = strParam & "|" & objLangCap.EnumLangCap.Block & "|" & strChargeLevel
        Try 
            intErrNo = objMthEnd.mtdMonthEndProcess(strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strOpCd, _
                                                    strParam, _
                                                    objResult, _
                                                    colParam)
        Catch Exp As System.Exception 
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MTHEND_PROCESS&errmesg=" & Exp.ToString() & "&redirect=")
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PRNMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=PR/mthend/PR_MthEnd_Process.aspx")
            End Try

            Session("SS_PRACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("PRAccMonth").Trim()
            Session("SS_PRACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("PRAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("PRAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("PRAccYear").Trim()
            onLoad_Display()
        Else
            Select Case objResult
                Case 0      
                    lblErrNotClose.Visible = True
                Case 2      
                    lblErrProcess.Visible = True
                Case 10     
                    lblErrAttendance.Visible = True
                Case 11     
                    lblErrBank.Visible = True
                case 17     
                    lblErrCOABlank.visible = True
            End Select
        End If        
    End Sub


End Class
