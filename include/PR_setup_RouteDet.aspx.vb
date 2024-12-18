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

Public Class PR_Setup_RouteDet : Inherits Page
    
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTitle1 As Label
    Protected WithEvents lblTitle2 As Label
    Protected WithEvents lblDesc As Label
    Protected WithEvents lblVehType as Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents txtRouteCode As TextBox
    Protected WithEvents txtDesc As TextBox
    Protected WithEvents txtDistance As TextBox
    Protected WithEvents txtRouteRate As TextBox
    Protected WithEvents txtLoadBasis As TextBox
    Protected WithEvents ddlVehType As DropDownList
    Protected WithEvents ddlDumpType As DropDownList
    Protected WithEvents ddlDefAccCode As DropDownList
    Protected WithEvents ddlDefBlkCode As DropDownList
    Protected WithEvents cbTaxContribute As CheckBox
    Protected WithEvents ddlPaySlip As DropDownList

    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents routecode As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblHiddenSts As Label
    Protected WithEvents lblErrDupRouteCode As Label
    Protected WithEvents lblErrDumpType As Label
    Protected WithEvents lblErrVehType As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrSelect As Label
    Protected WithEvents lblSelect As Label

    Protected WithEvents txtBasisInc As TextBox

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objPRSetup As New agri.PR.clsSetup()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objADDs As New Object()
    Dim objADLnDs As New Object()
    Dim objADGroupDs As New Object()
    Dim objDefAccDs As New Object()
    Dim objDefBlkDs As New Object()
    Dim objUOMDs As New Object()
    Dim objPayADDs As New Object()
    Dim objLocDs As New Object()
    Dim objLangCapDs As New Object()
    dim objVehTypeDs as New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim intConfig As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strSelectedRouteCode As String = ""
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRRoute), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            lblErrDupRouteCode.Visible = False
            lblErrDumpType.Visible = False
            lblErrVehType.Visible = False
            
            strSelectedRouteCode = Trim(IIf(Request.QueryString("routecode") <> "", Request.QueryString("routecode"), Request.Form("routecode")))
            intStatus = CInt(lblHiddenSts.Text)
            If Not IsPostBack Then
                If strSelectedRouteCode <> "" Then
                    routecode.Value = strSelectedRouteCode
                    onLoad_Display()
                Else
                    onLoad_BindDumpType("")
                    onLoad_BindVehType("")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Route))
        lblTitle1.Text = GetCaption(objLangCap.EnumLangCap.Route)
        lblTitle2.Text = GetCaption(objLangCap.EnumLangCap.Route)
        lblDesc.Text = GetCaption(objLangCap.EnumLangCap.RouteDesc)
        lblVehType.Text = GetCaption(objLangCap.EnumLangCap.VehType)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=PR/Setup/PR_setup_ADDet.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


    Sub onLoad_BindButton()
        txtRouteCode.Enabled = False
        txtDesc.Enabled = False
        ddlVehType.Enabled = False
        ddlDumpType.Enabled = False
        txtDistance.Enabled = False
        txtLoadBasis.Enabled = False
        txtRouteRate.Enabled = False
        txtBasisInc.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objPRSetup.EnumRouteStatus.Active
                txtDesc.Enabled = True
                ddlVehType.Enabled = True
                ddlDumpType.Enabled = True
                txtDistance.Enabled = True
                txtLoadBasis.Enabled = True
                txtRouteRate.Enabled = True
                txtBasisInc.Enabled = True
                SaveBtn.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPRSetup.EnumRouteStatus.Deleted
                UnDelBtn.Visible = True
            Case Else
                txtRouteCode.Enabled = True
                txtDesc.Enabled = True
                ddlVehType.Enabled = True
                ddlDumpType.Enabled = True
                txtDistance.Enabled = True
                txtLoadBasis.Enabled = True
                txtRouteRate.Enabled = True
                txtBasisInc.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PR_CLSSETUP_ROUTE_LIST_GET"
        Dim strParam As String 
        Dim intErrNo As Integer        

        strParam = "|and R.RouteCode = '" & strSelectedRouteCode & "'"
        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOpCd, _
                                           strParam, _
                                           objPRSetup.EnumPayrollMasterType.Route, _ 
                                           objADDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_ROUTE_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objADDs.Tables(0).Rows.Count > 0 Then
            txtRouteCode.Text = objADDs.Tables(0).Rows(0).Item("RouteCode").Trim()
            txtDesc.Text = objADDs.Tables(0).Rows(0).Item("Description").Trim()
            intStatus = CInt(objADDs.Tables(0).Rows(0).Item("Status"))
            lblHiddenSts.Text = objADDs.Tables(0).Rows(0).Item("Status").Trim()
            lblStatus.Text = objPRSetup.mtdGetADStatus(objADDs.Tables(0).Rows(0).Item("Status").Trim())
            lblDateCreated.Text = objGlobal.GetLongDate(objADDs.Tables(0).Rows(0).Item("CreateDate"))
            lblLastUpdate.Text = objGlobal.GetLongDate(objADDs.Tables(0).Rows(0).Item("UpdateDate"))
            lblUpdatedBy.Text = objADDs.Tables(0).Rows(0).Item("UserName")
            txtDistance.Text = objADDs.Tables(0).Rows(0).Item("Distance")
            txtRouteRate.Text = objADDs.Tables(0).Rows(0).Item("RouteRate")
            txtLoadBasis.Text = objADDs.Tables(0).Rows(0).Item("LoadBasis")

            txtBasisInc.Text = objADDs.Tables(0).Rows(0).Item("BasisInc")

            onLoad_BindVehType(objADDs.Tables(0).Rows(0).Item("VehicleType").Trim())            
            onLoad_BindDumpType(objADDs.Tables(0).Rows(0).Item("DumpType").Trim())
            onLoad_BindButton()
        End If
    End Sub


    Sub onLoad_BindDumpType(ByVal pv_strDumpType As String)
        ddlDumpType.Items.Clear
        ddlDumpType.Items.Add(New ListItem("Select Dump Type", ""))
        ddlDumpType.Items.Add(New ListItem(objPRSetup.mtdGetDumpType(objPRSetup.EnumDumpType.Tipping), objPRSetup.EnumDumpType.Tipping))
        ddlDumpType.Items.Add(New ListItem(objPRSetup.mtdGetDumpType(objPRSetup.EnumDumpType.NonTipping), objPRSetup.EnumDumpType.NonTipping))
        If Trim(pv_strDumpType) = "" Then
            ddlDumpType.SelectedValue = ""
        Else
            ddlDumpType.SelectedValue = CInt(Trim(pv_strDumpType))
        End If
    End Sub


    Sub InsertRouteRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_ROUTE_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_ROUTE_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_ROUTE_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = False

        If ddlVehType.SelectedItem.Value = "" Then            
            lblErrVehType.Visible = True
            Exit Sub
        ElseIf ddlDumpType.SelectedItem.Value = "" Then
            lblErrDumpType.Visible = True
            Exit Sub
        Else
            strParam = "|" & " AND R.RouteCode like '" & Trim(txtRouteCode.Text) & "%'"
            Try
                intErrNo = objPRSetup.mtdGetMasterList(strOpCd_Get, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Route, _
                                                objADDs)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ROUTE_GET&errmesg=" & Exp.ToString() & "&redirect=pr/setup/HR_setup_ADList.aspx")
            End Try

            If objADDs.Tables(0).Rows.Count > 0 And intStatus = 0 Then
                lblErrDupRouteCode.Visible = True
            Else
                strOpCd = IIf(intStatus = 0, strOpCd_Add, strOpCd_Upd)
                strSelectedRouteCode = Trim(txtRouteCode.Text)
                routecode.Value = strSelectedRouteCode
                strParam = strSelectedRouteCode & "|" & _
                            Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                            ddlVehType.SelectedItem.Value & "|" & _
                            Trim(txtDistance.Text) & "|" & _
                            Trim(txtLoadBasis.Text) & "|" & _
                            Trim(txtRouteRate.Text) & "|" & _                            
                            ddlDumpType.SelectedItem.Value & "|" & _
                            objPRSetup.EnumRouteStatus.Active & "|" & _
                            Trim(txtBasisInc.Text)


                
                Try
                    intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                    strOpCd_Upd, _
                                                    strOpCd_Get, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    objPRSetup.EnumPayrollMasterType.Route, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ROUTE_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/HR_setup_ADList.aspx")
                End Try
            End If
        End If
    End Sub

    Sub UpdateRouteRecord()
        Dim strOpCd_Add As String = "PR_CLSSETUP_ROUTE_LIST_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_ROUTE_LIST_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_ROUTE_LIST_GET"
        Dim strOpCd As String
        Dim blnDupKey As Boolean = False
        Dim intErrNo As Integer
        Dim strParam As String = ""
        blnUpdate.Text = True
        If ddlVehType.SelectedItem.Value = "" Then            
            lblErrVehType.Visible = True
            Exit Sub
        ElseIf ddlDumpType.SelectedItem.Value = "" Then
            lblErrDumpType.Visible = True
            Exit Sub
        Else
            strSelectedRouteCode = Trim(txtRouteCode.Text)
            routecode.Value = strSelectedRouteCode
            strParam = strSelectedRouteCode & "|" & _
                        Trim(Replace(txtDesc.Text, "'", "''")) & "|" & _
                        ddlVehType.SelectedItem.Value & "|" & _
                        Trim(txtDistance.Text) & "|" & _
                        Trim(txtLoadBasis.Text) & "|" & _
                        Trim(txtRouteRate.Text) & "|" & _                            
                        ddlDumpType.SelectedItem.Value & "|" & _
                        objPRSetup.EnumRouteStatus.Active & "|" & _
                        Trim(txtBasisInc.Text)
            
            Try
                intErrNo = objPRSetup.mtdUpdMasterList(strOpCd_Add, _
                                                strOpCd_Upd, _
                                                strOpCd_Get, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                objPRSetup.EnumPayrollMasterType.Route, _
                                                blnDupKey, _
                                                blnUpdate.Text)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ROUTE_SAVE&errmesg=" & Exp.ToString() & "&redirect=pr/setup/HR_setup_ADList.aspx")
            End Try            
        End If
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PR_CLSSETUP_AD_ADD"
        Dim strOpCd_Upd As String = "PR_CLSSETUP_AD_UPD"
        Dim strOpCd_Get As String = "PR_CLSSETUP_AD_GET"
        Dim strOpCd_Sts As String = "PR_CLSSETUP_ROUTE_STATUS_UPD"
        Dim strOpCd As String
        Dim intAccType As Integer
        Dim intAccPurpose As Integer
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim blnIsUpdate As Boolean
        Dim strParam As String = ""

        If ddlVehType.SelectedItem.Value = "" Then
            lblErrVehType.Visible = True
            Exit Sub
        ElseIf ddlDumpType.SelectedItem.Value = "" Then
            lblErrDumpType.Visible = True
            Exit Sub
        Else
            If strCmdArgs = "Save" Then
                Select Case intStatus
                    Case objPRSetup.EnumRouteStatus.Active
                        UpdateRouteRecord()
                    Case Else
                        InsertRouteRecord()
                    End Select
            ElseIf strCmdArgs = "Del" Then
                strParam = strSelectedRouteCode & "|" & objPRSetup.EnumRouteStatus.Deleted
                Try
                    intErrNo = objPRSetup.mtdUpdRoute(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ROUTE_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_ADDet.aspx?adcode=" & strSelectedRouteCode)
                End Try

            ElseIf strCmdArgs = "UnDel" Then
                strParam = strSelectedRouteCode & "|" & objPRSetup.EnumRouteStatus.Active
                Try
                    intErrNo = objPRSetup.mtdUpdRoute(strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strParam)
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_ROUTE_UNDEL&errmesg=" & Exp.ToString() & "&redirect=pr/setup/PR_setup_ADDet.aspx?adcode=" & strSelectedRouteCode)
                End Try
            End If

            If strSelectedRouteCode <> "" Then
                onLoad_Display()
            End If
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_setup_Route.aspx")
    End Sub
    

    Sub onLoad_BindVehType(ByVal pv_strVehType As String)
        Dim strOpCode As String = "GL_CLSSETUP_VEHICLE_VEHTYPECODE_GET"
        Dim strParam As String
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intCnt As Integer
        Dim intSelectIndex As Integer = 0
        Dim strSort As String
        Dim strSearch As String

        strSort = "order by VehTypeCode"
        strSearch = "where Status = '" & objGLSetup.EnumVehTypeStatus.Active & "' "

        strParam = strSort & "|" & strSearch

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCode, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.VehType, _
                                                   objVehTypeDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_VEHICLE_VEHICLETYPECODE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objVehTypeDs.Tables(0).Rows.Count - 1
            objVehTypeDs.Tables(0).Rows(intCnt).Item("VehTypeCode") = Trim(objVehTypeDs.Tables(0).Rows(intCnt).Item("VehTypeCode"))
            objVehTypeDs.Tables(0).Rows(intCnt).Item("Description") = objVehTypeDs.Tables(0).Rows(intCnt).Item("VehTypeCode") & " (" & Trim(objVehTypeDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objVehTypeDs.Tables(0).Rows(intCnt).Item("VehTypeCode") = Trim(pv_strVehType) Then
                intSelectIndex = intCnt + 1
            End If
        Next

        dr = objVehTypeDs.Tables(0).NewRow()
        dr("VehTypeCode") = ""
        dr("Description") = "Select " & lblVehType.Text
        objVehTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlVehType.DataSource = objVehTypeDs.Tables(0)
        ddlVehType.DataValueField = "VehTypeCode"
        ddlVehType.DataTextField = "Description"
        ddlVehType.DataBind()
        ddlVehType.SelectedIndex = intSelectIndex
    End Sub


End Class
