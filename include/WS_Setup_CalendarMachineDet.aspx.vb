

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




Public Class WS_Setup_CalendarMachineDet : Inherits Page

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblBlkTag As Label
    Protected WithEvents ddlBlock As DropDownList
    Protected WithEvents lblErrBlock As Label
    Protected WithEvents lblSubBlkTag As Label
    Protected WithEvents ddlSubBlock As DropDownList
    Protected WithEvents lblErrSubBlock As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents chkAllMonth As CheckBox
    Protected WithEvents ddlMonth As DropDownList
    Protected WithEvents lblErrMonth As Label
    Protected WithEvents chkWeek1 As CheckBox
    Protected WithEvents chkWeek2 As CheckBox
    Protected WithEvents chkWeek3 As CheckBox
    Protected WithEvents chkWeek4 As CheckBox
    Protected WithEvents chkWeek5 As CheckBox
    Protected WithEvents lblErrDupl As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblSelect As Label

    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents SaveBtn As ImageButton 
    Protected WithEvents BackBtn As ImageButton 
    Protected objGL As New agri.GL.clsSetup()

    Dim objWS As New agri.WS.clsSetup()    
    Dim objINTrx As New agri.IN.clsTrx()    
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLset As New agri.GL.clsSetup()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
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
    Dim strSelectedSubBlkCode As String = ""
    Dim strSelectedBlkCode As String = ""
    Dim strSelectedMonth As String = ""
    Dim pv_strSubBlkCode As String
    Dim pv_strBlockCode As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        Dim arrParam As Array

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSCalMachine), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()  
            If Request.QueryString("tbcode") <> "" Then
                arrParam = Split(Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode"))), "|")
                strSelectedSubBlkCode = arrParam(0)
                strSelectedMonth = arrParam(1)
            Else
                strSelectedSubBlkCode = Trim(IIf(Request.QueryString("tbcode") <> "", Request.QueryString("tbcode"), Request.Form("tbcode")))
            End If

            lblErrBlock.Visible = False
            lblErrSubBlock.Visible = False
            lblErrMonth.Visible = False
            If Not IsPostBack Then
                If strSelectedSubBlkCode <> "" Then
                    BindSubBlock("")
                    BindBlock("")  
                    onLoad_Display()
                    onLoad_BindButton()
                Else
                    BindSubBlock("")
                    BindBlock("")            
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        Try
            lblBlkTag.Text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.Text
            lblSubBlkTag.Text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.Text

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_CALENDARMACHINE_GET_LANGCAP_COSTLEVEL&errmesg=" & Exp.ToString() & "&redirect=PR/trx/IN_trx_wpdet.aspx")
        End Try
        lblErrBlock.Text = lblErrSelect.Text & lblBlkTag.Text
        lblErrSubBlock.Text = lblErrSelect.Text & lblSubBlkTag.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_CALENDARMACHINE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_workcodedet.aspx")
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
            Case objWS.mtdGetCalendarizedMachineStatus(objWS.EnumCalendarizedMachineStatus.Active)
                ddlBlock.Enabled = False
                ddlSubBlock.Enabled = False
                ddlMonth.Enabled = False
                chkAllMonth.Enabled = False
                chkWeek1.Enabled = True
                chkWeek2.Enabled = True
                chkWeek3.Enabled = True
                chkWeek4.Enabled = True
                chkWeek5.Enabled = True
                SaveBtn.Visible = True 
                DelBtn.visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objWS.mtdGetCalendarizedMachineStatus(objWS.EnumCalendarizedMachineStatus.Deleted)
                ddlBlock.Enabled = False
                ddlSubBlock.Enabled = False
                ddlMonth.Enabled = False
                chkAllMonth.Enabled = False
                chkWeek1.Enabled = False
                chkWeek2.Enabled = False
                chkWeek3.Enabled = False
                chkWeek4.Enabled = False
                chkWeek5.Enabled = False
                SaveBtn.Visible = False 
                UnDelBtn.Visible = True
                UnDelBtn.Attributes("onclick") = "javascript:return ConfirmAction('undelete');"
        End Select
    End Sub

    Sub chkAllMonth_CheckedChanged(ByVal Sender As Object, ByVal E As EventArgs)  
        If chkAllMonth.Checked Then
            ddlMonth.Enabled = False
        Else
            ddlMonth.Enabled = True
        End If
    End Sub

    Sub BindBlock(ByVal pv_strBlockCode As String)
        Dim objBlkDs As New Dataset()
        Dim strOpCd As String = "GL_CLSSETUP_BLOCK_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = "|" & "And blk.Status = '" & objGLSetup.EnumBlockStatus.Active & "' And blk.LocCode ='" & strLocation & "'" '& "|" & strLocation
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objBlkDs)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_CALENDARMACHINE_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strBlockCode = Trim(UCase(pv_strBlockCode))

        For intCnt = 0 To objBlkDs.Tables(0).Rows.Count - 1
            objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objBlkDs.Tables(0).Rows(intCnt).Item("Description") = objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode") & " (" & objBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ") "
            If UCase(objBlkDs.Tables(0).Rows(intCnt).Item("BlkCode")) = pv_strBlockCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objBlkDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strBlockCode <> "" Then
                dr("BlkCode") = Trim(pv_strBlockCode)
                dr("Description") = Trim(pv_strBlockCode)
            Else
                dr("BlkCode") = ""
                dr("Description") = lblSelect.Text & lblBlkTag.Text
            End If
        Else
            dr("BlkCode") = ""
            dr("Description") = lblSelect.Text & lblBlkTag.Text
        End If
        objBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlBlock.DataSource = objBlkDs.Tables(0)
        ddlBlock.DataValueField = "BlkCode"
        ddlBlock.DataTextField = "Description"
        ddlBlock.DataBind()
        ddlBlock.SelectedIndex = intSelectedIndex
        objBlkDs = Nothing
    End Sub

    Sub onSelect_Block(ByVal Sender As Object, ByVal E As EventArgs)
        If Not (ddlBlock.SelectedItem.Value = "")
           strSelectedBlkCode = Trim(ddlBlock.SelectedItem.Value)
           BindSubBlock("")
        End If
    End Sub

    Sub BindSubBlock(ByVal pv_strSubBlkCode As String)
        Dim objSubBlkDs As New Dataset()
        Dim strOpCd As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intMasterType As Integer
        Dim intSelectedIndex As Integer = 0
        
        strParam = "|" & "And Sub.Status = '" & objGLSetup.EnumSubBlockStatus.Active & "' And BlkCode = '" & Trim(strSelectedBlkCode) & "'" & "|" & strLocation
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, strParam, 0, objSubBlkDs)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_CALENDARMACHINE_BLOCK_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        pv_strSubBlkCode = Trim(UCase(pv_strSubBlkCode))

        For intCnt = 0 To objSubBlkDs.Tables(0).Rows.Count - 1
            objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode").Trim()
            objSubBlkDs.Tables(0).Rows(intCnt).Item("Description") = objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode") & " (" & objSubBlkDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ") "
            If UCase(objSubBlkDs.Tables(0).Rows(intCnt).Item("SubBlkCode")) = pv_strSubBlkCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objSubBlkDs.Tables(0).NewRow()
        If intSelectedIndex = 0 Then
            If pv_strSubBlkCode <> "" Then
                dr("SubBlkCode") = Trim(pv_strSubBlkCode)
                dr("Description") = Trim(pv_strSubBlkCode)
            Else
                dr("SubBlkCode") = ""
                dr("Description") = lblSelect.Text & lblSubBlkTag.Text
            End If
        Else
            dr("SubBlkCode") = ""
            dr("Description") = lblSelect.Text & lblSubBlkTag.Text
        End If
        objSubBlkDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlSubBlock.DataSource = objSubBlkDs.Tables(0)
        ddlSubBlock.DataValueField = "SubBlkCode"
        ddlSubBlock.DataTextField = "Description"
        ddlSubBlock.DataBind()
        ddlSubBlock.SelectedIndex = intSelectedIndex
        objSubBlkDs = Nothing
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "WS_CLSSETUP_CALENDARMACHINE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        
        strParam = " And ws.LocCode = '" & strLocation & "' And ws.AccYear = '" & strAccYear & "' " & _
                   " And ws.SubBlkCode = '" & Trim(strSelectedSubBlkCode) & "' " & " And ws.SelMonth = '" & (strSelectedMonth) & "' " & "|"
                   
        Try
            intErrNo = objWS.mtdGetCalendarizedMachine(strOpCd, strParam, objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then
            If objDs.Tables(0).Rows(0).Item("SelWeek1") = objWS.EnumSelectedWeeks.Yes Then
                chkWeek1.Checked = True
            End If
            If objDs.Tables(0).Rows(0).Item("SelWeek2") = objWS.EnumSelectedWeeks.Yes Then
                chkWeek2.Checked = True
            End If
            If objDs.Tables(0).Rows(0).Item("SelWeek3") = objWS.EnumSelectedWeeks.Yes Then
                chkWeek3.Checked = True
            End If
            If objDs.Tables(0).Rows(0).Item("SelWeek4") = objWS.EnumSelectedWeeks.Yes Then
                chkWeek4.Checked = True
            End If
            If objDs.Tables(0).Rows(0).Item("SelWeek5") = objWS.EnumSelectedWeeks.Yes Then
                chkWeek5.Checked = True
            End If

            strSelectedBlkCode = Trim(objDs.Tables(0).Rows(0).Item("BlkCode"))
            BindBlock(strSelectedBlkCode)
            strSelectedSubBlkCode = Trim(objDs.Tables(0).Rows(0).Item("SubBlkCode"))
            BindSubBlock(strSelectedSubBlkCode)
            strSelectedMonth = Trim(objDs.Tables(0).Rows(0).Item("SelMonth"))
            ddlMonth.SelectedIndex = strSelectedMonth - 1

            lblStatus.Text = Trim(objWS.mtdGetCalendarizedMachineStatus(Convert.ToInt16(objDs.Tables(0).Rows(0).Item("Status"))))
            lblDateCreated.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objDs.Tables(0).Rows(0).Item("UpdateDate"))            
            lblUpdatedBy.Text = objDs.Tables(0).Rows(0).Item("UserName")
        End If       

        objDs = Nothing                                  
    End Sub

    Sub SaveBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "WS_CLSSETUP_CALENDARMACHINE_ADD"
        Dim strOpCd_Upd As String = "WS_CLSSETUP_CALENDARMACHINE_LIST_UPD"
        Dim strOpCd_Get As String = "WS_CLSSETUP_CALENDARMACHINE_LIST_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
       
        strSelectedBlkCode = Trim(ddlBlock.SelectedItem.Value)
        strSelectedSubBlkCode = Trim(ddlSubBlock.SelectedItem.Value) 
        strSelectedMonth = Trim(ddlMonth.SelectedItem.Value)
        
        If strSelectedBlkCode = "" Then
            lblErrBlock.Visible = True
        ElseIf strSelectedSubBlkCode = "" Then
            lblErrSubBlock.Visible = True
        ElseIf chkAllMonth.Checked = False Then
            If strSelectedMonth = "" Then
                lblErrMonth.Visible = True
            End If
        End If
            
        If chkAllMonth.Checked Then
            For intCnt = 1 to 12
                strParam = " And ws.LocCode = '" & strLocation & "' And ws.AccYear = '" & strAccYear & "' " & _
                        " And ws.SubBlkCode = '" & Trim(strSelectedSubBlkCode) & "' " & _
                        " And ws.SelMonth = '" & Trim(intCnt) & "' And ws.BlkCode = '" & ddlBlock.SelectedItem.Value & "' " & "|"
                            
                Try
                    intErrNo = objWS.mtdGetCalendarizedMachine(strOpCd_Get, strParam, objDs)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
                If objDs.Tables(0).Rows.Count > 0 Then
                    strParam = Trim(ddlBlock.SelectedItem.Value) & "|" & Trim(ddlSubBlock.SelectedItem.Value) & "|" & _
                            Trim(intCnt) & "|" & _
                            IIf(chkWeek1.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                            IIf(chkWeek2.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                            IIf(chkWeek3.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                            IIf(chkWeek4.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                            IIf(chkWeek5.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                            objWS.EnumCalendarizedMachineStatus.Active 

                    Try
                        intErrNo = objWS.mtdUpdCalendarizedMachine(strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strAccYear, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try
                Else
                    strParam = Trim(strLocation) & "|" & Trim(ddlBlock.SelectedItem.Value) & "|" & Trim(ddlSubBlock.SelectedItem.Value) & "|" & _
                        Trim(strAccYear) & "|" & Trim(intCnt) & "|" & _
                        IIf(chkWeek1.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek2.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek3.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek4.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek5.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        objWS.EnumCalendarizedMachineStatus.Active 

                    Try
                        intErrNo = objWS.mtdUpdCalendarizedMachine(strOpCd_Add, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strAccYear, _
                                                    strUserId, _
                                                    strParam, _
                                                    False)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try
                End If
            Next
        Else
            strParam = " And ws.LocCode = '" & strLocation & "' And ws.AccYear = '" & strAccYear & "' " & _
                        " And ws.SubBlkCode = '" & Trim(strSelectedSubBlkCode) & "' " & _
                        " And ws.SelMonth = '" & Trim(strSelectedMonth) & "' And ws.BlkCode = '" & ddlBlock.SelectedItem.Value & "' " & "|"
                            
            Try
                intErrNo = objWS.mtdGetCalendarizedMachine(strOpCd_Get, strParam, objDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            If objDs.Tables(0).Rows.Count > 0 Then
                strParam = Trim(ddlBlock.SelectedItem.Value) & "|" & Trim(ddlSubBlock.SelectedItem.Value) & "|" & _
                        Trim(strSelectedMonth) & "|" & _
                        IIf(chkWeek1.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek2.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek3.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek4.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek5.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        objWS.EnumCalendarizedMachineStatus.Active 

                Try
                    intErrNo = objWS.mtdUpdCalendarizedMachine(strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strAccYear, _
                                                strUserId, _
                                                strParam, _
                                                True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
            Else
                strParam = Trim(strLocation) & "|" & Trim(ddlBlock.SelectedItem.Value) & "|" & Trim(ddlSubBlock.SelectedItem.Value) & "|" & _
                        Trim(strAccYear) & "|" & Trim(strSelectedMonth) & "|" & _
                        IIf(chkWeek1.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek2.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek3.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek4.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        IIf(chkWeek5.Checked, objWS.EnumSelectedWeeks.Yes, objWS.EnumSelectedWeeks.No)  & "|" & _
                        objWS.EnumCalendarizedMachineStatus.Active 

                Try
                    intErrNo = objWS.mtdUpdCalendarizedMachine(strOpCd_Add, _
                                                strCompany, _
                                                strLocation, _
                                                strAccYear, _
                                                strUserId, _
                                                strParam, _
                                                False)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
            End If
        End If
                
        onLoad_Display()
        onLoad_BindButton()
    End Sub

    Sub DeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "WS_CLSSETUP_CALENDARMACHINE_LIST_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSelectedBlkCode = Trim(ddlBlock.SelectedItem.Value)
        strSelectedSubBlkCode = Trim(ddlSubBlock.SelectedItem.Value) 
        strSelectedMonth = Trim(ddlMonth.SelectedItem.Value)

        strParam = Trim(ddlBlock.SelectedItem.Value) & "|" & _
                   Trim(ddlSubBlock.SelectedItem.Value) & "|" & _
                   Trim(ddlMonth.SelectedItem.Value) & "|" & _
                   "|||||" & _
                   objWS.EnumCalendarizedMachineStatus.Deleted

        Try
            intErrNo = objWS.mtdUpdCalendarizedMachine(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strAccYear, _
                                           strUserId, _
                                           strParam, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display()
        onLoad_BindButton()
    End Sub

    Sub UnDeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "WS_CLSSETUP_CALENDARMACHINE_LIST_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSelectedBlkCode = Trim(ddlBlock.SelectedItem.Value)
        strSelectedSubBlkCode = Trim(ddlSubBlock.SelectedItem.Value) 
        strSelectedMonth = Trim(ddlMonth.SelectedItem.Value)

        strParam = Trim(ddlBlock.SelectedItem.Value) & "|" & _
                   Trim(ddlSubBlock.SelectedItem.Value) & "|" & _
                   Trim(ddlMonth.SelectedItem.Value) & "|" & _
                   "|||||" & _
                   objWS.EnumCalendarizedMachineStatus.Active

        Try
            intErrNo = objWS.mtdUpdCalendarizedMachine(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strAccYear, _
                                           strUserId, _
                                           strParam, _
                                           True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_MILLPROCDISTR_LIST_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        onLoad_Display()
        onLoad_BindButton()
    End Sub

    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WS_CalendarMachineList.aspx")
        dim isi as integer
    End Sub

   Public Function GetTotDayInMonth(ByVal dMonth As Integer, ByVal dYear As Integer) As Integer
        Dim chkWeek As CheckBox
        Dim dDate, sDate As Date
        Dim dDay As Integer
        Dim cMonth As Integer
        Dim dSunday As Integer = 0
        Dim dWeek, intWeek As Integer 
        Dim intCnt,intErrNo As Integer
        Dim mthCnt, itmCnt As Integer
        Dim HM As Integer 
        Dim strActHourMeter As Integer
        Dim HMDay As Integer 
        Dim NextReplHM As Integer
        Dim NextReplDate As Date
        Dim strOpCd_Item As String = "WS_CLSSETUP_PREVESTIMATION_ITEM_GET"
        Dim strOpCd_Month As String = "WS_CLSSETUP_PREVESTIMATION_MONTH_GET"
        Dim strOpCd_Add As String = "WS_CLSSETUP_PREV_ESTIMATION_ADD"
        Dim strOpCd_Upd As String = "WS_CLSSETUP_PREV_ESTIMATION_UPD"
        Dim strParam As String = ""
        Dim strItemCode, strLinesNo, strLinesDesc, strBlkCode, strSubBlkCode As String
        Dim strInstallDate, strTransDate As Date
        Dim strLifetime As Integer
        Dim objItem As New Dataset()
        Dim objMonth As New Dataset()
        Dim strHMDay, strHM, strDate As String

        strParam = "|" & " IM.LocCode = '" & Trim(strLocation) & "' " & "|" & strLocation

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Item, strParam, 0, objItem)
 
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_PREVESTIMATION_ITEM_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        
        If objItem.Tables(0).Rows.Count > 0 Then
            For itmCnt = 0 To objItem.Tables(0).Rows.Count-1
                strBlkCode = Trim(objItem.Tables(0).Rows(itmCnt).Item("BlkCode"))   
                strSubBlkCode = Trim(objItem.Tables(0).Rows(itmCnt).Item("SubBlkCode"))
                strItemCode = Trim(objItem.Tables(0).Rows(itmCnt).Item("ItemCode"))
                strLinesNo = Trim(objItem.Tables(0).Rows(itmCnt).Item("LinesNo"))
                strLinesDesc = Trim(objItem.Tables(0).Rows(itmCnt).Item("LinesDesc"))
                strInstallDate = objGlobal.GetLongDate(objItem.Tables(0).Rows(itmCnt).Item("InstallDate"))
                strLifetime = objItem.Tables(0).Rows(itmCnt).Item("Lifetime")
                strActHourMeter = objItem.Tables(0).Rows(itmCnt).Item("ActHourMeter")
                    
                strParam = Trim(strLocation) & "|" & Trim(strBlkCode) & "|" & Trim(strSubBlkCode) & "|" & Trim(strAccYear) & "|" & _
                           Trim(strItemCode) & "|" & Trim(strLinesNo) & "|" & Trim(strLinesDesc) & "|" & Trim(strLifetime) & "|" & _
                           strInstallDate & "|1"
              
                Try
                    intErrNo = objINTrx.mtdUpdPreventiveMaintenance(strOpCd_Add, _
                                                strCompany, _
                                                strLocation, _
                                                strAccYear, _
                                                strUserId, _
                                                strParam, _
                                                False)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_PREV_ESTIMATION_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try

                strParam = "|" & " CM.LocCode = '" & Trim(strLocation) & "' And CM.AccYear = '" & Trim(strAccYear) & "'" & _
                           " And BlkCode = '" & strBlkCode & "' And SubBlkCode = '" & strSubBlkCode & "' And CM.Status = '" & objWS.EnumCalendarizedMachineStatus.Active & "'" & "|" & strLocation
     
                Try
                    intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Month, strParam, 0, objMonth)
         
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_PREVESTIMATION_MONTH_GET&errmesg=" & Exp.ToString() & "&redirect=")
                End Try
                
                HM = strActHourMeter
                NextReplHM = strActHourMeter + strLifetime

                If objMonth.Tables(0).Rows.Count > 0 Then
                    intCnt = 1
                    dWeek = 1
                    For mthCnt = 0 To objMonth.Tables(0).Rows.Count-1
                        cMonth = mthCnt + 1
                        dMonth = objMonth.Tables(0).Rows(mthCnt).Item("SelMonth")
                        dDate = Iif(mthCnt = 0, strInstallDate, dMonth & "/1/" & strAccYear)
                        Select Case dMonth
                            Case 1 
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("JanHMDay")
                                strHMDay = "JANHMDAY"
                                strHM = "JANHM"
                                strDate = "JANDATE"
                            Case 2
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("FebHMDay")
                                strHMDay = "FEBHMDAY"
                                strHM = "FEBHM"
                                strDate = "FEBDATE"
                            Case 3
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("MarHMDay")
                                strHMDay = "MARHMDAY"
                                strHM = "MARHM"
                                strDate = "MARDATE"
                            Case 4
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("AprHMDay")
                                strHMDay = "APRHMDAY"
                                strHM = "APRHM"
                                strDate = "APRDATE"
                            Case 5
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("MayHMDay")
                                strHMDay = "MAYHMDAY"
                                strHM = "MAYHM"
                                strDate = "MAYDATE"
                            Case 6
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("JunHMDay")
                                strHMDay = "JUNHMDAY"
                                strHM = "JUNHM"
                                strDate = "JUNDATE"
                            Case 7
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("JulHMDay")
                                strHMDay = "JULHMDAY"
                                strHM = "JULHM"
                                strDate = "JULDATE"
                            Case 8
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("AugHMDay")
                                strHMDay = "AUGHMDAY"
                                strHM = "AUGHM"
                                strDate = "AUGDATE"
                            Case 9
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("SepHMDay")
                                strHMDay = "SEPHMDAY"
                                strHM = "SEPHM"
                                strDate = "SEPDATE"
                            Case 10
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("OctHMDay")
                                strHMDay = "OCTHMDAY"
                                strHM = "OCTHM"
                                strDate = "OCTDATE"
                            Case 11
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("NovHMDay")
                                strHMDay = "NOVHMDAY"
                                strHM = "NOVHM"
                                strDate = "NOVDATE"
                            Case 12
                                HMDay = objMonth.Tables(0).Rows(mthCnt).Item("DecHMDay")
                                strHMDay = "DECHMDAY"
                                strHM = "DECHM"
                                strDate = "DECDATE"
                        End Select 
                        
                        For intCnt = dWeek To 6
                            Select Case intCnt
                                Case 1
                                    intWeek = objMonth.Tables(0).Rows(mthCnt).Item("SelWeek1") 
                                Case 2
                                    intWeek = objMonth.Tables(0).Rows(mthCnt).Item("SelWeek2")  
                                Case 3
                                    intWeek = objMonth.Tables(0).Rows(mthCnt).Item("SelWeek3") 
                                Case 4
                                    intWeek = objMonth.Tables(0).Rows(mthCnt).Item("SelWeek4") 
                                Case 5
                                    intWeek = objMonth.Tables(0).Rows(mthCnt).Item("SelWeek5") 
                                Case Else
                                    intWeek = objMonth.Tables(0).Rows(mthCnt).Item("SelWeek5")                                         
                            End Select
                            
                            If intWeek = objWS.EnumSelectedWeeks.Yes Then                                    
                                Do While Month(dDate) = dMonth
                                    If DatePart(DateInterval.Weekday, CDate(dDate)) <> vbSunday Then
                                        HM = HM + HMDay
                                        If HM >= NextReplHM Then
                                            NextReplHM = HM + strLifetime
                                            NextReplDate = dDate

                                            strHMDay = strHMDay & "='" & HMDay & "'"
                                            strHM = strHM & "='" & HM & "'"
                                            strDate = strDate & "='" & dDate & "'"
                                            strParam = Trim(strLocation) & "|" & Trim(strBlkCode) & "|" & Trim(strSubBlkCode) & "|" & Trim(strAccYear) & "|" & _
                                                       Trim(strItemCode) & "|" & Trim(strLinesNo) & "|" & _
                                                       Trim(strHMDay) & "|" & Trim(strHM) & "|" & Trim(strDate) & "|1"
                                            
                                            Try
                                                intErrNo = objINTrx.mtdUpdPreventiveMaintenance(strOpCd_Upd, _
                                                                            strCompany, _
                                                                            strLocation, _
                                                                            strAccYear, _
                                                                            strUserId, _
                                                                            strParam, _
                                                                            True)
                                            Catch Exp As System.Exception
                                                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_PREV_ESTIMATION_ADD&errmesg=" & lblErrMessage.Text & "&redirect=")
                                            End Try

                                        End If
                                        If DatePart(DateInterval.Weekday, CDate(dDate)) = vbSaturday Then
                                            dWeek = dWeek + 1                    
                                            dDate = DateAdd(DateInterval.Day, 1, CDate(dDate))
                                            Exit Do
                                        End If
                                    End If
                                    
                                    dDate = DateAdd(DateInterval.Day, 1, CDate(dDate))
                                Loop    
                            Else
                                Do While Month(dDate) = dMonth
                                    If DatePart(DateInterval.Weekday, CDate(dDate)) = vbSaturday Then
                                        dWeek = dWeek + 1
                                        dDate = DateAdd(DateInterval.Day, 1, CDate(dDate))
                                        Exit Do
                                    End If
                                
                                    dDate = DateAdd(DateInterval.Day, 1, CDate(dDate))
                                Loop      
                            End If
                            dDate = DateAdd(DateInterval.Day, 1, CDate(dDate))
                        Next
                        intCnt = 1
                        dWeek = 1
                    Next
                End If
            Next 
        End If

  
    End Function


End Class
