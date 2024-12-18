
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class GL_Setup_ChartOfAcc : Inherits Page


    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchAccCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblAccCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblType As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label
    Protected WithEvents ddlCOALevel As DropDownList
    Protected WithEvents cbExcel As CheckBox

    Protected WithEvents NewTBBtn As ImageButton

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLSetup As New agri.GL.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer

    Dim objGLSetupDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strAccCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim strAccGrpCodeTag As String
    Dim strAccTypeTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim intLocLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")
        intLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLAccount), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "acc.AccCode"
            End If
            If Not Page.IsPostBack Then
                AssignMaxLength()
                BindGrid()
                BindPageList()
            End If

            If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
                NewTBBtn.Visible = True
            Else
                NewTBBtn.Visible = True
            End If
        End If
    End Sub

    Sub AssignMaxLength()
        Dim strOpCd As String = "PWSYSTEM_CLSCONFIG_CONFIG_GET"
        Dim intErrNo As Integer
        Dim intMaxLen As Integer

        Try
            intErrNo = objSys.mtdGetConfigInfo(strOpCd, _
                                               strCompany, _
                                               strLocation, _
                                               strUserId, _
                                               objConfigDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACC_GETMAXLENGTH&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/GL_setup_chartofacc.aspx")
        End Try

        intMaxLen = CInt(Trim(objConfigDs.Tables(0).Rows(0).Item("AccCodeLen")))
        srchAccCode.maxlength = intMaxLen
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        dgLine.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim dsTemp As New DataSet()
        Dim lblCOALevel As Label

        Dim PageNo As Integer
        Dim PageCount As Integer

        dsTemp = LoadData
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgLine.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsTemp = LoadData
            End If
        End If

        dgLine.DataSource = dsTemp
        dgLine.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text = "Page " & PageNo & " of " & PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            Select Case CInt(dsTemp.Tables(0).Rows(intCnt).Item("Status"))
                Case objGLSetup.EnumAccStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objGLSetup.EnumAccStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next
 
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.Account))
        lblAccCode.text = GetCaption(objLangCap.EnumLangCap.Account) & lblCode.text
        lblDescription.text = GetCaption(objLangCap.EnumLangCap.AccDesc)


        dgLine.Columns(0).HeaderText = lblAccCode.Text
        dgLine.Columns(1).HeaderText = lblDescription.Text

        strAccCodeTag = lblAccCode.text
        strDescTag = lblDescription.text
        strTitleTag = lblTitle.text
        strAccGrpCodeTag = GetCaption(objLangCap.EnumLangCap.AccGrp) & lblCode.text
        strAccTypeTag = GetCaption(objLangCap.EnumLangCap.Account) & lblType.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCLIST_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_chartofacc.aspx")
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

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "GL_CLSSETUP_CHARTOFACCOUNT_LIST_GET"
        Dim strSrchAccCode As String
        Dim strSrchDesc As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strCOALevel As String

        Session("SS_PAGING") = lblCurrentIndex.Text

        strSrchAccCode = IIf(srchAccCode.Text = "", "", srchAccCode.Text)
        strSrchDesc = IIf(srchDescription.Text = "", "", srchDescription.Text)
        strSrchStatus = IIf(srchStatus.SelectedItem.Value = "0", "", srchStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strCOALevel = ddlCOALevel.SelectedItem.Value

        strParam = strSrchAccCode & "|" & _
                   strSrchDesc & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|" & _
                   IIf(Session("SS_COACENTRALIZED") = "1", "", strLocation) & "|" & _
                   strCOALevel

        Try
            intErrNo = objGLSetup.mtdGetAccount(strOpCd_GET, strParam, objGLSetupDs, False)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objGLSetupDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objGLSetupDs.Tables(0).Rows.Count - 1
                objGLSetupDs.Tables(0).Rows(intCnt).Item("AccCode") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("AccCode"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("Description"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objGLSetupDs
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strAccCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String
        Dim strExportToExcel As String

        strStatus = IIf(Not srchStatus.selectedItem.Value = "0", srchStatus.selectedItem.Value, "")
        strAccCode = srchAccCode.text
        strDescription = srchDescription.text
        strUpdateBy = txtLastUpdate.text
        strSortExp = SortExpression.text
        strSortCol = SortCol.text

        If cbExcel.Checked = True Then
            strExportToExcel = "1"
        Else
            strExportToExcel = "0"
        End If



        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_ChartOfAcc.aspx?strAccCodeTag=" & strAccCodeTag & _
                    "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & _
                    "&strAccGrpCodeTag=" & strAccGrpCodeTag & "&strAccTypeTag=" & strAccTypeTag & "&strStatus=" & strStatus & _
                    "&strAccCode=" & strAccCode & "&strDescription=" & strDescription & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&ExportToExcel=" & strExportToExcel & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                    Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                    Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strOpCd_ADD As String = ""
        Dim strOpCd_UPD As String = "GL_CLSSETUP_CHARTOFACCOUNT_LIST_UPD"
        Dim strParam As String = ""

        Dim strSelAccCode As String
        Dim intErrNo As Integer

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblLnId")
        strSelAccCode = lblDelText.Text


        strParam = strSelAccCode & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & objGLSetup.EnumAccStatus.Deleted & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9)
        Try
            intErrNo = objGLSetup.mtdUpdAccount(strOpCd_ADD, _
                                                strOpCd_UPD, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                "", 0, 0, 0, 0, "", "", _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_CHARTOFACCOUNT_DEL&errmesg=" & Exp.ToString() & "&redirect=gl/setup/gl_setup_chartofacc.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_ChartOfAccDet.aspx")
    End Sub

    Sub dgLine_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

End Class
