Imports System
Imports System.Data
Imports System.Collections 
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic



Public Class WS_WorkCodeList : Inherits Page

    Protected WithEvents dgWorkCode As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtWorkCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents lblCode As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblWork As Label
    Protected WithEvents lblWorkDesc As Label

    
    Protected objWS As New agri.WS.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLoc As New agri.Admin.clsLoc()


    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer

    Dim lblServType As String
    Dim lblAccCode As String
    Dim lblBlkCode As String
    Dim strLocType as String

    Dim objWorkCodeDs As New Object()
    Dim objLangCapDs As New Object()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        intWSAR = Session("SS_WSAR")
        strLocType = Session("SS_LOCTYPE")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSWorkMaster), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "WorkCode"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblWork.Text = GetCaption(objLangCap.EnumLangCap.Work) 
        lblWorkDesc.Text = GetCaption(objLangCap.EnumLangCap.WorkDesc)
        lblTitle.Text = UCase(lblWork.Text) 


        lblAccCode = GetCaption(objLangCap.EnumLangCap.Account)  & lblCode.text
        lblBlkCode = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.text
        lblServType = GetCaption(objLangCap.EnumLangCap.ServType) & lblCode.text

        dgWorkCode.Columns(1).HeaderText = lblWork.Text & lblCode.Text
        dgWorkCode.Columns(2).HeaderText = lblWorkDesc.Text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_SETUP_WORK_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=ws/setup/ws_workcodelist.aspx")
        End Try

    End Sub



    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

       For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function




    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgWorkCode.CurrentPageIndex = 0
        dgWorkCode.EditItemIndex = -1
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
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgWorkCode.PageSize)

        dgWorkCode.DataSource = dsData
        If dgWorkCode.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgWorkCode.CurrentPageIndex = 0
            Else
                dgWorkCode.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgWorkCode.DataBind()
        BindPageList()
        PageNo = dgWorkCode.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgWorkCode.PageCount

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgWorkCode.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgWorkCode.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "WS_CLSSETUP_WORKCODE_LIST_GET"
        Dim strSrchWorkCode As String
        Dim strSrchDescription As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchWorkCode = IIf(txtWorkCode.Text = "", "", txtWorkCode.Text)
        strSrchDescription = IIf(txtDescription.Text = "", "", txtDescription.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchWorkCode & "|" & _
                   strSrchDescription & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text

        Try
            intErrNo = objWS.mtdGetWorkCode(strOpCd_GET, _
                                            strParam, _
                                            objWorkCodeDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WORKCODELIST_GET_WORKCODE&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objWorkCodeDs.Tables(0).Rows.Count - 1
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(0) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(0))
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(1) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(1))
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(5) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(5))
            objWorkCodeDs.Tables(0).Rows(intCnt).Item(3) = Trim(objWorkCodeDs.Tables(0).Rows(intCnt).Item(3))
        Next

        Return objWorkCodeDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgWorkCode.CurrentPageIndex = 0
            Case "prev"
                dgWorkCode.CurrentPageIndex = _
                    Math.Max(0, dgWorkCode.CurrentPageIndex - 1)
            Case "next"
                dgWorkCode.CurrentPageIndex = _
                    Math.Min(dgWorkCode.PageCount - 1, dgWorkCode.CurrentPageIndex + 1)
            Case "last"
                dgWorkCode.CurrentPageIndex = dgWorkCode.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgWorkCode.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgWorkCode.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgWorkCode.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgWorkCode.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub
    

    Sub NewWorkCodeBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WS_WorkCodeDet.aspx")
    End Sub

    Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strWorkCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIF(Not ddlStatus.selectedItem.Value = "0", ddlStatus.selectedItem.Value, "")
        strWorkCode = txtWorkCode.text
        strDescription = txtDescription.text
        strUpdateBy =  txtLastUpdate.text
        strSortExp = SortExpression.text
        strSortCol = SortCol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/WS_Rpt_WorkCodeList.aspx?strWorkCodeTag=" & lblWork.Text & lblCode.text & _
                    "&strDescTag=" & lblWorkDesc.Text & "&strTitleTag=" & lblTitle.Text & lblCode.text & _
                    "&strStatus=" & strStatus & "&strWorkCode=" & strWorkCode & "&strDescription=" & strDescription & _
                    "&strServTypeTag=" & lblServType & "&strAccCodeTag=" & lblAccCode & "&strBlkCodeTag=" & lblBlkCode & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub



End Class
