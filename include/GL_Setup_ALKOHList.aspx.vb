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


Public Class GL_Setup_ALKOHList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchVehCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblVehCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLSetup As New agri.GL.clsSetup()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLTrx As New agri.GL.ClsTrx()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer

    Dim strVehCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim strVehTypeTag As String
    Dim strVRATag As String

    Dim objGLSetupDs As New Object()
    Dim objLangCapDs As New Object()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicle), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "veh.AlkohCode"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)

        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgLine.DataBind()
        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgLine.PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objGLSetup.EnumVehicleStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objGLSetup.EnumVehicleStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next


    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = "ALOKASI OVERHEAD"
        lblVehCode.Text = "Alokasi " & lblCode.Text
        lblDescription.Text = "Deskripsi"

        dgLine.Columns(0).HeaderText = lblVehCode.Text
        dgLine.Columns(1).HeaderText = lblDescription.Text

        strVehCodeTag = lblVehCode.Text
        strDescTag = lblDescription.Text
        strTitleTag = lblTitle.Text
        strVehTypeTag = GetCaption(objLangCap.EnumLangCap.VehType) & lblCode.Text
        strVRATag = GetCaption(objLangCap.EnumLangCap.VRA)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_CLSSETUP_VEHICLE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_vehicle.aspx")
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

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCd_GET As String = "GL_CLSSETUP_ALKOH_LIST_GET"
        Dim strSrchVehCode As String
        Dim strSrchDesc As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchVehCode = IIf(srchVehCode.Text = "", "", srchVehCode.Text)
        strSrchDesc = IIf(srchDescription.Text = "", "", srchDescription.Text)
        strSrchStatus = IIf(srchStatus.SelectedItem.Value = "0", "", srchStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchVehCode & "|" & _
                   strSrchDesc & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"

        Try
            intErrNo = objGLSetup.mtdGetVehicle(strOpCd_GET, strLocation, strParam, objGLSetupDs, False)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_ALKOH_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objGLSetupDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objGLSetupDs.Tables(0).Rows.Count - 1
                objGLSetupDs.Tables(0).Rows(intCnt).Item("AlkohCode") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("AlkohCode"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("Description"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("Status"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objGLSetupDs
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strVehCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatus.SelectedItem.Value = "0", srchStatus.SelectedItem.Value, "")
        strVehCode = srchVehCode.Text
        strDescription = srchDescription.Text
        strUpdateBy = txtLastUpdate.Text
        strSortExp = SortExpression.Text
        strSortCol = SortCol.Text

        'Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_Vehicle.aspx?strVehCodeTag=" & strVehCodeTag & _
        '            "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & _
        '            "&strVehTypeTag=" & strVehTypeTag & "&strVRATag=" & strVRATag & "&strStatus=" & strStatus & _
        '            "&strVehCode=" & strVehCode & "&strDescription=" & strDescription & _
        '            "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
        '            "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgLine.CurrentPageIndex = 0
            Case "prev"
                dgLine.CurrentPageIndex = _
                    Math.Max(0, dgLine.CurrentPageIndex - 1)
            Case "next"
                dgLine.CurrentPageIndex = _
                    Math.Min(dgLine.PageCount - 1, dgLine.CurrentPageIndex + 1)
            Case "last"
                dgLine.CurrentPageIndex = dgLine.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "asc", "desc", "asc")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strOpCd_ADD As String = ""
        Dim strOpCd_UPD As String = "GL_CLSSETUP_ALKOH_LIST_UPD"
        Dim strParam As String = ""
        Dim strSelVehCode As String
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblLnId")
        strSelVehCode = lblDelText.Text

        Try
            strParamName = "STRUPDATE"
            strParamValue = "SET Status='" & objGLSetup.EnumVehicleStatus.Deleted & "', UpdateID='" & Trim(strUserId) & "', UpdateDate=GETDATE() " & _
                            "WHERE AlkohCode='" & Trim(strSelVehCode) & "' AND LocCode='" & Trim(strLocation) & "'"

            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_UPD, _
                                                     strParamName, _
                                                     strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_VEH_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_ALKOHDet.aspx")
    End Sub

End Class
