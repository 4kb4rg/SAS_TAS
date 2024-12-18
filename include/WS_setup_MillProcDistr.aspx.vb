

Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports System.Collections

Imports System.Data.SqlClient
Imports Microsoft.VisualBasic


Imports agri.WS.clsSetup
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class WS_Setup_MillProcDistr : Inherits Page

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents txtJan As TextBox
    Protected WithEvents lblErrBlankJan As Label
    Protected WithEvents txtFeb As TextBox
    Protected WithEvents lblErrBlankFeb As Label
    Protected WithEvents txtMar As TextBox
    Protected WithEvents lblErrBlankMar As Label
    Protected WithEvents txtApr As TextBox
    Protected WithEvents lblErrBlankApr As Label
    Protected WithEvents txtMay As TextBox
    Protected WithEvents lblErrBlankMay As Label
    Protected WithEvents txtJun As TextBox
    Protected WithEvents lblErrBlankJun As Label
    Protected WithEvents txtJul As TextBox
    Protected WithEvents lblErrBlankJul As Label
    Protected WithEvents txtAug As TextBox
    Protected WithEvents lblErrBlankAug As Label
    Protected WithEvents txtSep As TextBox
    Protected WithEvents lblErrBlankSep As Label
    Protected WithEvents txtOct As TextBox
    Protected WithEvents lblErrBlankOct As Label
    Protected WithEvents txtNov As TextBox
    Protected WithEvents lblErrBlankNov As Label
    Protected WithEvents txtDec As TextBox
    Protected WithEvents lblErrBlankDec As Label
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton 
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDupl As Label
    Protected objGL As New agri.GL.clsSetup()

    Dim objWS As New agri.WS.clsSetup()    
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim objDs As New Object()
    Dim objLangCapDs As New Object()
    
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer
    Dim strBlkTag As String
    Dim intConfigsetting As Integer
    Dim strLocType as String
 
    Dim strServTypeCode As String = ""
    Dim strAccCode As String = ""
    Dim strBlkCode As String = ""

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSMillProcDistr), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrBlankJan.Visible = False
            lblErrBlankFeb.Visible = False
            lblErrBlankMar.Visible = False
            lblErrBlankApr.Visible = False
            lblErrBlankMay.Visible = False
            lblErrBlankJun.Visible = False
            lblErrBlankJul.Visible = False
            lblErrBlankAug.Visible = False
            lblErrBlankSep.Visible = False
            lblErrBlankOct.Visible = False
            lblErrBlankNov.Visible = False
            lblErrBlankDec.Visible = False

            If Not IsPostBack Then
                onLoad_Display()
                onLoad_BindButton()
            End If
        End If
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_WORKDET_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_workcodedet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function

    Sub onLoad_BindButton()
        UnDelBtn.visible = False
        DelBtn.visible = False

        Select Case Trim(lblStatus.Text)
            Case objWS.mtdGetMillProcessDistributionStatus(objWS.EnumMillProcessDistributionStatus.Active)
                txtJan.Enabled = True
                txtFeb.Enabled = True
                txtMar.Enabled = True
                txtApr.Enabled = True
                txtMay.Enabled = True
                txtJun.Enabled = True
                txtJul.Enabled = True
                txtAug.Enabled = True
                txtSep.Enabled = True
                txtOct.Enabled = True
                txtNov.Enabled = True
                txtDec.Enabled = True
                SaveBtn.Visible = True 
                DelBtn.visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objWS.mtdGetMillProcessDistributionStatus(objWS.EnumMillProcessDistributionStatus.Deleted)
                txtJan.Enabled = False
                txtFeb.Enabled = False
                txtMar.Enabled = False
                txtApr.Enabled = False
                txtMay.Enabled = False
                txtJun.Enabled = False
                txtJul.Enabled = False
                txtAug.Enabled = False
                txtSep.Enabled = False
                txtOct.Enabled = False
                txtNov.Enabled = False
                txtDec.Enabled = False   
                SaveBtn.Visible = False 
                UnDelBtn.Visible = True
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "WS_CLSSETUP_MILLPROCDISTR_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        
        strParam = strLocation & "|" & strAccYear

        Try
            intErrNo = objWS.mtdGetMillProcessDitribution(strOpCd, strParam, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            txtJan.Text = Trim(objDs.Tables(0).Rows(0).Item("JanHM"))
            txtFeb.Text = Trim(objDs.Tables(0).Rows(0).Item("FebHM"))
            txtMar.Text = Trim(objDs.Tables(0).Rows(0).Item("MarHM"))
            txtApr.Text = Trim(objDs.Tables(0).Rows(0).Item("AprHM"))
            txtMay.Text = Trim(objDs.Tables(0).Rows(0).Item("MayHM"))
            txtJun.Text = Trim(objDs.Tables(0).Rows(0).Item("JunHM"))
            txtJul.Text = Trim(objDs.Tables(0).Rows(0).Item("JulHM"))
            txtAug.Text = Trim(objDs.Tables(0).Rows(0).Item("AugHM"))
            txtSep.Text = Trim(objDs.Tables(0).Rows(0).Item("SepHM"))
            txtOct.Text = Trim(objDs.Tables(0).Rows(0).Item("OctHM"))
            txtNov.Text = Trim(objDs.Tables(0).Rows(0).Item("NovHM"))
            txtDec.Text = Trim(objDs.Tables(0).Rows(0).Item("DecHM"))           

            lblStatus.Text = Trim(objWS.mtdGetMillProcessDistributionStatus(Convert.ToInt16(objDs.Tables(0).Rows(0).Item("Status"))))
            lblDateCreated.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("UpdateDate"))            
            lblUpdatedBy.Text = objDs.Tables(0).Rows(0).Item("UserName")
        End If                                         
    End Sub

    Sub SaveBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "WS_CLSSETUP_MILLPROCDISTR_LIST_ADD"
        Dim strOpCd_Upd As String = "WS_CLSSETUP_MILLPROCDISTR_LIST_UPD"
        Dim strOpCd_Get As String = "WS_CLSSETUP_MILLPROCDISTR_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer

        If txtJan.Text = "" Then 
            lblErrBlankJan.Visible = True
            Exit Sub
        ElseIf txtFeb.Text = "" Then 
            lblErrBlankFeb.Visible = True
            Exit Sub
        ElseIf txtMar.Text = "" Then 
            lblErrBlankMar.Visible = True
            Exit Sub
        ElseIf txtApr.Text = "" Then 
            lblErrBlankApr.Visible = True
            Exit Sub
        ElseIf txtMay.Text = "" Then 
            lblErrBlankMay.Visible = True
            Exit Sub
        ElseIf txtJun.Text = "" Then 
            lblErrBlankJun.Visible = True
            Exit Sub
        ElseIf txtJul.Text = "" Then 
            lblErrBlankJul.Visible = True
            Exit Sub
        ElseIf txtAug.Text = "" Then 
            lblErrBlankAug.Visible = True
            Exit Sub
        ElseIf txtSep.Text = "" Then 
            lblErrBlankSep.Visible = True
            Exit Sub
        ElseIf txtOct.Text = "" Then 
            lblErrBlankOct.Visible = True
            Exit Sub
        ElseIf txtNov.Text = "" Then 
            lblErrBlankNov.Visible = True
            Exit Sub
        ElseIf txtDec.Text = "" Then 
            lblErrBlankDec.Visible = True
            Exit Sub
        End If

        strParam = strLocation & "|" & strAccYear

        Try
            intErrNo = objWS.mtdGetMillProcessDitribution(strOpCd_Get, strParam, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        If objDs.Tables(0).Rows.Count > 0 Then
            strParam = Trim(txtJan.Text) & "|" & Trim(txtFeb.Text) & "|" & Trim(txtMar.Text) & "|" & _
                    Trim(txtApr.Text) & "|" & Trim(txtMay.Text) & "|" & Trim(txtJun.Text) & "|" & _
                    Trim(txtJul.Text) & "|" & Trim(txtAug.Text) & "|" & Trim(txtSep.Text) & "|" & _
                    Trim(txtOct.Text) & "|" & Trim(txtNov.Text) & "|" & Trim(txtDec.Text) & "|" & _ 
                    objWS.EnumMillProcessDistributionStatus.Active 

            Try
                intErrNo = objWS.mtdUpdMillProcessDitribution(strOpCd_Upd, strCompany, strLocation, strUserId, strAccYear, strParam, True)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        Else 
            strParam = Trim(txtJan.Text) & "|" & Trim(txtFeb.Text) & "|" & Trim(txtMar.Text) & "|" & _
                    Trim(txtApr.Text) & "|" & Trim(txtMay.Text) & "|" & Trim(txtJun.Text) & "|" & _
                    Trim(txtJul.Text) & "|" & Trim(txtAug.Text) & "|" & Trim(txtSep.Text) & "|" & _
                    Trim(txtOct.Text) & "|" & Trim(txtNov.Text) & "|" & Trim(txtDec.Text) & "|" & _ 
                    objWS.EnumMillProcessDistributionStatus.Active 

            Try
                intErrNo = objWS.mtdUpdMillProcessDitribution(strOpCd_Add, strCompany, strLocation, strUserId, strAccYear, strParam, False)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        End If
        
        onLoad_Display()
        onLoad_BindButton()
    End Sub

    Sub DeleteBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd As String = "WS_CLSSETUP_MILLPROCDISTR_LIST_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        
        strParam = "||||||||||||" & _ 
                objWS.EnumMillProcessDistributionStatus.Deleted 

        Try
            intErrNo = objWS.mtdUpdMillProcessDitribution(strOpCd, strCompany, strLocation, strUserId, strAccYear, strParam, True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        onLoad_Display()
        onLoad_BindButton()
    End Sub

    Sub UnDelBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd As String = "WS_CLSSETUP_MILLPROCDISTR_LIST_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        
        strParam = "||||||||||||" & _ 
                objWS.EnumMillProcessDistributionStatus.Active 

        Try
            intErrNo = objWS.mtdUpdMillProcessDitribution(strOpCd, strCompany, strLocation, strUserId, strAccYear, strParam, True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display()
        onLoad_BindButton()
    End Sub




End Class
