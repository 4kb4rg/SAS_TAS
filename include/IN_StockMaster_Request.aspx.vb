Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.IN
Imports agri.PWSystem.clsLangCap


Public Class IN_StockMaster_Request : Inherits Page
    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objCBTrx As New agri.CB.clsTrx()
    Protected WithEvents EventData As DataGrid
    Protected WithEvents ErrorMessage As Label
    Protected WithEvents lblErrMesage As Label

    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SQLStatement As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents SrchSearchByMonth As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

    Protected WithEvents ibNew As ImageButton

    Protected objIN As New agri.IN.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()


    Dim strOppCd_GET As String = "IN_CLSSETUP_INVMASTER_DETAILS_REQUEST_LIST_GET"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim objItemDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strStockItemCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String

    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strLocLevel As String
    Dim nI As Integer = 0
    Dim intLevel As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim nMonth As Integer

    Private nColItemCode As Byte = 1
    Private nColDocId As Byte = 2


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLocType = Session("SS_LOCTYPE")
        strLocLevel = Session("SS_LOCLEVEL")
        intLevel = Session("SS_USRLEVEL")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
            'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INStockItemMaster), intINAR) = False Then
            '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "ItemCode"
            End If

            If Not Page.IsPostBack Then
                BindAccMonth()

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
        'lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.StockMaster))
        'lblStockItemCode.Text = GetCaption(objLangCap.EnumLangCap.StockItem) & lblCode.Text
        'lblDescription.Text = GetCaption(objLangCap.EnumLangCap.StockItemDesc)

        EventData.Columns(0).HeaderText = "Stock ItemCode" 'lblStockItemCode.Text
        EventData.Columns(1).HeaderText = "Description" 'lblDescription.Text

        'strStockItemCodeTag = lblStockItemCode.Text
        'strDescTag = lblDescription.Text
        '    strTitleTag = lblTitle.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=IN/setup/IN_setup_stockmaster.aspx")
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

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCurrentIndex.Text = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    '#### GRID SECTION -S --------------------------------------
    '-------Bind dataset to Datagrid---------------
    Sub BindGrid()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim ni As Integer

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), EventData.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsData = LoadData()
            End If
        End If

        EventData.DataSource = dsData
        EventData.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text = "Page " & PageNo & " of " & PageCount

        With EventData
            For ni = 0 To .Items.Count - 1 'Deleted 
                If Len(CType(EventData.Items(ni).FindControl("LblItemCode"), Label).Text.Trim) = 0 Then
                    CType(EventData.Items(ni).FindControl("BtnCreateItem"), Button).Enabled = True
                Else
                    CType(EventData.Items(ni).FindControl("BtnCreateItem"), Button).Enabled = False
                End If

                If CType(EventData.Items(ni).FindControl("lblstatus"), Label).Text = "Deleted" Then
                    CType(EventData.Items(ni).FindControl("BtnCreateItem"), Button).Enabled = False
                End If
            Next
        End With
    End Sub

    '-------Calls the com to create a dataset  --------------------
    Protected Function LoadData() As DataSet
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strSrchStatus As String

        Session("SS_PAGING") = lblCurrentIndex.Text

        If intLevel = 0 Then
            strSrchStatus = IIf(srchStatusList.SelectedItem.Value = "0", "1,2,3,4,5,6,9", srchStatusList.SelectedItem.Value)
        Else
            strSrchStatus = IIf(srchStatusList.SelectedItem.Value = "0", "1,2,3,4,5,6,9", srchStatusList.SelectedItem.Value)
        End If

        strParamName = "LOCCODE|TAHUN|BULAN|STATUS"
        strParamValue = strLocation & "|" & lstAccYear.SelectedValue & "|" & lstAccMonth.SelectedValue & "|" & strSrchStatus

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOppCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objItemDs

    End Function

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
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMesage.Text & "&redirect=")
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

    Sub BindAccMonth()
        For nMonth = 1 To 12
            lstAccMonth.Items.Add(nMonth)
        Next
    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.All), objCBTrx.EnumCashBankStatus.All))
        srchStatusList.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.Active), objCBTrx.EnumCashBankStatus.Active))
        srchStatusList.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.Confirmed), objCBTrx.EnumCashBankStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objCBTrx.mtdGetCashBankStatus(objCBTrx.EnumCashBankStatus.Deleted), objCBTrx.EnumCashBankStatus.Deleted))
    End Sub

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

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strStockCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatusList.SelectedItem.Value = objIN.EnumStockItemStatus.All, srchStatusList.SelectedItem.Value, "")
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/IN_Rpt_StockMaster.aspx?strStockItemCodeTag=" & strStockItemCodeTag & _
                    "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & _
                    "&strStatus=" & strStatus & _
                    "&strStockCode=" & strStockCode & "&strDescription=" & strDescription & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
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

    Sub BtnCreateItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        If intLevel > 0 Then
            Session("DOCID") = CType(dgItem.Cells(nColDocId).FindControl("lblNodoc"), Label).Text
            Response.Redirect("IN_StockMaster_RequestDetailAdd.aspx")
        End If
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
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("IN_StockMaster_Request_Detail.aspx")
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