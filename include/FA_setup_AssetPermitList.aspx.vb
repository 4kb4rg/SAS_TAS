Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Public Class FA_setup_AssetPermitList : Inherits Page

    Protected WithEvents dgTxList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchAssetCode As TextBox

    Protected WithEvents lblCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblAssetCode As Label

    Protected objFASetup As New agri.FA.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intFAAR As Integer
    Dim objTxDs As New DataSet()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer

    Dim AssetCodeTag As String
    Dim AssetDescTag As String
    Dim AssetHeaderTag As String
    Dim AssetAddPermTag As String
    Dim AssetGenDeprPermTag As String
    Dim AssetManDeprPermTag As String
    Dim AssetDispPermTag As String
    Dim AssetWOPermTag As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intFAAR = Session("SS_FAAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAPermissionSetup), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "FA.AssetCode"
            End If
            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AssetPermit))
        lblAssetCode.Text = GetCaption(objLangCap.EnumLangCap.Asset) & lblCode.Text

        AssetCodeTag = GetCaption(objLangCap.EnumLangCap.Asset) & lblCode.Text
        AssetHeaderTag = GetCaption(objLangCap.EnumLangCap.AssetHeader)
        AssetDescTag = GetCaption(objLangCap.EnumLangCap.AssetDesc)
        AssetAddPermTag = GetCaption(objLangCap.EnumLangCap.AssetAddPerm)
        AssetGenDeprPermTag = GetCaption(objLangCap.EnumLangCap.AssetGenDeprPerm)
        AssetManDeprPermTag = GetCaption(objLangCap.EnumLangCap.AssetManDeprPerm)
        AssetDispPermTag = GetCaption(objLangCap.EnumLangCap.AssetDispPerm)
        AssetWOPermTag = GetCaption(objLangCap.EnumLangCap.AssetWOPerm)

        dgTxList.Columns(0).HeaderText = lblAssetCode.Text
        dgTxList.Columns(1).HeaderText = GetCaption(objLangCap.EnumLangCap.AssetDesc)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_SETUP_ASSETPERMIT_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/Setup/FA_Setup_AssetPermitList.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function


    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetPermitStatus(objFASetup.EnumAssetPermitStatus.Active), objFASetup.EnumAssetPermitStatus.Active))
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetPermitStatus(objFASetup.EnumAssetPermitStatus.Deleted), objFASetup.EnumAssetPermitStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objFASetup.mtdGetAssetPermitStatus(objFASetup.EnumAssetPermitStatus.All), objFASetup.EnumAssetPermitStatus.All))
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgTxList.CurrentPageIndex = 0
        dgTxList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim intStatus As Integer

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTxList.PageSize)

        dgTxList.DataSource = dsData
        If dgTxList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTxList.CurrentPageIndex = 0
            Else
                dgTxList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgTxList.DataBind()
        BindPageList()
        PageNo = dgTxList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgTxList.PageCount

        intStatus = CInt(srchStatusList.SelectedItem.Value)

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgTxList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgTxList.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "FA_CLSSETUP_ASSETPERMIT_GET"
        Dim strParam As String
        Dim intCnt As Integer

        strParam = srchAssetCode.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    srchUpdateBy.Text & "|" & _
                    SortExpression.Text & " " & SortCol.Text

        Try
            intErrNo = objFASetup.mtdGetAssetPermit(strOppCode_Get, strLocation, strParam, objTxDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSSETUP_ASSETPERMITLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/setup/FA_setup_AssetPermitList.aspx")
        End Try

        For intCnt = 0 To objTxDs.Tables(0).Rows.Count - 1
            objTxDs.Tables(0).Rows(intCnt).Item("AssetCode") = objTxDs.Tables(0).Rows(intCnt).Item("AssetCode").Trim()
            objTxDs.Tables(0).Rows(intCnt).Item("Status") = objTxDs.Tables(0).Rows(intCnt).Item("Status").Trim()
            objTxDs.Tables(0).Rows(intCnt).Item("UserName") = objTxDs.Tables(0).Rows(intCnt).Item("UserName").Trim()
        Next

        Return objTxDs
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strAssetCode As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = srchStatusList.SelectedItem.Value
        strAssetCode = srchAssetCode.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = "Reg.AssetHeaderCode, " & SortExpression.Text
        strSortCol = SortCol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/FA_rpt_AssetPermit.aspx?strStatus=" & strStatus & _
                       "&strAssetCode=" & strAssetCode & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & lblTitle.Text & _
                       "&AssetCodeTag=" & AssetCodeTag & _
                       "&AssetHeaderTag=" & AssetHeaderTag & _
                       "&AssetDescTag=" & AssetDescTag & _
                       "&AssetAddPermTag=" & AssetAddPermTag & _
                       "&AssetGenDeprPermTag=" & AssetGenDeprPermTag & _
                       "&AssetManDeprPermTag=" & AssetManDeprPermTag & _
                       "&AssetDispPermTag=" & AssetDispPermTag & _
                       "&AssetWOPermTag=" & AssetWOPermTag & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgTxList.CurrentPageIndex = 0
            Case "prev"
                dgTxList.CurrentPageIndex = _
                Math.Max(0, dgTxList.CurrentPageIndex - 1)
            Case "next"
                dgTxList.CurrentPageIndex = _
                Math.Min(dgTxList.PageCount - 1, dgTxList.CurrentPageIndex + 1)
            Case "last"
                dgTxList.CurrentPageIndex = dgTxList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgTxList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgTxList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgTxList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgTxList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dgTxList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("FA_setup_AssetPermitDetails.aspx")
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
