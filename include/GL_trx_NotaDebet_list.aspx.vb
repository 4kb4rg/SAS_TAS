
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


Public Class GL_trx_NotaDebet_list : Inherits Page

    Protected WithEvents dgTx As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchDocID As TextBox
    Protected WithEvents srchDescr As TextBox
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents SearchBtn As Button
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected WithEvents Usage As ImageButton
    Protected WithEvents ibPrint As ImageButton
    Protected WithEvents GetTax As ImageButton

    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objGLtx As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdmin As New agri.Admin.clsLoc()
    Dim objPU As New agri.PU.clsTrx()

    Dim strOppCd_GET As String = "GL_STDRPT_NOTADEBETANTARUNIT_LIST"
    Dim objDataSet As New DataSet()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer
    Dim intLocLevel As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intGLAR = Session("SS_GLAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        intLevel = Session("SS_USRLEVEL")
        intLocLevel = Session("SS_LOCLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournal), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            lblErrMessage.Visible = False

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindLocation("")
                'BindGrid()
                'BindPageList()
            End If

        End If
    End Sub

    Sub onload_GetLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim objLangCapDs As New DataSet()
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode & "|" & objGlobal.mtdGetModuleCode(objGlobal.EnumModule.Inventory)
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=../../menu/menu_GLTrx_page.aspx")
        End Try


    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgTx.CurrentPageIndex = 0
        dgTx.EditItemIndex = -1
        BindGrid()
        'BindPageList()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lbl As Label
        Dim intCnt As Integer

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTx.PageSize)

        dgTx.DataSource = dsData
        'If dgTx.CurrentPageIndex >= PageCount Then
        '    If PageCount = 0 Then
        '        dgTx.CurrentPageIndex = 0
        '    Else
        '        dgTx.CurrentPageIndex = PageCount - 1
        '    End If
        'End If

        dgTx.DataBind()
        'BindPageList()

        For intCnt = 0 To dgTx.Items.Count - 1
            lbl = dgTx.Items.Item(intCnt).FindControl("lblNoUrut")
            If Trim(lbl.Text) <> 99999 Then
                lbl = dgTx.Items.Item(intCnt).FindControl("lblDocIDFrom")
                If Trim(lbl.Text) = "" Then
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblDocDateFrom")
                    lbl.Visible = False
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblAmountFrom")
                    lbl.Visible = False
                End If
                lbl = dgTx.Items.Item(intCnt).FindControl("lblDocIDTo")
                If Trim(lbl.Text) = "" Then
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblDocDateTo")
                    lbl.Visible = False
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblAmountTo")
                    lbl.Visible = False
                End If
            Else
                lbl = dgTx.Items.Item(intCnt).FindControl("lblNoUrut")
                If Trim(lbl.Text) = 99999 Then
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblDescrFrom")
                    lbl.Font.Bold = True
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblAmountFrom")
                    lbl.Font.Bold = True
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblDescrTo")
                    lbl.Font.Bold = True
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblAmountTo")
                    lbl.Font.Bold = True
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblNoUrut")
                    lbl.Visible = False
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblDocDateFrom")
                    lbl.Visible = False
                    lbl = dgTx.Items.Item(intCnt).FindControl("lblDocDateTo")
                    lbl.Visible = False
                End If
            End If
        Next

        PageNo = dgTx.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgTx.PageCount
    End Sub


    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgTx.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgTx.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim SearchStr As String = ""
        Dim IntSearch As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""


        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        If Not Trim(srchDocID.Text) = "" Then
            SearchStr = " (DocIDFrom not like '%" & Trim(srchDocID.Text) & "%' OR DocIDTo not like '%" & Trim(srchDocID.Text) & "%') "
        End If

        If Not Trim(srchDescr.Text) = "" Then
            SearchStr = IIf(Trim(srchDocID.Text) = "", "", SearchStr & " AND ")
            SearchStr = SearchStr & " (DescrFrom not like '%" & Trim(srchDescr.Text) & "%' OR DescrTo not like '%" & Trim(srchDescr.Text) & "%') "
        End If

        If SearchStr <> "" Then
            SearchStr = " WHERE " & SearchStr
            IntSearch = 1
        Else
            IntSearch = 2
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|ANTARUNITLOC|SEARCHSTR|INTSEARCH"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & ddlLocation.SelectedItem.Value & "|" & Trim(SearchStr) & "|" & IntSearch

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOppCd_GET, strParamName, strParamValue, objDataSet)

        Catch Exp As System.Exception
            Response.Redirect("../../include/mesg/ErrorMessage.aspx?errcode=GL_STDRPT_TRIALBAL_TEMPORARY_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgTx.CurrentPageIndex = 0
            Case "prev"
                dgTx.CurrentPageIndex = _
                    Math.Max(0, dgTx.CurrentPageIndex - 1)
            Case "next"
                dgTx.CurrentPageIndex = _
                    Math.Min(dgTx.PageCount - 1, dgTx.CurrentPageIndex + 1)
            Case "last"
                dgTx.CurrentPageIndex = dgTx.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgTx.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgTx.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgTx.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgTx.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
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
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
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

    Sub BindLocation(ByVal pv_strLocCode As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strParam = "" & "|" & objAdmin.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            With objLocDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = strPRRefLocCode Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "-All-"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocation.DataSource = objLocDs.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex
    End Sub

    Sub dgTx_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Private Sub dgTx_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTx.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "No."
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
            dgItem.Cells.Add(dgCell)
            dgCell.Text = strLocation
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
            dgItem.Cells.Add(dgCell)
            dgCell.Text = IIf(ddlLocation.SelectedItem.Value = "", "-ALL-", ddlLocation.SelectedItem.Value)
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgTx.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgTx_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTx.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
        End If
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(lstAccMonth.SelectedItem.Value) = 1, "0" & Trim(lstAccMonth.SelectedItem.Value), Trim(lstAccMonth.SelectedItem.Value))
        strAccYear = lstAccYear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=NOTADEBET/" & Trim(strLocation) & "/" & Trim(strAccYear) & "/" & Trim(cAccMonth) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgTx.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
End Class
