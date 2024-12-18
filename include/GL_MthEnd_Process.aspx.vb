Imports System
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.GlobalHdl.clsAccessRights
Imports agri.GL.clsMthEnd
Imports agri.Admin.clsShare
Imports agri.PWSystem.clsLangCap


Public Class GL_mthend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastProcessDate As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblErrNotClose As Label
    Protected WithEvents lblErrVehNoDist As Label
    Protected WithEvents lblErrProcess As Label
    Protected WithEvents lblErrJournal As Label
    Protected WithEvents lblErrGCFail As Label
    Protected WithEvents lblErrGCNoAllocation As Label
    Protected WithEvents lblErrGCNoLocation As Label
    Protected WithEvents lblErrVehList As Label
    Protected WithEvents lblCurrAccPeriod As Label
    Protected WithEvents lblErrMsg As Label
    Protected WithEvents lblWSMessage As Label

    Protected WithEvents CloseInd_Final As RadioButton
    Protected WithEvents CloseInd_Temporary As RadioButton
    Protected WithEvents CloseInd_Rollback As RadioButton
    Protected WithEvents hidClsAccMonth As HtmlInputHidden
    Protected WithEvents hidClsAccYear As HtmlInputHidden

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEndDs As New agri.GL.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLTrx As New agri.GL.clsTrx
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intConfig As Integer
    Dim intGLAR As Integer
    Dim objLangCapDs As New DataSet()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strClsAccMonth As String
    Dim strClsAccYear As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intConfig = Session("SS_CONFIGSETTING")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")
        lblCurrAccPeriod.Text = strAccMonth & "/" & strAccYear
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLMonthEnd), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            GetEntireLangCap()
            lblErrNotClose.Visible = False
            lblErrVehNoDist.Visible = False
            lblErrProcess.Visible = False
            lblErrJournal.Visible = False
            lblErrGCFail.Visible = False
            lblErrGCNoAllocation.Visible = False
            lblErrGCNoLocation.Visible = False
            lblErrVehList.Visible = False
            lblErrMsg.Visible = False
            lblWSMessage.Visible = False

           

            If Not Page.IsPostBack Then
                btnProceed.Attributes("onclick") = "javascript:return ConfirmAction('Process Month End');"
                onLoad_Display()
            End If
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

    Sub onLoad_Display()
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strParam As String = objGlobal.EnumModule.GeneralLedger
        Dim objResult As New Object()

        If CloseInd_Final.Checked = True Then
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            If objResult.Tables(0).Rows.Count > 0 Then
                lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
                lblStatus.Text = objAdminShare.mtdGetMtdEndClose(CInt(objResult.Tables(0).Rows(0).Item("CloseInd")))
                lblAccPeriod.Text = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim() & "/" & objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
                strClsAccMonth = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()
                strClsAccYear = objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
                hidClsAccMonth.Value = strClsAccMonth
                hidClsAccYear.Value = strClsAccYear
                If (CInt(objResult.Tables(0).Rows(0).Item("CloseInd")) = objAdminShare.EnumMthEndClose.No) Then
                    btnProceed.Visible = True
                Else
                    btnProceed.Visible = False
                End If
                'If (CInt(objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()) = CInt(strAccMonth)) And _
                '   (CInt(objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()) = CInt(strAccYear)) And _
                '   (CInt(objResult.Tables(0).Rows(0).Item("CloseInd")) = objAdminShare.EnumMthEndClose.No) Then
                '    btnProceed.Visible = True
                'Else
                '    btnProceed.Visible = False
                'End If
            Else
                btnProceed.Visible = True
                lblStatus.Text = ""
                lblLastProcessDate.Text = ""
                lblAccPeriod.Text = strAccMonth & "/" & strAccYear
            End If
        Else
            Dim clsAccMonth As String
            Dim clsAccyear As String
            Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
            Dim objSysLocDs As New DataSet()


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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            If objResult.Tables(0).Rows.Count > 0 Then

                lblAccPeriod.Text = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim() & "/" & objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
                strClsAccMonth = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()
                strClsAccYear = objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
                hidClsAccMonth.Value = strClsAccMonth
                hidClsAccYear.Value = strClsAccYear

                strParam = strCompany & "|" & strLocation & "|" & strUserId
                Try
                    intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        objSysLocDs, _
                                                        strParam)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PUMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=PU/mthend/PU_MthEnd_Process.aspx")
                End Try

                If CloseInd_Temporary.Checked = True Then

                    clsAccyear = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
                    clsAccMonth = Val(objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim())
                    If clsAccMonth > 12 Then
                        clsAccMonth = 1
                        clsAccyear = Val(clsAccyear) + 1
                    End If

                ElseIf CloseInd_Rollback.Checked = True Then
                    clsAccyear = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
                    clsAccMonth = Val(objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim())
                    If clsAccMonth = 0 Then
                        clsAccMonth = 12
                        clsAccyear = Val(clsAccyear) - 1
                    End If
                End If
                lblCurrAccPeriod.Text = clsAccMonth & "/" & clsAccyear
            End If
        End If
            
    End Sub

    Sub CloseInd_OnCheckChange(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objSysLocDs As New DataSet()
        Dim strParam As String = objGlobal.EnumModule.GeneralLedger
        Dim objResult As New Object()

        If CloseInd_Final.Checked = True Then
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            If objResult.Tables(0).Rows.Count > 0 Then
                lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
                lblStatus.Text = objAdminShare.mtdGetMtdEndClose(CInt(objResult.Tables(0).Rows(0).Item("CloseInd")))
                lblAccPeriod.Text = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim() & "/" & objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
                strClsAccMonth = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()
                strClsAccYear = objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
                hidClsAccMonth.Value = strClsAccMonth
                hidClsAccYear.Value = strClsAccYear
                If (CInt(objResult.Tables(0).Rows(0).Item("CloseInd")) = objAdminShare.EnumMthEndClose.No) Then
                    btnProceed.Visible = True
                Else
                    btnProceed.Visible = False
                End If

                'If (CInt(objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()) = CInt(strAccMonth)) And _
                '   (CInt(objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()) = CInt(strAccYear)) And _
                '   (CInt(objResult.Tables(0).Rows(0).Item("CloseInd")) = objAdminShare.EnumMthEndClose.No) Then
                '    btnProceed.Visible = True
                'Else
                '    btnProceed.Visible = False
                'End If
            Else
                btnProceed.Visible = True
                lblStatus.Text = ""
                lblLastProcessDate.Text = ""
                lblAccPeriod.Text = strAccMonth & "/" & strAccYear
            End If
        Else
            strParam = strCompany & "|" & strLocation & "|" & strUserId
            Try
                intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    objSysLocDs, _
                                                    strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PUMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=PU/mthend/PU_MthEnd_Process.aspx")
            End Try

            Session("SS_GLACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()
            Session("SS_GLACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()

            'lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
            lblStatus.Text = objAdminShare.mtdGetMtdEndClose(objAdminShare.EnumMthEndClose.No)
            lblAccPeriod.Text = strAccMonth & "/" & strAccYear
            btnProceed.Visible = True
        End If

    End Sub


    Sub btnProceed_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        'Dim intErrNo As Integer
        'Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        'Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        'Dim objSysLocDs As New Dataset()
        'Dim strParam As String = objGlobal.EnumModule.GeneralLedger & "|" & intConfig
        'Dim objResult As Integer
        'Dim strVehNoDist As String = ""
        'Dim strErrMsg As String = ""
        'Dim strWSMessage As String = ""
        'Dim colParam As New Collection
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.Location), "MS_LOCATION")
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.Account), "MS_COA")
        'If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), Session("SS_CONFIGSETTING")) = True Then
        '    colParam.Add(GetCaption(objLangCap.EnumLangCap.Block), "MS_BLOCK")
        'Else
        '    colParam.Add(GetCaption(objLangCap.EnumLangCap.SubBlock), "MS_BLOCK")
        'End If
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.Vehicle), "MS_VEHICLE")
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.VehUsage), "MS_VEHUSAGE")
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.VehUsageUnit), "MS_VEHUSAGEUNIT")
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.VehType), "MS_VEHTYPE")
        'colParam.Add("Inter-" & GetCaption(objLangCap.EnumLangCap.Location), "MS_INTER_LOCATION")
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.VehExpense), "MS_VEHEXP")
        'colParam.Add(GetCaption(objLangCap.EnumLangCap.Work), "MS_WORK")

        'colParam.Add("GL_CLSTRX_JOURNAL_DETAIL_ADD", "OC_JOURNAL_ADD")
        'colParam.Add("GL_CLSTRX_JOURNAL_LINE_ADD", "OC_JOURNAL_LINE_ADD")
        'colParam.Add("GL_CLSTRX_JOURNAL_LINE_GET", "OC_JOURNAL_LINE_GET")
        'colParam.Add("GL_CLSTRX_JOURNAL_DETAIL_UPD", "OC_JOURNAL_UPD")

        'colParam.Add(Session("SS_GLACCMONTH"), "PM_ACCMONTH")
        'colParam.Add(Session("SS_GLACCYEAR"), "PM_ACCYEAR")
        'colParam.Add(Session("SS_ARACCMONTH"), "PM_AR_ACCMONTH")
        'colParam.Add(Session("SS_ARACCYEAR"), "PM_AR_ACCYEAR")
        'colParam.Add(Session("SS_COMPANY"), "PM_COMPANY")
        'colParam.Add(Session("SS_DATEFMT"), "PM_DATEFMT")
        'colParam.Add(Session("SS_LOCATION"), "PM_LOCCODE")
        'colParam.Add(Session("SS_PRACCMONTH"), "PM_PR_ACCMONTH")
        'colParam.Add(Session("SS_PRACCYEAR"), "PM_PR_ACCYEAR")
        'colParam.Add(Session("SS_USERID"), "PM_USERID")
        'Try 
        '    intErrNo = objMthEndDs.mtdMonthEndProcess(strCompany, _
        '                                            strLocation, _
        '                                            strUserId, _
        '                                            strAccMonth, _
        '                                            strAccYear, _
        '                                            strOpCd, _
        '                                            strParam, _
        '                                            colParam, _
        '                                            objResult, _
        '                                            strVehNoDist, _
        '                                            strErrMsg, _
        '                                            strWSMessage)
        '    If strErrMsg <> "" Then
        '        lblErrMsg.Text = "<br>" & strErrMsg
        '        lblErrMsg.Visible = True
        '    End If
        '    If strWSMessage <> "" And objResult = 1 Then
        '        lblWSMessage.Text = "<br>" & strWSMessage
        '        lblWSMessage.Visible = True
        '    End If
        'Catch Exp As System.Exception 
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_MTHEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
        'End Try

        'If objResult = 1 Then 
        '    strParam = strCompany & "|" & strLocation & "|" & strUserId
        '    Try
        '        intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
        '                                            strCompany, _
        '                                            strLocation, _
        '                                            strUserId, _
        '                                            objSysLocDs, _
        '                                            strParam)
        '    Catch Exp As System.Exception 
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GLMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=GL/mthend/GL_MthEnd_Process.aspx")
        '    End Try

        '    Session("SS_GLACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()
        '    Session("SS_GLACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
        '    strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()
        '    strAccYear = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()

        '    onLoad_Display()
        'Else
        '    Select Case objResult
        '        Case 0      
        '            lblErrNotClose.Visible = True
        '        Case 2      
        '            lblErrVehNoDist.Visible = True
        '            lblErrVehList.Visible = True
        '            lblErrVehList.Text = strVehNoDist
        '        Case 3      
        '            lblErrProcess.Visible = True
        '        Case 10     
        '            lblErrJournal.Visible = True
        '        Case 20     
        '            lblErrGCFail.Visible = True
        '        Case 21     
        '            lblErrGCNoAllocation.Visible = True
        '        Case 22     
        '            lblErrGCNoLocation.Visible = True
        '    End Select
        'End If        

        Dim intErrNo As Integer
        Dim dsResult As New Object
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strOpCode As String = "GL_MTHEND_MONTHEND"

        Dim strAccMonth2 As String
        Dim strAccYear2 As String
        Dim strAccMonth3 As String
        Dim strAccYear3 As String
        Dim strAccMonthPrev As String
        Dim strAccYearPrev As String

        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)    
        Dim intClsPeriod As Integer = (CInt(hidClsAccYear.Value) * 100) + CInt(hidClsAccMonth.Value)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intSelPeriod < intCurPeriod Then
                lblErrNotClose.Visible = True
                lblErrNotClose.Text = "Invalid Period"
                Exit Sub
            End If
        Else
            If intSelPeriod < intCurPeriod Then
                lblErrNotClose.Visible = True
                lblErrNotClose.Text = "Invalid Period"
                Exit Sub
            End If

            If CloseInd_Final.Checked = True Then
                If intSelPeriod <> intClsPeriod Then
                    lblErrNotClose.Visible = True
                    lblErrNotClose.Text = "Invalid Period"
                    Exit Sub
                End If
            End If
        End If

        If CloseInd_Final.Checked = True Then
            If Val(strAccMonth) = 12 Then
                strAccMonth2 = 1
                strAccYear2 = Val(strAccYear) + 1
                strAccMonth3 = 2
                strAccYear3 = Val(strAccYear) + 1
            Else
                strAccMonth2 = Val(strAccMonth) + 1
                strAccYear2 = strAccYear
                If Val(strAccMonth2) = 12 Then
                    strAccMonth3 = 1
                    strAccYear3 = Val(strAccYear) + 1
                Else
                    strAccMonth3 = Val(strAccMonth) + 2
                    strAccYear3 = strAccYear
                End If
            End If

            If Val(strAccMonth) = 1 Then
                strAccMonthPrev = 12
                strAccYearPrev = Val(strAccMonth) - 1
            Else
                strAccMonthPrev = Val(strAccMonth) - 1
                strAccYearPrev = strAccYear
            End If

            strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID|ACCMONTH2|ACCYEAR2|ACCMONTH3|ACCYEAR3|ACCMONTHPREV|ACCYEARPREV"
            strParamValue = strLocation & "|" & strAccMonth & _
                            "|" & strAccYear & "|" & Session("SS_USERID") & _
                            "|" & strAccMonth2 & "|" & strAccYear2 & _
                            "|" & strAccMonth3 & "|" & strAccYear3 & _
                            "|" & strAccMonthPrev & "|" & strAccYearPrev


            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCode, _
                                                        strParamName, _
                                                        strParamValue, _
                                                        dsResult)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_DAYEND_PROCESS&errmesg=" & Exp.Message.ToString & "&redirect=")
            End Try



            lblErrProcess.Visible = True

            If dsResult.Tables.Count = 2 Then
                lblErrProcess.Text = dsResult.Tables(1).Rows(0).Item("Msg")
            Else
                lblErrProcess.Text = dsResult.Tables(0).Rows(0).Item("Msg")
            End If

        Else
            'Temporary and Rollback Temporary Closing
            'Only update AccMonth/AccYear on AD_SYSCFG
            Dim strOpCodeTemp As String = "PWSYSTEM_CLSCONFIG_SYSLOC_ACCPERIOD_UPD_TEMP"
            Dim clsAccMonth As String
            Dim clsAccyear As String
            Dim objGLtrx As New agri.GL.ClsTrx()
            Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
            Dim objSysLocDs As New DataSet()
            Dim strParam As String

            strParam = strCompany & "|" & strLocation & "|" & strUserId
            Try
                intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    objSysLocDs, _
                                                    strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PUMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=PU/mthend/PU_MthEnd_Process.aspx")
            End Try

            If CloseInd_Temporary.Checked = True Then
                clsAccyear = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
                clsAccMonth = Val(objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()) + 1
                If clsAccMonth > 12 Then
                    clsAccMonth = 1
                    clsAccyear = Val(clsAccyear) + 1
                End If

            ElseIf CloseInd_Rollback.Checked = True Then
                clsAccyear = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
                clsAccMonth = Val(objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()) - 1
                If clsAccMonth = 0 Then
                    clsAccMonth = 12
                    clsAccyear = Val(clsAccyear) - 1
                End If
            End If

            strParamName = "LOCCODE|COMPCODE|STRUPDATE"
            strParamValue = Trim(strLocation) & "|" & Trim(strCompany) & "|" & _
                            "GLACCMONTH='" & clsAccMonth & "', GLACCYEAR='" & clsAccyear & "' "

            Try
                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCodeTemp, _
                                                        strParamName, _
                                                        strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_MTHEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            strParam = strCompany & "|" & strLocation & "|" & strUserId
            Try
                intErrNo = objSysCfg.mtdGetSysLocInfo(strOpCode_GetSysLoc, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    objSysLocDs, _
                                                    strParam)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PUMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=PU/mthend/PU_MthEnd_Process.aspx")
            End Try

            Session("SS_GLACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()
            Session("SS_GLACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("GLAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("GLAccYear").Trim()
            onLoad_Display()
        End If
    End Sub

End Class
