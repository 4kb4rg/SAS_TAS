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

Public Class PM_ProducedKernelQuality_Det : Inherits Page

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

    Protected WithEvents txtdate As TextBox

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
    Protected WithEvents lblTracker As System.Web.UI.WebControls.Label
    Protected WithEvents rfvDate As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Back As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtSiloBK1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloBK1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtSiloMoist1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloMoist1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloMoist1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloLShell1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloLShell1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloLShell1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloWN1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloWN1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloWN1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloBN1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloBN1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloBN1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloTotalDirt1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloTotalDirt As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloTotalDirt1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvSiloBK1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloBK2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloBK2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloBK2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtProdBK As System.Web.UI.WebControls.TextBox
    Protected WithEvents revProdBK As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvProdBK As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloMoist2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloMoist2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloMoist2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtProdMoist As System.Web.UI.WebControls.TextBox
    Protected WithEvents revProdMoist As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvProdMoist As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloLShell2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloLShell2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloLShell2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtProdLShell As System.Web.UI.WebControls.TextBox
    Protected WithEvents revProdLShell As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvProdLShell As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloWN2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloWN2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloWN2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtProdWN As System.Web.UI.WebControls.TextBox
    Protected WithEvents revProdWN As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvProdWN As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloBN2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloBN2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloBN2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtProdBN As System.Web.UI.WebControls.TextBox
    Protected WithEvents revProdBN As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvProdBN As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloTotalDirt2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloTotalDirt2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloTotalDirt2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtProdTotalDirt As System.Web.UI.WebControls.TextBox
    Protected WithEvents revProdTotalDirt As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvProdTotalDirt As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloBK3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloBK3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloBK3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloMoist3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloMoist3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloMoist3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloLShell3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloLShell3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloLShell3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloWN3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloWN3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloWN3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloBN3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloBN3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloBN3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtSiloTotalDirt3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revSiloTotalDirt3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvSiloTotalDirt3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtProdFFA As System.Web.UI.WebControls.TextBox
    Protected WithEvents revProdFFA As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvProdFFA As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtProdOD As System.Web.UI.WebControls.TextBox
    Protected WithEvents revProdOD As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvProdOD As System.Web.UI.WebControls.RangeValidator
    Dim strOppCd_ProducedKernelQuality_GET As String = "PM_CLSTRX_PRODUCED_KERNEL_QUALITY_GET"
	
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


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMProducedKernel), intPMAR) = False Then
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_ProducedKernelQuality_list.aspx")
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
            intErrNo = objPMTrx.mtdGetProduceKernelQuality(strOppCd_ProducedKernelQuality_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_ProducedKernelQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_ProducedKernelQuality_List.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumProducedKernelQualityStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumProducedKernelQualityStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()

        Dim dsProducedKernelQuality As DataSet = LoadData()

        If dsProducedKernelQuality.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("TransDate")))
            txtSiloBK1.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloBK1"))
            txtSiloMoist1.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloMoist1"))
            txtSiloLShell1.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloLShell1"))
            txtSiloWN1.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloWN1"))
            txtSiloBN1.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloBN1"))
            txtSiloTotalDirt1.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloTotalDirt1"))
            txtSiloBK2.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloBK2"))
            txtSiloMoist2.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloMoist2"))
            txtSiloLShell2.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloLShell2"))
            txtSiloWN2.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloWN2"))
            txtSiloBN2.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloBN2"))
            txtSiloTotalDirt2.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloTotalDirt2"))
            txtSiloBK3.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloBK3"))
            txtSiloMoist3.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloMoist3"))
            txtSiloLShell3.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloLShell3"))
            txtSiloWN3.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloWN3"))
            txtSiloBN3.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloBN3"))
            txtSiloTotalDirt3.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("SiloTotalDirt3"))
            txtProdBK.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("ProdBK"))
            txtProdMoist.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("ProdMoist"))
            txtProdLShell.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("ProdLShell"))
            txtProdWN.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("ProdWN"))
            txtProdBN.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("ProdBN"))
            txtProdTotalDirt.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("ProdTotalDirt"))
            txtProdFFA.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("ProdFFA"))
            txtProdOD.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("ProdOD"))

            lblPeriod.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objPMTrx.mtdGetProducedKernelQualityStatus(Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsProducedKernelQuality.Tables(0).Rows(0).Item("Status")))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.Visible = True
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_ProducedKernelQuality_ADD As String = "PM_CLSTRX_PRODUCED_KERNEL_QUALITY_ADD"
        Dim strOppCd_ProducedKernelQuality_UPD As String = "PM_CLSTRX_PRODUCED_KERNEL_QUALITY_UPD"
        Dim blnDupKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strUncrackedNut As String
        Dim strHalfCrackedNut As String
        Dim strFreeShell As String
        Dim strStone As String
        Dim strTotalDirt As String
        Dim strMoist As String
        Dim strOilContent As String
        Dim strFFA As String
        Dim strParam1 As String
        Dim objDataSet1 As New DataSet

        Dim strSiloBK1 As String
        Dim strSiloMoist1 As String
        Dim strSiloLShell1 As String
        Dim strSiloWN1 As String
        Dim strSiloBN1 As String
        Dim strSiloTotalDirt1 As String
        Dim strSiloBK2 As String
        Dim strSiloMoist2 As String
        Dim strSiloLShell2 As String
        Dim strSiloWN2 As String
        Dim strSiloBN2 As String
        Dim strSiloTotalDirt2 As String
        Dim strSiloBK3 As String
        Dim strSiloMoist3 As String
        Dim strSiloLShell3 As String
        Dim strSiloWN3 As String
        Dim strSiloBN3 As String
        Dim strSiloTotalDirt3 As String
        Dim strProdBK As String
        Dim strProdMoist As String
        Dim strProdLShell As String
        Dim strProdWN As String
        Dim strProdBN As String
        Dim strProdTotalDirt As String
        Dim strProdOD As String
        Dim strProdFFA As String

        strSiloBK1 = txtSiloBK1.Text
        strSiloMoist1 = txtSiloMoist1.Text
        strSiloLShell1 = txtSiloLShell1.Text
        strSiloWN1 = txtSiloWN1.Text
        strSiloBN1 = txtSiloBN1.Text
        strSiloTotalDirt1 = txtSiloTotalDirt1.Text
        strSiloBK2 = txtSiloBK2.Text
        strSiloMoist2 = txtSiloMoist2.Text
        strSiloLShell2 = txtSiloLShell2.Text
        strSiloWN2 = txtSiloWN2.Text
        strSiloBN2 = txtSiloBN2.Text
        strSiloTotalDirt2 = txtSiloTotalDirt2.Text
        strSiloBK3 = txtSiloBK3.Text
        strSiloMoist3 = txtSiloMoist3.Text
        strSiloLShell3 = txtSiloLShell3.Text
        strSiloWN3 = txtSiloWN3.Text
        strSiloBN3 = txtSiloBN3.Text
        strSiloTotalDirt3 = txtSiloTotalDirt3.Text
        strProdBK = txtProdBK.Text
        strProdMoist = txtProdMoist.Text
        strProdLShell = txtProdLShell.Text
        strProdWN = txtProdWN.Text
        strProdBN = txtProdBN.Text
        strProdTotalDirt = txtProdTotalDirt.Text
        strProdOD = txtProdOD.Text
        strProdFFA = txtProdFFA.Text

        If strEdit = "True" Then
            blnUpd = True
        Else
            blnUpd = False


            strParam1 = "||TransDate||" & _
                      strDate & "|||"

            Try
                intErrNo = objPMTrx.mtdGetProduceKernelQuality(strOppCd_ProducedKernelQuality_GET, strLocation, strParam1, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_ProducedKernelQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_ProducedKernelQuality_List.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDupKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDupKey = False
            End If
        End If

        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetProducedKernelQualityStatus(objPMTrx.EnumProducedKernelQualityStatus.Deleted), _
                        objPMTrx.EnumProducedKernelQualityStatus.Deleted, _
                        objPMTrx.EnumProducedKernelQualityStatus.Active)

        strParam = strDate & "|||||||||" & _
                    strSiloBK1 & "|" & _
                    strSiloMoist1 & "|" & _
                    strSiloLShell1 & "|" & _
                    strSiloWN1 & "|" & _
                    strSiloBN1 & "|" & _
                    strSiloTotalDirt1 & "|" & _
                    strSiloBK2 & "|" & _
                    strSiloMoist2 & "|" & _
                    strSiloLShell2 & "|" & _
                    strSiloWN2 & "|" & _
                    strSiloBN2 & "|" & _
                    strSiloTotalDirt2 & "|" & _
                    strSiloBK3 & "|" & _
                    strSiloMoist3 & "|" & _
                    strSiloLShell3 & "|" & _
                    strSiloWN3 & "|" & _
                    strSiloBN3 & "|" & _
                    strSiloTotalDirt3 & "|" & _
                    strProdBK & "|" & _
                    strProdMoist & "|" & _
                    strProdLShell & "|" & _
                    strProdWN & "|" & _
                    strProdBN & "|" & _
                    strProdTotalDirt & "|" & _
                    strProdOD & "|" & _
                    strProdFFA & "|" & _
                    strStatus
        Try
            intErrNo = objPMTrx.mtdUpdProducedKernelQuality(strOppCd_ProducedKernelQuality_ADD, _
                                              strOppCd_ProducedKernelQuality_UPD, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strParam, _
                                              blnUpd)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_ProducedKernelQuality_list.aspx")
        End Try

        Select Case blnUpd
            Case True
                Response.Redirect("PM_trx_ProducedKernelQuality_Det.aspx?LocCode=" & strLocation & "&TransDate=" & strTransDate & "&Edit=True")
            Case False
                Response.Redirect("PM_trx_ProducedKernelQuality_List.aspx")
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
        Dim strOppCd_DEL As String = "PM_CLSTRX_PRODUCEKERNEL_DEL"
        Dim strDate As String = CheckDate()
        Dim strParam As String

        strParam = strLocation & "|" & strDate
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.ProducedKernelQuality)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_PRODUCED_KERNEL_QUALITY_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_ProducedKernelQuality_List.aspx")
        End Try

        Response.Redirect("PM_trx_ProducedKernelQuality_List.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_ProducedKernelQuality_List.aspx")
    End Sub

End Class
