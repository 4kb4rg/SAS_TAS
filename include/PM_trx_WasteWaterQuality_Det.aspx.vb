
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

Public Class PM_WasteWaterQuality_Det : Inherits Page

    Dim objPMTrx As New agri.PM.clsTrx()
    Dim objPMSetup As New agri.PM.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblOER As Label
    Protected WithEvents lblKER As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label

    Protected WithEvents txtdate As TextBox

    Protected WithEvents Save As ImageButton
    Protected WithEvents Delete As ImageButton
    Protected WithEvents btnSelDateFrom As Image

    Dim strTransDate As String
    Dim strTestSampleCode As String
    Dim strPondNo As String
    Dim strEdit As String

    Dim strParam As String
    Dim objDataSet As New DataSet
    Dim objTestSampleCode As New DataSet
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
    Protected WithEvents Back As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlTestSampleCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvTestSampleCode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlPondNo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvPondNo As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblCreateDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate As System.Web.UI.WebControls.Label
    Protected WithEvents lblUpdateBy As System.Web.UI.WebControls.Label
    Protected WithEvents txtPH As System.Web.UI.WebControls.TextBox
    Protected WithEvents revPH As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvPH As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtTA As System.Web.UI.WebControls.TextBox
    Protected WithEvents revTA As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvTA As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtVFA As System.Web.UI.WebControls.TextBox
    Protected WithEvents revVFA As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvVFA As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtVFATA As System.Web.UI.WebControls.TextBox
    Protected WithEvents revVFATA As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvVFATA As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents lblDupMsg As System.Web.UI.WebControls.Label
    Dim strOppCd_WasteWQ_GET As String = "PM_CLSTRX_WASTE_WATER_QUALITY_GET"
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Private Sub InitializeComponent()

    End Sub

    Public Sub Page_Load(ByVal Sender As System.Object, ByVal E As System.EventArgs)
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
        strTestSampleCode = Request.QueryString("TestSample")
        strPondNo = Request.QueryString("PondNo")
        strEdit = Request.QueryString("Edit")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                BindTestSampleCode()
                BindPondNo()

                If strEdit = "True" Then
                    DisplayData()
                    blnUpdate.Text = False
                    txtdate.Enabled = False
                    ddlTestSampleCode.Enabled = False
                    ddlPondNo.Enabled = False
                    btnSelDateFrom.Visible = False
                Else
                    blnUpdate.Text = True
                    txtdate.Enabled = True
                    ddlTestSampleCode.Enabled = True
                    ddlPondNo.Enabled = True
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_WasteWaterQuality_list.aspx")
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
                   strTransDate & "|" & _
                   strTestSampleCode & "|" & _
                   strPondNo & "|"

        Try
            intErrNo = objPMTrx.mtdGetWastedWaterQuality(strOppCd_WasteWQ_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_WasteWaterQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_WasteWaterQuality_List.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumWastedWaterQualityStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumWastedWaterQualityStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()
        Dim dsWWQuality As DataSet = LoadData()
        Dim I As Int16

        If dsWWQuality.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsWWQuality.Tables(0).Rows(0).Item("TransDate")))

            For I = 0 To ddlTestSampleCode.Items.Count - 1
                If LCase(Trim(ddlTestSampleCode.Items(I).Value)) = LCase(Trim(dsWWQuality.Tables(0).Rows(0).Item("TSCode"))) Then
                    ddlTestSampleCode.SelectedIndex = I
                    Exit For
                End If
            Next
            For I = 0 To ddlPondNo.Items.Count - 1
                If Not IsDBNull(dsWWQuality.Tables(0).Rows(0).Item("PondNo")) AndAlso _
                        LCase(Trim(ddlPondNo.Items(I).Value)) = LCase(Trim(dsWWQuality.Tables(0).Rows(0).Item("PondNo"))) Then
                    ddlPondNo.SelectedIndex = I
                    Exit For
                End If
            Next

            With dsWWQuality.Tables(0).Rows(0)
                txtPH.Text = .Item("PH")
                txtTA.Text = .Item("TA")
                txtVFA.Text = .Item("VFA")
                txtVFATA.Text = .Item("VFATA")
            End With

            lblPeriod.Text = Trim(dsWWQuality.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsWWQuality.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objPMTrx.mtdGetOilQualityStatus(Trim(dsWWQuality.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsWWQuality.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsWWQuality.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsWWQuality.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsWWQuality.Tables(0).Rows(0).Item("Status")))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.Visible = True
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_WastedWQ_ADD As String = "PM_CLSTRX_WASTE_WATER_QUALITY_ADD"
        Dim strOppCd_WastedWQ_UPD As String = "PM_CLSTRX_WASTE_WATER_QUALITY_UPD"
        Dim blnDupKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strParam1 As String
        Dim objDataSet1 As New DataSet

        Dim strPH As String
        Dim strTA As String
        Dim strVFA As String
        Dim strVFATA As String

        strTestSampleCode = ddlTestSampleCode.SelectedItem.Value
        strPondNo = ddlPondNo.SelectedValue
        strPH = txtPH.Text
        strTA = txtTA.Text
        strVFA = txtVFA.Text
        strVFATA = txtVFATA.Text

        If strEdit = "True" Then
            blnUpd = True
        Else
            blnUpd = False

            strParam1 = "||TransDate||" & _
                      strDate & "|" & _
                      strTestSampleCode & "|" & _
                      strPondNo & "|"

            Try
                intErrNo = objPMTrx.mtdGetWastedWaterQuality(strOppCd_WasteWQ_GET, strLocation, strParam1, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_WasteWaterQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_WasteWaterQuality_List.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDupKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDupKey = False
            End If
        End If

        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetWastedWaterQualityStatus(objPMTrx.EnumWastedWaterQualityStatus.Deleted), _
                        objPMTrx.EnumWastedWaterQualityStatus.Deleted, _
                        objPMTrx.EnumWastedWaterQualityStatus.Active)

        strParam = strDate & "|" & _
                   strTestSampleCode & "|" & _
                   strStatus & "|" & _
                   strPH & "|" & _
                   strTA & "|" & _
                   strVFA & "|" & _
                   strVFATA & "|0.00|" & _
                   strPondNo

        Try
            intErrNo = objPMTrx.mtdUpdWastedWaterQuality(strOppCd_WastedWQ_ADD, _
                                              strOppCd_WastedWQ_UPD, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strParam, _
                                              blnUpd)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_WASTED_WASTE_QUALITY_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_WasteWaterQuality_list.aspx")
        End Try

        Select Case blnUpd
            Case True
                Response.Redirect("PM_trx_WasteWaterQuality_Det.aspx?LocCode=" & strLocation & "&TransDate=" & strTransDate & "&TestSample=" & strTestSampleCode & "&PondNo=" & strPondNo & "&Edit=True")
            Case False
                Response.Redirect("PM_trx_WasteWaterQuality_List.aspx")
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
        Dim strOppCd_DEL As String = "PM_CLSTRX_WASTEWATERQUALITY_DEL"
        Dim strDate As String = CheckDate()
        Dim strParam As String

        strParam = strLocation & "|" & strDate & "|" & ddlTestSampleCode.SelectedItem.Value.Trim() & "|" & ddlPondNo.SelectedValue
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.WastedWaterQuality)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_WASTEWATERQUALITY_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_WasteWaterQuality_List.aspx")
        End Try

        Response.Redirect("PM_trx_WasteWaterQuality_List.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_WasteWaterQuality_List.aspx")
    End Sub

    Sub BindTestSampleCode()
        Dim strOpCode = "PM_CLSSETUP_TESTSAMPLE_GET"
        Dim strParam = "ORDER BY M.TestSampleCode| AND M.LocCode='" & Session("SS_LOCATION") & "'"
        Dim intCnt As Integer

        objPMSetup.mtdGetTestSample(strOpCode, strParam, objTestSampleCode)
        ddlTestSampleCode.Items.Add(New ListItem("Please select Test Sample Code", ""))

        For intCnt = 0 To objTestSampleCode.Tables(0).Rows.Count - 1
            objTestSampleCode.Tables(0).Rows(intCnt).Item("TestSampleCode") = Trim(objTestSampleCode.Tables(0).Rows(intCnt).Item("TestSampleCode"))
            ddlTestSampleCode.Items.Add(New ListItem(objTestSampleCode.Tables(0).Rows(intCnt).Item("TestSampleCode") & " (" & objTestSampleCode.Tables(0).Rows(intCnt).Item("Description") & ")", _
                                        objTestSampleCode.Tables(0).Rows(intCnt).Item("TestSampleCode")))
        Next

    End Sub

    Private Sub BindPondNo()
        Dim enumPond As agri.PM.clsTrx.EnumPondNumber
        Dim item() As ListItem = New ListItem(16) {}

        item(0) = New ListItem("Please select a Pond No.", "")
        item(1) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo1), enumPond.LNo1)
        item(2) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo2), enumPond.LNo2)
        item(3) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo3), enumPond.LNo3)
        item(4) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo4), enumPond.LNo4)
        item(5) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo5), enumPond.LNo5)
        item(6) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo6), enumPond.LNo6)
        item(7) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo7), enumPond.LNo7)
        item(8) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo8), enumPond.LNo8)
        item(9) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo9), enumPond.LNo9)
        item(10) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo10), enumPond.LNo10)
        item(11) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo11), enumPond.LNo11)
        item(12) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo12), enumPond.LNo12)
        item(13) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo13), enumPond.LNo13)
        item(14) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo14), enumPond.LNo14)
        item(15) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo15), enumPond.LNo15)
        item(16) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo16), enumPond.LNo16)

        ddlPondNo.Items.AddRange(item)
    End Sub

End Class
