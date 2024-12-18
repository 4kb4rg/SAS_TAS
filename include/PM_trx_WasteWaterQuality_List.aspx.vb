
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

Public Class PM_WastedWaterQualityList : Inherits Page

    Protected WithEvents dgWasteWQList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents txtTestSample As TextBox
    Protected WithEvents txtdate As System.Web.UI.WebControls.TextBox

    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lblFmt As Label
    Protected WithEvents lblDate As Label
    Protected WithEvents lblDupMsg As Label

    Protected objPMTrx As New agri.PM.clsTrx()
    Protected objPMSetup As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objWasteWQ As New DataSet
    Dim objTestSample as New DataSet()
    Dim objMachine As New DataSet()
    Protected WithEvents btnSelDateFrom As System.Web.UI.WebControls.Image
    Protected WithEvents SearchBtn As System.Web.UI.WebControls.Button
    Protected WithEvents btnPrev As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btnNext As System.Web.UI.WebControls.ImageButton
    Protected WithEvents NewBtn As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ibPrint As System.Web.UI.WebControls.ImageButton
    Protected WithEvents srchPondNo As System.Web.UI.WebControls.DropDownList
    Dim strDateFormat As String

    Private Sub InitializeComponent()

    End Sub

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMWastedWaterQuality), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransDate"
            End If

            If Not Page.IsPostBack Then
                BindSearchList()
                BindPondNumberList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetWastedWaterQualityStatus(objPMTrx.EnumWastedWaterQualityStatus.All), objPMTrx.EnumWastedWaterQualityStatus.All))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetWastedWaterQualityStatus(objPMTrx.EnumWastedWaterQualityStatus.Active), objPMTrx.EnumWastedWaterQualityStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPMTrx.mtdGetWastedWaterQualityStatus(objPMTrx.EnumWastedWaterQualityStatus.Deleted), objPMTrx.EnumWastedWaterQualityStatus.Deleted))
        srchStatusList.SelectedIndex = 1
    End Sub

    Private Sub BindPondNumberList()
        Dim enumPond As agri.PM.clsTrx.EnumPondNumber
        Dim item() As ListItem = New ListItem(16) {}

        item(0) = New ListItem("All", "")
        item(1) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo1), enumPond.LNo1)
        item(2) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo2), enumPond.LNo2)
        item(3) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo3), enumPond.LNo3)
        item(4) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo4), enumPond.LNo4)
        item(5) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo5), enumPond.LNo5)
        item(6) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo6), enumPond.LNo6)
        item(7) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo7), enumPond.LNo7)
        item(8) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo8), enumPond.LNo8)
        item(9) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo9), enumPond.LNo9)
        item(10) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo10), enumPond.LNo10)
        item(11) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo11), enumPond.LNo11)
        item(12) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo12), enumPond.LNo12)
        item(13) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo13), enumPond.LNo13)
        item(14) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo14), enumPond.LNo14)
        item(15) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo15), enumPond.LNo15)
        item(16) = New ListItem(objPMTrx.mtdGetPondNumber(enumPond.LNo16), enumPond.LNo16)

        srchPondNo.Items.AddRange(item)
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgWasteWQList.CurrentPageIndex = 0
        dgWasteWQList.EditItemIndex = -1
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
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgWasteWQList.PageSize)

        If SortExpression.Text.Trim() = "PondNo" Then
            Dim dView As New DataView(dsData.Tables(0))
            dView.Sort = "PondNo " & SortCol.Text
            dgWasteWQList.DataSource = dView
        Else
            dgWasteWQList.DataSource = dsData
        End If

        If dgWasteWQList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgWasteWQList.CurrentPageIndex = 0
            Else
                dgWasteWQList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgWasteWQList.DataBind()
        BindPageList()
        PageNo = dgWasteWQList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgWasteWQList.PageCount

        intStatus = CInt(srchStatusList.SelectedItem.Value)

        For intCnt = 0 To dgWasteWQList.Items.Count - 1
            Select Case intStatus
                Case objPMTrx.EnumWastedWaterQualityStatus.Active
                    lbButton = dgWasteWQList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPMTrx.EnumWastedWaterQualityStatus.Deleted
                    lbButton = dgWasteWQList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList

        While Not count = dgWasteWQList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgWasteWQList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String
        Dim strDate As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSrchTestSample As String
        Dim strSrchPondNo As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strDate = IIf(txtdate.Text = "", "", CheckDate())
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objPMTrx.EnumWastedWaterQualityStatus.All, "", srchStatusList.SelectedItem.Value)
        strSrchLastUpdate = srchUpdateBy.Text
        strSrchTestSample = txtTestSample.Text
        strSrchPondNo = srchPondNo.SelectedItem.Value

        strOppCode_Get = "PM_CLSTRX_WASTE_WATER_QUALITY_LIST_GET"

        'strParam = " AND WWQ.LocCode = '" & strLocation & "' AND WWQ.AccMonth = '" & strAccMonth & "' AND WWQ.AccYear = '" & strAccYear & "'" & _
        '           " AND TS.LocCode = '" & strLocation & "'"


	 strParam = " AND WWQ.LocCode = '" & strLocation & "' AND WWQ.AccYear = '" & strAccYear & "'" & _
                   " AND TS.LocCode = '" & strLocation & "'"


        If strDate <> "" Then
            strParam = strParam & " AND WWQ.TransDate = '" & strDate & "'"
        End If
        If strSrchStatus <> "" Then
            strParam = strParam & " AND WWQ.Status = '" & strSrchStatus & "'"
        End If
        If strSrchLastUpdate <> "" Then
            strParam = strParam & " AND USR.UserName LIKE '" & strSrchLastUpdate & "'"
        End If
        If strSrchTestSample <> "" Then
            strParam = strParam & " AND WWQ.TestSampleCode = '" & strSrchTestSample & "'"
        End If
        If strSrchPondNo <> "" Then
            strParam = strParam & " AND WWQ.PondNo = '" & strSrchPondNo & "'"
        End If
        strParam = " ORDER BY WWQ." & SortExpression.Text.Trim & " " & SortCol.Text & "|" & strParam
        Try
            intErrNo = objPMTrx.mtdGetTransactionList(strOppCode_Get, strParam, objWasteWQ)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_CLSTRX_WASTE_WATER_QUALITY_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/trx/pm_trx_WasteWaterQuality_list.asp.aspx")
        End Try

        Dim dColumn As New DataColumn("PondNoDesc")
        dColumn.AllowDBNull = True
        dColumn.DataType = Type.GetType("System.String")
        objWasteWQ.Tables(0).Columns.Add(dColumn)

        For intCnt = 0 To objWasteWQ.Tables(0).Rows.Count - 1
            objWasteWQ.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objWasteWQ.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objWasteWQ.Tables(0).Rows(intCnt).Item("Status") = Trim(objWasteWQ.Tables(0).Rows(intCnt).Item("Status"))
            objWasteWQ.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objWasteWQ.Tables(0).Rows(intCnt).Item("UpdateID"))
            objWasteWQ.Tables(0).Rows(intCnt).Item("TestSample") = Trim(objWasteWQ.Tables(0).Rows(intCnt).Item("TestSample"))

            If Not IsDBNull(objWasteWQ.Tables(0).Rows(intCnt).Item("PondNo")) AndAlso Trim(objWasteWQ.Tables(0).Rows(intCnt).Item("PondNo")) <> "" Then
                objWasteWQ.Tables(0).Rows(intCnt).Item("PondNoDesc") = objPMTrx.mtdGetPondNumber(Trim(objWasteWQ.Tables(0).Rows(intCnt).Item("PondNo")))
            Else
                objWasteWQ.Tables(0).Rows(intCnt).Item("PondNoDesc") = ""
            End If
        Next

        Return objWasteWQ
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgWasteWQList.CurrentPageIndex = 0
            Case "prev"
                dgWasteWQList.CurrentPageIndex = _
                Math.Max(0, dgWasteWQList.CurrentPageIndex - 1)
            Case "next"
                dgWasteWQList.CurrentPageIndex = _
                Math.Min(dgWasteWQList.PageCount - 1, dgWasteWQList.CurrentPageIndex + 1)
            Case "last"
                dgWasteWQList.CurrentPageIndex = dgWasteWQList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgWasteWQList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgWasteWQList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgWasteWQList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgWasteWQList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_trx_WasteWaterQuality_Det.aspx")
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strSelectedLocCode As String
        Dim strSelectedTransDate As String
        Dim strSelectedTestSample As String
        Dim strSelectedPondNo As String
        Dim QString As String

        Dim lblLocCode As Label
        Dim lblTransDate As Label
        Dim lblTestSample As Label
        Dim lblPondNo As Label

        dgWasteWQList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblLocCode = dgWasteWQList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strSelectedLocCode = lblLocCode.Text
        lblTransDate = dgWasteWQList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransDate")
        strSelectedTransDate = lblTransDate.Text
        lblTestSample = dgWasteWQList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTestSample")
        strSelectedTestSample = lblTestSample.Text

        lblPondNo = dgWasteWQList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblPondNo")
        strSelectedPondNo = lblPondNo.Text
        QString = "?LocCode=" & Server.UrlEncode(Trim(strSelectedLocCode)) & _
                  "&TransDate=" & Server.UrlEncode(Trim(objGlobal.GetLongDate(strSelectedTransDate))) & _
                  "&TestSample=" & Server.UrlEncode(Trim(strSelectedTestSample)) & _
                  "&PondNo=" & Server.UrlEncode(Trim(strSelectedPondNo)) & _
                  "&Edit=True"
        Response.Redirect("PM_trx_WasteWaterQuality_Det.aspx" & QString)
    End Sub


    Protected Function CheckDate() As String

        Dim strDateSetting As String
        Dim objSysCfgDs As DataSet
        Dim objDateFormat As String
        Dim strValidDate As String
        lblFmt.Visible = False
        lblDate.Visible = False
        lblFmt.Visible = False
        If Not txtdate.Text = "" Then
            If objGlobal.mtdValidInputDate(strDateFormat, txtdate.Text, objDateFormat, strValidDate) = True Then
                Return strValidDate
            Else
                lblFmt.Visible = True
                lblFmt.Text = objDateFormat & "."
                lblDate.Visible = True
                lblFmt.Visible = True

            End If
        End If
    End Function

End Class
