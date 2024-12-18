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

Public Class NU_trx_CullList : Inherits Page

    Protected WithEvents dgTxList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblNurseryBlock As Label
    Protected WithEvents lblBatchNo As Label
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchTxID As TextBox
    Protected WithEvents srchBlkCode As TextBox
    Protected WithEvents srchBatchNo As TextBox
    Protected WithEvents srchAccPeriod As TextBox
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected objNUTrx As New agri.NU.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    
    Dim objTxDs As New DataSet()
    Dim objLangCapDs As New DataSet()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intNUAR As Integer
    Dim intConfigSetting As Integer
    Dim intErrNo As Integer
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_NUACCMONTH")
        strAccYear = Session("SS_NUACCYEAR")
        intNUAR = Session("SS_NUAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumNUAccessRights.NUCulling), intNUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()

            If SortExpression.Text = "" Then
                SortExpression.Text = "CullID"
            End If

            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()

        If objSysCfg.mtdHasConfigValue(objSysCfg.mtdGetConfigSetting(objSysCfg.EnumConfig.BlockCostLevel), intConfigSetting) = True Then
            lblNurseryBlock.Text = GetCaption(objLangCap.EnumLangCap.NurseryBlock) & lblCode.Text
        Else
            lblNurseryBlock.Text = GetCaption(objLangCap.EnumLangCap.NurserySubBlock) & lblCode.Text
        End If

        lblBatchNo.Text = GetCaption(objLangCap.EnumLangCap.BatchNo)

        dgTxList.Columns(1).HeaderText = lblNurseryBlock.Text
        dgTxList.Columns(2).HeaderText = lblBatchNo.Text
        dgTxList.Columns(3).HeaderText = GetCaption(objLangCap.EnumLangCap.CullType)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_TRX_CULLLIST_GET_LANGCAP&errmesg=" & Exp.ToString() & "&redirect=NU/trx/NU_trx_CullList.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = objLangCapDs.Tables(0).Rows(count).Item("TermCode") Then
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
        srchStatusList.Items.Add(New ListItem(objNUTrx.mtdGetCullStatus(objNUTrx.EnumCullStatus.All), objNUTrx.EnumCullStatus.All))
        srchStatusList.Items.Add(New ListItem(objNUTrx.mtdGetCullStatus(objNUTrx.EnumCullStatus.Closed), objNUTrx.EnumCullStatus.Closed))
        srchStatusList.Items.Add(New ListItem(objNUTrx.mtdGetCullStatus(objNUTrx.EnumCullStatus.Confirmed), objNUTrx.EnumCullStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objNUTrx.mtdGetCullStatus(objNUTrx.EnumCullStatus.Deleted), objNUTrx.EnumCullStatus.Deleted))

        srchStatusList.SelectedIndex = 2
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
        Dim strOppCode_Get As String = "NU_CLSTRX_CULL_GET"
        Dim strParam As String
        Dim intCnt As Integer

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If
        strAccYear = lstAccYear.SelectedItem.Value

        strParam = srchTxID.Text & "|" & _
                    srchBlkCode.Text & "|" & _
                    srchBatchNo.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    srchUpdateBy.Text & "|||" & _
                    SortExpression.Text & " " & SortCol.Text

        Try
            intErrNo = objNUTrx.mtdGetCull(strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strOppCode_Get, _
                                            strParam, _
                                            objTxDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=NU_CLSTRX_CULLLIST_GET&errmesg=" & Exp.ToString() & "&redirect=NU/trx/NU_trx_CullList.aspx")
        End Try

        For intCnt = 0 To objTxDs.Tables(0).Rows.Count - 1
            objTxDs.Tables(0).Rows(intCnt).Item("CullID") = objTxDs.Tables(0).Rows(intCnt).Item("CullID").Trim()
            objTxDs.Tables(0).Rows(intCnt).Item("BlkCode") = objTxDs.Tables(0).Rows(intCnt).Item("BlkCode").Trim()
            objTxDs.Tables(0).Rows(intCnt).Item("CullTypeCode") = objTxDs.Tables(0).Rows(intCnt).Item("CullTypeCode").Trim()
            objTxDs.Tables(0).Rows(intCnt).Item("Status") = objTxDs.Tables(0).Rows(intCnt).Item("Status").Trim()
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
        Response.Redirect("NU_trx_CullDetails.aspx")
    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objNUTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        lstAccYear.DataSource = objAccYearDs.Tables(0)
        lstAccYear.DataValueField = "AccYear"
        lstAccYear.DataTextField = "UserName"
        lstAccYear.DataBind()
        lstAccYear.SelectedIndex = intSelectedIndex - 1
    End Sub
End Class
