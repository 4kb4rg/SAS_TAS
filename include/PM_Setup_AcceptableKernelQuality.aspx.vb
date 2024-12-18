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

Public Class PM_Setup_AcceptableKernelQuality : Inherits Page
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
    

    Protected WithEvents txtDryBK1 As TextBox			
    Protected WithEvents txtDryLShell1 As TextBox		
    Protected WithEvents txtDryShellWN1 As TextBox		
    Protected WithEvents txtDryShellBN1 As TextBox		
    Protected WithEvents txtDryTotalDirt1 As TextBox	
    Protected WithEvents txtDryFlowRate1 As TextBox	
    Protected WithEvents txtDryBK2 As TextBox			
    Protected WithEvents txtDryLShell2 As TextBox		
    Protected WithEvents txtDryShellWN2 As TextBox		
    Protected WithEvents txtDryShellBN2 As TextBox		
    Protected WithEvents txtDryTotalDirt2 As TextBox	
    Protected WithEvents txtDryFlowRate2 As TextBox	
    Protected WithEvents txtClayBathBK1 As TextBox		
    Protected WithEvents txtClayBathLShell1 As TextBox
    Protected WithEvents txtClayBathShellWN1 As TextBox
    Protected WithEvents txtClayBathShellBN1 As TextBox
    Protected WithEvents txtClayBathTotalDirt1 As TextBox
    Protected WithEvents txtClayBathFlowRate1 As TextBox
    Protected WithEvents txtClayBathBK2 As TextBox		
    Protected WithEvents txtClayBathLShell2 As TextBox
    Protected WithEvents txtClayBathShellWN2 As TextBox
    Protected WithEvents txtClayBathShellBN2 As TextBox
    Protected WithEvents txtClayBathTotalDirt2 As TextBox
    Protected WithEvents txtClayBathFlowRate2 As TextBox

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

    Dim strOppCd_GET As String = "PM_CLSSETUP_ACCEPTABLEKERNELQUALITY_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_ACCEPTABLEKERNELQUALITY_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_ACCEPTABLEKERNELQUALITY_UPD"
	
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMAcceptableKernelQuality), intPMAR) = False Then
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
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_ACCEPTABLEKERNELQUALITY_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=menu/menu_PMSetup_page.aspx")
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
        
        strParam = "|AND AKQ.LocCode='" & strLocation & "'"
        Try
            intErrNo = objPMSetup.mtdGetAcceptableKernelQuality(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_ACCEPTABLEKERNELQUALITY&errmesg=" & lblErrMessage.Text & "&redirect=menu/menu_PMSetup_page.aspx")
        End Try

        Return objDataSet
    End Function


    Sub DisableControl(ByVal pv_strstatus)
        Dim blnFlag As Boolean

        If pv_strstatus = objPMSetup.EnumAcceptableKernelQualityStatus.Deleted Then
            blnFlag = False
        ElseIf pv_strstatus = objPMSetup.EnumAcceptableKernelQualityStatus.Active Then
            blnFlag = True
        End If

        txtDryBK1.Enabled = blnFlag
        txtDryLShell1.Enabled = blnFlag
        txtDryShellWN1.Enabled = blnFlag
        txtDryShellBN1.Enabled = blnFlag
        txtDryTotalDirt1.Enabled = blnFlag
        txtDryFlowRate1.Enabled = blnFlag
        txtDryBK2.Enabled = blnFlag
        txtDryLShell2.Enabled = blnFlag
        txtDryShellWN2.Enabled = blnFlag
        txtDryShellBN2.Enabled = blnFlag
        txtDryTotalDirt2.Enabled = blnFlag
        txtDryFlowRate2.Enabled = blnFlag
        txtClayBathBK1.Enabled = blnFlag
        txtClayBathLShell1.Enabled = blnFlag
        txtClayBathShellWN1.Enabled = blnFlag
        txtClayBathShellBN1.Enabled = blnFlag
        txtClayBathTotalDirt1.Enabled = blnFlag
        txtClayBathFlowRate1.Enabled = blnFlag
        txtClayBathBK2.Enabled = blnFlag
        txtClayBathLShell2.Enabled = blnFlag
        txtClayBathShellWN2.Enabled = blnFlag
        txtClayBathShellBN2.Enabled = blnFlag
        txtClayBathTotalDirt2.Enabled = blnFlag
        txtClayBathFlowRate2.Enabled = blnFlag
        Save.Visible = blnFlag
    End Sub

    Sub DisplayData()

        Dim dsAOQ As DataSet = LoadData()

        If dsAOQ.Tables(0).Rows.Count > 0 Then
            txtDryBK1.Text = dsAOQ.Tables(0).Rows(0).Item("DryBK1") 
            txtDryLShell1.Text = dsAOQ.Tables(0).Rows(0).Item("DryLShell1")
            txtDryShellWN1.Text = dsAOQ.Tables(0).Rows(0).Item("DryShellWN1")
            txtDryShellBN1.Text = dsAOQ.Tables(0).Rows(0).Item("DryShellBN1")
            txtDryTotalDirt1.Text = dsAOQ.Tables(0).Rows(0).Item("DryTotalDirt1")
            txtDryFlowRate1.Text = dsAOQ.Tables(0).Rows(0).Item("DryFlowRate1")
            txtDryBK2.Text = dsAOQ.Tables(0).Rows(0).Item("DryBK2") 
            txtDryLShell2.Text = dsAOQ.Tables(0).Rows(0).Item("DryLShell2")
            txtDryShellWN2.Text = dsAOQ.Tables(0).Rows(0).Item("DryShellWN2")
            txtDryShellBN2.Text = dsAOQ.Tables(0).Rows(0).Item("DryShellBN2")
            txtDryTotalDirt2.Text = dsAOQ.Tables(0).Rows(0).Item("DryTotalDirt2")
            txtDryFlowRate2.Text = dsAOQ.Tables(0).Rows(0).Item("DryFlowRate2")
            txtClayBathBK1.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathBK1")
            txtClayBathLShell1.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathLShell1")
            txtClayBathShellWN1.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathShellWN1")
            txtClayBathShellBN1.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathShellBN1")
            txtClayBathTotalDirt1.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathTotalDirt1")
            txtClayBathFlowRate1.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathFlowRate1")
            txtClayBathBK2.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathBK2")
            txtClayBathLShell2.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathLShell2")
            txtClayBathShellWN2.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathShellWN2")
            txtClayBathShellBN2.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathShellBN2")
            txtClayBathTotalDirt2.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathTotalDirt2")
            txtClayBathFlowRate2.Text = dsAOQ.Tables(0).Rows(0).Item("ClayBathFlowRate2")

            lblStatus.Text = objPMSetup.mtdGetAcceptableKernelQualityStatus(Trim(dsAOQ.Tables(0).Rows(0).Item("Status")))
            lblCreateDate.Text = objGlobal.GetLongDate(Trim(dsAOQ.Tables(0).Rows(0).Item("CreateDate")))
            txtCreateDate.Text = Trim(dsAOQ.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(Trim(dsAOQ.Tables(0).Rows(0).Item("UpdateDate")))
            lblUpdateBy.Text = Trim(dsAOQ.Tables(0).Rows(0).Item("Username"))

            DisableControl(Trim(dsAOQ.Tables(0).Rows(0).Item("Status")))
        End If
    End Sub

    Sub btnSave_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strStatus As String
        
        If Trim(lblStatus.Text) = objPMSetup.mtdGetAcceptableKernelQualityStatus(objPMSetup.EnumAcceptableKernelQualityStatus.Deleted) Then
            strStatus = objPMSetup.EnumAcceptableKernelQualityStatus.Deleted
        Else
            strStatus = objPMSetup.EnumAcceptableKernelQualityStatus.Active
        End If
        strParam =  txtDryBK1.Text.Trim & "|" & _ 
                    txtDryLShell1.Text.Trim & "|" & _
                    txtDryShellWN1.Text.Trim & "|" & _
                    txtDryShellBN1.Text.Trim & "|" & _
                    txtDryTotalDirt1.Text.Trim & "|" & _
                    txtDryFlowRate1.Text.Trim & "|" & _
                    txtDryBK2.Text.Trim & "|" & _
                    txtDryLShell2.Text.Trim & "|" & _
                    txtDryShellWN2.Text.Trim & "|" & _
                    txtDryShellBN2.Text.Trim & "|" & _
                    txtDryTotalDirt2.Text.Trim & "|" & _
                    txtDryFlowRate2.Text.Trim & "|" & _
                    txtClayBathBK1.Text.Trim & "|" & _
                    txtClayBathLShell1.Text.Trim & "|" & _
                    txtClayBathShellWN1.Text.Trim & "|" & _
                    txtClayBathShellBN1.Text.Trim & "|" & _
                    txtClayBathTotalDirt1.Text.Trim & "|" & _
                    txtClayBathFlowRate1.Text.Trim & "|" & _
                    txtClayBathBK2.Text.Trim & "|" & _
                    txtClayBathLShell2.Text.Trim & "|" & _
                    txtClayBathShellWN2.Text.Trim & "|" & _
                    txtClayBathShellBN2.Text.Trim & "|" & _
                    txtClayBathTotalDirt2.Text.Trim & "|" & _
                    txtClayBathFlowRate2.Text.Trim & "|" & _
                    strStatus & "|" & _
                    txtCreateDate.Text
        response.write (strParam)
        Try
            intErrNo = objPMSetup.mtdUpdAcceptableKernelQuality(strOppCd_ADD, _
                                                strOppCd_UPD, _
                                                strOppCd_GET, _
                                                strLocation, _
                                                strUserId, _
                                                strParam)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PM_CLSSETUP_ACCEPTABLEKERNELQUALITY&errmesg=" & lblErrMessage.Text & "&redirect=menu/menu_PMSetup_page.aspx")
        End Try
        
        Response.Redirect("PM_Setup_AcceptableKernelQuality.aspx")
    End Sub

    Sub btnBack_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("../../menu/menu_PMSetup_page.aspx")
    End Sub


End Class
