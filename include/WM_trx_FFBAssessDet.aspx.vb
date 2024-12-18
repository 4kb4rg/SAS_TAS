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


Public Class WM_FFBAssess_Det : Inherits Page

    Dim dsFFBItem As DataSet

    Protected objWMTrx As New agri.WM.clsTrx()
    Dim objWMSetup As New agri.WM.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents hidGradedBunch As HtmlInputHidden
    Protected WithEvents hidCustNetWgt As HtmlInputHidden
    Protected WithEvents hidTicketNo As HtmlInputHidden
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblErrDateInsp As Label
    Protected WithEvents lblErrDateInspMsg As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label

    Protected WithEvents lstTicketNo As DropDownList
    Protected WithEvents txtDateInsp As TextBox
    Protected WithEvents txtRipeBunches As TextBox
    Protected WithEvents txtOverRipeBunches As TextBox
    Protected WithEvents txtUnderRipeBunches As TextBox
    Protected WithEvents txtUnripeBunches As TextBox
    Protected WithEvents txtEmptyBunches As TextBox
    Protected WithEvents txtRottenBunches As TextBox
    Protected WithEvents txtPoorBunches As TextBox
    Protected WithEvents txtSmallBunches As TextBox
    Protected WithEvents txtLongStalkBunches As TextBox
    Protected WithEvents txtGradedPct As TextBox
    Protected WithEvents txtUngBunches As TextBox
    Protected WithEvents txtRemarks As TextBox
    Protected WithEvents txtOthers As TextBox
    Protected WithEvents txtContamination As TextBox

    Protected WithEvents btnSave As ImageButton
    Protected WithEvents btnDelete As ImageButton
    Protected WithEvents btnUnDelete As ImageButton

    Dim strOppCd_FFB_Assess_GET As String = "WM_CLSTRX_FFB_ASSESS_GET"
    Dim strOppCd_FFB_Assess_ADD As String = "WM_CLSTRX_FFB_ASSESS_ADD"
    Dim strOppCd_FFB_Assess_UPD As String = "WM_CLSTRX_FFB_ASSESS_UPD"

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWMAR As Integer
    Dim strDateFormat As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")
        strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            lblErrDateInsp.Visible = False
            lblErrDateInspMsg.Visible = False

            If Not Page.IsPostBack Then
                If Not Request.QueryString("TicketNo") = "" Then
                    hidTicketNo.Value = Request.QueryString("TicketNo")
                End If

                If Not hidTicketNo.Value = "" Then
                    BindTicketNo(hidTicketNo.Value)
                    DisplayData()

                    blnUpdate.Text = True
                Else
                    BindTicketNo("")
                    EnableControl()

                    blnUpdate.Text = False
                End If
            End If
        End If
    End Sub

    Protected Function LoadData() As DataSet

        If txtDateInsp.Text.Trim = "1/1/1900" Then
            txtDateInsp.Text = ""
        End If

        strParam = "|||||AND FFB.TicketNo = '" & hidTicketNo.Value & "' |LocCode, FFB.TicketNo|"

        Try
            intErrNo = objWMTrx.mtdGetFFBAssess(strOppCd_FFB_Assess_GET, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_FFB_ASSESS_DETAILS_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_FFBAssessDet.aspx")
        End Try

        Return objDataSet

    End Function

    Sub Date_Insp(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOppCd_WeighBridge_Ticket_GET As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_GET"
        Dim dsWBTicketNo As New DataSet()

        strParam = "||||||||LocCode, TIC.TicketNo||" & lstTicketNo.SelectedItem.Value.Trim
        Try
            intErrNo = objWMTrx.mtdGetWeighBridgeTicket(strOppCd_WeighBridge_Ticket_GET, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        dsWBTicketNo)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_FFBASSESSDET_TICKETNO_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_FFBAssessDet.aspx")
        End Try

        txtDateInsp.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsWBTicketNo.Tables(0).Rows(0).Item("InDate")))
    End Sub

    Sub BindTicketNo(ByVal pv_strTicketNo As String)
        Dim strOpCd_Ticket_GET As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_GET"
        Dim dsTicket As New DataSet()
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intIndex As Integer

        If hidTicketNo.Value = "" Then
            strParam = "|||||" & objWMTrx.EnumWeighBridgeTicketStatus.Active & _
                       "||AND TIC.TicketNo NOT IN (SELECT TicketNo FROM WM_FFBASSESS WHERE LocCode = '" & strLocation & "' " & vbCrLf & _
                       "AND AccMonth = '" & strAccMonth & "' AND AccYear = '" & strAccYear & "') " & vbCrLf & _
                       "AND TIC.ProductCode = '" & objWMTrx.EnumWeighBridgeTicketProduct.FFB & "' |TicketNo||"
        Else
            strParam = "||||||||TicketNo||"
        End If

        Try
            intErrNo = objWMTrx.mtdGetWeighBridgeTicket(strOpCd_Ticket_GET, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        dsTicket)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_FFB_ASSESS_TICKET_DROPDOWNLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_FFBAssessDet.aspx")
        End Try

        If dsTicket.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsTicket.Tables(0).Rows.Count - 1
                dsTicket.Tables(0).Rows(intCnt).Item("TicketNo") = Trim(dsTicket.Tables(0).Rows(intCnt).Item("TicketNo"))
                dsTicket.Tables(0).Rows(intCnt).Item("TransType") = Trim(dsTicket.Tables(0).Rows(intCnt).Item("TicketNo"))

                If dsTicket.Tables(0).Rows(intCnt).Item("TicketNo") = pv_strTicketNo Then
                    intIndex = intCnt + 1
                End If
            Next intCnt
        End If

        dr = dsTicket.Tables(0).NewRow()
        dr("TicketNo") = ""
        dr("TransType") = "Select Ticket No."
        dsTicket.Tables(0).Rows.InsertAt(dr, 0)

        lstTicketNo.DataSource = dsTicket.Tables(0)
        lstTicketNo.DataValueField = "TicketNo"
        lstTicketNo.DataTextField = "TransType"
        lstTicketNo.DataBind()
        lstTicketNo.SelectedIndex = intIndex

        If Not dsTicket Is Nothing Then
            dsTicket = Nothing
        End If

    End Sub

    Sub DisableControl()

        lstTicketNo.Enabled = False
        txtDateInsp.Enabled = False
        txtRipeBunches.Enabled = False
        txtOverRipeBunches.Enabled = False
        txtUnderRipeBunches.Enabled = False
        txtUnripeBunches.Enabled = False
        txtEmptyBunches.Enabled = False
        txtRottenBunches.Enabled = False
        txtPoorBunches.Enabled = False
        txtSmallBunches.Enabled = False
        txtLongStalkBunches.Enabled = False
        txtGradedPct.Enabled = False
        txtUngBunches.Enabled = False
        txtRemarks.Enabled = False

        txtContamination.Enabled = False
        txtOthers.Enabled = False

        lblPeriod.Enabled = False
        lblStatus.Enabled = False
        lblCreateDate.Enabled = False
        lblLastUpdate.Enabled = False
        lblUpdateBy.Enabled = False

        btnSave.Visible = False
        btnDelete.Visible = False
        btnUnDelete.Visible = True

    End Sub

    Sub EnableControl()
        txtDateInsp.Enabled = True
        txtRipeBunches.Enabled = True
        txtOverRipeBunches.Enabled = True
        txtUnderRipeBunches.Enabled = True
        txtUnripeBunches.Enabled = True
        txtEmptyBunches.Enabled = True
        txtRottenBunches.Enabled = True
        txtPoorBunches.Enabled = True
        txtSmallBunches.Enabled = True
        txtLongStalkBunches.Enabled = True
        txtGradedPct.Enabled = True
        txtUngBunches.Enabled = True
        txtRemarks.Enabled = True

        txtContamination.Enabled = True
        txtOthers.Enabled = True

        lblPeriod.Enabled = True
        lblStatus.Enabled = True
        lblCreateDate.Enabled = True
        lblLastUpdate.Enabled = True
        lblUpdateBy.Enabled = True

        btnSave.Visible = True

        If hidTicketNo.Value <> "" Then
            lstTicketNo.Enabled = False
            btnDelete.Visible = True
            btnDelete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            btnUnDelete.Visible = False
        Else
            lstTicketNo.Enabled = True
            btnDelete.Visible = False
            btnUnDelete.Visible = False
        End If
    End Sub

    Sub DisplayData()

        dsFFBItem = LoadData()

        If dsFFBItem.Tables(0).Rows.Count > 0 Then

            lblPeriod.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsFFBItem.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = Trim(objWMTrx.mtdGetWeighBridgeTicketStatus(dsFFBItem.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = Trim(objGlobal.Getlongdate(dsFFBItem.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = Trim(objGlobal.Getlongdate(dsFFBItem.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("UserName"))

            BindTicketNo(Trim(dsFFBItem.Tables(0).Rows(0).Item("TicketNo")))
            txtDateInsp.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsFFBItem.Tables(0).Rows(0).Item("InspectedDate")))
            txtRipeBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("RipeBunch"))
            txtOverRipeBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("OverripeBunch"))
            txtUnderRipeBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("UnderripeBunch"))
            txtUnripeBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("UnripeBunch"))
            txtEmptyBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("EmptyBunch"))
            txtRottenBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("RottenBunch"))
            txtPoorBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("PoorBunch"))
            txtSmallBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("SmallBunch"))
            txtLongStalkBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("LongStalkBunch"))
            txtGradedPct.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("GradedPercent"))
            txtUngBunches.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("UngradableBunch"))
            txtRemarks.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("Remark"))

            txtContamination.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("Contamination"))
            txtOthers.Text = Trim(dsFFBItem.Tables(0).Rows(0).Item("Others"))

            hidGradedBunch.Value = Trim(dsFFBItem.Tables(0).Rows(0).Item("GradedBunch"))

            Select Case Trim(lblStatus.Text)
                Case objWMTrx.mtdGetFFBAssessStatus(objWMTrx.EnumFFBAssessStatus.Active)
                    EnableControl()
                Case objWMTrx.mtdGetFFBAssessStatus(objWMTrx.EnumFFBAssessStatus.Deleted)
                    DisableControl()
            End Select
        End If
    End Sub


    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim strDateInsp As String
        Dim decBunchesGraded As Decimal

        If Not txtDateInsp.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(txtDateInsp.Text), objFormatDate, objActualDate) = False Then
                lblErrDateInsp.Visible = True
                lblErrDateInsp.Text = lblErrDateInspMsg.Text & objFormatDate
                Exit Sub
            Else
                strDateInsp = objActualDate
            End If
        End If
        decBunchesGraded = hidGradedBunch.Value

        strParam = _
        Trim(lstTicketNo.SelectedItem.Value) & "|" & _
        IIf(Trim(strDateInsp) = "", "", Trim(strDateInsp)) & "|" & _
        IIf(Trim(txtRipeBunches.Text) = "", 0, Trim(txtRipeBunches.Text)) & "|" & _
        IIf(Trim(txtOverRipeBunches.Text) = "", 0, Trim(txtOverRipeBunches.Text)) & "|" & _
        IIf(Trim(txtUnderRipeBunches.Text) = "", 0, Trim(txtUnderRipeBunches.Text)) & "|" & _
        IIf(Trim(txtUnripeBunches.Text) = "", 0, Trim(txtUnripeBunches.Text)) & "|" & _
        IIf(Trim(txtEmptyBunches.Text) = "", 0, Trim(txtEmptyBunches.Text)) & "|" & _
        IIf(Trim(txtRottenBunches.Text) = "", 0, Trim(txtRottenBunches.Text)) & "|" & _
        IIf(Trim(txtPoorBunches.Text) = "", 0, Trim(txtPoorBunches.Text)) & "|" & _
        IIf(Trim(txtSmallBunches.Text) = "", 0, Trim(txtSmallBunches.Text)) & "|" & _
        IIf(Trim(txtLongStalkBunches.Text) = "", 0, Trim(txtLongStalkBunches.Text)) & "|" & _
        IIf(Trim(decBunchesGraded) = "", 0, Trim(decBunchesGraded)) & "|" & _
        IIf(Trim(txtGradedPct.Text) = "", 0, Trim(txtGradedPct.Text)) & "|" & _
        IIf(Trim(txtUngBunches.Text) = "", 0, Trim(txtUngBunches.Text)) & "|" & _
        IIf(Trim(txtRemarks.Text) = "", "", Trim(txtRemarks.Text)) & "|" & _
        objWMTrx.EnumFFBAssessStatus.Active & "|" & _
        IIf(Trim(txtContamination.Text) = "", 0, Trim(txtContamination.Text)) & "|" & _
        IIf(Trim(txtOthers.Text) = "", 0, Trim(txtOthers.Text)) & "|" 

        Try
            intErrNo = objWMTrx.mtdUpdFFBAssess(strOppCd_FFB_Assess_ADD, _
                                                strOppCd_FFB_Assess_UPD, _
                                                strOppCd_FFB_Assess_GET, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_FFB_ASSESS_DET_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_FFBAssessDet.aspx")
        End Try

        Select Case blnUpdate.Text
            Case True
            Case False
                Response.Redirect("WM_trx_FFBAssessDet.aspx")
        End Select

    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        blnUpdate.Text = True
        strParam = Trim(lstTicketNo.SelectedItem.Value) & "|||||||||||||||" & objWMTrx.EnumFFBAssessStatus.Deleted & "||"
        Try
            intErrNo = objWMTrx.mtdUpdFFBAssess(strOppCd_FFB_Assess_ADD, _
                                                strOppCd_FFB_Assess_UPD, _
                                                strOppCd_FFB_Assess_GET, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_FFB_ASSESS_DET_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_FFBAssessDet.aspx")
        End Try

        DisplayData()
    End Sub

    Sub btnUnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        blnUpdate.Text = True
        strParam = Trim(lstTicketNo.SelectedItem.Value) & "|||||||||||||||" & objWMTrx.EnumFFBAssessStatus.Active & "||"
        
        Try
            intErrNo = objWMTrx.mtdUpdFFBAssess(strOppCd_FFB_Assess_ADD, _
                                                strOppCd_FFB_Assess_UPD, _
                                                strOppCd_FFB_Assess_GET, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_FFB_ASSESS_DET_UNDELETE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_FFBAssessDet.aspx")
        End Try

        DisplayData()
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WM_trx_FFBAssessList.aspx")
    End Sub


End Class
