Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction
Imports Microsoft.VisualBasic.DateAndTime

Imports agri.PD
Imports agri.GlobalHdl


Public Class PD_trx_POMProdDet : Inherits Page

    Protected WithEvents txtDate As Textbox
    Protected WithEvents ddlProduction As DropDownList
    Protected WithEvents ddlType As DropDownList
    Protected WithEvents ddlStorage As DropDownList
    Protected WithEvents txtWeight As Textbox
    Protected WithEvents lblPOMYieldID As Label
    Protected WithEvents lblPeriod As Label
    Protected WithEvents lblStatus As Label
    Protected WithEvents lblDateCreated As Label
    Protected WithEvents lblLastUpdate As Label
    Protected WithEvents lblUpdatedBy As Label
    Protected WithEvents pomid As HtmlInputHidden
    Protected WithEvents SaveBtn As ImageButton
    Protected WithEvents DelBtn As ImageButton
    Protected WithEvents btnNew As ImageButton
    Protected WithEvents UnDelBtn As ImageButton
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrProd As Label
    Protected WithEvents lblErrType As Label
    Protected WithEvents lblErrRelDesc As Label
    Protected WithEvents lblErrRel As Label
    Protected WithEvents lblErrDate As Label
    Protected WithEvents lblErrDateMsg As Label
    Protected WithEvents lblHiddenSts As Label

    Dim objPDTrx As New agri.PD.clsTrx()
    Dim objPDSetup As New agri.PD.clsSetup()
    Dim objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim objPOMProdDs As New Object()
    Dim objProdDs As New Object()
    Dim objTypeDs As New Object()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim strDateFormat As String
    Dim intPDAR As Integer
    Dim intConfig As Integer

    Dim strSelectedPOMYldId As String = ""
    Dim intStatus As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        strDateFormat = Session("SS_DATEFMT")
        intPDAR = Session("SS_PDAR")
        intConfig = Session("SS_CONFIGSETTING")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDEstateProduction), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrProd.Visible = False
            lblErrType.Visible = False
            lblErrRel.Visible = False
            lblErrDate.Visible = False
            strSelectedPOMYldId = Trim(IIf(Request.QueryString("pomid") <> "", Request.QueryString("pomid"), Request.Form("pomid")))
            intStatus = CInt(lblHiddenSts.Text)

            If Not IsPostBack Then
                If strSelectedPOMYldId <> "" Then
                    pomid.Value = strSelectedPOMYldId
                    onLoad_Display()
                Else
                    txtDate.Text = objGlobal.GetShortDate(strDateFormat, Now())
                    BindProduction("")
                    BindType("","")
                    BindStorage("","")
                    onLoad_BindButton()
                End If
            End If
        End If
    End Sub


    Sub onLoad_BindButton()
        txtDate.Enabled = False
        ddlProduction.Enabled = False
        ddlType.Enabled = False
        ddlStorage.Enabled = False
        txtWeight.Enabled = False
        SaveBtn.Visible = False
        DelBtn.Visible = False
        btnNew.Visible = False
        UnDelBtn.Visible = False

        Select Case intStatus
            Case objPDTrx.EnumPOMYieldStatus.Active, objPDTrx.EnumPOMYieldStatus.Confirmed
                btnNew.Visible = True
                DelBtn.Visible = True
                DelBtn.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case objPDTrx.EnumPOMYieldStatus.Deleted
                btnNew.Visible = True
                UnDelBtn.Visible = True
            Case objPDTrx.EnumPOMYieldStatus.Closed
                btnNew.Visible = True
            Case Else
                txtDate.Enabled = True
                ddlProduction.Enabled = True
                ddlType.Enabled = True
                ddlStorage.Enabled = True
                txtWeight.Enabled = True
                SaveBtn.Visible = True
        End Select
    End Sub

    Sub onLoad_Display()
        Dim strOpCd As String = "PD_CLSTRX_POMYIELD_GET"
        Dim strParam As String = strSelectedPOMYldId        
        Dim intErrNo As Integer

        Try
            intErrNo = objPDTrx.mtdGetPOMYield(strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objPOMProdDs, _
                                            True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        lblPOMYieldID.Text = strSelectedPOMYldId
        txtDate.Text = objGlobal.GetShortDate(strDateFormat, objPOMProdDs.Tables(0).Rows(0).Item("ProdDate"))
        txtWeight.Text = objPOMProdDs.Tables(0).Rows(0).Item("Weight")
        intStatus = CInt(objPOMProdDs.Tables(0).Rows(0).Item("Status"))
        lblHiddenSts.Text = objPOMProdDs.Tables(0).Rows(0).Item("Status").Trim()
        lblStatus.Text = objPDTrx.mtdGetEstateYieldStatus(objPOMProdDs.Tables(0).Rows(0).Item("Status"))
        lblPeriod.Text = objPOMProdDs.Tables(0).Rows(0).Item("AccMonth").Trim() & "/" & objPOMProdDs.Tables(0).Rows(0).Item("AccYear").Trim()
        lblDateCreated.Text = objGlobal.GetLongDate(objPOMProdDs.Tables(0).Rows(0).Item("CreateDate"))
        lblLastUpdate.Text = objGlobal.GetLongDate(objPOMProdDs.Tables(0).Rows(0).Item("UpdateDate"))
        lblUpdatedBy.Text = objPOMProdDs.Tables(0).Rows(0).Item("UserName")
        BindProduction(objPOMProdDs.Tables(0).Rows(0).Item("POMProdNameCode").Trim())
        BindType(objPOMProdDs.Tables(0).Rows(0).Item("POMProdNameCode").Trim(), objPOMProdDs.Tables(0).Rows(0).Item("POMTypeCode").Trim())
        BindStorage(objPOMProdDs.Tables(0).Rows(0).Item("POMProdNameCode").Trim(), objPOMProdDs.Tables(0).Rows(0).Item("TankCode").Trim())
        onLoad_BindButton()
    End Sub

    Sub BindProdNameRel(Sender As Object, E As EventArgs)
        Dim strProdName As String = ddlProduction.SelectedItem.Value
        If strProdName <> "" Then
            BindType(strProdName, "")
            BindStorage(strProdName, "")
        End If
    End Sub

    Sub BindProduction(ByVal pv_strProdCode As String)
        Dim strOpCd As String = "PD_CLSSETUP_POMPRODNAME_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPDSetup.mtdGetPOMProdName(strOpCd, _
                                                    strParam, _
                                                    objProdDs, _
                                                    False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_PRODNAME_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objProdDs.Tables(0).Rows.Count - 1
            objProdDs.Tables(0).Rows(intCnt).Item("POMProdNameCode") = Trim(objProdDs.Tables(0).Rows(intCnt).Item("POMProdNameCode"))
            objProdDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objProdDs.Tables(0).Rows(intCnt).Item("POMProdNameCode")) & " (" & Trim(objProdDs.Tables(0).Rows(intCnt).Item("Description")) & ")"
            If objProdDs.Tables(0).Rows(intCnt).Item("POMProdNameCode") = Trim(pv_strProdCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objProdDs.Tables(0).NewRow()
        dr("POMProdNameCode") = ""
        dr("Description") = "Select one Production"
        objProdDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlProduction.DataSource = objProdDs.Tables(0)
        ddlProduction.DataValueField = "POMProdNameCode"
        ddlProduction.DataTextField = "Description"
        ddlProduction.DataBind()
        ddlProduction.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindType(ByVal pv_strPOMProdName As String, ByVal pv_strType As String)
        Dim strOpCd As String = "PD_CLSSETUP_POMTYPE_LIST_GET"
        Dim dr As DataRow
        Dim strParam As String = pv_strPOMProdName
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            intErrNo = objPDSetup.mtdGetPOMType(strOpCd, _
                                                strParam, _
                                                objTypeDs, _
                                                False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_TYPE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objTypeDs.Tables(0).Rows.Count - 1
            objTypeDs.Tables(0).Rows(intCnt).Item("POMTypeCode") = objTypeDs.Tables(0).Rows(intCnt).Item("POMTypeCode").Trim()
            objTypeDs.Tables(0).Rows(intCnt).Item("Description") = objTypeDs.Tables(0).Rows(intCnt).Item("POMTypeCode") & " (" & objTypeDs.Tables(0).Rows(intCnt).Item("Description").Trim() & ")"
            If objTypeDs.Tables(0).Rows(intCnt).Item("POMTypeCode") = Trim(pv_strType) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objTypeDs.Tables(0).NewRow()
        dr("POMTypeCode") = ""
        dr("Description") = "Select one Type"
        objTypeDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlType.DataSource = objTypeDs.Tables(0)
        ddlType.DataValueField = "POMTypeCode"
        ddlType.DataTextField = "Description"
        ddlType.DataBind()
        ddlType.SelectedIndex = intSelectedIndex
    End Sub


    Sub BindStorage(ByVal pv_strPOMProdName As String, ByVal pv_strTankCode As String)
        Dim strOpCd As String = "PD_CLSSETUP_TANK_LIST_SEARCH"
        Dim objDs As New Dataset()
        Dim dr As DataRow
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        Try
            strParam = "ORDER BY T.TankCode ASC|And T.Status = '" & objPDSetup.EnumTankStatus.Active & "' And T.Type = '" & pv_strPOMProdName & "' "
            intErrNo = objPDSetup.mtdGetTank(strCompany, _
                                             strLocation, _
                                             strUserId, _
                                             strAccMonth, _
                                             strAccYear, _
                                             strOpCd, _
                                             strParam, _
                                             objDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_STORAGE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objDs.Tables(0).Rows.Count - 1
            objDs.Tables(0).Rows(intCnt).Item("TankCode") = objDs.Tables(0).Rows(intCnt).Item("TankCode").Trim()
            objDs.Tables(0).Rows(intCnt).Item("Name") = objDs.Tables(0).Rows(intCnt).Item("TankCode") & " (" & objDs.Tables(0).Rows(intCnt).Item("Name").Trim() & ")"
            If objDs.Tables(0).Rows(intCnt).Item("TankCode") = Trim(pv_strTankCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

        dr = objDs.Tables(0).NewRow()
        dr("TankCode") = ""
        dr("Name") = "Select one Storage"
        objDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlStorage.DataSource = objDs.Tables(0)
        ddlStorage.DataValueField = "TankCode"
        ddlStorage.DataTextField = "Name"
        ddlStorage.DataBind()
        ddlStorage.SelectedIndex = intSelectedIndex
    End Sub

    Sub Button_Click(Sender As Object, E As ImageClickEventArgs)
        Dim strOpCd_Add As String = "PD_CLSTRX_POMYIELD_ADD"
        Dim strOpCd_Upd As String = "PD_CLSTRX_POMYIELD_UPD"
        Dim strOpCd_Get As String = "PD_CLSTRX_POMYIELD_GET"
        Dim strOpCd_Sts As String = "PD_CLSTRX_POMYIELD_STATUS_UPD"
        Dim objPOMYieldId As String
        Dim strCmdArgs As String = CType(Sender, ImageButton).CommandArgument        
        Dim intErrNo As Integer
        Dim strParam As String = ""
        Dim objFormatDate As String
        Dim objActualDate As String

        If strCmdArgs = "Save" Then
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           Trim(txtDate.Text), _
                                           objFormatDate, _
                                           objActualDate) = False Then
                lblErrDate.Visible = True
                lblErrDate.Text = lblErrDateMsg.Text & objFormatDate
                Exit Sub
            ElseIf ddlProduction.SelectedItem.Value = "" Then
                lblErrProd.Visible = True
                Exit Sub
            ElseIf ddlType.SelectedItem.Value = "" Then
                lblErrType.Visible = True
                Exit Sub
            End If

            strParam = objGlobal.mtdGetDocPrefix(objGlobal.EnumDocType.POMProduction) & "|" & _
                       strSelectedPOMYldId & "|" & _
                       objActualDate & "|" & _
                       ddlProduction.SelectedItem.Value & "|" & _
                       ddlType.SelectedItem.Value & "|" & _
                       Trim(txtWeight.Text) & "||" & _
                       objPDTrx.EnumPOMYieldStatus.Confirmed & "|" & _
                       ddlStorage.SelectedItem.Value
            Try
                intErrNo = objPDTrx.mtdUpdPOMYield(strOpCd_Add, _
                                                   strOpCd_Upd, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strParam, _
                                                   False, _
                                                   objPOMYieldId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=PD/trx/PD_trx_POMProdDet.aspx")
            End Try

            If objPOMYieldId = "" Then
                lblErrRel.Text = ddlProduction.SelectedItem.Value & lblErrRelDesc.Text & ddlType.SelectedItem.Value
                lblErrRel.Visible = True
            Else
                strSelectedPOMYldId = objPOMYieldId
                pomid.Value = strSelectedPOMYldId
            End If

        ElseIf strCmdArgs = "Del" Then
            strParam = strSelectedPOMYldId & "|" & objPDTrx.EnumPOMYieldStatus.Deleted & "|" & _
                       Trim(txtWeight.Text) & "|" & ddlType.SelectedItem.Value & "|" & ddlStorage.SelectedItem.Value
            Try
                intErrNo = objPDTrx.mtdUpdPOMYield("", _
                                                    strOpCd_Sts, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam, _
                                                    True, _
                                                    objPOMYieldId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_DEL&errmesg=" & lblErrMessage.Text & "&redirect=PD/trx/PD_trx_POMProdDet.aspx?pomid=" & strSelectedPOMYldId)
            End Try

        ElseIf strCmdArgs = "UnDel" Then
            strParam = strSelectedPOMYldId & "|" & objPDTrx.EnumPOMYieldStatus.Confirmed & "|" & _
                       Trim(txtWeight.Text) & "|" & ddlType.SelectedItem.Value & "|" & ddlStorage.SelectedItem.Value
            Try
                intErrNo = objPDTrx.mtdUpdPOMYield("", _
                                                   strOpCd_Sts, _
                                                   strCompany, _
                                                   strLocation, _
                                                   strUserId, _
                                                   strAccMonth, _
                                                   strAccYear, _
                                                   strParam, _
                                                   True, _
                                                   objPOMYieldId)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_UNDEL&errmesg=" & lblErrMessage.Text & "&redirect=PD/trx/PD_trx_POMProdDet.aspx?pomid=" & strSelectedPOMYldId)
            End Try
        ElseIf strCmdArgs = "New" Then
            Response.Redirect("PD_trx_POMProdDet.aspx")
        End If

        If strSelectedPOMYldId <> "" Then
            onLoad_Display()
        End If
    End Sub

    Sub BackBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PD_trx_POMProdList.aspx")
    End Sub


End Class
