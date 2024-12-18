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

Public Class PM_Setup_AcceptableOilQuality : Inherits Page
    Dim dsItem As DataSet

    Dim objPMSetup As New agri.PM.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblCreateDate As Label
    Protected WithEvents txtCreateDate As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdateBy As Label
    Protected WithEvents txtOilTankMoist As TextBox
    Protected WithEvents txtOilTankDirt As TextBox
    Protected WithEvents txtPurifierMoist1 As TextBox
    Protected WithEvents txtPurifierDirt1 As TextBox
    Protected WithEvents txtPurifierMoist2 As TextBox
    Protected WithEvents txtPurifierDirt2 As TextBox
    Protected WithEvents txtPurifierMoist3 As TextBox
    Protected WithEvents txtPurifierDirt3 As TextBox
    Protected WithEvents txtPurifierMoist4 As TextBox
    Protected WithEvents txtPurifierDirt4 As TextBox
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
    Protected WithEvents txtProductOilFFA As TextBox
    Protected WithEvents txtProductOilMoist As TextBox
    Protected WithEvents txtProductOilDirt As TextBox

    Protected WithEvents Save As ImageButton
    
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

    Dim strOppCd_GET As String = "PM_CLSSETUP_ACCEPTABLEOILQUALITY_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_ACCEPTABLEOILQUALITY_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_ACCEPTABLEOILQUALITY_UPD"
	
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
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableOilQuality), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then

                    DisplayData()
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_ACCEPTABLEOILQUALITY_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=menu/menu_PMSetup_page.aspx")
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
        
        strParam = "|AND AOQ.LocCode='" & strLocation & "'"
        Try
            intErrNo = objPMSetup.mtdGetAcceptableOilQuality(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_ACCEPTABLEOILQUALITY&errmesg=" & lblErrMessage.Text & "&redirect=menu/menu_PMSetup_page.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim blnFlag As Boolean

        If pv_strstatus = objPMSetup.EnumAcceptableOilQualityStatus.Deleted Then
            blnFlag = False
        ElseIf pv_strstatus = objPMSetup.EnumAcceptableOilQualityStatus.Active Then
            blnFlag = True
        End If

        txtOilTankMoist.Enabled = blnFlag
        txtOilTankDirt.Enabled = blnFlag
        txtPurifierMoist1.Enabled = blnFlag
        txtPurifierDirt1.Enabled = blnFlag
        txtPurifierMoist2.Enabled = blnFlag
        txtPurifierDirt2.Enabled = blnFlag
        txtPurifierMoist3.Enabled = blnFlag
        txtPurifierDirt3.Enabled = blnFlag
        txtPurifierMoist4.Enabled = blnFlag
        txtPurifierDirt4.Enabled = blnFlag
        txtCPOTankFFA1.Enabled = blnFlag
        txtCPOTankMoist1.Enabled = blnFlag
        txtCPOTankDirt1.Enabled = blnFlag
        txtCPOTankFFA2.Enabled = blnFlag
        txtCPOTankMoist2.Enabled = blnFlag
        txtCPOTankDirt2.Enabled = blnFlag
        txtCPOTankFFA3.Enabled = blnFlag
        txtCPOTankMoist3.Enabled = blnFlag
        txtCPOTankDirt3.Enabled = blnFlag
        txtCPOTankFFA4.Enabled = blnFlag
        txtCPOTankMoist4.Enabled = blnFlag
        txtCPOTankDirt4.Enabled = blnFlag
        txtCPOTankFFA5.Enabled = blnFlag
        txtCPOTankMoist5.Enabled = blnFlag
        txtCPOTankDirt5.Enabled = blnFlag
        txtCPOTankFFA6.Enabled = blnFlag
        txtCPOTankMoist6.Enabled = blnFlag
        txtCPOTankDirt6.Enabled = blnFlag
        txtProductOilFFA.Enabled = blnFlag
        txtProductOilMoist.Enabled = blnFlag
        txtProductOilDirt.Enabled = blnFlag
        Save.Visible = blnFlag
    End Sub

    Sub DisplayData()

        Dim dsAOQ As DataSet = LoadData()

        If dsAOQ.Tables(0).Rows.Count > 0 Then
            txtOilTankMoist.Text = dsAOQ.Tables(0).Rows(0).Item("OilTankMoist")
            txtOilTankDirt.Text = dsAOQ.Tables(0).Rows(0).Item("OilTankDirt")
            txtPurifierMoist1.Text = dsAOQ.Tables(0).Rows(0).Item("PurifierMoist1")
            txtPurifierDirt1.Text = dsAOQ.Tables(0).Rows(0).Item("PurifierDirt1")
            txtPurifierMoist2.Text = dsAOQ.Tables(0).Rows(0).Item("PurifierMoist2")
            txtPurifierDirt2.Text = dsAOQ.Tables(0).Rows(0).Item("PurifierDirt2")
            txtPurifierMoist3.Text = dsAOQ.Tables(0).Rows(0).Item("PurifierMoist3")
            txtPurifierDirt3.Text = dsAOQ.Tables(0).Rows(0).Item("PurifierDirt3")
            txtPurifierMoist4.Text = dsAOQ.Tables(0).Rows(0).Item("PurifierMoist4")
            txtPurifierDirt4.Text = dsAOQ.Tables(0).Rows(0).Item("PurifierDirt4")
            txtCPOTankFFA1.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankFFA1")
            txtCPOTankMoist1.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankMoist1")
            txtCPOTankDirt1.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankDirt1")
            txtCPOTankFFA2.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankFFA2")
            txtCPOTankMoist2.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankMoist2")
            txtCPOTankDirt2.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankDirt2")
            txtCPOTankFFA3.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankFFA3")
            txtCPOTankMoist3.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankMoist3")
            txtCPOTankDirt3.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankDirt3")
            txtCPOTankFFA4.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankFFA4")
            txtCPOTankMoist4.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankMoist4")
            txtCPOTankDirt4.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankDirt4")
            txtCPOTankFFA5.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankFFA5")
            txtCPOTankMoist5.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankMoist5")
            txtCPOTankDirt5.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankDirt5")
            txtCPOTankFFA6.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankFFA6")
            txtCPOTankMoist6.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankMoist6")
            txtCPOTankDirt6.Text = dsAOQ.Tables(0).Rows(0).Item("CPOTankDirt6")
            txtProductOilFFA.Text = dsAOQ.Tables(0).Rows(0).Item("ProductOilFFA")
            txtProductOilMoist.Text = dsAOQ.Tables(0).Rows(0).Item("ProductOilMoist")
            txtProductOilDirt.Text = dsAOQ.Tables(0).Rows(0).Item("ProductOilDirt")
            
            lblStatus.Text = objPMSetup.mtdGetAcceptableOilQualityStatus(Trim(dsAOQ.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsAOQ.Tables(0).Rows(0).Item("CreateDate")))
            txtCreateDate.Text = Trim(dsAOQ.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsAOQ.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsAOQ.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsAOQ.Tables(0).Rows(0).Item("Status")))
        End If
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strStatus As String
        
        If Trim(lblStatus.Text) = objPMSetup.mtdGetAcceptableOilQualityStatus(objPMSetup.EnumAcceptableOilQualityStatus.Deleted) Then
            strStatus = objPMSetup.EnumAcceptableOilQualityStatus.Deleted
        Else
            strStatus = objPMSetup.EnumAcceptableOilQualityStatus.Active
        End If

		           'txtFinalEffluentOW.Text.Trim & "|" & _

		strParam = "0.00|0.00|0.00|" & _
                   txtOilTankMoist.Text.Trim & "|" & _
                   txtOilTankDirt.Text.Trim & "|" & _
                   txtPurifierMoist1.Text.Trim & "|" & _
				   txtPurifierDirt1.Text.Trim & "|" & _
                   txtPurifierMoist2.Text.Trim & "|" & _
				   txtPurifierDirt2.Text.Trim & "|" & _
                   txtPurifierMoist3.Text.Trim & "|" & _
				   txtPurifierDirt3.Text.Trim & "|" & _
                   txtPurifierMoist4.Text.Trim & "|" & _
				   txtPurifierDirt4.Text.Trim & "|" & _
                   txtCPOTankFFA1.Text.Trim & "|" & _
                   txtCPOTankMoist1.Text.Trim & "|" & _
                   txtCPOTankDirt1.Text.Trim & "|" & _
                   txtCPOTankFFA2.Text.Trim & "|" & _
                   txtCPOTankMoist2.Text.Trim & "|" & _
                   txtCPOTankDirt2.Text.Trim & "|" & _
                   txtCPOTankFFA3.Text.Trim & "|" & _
                   txtCPOTankMoist3.Text.Trim & "|" & _
                   txtCPOTankDirt3.Text.Trim & "|" & _
                   txtCPOTankFFA4.Text.Trim & "|" & _
                   txtCPOTankMoist4.Text.Trim & "|" & _
                   txtCPOTankDirt4.Text.Trim & "|" & _
                   txtCPOTankFFA5.Text.Trim & "|" & _
                   txtCPOTankMoist5.Text.Trim & "|" & _
                   txtCPOTankDirt5.Text.Trim & "|" & _
                   txtCPOTankFFA6.Text.Trim & "|" & _
                   txtCPOTankMoist6.Text.Trim & "|" & _
                   txtCPOTankDirt6.Text.Trim & "|" & _
                   txtProductOilFFA.Text.Trim & "|" & _
                   txtProductOilMoist.Text.Trim & "|" & _
                   txtProductOilDirt.Text.Trim & "|" & _
                   strStatus & "|" & _
                   txtCreateDate.Text

        Try
            intErrNo = objPMSetup.mtdUpdAcceptableOilQuality(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strLocation, _
                                                strUserId, _
                                                strParam)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_ACCEPTABLEOILQUALITY&errmesg=" & lblErrMessage.Text & "&redirect=menu/menu_PMSetup_page.aspx")
        End Try
        
        Response.Redirect("PM_Setup_AcceptableOilQuality.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("../../menu/menu_PMSetup_page.aspx")
    End Sub


End Class
