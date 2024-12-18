
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


Public Class PM_OilQuality_Det : Inherits Page

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
    Protected WithEvents txtOW As System.Web.UI.WebControls.TextBox
    Protected WithEvents revOW As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvOW As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtDW As System.Web.UI.WebControls.TextBox
    Protected WithEvents revDW As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvDW As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtOD As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDirt0 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMoist0 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDirt1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMoist1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDirt2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMoist2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDirt3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMoist3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMoist4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDirt4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents revOD As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revDirt0 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revMoist0 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revDirt1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revMoist1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revDirt2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revMoist2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revDirt3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revMoist3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revDirt4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents revMoist4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rvMoist4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvDirt4 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvMoist3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvDirt3 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvMoist2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvDirt2 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvMoist1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvDirt1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvMoist0 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvDirt0 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents rvOD As System.Web.UI.WebControls.RangeValidator

    Protected WithEvents txtCPOTankFFA1 As TextBox
    Protected WithEvents txtCPOTankMoist1 As TextBox
    Protected WithEvents txtCPOTankDirt1 As TextBox
    Protected WithEvents txtCPOTankFFA2 As TextBox
    Protected WithEvents txtCPOTankMoist2 As TextBox
    Protected WithEvents txtCPOTankDirt2 As TextBox
    Protected WithEvents txtCPOTankFFA3 As TextBox
    Protected WithEvents txtCPOTankMoist3 As TextBox
    Protected WithEvents txtCPOTankDirt3 As TextBox
    Protected WithEvents txtCPOTankFFA4 As TextBox
    Protected WithEvents txtCPOTankMoist4 As TextBox
    Protected WithEvents txtCPOTankDirt4 As TextBox
    Protected WithEvents txtCPOTankFFA5 As TextBox
    Protected WithEvents txtCPOTankMoist5 As TextBox
    Protected WithEvents txtCPOTankDirt5 As TextBox
    Protected WithEvents txtCPOTankFFA6 As TextBox
    Protected WithEvents txtCPOTankMoist6 As TextBox
    Protected WithEvents txtCPOTankDirt6 As TextBox

    Dim strOppCd_OilQuality_GET As String = "PM_CLSTRX_OIL_QUALITY_GET"
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Private Sub InitializeComponent()

    End Sub

    Public Sub Page_Load(ByVal Sender As System.Object, ByVal E As System.EventArgs) Handles MyBase.Load
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMOilQuality), intPMAR) = False Then
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_OilQuality_list.aspx")
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
            intErrNo = objPMTrx.mtdGetOilQuality(strOppCd_OilQuality_GET, strLocation, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OilQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim strView As Boolean

        If pv_strstatus = objPMTrx.EnumOilQualityStatus.Deleted Then
            strView = False

        ElseIf pv_strstatus = objPMTrx.EnumOilQualityStatus.Active Then
            strView = True
        End If

        txtdate.Enabled = strView
        Save.Visible = strView

    End Sub

    Sub DisplayData()

        Dim dsOilQuality As DataSet = LoadData()

        If dsOilQuality.Tables(0).Rows.Count > 0 Then
            txtdate.Text = objGlobal.GetShortDate(strDateFormat, Trim(dsOilQuality.Tables(0).Rows(0).Item("TransDate")))

            With dsOilQuality.Tables(0).Rows(0)
                txtOW.Text = .Item("OW")
                txtDW.Text = .Item("DW")
                txtOD.Text = .Item("OD")
                txtDirt0.Text = .Item("PureOilDirt")
                txtMoist0.Text = .Item("PureOilMoist")
                txtDirt1.Text = .Item("PurifierDirt1")
                txtMoist1.Text = .Item("PurifierMoist1")
                txtDirt2.Text = .Item("PurifierDirt2")
                txtMoist2.Text = .Item("PurifierMoist2")
                txtDirt3.Text = .Item("PurifierDirt3")
                txtMoist3.Text = .Item("PurifierMoist3")
                txtDirt4.Text = .Item("PurifierDirt4")
                txtMoist4.Text = .Item("PurifierMoist4")
                txtCPOTankFFA1.Text = .Item("CPOTankFFA1")
                txtCPOTankMoist1.Text = .Item("CPOTankMoist1")
                txtCPOTankDirt1.Text = .Item("CPOTankDirt1")
                txtCPOTankFFA2.Text = .Item("CPOTankFFA2")
                txtCPOTankMoist2.Text = .Item("CPOTankMoist2")
                txtCPOTankDirt2.Text = .Item("CPOTankDirt2")
                txtCPOTankFFA3.Text = .Item("CPOTankFFA3")
                txtCPOTankMoist3.Text = .Item("CPOTankMoist3")
                txtCPOTankDirt3.Text = .Item("CPOTankDirt3")
                txtCPOTankFFA4.Text = .Item("CPOTankFFA4")
                txtCPOTankMoist4.Text = .Item("CPOTankMoist4")
                txtCPOTankDirt4.Text = .Item("CPOTankDirt4")
                txtCPOTankFFA5.Text = .Item("CPOTankFFA5")
                txtCPOTankMoist5.Text = .Item("CPOTankMoist5")
                txtCPOTankDirt5.Text = .Item("CPOTankDirt5")
                txtCPOTankFFA6.Text = .Item("CPOTankFFA6")
                txtCPOTankMoist6.Text = .Item("CPOTankMoist6")
                txtCPOTankDirt6.Text = .Item("CPOTankDirt6")
            End With

            lblPeriod.Text = Trim(dsOilQuality.Tables(0).Rows(0).Item("AccMonth")) & "/" & Trim(dsOilQuality.Tables(0).Rows(0).Item("AccYear"))
            lblStatus.Text = objPMTrx.mtdGetOilQualityStatus(Trim(dsOilQuality.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsOilQuality.Tables(0).Rows(0).Item("CreateDate")))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsOilQuality.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsOilQuality.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsOilQuality.Tables(0).Rows(0).Item("Status")))
            Delete.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Delete.Visible = True
        End If
    End Sub


    Sub UpdateData(ByVal strAction As String)
        Dim strOppCd_OilQuality_ADD As String = "PM_CLSTRX_OIL_QUALITY_ADD"
        Dim strOppCd_OilQuality_UPD As String = "PM_CLSTRX_OIL_QUALITY_UPD"
        Dim blnDupKey As Boolean = False
        Dim blnUpd As Boolean = False
        Dim strStatus As String
        Dim strDate As String = CheckDate()
        Dim strOW As String
        Dim strDW As String
        Dim strOD As String
        Dim strPureOilDirt As String
        Dim strPureOilMoist As String
        Dim strPurifierDirt1 As String
        Dim strPurifierMoist1 As String
        Dim strPurifierDirt2 As String
        Dim strPurifierMoist2 As String
        Dim strPurifierDirt3 As String
        Dim strPurifierMoist3 As String
        Dim strPurifierDirt4 As String
        Dim strPurifierMoist4 As String    
        Dim strParam1 As String
        Dim objDataSet1 As New DataSet

        strOW = txtOW.Text
        strDW = txtDW.Text
        strOD = txtOD.Text
        strPureOilDirt = txtDirt0.Text
        strPureOilMoist = txtMoist0.Text
        strPurifierDirt1 = txtDirt1.Text
        strPurifierMoist1 = txtMoist1.Text
        strPurifierDirt2 = txtDirt2.Text
        strPurifierMoist2 = txtMoist2.Text
        strPurifierDirt3 = txtDirt3.Text
        strPurifierMoist3 = txtMoist3.Text
        strPurifierDirt4 = txtDirt4.Text
        strPurifierMoist4 = txtMoist4.Text

        If strEdit = "True" Then
            blnUpd = True
        Else
            blnUpd = False


            strParam1 = "||TransDate||" & _
                      strDate & "|||"

            Try
                intErrNo = objPMTrx.mtdGetOilQuality(strOppCd_OilQuality_GET, strLocation, strParam1, objDataSet1)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OilQuality_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/PM_trx_OilQuality_List.aspx")
            End Try

            If objDataSet1.Tables(0).Rows.Count > 0 Then
                blnDupKey = True
                lblDupMsg.Visible = True
                Exit Sub
            Else
                blnDupKey = False
            End If
        End If

        strStatus = IIf(lblStatus.Text = objPMTrx.mtdGetOilQualityStatus(objPMTrx.EnumOilQualityStatus.Deleted), _
                        objPMTrx.EnumOilQualityStatus.Deleted, _
                        objPMTrx.EnumOilQualityStatus.Active)

        strParam = strDate & "||||" & _
                    strOW & "|" & _
                    strDW & "|" & _
                    strOD & "|" & _
                    strPureOilDirt & "|" & _
                    strPureOilMoist & "|" & _
                    strPurifierDirt1 & "|" & _
                    strPurifierMoist1 & "|" & _
                    strPurifierDirt2 & "|" & _
                    strPurifierMoist2 & "|" & _
                    strPurifierDirt3 & "|" & _
                    strPurifierMoist3 & "|" & _
                    strPurifierDirt4 & "|" & _
                    strPurifierMoist4 & "|" & _
                    strStatus & "|" & _
                    iif(txtCPOTankFFA1.Text = "", 0, txtCPOTankFFA1.Text) & "|" & _
                    iif(txtCPOTankMoist1.Text = "", 0, txtCPOTankMoist1.Text) & "|" & _
                    iif(txtCPOTankDirt1.Text = "", 0, txtCPOTankDirt1.Text) & "|" & _
                    iif(txtCPOTankFFA2.Text = "", 0, txtCPOTankFFA2.Text) & "|" & _
                    iif(txtCPOTankMoist2.Text = "", 0, txtCPOTankMoist2.Text) & "|" & _
                    iif(txtCPOTankDirt2.Text = "", 0, txtCPOTankDirt2.Text) & "|" & _
                    iif(txtCPOTankFFA3.Text = "", 0, txtCPOTankFFA3.Text) & "|" & _
                    iif(txtCPOTankMoist3.Text = "", 0, txtCPOTankMoist3.Text) & "|" & _
                    iif(txtCPOTankDirt3.Text = "", 0, txtCPOTankDirt3.Text) & "|" & _
                    iif(txtCPOTankFFA4.Text = "", 0, txtCPOTankFFA4.Text) & "|" & _
                    iif(txtCPOTankMoist4.Text = "", 0, txtCPOTankMoist4.Text) & "|" & _
                    iif(txtCPOTankDirt4.Text = "", 0, txtCPOTankDirt4.Text) & "|" & _
                    iif(txtCPOTankFFA5.Text = "", 0, txtCPOTankFFA5.Text) & "|" & _
                    iif(txtCPOTankMoist5.Text = "", 0, txtCPOTankMoist5.Text) & "|" & _
                    iif(txtCPOTankDirt5.Text = "", 0, txtCPOTankDirt5.Text) & "|" & _
                    iif(txtCPOTankFFA6.Text = "", 0, txtCPOTankFFA6.Text) & "|" & _
                    iif(txtCPOTankMoist6.Text = "", 0, txtCPOTankMoist6.Text) & "|" & _
                    iif(txtCPOTankDirt6.Text = "", 0, txtCPOTankDirt6.Text)


        Try
            intErrNo = objPMTrx.mtdUpdOilQuality(strOppCd_OilQuality_ADD, _
                                              strOppCd_OilQuality_UPD, _
                                              strLocation, _
                                              strUserId, _
                                              strAccMonth, _
                                              strAccYear, _
                                              strParam, _
                                              blnUpd)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OIL_LOSS_UPDATE&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_OilQuality_list.aspx")
        End Try

        Select Case blnUpd
            Case True
                Response.Redirect("PM_trx_OilQuality_Det.aspx?LocCode=" & strLocation & "&TransDate=" & strTransDate & "&Edit=True")
            Case False
                Response.Redirect("PM_trx_OilQuality_List.aspx")
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
        Dim strOppCd_DEL As String = "PM_CLSTRX_OILQUALITY_DEL"
        Dim strDate As String = CheckDate()
        Dim strParam As String

        strParam = strLocation & "|" & strDate
        Try
            intErrNo = objPMTrx.mtdPhysicalDelete(strOppCd_DEL, _
                                                  strParam, _
                                                  objPMTrx.EnumTransType.OilQuality)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_OILQUALITY_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Trx/PM_trx_OilQuality_List.aspx")
        End Try

        Response.Redirect("PM_trx_OilQuality_List.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_OilQuality_List.aspx")
    End Sub

End Class
