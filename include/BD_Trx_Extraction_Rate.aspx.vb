Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Public Class BD_Extraction_Rate : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchMonthYear As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblMonthYear As Label
    Protected WithEvents lblOER As Label
    Protected WithEvents lblKER As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label

    Protected objBD As New agri.BD.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "BD_CLSTRX_EXTRACTRATE_LIST_GET"
    Dim strOppCd_ADD As String = "BD_CLSTRX_EXTRACTRATE_LIST_ADD"
    Dim strOppCd_UPD As String = "BD_CLSTRX_EXTRACTRATE_LIST_UPD"
    Dim strOppCd_DEL As String = "BD_CLSTRX_EXTRACTRATE_LIST_DEL"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intADAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strValidateMonthYear As String

    Dim DocTitleTag As String
    Dim MonthYearTag As String
    Dim OERTag As String
    Dim KERTag As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.ADBudgeting), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "AccYear, AccMonth"
                sortcol.Text = "ASC"
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

        lblTitle.Text = "EXTRACTION RATE LIST"
        lblMonthYear.Text = "Period"
        strValidateMonthYear = lblPleaseEnter.Text & lblMonthYear.Text & "."

        EventData.Columns(0).HeaderText = lblMonthYear.Text

        DocTitleTag = lblTitle.Text
        MonthYearTag = "Period"
        OERTag = "OER (%)"
        KERTag = "KER (%)"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=BD_TRX_EXTRACTION_RATE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_Trx_Extraction_Rate.aspx")
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


    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EventData.CurrentPageIndex = 0
        EventData.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, EventData.PageSize)

        EventData.DataSource = dsData
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If

        EventData.DataBind()
        BindPageList()
        PageNo = EventData.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & EventData.PageCount
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = EventData.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex

    End Sub

    Sub BindStatusList(ByVal index As Integer)

        StatusList = EventData.Items.Item(index).FindControl("StatusList")
        StatusList.Items.Add(New ListItem(objBD.mtdGetExtractRateStatus(objBD.EnumExtractRateStatus.Active), objBD.EnumExtractRateStatus.Active))
        StatusList.Items.Add(New ListItem(objBD.mtdGetExtractRateStatus(objBD.EnumExtractRateStatus.Budgeted), objBD.EnumExtractRateStatus.Budgeted))

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objBD.mtdGetExtractRateStatus(objBD.EnumExtractRateStatus.All), objBD.EnumExtractRateStatus.All))
        srchStatusList.Items.Add(New ListItem(objBD.mtdGetExtractRateStatus(objBD.EnumExtractRateStatus.Active), objBD.EnumExtractRateStatus.Active))
        srchStatusList.Items.Add(New ListItem(objBD.mtdGetExtractRateStatus(objBD.EnumExtractRateStatus.Budgeted), objBD.EnumExtractRateStatus.Budgeted))
    End Sub

    Protected Function LoadData() As DataSet

        Dim MonthYear As String
        Dim UpdateBy As String
        Dim srchStatus As String
        Dim strParam As String
        Dim SearchStr As String
        Dim sortitem As String
        Dim intCnt As Integer


        SearchStr = " AND BD.Status like '" & IIf(Not srchStatusList.SelectedItem.Value = objBD.EnumExtractRateStatus.All, _
                       srchStatusList.SelectedItem.Value, "%") & "' "

        If Not srchMonthYear.Text = "" Then
            SearchStr = SearchStr & " AND (BD.AccMonth + '/' + BD.AccYear) like '" & srchMonthYear.Text & "%' "
        End If

        If Not srchUpdateBy.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & _
                        srchUpdateBy.Text & "%' "
        End If
        If InStr(SortExpression.Text, ",") <> 0 Then
            sortitem = "ORDER BY " & Replace(SortExpression.Text, ",", " " & sortcol.Text & ",") & " " & sortcol.Text
        Else
            sortitem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        End If
        strParam = sortitem & "|" & SearchStr

        Try
            intErrNo = objBD.mtdGetExtractionRateList(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_EXTRACTION_RATE&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_Trx_Extraction_Rate.aspx")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("MonthYear") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("MonthYear"))
                objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("Status") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Status"))
                objDataSet.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objDataSet
    End Function

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strMonthYear As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIf(Not srchStatusList.SelectedItem.Value = objBD.EnumExtractRateStatus.All, srchStatusList.SelectedItem.Value, "")
        strMonthYear = srchMonthYear.Text
        strUpdateBy = srchUpdateBy.Text
        strSortExp = SortExpression.Text
        strSortCol = sortcol.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/BD_Rpt_ExtractRateList.aspx?strStatus=" & strStatus & _
                       "&strMonthYear=" & strMonthYear & _
                       "&strUpdateBy=" & strUpdateBy & _
                       "&strSortExp=" & strSortExp & _
                       "&strSortCol=" & strSortCol & _
                       "&DocTitleTag=" & DocTitleTag & _
                       "&MonthYearTag=" & MonthYearTag & _
                       "&OERTag=" & OERTag & _
                       "&KERTag=" & KERTag & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                EventData.CurrentPageIndex = 0
            Case "prev"
                EventData.CurrentPageIndex = _
                    Math.Max(0, EventData.CurrentPageIndex - 1)
            Case "next"
                EventData.CurrentPageIndex = _
                    Math.Min(EventData.PageCount - 1, EventData.CurrentPageIndex + 1)
            Case "last"
                EventData.CurrentPageIndex = EventData.PageCount - 1
        End Select
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim Updbutton As LinkButton
        Dim validateMonthYear As RequiredFieldValidator


        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        BindStatusList(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(EditText.Text) = objBD.EnumExtractRateStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtMonthYear")
                EditText.ReadOnly = True
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
        End Select
        validateMonthYear = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateMonthYear")

        validateMonthYear.ErrorMessage = strValidateMonthYear

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim list As DropDownList
        Dim MonthYear As String
        Dim OER As String
        Dim KER As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim CreateDate As String

        EditText = E.Item.FindControl("txtMonthYear")
        MonthYear = EditText.Text
        EditText = E.Item.FindControl("txtOER")
        OER = EditText.Text
        EditText = E.Item.FindControl("txtKER")
        KER = EditText.Text
        list = E.Item.FindControl("StatusList")
        Status = list.SelectedItem.Value
        EditText = E.Item.FindControl("CreateDate")
        CreateDate = EditText.Text
        strParam = Replace(MonthYear, "/", "|") & "|" & _
                    OER & "|" & KER & "|" & _
                    Status & "|" & _
                    CreateDate

        Try
            intErrNo = objBD.mtdUpdExtractionRateList(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_EXTRACTION_RATE&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_Trx_Extraction_Rate.aspx")
        End Try

        If blnDupKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If CInt(E.Item.ItemIndex) = 0 And EventData.Items.Count = 1 And Not EventData.CurrentPageIndex = 0 Then
            EventData.CurrentPageIndex = EventData.PageCount - 2
            BindGrid()
            BindPageList()
        End If
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim EditText As TextBox
        Dim MonthYear As String
        Dim strMonth As String
        Dim strYear As String
        Dim OER As String
        Dim KER As String
        Dim Status As String
        Dim blnDupKey As Boolean = False
        Dim CreateDate As String
        Dim intError As Integer

        EditText = E.Item.FindControl("txtMonthYear")
        MonthYear = EditText.Text
        strMonth = Left(MonthYear, 2)
        strYear = Right(MonthYear, 4)


        Try
            intErrNo = objBD.mtdDelExtractionRate(strOppCd_DEL, _
                                                strMonth, _
                                                strYear, _
                                                strLocation, _
                                                intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_EXTRACTION_RATE&errmesg=" & lblErrMessage.Text & "&redirect=BD/Trx/BD_Trx_Extraction_Rate.aspx")
        End Try

        EventData.EditItemIndex = -1

        If CInt(E.Item.ItemIndex) = 0 And EventData.Items.Count = 1 And EventData.PageCount <> 1 Then
            EventData.CurrentPageIndex = EventData.PageCount - 2
        End If

        BindGrid()
        BindPageList()
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateMonthYear As RequiredFieldValidator


        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("MonthYear") = ""
        newRow.Item("OER") = 0.0
        newRow.Item("KER") = 0.0
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        EventData.DataSource = dataSet
        EventData.DataBind()
        BindPageList()

        EventData.CurrentPageIndex = EventData.PageCount - 1
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()
        BindStatusList(EventData.EditItemIndex)

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        validateMonthYear = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateMonthYear")


        validateMonthYear.ErrorMessage = strValidateMonthYear


    End Sub

End Class
