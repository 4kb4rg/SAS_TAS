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
Imports agri.WS.clsMthEnd
Imports agri.Admin.clsShare
Imports agri.PWSystem.clsConfig


Public Class WS_mthend_Process : Inherits Page

    Protected WithEvents lblAccPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblLastProcessDate As Label
    Protected WithEvents btnProceed As ImageButton
    Protected WithEvents lblActionResult As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblBlkCodeTag As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblVehCodeTag As Label
    Protected WithEvents lblVehExpCodeTag As Label
    Protected WithEvents lblBillPartyCode As Label
    Protected WithEvents lblWorkCodeTag As Label
    
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminShare As New agri.Admin.clsShare()
    Dim objMthEnd As New agri.WS.clsMthEnd()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()
    
    Dim dsLangCap As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim intConfigsetting As Integer
    Dim strLocType as String


    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        intConfigsetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMonthEnd), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblActionResult.Visible = False

            If Not Page.IsPostBack Then
                GetLangCap()
                onLoad_Display()
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim intErrNo As Integer
        Dim strOpCd As String = "ADMIN_CLSSHARE_MTHEND_GET"
        Dim strParam As String = objGlobal.EnumModule.Workshop
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_MTHEND_GET&errmesg=" & Exp.ToString & "&redirect=")
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
    
    Sub GetLangCap()
        dsLangCap = GetLanguageCaptionDS()
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
        Else
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text
        End If
        
        lblAccCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text
        lblVehCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text
        lblVehExpCodeTag.Text = GetCaption(objLangCap.EnumLangCap.VehExpense) & lblCode.Text
        lblVehExpCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Expense) & lblCode.Text
        lblBillPartyCode.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & lblCode.Text
        lblWorkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Work) & lblCode.Text
    End Sub




    Function GetCaption(ByVal pv_TermCode As String) As String
        Dim I As Integer

        For I = 0 To dsLangCap.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(dsLangCap.Tables(0).Rows(I).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTermMW"))
                else
                    Return Trim(dsLangCap.Tables(0).Rows(I).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function
    
    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsLC As DataSet
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
                                                 dsLC, _
                                                 strParam)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSMTHEND_GET_LANGCAP&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_List.aspx")
        End Try

        Return dsLC
        If Not dsLC Is Nothing Then
            dsLC = Nothing
        End If
    End Function

    Sub btnProceed_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCode_DebitNote_Add As String = "BI_CLSTRX_DEBITNOTE_ADD"
        Dim strOpCode_DebitNote_Upd As String = "BI_CLSTRX_DEBITNOTE_UPD"
        Dim strOpCode_DebitNoteLine_Add As String = "BI_CLSTRX_DEBITNOTE_LINE_ADD"
        Dim strOpCode_DebitNoteLine_Sum As String = "BI_CLSTRX_DEBITNOTE_SUM_LINEAMOUNT_GET"
        Dim strOpCode_DebitNote_Amt_Upd As String = "BI_CLSTRX_DEBITNOTE_UPD"
        Dim strOpCode_CreditNote_Add As String = "BI_CLSTRX_CREDITNOTE_ADD"
        Dim strOpCode_CreditNote_Upd As String = "BI_CLSTRX_CREDITNOTE_UPD"
        Dim strOpCode_CreditNoteLine_Add As String = "BI_CLSTRX_CREDITNOTE_LINE_ADD"
        Dim strOpCode_CreditNoteLine_Sum As String = "BI_CLSTRX_CREDITNOTE_SUM_LINEAMOUNT_GET"
        Dim strOpCode_CreditNote_Amt_Upd As String = "BI_CLSTRX_CREDITNOTE_TOTALAMOUNT_UPD"
        
        Dim colParam As New Collection
        Dim strErrMsg As String
        Dim intErrNo As Integer
        Dim strParam As String
        Dim objSysLocDs As New Dataset()

        colParam.Add(lblBlkCodeTag.Text, "MS_BLOCK")
        colParam.Add(lblAccCodeTag.Text, "MS_COA")
        colParam.Add(lblVehCodeTag.Text, "MS_VEHICLE")
        colParam.Add(lblVehExpCodeTag.Text, "MS_VEHEXP")
        colParam.Add(lblBillPartyCode.Text, "MS_BILLPARTY")
        colParam.Add(lblWorkCodeTag.Text, "MS_WORKCODE")

        colParam.Add("GL_CLSSETUP_ENTRYSETUP_TYPE_GET", "OC_ENTRYSETUP_GET")
        colParam.Add("WS_CLSMTHEND_AUTO_GENERATE_DNCN_GET", "OC_JOB_GET_FOR_DNCN")
        colParam.Add("WS_CLSTRX_JOB_STOCK_UPD", "OC_JOB_STOCK_UPD")
        colParam.Add("WS_CLSTRX_JOB_UPD", "OC_JOB_UPD")
        colParam.Add("GL_CLSTRX_JOURNAL_DETAIL_ADD", "OC_JOURNAL_ADD")
        colParam.Add("GL_CLSTRX_JOURNAL_LINE_ADD", "OC_JOURNAL_LINE_ADD")
        colParam.Add("GL_CLSTRX_JOURNAL_LINE_GET", "OC_JOURNAL_LINE_GET")
        colParam.Add("GL_CLSTRX_JOURNAL_DETAIL_UPD", "OC_JOURNAL_UPD")
        colParam.Add("WS_CLSTRX_MECHANIC_HOUR_LINE_UPD", "OC_MECHANIC_HOUR_LINE_UPD")
        colParam.Add("WS_CLSTRX_MECHANIC_HOUR_UPD", "OC_MECHANIC_HOUR_UPD")
        colParam.Add("WS_CLSMTHEND_PROCESS_TRANSACTION", "OC_MTHEND_PROCESS_TRANSACTION")
        colParam.Add("WS_CLSMTHEND_JOB_TRX_PURGE", "OC_PURGE_JOB")
        colParam.Add("WS_CLSMTHEND_JOBSTOCK_TRX_PURGE", "OC_PURGE_JOBSTOCK")
        colParam.Add("WS_CLSMTHEND_JOBWORKCODE_TRX_PURGE", "OC_PURGE_JOBWORKCODE")
        colParam.Add("WS_CLSMTHEND_MECHHOUR_TRX_PURGE", "OC_PURGE_MECHHOUR")
        colParam.Add("WS_CLSMTHEND_MECHHOURLN_TRX_PURGE", "OC_PURGE_MECHHOURLN")
        colParam.Add("WS_CLSMTHEND_SHMTHENDTRX_ADD", "OC_SH_MTHENDTRX_ADD")
        colParam.Add("PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET", "OC_SYSLOC_DETAILS_GET")
        colParam.Add("WS_CLSTRX_JOB_GENERATE_DNCN_GET", "OC_TRANSACTION_GET")
        colParam.Add("WS_CLSMTHEND_PROCESS_INTER_ESTATE", "OC_INTER_TRANSACTION_GET")
        
        ColParam.Add(strOpCode_DebitNote_Add, "OC_DEBIT_NOTE_ADD")
        ColParam.Add(strOpCode_DebitNote_Upd, "OC_DEBIT_NOTE_UPD")
        ColParam.Add(strOpCode_DebitNoteLine_Add, "OC_DEBIT_NOTE_LINE_ADD")
        ColParam.Add(strOpCode_DebitNoteLine_Sum, "OC_DEBIT_NOTE_LINE_AMOUNT_SUM_GET")
        ColParam.Add(strOpCode_DebitNote_Amt_Upd, "OC_DEBIT_NOTE_TOTAL_AMOUNT_UPD")
        ColParam.Add(strOpCode_CreditNote_Add, "OC_CREDIT_NOTE_ADD")
        ColParam.Add(strOpCode_CreditNote_Upd, "OC_CREDIT_NOTE_UPD")
        ColParam.Add(strOpCode_CreditNoteLine_Add, "OC_CREDIT_NOTE_LINE_ADD")
        ColParam.Add(strOpCode_CreditNoteLine_Sum, "OC_CREDIT_NOTE_LINE_AMOUNT_SUM_GET")
        ColParam.Add(strOpCode_CreditNote_Amt_Upd, "OC_CREDIT_NOTE_TOTAL_AMOUNT_UPD")

        colParam.Add(Session("SS_INACCMONTH"), "PM_ACCMONTH")
        colParam.Add(Session("SS_INACCYEAR"), "PM_ACCYEAR")
        colParam.Add(Session("SS_ARACCMONTH"), "PM_AR_ACCMONTH")
        colParam.Add(Session("SS_ARACCYEAR"), "PM_AR_ACCYEAR")
        colParam.Add(strCompany, "PM_COMPANY")
        colParam.Add(strLocation, "PM_LOCCODE")
        colParam.Add(strUserId, "PM_USERID")

        Try 
            intErrNo = objMthEnd.mtdMonthEndProcess(colParam, strErrMsg)
        Catch Exp As System.Exception 
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_MTHEND_PROCESS&errmesg=" & Exp.ToString & "&redirect=")
        End Try

        If intErrNo = objMthEnd.EnumException.NoError Then 
            strParam = strCompany & "|" & strLocation & "|" & strUserId
            Try
                intErrNo = objSysCfg.mtdGetSysLocInfo("PWSYSTEM_CLSCONFIG_SYSLOC_DETAILS_GET", _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    objSysLocDs, _
                                                    strParam)
            Catch Exp As System.Exception 
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WSMTHEND_GET_SYSLOC&errmesg=" & Exp.ToString() & "&redirect=WS/mthend/WS_MthEnd_Process.aspx")
            End Try

            Session("SS_INACCMONTH") = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
            Session("SS_INACCYEAR") = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
            strAccMonth = objSysLocDs.Tables(0).Rows(0).Item("INAccMonth").Trim()
            strAccYear = objSysLocDs.Tables(0).Rows(0).Item("INAccYear").Trim()
            onLoad_Display()
        Else
            lblActionResult.Text = strErrMsg
            lblActionResult.Visible = True
        End If        
    End Sub



End Class
