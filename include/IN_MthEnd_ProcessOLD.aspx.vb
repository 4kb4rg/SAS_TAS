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
Imports agri.IN.clsMthEnd
Imports agri.Admin.clsShare
Imports agri.PWSystem.clsConfig

Public Class IN_mthend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastProcessDate As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblErrNotClose As Label
    Protected WithEvents lblErrProcess As Label
    Protected WithEvents lblErrStockReceive As Label
    Protected WithEvents lblErrStockReturnAdvice As Label
    Protected WithEvents lblErrStockAdjustment As Label
    Protected WithEvents lblErrStockIssue As Label
    Protected WithEvents lblErrStockReturn As Label
    Protected WithEvents lblErrFuelIssue As Label
    Protected WithEvents lblErrNoItemAcc As Label
    Protected WithEvents lblErrNoLocAcc As Label
    Protected WithEvents lblErrNoDblEntryAcc As Label

    Protected WithEvents CloseInd_Final As RadioButton
    Protected WithEvents CloseInd_Temporary As RadioButton
    Protected WithEvents CloseInd_Rollback As RadioButton

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEnd As New agri.IN.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intINAR As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strClsAccMonth As String
    Dim strClsAccYear As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intINAR = Session("SS_INAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INMonthEnd), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrNotClose.Visible = False
            lblErrProcess.Visible = False
            lblErrStockReceive.Visible = False
            lblErrStockReturnAdvice.Visible = False
            lblErrStockAdjustment.Visible = False
            lblErrStockIssue.Visible = False
            lblErrStockReturn.Visible = False
            lblErrFuelIssue.Visible = False
            lblErrNoItemAcc.Visible = False
            lblErrNoLocAcc.Visible = False
            lblErrNoDblEntryAcc.Visible = False
            If Not Page.IsPostBack Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strParam As String = objGlobal.EnumModule.Inventory
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
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
            End Try

            If objResult.Tables(0).Rows.Count > 0 Then
                lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
                lblStatus.Text = objAdminShare.mtdGetMtdEndClose(CInt(objResult.Tables(0).Rows(0).Item("CloseInd")))
                lblAccPeriod.Text = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim() & "/" & objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
                strClsAccMonth = objResult.Tables(0).Rows(0).Item("CurrAccMonth").Trim()
                strClsAccYear = objResult.Tables(0).Rows(0).Item("CurrAccYear").Trim()
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
            Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
            Dim objSysLocDs As New DataSet()
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

            Session("SS_INACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
            Session("SS_GLACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()

            'lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
            lblStatus.Text = objAdminShare.mtdGetMtdEndClose(objAdminShare.EnumMthEndClose.No)
            lblAccPeriod.Text = strAccMonth & "/" & strAccYear
            btnProceed.Visible = True
        End If
    End Sub

    Sub CloseInd_OnCheckChange(ByVal Sender As Object, ByVal E As EventArgs)
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objSysLocDs As New DataSet()
        Dim strParam As String = objGlobal.EnumModule.Purchasing
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

            Session("SS_INACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
            Session("SS_GLACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()

            'lblLastProcessDate.Text = objGlobal.GetLongDate(objResult.Tables(0).Rows(0).Item("LastProcessDate"))
            lblStatus.Text = objAdminShare.mtdGetMtdEndClose(objAdminShare.EnumMthEndClose.No)
            lblAccPeriod.Text = strAccMonth & "/" & strAccYear
            btnProceed.Visible = True
        End If

    End Sub


    Sub btnProceed_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strOpCode_GetSysLoc As String = "PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET"
        Dim objSysLocDs As New DataSet()
        Dim strParam As String
        Dim objResult As Integer
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)
        Dim intClsPeriod As Integer = (CInt(strClsAccYear) * 100) + CInt(strClsAccMonth)

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
            Try
                strParam = objGlobal.EnumModule.Inventory & "|"

                intErrNo = objMthEnd.mtdMonthEndProcess(strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strOpCd, _
                                                        strParam, _
                                                        objResult)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_MTHEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
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
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=IN/mthend/IN_MthEnd_Process.aspx")
                End Try

                Session("SS_INACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
                Session("SS_INACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
                strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
                strAccYear = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
                onLoad_Display()
            Else
                Select Case objResult
                    Case 0
                        lblErrNotClose.Visible = True
                    Case 2
                        lblErrProcess.Visible = True
                    Case 10
                        lblErrStockReceive.Visible = True
                    Case 11
                        lblErrStockReturnAdvice.Visible = True
                    Case 12
                        lblErrStockAdjustment.Visible = True
                    Case 13
                        lblErrStockIssue.Visible = True
                    Case 14
                        lblErrStockReturn.Visible = True
                    Case 15
                        lblErrFuelIssue.Visible = True
                    Case 16
                        lblErrNoItemAcc.Visible = True
                    Case 17
                        lblErrNoLocAcc.Visible = True
                    Case 18
                        lblErrNoDblEntryAcc.Visible = True
                End Select
            End If

        Else
            'Temporary and Rollback Temporary Closing
            'Only update AccMonth/AccYear on AD_SYSCFG
            Dim strOpCodeTemp As String = "PWSYSTEM_CLSCONFIG_SYSLOC_ACCPERIOD_UPD_TEMP"
            Dim strParamName As String = ""
            Dim strParamValue As String = ""
            Dim clsAccMonth As String
            Dim clsAccyear As String
            Dim objGLtrx As New agri.GL.ClsTrx()

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
                clsAccyear = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
                clsAccMonth = Val(objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()) + 1
                If clsAccMonth > 12 Then
                    clsAccMonth = 1
                    clsAccyear = Val(clsAccyear) + 1
                End If

            ElseIf CloseInd_Rollback.Checked = True Then
                clsAccyear = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
                clsAccMonth = Val(objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()) - 1
                If clsAccMonth = 0 Then
                    clsAccMonth = 12
                    clsAccyear = Val(clsAccyear) - 1
                End If
            End If

            strParamName = "LOCCODE|COMPCODE|STRUPDATE"
            strParamValue = Trim(strLocation) & "|" & Trim(strCompany) & "|" & _
                            "INACCMONTH='" & clsAccMonth & "', INACCYEAR='" & clsAccyear & "' "

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

            Session("SS_INACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
            Session("SS_GLACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
            onLoad_Display()
        End If

    End Sub

End Class
