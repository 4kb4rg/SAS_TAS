
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


Public Class GL_Journal_List : Inherits Page

    Protected WithEvents dgTx As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchStockTxID As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents srchTransTypeList As DropDownList
    Protected WithEvents SearchBtn As Button
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected WithEvents Usage As ImageButton
    Protected WithEvents ibPrint As ImageButton
    Protected WithEvents GetTax As ImageButton

    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objGLtx As New agri.GL.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdmin As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "GL_CLSTRX_JOURNAL_LIST_GET"
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
            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Usage.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Usage).ToString())
            ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())
            GetTax.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(GetTax).ToString())

            If SortExpression.Text = "" Then
                SortExpression.Text = "jrn.JournalID"
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
                BindTransType()
                BindGrid()
                BindPageList()
            End If

            'If intLocLevel = objAdmin.EnumLocLevel.HQ Then
            '    If intLevel > 1 Then
            '        GetTax.Visible = True
            '        GetTax.Attributes("onclick") = "javascript:return ConfirmAction('get data tax from all unit');"
            '    Else
            '        GetTax.Visible = False
            '    End If
            'Else
            '    GetTax.Visible = False
            'End If
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
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lblBalance As Label
        Dim intCnt As Integer

        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTx.PageSize)
        
        dgTx.DataSource = dsData
        If dgTx.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTx.CurrentPageIndex = 0
            Else
                dgTx.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgTx.DataBind()
        BindPageList()

        For intCnt = 0 To dgTx.Items.Count - 1
            lblBalance = dgTx.Items.Item(intCnt).FindControl("lblBalance")
            If lblBalance.Text > 0 Then
                dgTx.Items(intCnt).ForeColor = Drawing.Color.Red
            End If
        Next

        PageNo = dgTx.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgTx.PageCount
    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objGLtx.mtdGetJournalStatus(objGLtx.EnumJournalStatus.All), objGLtx.EnumJournalStatus.All))
        srchStatusList.Items.Add(New ListItem(objGLtx.mtdGetJournalStatus(objGLtx.EnumJournalStatus.Active), objGLtx.EnumJournalStatus.Active))
        srchStatusList.Items.Add(New ListItem(objGLtx.mtdGetJournalStatus(objGLtx.EnumJournalStatus.Deleted), objGLtx.EnumJournalStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objGLtx.mtdGetJournalStatus(objGLtx.EnumJournalStatus.Closed), objGLtx.EnumJournalStatus.Closed))
        srchStatusList.Items.Add(New ListItem(objGLtx.mtdGetJournalStatus(objGLtx.EnumJournalStatus.Posted), objGLtx.EnumJournalStatus.Posted))
        srchStatusList.SelectedIndex = 1
    End Sub

    Sub BindTransType()
        srchTransTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.All), objGLtx.EnumJournalTransactType.All))
        srchTransTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Adjustment), objGLtx.EnumJournalTransactType.Adjustment))
        srchTransTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.CreditNote), objGLtx.EnumJournalTransactType.CreditNote))
        srchTransTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.DebitNote), objGLtx.EnumJournalTransactType.DebitNote))
        srchTransTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Invoice), objGLtx.EnumJournalTransactType.Invoice))
        srchTransTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.WorkshopDistribution), objGLtx.EnumJournalTransactType.WorkshopDistribution))
        srchTransTypeList.Items.Add(New ListItem(objGLtx.mtdGetJournalTransactType(objGLtx.EnumJournalTransactType.Tax), objGLtx.EnumJournalTransactType.Tax))
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
        Dim StockCode As String
        Dim Desc As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strParam = srchStockTxID.Text & "|" & _
                    srchDesc.Text & "|" & _
                    srchUpdateBy.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    strLocation & "|" & _
                    strAccMonth & "|" & _
                    strAccYear & "|" & _
                    SortExpression.Text & "|" & _
                    sortcol.Text & "|" & _
                    srchTransTypeList.SelectedItem.Value
        Try
            intErrNo = objGLtx.mtdGetJournalList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_JOURNALLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_GLTrx_page.aspx")
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

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("../../GL/Trx/GL_Trx_Journal_Details.aspx")
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

    Sub btnGetDataTax_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strOpCd As String = "TX_CLSTRX_TAXVERIFIED_GETDATA_UNIT"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet
        Dim intCnt As Integer = 0

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value

        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|USERID"
        strParamValue = strAccMonth & "|" & strAccYear & "|" & strLocation & "|" & strUserId

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_JOURNALLIST&errmesg=" & lblErrMessage.Text & "&redirect=../EN/menu/menu_GLTrx_page.aspx")
        End Try

        BindGrid()
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
