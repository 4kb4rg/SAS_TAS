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

Public Class WM_FFBAssessList : Inherits Page

    Protected WithEvents dgFFBList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrDateInsp As Label
    Protected WithEvents lblErrDateInspMsg As Label
    Protected WithEvents lblErrLastUpdDate As Label
    Protected WithEvents lblErrLastUpdDateMsg As Label

    Protected WithEvents srchTicketNo As TextBox
    Protected WithEvents srchDateInsp As TextBox
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents srchUpdDate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList

    Protected objWMTrx As New agri.WM.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWMAR As Integer
    Dim strDateFormat As String

    Dim objFFBDs As New DataSet()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")
        strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMFFBAssessment), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDateInsp.Visible = False
            lblErrDateInspMsg.Visible = False
            lblErrLastUpdDate.Visible = False
            lblErrLastUpdDateMsg.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "TicketNo"
            End If

            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetFFBAssessStatus(objWMTrx.EnumFFBAssessStatus.Active), objWMTrx.EnumFFBAssessStatus.Active))
        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetFFBAssessStatus(objWMTrx.EnumFFBAssessStatus.Deleted), objWMTrx.EnumFFBAssessStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetFFBAssessStatus(objWMTrx.EnumFFBAssessStatus.All), objWMTrx.EnumFFBAssessStatus.All))

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgFFBList.CurrentPageIndex = 0
        dgFFBList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgFFBList.PageSize)
        
        dgFFBList.DataSource = dsData
        If dgFFBList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgFFBList.CurrentPageIndex = 0
            Else
                dgFFBList.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgFFBList.DataBind()
        BindPageList()
        PageNo = dgFFBList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgFFBList.PageCount


        For intCnt = 0 To dgFFBList.Items.Count - 1
            Status = dgFFBList.Items.Item(intCnt).FindControl("lblStatus")
            strStatus = Status.Text

            Select Case strStatus
                Case objWMTrx.mtdGetFFBAssessStatus(objWMTrx.EnumFFBAssessStatus.Active)
                    lbButton = dgFFBList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objWMTrx.mtdGetFFBAssessStatus(objWMTrx.EnumFFBAssessStatus.Deleted)
                    lbButton = dgFFBList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgFFBList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgFFBList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "WM_CLSTRX_FFB_ASSESS_GET"
        Dim strSrchTicketNo As String
        Dim strSrchDateInsp As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSrchLastUpdDate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String

        If Not srchDateInsp.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(srchDateInsp.Text), objFormatDate, objActualDate) = False Then
                lblErrDateInsp.Visible = True
                lblErrDateInsp.Text = lblErrDateInspMsg.Text & objFormatDate
                Exit Function
            Else
                strSrchDateInsp = objActualDate
            End If
        End If

        If Not srchUpdDate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, Trim(srchUpdDate.Text), objFormatDate, objActualDate) = False Then
                lblErrLastUpdDate.Visible = True
                lblErrLastUpdDate.Text = lblErrLastUpdDateMsg.Text & objFormatDate
                Exit Function
            Else
                strSrchLastUpdDate = objActualDate
            End If
        End If

        strSrchTicketNo = IIf(srchTicketNo.Text = "", "", srchTicketNo.Text)
        strSrchDateInsp = IIf(srchDateInsp.Text = "", "", objGlobal.GetShortDate(strDateFormat, strSrchDateInsp))
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objWMTrx.EnumFFBAssessStatus.All, "", srchStatusList.SelectedItem.Value)
        strSrchLastUpdate = IIf(srchUpdateBy.Text = "", "", srchUpdateBy.Text)
        strSrchLastUpdDate = IIf(srchUpdDate.Text = "", "", objGlobal.GetShortDate(strDateFormat, strSrchLastUpdDate))

        strParam = strSrchTicketNo & "|" & _
                   strSrchDateInsp & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   strSrchLastUpdDate & "||" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text

        Try
            intErrNo = objWMTrx.mtdGetFFBAssess(strOppCode_Get, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                objFFBDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_FFB_ASSESS_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_FFBAssessList.aspx")
        End Try

        For intCnt = 0 To objFFBDs.Tables(0).Rows.Count - 1
            objFFBDs.Tables(0).Rows(intCnt).Item("TicketNo") = Trim(objFFBDs.Tables(0).Rows(intCnt).Item("TicketNo"))
            objFFBDs.Tables(0).Rows(intCnt).Item("InspectedDate") = Trim(objFFBDs.Tables(0).Rows(intCnt).Item("InspectedDate"))
            objFFBDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objFFBDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objFFBDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objFFBDs.Tables(0).Rows(intCnt).Item("Status"))
            objFFBDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objFFBDs.Tables(0).Rows(intCnt).Item("UserName"))
            objFFBDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objFFBDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
        Next

        Return objFFBDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgFFBList.CurrentPageIndex = 0
            Case "prev"
                dgFFBList.CurrentPageIndex = _
                Math.Max(0, dgFFBList.CurrentPageIndex - 1)
            Case "next"
                dgFFBList.CurrentPageIndex = _
                Math.Min(dgFFBList.PageCount - 1, dgFFBList.CurrentPageIndex + 1)
            Case "last"
                dgFFBList.CurrentPageIndex = dgFFBList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgFFBList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgFFBList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgFFBList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgFFBList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_FFB_Assess_GET As String = "WM_CLSTRX_FFB_ASSESS_GET"
        Dim strOppCd_FFB_Assess_ADD As String = "WM_CLSTRX_FFB_ASSESS_ADD"
        Dim strOppCd_FFB_Assess_UPD As String = "WM_CLSTRX_FFB_ASSESS_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strTicketNo As String
        Dim lblTicketNo As Label

        dgFFBList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTicketNo = dgFFBList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTicketNo")
        strTicketNo = lblTicketNo.Text

        strParam = Trim(strTicketNo) & "|||||||||||||||" & objWMTrx.EnumFFBAssessStatus.Deleted & "||"
        Try
            intErrNo = objWMTrx.mtdUpdFFBAssess(strOppCd_FFB_Assess_ADD, _
                                                strOppCd_FFB_Assess_UPD, _
                                                strOppCd_FFB_Assess_GET, _
                                                strLocation, _
                                                strUserId, _
                                                strAccMonth, _
                                                strAccYear, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_FFB_ASSESS_DET_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_FFBAssessDet.aspx")
        End Try

        dgFFBList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewFFBAssessBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WM_trx_FFBAssessDet.aspx")
    End Sub



End Class
