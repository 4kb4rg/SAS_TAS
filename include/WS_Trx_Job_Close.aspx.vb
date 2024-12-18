
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.Math 


Public Class WS_JobClose : Inherits Page
    
    Dim objINSetup As New agri.IN.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()
    
    Protected WithEvents ibClose As ImageButton
    Protected WithEvents ibBack As ImageButton

    Protected WithEvents lblJobID As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblStatusText As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDocRefNo As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblDocRefDate As Label
    Protected WithEvents lblUpdateDate As Label
    Protected WithEvents lblJobStartDate As Label
    Protected WithEvents lblUserName As Label
    Protected WithEvents lblJobEndDate As Label
    Protected WithEvents lblChrgRate As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblExpCode As Label
    Protected WithEvents lblAccCodeTag As Label
    Protected WithEvents lblBlkCodeTag As Label
    Protected WithEvents lblVehCodeTag As Label
    Protected WithEvents lblVehExpCodeTag As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblTotalQty As Label
    Protected WithEvents lblTotalCostAmt As Label
    Protected WithEvents lblTotalPriceAmt As Label
    Protected WithEvents lblTotalMechAmt As Label
    Protected WithEvents lblTotalMercTime As Label
    Protected WithEvents lblJobRemarks As Label
    Protected WithEvents lblEmpCodeTag As Label
    Protected WithEvents lblVehRegCodeTag As Label
    Protected WithEvents lblEmpCode As Label
    Protected WithEvents lblVehRegCode As Label
    Protected WithEvents lblRegCode As Label
    Protected WithEvents lblBillPartyCodeTag As Label
    Protected WithEvents lblBillPartyCode As Label

    
    Protected WithEvents dgJobStock As DataGrid
    Protected WithEvents dgMechHour As DataGrid

    Protected objWSTrx As New agri.WS.clsTrx()  
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strOpCode_Job_Get As String = "WS_CLSTRX_JOB_GET"
    Dim strOpCode_JobStock_Get As String = "WS_CLSTRX_JOB_STOCK_GET"
    Dim strOpCode_Mech_Hour_Get As String = "WS_CLSTRX_MECHANIC_HOUR_DOCUMENT_GET"
    Dim strOpCode_Job_Close_Upd As String = "WS_CLSTRX_JOB_UPD"           

    Dim dsLangCap As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intWSAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intConfigsetting As Integer
    Dim strDateFMT As String
    Dim strLocType as String


    Dim intErrNo As Integer

    Protected WithEvents lblBPErr As Label
    Protected WithEvents lblLocCodeErr As Label

    Dim strInterLocCode As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intConfigsetting = Session("SS_CONFIGSETTING")
        intWSAR = Session("SS_WSAR")
        strDateFMT = Session("SS_DATEFMT")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSJobRegistration), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            
            If Not IsPostBack Then
                 GetLangCap()

                lblJobID.Text = Request.QueryString("jobid")
                ResetPage()
            End If
            
            lblBPErr.Visible = False
            lblLocCodeErr.Visible = False
        End If
    End Sub

    Sub ResetPage()
        lblAccCodeTag.Visible = False
        lblBlkCodeTag.Visible = False
        lblVehCodeTag.Visible = False
        lblVehExpCodeTag.Visible = False
        lblEmpCodeTag.Visible = False
        lblEmpCode.Visible = False    
        lblVehRegCodeTag.Visible = False
        lblVehRegCode.Visible = False
        lblBillPartyCodeTag.Visible = False
        lblBillPartyCode.Visible = False
        

        DisplayJobHeader()
        DisplayJobStock()
        DisplayMechHours()

        If lblStatus.Text <> objWSTrx.EnumJobStatus.Active Then
            ibClose.Visible = False
        Else
            ibClose.Visible = True
        End If
    End Sub


    Sub GetLangCap()
        dsLangCap = GetLanguageCaptionDS()
        
        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigsetting) = True Then
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text & " :"
        Else
            lblBlkCodeTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text & " :"
        End If
        
        lblAccCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.Text & " :"
        lblVehCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblCode.Text & " :"
        lblVehExpCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Expense) & lblCode.Text & "(Labour) :"        
        lblVehRegCodeTag.Text = GetCaption(objLangCap.EnumLangCap.Vehicle) & lblRegCode.Text & lblCode.Text & " :"
        lblBillPartyCodeTag.Text = GetCaption(objLangCap.EnumLangCap.BillParty) & lblCode.Text & " :"

        dgJobStock.Columns(2).HeaderText = GetCaption(objLangCap.EnumLangCap.Work) & lblCode.Text
        dgMechHour.Columns(2).HeaderText = GetCaption(objLangCap.EnumLangCap.Work) & lblCode.Text

    End Sub



    Function GetCaption(ByVal pv_TermCode as String) As String
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




    Protected Function GetJobHeaderDS() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String
    
        Try
            strSearch = " WHERE J.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "'"
            colParam.Add(strSearch, "PM_SEARCH")
            colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")
            
            intErrNo = objWSTrx.mtdJob_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If
            
            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GET_JOB_HEADER&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Close.aspx")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GET_LANGCAP&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Close.aspx")
        End Try

        Return dsLC
        If Not dsLC Is Nothing Then
            dsLC = Nothing
        End If
    End Function

    Sub DisplayJobHeader()
        Dim dsHeader As DataSet
        Dim iJobType As Integer

        dsHeader = GetJobHeaderDS()
        lblJobID.Text = Trim(dsHeader.Tables(0).Rows(0).Item("JobID"))
        lblDescription.Text = Trim(dsHeader.Tables(0).Rows(0).Item("Description"))
        lblDocRefNo.Text = Trim(dsHeader.Tables(0).Rows(0).Item("DocRefNo"))
        lblDocRefDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("DocRefDate"))
        lblJobStartDate.Text = FormatDate(dsHeader.Tables(0).Rows(0).Item("JobStartDate"), 1)
        lblJobEndDate.Text = FormatDate(dsHeader.Tables(0).Rows(0).Item("JobEndDate"), 1)

        lblChrgRate.Text = objGlobal.GetIDDecimalSeparator(ROUND(dsHeader.Tables(0).Rows(0).Item("ChrgRate"),0))                

        lblPeriod.Text = Trim(dsHeader.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsHeader.Tables(0).Rows(0).Item("AccYear"))
        lblStatus.Text = Trim(dsHeader.Tables(0).Rows(0).Item("Status"))
        lblStatusText.Text = objWSTrx.mtdGetJobStatus(lblStatus.Text)
        lblCreateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("CreateDate"))
        lblUpdateDate.Text = objGlobal.GetLongDate(dsHeader.Tables(0).Rows(0).Item("UpdateDate"))
        lblUserName.Text = Trim(dsHeader.Tables(0).Rows(0).Item("UserName"))
        lblJobRemarks.Text = Trim(dsHeader.Tables(0).Rows(0).Item("Remark"))

        iJobType = dsHeader.Tables(0).Rows(0).Item("JobType")

        Select Case iJobType
            Case objWSTrx.EnumJobType.InternalUse
                    lblAccCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("AccCode"))
                    lblBlkCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("BlkCode"))
                    lblVehCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("VehCode"))
                    lblExpCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("VehLabExpCode"))

                    lblAccCode.Visible = True
                    lblBlkCode.Visible = True
                    lblVehCode.Visible = True
                    lblExpCode.Visible = True

                    lblAccCodeTag.Visible = True
                    lblBlkCodeTag.Visible = True
                    lblVehCodeTag.Visible = True
                    lblVehExpCodeTag.Visible = True

            Case objWSTrx.EnumJobType.StaffPayroll, objWSTrx.EnumJobType.StaffDebitNote
                    lblEmpCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("EmpCode"))
                    lblVehRegCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("VehRegNo"))
                    
                    lblEmpCode.Visible = True
                    lblVehRegCode.Visible = True

                    lblEmpCodeTag.Visible = True
                    lblVehRegCodeTag.Visible = True



            Case objWSTrx.EnumJobType.ExternalParty
                    lblBillPartyCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("BillPartyCode"))
                    lblVehRegCode.Text = Trim(dsHeader.Tables(0).Rows(0).Item("VehRegNo"))
                    
                    lblBillPartyCode.Visible = True
                    lblVehRegCode.Visible = True

                    lblBillPartyCodeTag.Visible = True
                    lblVehRegCodeTag.Visible = True
        End Select
    End Sub

    Sub DisplayJobStock()
        Dim dsJobStock As New DataSet()
        Dim iCounter As Integer = 0
        Dim dTotalQty As Decimal = 0
        Dim dTotalCostAmt As Decimal = 0
        Dim dTotalPriceAmt As Decimal = 0

        dsJobStock = GetJobStockDS()
        dgJobStock.DataSource = dsJobStock
        dgJobStock.DataBind()

        If dsJobStock.Tables(0).Rows.Count > 0 Then
            For iCounter = 0 To dsJobStock.Tables(0).Rows.Count - 1

                If Trim(dsJobStock.Tables(0).Rows(iCounter).Item("TransType")) = Trim(CStr(objWSTrx.EnumJobStockType.Issued)) Then
                    dTotalQty = dTotalQty + (dsJobStock.Tables(0).Rows(iCounter).Item("Qty") - dsJobStock.Tables(0).Rows(iCounter).Item("QtyReturn"))
                    dTotalCostAmt = dTotalCostAmt + (dsJobStock.Tables(0).Rows(iCounter).Item("Amount") - dsJobStock.Tables(0).Rows(iCounter).Item("AmountReturn"))
                    dTotalPriceAmt = dTotalPriceAmt + (dsJobStock.Tables(0).Rows(iCounter).Item("PriceAmount") - dsJobStock.Tables(0).Rows(iCounter).Item("PriceAmountReturn"))
                End If
            Next
        End If
        
        lblTotalQty.Text = FormatNumber(dTotalQty, 5, True, False, False)

        lblTotalCostAmt.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(dTotalCostAmt, 0), 0, True, False, False))
        lblTotalPriceAmt.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(dTotalPriceAmt, 0), 0, True, False, False))
    End Sub
    
    Sub DisplayMechHours()
        Dim dsMechHour As New DataSet()
        Dim iCounter As Integer = 0
        Dim dTotalMechAmt As Decimal = 0
        Dim iHour As Integer = 0
        Dim iMin As Integer = 0

        dsMechHour = GetMechHourDS()
        dgMechHour.DataSource = dsMechHour
        dgMechHour.DataBind()

        If dsMechHour.Tables(0).Rows.Count > 0 Then
            For iCounter = 0 To dsMechHour.Tables(0).Rows.Count - 1
                dTotalMechAmt = dTotalMechAmt + dsMechHour.Tables(0).Rows(iCounter).Item("OriginalAmount")
                iHour = iHour + dsMechHour.Tables(0).Rows(iCounter).Item("HourSpent")
                iMin = iMin + dsMechHour.Tables(0).Rows(iCounter).Item("MinuteSpent")
            Next
        End If

        If iMin >= 60 Then
            iHour = iHour + (iMin / 60)
        End If
        iMin = iMin Mod 60


        lblTotalMechAmt.Text = objGlobal.GetIDDecimalSeparator(FormatNumber(dTotalMechAmt, 0, True, False, False))

        lblTotalMercTime.Text = Format$(iHour, "00") + ":" + Format$(iMin, "00")
    End Sub

    Protected Function GetJobStockDS() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String
    
        Try
            strSearch = " WHERE JS.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "' AND JS.TransType ='" & FixSQL(TRIM(CSTR(objWSTrx.EnumJobStockType.Issued))) & "' AND JS.Qty <> JS.QtyReturn"
            colParam.Add(strSearch, "PM_SEARCH")            
            colParam.Add(strOpCode_JobStock_Get, "OC_JOB_STOCK_GET")
            
            intErrNo = objWSTrx.mtdJobStock_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If
            
            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_STOCK_GET_JOB_LINE&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Close.aspx")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function

    Protected Function GetMechHourDS() As DataSet
        Dim dsTemp As New DataSet
        Dim colParam As New Collection
        Dim strSearch As String
        Dim strErrMsg As String
    
        Try
            strSearch = " WHERE MHL.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "' AND MH.Status In (" & objWSTrx.EnumMechanicHourStatus.Active & "," & objWSTrx.EnumMechanicHourStatus.Closed & ")"
            colParam.Add(strSearch, "PM_SEARCH")            
            colParam.Add(strOpCode_Mech_Hour_Get, "OC_MECHANIC_HOUR_GET")
            
            intErrNo = objWSTrx.mtdMechanicHour_Get(colParam, dsTemp, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            End If
            
            Return dsTemp
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_MECHANIC_HOUR_DOCUMENT_GET&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Close.aspx")
        Finally
            If Not dsTemp Is Nothing Then
                dsTemp = Nothing
            End If
        End Try
    End Function 

    Sub CloseJob()
        Dim colParam As New Collection
        Dim strErrMsg As String

        If CheckLocBillParty() = False Then
            lblBPErr.Visible = True
            lblLocCodeErr.Visible = True
            Exit Sub
        End If
    
        Try
            colParam.Add(lblJobID.Text, "PM_JOBID")
            colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")
            
            colParam.Add(Trim(strUserId), "PM_UPDATEID")
            colParam.Add(strOpCode_Job_Close_Upd, "OC_JOB_UPD")
            
            intErrNo = objWSTrx.mtdJob_Close(colParam, strErrMsg)
            If intErrNo <> objWSTrx.EnumException.NoError Then
                Throw New System.Exception(strErrMsg)
            Else
                ResetPage()
            End If
            
            
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_UPD&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Close.aspx")
        Finally            
        End Try

        If TRIM(strLocation) <> TRIM(strInterLocCode) Then
            mtdGenerateDNCN(True, True)

        End If
    End Sub


    Protected Function FixSQL(ByVal pv_strParam As String) As String
        Return Replace(Trim(pv_strParam), "'", "''")
    End Function

    Protected Function RoundNumber(ByVal d As Double, Optional ByVal decimals As Double = 0) As Double
        RoundNumber = Decimal.Divide(Int(Decimal.Add(Decimal.Multiply(d, 10.0 ^ decimals), 0.5)), 10.0 ^ decimals)
    End Function

    Protected Function FormatDate(ByVal pv_strDate As String, ByVal pv_iFormatType As Integer) As String
        Dim stResult As String = ""
        Dim stDate As String = ""
        Dim stHour As String = ""
        Dim stMin As String = ""
        
        Select Case pv_iFormatType
            Case 1  
                If IsDate(pv_strDate) = True Then
                    If DateDiff(DateInterval.Day, CDate(pv_strDate), CDate("1 Jan 1900")) <> 0 Then
                        stDate = objGlobal.GetLongDate(pv_strDate)
                        stHour = Right("00" & ((Hour(pv_strDate) + 11) Mod 12) + 1, 2)
                        stMin = Right("00" & Minute(pv_strDate), 2)

                    End If
                End If
            Case 2 
                If IsDate(pv_strDate) = True Then
                    If DateDiff(DateInterval.Day, CDate(pv_strDate), CDate("1 Jan 1900")) <> 0 Then
                        stDate = objGlobal.GetShortDate(strDateFMT, pv_strDate)
                        stResult = stDate
                    End If
                End If
        End Select

        If pv_iFormatType = 1 Then
            stResult = stDate + " " + stHour + ":" + stMin
            If Hour(pv_strDate) > 11 Then
                stResult = stResult + " PM"
            Else
                stResult = stResult + " AM"
            End If
        End If

        Return stResult
    End Function

    Sub ibBack_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("WS_Trx_Job_Detail.aspx?jobid=" & Trim(lblJobID.Text))
    End Sub

    Sub ibClose_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)        
        CloseJob()
    End Sub

    Function CheckLocBillParty () As Boolean
        Dim strOpCd_Get As String = "GL_CLSSETUP_BILLPARTY_GET"
        Dim strOpCdStckTxDet_GET As String = "IN_CLSTRX_STOCKISSUE_DETAIL_GET"
        Dim strSearch As String
        Dim objBPLocDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String
        Dim intCnt As Integer
        Dim strLocCode As String
        Dim objWSJobDS As New DataSet()
        Dim colParam As New Collection
        Dim strErrMsg As String

        Try
            strSearch = " WHERE J.JobID = '" & FixSQL(Trim(lblJobID.Text)) & "'"
            colParam.Add(strSearch, "PM_SEARCH")
            colParam.Add(strOpCode_Job_Get, "OC_JOB_GET")
            
            intErrNo = objWSTrx.mtdJob_Get(colParam, objWSJobDS, strErrMsg)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GET_JOB_HEADER&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Close.aspx")
        End Try

        For intCnt = 0 To objWSJobDS.Tables(0).Rows.Count - 1
            strLocCode = TRIM(objWSJobDS.Tables(0).Rows(intCnt).Item("ChargeLocCode"))
            strInterLocCode = strLocCode

            If Not (strLocCode = "" Or strLocCode = TRIM(strLocation)) Then
                strSearch = " AND BP.Status = '" & objGLSetup.EnumBillPartyStatus.Active & "'" & _
                            " AND BP.InterLocCode = '" & strLocCode & "'" 
                    
                Try
                    intErrNo = objGLSetup.mtdGetBPInterLoc(strOpCd_Get, strSearch, objBPLocDs)
                Catch ex As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BI_SETUP_BILLPARTY_GET&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Close.asp")
                End Try

                If objBPLocDs.Tables(0).Rows.Count <= 0 Then
                    lblLocCodeErr.Text = strLocCode
                    return False
                End If
            End If
        Next intCnt

        return True
    End Function

    Sub mtdGenerateDNCN(ByVal pv_blnLabour As Boolean, ByVal pv_blnParts As Boolean)
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
        Dim strDebitNoteID As String = ""
        Dim strCreditNoteID As String = ""
        
        colParam.Add(Trim(lblJobID.Text), "PM_JOBID")
        colParam.Add(IIf(pv_blnLabour = True, "true", "false"), "PM_LABOUR")
        colParam.Add(IIf(pv_blnParts = True, "true", "false"), "PM_PARTS")
        colParam.Add(Trim(strLocation), "PM_LOCCODE")
        colParam.Add(Session("SS_ARACCMONTH"), "PM_AR_ACCMONTH")
        colParam.Add(Session("SS_ARACCYEAR"), "PM_AR_ACCYEAR")
        colParam.Add(Trim(strCompany), "PM_COMPANY")
        colParam.Add(Trim(strUserId), "PM_UPDATEID")

        colParam.Add("WS_CLSTRX_INTERNAL_JOB_GENERATE_DNCN_GET", "OC_TRANSACTION_GET")
        colParam.Add("WS_CLSTRX_JOB_UPD", "OC_JOB_UPD")
        colParam.Add("WS_CLSTRX_JOB_STOCK_UPD", "OC_JOB_STOCK_UPD")
        colParam.Add("WS_CLSTRX_MECHANIC_HOUR_UPD", "OC_MECHANIC_HOUR_UPD")
        colParam.Add("WS_CLSTRX_MECHANIC_HOUR_LINE_UPD", "OC_MECHANIC_HOUR_LINE_UPD")

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
        Try
            intErrNo = objWSTrx.mtdInternalJob_GenerateDNCN(colParam, strDebitNoteID, strCreditNoteID, strErrMsg)
        Catch ex As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSTRX_JOB_DETAIL_GENERATE_DNCN&errmesg=" & Server.UrlEncode(ex.Message) & "&redirect=WS/trx/WS_Trx_Job_Close.aspx")
        End Try
    End Sub


End Class

