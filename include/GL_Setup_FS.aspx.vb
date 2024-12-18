
Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.PWSystem.clsConfig


Public Class GL_Setup_FS : Inherits Page

    Dim strLocType As String
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Protected objGLSetup As New agri.GL.clsSetup()

    Dim objSysConfig As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOpCd_Get As String = "GL_SETUP_FSTEMPLDTL_GET"
    Dim strOpCd_Del As String = "GL_SETUP_FSTEMPLDTL_DEL"
    Dim strOpCdDelAcc As String = "GL_SETUP_FSTEMPL_ACC_DEL"

    Dim strOpCd_Config As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"

    Dim objLangCapDs As New Object
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer

    Dim objGLTrx As New agri.GL.ClsTrx()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")

        strLocType = Session("SS_LOCTYPE")
        Page.MaintainScrollPositionOnPostBack = True

        lblErrCalc.Visible = False
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLFSSETUP), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If Not Page.IsPostBack Then
                BindBlockDropList()
                If Not Request.QueryString("ReportCode") = "" Then
                    txtCode.Text = Request.QueryString("ReportCode")
                    ViewState.Item("ReportCode") = Request.QueryString("ReportCode")
                Else
                    BindReportType("")
                End If

                If Not txtCode.Text = "" Then
                    DisplayData()
                    BindGrid()
                End If
            Else
                lblReqField.Visible = False
            End If

            TypeDisplay()
        End If

    End Sub
    Sub BindBlockDropList()

        Dim strParamName As String
        Dim strParamValue As String
        Dim dr As DataRow
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet

        sSQLKriteria = "Select Distinct BlkCode,(rtrim(Description) + '(' + rTrim(BlkCode)) + ')' AS BlkName FROM GL_BLOCK Where Status='1'"

        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                             strParamName,
                                             strParamValue,
                                             objdsST)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CM_TRX_CONTRACTREGDET_SELLERLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objdsST.Tables(0).NewRow()
        dr("BlkCode") = ""
        objdsST.Tables(0).Rows.InsertAt(dr, 0)
        radGroupID.DataSource = objdsST.Tables(0)
        radGroupID.DataValueField = "BlkCode"
        radGroupID.DataTextField = "BlkName"
        radGroupID.DataBind()


    End Sub

    Sub BindGrid()
        Dim lbl As Label
        Dim intDispSeq As Integer

        TmplList.DataSource = LoadDtlData()
        TmplList.DataBind()

        'get last DispSeq
        If CInt(TmplList.Items.Count) > 0 Then
            lbl = TmplList.Items.Item(CInt(TmplList.Items.Count - 1)).FindControl("lblDispSeq")
            intDispSeq = CInt(lbl.Text) + 1
        Else
            intDispSeq = 1
        End If

        txtDispSeq1.Text = intDispSeq

        Dim vSpace As String = ""

        For intCnt = 0 To TmplList.Items.Count - 1
            If CType(TmplList.Items(intCnt).FindControl("lblFBold"), Label).Text = "1" Then
                TmplList.Items(intCnt).Font.Bold = True
            End If

            Select Case CType(TmplList.Items(intCnt).FindControl("lblSpace"), Label).Text
                Case "1"
                    vSpace = "&nbsp&nbsp"
                Case "2"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp"
                Case "3"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case "4"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case "5"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case "6"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case "7"
                    vSpace = "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                Case Else
                    vSpace = ""
            End Select

            CType(TmplList.Items(intCnt).FindControl("lblDescription"), Label).Text = vSpace & CType(TmplList.Items(intCnt).FindControl("lblDescription"), Label).Text
        Next
    End Sub

    'masih dipakai untuk edit akun
    Sub DataGrid_ItemCreated(ByVal Sender As Object, ByVal e As DataGridItemEventArgs)
        Dim lbl As Label
        Dim btn As Button
        Dim DeleteButton As LinkButton
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                lbl = e.Item.FindControl("lblNo")
                lbl.Text = e.Item.ItemIndex.ToString + 1

                lbl = e.Item.FindControl("lblFSRowType")
                If lbl.Text.Trim = objGLSetup.EnumFSRowType.Entry Or lbl.Text.Trim = objGLSetup.EnumFSRowType.SubEntry Then
                    btn = e.Item.FindControl("btnAccount")
                    btn.Visible = True
                End If

                DeleteButton = e.Item.FindControl("Delete")
                DeleteButton.Visible = True
                DeleteButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        End Select


        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If

    End Sub

    '--tidak dipakai
    Sub BindRowTypeList(ByRef DropList As DropDownList, Optional ByVal itemtype As String = "")

        DropList.Items.Add(New ListItem(objGLSetup.mtdGetFSRowType(objGLSetup.EnumFSRowType.Entry), objGLSetup.EnumFSRowType.Entry))
        DropList.Items.Add(New ListItem(objGLSetup.mtdGetFSRowType(objGLSetup.EnumFSRowType.Header), objGLSetup.EnumFSRowType.Header))
        DropList.Items.Add(New ListItem(objGLSetup.mtdGetFSRowType(objGLSetup.EnumFSRowType.Formula), objGLSetup.EnumFSRowType.Formula))
        DropList.Items.Add(New ListItem(objGLSetup.mtdGetFSRowType(objGLSetup.EnumFSRowType.SubEntry), objGLSetup.EnumFSRowType.SubEntry))
        DropList.Items.Add(New ListItem(objGLSetup.mtdGetFSRowType(objGLSetup.EnumFSRowType.SubFormula), objGLSetup.EnumFSRowType.SubFormula))
        Select Case itemtype.Trim
            Case objGLSetup.EnumFSRowType.Entry
                DropList.SelectedIndex = 0
            Case objGLSetup.EnumFSRowType.Header
                DropList.SelectedIndex = 1
            Case objGLSetup.EnumFSRowType.Formula
                DropList.SelectedIndex = 2
            Case objGLSetup.EnumFSRowType.SubEntry
                DropList.SelectedIndex = 3
            Case objGLSetup.EnumFSRowType.SubFormula
                DropList.SelectedIndex = 4

        End Select
    End Sub

    Protected Function LoadHdrData() As DataSet
        Dim strOpCode As String = "GL_CLSSETUP_FSTEMPLATE_LIST_GET"
        strParam = Replace(txtCode.Text, "'", "''") & "|"
        Try
            intErrNo = objGLSetup.mtdGetFSTemplateList(strOpCode,
            strCompany,
            strLocation,
            strUserId,
            strParam,
                                              objDataSet)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTMPL_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_FS.aspx")
        End Try
        Return objDataSet
    End Function

    Protected Function LoadDtlData() As DataSet
        Dim strOpCode As String = "GL_SETUP_FSTEMPLDTL_GET"
        strParam = txtCode.Text & "|ORDER BY DISPSEQ"
        Try
            intErrNo = objGLSetup.mtdGetFSTemplateDtl(strOpCode,
                                              strCompany,
                                              strLocation,
                                            strUserId,
                                            strParam,
                                            objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTEMPLDTL_GET&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_FS.aspx")
        End Try
        Return objDataSet
    End Function

    Sub DisplayData()
        Dim intReportTypeindex As Integer = 0
        Dim dsTx As DataSet = LoadHdrData()
        If dsTx.Tables(0).Rows.Count > 0 Then
            txtCode.Enabled = False
            txtName.Text = Trim(dsTx.Tables(0).Rows(0).Item("Name"))
            txtDescription.Text = Trim(dsTx.Tables(0).Rows(0).Item("Description"))
            txtDescription3.Text = Trim(dsTx.Tables(0).Rows(0).Item("Description2"))

            ddlReportType.Items.Clear()
            ddlReportType.Items.Add(New ListItem("Select Report Type", ""))
            ddlReportType.Items.Add(New ListItem(objGLSetup.mtdGetReportType(objGLSetup.EnumReportType.BalanceSheet), objGLSetup.EnumReportType.BalanceSheet))
            ddlReportType.Items.Add(New ListItem(objGLSetup.mtdGetReportType(objGLSetup.EnumReportType.ProfitLoss), objGLSetup.EnumReportType.ProfitLoss))
            ddlReportType.Items.Add(New ListItem(objGLSetup.mtdGetReportType(objGLSetup.EnumReportType.COGS), objGLSetup.EnumReportType.COGS))
            ddlReportType.Items.Add(New ListItem(objGLSetup.mtdGetReportType(objGLSetup.EnumReportType.Other), objGLSetup.EnumReportType.Other))
            If dsTx.Tables(0).Rows(0).Item("StmtType") = objGLSetup.EnumReportType.BalanceSheet Then
                intReportTypeindex = 1
            ElseIf dsTx.Tables(0).Rows(0).Item("StmtType") = objGLSetup.EnumReportType.ProfitLoss Then
                intReportTypeindex = 2
            ElseIf dsTx.Tables(0).Rows(0).Item("StmtType") = objGLSetup.EnumReportType.COGS Then
                intReportTypeindex = 3
            ElseIf dsTx.Tables(0).Rows(0).Item("StmtType") = objGLSetup.EnumReportType.Other Then
                intReportTypeindex = 4
            End If

            If dsTx.Tables(0).Rows(0).Item("StmtType") = objGLSetup.EnumReportType.Other Then

                trGroupID.Visible = True
            Else
                trGroupID.Visible = False
            End If

            radGroupID.SelectedIndex = 0
            radGroupID.Text = ""
            ddlReportType.SelectedIndex = intReportTypeindex
            ddlReportType.Enabled = False


        End If
    End Sub

    Sub ddlCheckType1(ByVal Sender As Object, ByVal E As EventArgs)

        Call TypeDisplay()

    End Sub


    Private Sub TypeDisplay()

        If ddlFSRowType1.SelectedItem.Value.Trim = objGLSetup.EnumFSRowType.Entry Then

            lblRefNo1.Visible = True
            txtRefNo1.Visible = True
            lblFormula1.Visible = False
            txtFormula1.Visible = False
            chkBegBal.Visible = True
            trStyle.Visible = False
            trEditor.Visible = True
            lblRefNo2.Visible = True
            txtRefNo2.Visible = True

        ElseIf ddlFSRowType1.SelectedItem.Value.Trim = objGLSetup.EnumFSRowType.Header Then

            lblRefNo1.Visible = False
            txtRefNo1.Visible = False
            lblFormula1.Visible = False
            txtFormula1.Visible = False
            chkBegBal.Visible = False
            trStyle.Visible = False
            trEditor.Visible = True
            lblRefNo2.Visible = False
            txtRefNo2.Visible = False

        ElseIf ddlFSRowType1.SelectedItem.Value.Trim = objGLSetup.EnumFSRowType.SubEntry Then

            lblRefNo1.Visible = True
            txtRefNo1.Visible = True
            lblFormula1.Visible = False
            txtFormula1.Visible = False
            chkBegBal.Visible = True
            trStyle.Visible = False
            trEditor.Visible = False
            lblRefNo2.Visible = False
            txtRefNo2.Visible = False


        ElseIf ddlFSRowType1.SelectedItem.Value.Trim = objGLSetup.EnumFSRowType.SubFormula Then

            lblRefNo1.Visible = True
            txtRefNo1.Visible = True
            lblFormula1.Visible = True
            txtFormula1.Visible = True
            chkBegBal.Visible = False
            trStyle.Visible = False
            trEditor.Visible = False
            lblRefNo2.Visible = False
            txtRefNo2.Visible = False

        Else 'formula

            lblRefNo1.Visible = True
            txtRefNo1.Visible = True
            lblFormula1.Visible = True
            txtFormula1.Visible = True
            chkBegBal.Visible = False
            trStyle.Visible = False
            trEditor.Visible = True
            lblRefNo2.Visible = False
            txtRefNo2.Visible = False

        End If

    End Sub


    Sub onClick_Account(ByVal Sender As Object, ByVal E As EventArgs)
        Dim lbl As Label
        Dim txt As TextBox
        Dim strRowId As String
        Dim strDesc As String
        Dim strID As String
        Dim strReportCode As String
        Dim strStmtType As String

        strReportCode = Trim(txtCode.Text)
        strStmtType = ddlReportType.SelectedItem.Value
        strID = CType(Sender, Button).ID

        If strID = "btnAccount" Then
            strRowId = CType(Sender, Button).CommandArgument.Trim
            strDesc = CType(Sender, Button).CommandName.Trim
        Else
            lbl = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("lblRowId")
            strRowId = lbl.Text.Trim
            txt = TmplList.Items.Item(CInt(TmplList.EditItemIndex)).FindControl("txtDescription")
            strDesc = txt.Text.Trim
        End If
        If strRowId <> "" Then
            Response.Write("<Script Language=""JavaScript"">pop_Account=window.open(""../../GL/Setup/GL_Setup_FSTemplAccount.aspx?reportcode=" & strReportCode &
                            "&rowid=" & strRowId &
"&strdesc=" & strDesc &
                            "&stmttype=" & strStmtType &
                            """, null ,""'pop_Account',width=800,height=600,top=50,left=150,status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");pop_Account.focus();</Script>")
        End If
    End Sub

    Sub btnAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim strOpCode As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer


        'header not save yet
        If txtCode.Enabled = True Then
            lblReqField.Text = "Please Save Header First!"
            lblReqField.Visible = True
            Exit Sub
        End If

        'cek required field
        If Trim(txtDescription1.Text) = "" Then
            lblReqField.Visible = True
            Exit Sub
        End If

        'cek required field for spesific condition
        Select Case ddlFSRowType1.SelectedItem.Value
            Case 1, 4 'Entry, SubEntry
                If Trim(txtRefNo1.Text) = "" Then
                    lblReqField.Visible = True
                    Exit Sub
                End If
            Case 3, 5 'Formula, Sub Formula
                If Trim(txtRefNo1.Text) = "" Then
                    lblReqField.Visible = True
                    Exit Sub
                End If
                If Trim(txtFormula1.Text) = "" Then
                    lblReqField.Visible = True
                    Exit Sub
                End If
        End Select


        If lblRowID.Text = "" Then
            strOpCode = "GL_SETUP_FSTEMPLDTL_ADD"
        Else
            strOpCode = "GL_SETUP_FSTEMPLDTL_UPD"
        End If

        strParamName = "CODE|ROWID|DISPSEQ|DESCRIPTION|DESCRIPTION2|ROWTYPE|REFNO|FORMULA|BEGBALANCE|CREATEID|UPDATEID|FSPACE|FUNDERLINE|FBOLD|FFONT|FNEGSIGN|REFNORVS|GROUPID"
        strParamValue = txtCode.Text & "|" & lblRowID.Text & "|" & txtDispSeq1.Text & "|" & txtDescription1.Text & "|" & txtDescription2.Text &
                        "|" & ddlFSRowType1.SelectedItem.Value & "|" & txtRefNo1.Text & "|" & txtFormula1.Text & "|" &
                        IIf(chkBegBal.Checked = True, 1, 0) & "|" & strUserId & "|" & strUserId &
                        "|" & ddlFSpacing.SelectedItem.Value & "|" &
                        ddlFEffect.SelectedItem.Value & "|" &
                        ddlFStyle.SelectedItem.Value & "|" &
                        ddlFSize.SelectedItem.Value & "|" &
                        ddlFNegSign.SelectedItem.Value & "|" &
                        txtRefNo2.Text & "|" &
                        radGroupID.SelectedValue

        'strParamValue = txtCode.Text & "|" & lblRowID.Text & "|" & txtDispSeq1.Text & "|" & txtDescription1.Text & "|" & txtDescription2.Text & _
        '                "|" & ddlFSRowType1.SelectedItem.Value & "|" & txtRefNo1.Text & "|" & txtFormula1.Text & "|" & _
        '                IIf(chkBegBal.Checked = True, 1, 0) & "|" & strUserId & "|" & strUserId & _
        '                "|" & ddlSpace1.SelectedItem.Value & "|" & _
        '                IIf(chkUnderline.Checked = True, 1, 0) & "|" & _
        '                IIf(chkBold.Checked = True, 1, 0) & "|" & _
        '                ddlFont.SelectedItem.Value

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode,
                                                  strParamName,
                                                  strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CP_CLSTRX_REKONSILEHDR_SAVE&errmesg=" & lblErrMessage.Text & "&redirect=CB/trx/CB_trx_Rekonsilelist")
        End Try

        If lblRowID.Text <> "" Then
            TmplList.EditItemIndex = -1
        End If

        lblRowID.Text = ""

        txtDescription1.Text = ""
        txtDescription2.Text = ""
        txtRefNo1.Text = ""
        txtFormula1.Text = ""
        txtRefNo2.Text = ""
        radGroupID.SelectedIndex = 0
        radGroupID.Text = ""
        Add.Visible = True
        Update.Visible = False

        BindGrid()

    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim Label As Label
        Dim strOpCode As String = "GL_SETUP_FSTEMPLDTLLN_GET"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim dsLine As Object
        Label = E.Item.FindControl("lblRowId")
        lblRowID.Text = Label.Text

        TmplList.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()

        'get detail info
        strParamName = "CODE|ROWID"
        strParamValue = txtCode.Text & "|" & lblRowID.Text

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCode,
                                                strParamName,
                                                strParamValue,
                                                dsLine)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_CLSTRX_REKONSILELN_GET&errmesg=" & lblErrMessage.Text & "&redirect=CB/trx/CB_trx_Rekonsilelist.aspx")
        End Try

        If dsLine.Tables(0).Rows.Count > 0 Then
            lblRowID.Text = Trim(dsLine.Tables(0).Rows(0).Item("RowID"))
            txtDispSeq1.Text = Trim(dsLine.Tables(0).Rows(0).Item("DispSeq"))
            txtDescription1.Text = Trim(dsLine.Tables(0).Rows(0).Item("Description"))
            txtDescription2.Text = Trim(dsLine.Tables(0).Rows(0).Item("Description2"))
            txtRefNo1.Text = Trim(dsLine.Tables(0).Rows(0).Item("RefNo"))
            txtRefNo2.Text = Trim(dsLine.Tables(0).Rows(0).Item("RefNoRvs"))
            txtFormula1.Text = Trim(dsLine.Tables(0).Rows(0).Item("Formula"))
            ddlFSRowType1.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("RowType"))

            ddlSpace1.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("FSpace"))
            chkUnderline.Checked = IIf(dsLine.Tables(0).Rows(0).Item("FUnderline") = 1, True, False)
            chkBegBal.Checked = IIf(dsLine.Tables(0).Rows(0).Item("BegBalance") = 1, True, False)
            chkBold.Checked = IIf(dsLine.Tables(0).Rows(0).Item("FBold") = 1, True, False)
            ddlFont.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("FFont"))

            ddlFSpacing.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("FSpace"))
            ddlFEffect.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("FUnderline"))
            ddlFStyle.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("FBold"))
            ddlFSize.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("FFont"))
            ddlFNegSign.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("FNegSign"))
            radGroupID.SelectedValue = Trim(dsLine.Tables(0).Rows(0).Item("GroupID"))
            Call TypeDisplay()


        End If

        Add.Visible = False
        Update.Visible = True


    End Sub


    Sub ddlReportType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        trGroupID.Visible = False

        If ddlReportType.SelectedValue = "4" Then
            trGroupID.Visible = True
        End If
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim lbl As Label
        Dim txt As TextBox
        Dim list As DropDownList
        Dim strReportCode As String
        Dim strRowId As String

        strReportCode = Trim(txtCode.Text)
        lbl = E.Item.FindControl("lblRowId")
        strRowId = lbl.Text.Trim

        strParam = strReportCode & Chr(9) & strRowId
        Try
            intErrNo = objGLSetup.mtdDelFSTemplDtl(strOpCd_Del,
                                                    strOpCdDelAcc,
                                                    strCompany,
strLocation,
                                                    strUserId,
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTMPL_DEL&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_FS.aspx")
        End Try

        TmplList.EditItemIndex = -1
        BindGrid()
    End Sub

    'modif
    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        lblRowID.Text = ""

        txtDescription1.Text = ""
        txtDescription2.Text = ""
        txtRefNo1.Text = ""
        txtFormula1.Text = ""
        chkBegBal.Checked = False
        chkUnderline.Checked = False
        chkBold.Checked = False
        ddlFont.SelectedIndex = 3
        ddlSpace1.SelectedIndex = 0

        ddlFSize.SelectedIndex = 3
        ddlFSpacing.SelectedIndex = 0
        ddlFEffect.SelectedIndex = 0
        ddlFStyle.SelectedIndex = 0
        ddlFNegSign.SelectedIndex = 1

        Add.Visible = True
        Update.Visible = False

        TmplList.EditItemIndex = -1
        BindGrid()
    End Sub


    Sub BackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_FSList.aspx")
    End Sub


    Sub DeleteBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

    End Sub

    'modif
    Sub SaveBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)

        Dim strOpCd As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strDocCode As String
        Dim strReportType As String = ddlReportType.SelectedItem.Value

        If txtCode.Enabled = True Then
            strOpCd = "GL_CLSSETUP_FSTEMPL_ADD"
        Else
            strOpCd = "GL_CLSSETUP_FSTEMPL_UPD"
        End If

        If strReportType = "" Then
            lblErrReportType.Visible = True
            Exit Sub
        Else
            lblErrReportType.Visible = False
        End If


        strParam = txtCode.Text & "|" & strReportType & "|" & txtName.Text & "|" & txtDescription.Text & "|" & txtDescription3.Text

        Try
            intErrNo = objGLSetup.mtdUpdFSTempl(strOpCd,
                                                    strCompany,
                                                    strLocation,
                                                    strUserId,
                                                    strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_FSTEMPL_UPDATE&errmesg=" & Exp.Message & "&redirect=GL/Setup/GL_Setup_FSList.aspx")
        End Try

        DisplayData()
        BindGrid()
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblAccount.Text = GetCaption(objLangCap.EnumLangCap.Account)
        TmplList.Columns(8).HeaderText = lblAccount.Text
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm,
                                                 strCompany,
                                                 strLocation,
                                                 strUserId,
                                                 strAccMonth,
                                                 strAccYear,
                                                 objLangCapDs,
                                                 strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_FSTMPL_GETLANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_FS.aspx")
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

    Sub BindReportType(ByVal pv_strReportType As String)
        ddlReportType.Items.Clear()
        ddlReportType.Items.Add(New ListItem("Select Report Type", ""))
        ddlReportType.Items.Add(New ListItem(objGLSetup.mtdGetReportType(objGLSetup.EnumReportType.BalanceSheet), objGLSetup.EnumReportType.BalanceSheet))
        ddlReportType.Items.Add(New ListItem(objGLSetup.mtdGetReportType(objGLSetup.EnumReportType.ProfitLoss), objGLSetup.EnumReportType.ProfitLoss))
        ddlReportType.Items.Add(New ListItem(objGLSetup.mtdGetReportType(objGLSetup.EnumReportType.COGS), objGLSetup.EnumReportType.COGS))
        ddlReportType.Items.Add(New ListItem(objGLSetup.mtdGetReportType(objGLSetup.EnumReportType.Other), objGLSetup.EnumReportType.Other))

    End Sub

End Class
