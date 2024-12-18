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

Public Class PM_DispatchedKernelQuality_Det : Inherits Page

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
    Protected WithEvents txtUncrackedNut As TextBox
    Protected WithEvents txtBrokenKernel As TextBox
    Protected WithEvents txtHalfCrackedNut As TextBox
    Protected WithEvents txtFreeShell As TextBox
    Protected WithEvents txtStone As TextBox
    Protected WithEvents txtMoist As TextBox
    Protected WithEvents txtOilContent As TextBox
    Protected WithEvents txtFFA As TextBox

    Protected WithEvents Save As ImageButton
    Protected WithEvents Delete As ImageButton
    Protected WithEvents btnSelDateFrom As Image
    Dim strTransDate As String
    Dim strEdit As String

    Dim strParam As String
    Dim objDataSet As New DataSet()
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
    Dim strOppCd_DispatchedKernelQuality_GET As String = "PM_CLSTRX_Dispatched_KERNEL_QUALITY_GET"
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
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


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMDispatchedKernel), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                If Not (Request.QueryString("LocCode") = "" And Request.QueryString("TransDate") = "") Then
                    strTransDate = Request.QueryString("TransDate")
                    strEdit = Request.QueryString("Edit")
                End If

                If strEdit = "True" Then
                    DisplayData()
                    blnUpdate.Text = False
                    txtdate.Enabled = False
                    btnSelDateFrom.Visible = False
                Else
                    blnUpdate.Text = True
                    txtdate.Enabled = True
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_DispatchedKernelQuality_list.aspx")
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
                   strTransDate & "|||"


        Try
            intErrNo = objPMTrx.mtdGetDispatchedKernelQuality(strOppCd_DispatchedKernelQuality_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_DispatchedKernelQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_DispatchedKernelQuality_List.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumDispatchedKernelQualityStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumDispatchedKernelQualityStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()

        Dim dsDispatchedKernelQuality As DataSet = LoadData()

        If dsDispatchedKernelQuality.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("TransDate")))
            txtUncrackedNut.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKUNCRACKEDNUT"))
            txtBrokenKernel.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKBROKENKERNEL"))
            txtHalfCrackedNut.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKHALFCRACKEDNUT"))
            txtFreeShell.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKFREESHELL"))
            txtStone.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKSTONE"))
            lblTotalDirt.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKTOTALDIRT"))
            txtMoist.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKMOIST"))
            txtOilContent.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKOILCONTENT"))
            txtFFA.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("DKFFA"))

            lblPeriod.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objPMTrx.mtdGetDispatchedKernelQualityStatus(Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsDispatchedKernelQuality.Tables(0).Rows(0).Item("Status")))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.visible = True
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_DispatchedKernelQuality_ADD As String = "PM_CLSTRX_Dispatched_KERNEL_QUALITY_ADD"
        Dim strOppCd_DispatchedKernelQuality_UPD As String = "PM_CLSTRX_Dispatched_KERNEL_QUALITY_UPD"
        Dim blnDuDKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strUncrackedNut As String
        Dim strBrokenKernel As String
        Dim strHalfCrackedNut As String
        Dim strFreeShell As String
        Dim strStone As String
        Dim strTotalDirt As String
        Dim strMoist As String
        Dim strOilContent As String
        Dim strFFA As String
        Dim strParam1 As String
        Dim objDataSet1 As New DataSet()

        strUncrackedNut = txtUncrackedNut.Text
        strBrokenKernel = txtBrokenKernel.Text
        strHalfCrackedNut = txtHalfCrackedNut.Text
        strFreeShell = txtFreeShell.Text
        strStone = txtStone.Text
        strTotalDirt = val(lblTotalDirt.Text)
        strMoist = txtMoist.Text
        strOilContent = txtOilContent.Text
        strFFA = txtFFA.Text


        If strEdit = "True" Then
            blnUpd = True
        Else
            blnUpd = False


            strParam1 = "||TransDate||" & _
                      strDate & "|||"

            Try
                intErrNo = objPMTrx.mtdGetDispatchedKernelQuality(strOppCd_DispatchedKernelQuality_GET, strLocation, strParam1, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_DispatchedKernelQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_DispatchedKernelQuality_List.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDuDKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDuDKey = False
            End If
        End If

        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetDispatchedKernelQualityStatus(objPMTrx.EnumDispatchedKernelQualityStatus.Deleted), _
                        objPMTrx.EnumDispatchedKernelQualityStatus.Deleted, _
                        objPMTrx.EnumDispatchedKernelQualityStatus.Active)

        strParam = strDate & "|" & _
                   strUncrackedNut & "|" & _
                   strBrokenKernel & "|" & _
                   strHalfCrackedNut & "|" & _
                   strFreeShell & "|" & _
                   strStone & "|" & _
                   strTotalDirt & "|" & _
                   strMoist & "|" & _
                   strOilContent & "|" & _
                   strFFA & "|" & _
                   strStatus

        Try
            intErrNo = objPMTrx.mtdUpdDispatchedKernelQuality(strOppCd_DispatchedKernelQuality_ADD, _
                                              strOppCd_DispatchedKernelQuality_UPD, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strParam, _
                                              blnUpd)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_DispatchedKernelQuality_list.aspx")
        End Try

        Select Case blnUpd
            Case True
                Response.Redirect("PM_trx_DispatchedKernelQuality_Det.aspx?LocCode=" & strLocation & "&TransDate=" & strTransDate & "&Edit=True")
            Case False
                Response.Redirect("PM_trx_DispatchedKernelQuality_List.aspx")
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
        Dim strOppCd_DEL As String = "PM_CLSTRX_DISPATCHKERNEL_DEL"
        Dim strDate As String = CheckDate()
        Dim strParam As String

        strParam = strLocation & "|" & strDate
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.DispatchedKernelQuality)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_DISPATCHED_KERNEL_QUALITY_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_DispatchedKernelQuality_List.aspx")
        End Try
        
        Response.Redirect("PM_trx_DispatchedKernelQuality_List.aspx")
    End Sub    

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_DispatchedKernelQuality_List.aspx")
    End Sub



End Class
