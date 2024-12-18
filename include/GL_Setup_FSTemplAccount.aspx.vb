

Imports System
Imports System.Data
Imports System.IO 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.PWSystem.clsLangCap

Public Class gl_setup_FSTemplAccount : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDup As Label
    Protected WithEvents dgLineDet As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents validateCode As RequiredFieldValidator
    Protected WithEvents validateDesc As RequiredFieldValidator

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblReportCode As Label
    Protected WithEvents lblRowId As Label
    Protected WithEvents lblRowDesc As Label
    Protected WithEvents lblStmtType As Label
    Protected WithEvents ddlAccount As DropDownList
    Protected WithEvents ddlAccount1 As DropDownList
    Protected WithEvents tblSelection As HtmlTable
    'Protected WithEvents lblAccount As Label
    Protected WithEvents lblPlsSelectEither As Label
    Protected WithEvents lblPlsSelect As Label
    Protected WithEvents lblOr As Label
    Protected WithEvents lblOnly As Label
    'Protected WithEvents lblErrSelectBoth As Label
    Protected WithEvents lblErrNotSelect As Label
    Protected WithEvents lblCode As Label

    Protected WithEvents btnDellAll As ImageButton

    Protected WithEvents tblDetail As HtmlTable
    Protected WithEvents dgLineDetail As DataGrid
    Protected WithEvents ddlCOAFrom As DropDownList
    Protected WithEvents ddlCOATo As DropDownList
    Protected WithEvents lblErrNotSelectDetail As Label
    Protected WithEvents btnAddDetail As ImageButton
    Protected WithEvents btnRollDetail As ImageButton
    Protected WithEvents lblCOAGeneral As Label
    Protected WithEvents lblCOAGeneralDescr As Label
    Protected WithEvents rblDetail As DropDownList
    Protected WithEvents lblOptDetail As Label
    Protected WithEvents txtRefNo As TextBox
    Protected WithEvents ddlRefType As DropDownList
    Protected WithEvents lblPersentase As Label
    Protected WithEvents txtPersentase As TextBox

    Dim objAccGrpDs As New Object()
    Dim objAccDs As New Object()
    Dim objLineDs As New Object()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Protected objPU As New agri.PU.clsTrx()
    Protected objPUSetup As New agri.PU.clsSetup()
    Protected objAdminSetup As New agri.Admin.clsLoc()
    Dim objLangCapDs As New Object()

    Dim strLocType As String
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOpCdGetAcc As String = "GL_CLSSETUP_TEMPLACC_GETACCLIST"
    Dim strOpCdGetAccGrp As String = "GL_CLSSETUP_CHARTOFACCGRP_LIST_SEARCH"
    Dim strOpCdGetLine As String = "GL_CLSSETUP_TEMPLACC_GET"
    Dim strOpCdDelLine As String = "GL_CLSSETUP_TEMPLACC_DEL"
    Dim strOpCdAddLine As String = "GL_CLSSETUP_TEMPLACC_ADD"
    Dim strOpCdUpdTempl As String = "GL_CLSSETUP_TEMPLATE_UPDATETRACK"
    Dim strOpCdGetAccByGrp As String = "GL_CLSSETUP_FSCHARTOFACCOUNT_LIST_GET2"

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer

    Dim strReportCode As String
    Dim strRowId As String
    Dim strRowDesc As String
    Dim strStmtType As String
    Dim strCOAGeneral As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            'lblErrSelectBoth.visible = false
            lblErrNotSelect.Visible = False
            onload_GetLangCap()

            strReportCode = IIf(Trim(Request.QueryString("reportcode")) <> "", Trim(Request.QueryString("reportcode")), Trim(Request.Form("lblReportCode")))
            strRowId = IIf(Trim(Request.QueryString("rowid")) <> "", Trim(Request.QueryString("rowid")), Trim(Request.Form("lblRowId")))
            strRowDesc = IIf(Trim(Request.QueryString("strDesc")) <> "", Trim(Request.QueryString("strDesc")), Trim(Request.Form("lblRowDesc")))
            strStmtType = IIf(Trim(Request.QueryString("StmtType")) <> "", Trim(Request.QueryString("StmtType")), Trim(Request.Form("lblStmtType")))

            If Not IsPostBack Then
                BindAccount()
                'BindAccGrpCode()
                onLoad_LineDisplay()

                btnDellAll.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            End If
        End If
    End Sub

    Sub onLoad_LineDisplay()
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim hlButton As HyperLink


        lblRowId.Text = strRowId
        lblRowDesc.Text = strRowDesc
        lblStmtType.Text = strStmtType

        strParam = "order by tpl.AccCode|" & _
                   "Where tpl.ReportCode = '" & strReportCode & "' and tpl.RowId = '" & strRowId & "'|"

        Try
            intErrNo = objGLSetup.mtdGetFSTemplAcc(strOpCdGetLine, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strParam, _
                                                 objLineDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_TEMPLACC_GETLINE&errmesg=" & lblErrMessage.Text & "&redirect=GL/SETUP/gl_setup_FSTemplAccount.aspx")
        End Try

        dgLineDet.DataSource = objLineDs.Tables(0)
        dgLineDet.DataBind()

        For intCnt = 0 To dgLineDet.Items.Count - 1
            lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbDelete")
            lbButton.Visible = True
            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            lbl = dgLineDet.Items.Item(intCnt).FindControl("lblAccCode")
            strCOAGeneral = lbl.Text

            lbButton = dgLineDet.Items.Item(intCnt).FindControl("lbAccCode")
            'lbButton.NavigateUrl = "/en/GL/Setup/gl_setup_FSTemplAccountDet.aspx?regid=" & Trim(strCOAGeneral) & _
            '        "&RowID=" & Trim(strRowId)
            lbButton.Text = strCOAGeneral
        Next

        onLoad_LineDetailDisplay("")
    End Sub

    Sub BindAccount()
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim strAccType As String


        Select Case Val(strStmtType)
            Case objGLSetup.EnumReportType.BalanceSheet
                'strAccType = " and b.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "'"
                'tampil semua type 
                strAccType = ""
            Case objGLSetup.EnumReportType.ProfitLoss
                strAccType = " and b.AccType = '" & objGLSetup.EnumAccountType.ProfitAndLost & "'"
            Case objGLSetup.EnumReportType.COGS
                'strAccType = " and b.AccType = '" & objGLSetup.EnumAccountType.BalanceSheet & "'"
                strAccType = ""
            Case Else
                strAccType = ""
        End Select

        strParam = "order by AccCode|" & _
                    strAccType & " |" & _
                   " where ReportCode = '" & strReportCode & "' and RowId = '" & strRowId & "' "


        Try
            intErrNo = objGLSetup.mtdGetFSTemplAcc(strOpCdGetAcc, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strParam, _
                                                 objAccDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_FSTEMPLACC_GETACCLIST&errmesg=" & lblErrMessage.Text & "&redirect=GL/SETUP/gl_setup_FSTemplAccount.aspx")
        End Try

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select One"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlAccount.DataSource = objAccDs.Tables(0)
        ddlAccount.DataValueField = "AccCode"
        ddlAccount.DataTextField = "_Description"
        ddlAccount.DataBind()

        ddlAccount1.DataSource = objAccDs.Tables(0)
        ddlAccount1.DataValueField = "AccCode"
        ddlAccount1.DataTextField = "_Description"
        ddlAccount1.DataBind()
    End Sub


    Sub btnAdd_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strParam As String
        Dim strAccCode As String
        Dim strAccCode1 As String
        Dim strAccGrpCode As String
        Dim strAccGrpCode1 As String
        Dim intErrNo As Integer

        Try
            strAccCode = ddlAccount.SelectedItem.Value
            strAccCode1 = ddlAccount1.SelectedItem.Value
        Catch Exp As System.Exception
            Exit Sub
        End Try

        If strAccCode = "" And strAccCode1 = "" Then
            lblErrNotSelect.Visible = True
            Exit Sub
        End If

        If strAccCode <> "" And strAccCode1 = "" Then
            strAccCode1 = strAccCode
        ElseIf strAccCode = "" And strAccCode1 <> "" Then
            strAccCode = strAccCode1
        End If


        strParam = strReportCode & Chr(9) & strRowId & Chr(9) & strAccCode & Chr(9) & strAccCode1 & Chr(9) & "" & Chr(9) & "" & Chr(9) & strStmtType

        Try
            intErrNo = objGLSetup.mtdUpdFSTemplAcc(strOpCdUpdTempl, _
                                                 strOpCdAddLine, _
                                                 strOpCdGetAccByGrp, _
                                                 strOpCdDelLine, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strParam, False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTEMPLACC_ADD&errmesg=" & lblErrMessage.Text & "&redirect=GL/SETUP/gl_setup_FSTemplAccount.aspx")
        End Try

        onLoad_LineDisplay()
        BindAccount()

    End Sub


    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strParam As String
        Dim strAccCode As String
        Dim strAccGrpCode As String = ""
        Dim intErrNo As Integer
        Dim lbl As Label

        lbl = E.Item.FindControl("lblAccCode")
        strAccCode = lbl.Text.Trim

        strParam = strReportCode & Chr(9) & strRowId & Chr(9) & strAccCode & Chr(9) & "" & Chr(9) & "" & Chr(9) & "" & Chr(9) & ""

        Try
            intErrNo = objGLSetup.mtdUpdFSTemplAcc(strOpCdUpdTempl, _
                                                 strOpCdAddLine, _
                                                 strOpCdGetAccByGrp, _
                                                 strOpCdDelLine, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strParam, True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTEMPLACC_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_FSTemplAccount.aspx")
        End Try

        onLoad_LineDisplay()
        BindAccount()
    End Sub

    Sub CloseWindow_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Account))
        lblErrNotSelect.Text = lblPlsSelect.Text & " General COA."

        dgLineDet.Columns(0).HeaderText = "General COA Code "
        dgLineDet.Columns(1).HeaderText = "Description"

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_TemplAccount.aspx")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If

                Exit For
            End If
        Next
    End Function


    Sub btnDeleteAll_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCode As String
        Dim intErrNo As Integer

        strParamName = "CODE|ROWID"
        strParamValue = strReportCode & "|" & strRowId
        strOpCode = "GL_CLSSETUP_FSACCOUNT_DELETEDALL"

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTEMPLACC_ADD&errmesg=" & lblErrMessage.Text & "&redirect=GL/SETUP/gl_setup_FSTemplAccount.aspx")
        End Try

        onLoad_LineDisplay()

    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label

        tblDetail.Visible = True

        lbl = E.Item.FindControl("lblAccCode")
        lblCOAGeneral.Text = lbl.Text.Trim
        lbl = E.Item.FindControl("lblAccDesc")
        lblCOAGeneralDescr.Text = lbl.Text.Trim

        rblDetail.Items.Clear()
        rblDetail.Items.Add(New ListItem("Nett Off  ", 1))
        rblDetail.Items.Add(New ListItem("Moving Location  ", 2))
        rblDetail.Items.Add(New ListItem("Specific COA Only  ", 3))
        rblDetail.SelectedIndex = 0
        lblOptDetail.Text = " Net Off with "
        ddlCOATo.Visible = True

        lblCOAGeneral.Text = Trim(lblCOAGeneral.Text)
        BindAccCodeDropListFrom(lblCOAGeneral.Text)
        If UCase(Trim(lblCOAGeneral.Text)) = "33.1" Then
            BindSupplierDropListTo()
        Else
            BindAccCodeDropListTo()
        End If
        onLoad_LineDetailDisplay(lblCOAGeneral.Text)
    End Sub

    Sub BindAccCodeDropListFrom(Optional ByVal pv_strAccCode As String = "")
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET_FS"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        'strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")
        strParam = strParam & "AND COAGeneral = '" & Trim(lblCOAGeneral.Text) & "' "
        'strParam = strParam & "AND ACC.AccCode NOT IN (SELECT Option1 FROM GL_FSTEMPLATEDACCOPTION WHERE ReportCode = '" & strReportCode & "' AND RowID = '" & strRowId & "' AND COAGeneral = '" & lblCOAGeneral.Text & "')"

        Select Case rblDetail.SelectedItem.Value
            Case 1
                strOpCd = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET_FS"
            Case 2
                strOpCd = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET_FS_ALL"
        End Select

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To dsForDropDown.Tables(0).Rows.Count - 1
            If dsForDropDown.Tables(0).Rows(intCnt).Item("AccCode") = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
                Exit For
            End If
        Next

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select One"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlCOAFrom.DataSource = dsForDropDown.Tables(0)
        ddlCOAFrom.DataValueField = "AccCode"
        ddlCOAFrom.DataTextField = "_Description"
        ddlCOAFrom.DataBind()
        ddlCOAFrom.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindAccCodeDropListTo()
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET_FS"
        Dim dr As DataRow
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim dsForDropDown As DataSet

        'strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd, _
                                                   strParam, _
                                                   objGLSetup.EnumGLMasterType.AccountCode, _
                                                   dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BindAccCodeDropList&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("_Description") = "Select One"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlCOATo.DataSource = dsForDropDown.Tables(0)
        ddlCOATo.DataValueField = "AccCode"
        ddlCOATo.DataTextField = "_Description"
        ddlCOATo.DataBind()
        ddlCOATo.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindLocCodeDropListTo()
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim dsForDropDown As DataSet

        strParam = "" & "|" & objAdminSetup.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, dsForDropDown, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        dr = dsForDropDown.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Select One"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlCOATo.DataSource = dsForDropDown.Tables(0)
        ddlCOATo.DataValueField = "LocCode"
        ddlCOATo.DataTextField = "Description"
        ddlCOATo.DataBind()
        ddlCOATo.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub BindSupplierDropListTo()
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim objLocDs As New Object()
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim dsForDropDown As DataSet
        Dim strSuppType As String = objPUSetup.EnumSupplierType.Internal & "','" & objPUSetup.EnumSupplierType.External & "','" & objPUSetup.EnumSupplierType.Associate & "','" & objPUSetup.EnumSupplierType.Contractor

        'strSuppCode = IIf(pv_strPRId = "", "", strSelectedSuppCode)
        strParam = "||" & objPUSetup.EnumSuppStatus.Active & "||SupplierCode||SELECT"
        strParam = strParam & "|" '& IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ")
        strParam = strParam & "|" & strSuppType

        Try
            intErrNo = objPUSetup.mtdGetSupplier(strOpCd, strParam, dsForDropDown)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIER&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        dr = dsForDropDown.Tables(0).NewRow()
        dr("SupplierCode") = ""
        dr("Description") = "Select One"
        dsForDropDown.Tables(0).Rows.InsertAt(dr, 0)

        ddlCOATo.DataSource = dsForDropDown.Tables(0)
        ddlCOATo.DataValueField = "SupplierCode"
        ddlCOATo.DataTextField = "Description"
        ddlCOATo.DataBind()
        ddlCOATo.SelectedIndex = intSelectedIndex

        If Not dsForDropDown Is Nothing Then
            dsForDropDown = Nothing
        End If
    End Sub

    Sub rblDetail_Change(ByVal Sender As Object, ByVal E As EventArgs)
        Select Case Sender.SelectedItem.Value
            Case 1
                lblOptDetail.Text = " Net Off with "
                If UCase(Trim(lblCOAGeneral.Text)) = "33.1" Then
                    BindSupplierDropListTo()
                Else
                    BindAccCodeDropListTo()
                End If
                ddlCOATo.Visible = True
                lblPersentase.Visible = False
                txtPersentase.Visible = False

            Case 2
                lblOptDetail.Text = " Move into "
                BindLocCodeDropListTo()
                BindAccCodeDropListFrom(lblCOAGeneral.Text)
                ddlCOATo.Visible = True
                lblPersentase.Visible = True
                txtPersentase.Visible = True

            Case 3
                lblOptDetail.Text = "  "
                ddlCOATo.Visible = False
                lblPersentase.Visible = False
                txtPersentase.Visible = False
        End Select
    End Sub

    Sub btnAddDetail_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParam As String
        Dim strAccCode As String
        Dim strAccCode1 As String
        Dim strRefNo As String
        Dim strRefType As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCdUpdAccDetail As String = "GL_CLSSETUP_FSTEMPLACCOPT_ADD"

        If rblDetail.SelectedItem.Value <> 3 Then
            Try
                strAccCode = ddlCOAFrom.SelectedItem.Value
                strAccCode1 = ddlCOATo.SelectedItem.Value
                strRefNo = txtRefNo.Text
                strRefType = ddlRefType.SelectedItem.Value
            Catch Exp As System.Exception
                Exit Sub
            End Try
        Else
            Try
                strAccCode = ddlCOAFrom.SelectedItem.Value
                strAccCode1 = ddlCOAFrom.SelectedItem.Value
                strRefNo = txtRefNo.Text
                strRefType = ddlRefType.SelectedItem.Value
            Catch Exp As System.Exception
                Exit Sub
            End Try
        End If
       

        If strAccCode = "" And strAccCode1 = "" Then
            lblErrNotSelectDetail.Visible = True
            Exit Sub
        End If
        If strAccCode <> "" And strAccCode1 = "" Then
            strAccCode1 = strAccCode
        ElseIf strAccCode = "" And strAccCode1 <> "" Then
            strAccCode = strAccCode1
        End If
        If strRefNo = "" Then
            lblErrNotSelectDetail.Visible = True
            lblErrNotSelectDetail.Text = "Please fill in Ref. No"
            Exit Sub
        End If
        If rblDetail.SelectedItem.Value = 2 Then
            If txtPersentase.Text = "" Or CDbl(txtPersentase.Text) = 0 Then
                lblErrNotSelectDetail.Visible = True
                lblErrNotSelectDetail.Text = "Please fill in Persentase"
                Exit Sub
            End If
        End If

        strParamName = "REPORTCODE|ROWID|COAGENERAL|ACCOPTION|OPTION1|OPTION2|REFNO|REFTYPE|PERSENTASE"
        strParamValue = strReportCode & "|" & strRowId & "|" & lblCOAGeneral.Text & "|" & rblDetail.SelectedItem.Value & "|" & ddlCOAFrom.SelectedItem.Value & "|" & ddlCOATo.SelectedItem.Value & "|" & _
                        strRefNo & "|" & strRefType & "|" & txtPersentase.Text

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCdUpdAccDetail, _
                                                strParamName, _
                                                strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTEMPLACC_ADD&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvlist.aspx")
        End Try

        onLoad_LineDetailDisplay(lblCOAGeneral.Text)
    End Sub

    Sub DEDR_DeleteDetail(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strIDCOAGeneral As String
        Dim strAccOption As String
        Dim strOption1 As String
        Dim strOption2 As String
        Dim strAccGrpCode As String = ""
        Dim intErrNo As Integer
        Dim lbl As Label
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCdUpdAccDetail As String = "GL_CLSSETUP_FSTEMPLACCOPT_DEL"

        lbl = E.Item.FindControl("lblIDCOAGeneral")
        strIDCOAGeneral = lbl.Text.Trim
        lbl = E.Item.FindControl("lblAccOption")
        strAccOption = lbl.Text.Trim
        lbl = E.Item.FindControl("lblOption1")
        strOption1 = lbl.Text.Trim
        lbl = E.Item.FindControl("lblOption2")
        strOption2 = lbl.Text.Trim

        strParamName = "STRSEARCH"
        strParamValue = "WHERE ReportCode = '" & strReportCode & "' AND RowID = '" & strRowId & "' AND COAGeneral = '" & strIDCOAGeneral & "'" & _
                        " AND AccOption = '" & strAccOption & "' AND Option1 = '" & strOption1 & "' AND Option2 = '" & strOption2 & "'"

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCdUpdAccDetail, _
                                                strParamName, _
                                                strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTEMPLACC_ADD&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvlist.aspx")
        End Try

        onLoad_LineDetailDisplay(lblCOAGeneral.Text)
        'BindAccCodeDropListFrom(lblCOAGeneral.Text)
    End Sub

    Sub btnDeleteDetailAll_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim strOpCdUpdAccDetail As String = "GL_CLSSETUP_FSTEMPLACCOPT_DEL"

        strParamName = "STRSEARCH"
        strParamValue = "WHERE ReportCode = '" & strReportCode & "' AND RowID = '" & strRowId & "' AND COAGeneral = '" & lblCOAGeneral.Text & "'"

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCdUpdAccDetail, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTEMPLACC_ADD&errmesg=" & lblErrMessage.Text & "&redirect=GL/SETUP/gl_setup_FSTemplAccount.aspx")
        End Try

        onLoad_LineDetailDisplay(lblCOAGeneral.Text)
        'BindAccCodeDropListFrom(lblCOAGeneral.Text)
    End Sub

    Sub onLoad_LineDetailDisplay(Optional ByVal pv_strCOAGeneral As String = "")
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim objDS As Object
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCdUpdAccDetail As String = "GL_CLSSETUP_FSTEMPLACCOPT_GET"

        strParamName = "STRSEARCH"
        If pv_strCOAGeneral = "" Then
            strParamValue = " AND ReportCode = '" & strReportCode & "' AND RowID = '" & strRowId & "' "
        Else
            strParamValue = " AND ReportCode = '" & strReportCode & "' AND RowID = '" & strRowId & "' AND A.COAGeneral = '" & pv_strCOAGeneral & "' "
        End If

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCdUpdAccDetail, _
                                                strParamName, _
                                                strParamValue, _
                                                objDS)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTEMPLACC_ADD&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcvlist.aspx")
        End Try


        If objDS.Tables(0).Rows.Count > 0 Then
            If lblCOAGeneral.Text = "" Then
                tblDetail.Visible = False
            Else
                tblDetail.Visible = True
            End If
        End If

        dgLineDetail.DataSource = objDS.Tables(0)
        dgLineDetail.DataBind()

        For intCnt = 0 To dgLineDetail.Items.Count - 1
            lbButton = dgLineDetail.Items.Item(intCnt).FindControl("lbDelete")
            lbButton.Visible = True
            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        Next
    End Sub
End Class
