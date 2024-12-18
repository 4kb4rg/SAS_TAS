
Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.WS.clsSetup
Imports agri.GlobalHdl.clsGlobalHdl


Public Class WS_ServTypeDet : Inherits Page

    Protected WithEvents txtServType As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlAccCode As DropDownList
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents dgServTypeDet As DataGrid
    Protected WithEvents ddlWorkCode As DropDownList
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblChrgRate As Label
    Protected WithEvents Addbtn As Button
    Protected WithEvents servtype As HtmlInputHidden
    Protected WithEvents lblErrBlankST As Label
    Protected WithEvents lblErrDupST As Label
    Protected WithEvents lblErrDupWC As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblAccCode As Label
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim dsLangCap As New DataSet

    Protected objWS As New agri.WS.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objLoc As New agri.Admin.clsLoc()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intWSAR As Integer
    Dim strLocType as String


    Dim objServTypeDs As New Object()
    Dim strSelectedServType As String = ""
    Dim strSortExpression As String = "ServTypeCode"

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")
        
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkshopService), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            strSelectedServType = Trim(IIf(Request.QueryString("servtypecode") <> "", Request.QueryString("servtypecode"), Request.Form("servtype")))

            If Not IsPostBack Then
                InitializeLangCap()
                If strSelectedServType <> "" Then
                    servtype.Value = strSelectedServType
                    AddBtn.Visible = True
                    onLoad_Display()
                    onLoad_BindButton()
                Else
                    AddBtn.Visible = False
                    newLoad_Display()
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onLoad_Display()
        Dim strOpCd_GetServType As String = "WS_CLSSETUP_SERVTYPE_LIST_GET"
        Dim strOpCd_GetAccCode As String = "GL_GLACCOUNT_GET"
        Dim objGLAccountDs As New Object()
        Dim strParam As String = ""
        Dim strSelectedAccCode As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim lbButton As LinkButton 
       
        strParam = strSelectedServType & "||||" & strSortExpression & "||" 

        Try
            intErrNo = objWS.mtdGetServType(strOpCd_GetServType, strParam, objServTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_GET_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objServTypeDs.Tables(0).Rows.Count > 0 Then
            objServTypeDs.Tables(0).Rows(0).Item(0) = Trim(objServTypeDs.Tables(0).Rows(0).Item(0))
            objServTypeDs.Tables(0).Rows(0).Item(1) = Trim(objServTypeDs.Tables(0).Rows(0).Item(1))
            objServTypeDs.Tables(0).Rows(0).Item(2) = Trim(objServTypeDs.Tables(0).Rows(0).Item(2))
            objServTypeDs.Tables(0).Rows(0).Item(3) = Trim(objServTypeDs.Tables(0).Rows(0).Item(3))
            objServTypeDs.Tables(0).Rows(0).Item(4) = Trim(objServTypeDs.Tables(0).Rows(0).Item(4))
            objServTypeDs.Tables(0).Rows(0).Item(5) = Trim(objServTypeDs.Tables(0).Rows(0).Item(5))
            objServTypeDs.Tables(0).Rows(0).Item(6) = Trim(objServTypeDs.Tables(0).Rows(0).Item(6))
            objServTypeDs.Tables(0).Rows(0).Item(7) = Trim(objServTypeDs.Tables(0).Rows(0).Item(7))

            txtServType.Text = objServTypeDs.Tables(0).Rows(0).Item(0)
            txtDescription.Text = objServTypeDs.Tables(0).Rows(0).Item(1)
            strSelectedAccCode = objServTypeDs.Tables(0).Rows(0).Item(2)
            lblStatus.Text = objWS.mtdGetStatus(objServTypeDs.Tables(0).Rows(0).Item(3))
            lblDateCreated.Text = objGlobal.GetLongDate(objServTypeDs.Tables(0).Rows(0).Item(4))
            lblLastUpdate.Text = objGlobal.GetLongDate(objServTypeDs.Tables(0).Rows(0).Item(5))
            lblUpdatedBy.Text = objServTypeDs.Tables(0).Rows(0).Item(7)
 
        End If

        strParam = "||AccCode|"

        Try
            intErrNo = objWS.mtdGetAccCode(strOpCd_GetAccCode, strParam, objGLAccountDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_GET_GLACCOUNT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objGlAccountDs.Tables(0).Rows.Count - 1
            objGlAccountDs.Tables(0).Rows(intCnt).Item(0) = Trim(objGlAccountDs.Tables(0).Rows(intCnt).Item(0))
            objGlAccountDs.Tables(0).Rows(intCnt).Item(1) = Trim(objGlAccountDs.Tables(0).Rows(intCnt).Item(0)) & " (" & Trim(objGlAccountDs.Tables(0).Rows(intCnt).Item(1)) & ")"

            If objGlAccountDs.Tables(0).Rows(intCnt).Item(0) = strSelectedAccCode Then
                intSelectedIndex = intCnt
            End If
        Next
        ddlAccCode.DataSource = objGLAccountDs.Tables(0)
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataBind()
        ddlAccCode.SelectedIndex = intSelectedIndex

        dgServTypeDet.DataSource = LoadData
        dgServTypeDet.DataBind()
        
        For intCnt = 0 To dgServTypeDet.Items.Count - 1
            Select case Trim(lblStatus.Text)
                case objWS.mtdGetStatus(objWS.EnumStatus.Active)
                    lbButton = dgServTypeDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = true
                    lbButton.attributes("onclick") = "javascript:return ConfirmAction('delete');"
                case else
                    lbButton = dgServTypeDet.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.visible = false
            End Select
        Next
                
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GetServTypeDet As String = "WS_CLSSETUP_SERVTYPE_DET_GET"
        Dim objServTypeDetDs As New Object()
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSelectedServType = txtServType.Text
        strParam = strSelectedServType & "||||" & strSortExpression & "||" 
    
        Try
            intErrNo = objWS.mtdGetServTypeDet(strOpCd_GetServTypeDet, strParam, objServTypeDetDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_GET_SERVTYPEDET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
                                         
        For intCnt = 0 To objServTypeDetDs.Tables(0).Rows.Count - 1
            objServTypeDetDs.Tables(0).Rows(intCnt).Item(0) = Trim(objServTypeDetDs.Tables(0).Rows(intCnt).Item(0))
            objServTypeDetDs.Tables(0).Rows(intCnt).Item(1) = Trim(objServTypeDetDs.Tables(0).Rows(intCnt).Item(1))
            objServTypeDetDs.Tables(0).Rows(intCnt).Item(2) = Trim(objServTypeDetDs.Tables(0).Rows(intCnt).Item(2)) 
        Next

        Return objServTypeDetDs
    End Function

    Sub onLoad_BindButton()
        txtServType.enabled = False
        txtDescription.enabled = False
        ddlAccCode.Enabled = False
        UnDelBtn.visible = False
        DelBtn.visible = False

        Select case Trim(lblStatus.Text)
            case objWS.mtdGetStatus(objWS.EnumStatus.Active)
                txtServType.Enabled = False
                txtDescription.Enabled = True
                ddlAccCode.Enabled = True
                DelBtn.visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            case objWS.mtdGetStatus(objWS.EnumStatus.Deleted)
                txtServType.Enabled = False
                txtDescription.Enabled = False
                ddlAccCode.Enabled = False
                UnDelBtn.visible = True
            case else
                txtServType.Enabled = True
                txtDescription.Enabled = True
                ddlAccCode.Enabled = True
        End Select
    End Sub

    Sub newLoad_Display()
        Dim strOpCd_GetAccCode As String = "GL_GLACCOUNT_GET"
        Dim objGLAccountDs As New Object()
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim intCnt As Integer
           
        strParam = "||AccCode|"

        Try
            intErrNo = objWS.mtdGetAccCode(strOpCd_GetAccCode, strParam, objGLAccountDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_GET_GLACCOUNT&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        
        For intCnt = 0 To objGlAccountDs.Tables(0).Rows.Count - 1
            objGlAccountDs.Tables(0).Rows(intCnt).Item(0) = Trim(objGlAccountDs.Tables(0).Rows(intCnt).Item(0))
            objGlAccountDs.Tables(0).Rows(intCnt).Item(1) = Trim(objGlAccountDs.Tables(0).Rows(intCnt).Item(0)) & " (" & Trim(objGlAccountDs.Tables(0).Rows(intCnt).Item(1)) & ")"
        Next

        ddlAccCode.DataSource = objGLAccountDs.Tables(0)
        ddlAccCode.DataTextField = "Description"
        ddlAccCode.DataValueField = "AccCode"
        ddlAccCode.DataBind()
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "WS_CLSSETUP_SERVTYPE_LIST_ADD"
        Dim strOpCd_Upd As String = "WS_CLSSETUP_SERVTYPE_LIST_UPD"
        Dim strOpCd_Get As String = "WS_CLSSETUP_SERVTYPE_LIST_GET"
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strDescription As String
        Dim strAccCode As String
        Dim strStatus As String
        Dim strParam As String = ""

        strSelectedServType = txtServType.Text
        strDescription = txtDescription.Text
        strAccCode = ddlAccCode.SelectedItem.Value.Trim()
        lblErrBlankST.Visible = False
        lblErrDupST.Visible = False

        Select case Trim(lblStatus.Text)
            case objWS.mtdGetStatus(objWS.EnumStatus.Active)
             strStatus = objWS.EnumStatus.Active
            case objWS.mtdGetStatus(objWS.EnumStatus.Deleted)
             strStatus = objWS.EnumStatus.Deleted
        End Select
                
   
        If strCmdArgs = "Save" Then
            If servtype.Value = "" Then
                If strSelectedServType = ""
                    lblErrBlankST.Visible = True
                Else
                    strParam = strSelectedServType & "||||" & strSortExpression & "||Add"

                    Try
                        intErrNo = objWS.mtdGetServType(strOpCd_Get, strParam, objServTypeDs)
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_GET_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
                    End Try

                    If objServTypeDs.Tables(0).Rows.Count <> 0 Then
                        lblErrDupST.Visible = True
                    Else
                        strParam = strSelectedServType & "|" & _
                                   strDescription & "|" & _
                                   strAccCode & "|" & _
                                   objWS.EnumStatus.Active & "|"

                        Try                       
                            intErrNo = objWS.mtdUpdServType(strOpCd_Add, _
                                                            strOpCd_Upd, _
                                                            strCompany, _
                                                            strLocation, _
                                                            strUserId, _
                                                            strParam, _
                                                            False)
                        Catch Exp As System.Exception
                            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_ADD_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
                        End Try
                    End If
                End If
            Else
                strParam = strSelectedServType & "|" & _
                           strDescription & "|" & _
                           strAccCode & "|" & _
                           strStatus & "|"
                           
                Try
                    intErrNo = objWS.mtdUpdServType(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)                                           
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_UPD_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
            End If

            AddBtn.Visible = True
            onLoad_Display()
        
        ElseIf strCmdArgs = "Del" Then
            If servtype.Value <> ""
                strParam = strSelectedServType & "|||" & objWs.EnumStatus.Deleted & "||"

                Try
                    intErrNo = objWS.mtdUpdServType(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    True)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_DEL_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
                End Try
             End If
 
       ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedServType & "|||" & objWs.EnumStatus.Active & "||"

            Try
                intErrNo = objWS.mtdUpdServType(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)                                            
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_UNDEL_SERVTYPE&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

        End If

        If servtype.Value <> ""
            AddBtn.Visible = True
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCd As String = "WS_CLSSETUP_SERVTYPE_DET_DEL"
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim EditText As label
        Dim strSelectedWorkCode As String
 
        EditText = E.Item.FindControl("WorkCode")
        strSelectedWorkCode = EditText.Text
        strSelectedServType = txtServType.Text

        strParam = strSelectedServType & "|" & strSelectedWorkCode

        Try
            intErrNo = objWS.mtdUpdServTypeDet(strOpCd, strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_DEL_SERVTYPELN&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
      
        dgServTypeDet.EditItemIndex = -1

        If strSelectedServType <> ""
            onLoad_Display()
            onLoad_BindButton()
        End If
    End Sub

    Sub DEDR_Update(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCd_GET As String = "WS_CLSSETUP_SERVTYPE_DET_GET"
        Dim strOpCd_ADD As String = "WS_CLSSETUP_SERVTYPE_DET_ADD"
        Dim objServTypeDetDs As New Object()
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim list As Dropdownlist
        Dim strSelectedWorkCode As String

        list = E.Item.FindControl("ddlWorkCode")
        strSelectedWorkCode = list.SelectedItem.Value
        strSelectedServType = txtServType.Text
        strParam = strSelectedServType & "|" & strSelectedWorkCode

        Try
            intErrNo = objWS.mtdGetServTypeDet(strOpCd_GET, strParam, objServTypeDetDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_GET_SERVTYPEDET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
                                         
        IF objServTypeDetDs.Tables(0).Rows.Count <> 0 Then
            lblErrDupWC = E.Item.FindControl("lblErrDupWC")
            lblErrDupWC.Visible = True
        Else
            Try 
                intErrNo = objWS.mtdUpdServTypeDet(strOpCd_ADD, strParam)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_ADD_SERVTYPELN&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try
        
            dgServTypeDet.EditItemIndex = -1
            If strSelectedServType <> ""
                onLoad_Display()
                onLoad_BindButton()
            End If
        End If        
    End Sub

    Sub DEDR_Cancel(Sender As Object, E As DataGridCommandEventArgs)
        If CInt(e.Item.ItemIndex) = 0 and dgServTypeDet.Items.Count = 1 and not dgServTypeDet.CurrentPageIndex = 0 then
            dgServTypeDet.CurrentPageIndex = dgServTypeDet.Pagecount - 2 
        End If

        dgServTypeDet.EditItemIndex = -1
        strSelectedServType = txtServType.Text
        onLoad_Display()
        onLoad_BindButton()
     End Sub


    Sub DEDR_Add(Sender As Object, E As EventArgs)
        Dim DataSet As DataSet = LoadData
        Dim newRow As DataRow
        Dim strOpcd_GetWorkCode As String = "WS_CLSSETUP_WORKCODE_LIST_GET"
        Dim objWorkCodeDs As New DataSet()
        Dim strParam As String = ""
        Dim intErrNo As Integer 
        Dim intCnt As Integer

        newRow = DataSet.Tables(0).NewRow()
        newRow.Item("WorkCode") = ""
        newRow.Item("Description") = ""
        newRow.Item("ChrgRate") = 0
        DataSet.Tables(0).Rows.Add(newRow)
        
        dgServTypeDet.DataSource = DataSet
        dgServTypeDet.DataBind()

        dgServTypeDet.CurrentPageIndex = dgServTypeDet.PageCount - 1
        dgServTypeDet.DataBind()
        dgServTypeDet.EditItemIndex = dgServTypeDet.Items.Count - 1
        dgServTypeDet.DataBind()

        strParam = "||||WorkCode|" 
    
        Try
            intErrNo = objWS.mtdGetWorkCode(strOpCd_GetWorkCode, strParam, objWorkCodeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=SERVTYPEDET_GET_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
                                         
        For intCnt = 0 To objWorkCodeDs.Tables(0).Rows.Count - 1
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(0) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(0))
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(1) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(0)) & " (" & Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(1)) & ")"
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(2) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(2)) 
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(3) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(3)) 
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(4) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(4)) 
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(5) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(5)) 
        Next

        ddlWorkCode = dgServTypeDet.Items.Item(CInt(dgServTypeDet.EditItemIndex)).FindControl("ddlWorkCode")
        ddlWorkCode.DataSource = objWorkCodeDs.Tables(0)
        ddlWorkCode.DataTextField = "Description"
        ddlWorkCode.DataValueField = "WorkCode"
        ddlWorkCode.DataBind()
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("WS_ServTypeList.aspx")
    End Sub
    
    Function GetLanguageCaptionDS() As DataSet
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim dsTemp As DataSet
        Dim strParam As String
        Dim intErrNo As Integer
        
        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 "", _
                                                 "", _
                                                 dsTemp, _
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SERVICE_TYPE_LANGCAP_GET&errmesg=&redirect=")
        End Try
        
        Return dsTemp
        If Not dsTemp Is Nothing Then
            dsTemp = Nothing
        End If
    End Function
    
    Sub InitializeLangCap() 
        dsLangCap = GetLanguageCaptionDS()
        lblAccCode.Text = GetCaption(objLangCap.EnumLangCap.Account)
    End Sub
    
    Function GetCaption(ByVal pv_TermCode As String) As String
        Dim drLangCap() As DataRow
        drLangCap = dsLangCap.Tables(0).Select("TermCode = '" & Replace(pv_TermCode, "'", "''") & "'", "TermCode ASC")
        If drLangCap.Length = 1 Then
            If strLocType = objLoc.EnumLocType.Mill then
                GetCaption = drLangCap(0).Item("BusinessTermMW")
            Else
                GetCaption = drLangCap(0).Item("BusinessTerm")
            End If
        End If
    End Function






End Class
