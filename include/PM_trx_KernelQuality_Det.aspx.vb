
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
Imports System.Globalization

Public Class PM_KernelQuality_Det : Inherits Page

    Dim objPMTrx As New agri.PM.clsTrx()
    Dim objPMSetup As New agri.PM.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblOER As Label
    Protected WithEvents lblKER As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents lblTotalDirt As TextBox

    Protected WithEvents txtdate As TextBox    
    Protected WithEvents ddlProcessingLnNo As DropDownList

    Protected WithEvents Save As ImageButton
    Protected WithEvents Delete As ImageButton
    Protected WithEvents btnSelDateFrom As Image
    Dim strTransDate As String
    Dim strEdit As String
    Dim strProcessingLnNo As String

    Dim strParam As String
    Dim objDataSet As New DataSet()
    Dim objProcessingLine As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim strDateFormat As String
    Protected WithEvents lblTracker As System.Web.UI.WebControls.Label
    Protected WithEvents rfvDate As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvProcessingLnNo As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Back As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtDrySepBK As System.Web.UI.WebControls.TextBox
    Protected WithEvents revDrySepBK As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvDrySepBK As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtDrySepLShell As System.Web.UI.WebControls.TextBox
    Protected WithEvents revDryBathLShell As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvDryBathLShell As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtDrySepWN As System.Web.UI.WebControls.TextBox
    Protected WithEvents revDryBathWN As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvDryBathWN As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtDrySepBN As System.Web.UI.WebControls.TextBox
    Protected WithEvents revDryBathBN As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvDryBathBN As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtDrySepTotalDirt As System.Web.UI.WebControls.TextBox
    Protected WithEvents revDryBathTotalDirt As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvDryBathTotalDirt As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtClayBathBK As System.Web.UI.WebControls.TextBox
    Protected WithEvents revClayBathBK As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvClayBathBK As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtClayBathLShell As System.Web.UI.WebControls.TextBox
    Protected WithEvents revClayBathLShell As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvClayBathLShell As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtClayBathWN As System.Web.UI.WebControls.TextBox
    Protected WithEvents revClayBathWN As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvClayBathWN As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtClayBathBN As System.Web.UI.WebControls.TextBox
    Protected WithEvents revClayBathBN As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvClayBathBN As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtClayBathTotalDirt As System.Web.UI.WebControls.TextBox
    Protected WithEvents revClayBathTotalDirt As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvClayBathTotalDirt As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtClayBathFlowRate As System.Web.UI.WebControls.TextBox
    Protected WithEvents revClayBathFlowRate As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvClayBathFlowRate As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvDryBathFlowRate As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents revDryBathFlowRate As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtDrySepFlowRate As System.Web.UI.WebControls.TextBox
    Dim strOppCd_KernelQuality_GET As String = "PM_CLSTRX_KERNEL_QUALITY_GET"
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Private Sub InitializeComponent()

    End Sub

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")
	    
        strLocType = Session("SS_LOCTYPE")
        strTransDate = Request.QueryString("TransDate")
        strEdit = Request.QueryString("Edit")
        strProcessingLnNo = Request.QueryString("ProcessingLnNo")



        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMKernelQuality), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                BindProcessingLineList()
                If Not (Request.QueryString("LocCode") = "" And Request.QueryString("TransDate") = "") Then
                    strTransDate = Request.QueryString("TransDate")
                    strEdit = Request.QueryString("Edit")
                End If

                If strEdit = "True" Then
                    DisplayData()
                    blnUpdate.Text = False
                    txtdate.Enabled = False
                    ddlProcessingLnNo.Enabled = False
                    btnSelDateFrom.Visible = False
                Else
                    blnUpdate.Text = True
                    txtdate.Enabled = True
                    ddlProcessingLnNo.Enabled = True
                    btnSelDateFrom.Visible = True
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_KernelQuality_list.aspx")
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



    Protected Function LoadData() As DataSet

        strParam = "||TransDate||" & _
                   strTransDate & "||" & strProcessingLnNo

        Try
            intErrNo = objPMTrx.mtdGetKernelQuality(strOppCd_KernelQuality_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_KernelQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_KernelQuality_List.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumKernelQualityStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumKernelQualityStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()

        Dim dsKernelQuality As DataSet = LoadData()

        If dsKernelQuality.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsKernelQuality.Tables(0).Rows(0).Item("TransDate")))
            ddlProcessingLnNo.SelectedItem.Text = Trim(dsKernelQuality.Tables(0).Rows(0).Item("ProcessLnNo"))
            ddlProcessingLnNo.SelectedItem.Value = Trim(dsKernelQuality.Tables(0).Rows(0).Item("ProcessLnNo"))

            With dsKernelQuality.Tables(0).Rows(0)
                txtDrySepBK.Text = .Item("DrySepBK")
                txtDrySepLShell.Text = .Item("DrySepLShell")
                txtDrySepWN.Text = .Item("DrySepWN")
                txtDrySepBN.Text = .Item("DrySepBN")
                txtDrySepTotalDirt.Text = .Item("DrySepTotalDirt")
                txtDrySepFlowRate.Text = .Item("DrySepFlowRate")
                txtClayBathBK.Text = .Item("ClayBathBK")
                txtClayBathLShell.Text = .Item("ClayBathLShell")
                txtClayBathWN.Text = .Item("ClayBathWN")
                txtClayBathBN.Text = .Item("ClayBathBN")
                txtClayBathTotalDirt.Text = .Item("ClayBathTotalDirt")
                txtClayBathFlowRate.Text = .Item("ClayBathFlowRate")
            End With

            lblPeriod.Text = Trim(dsKernelQuality.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsKernelQuality.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objPMTrx.mtdGetKernelQualityStatus(Trim(dsKernelQuality.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsKernelQuality.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsKernelQuality.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsKernelQuality.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsKernelQuality.Tables(0).Rows(0).Item("Status")))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.Visible = True
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_KernelQuality_ADD As String = "PM_CLSTRX_KERNEL_QUALITY_ADD"
        Dim strOppCd_KernelQuality_UPD As String = "PM_CLSTRX_KERNEL_QUALITY_UPD"
        Dim blnDupKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strProcessingLnNo As String
        Dim strRMCrackingEfficiency1 As String
        Dim strRMCrackingEfficiency2 As String
        Dim strSCCrackingEfficiency As String
        Dim strKLFibreCyclone As String
        Dim strKLDryShell As String
        Dim strKLDestonerAirlock As String
        Dim strKLKernelVibratingScreen As String
        Dim strDKWholeNut As String
        Dim strDKBrokenNut As String
        Dim strDKFreeShell As String
        Dim strDKStone As String
        Dim strDKTotalDirt As String
        Dim strWKHalfCrackedNut As String
        Dim strWKFreeShellNut As String
        Dim strWKTotalDirt As String
        Dim strWSWholeNut As String
        Dim strWSBrokenNut As String
        Dim strWSBrokenKernel As String
        Dim strWSKernelLoss As String
        Dim strParam1 As String
        Dim objDataSet1 As New DataSet
        Dim strDrySepBK As String
        Dim strDrySepLShell As String
        Dim strDrySepWN As String
        Dim strDrySepBN As String
        Dim strDrySepTotalDirt As String
        Dim strDrySepFlowRate As String
        Dim strClayBathBK As String
        Dim strClayBathLShell As String
        Dim strClayBathWN As String
        Dim strClayBathBN As String
        Dim strClayBathTotalDirt As String
        Dim strClayBathFlowRate As String

        strProcessingLnNo = ddlProcessingLnNo.SelectedItem.Text
        strDrySepBK = txtDrySepBK.Text
        strDrySepLShell = txtDrySepLShell.Text
        strDrySepWN = txtDrySepWN.Text
        strDrySepBN = txtDrySepBN.Text
        strDrySepTotalDirt = txtDrySepTotalDirt.Text
        strDrySepFlowRate = txtDrySepFlowRate.Text
        strClayBathBK = txtClayBathBK.Text
        strClayBathLShell = txtClayBathLShell.Text
        strClayBathWN = txtClayBathWN.Text
        strClayBathBN = txtClayBathBN.Text
        strClayBathTotalDirt = txtClayBathTotalDirt.Text
        strClayBathFlowRate = txtClayBathFlowRate.Text

        If strEdit = "True" Then
            blnUpd = True
        Else
            blnUpd = False
            strParam1 = "||TransDate||" & _
                      strDate & "||" & strProcessingLnNo

            Try
                intErrNo = objPMTrx.mtdGetKernelQuality(strOppCd_KernelQuality_GET, strLocation, strParam1, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_KernelQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_KernelQuality_List.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDupKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDupKey = False
            End If
        End If

        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetKernelQualityStatus(objPMTrx.EnumKernelQualityStatus.Deleted), _
                        objPMTrx.EnumKernelQualityStatus.Deleted, _
                        objPMTrx.EnumKernelQualityStatus.Active)

        strParam = strDate & "|" & _
                    strProcessingLnNo & "||||||||||||||||||||" & _
                    strDrySepBK & "|" & _
                    strDrySepLShell & "|" & _
                    strDrySepWN & "|" & _
                    strDrySepBN & "|" & _
                    strDrySepTotalDirt & "|" & _
                    strDrySepFlowRate & "|" & _
                    strClayBathBK & "|" & _
                    strClayBathLShell & "|" & _
                    strClayBathWN & "|" & _
                    strClayBathBN & "|" & _
                    strClayBathTotalDirt & "|" & _
                    strClayBathFlowRate & "|" & _
                    strStatus

        Try
            intErrNo = objPMTrx.mtdUpdKernelQuality(strOppCd_KernelQuality_ADD, _
                                                    strOppCd_KernelQuality_UPD, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    blnUpd)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_KernelQuality_list.aspx")
        End Try

        Select Case blnUpd
            Case True
                Response.Redirect("PM_trx_KernelQuality_Det.aspx?LocCode=" & strLocation & "&TransDate=" & strTransDate & "&Edit=True")
            Case False
                Response.Redirect("PM_trx_KernelQuality_List.aspx")
        End Select

    End Sub


    Protected Function CheckDate() As String

        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        If Not txtdate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtdate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True

            End If
        End If

    End Function

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        UpdateData("Save")
    End Sub

    Sub btnDelete_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOppCd_DEL As String = "PM_CLSTRX_KERNELQUALITY_DEL"
        Dim strDate As String = CheckDate()
        Dim strParam As String

        strParam = strLocation & "|" & strDate & "|" & ddlProcessingLnNo.SelectedItem.Text.Trim
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.KernelQuality)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_KERNEL_QUALITY_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_KernelQuality_List.aspx")
        End Try

        Response.Redirect("PM_trx_KernelQuality_List.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_KernelQuality_List.aspx")
    End Sub

    Sub BindProcessingLineList()

        Dim strOpCode = "PM_CLSSETUP_PROCESSINGLINE_GET"
        Dim strParam = "ORDER BY PL.ProcessingLnNo| AND PL.LocCode='" & Session("SS_LOCATION") & "'"
        Dim intCnt As Integer

        objPMSetup.mtdGetProcessingLine(strOpCode, strParam, objProcessingLine)
        ddlProcessingLnNo.Items.Add(New ListItem("Please select Processing Line No.", ""))
        For intCnt = 0 To objProcessingLine.Tables(0).Rows.Count - 1
            objProcessingLine.Tables(0).Rows(intCnt).Item("ProcessingLnNo") = Trim(objProcessingLine.Tables(0).Rows(intCnt).Item("ProcessingLnNo"))
            ddlProcessingLnNo.Items.Add(New ListItem(objProcessingLine.Tables(0).Rows(intCnt).Item("ProcessingLnNo")))
        Next

    End Sub


End Class
