
Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.PWSystem
Imports agri.Admin
Imports agri.PR
Imports agri.GL
Imports agri.GlobalHdl

Public Class PR_setup_AirBusdet : Inherits Page

    Protected WithEvents txtAirBusCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtAmount As TextBox
    Protected WithEvents ddlPOHCode As DropDownList
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents ddlADCode As DropDownList
    Protected WithEvents ddlType As DropDownList
    Protected WithEvents ddlCategory As DropDownList
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblPOH As Label
    Protected WithEvents lblLoc As Label
    Protected WithEvents lblSelect As Label
    Protected WithEvents lblDefault As Label
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupAirBusCode As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblErrPOHCode As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblErrLocation As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents lblErrADCode As Label
    Protected WithEvents lblErrType As Label
    Protected WithEvents lblErrCategory As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents lbldgPOHCode As Label
    Protected WithEvents lbldgLocation As Label
    Protected WithEvents lbldgType As Label
    Protected WithEvents lbldgCategory As Label
    Protected WithEvents lbldgAmount As Label
    Protected WithEvents lbldgTicketCode As Label
    Protected WithEvents lblTotalAmount As Label
    Protected WithEvents ticketcode As HtmlInputHidden
    Protected WithEvents btnFind1 As HtmlInputButton    
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents BtnAdd As ImageButton
    Protected WithEvents dgAirBusTicket As DataGrid

    Protected WithEvents lblErrExists As Label
    Protected WithEvents lblErrDouble As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTitle1 As Label

    Protected WithEvents ddlNearLocation As DropDownList
    Protected WithEvents lblErrNearLocation As Label
    Protected WithEvents lblNearLoc As Label
    Protected WithEvents lblErrFill As Label
    Protected WithEvents lblErrInsert As Label    
    Protected WithEvents tblSelection As HtmlTable

    
    Protected objGlobalHdl As New agri.GlobalHdl.clsGlobalHdl()
    
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objAdminComp As New agri.Admin.clsComp()
    Dim objAdminUOM As New agri.Admin.clsUOM()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objHRSetup As New agri.HR.clsSetup()
    Dim objSysLoc As New agri.Admin.clsLoc()

    Dim objADDs As New Object()
    Dim objAirBusDs As New Object()
    Dim objLocDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objPOHDs As New Object()
    Dim objResult As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strSelectedAirBusCode As String = ""
    Dim intStatus As Integer
    Dim strLocType as String


    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        intConfig = Session("SS_CONFIGSETTING")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRAirBus), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            
            lblErrDupAirBusCode.Visible = False
            lblErrADCode.Visible = False 
            lblErrType.Visible = False
            lblErrCategory.Visible = False
            lblErrPOHCode.Visible = False
            lblErrLocation.Visible = False
            lblErrExists.Visible = False  
            lblErrDouble.Visible = False  
            lblErrNearLocation.Visible = False
            lblErrFill.Visible = False
            lblErrInsert.Visible = False
            
            strSelectedAirBusCode = Trim(IIf(Request.QueryString("ticketcode") <> "", Request.QueryString("ticketcode"), Request.Form("ticketcode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                lblErrPOHCode.Text = lblErrPOHCode.Text & lblPOH.Text
                lblErrLocation.Text = lblErrLocation.Text & lblLoc.Text
                If strSelectedAirBusCode <> "" Then
                    onLoad_Display()
                    onLoad_LineDisplay()
                    onLoad_BindButton
                Else
                    onLoad_BindPOH("")
                    onLoad_BindLocation(strLocation)
                    onLoad_BindNearLocation("")
                    onLoad_BindADCode("")
                    onload_BindAirBusCategory("")
                    onLoad_BindButton()                    
                End If
            End If
        End If
        If strSelectedAirBusCode = "" Then
            tblSelection.Visible = False
        End If
    End Sub


    Sub onSelect_ChangeType (Sender As Object, E As EventArgs)
       onload_BindAirBusCategory("")
       tblSelection.Visible = True
    End Sub

    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdLn_Add as String = "PR_CLSSETUP_AIRBUSTICKET_LN_ADD"
        Dim strOpCd_Add As String = "PR_CLSSETUP_AIRBUSTICKET_ADD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_AIRBUSTICKET_GET"
        Dim strOpCd_GetLN As String = "PR_CLSSETUP_AIRBUSTICKET_LN_GET"
        Dim objSearch As New object()
        Dim strPOH as String = Trim(ddlPOHCode.SelectedItem.Value)
        Dim strLoc as String = Trim(ddlLocation.SelectedItem.Value)
        Dim strNearLoc as String = Trim(ddlNearLocation.SelectedItem.Value)

        Dim strOpCd As String
        Dim intErrNo As Integer
        Dim strParam As String = ""
        
        Dim intAmount As Double
      
        tblSelection.Visible = True
        If strSelectedAirBusCode <> ""  or txtAirBusCode.Enabled = False Then
            If Not strPOH = "" And Not strLoc = "" And Not strNearLoc = "" Then
                lblErrFill.Visible =  True
                Exit Sub
            ElseIf strPOH = "" AND strLoc = "" And strNearLoc = "" Then
                lblErrFill.Visible =  True
                Exit Sub
            End If            
        Else
            If strPOH = "" Then
                lblErrPOHCode.Visible = True
                Exit Sub
            ElseIf strLoc = "" Then
                lblErrLocation.Visible = True
                Exit Sub
            End If
        End If
        If txtAirBusCode.Enabled = True Then
            ValidateADCode()
        End If
        If Trim(ddlLocation.SelectedItem.Value) = ""  Then
            strLoc = strNearLoc
        End If
        If Trim(ddlPOHCode.SelectedItem.Value) = ""  Then
             strPOH = strNearLoc
        End If
        ValidateLine(strPOH,strLoc)

        If ddlADCode.SelectedItem.Value = "" Then
            lblErrADCode.Visible = True
            Exit Sub
        ElseIf ddlType.SelectedItem.Value = "0" Then
            lblErrType.Visible = True
            Exit Sub
        ElseIf ddlCategory.SelectedItem.Value = "" Then
            lblErrCategory.Visible = True
            Exit Sub
        ElseIf lblErrExists.Visible = True Then
            Exit Sub     
        ElseIf lblErrDouble.Visible = True Then
            Exit Sub       
        End if   

        If Trim(txtAmount.Text) = "" Then
            intAmount = "0"
        Else
            intAmount = FormatNumber(txtAmount.Text, 0)
        End If

        strSelectedAirBusCode = Trim(txtAirBusCode.text)

        If strSelectedAirBusCode = "" Then
            InsertAirBusRecord()
        End If
        If lblErrDupAirBusCode.Visible = True Then
            Exit Sub
        End If        
        If strSelectedAirBusCode = "" Then
            Exit sub
        Else
        



                strParam = strSelectedAirBusCode & "|" & _
                       strLoc & "|" & _
                       strPOH & "|" & _
                       Trim(ddlType.SelectedItem.Value) & "|" & _ 
                       Trim(ddlCategory.SelectedItem.Value) & "|" & intAmount
                Try
                    intErrNo = objPRSetup.mtdUpdAirBusLine(strOpCd_Add, _
                                                    strOpCdLn_Add, _ 
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True, _
                                                    objResult)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AIRBUSTICKET_LINE_ADD&errmesg=" & lblErrMesage.Text & "&redirect=PR/trx/PR_Setup_AirBusDet.aspx")
                End Try
        End If

        If strSelectedAirBusCode <> "" Then
            onLoad_Display()
            onLoad_LineDisplay()
        End If
    End Sub

    Sub DEDR_DeleteAirBus(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim objDel As New Object()
        Dim strOpCode_DelLine As String = "PR_CLSSETUP_AIRBUSTICKET_LINE_DEL"
        Dim strOpCode_UpdID As String = "PR_CLSSETUP_AIRBUSTICKET_STATUS_UPD"
        Dim strParamTemp As String
        Dim lblDelText As Label
        Dim strdgTicketCode As String
        Dim strdgType As String
        Dim strdgCategory As String
        Dim strTotalAmt As String
        Dim strProgressiveInd As String
        Dim strPickerType As String        
        Dim intErrNo As Integer
        Dim strPOH as String 
        Dim strLoc as String 

        dgAirBusTicket.EditItemIndex = CInt(E.Item.ItemIndex)
        strdgTicketCode = txtAirBusCode.Text 

        lblDelText = dgAirBusTicket.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lbldgType")
        if lblDelText.Text = "Air Fare" then
            lblDelText.Text = "1"
        else 
            lblDelText.Text = "2"
        end if
        strdgType =  lblDelText.Text

        lblDelText = dgAirBusTicket.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lbldgCategory")
        if lblDelText.Text = "Adult" then
            lblDelText.Text = objPRSetup.EnumAirBusCategory.Adult
        end if 
        if lblDelText.Text = "Child" then
            lblDelText.Text = objPRSetup.EnumAirBusCategory.Child
        end if 
        if lblDelText.Text = "Infant" then
            lblDelText.Text = objPRSetup.EnumAirBusCategory.Infant
        end if 
        strdgCategory =  lblDelText.Text

        lblDelText = dgAirBusTicket.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lbldgPOHCode")
        strPOH = Trim(lblDelText.text)

        lblDelText = dgAirBusTicket.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lbldgLocation")
        strLoc = Trim(lblDelText.text)

        lblDelText = dgAirBusTicket.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lbldgAmount")
        strTotalAmt = lblDelText.Text
        
        strParamTemp =  strdgTicketCode & "|" & strLoc & "|" & strPOH & "|" & strdgType & "|" & strdgCategory & "|" & strTotalAmt & "|" & "1"
        
        Try
            intErrNo = objPRSetup.mtdUpdAirBusLine(strOpCode_UpdID, _
                                            strOpCode_DelLine, _ 
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParamTemp, _
                                            False, _
                                            objDel)
       
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_TICKETCODE_LINE_DEL&errmesg=" & lblErrMesage.Text & "&redirect=PR/trx/PR_SETUP_AIRBUSDET.aspx")
        End Try        

            onLoad_LineDisplay()
        tblSelection.Visible = True
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_AirBusList.aspx")
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_AIRBUSTICKET_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_AIRBUSTICKET_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_AIRBUSTICKET_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_AIRBUSTICKET_STATUS_UPD"
        Dim strOpCd As String
        Dim strCmdArgs As String = Trim(CType(Sender, ImageButton).CommandArgument)
        Dim intIncType As Integer
        Dim intIncAmt As Integer
        Dim intErrNo As Integer
        Dim strParam As String = ""

        If strSelectedAirBusCode = "" or strCmdArgs = "UnDel"  Then
            ValidateADCode()
        End If
        If Trim(ddlPOHCode.SelectedItem.Value) = "" Then
            lblErrPOHCode.Visible = True            
            Exit Sub
        ElseIf Trim(ddlLocation.SelectedItem.Value) = "" Then
            lblErrLocation.Visible = True
            Exit Sub
        ElseIf Trim(ddlADCode.SelectedItem.Value) = "" Then
            lblErrADCode.Visible = True
            Exit Sub
        ElseIf lblErrExists.Visible = True Then
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                InsertAirBusRecord()   
                If lblErrDupAirBusCode.Visible = True Then
                    Exit Sub
                End If             
            ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedAirBusCode & "|||||" & objPRSetup.EnumAirBusStatus.Deleted & "|"
                Try
                    intErrNo = objPRSetup.mtdUpdAirBus(strOpCd_Sts, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strParam, _
                                                          True)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AIRBUS_DEL&errmesg=" & lblErrMesage.Text & "&redirect=hr/setup/PR_setup_HarvIncDet.aspx?hicode=" & strSelectedAirBusCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = strSelectedAirBusCode & "|||||" & objPRSetup.EnumAirBusStatus.Active & "|"
                Try
                    intErrNo = objPRSetup.mtdUpdAirBus(strOpCd_Sts, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        True)
                Catch Exp As Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AIRBUS_UNDEL&errmesg=" & lblErrMesage.Text & "&redirect=hr/setup/PR_setup_HarvIncDet.aspx?hicode=" & strSelectedAirBusCode)
                End Try
            End If
            strSelectedAirBusCode = Trim(txtAirBusCode.Text)
            If strSelectedAirBusCode <> "" Then
                onLoad_Display()
                onLoad_LineDisplay()
            End If
        End If
        
    End Sub

    
    Sub onload_GetLangCap()
        GetEntireLangCap()     
        lblTitle.Text = Ucase(GetCaption(objLangCap.EnumLangCap.AirBusTicket)) & " "
        lblTitle1.Text = GetCaption(objLangCap.EnumLangCap.AirBusTicket) & " "
        lblPOH.Text = GetCaption(objLangCap.EnumLangCap.POHCode) & " "
        lblLoc.Text = GetCaption(objLangCap.EnumLangCap.Location) & " "
        lblNearLoc.Text = GetCaption(objLangCap.EnumLangCap.NearestLocation) & " "   
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 "", _
                                                 "", _
                                                 strUserId, _
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=MENU_PRSETUP_LANGCAP&errmesg=&redirect=")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objSysLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function  

    Sub onLoad_BindButton()
       
        lblCode.Visible = False 
        lblErrSelect.Visible = False 
        lblSelect.Visible = False 
        lblDefault.Visible = False 
        lblHiddenSts.Visible = False 
        lblErrDupAirBusCode.Visible = False 
        lblStatus.Visible = False 
        lblDateCreated.Visible = False
        lblErrPOHCode.Visible = False 
        lblLastUpdate.Visible = False 
        lblErrLocation.Visible = False 
        lblUpdatedBy.Visible = False 
        lblErrADCode.Visible = False 
        lblErrType.Visible = False 
        lblErrCategory.Visible = False 

        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False
        btnFind1.Disabled = False

        Select Case intStatus
            Case objPRSetup.EnumAirBusStatus.Active                
                txtAirBusCode.Enabled = False
                ddlPOHCode.Enabled = True
                ddlLocation.Enabled = True
                ddlNearLocation.Enabled = True

                lblStatus.Visible = True 
                lblDateCreated.Visible = True               
                lblLastUpdate.Visible = True                 
                lblUpdatedBy.Visible = True               
               
                txtDesc.Enabled = True
                ddlType.Enabled = True
                ddlCategory.Enabled = True
                btnAdd.Enabled = True
                txtAmount.Enabled = True
                ddlADCode.Enabled = True

                SaveBtn.Visible = True
                DelBtn.Visible = True
                UnDelBtn.Visible = False

                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPRSetup.EnumAirBusStatus.Deleted
                btnFind1.Disabled = True
                UnDelBtn.Visible = True
                DelBtn.Visible = False
                
                txtAirBusCode.Enabled = False
                ddlPOHCode.Enabled = False
                ddlLocation.Enabled = False
                ddlNearLocation.Enabled = False
                txtDesc.Enabled = False
                ddlType.Enabled = False
                ddlCategory.Enabled = False
                btnAdd.Enabled = False
                txtAmount.Enabled = False
                ddlADCode.Enabled = False
            Case Else                
                
                lblStatus.Visible = True 
                lblDateCreated.Visible = True             
                lblLastUpdate.Visible = True                
                lblUpdatedBy.Visible = True               
             
                SaveBtn.Visible = True
                UnDelBtn.Visible = False
                DelBtn.Visible = False
        End Select
    End Sub   
   
    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_AIRBUSTICKET_GET"
        Dim strOpCd_POH As String = "HR_CLSSETUP_POH_GET"
        Dim strOpCode_Loc As String = "ADMIN_CLSLOC_COMPANY_LOCATION_LIST_GET"
        Dim strOpCd_AD As String = "PR_CLSSETUP_ADCODE_GET"
        Dim strParam As String = strSelectedAirBusCode        
        Dim intErrNo As Integer
        Dim intIndex As Integer
        Dim intCnt As Integer
        Dim strParamTemp As String
        Dim searchStr As String
        Dim sortitem As String
        Dim dr as DataRow
        Dim lbButton As LinkButton
        Dim strPOHCode As String
        Dim strADCode As String
        Dim strLocCode As String
        Dim strNearLocCode As String  
        
        strParam = "TicketCode = '" & strParam & "' "

        Try
            intErrNo = objPRSetup.mtdGetAirBus(strOpCd, _
                                           strParam, _
                                           objAirBusDs, _
                                           True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AIRBUS_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objAirBusDs.Tables(0).Rows.Count > 0 Then
            txtAirBusCode.Text = objAirBusDs.Tables(0).Rows(0).Item("TicketCode").Trim()
            txtDesc.Text = objAirBusDs.Tables(0).Rows(0).Item("TicketDesc").Trim()
            lblStatus.Text = objPRSetup.mtdGetAirBusStatus(objAirBusDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objAirBusDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objAirBusDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objAirBusDs.Tables(0).Rows(0).Item("UserName")
            strPOHCode = Trim(objAirBusDs.Tables(0).Rows(0).Item("POHCode"))
            strLocCode = Trim(objAirBusDs.Tables(0).Rows(0).Item("Location"))
            strADCode = Trim(objAirBusDs.Tables(0).Rows(0).Item("ADCode"))
            strNearLocCode = Trim(objAirBusDs.Tables(0).Rows(0).Item("NearLocCode"))

            If lblStatus.Text ="Active" Then
                intStatus = objPRSetup.EnumAirBusStatus.Active
                lblHiddenSts.Text = intStatus
            Else
                intStatus = objPRSetup.EnumAirBusStatus.Deleted
                lblHiddenSts.Text = intStatus
            End If        
                       
            onLoad_BindPOH(strPOHCode)          
            onLoad_BindLocation(strLocCode)  
            onLoad_BindNearLocation(strNearLocCode)     
            onLoad_BindADCode(strADCode)
            onload_BindAirBusCategory("")
            onLoad_BindButton()
            If strSelectedAirBusCode = "" Then
                tblSelection.Visible = False
            Else
                tblSelection.Visible = True
            End If
        end if
        
    End Sub

    Sub onLoad_BindPOH(ByVal pv_strPOH As String)
        Dim strOpCd As String = "HR_CLSSETUP_POH_GET"
        Dim strParam As String = "Order By a.POHCode ASC|a.Status = '1'"
        Dim dr As DataRow
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0


        Try
            intErrNo = objHRSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objHRSetup.EnumHRMasterType.POHCode, _
                                                   objAirBusDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_POH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAirBusDs.Tables(0).Rows.Count - 1
            objAirBusDs.Tables(0).Rows(intCnt).Item("POHCode") = Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("POHCode"))
            objAirBusDs.Tables(0).Rows(intCnt).Item("POHDesc") = Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("POHCode")) & " (" & Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("POHDesc")) & ")"
            If objAirBusDs.Tables(0).Rows(intCnt).Item("POHCode") = Trim(pv_strPOH) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objAirBusDs.Tables(0).NewRow()
        dr("POHCode") = ""
        dr("POHDesc") = "Select " & lblPOH.Text & " Code"
        objAirBusDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlPOHCode.DataSource = objAirBusDs.Tables(0)
        ddlPOHCode.DataValueField = "POHCode"
        ddlPOHCode.DataTextField = "POHDesc"
        ddlPOHCode.DataBind()
        ddlPOHCode.SelectedIndex = intSelectedIndex

    End Sub

    Sub onLoad_BindLocation(ByVal pv_strLoc As String)
        Dim strOpCode_Loc As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim dr As DataRow
        Dim strParamTemp As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0


        strParamTemp =  "||1||LocCode||" 

        Try
            intErrNo = objSysLoc.mtdGetLocCode(strOpCode_Loc, strParamTemp, objLocDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AIRBUS_GET_LOCATION&errmesg=" & lblErrMesage.Text & "&redirect=PR/setup/PR_SETUP_AIRBUSDET.aspx")
        End Try


        If objLocDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
                objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("LocCode"))
                objLocDs.Tables(0).Rows(intCnt).Item("Description") = objLocDs.Tables(0).Rows(intCnt).Item("_Description") 
                If objLocDs.Tables(0).Rows(intCnt).Item("LocCode") = Trim(pv_strLoc) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next intCnt
        End If

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "Select " & lblLoc.Text & " Code"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocation.DataSource = objLocDs.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex

    End Sub

    Sub onLoad_BindADCode(ByVal pv_strAD As String)
        Dim strOpCode_Loc As String = "PR_CLSSETUP_AD_SEARCH"
        Dim dr As DataRow
        Dim strParamTemp As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strADCode As String       
        Dim strDesc As String        
        Dim strStatus As String
        Dim strLastUpdate As String 
        Dim strSort as String = "AD.ADCode"
        Dim strSortCol as String = ""
      
        strADCode = ""
        strDesc = ""
        strStatus = objPRSetup.EnumADStatus.Active & "' AND AD.AirBusInd like '1"
        strLastUpdate = ""
        strParamTemp = strADCode & "|" & _
                   strDesc & "|" & _
                   strStatus & "|" & _
                   strLastUpdate & "|" & _
                   strSort & "|" & _
                   strSortCol & "|"

        Try
            intErrNo = objPRSetup.mtdGetAD(strOpCode_Loc, strParamTemp, objADDs, False)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AIRBUS_GET_LOCATION&errmesg=" & lblErrMesage.Text & "&redirect=PR/setup/PR_SETUP_AIRBUSDET.aspx")
        End Try



        If objADDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objADDs.Tables(0).Rows.Count - 1
                objADDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(objADDs.Tables(0).Rows(intCnt).Item("ADCode"))
                objADDs.Tables(0).Rows(intCnt).Item("Description") = objADDs.Tables(0).Rows(intCnt).Item("_Description") 
                If objADDs.Tables(0).Rows(intCnt).Item("ADCode") = Trim(pv_strAD) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next intCnt
        End If

        dr = objADDs.Tables(0).NewRow()
        dr("ADCode") = ""
        dr("Description") = "Select Allowance & Deduction Code"
        objADDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlADCode.DataSource = objADDs.Tables(0)
        ddlADCode.DataValueField = "ADCode"
        ddlADCode.DataTextField = "Description"
        ddlADCode.DataBind()
        ddlADCode.SelectedIndex = intSelectedIndex

    End Sub
   
    Sub onLoad_LineDisplay()
        Dim strOpCd As String = "PR_CLSSETUP_AIRBUSTICKET_LN_GET"
        Dim strParam As String = strSelectedAirBusCode
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim dblTotalAmt As Double

        Try
            intErrNo = objPRSetup.mtdGetAirBus(strOpCd, _
                                               strParam, _
                                               objAirBusDs, _
                                               True)


        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AIRBUS_LINE_GET&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        btnAdd.Visible = True


        If  objAirBusDs.Tables(0).Rows.Count > 0 Then
            objAirBusDs.Tables(0).Rows(0).Item("POHCode") = Trim(objAirBusDs.Tables(0).Rows(0).Item("POHCode"))
            objAirBusDs.Tables(0).Rows(0).Item("Location") = Trim(objAirBusDs.Tables(0).Rows(0).Item("Location"))
            objAirBusDs.Tables(0).Rows(0).Item("Type") = Trim(objAirBusDs.Tables(0).Rows(0).Item("Type"))
            objAirBusDs.Tables(0).Rows(0).Item("Category") = Trim(objAirBusDs.Tables(0).Rows(0).Item("Category"))
            objAirBusDs.Tables(0).Rows(0).Item("Amount") = Trim(objAirBusDs.Tables(0).Rows(0).Item("Amount"))

        End If

        For intCnt = 0 To objAirBusDs.Tables(0).Rows.Count - 1
            objAirBusDs.Tables(0).Rows(intCnt).Item("Type") = Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("Type")) 
            if Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("Type")) = "1" then 
                objAirBusDs.Tables(0).Rows(intCnt).Item("Type") = "Air Fare"
            else 
                objAirBusDs.Tables(0).Rows(intCnt).Item("Type") = "Bus Ticket"
            end if
        Next

        For intCnt = 0 To objAirBusDs.Tables(0).Rows.Count - 1
            objAirBusDs.Tables(0).Rows(intCnt).Item("Category") = Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("Category")) 
            if Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("Category")) = "1" then 
                objAirBusDs.Tables(0).Rows(intCnt).Item("Category") = "Adult"
            end if
            if Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("Category")) = "2" then 
                objAirBusDs.Tables(0).Rows(intCnt).Item("Category") = "Child"
            end if
            if Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("Category")) = "3" then 
                objAirBusDs.Tables(0).Rows(intCnt).Item("Category") = "Infant"
            end if
        Next
       
        GetEntireLangCap() 
        dgAirBusTicket.Columns(0).HeaderText = GetCaption(objLangCap.EnumLangCap.POHCode) & "/" &  GetCaption(objLangCap.EnumLangCap.NearestLocation) 
        dgAirBusTicket.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.Location) & "/" &  GetCaption(objLangCap.EnumLangCap.NearestLocation) 

        dgAirBusTicket.DataSource = objAirBusDs.Tables(0)
        dgAirBusTicket.DataBind()
             
        For intCnt=0 To dgAirBusTicket.Items.Count - 1
            lbButton = dgAirBusTicket.Items.Item(intCnt).FindControl("lbDelete")
           Select Case intStatus
            Case objPRSetup.EnumAirBusStatus.Active                    
                    lbButton.visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPRSetup.EnumAirBusStatus.Deleted
                    lbButton.visible = False
            End Select
        Next

        For intCnt = 0 To objAirBusDs.Tables(0).Rows.Count - 1
            objAirBusDs.Tables(0).Rows(intCnt).Item("Amount") = Trim(objAirBusDs.Tables(0).Rows(intCnt).Item("Amount"))
            dblTotalAmt = dblTotalAmt + objAirBusDs.Tables(0).Rows(intCnt).Item("Amount")
        Next

        lblTotalAmount.Text = ObjGlobal.GetIDDecimalSeparator(FormatNumber(dblTotalAmt, 2))
        ddlType.SelectedIndex = -1
        ddlCategory.SelectedIndex = -1
        txtAmount.Text = ""
    End Sub

    Sub InsertAirBusRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_AIRBUSTICKET_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_AIRBUSTICKET_UPD"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_AIRBUSTICKET_STATUS_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_AIRBUSTICKET_GET"
        dim strOpCd As String
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""
        Dim intIncType As Integer
        Dim intPickerType As Integer
               

        strParam = "TICKETCODE = '" & Trim(txtAirBusCode.Text) & "'"
        Try
            intErrNo = objPRSetup.mtdGetAirBus(strOpCd_Get, _
                                                      strParam, _
                                                      objAirBusDs, _
                                                      True)
            
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AIRBUSTICKET_GET&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try
        
        If objAirBusDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
            lblErrDupAirBusCode.Visible = True
            Exit Sub
        Else               
            strOpCd = IIF(intStatus = 0, strOpCd_Add,strOpCd_Upd) 
            blnIsUpdate = IIF(intStatus = 0, False, True)          
            strSelectedAirBusCode = Trim(txtAirBusCode.Text)            
                       
            strParam = Trim(txtAirBusCode.Text) & "|" & _
                        Trim(txtDesc.Text) & "|" & _
                        Trim(ddlPOHCode.SelectedItem.Value) & "|" & _
                        Trim(ddlLocation.SelectedItem.Value) & "|" & _ 
                        Trim(ddlADCode.SelectedItem.Value) & "|" & _ 
                        objPRSetup.EnumAirBusStatus.Active & "|" & _
                        Trim(ddlNearLocation.SelectedItem.Value)
            Try
                intErrNo = objPRSetup.mtdUpdAirBus(strOpCd, _
                                                          strCompany, _
                                                          strLocation, _
                                                          strUserId, _
                                                          strParam, _
                                                          blnIsUpdate)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AIRBUSTICKET_ADD&errmesg=" & lblErrMesage.Text & "&redirect=hr/setup/PR_setup_AirBusDet.aspx?TICKETCODE=" & strSelectedAirBusCode)
            End Try           
        End If
    End Sub    

    Sub onload_BindAirBusCategory(ByVal pv_strAirBusCategory As String)
        ddlType.AutoPostBack = True
        If ddlType.SelectedItem.Value = "2" Then
            ddlCategory.Items.Clear()
            ddlCategory.Items.Add(New ListItem("Select Category", ""))
            ddlCategory.Items.Add(New ListItem(objPRSetup.mtdGetAirBusCategory(objPRSetup.EnumAirBusCategory.Adult), objPRSetup.EnumAirBusCategory.Adult))
        Else
            ddlCategory.Items.Clear()
            ddlCategory.Items.Add(New ListItem("Select Category", ""))
            ddlCategory.Items.Add(New ListItem(objPRSetup.mtdGetAirBusCategory(objPRSetup.EnumAirBusCategory.Adult), objPRSetup.EnumAirBusCategory.Adult))
            ddlCategory.Items.Add(New ListItem(objPRSetup.mtdGetAirBusCategory(objPRSetup.EnumAirBusCategory.Child), objPRSetup.EnumAirBusCategory.Child))
            ddlCategory.Items.Add(New ListItem(objPRSetup.mtdGetAirBusCategory(objPRSetup.EnumAirBusCategory.Infant), objPRSetup.EnumAirBusCategory.Infant))
        End If
        
        If Trim(pv_strAirBusCategory) = "" Then           
            ddlCategory.SelectedValue = ""
        Else
            ddlCategory.SelectedValue = CInt(Trim(pv_strAirBusCategory))
        End If
    End Sub
    Sub ValidateADCode() 
        Dim strOpCd_GET As String = "PR_CLSSETUP_AIRBUSTICKET_GET"
       
        Dim strSort As String = ""
        Dim strSrchStatus As String
        Dim strSortExp As String = "TicketCode"
        Dim strSrchPOHCode As String 
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objADDs As New DataSet

        strSrchStatus = objPRsetup.EnumAirBusStatus.Active
        strSrchPOHCode = ddlPOHCode.SelectedItem.Value.ToString & "' AND ADCode='" & ddlADCode.SelectedItem.Value.ToString & _
                        "' AND Location='" & ddlLocation.SelectedItem.Value.ToString
       
        strParam =   "||" & _                     
                   strSrchPOHCode & "|" & _
                   strSrchStatus & "||" & _ 
                   strSortExp & "|" & _
                   strSort & "|"
        Try
           intErrNo = objPRSetup.mtdGetAirBus(strOpCd_GET, strParam, objADDs, False)

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_AD_GET&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        If objADDs.Tables(0).Rows.Count > 0 Then            
             lblErrExists.Visible = True           
        End If
    End Sub   
    Sub ValidateLine(ByVal pv_strPOH As String,ByVal pv_strLoc As String) 
        Dim strOpCd As String = "PR_CLSSETUP_AIRBUSTICKET_LN_GET"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim dblTotalAmt As Double
        Dim objDs As New DataSet

        strParam = Trim(txtAirBusCode.text) & "' AND ln.Location='" & pv_strLoc & _
                    "' AND ln.POHCode='" & pv_strPOH & "' AND ln.Type='" & ddlType.SelectedItem.Value.ToString & _
                    "' AND ln.Category='" & ddlCategory.SelectedItem.Value.ToString
    
        Try
            intErrNo = objPRSetup.mtdGetAirBus(strOpCd, _
                                               strParam, _
                                               objDs, _
                                               True)


        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_AIRBUS_LINE_GET&errmesg=" & lblErrMesage.Text & "&redirect=")
        End Try

        If objDs.Tables(0).Rows.Count > 0 Then            
             lblErrDouble.Visible = True          
        End If
    End Sub   
    Sub onLoad_BindNearLocation(ByVal pv_strNearLoc As String)
        Dim strOpCode_Loc As String = "ADMIN_CLSLOC_NEARESTLOC_LIST_GET"
        Dim dr As DataRow
        Dim strParamTemp As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0


        strParamTemp =  "||1||NearLocCode||" 

        Try
            intErrNo = objSysLoc.mtdGetNearLocCode(strOpCode_Loc, strParamTemp, objLocDs)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=AIRBUS_GET_LOCATION&errmesg=" & lblErrMesage.Text & "&redirect=PR/setup/PR_SETUP_AIRBUSDET.aspx")
        End Try


        If objLocDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
                objLocDs.Tables(0).Rows(intCnt).Item("NearLocCode") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("NearLocCode"))
                objLocDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objLocDs.Tables(0).Rows(intCnt).Item("NearLocCode")) & " (" & objLocDs.Tables(0).Rows(intCnt).Item("Description") & ")"
                If objLocDs.Tables(0).Rows(intCnt).Item("NearLocCode") = Trim(pv_strNearLoc) Then
                    intSelectedIndex = intCnt + 1
                End If
            Next intCnt
        End If

        dr = objLocDs.Tables(0).NewRow()
        dr("NearLocCode") = ""
        dr("Description") = "Select " & lblNearLoc.Text & " Code"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlNearLocation.DataSource = objLocDs.Tables(0)
        ddlNearLocation.DataValueField = "NearLocCode"
        ddlNearLocation.DataTextField = "Description"
        ddlNearLocation.DataBind()
        ddlNearLocation.SelectedIndex = intSelectedIndex

    End Sub
    
    
End Class
