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

Public Class GL_JournalAdj_List : Inherits Page

    Protected WithEvents dgResult As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents srchJournalAdjId As TextBox
    Protected WithEvents srchDesc As TextBox
    Protected WithEvents srchAccPeriod As TextBox
    Protected WithEvents lblErrAccPeriod As Label
    Protected WithEvents srchJournalAdjType As DropDownList
    Protected WithEvents lblErrMaxPeriod As Label
    Protected WithEvents SearchBtn As Button

    Protected WithEvents Usage As ImageButton
    Protected WithEvents ibPrint As ImageButton

    Protected objGLTrx As New agri.GL.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim strOppCd_GET As String = "GL_CLSTRX_JOURNALADJ_LIST_GET"
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

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLJournalAdj), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrAccPeriod.Visible = False
            If SortExpression.Text = "" Then
                SortExpression.Text = "JournalAdjID"
            End If

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Usage.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Usage).ToString())
            ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

            If Not Page.IsPostBack Then
                BindSearchList()
                BindTransType()
                BindGrid()
                BindPageList()
                if Request.QueryString("errmsg") <> "" then
                   lblErrMaxPeriod.Text = Request.QueryString("errmsg")
                    lblErrMaxPeriod.Visible = True
                end if
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_TRX_JRNADJ_LIST&errmesg=" & Exp.ToString & "&redirect=")
        End Try
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If srchAccPeriod.Text <> "" Then
            If InStr(srchAccPeriod.Text, "/") = 0 Then
                lblErrAccPeriod.Visible = True
                Exit Sub
            End If
        End If

        dgResult.CurrentPageIndex = 0
        dgResult.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgResult.PageSize)
        
        dgResult.DataSource = dsData
        If dgResult.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgResult.CurrentPageIndex = 0
            Else
                dgResult.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgResult.DataBind()
        BindPageList()
        PageNo = dgResult.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgResult.PageCount
        dsData = Nothing
    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.All), objGLTrx.EnumJournalAdjStatus.All))
        srchStatusList.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.Active), objGLTrx.EnumJournalAdjStatus.Active))
        srchStatusList.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.Deleted), objGLTrx.EnumJournalAdjStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.Posted), objGLTrx.EnumJournalAdjStatus.Posted))
        srchStatusList.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjStatus(objGLTrx.EnumJournalAdjStatus.Closed), objGLTrx.EnumJournalAdjStatus.Closed))
        srchStatusList.SelectedIndex = 1
    End Sub

    Sub BindTransType()
        srchJournalAdjType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.All), objGLTrx.EnumJournalAdjType.All))
        srchJournalAdjType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.Adjustment), objGLTrx.EnumJournalAdjType.Adjustment))
        srchJournalAdjType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.CreditNote), objGLTrx.EnumJournalAdjType.CreditNote))
        srchJournalAdjType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.DebitNote), objGLTrx.EnumJournalAdjType.DebitNote))
        srchJournalAdjType.Items.Add(New ListItem(objGLTrx.mtdGetJournalAdjType(objGLTrx.EnumJournalAdjType.Invoice), objGLTrx.EnumJournalAdjType.Invoice))
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgResult.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgResult.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim UpdateBy As String
        Dim strParam As String
        Dim objDataSet As New Dataset()

        strParam = srchJournalAdjId.Text & "|" & _
                    srchDesc.Text & "|" & _
                    srchAccPeriod.Text & "|" & _
                    Convert.ToString(srchStatusList.SelectedItem.Value) & "|" & _
                    strLocation & "|" & _
                    SortExpression.Text & "|" & _
                    sortcol.Text & "|" & _
                    srchJournalAdjType.SelectedItem.Value
        Try
            intErrNo = objGLTrx.mtdGetJournalAdjList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_JOURNALADJLIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try
        Return objDataSet
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgResult.CurrentPageIndex = 0
            Case "prev"
                dgResult.CurrentPageIndex = _
                    Math.Max(0, dgResult.CurrentPageIndex - 1)
            Case "next"
                dgResult.CurrentPageIndex = _
                    Math.Min(dgResult.PageCount - 1, dgResult.CurrentPageIndex + 1)
            Case "last"
                dgResult.CurrentPageIndex = dgResult.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgResult.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgResult.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgResult.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgResult.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub btnNewItm_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Response.Redirect("GL_Trx_JournalAdj_Details.aspx")
    End Sub


End Class
