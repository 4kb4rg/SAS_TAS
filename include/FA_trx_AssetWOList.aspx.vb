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



Public Class FA_trx_AssetWOList : Inherits Page

    Protected WithEvents dgTxList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchTxID As TextBox
    'Protected WithEvents srchRefNo As TextBox
    Protected WithEvents srchAssetCode As TextBox
    Protected WithEvents srchAccMonth As TextBox
    Protected WithEvents srchAccYear As TextBox

    Protected WithEvents lblCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTxID As Label
    Protected WithEvents lblAssetCode As Label

    Protected objFASetup As New agri.FA.clsSetup()
    Protected objFATrx As New agri.FA.clsTrx()
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
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intFAAR = Session("SS_FAAR")

        'strAccMonth = Session("SS_SELACCMONTH")
        'strAccYear = Session("SS_SELACCYEAR")

        strAccMonth = Session("SS_FAACCMONTH")
        strAccYear = Session("SS_FAACCYEAR")


        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumFAAccessRights.FAWriteOff), intFAAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "AssetWOID"
            End If
            If Not Page.IsPostBack Then
                srchAccMonth.Text = strAccMonth
                srchAccYear.Text = strAccYear
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.AssetWO))
        lblTxID.Text = GetCaption(objLangCap.EnumLangCap.AssetWO) & " ID"
        lblAssetCode.Text = "Description" 'GetCaption(objLangCap.EnumLangCap.Asset) & lblCode.Text

        dgTxList.Columns(0).HeaderText = lblTxID.Text
        dgTxList.Columns(1).HeaderText = "Description" 'lblAssetCode.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_TRX_ASSETWO_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWOList.aspx")
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
        srchStatusList.Items.Add(New ListItem(objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Active), objFATrx.EnumAssetWOStatus.Active))
        srchStatusList.Items.Add(New ListItem(objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Confirmed), objFATrx.EnumAssetWOStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Deleted), objFATrx.EnumAssetWOStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.Closed), objFATrx.EnumAssetWOStatus.Closed))
        srchStatusList.Items.Add(New ListItem(objFATrx.mtdGetAssetWOStatus(objFATrx.EnumAssetWOStatus.All), objFATrx.EnumAssetWOStatus.All))
        srchStatusList.SelectedIndex = 4
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
        Dim strOppCode_Get As String = "FA_CLSTRX_ASSETWO_GET"
        Dim strParam As String
        Dim intCnt As Integer

        strParam = srchTxID.Text & "|" & _
                    srchAssetCode.Text & "|" & _
                    srchAccMonth.Text & "|" & _
                    srchAccYear.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    srchUpdateBy.Text & "|" & _
                    SortExpression.Text & " " & SortCol.Text

        Try
            intErrNo = objFATrx.mtdGetAssetWO(strOppCode_Get, strLocation, strParam, objTxDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=FA_CLSTRX_ASSETWOLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=FA/trx/FA_trx_AssetWOList.aspx")
        End Try

        For intCnt = 0 To objTxDs.Tables(0).Rows.Count - 1
            objTxDs.Tables(0).Rows(intCnt).Item("AssetWOID") = objTxDs.Tables(0).Rows(intCnt).Item("AssetWOID").Trim()
            objTxDs.Tables(0).Rows(intCnt).Item("Description") = objTxDs.Tables(0).Rows(intCnt).Item("Description").Trim()
            objTxDs.Tables(0).Rows(intCnt).Item("Status") = objTxDs.Tables(0).Rows(intCnt).Item("Status").Trim()
            objTxDs.Tables(0).Rows(intCnt).Item("UserName") = objTxDs.Tables(0).Rows(intCnt).Item("UserName").Trim()
        Next

        Return objTxDs
    End Function

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
        Response.Redirect("FA_trx_AssetWODetails.aspx")
    End Sub

End Class

