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


Public Class PR_Setup_MedicalList : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchMedicalCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchUpdBy As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrEnter As Label
    Protected WithEvents lblRate As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTitle2 As Label

    Protected objPR As New agri.PR.clsSetup()  
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "PR_CLSSETUP_MEDICAL_LIST_GET"
    Dim strOppCd_ADD As String = "PR_CLSSETUP_MEDICAL_LIST_ADD"    
    Dim strOppCd_UPD As String = "PR_CLSSETUP_MEDICAL_STATUS_UPD"

    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""

    Dim objAR As New agri.GlobalHdl.clsAccessRights()  
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRMedical), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")

        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "MedicalCode"
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
        lblTitle.Text = UCase(GetCaption(objLangCap.EnumLangCap.Medical))
        lblTitle2.Text = GetCaption(objLangCap.EnumLangCap.Medical)

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
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_setup_Route.aspx")
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

        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label

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

        GetEntireLangCap()     

        Dim col as HyperLinkColumn
        Dim col1 as HyperLinkColumn
        col = EventData.Columns(1)
        col1 = EventData.Columns(2)
        col.HeaderText = GetCaption(objLangCap.EnumLangCap.Medical) & " Code"
        col1.HeaderText = GetCaption(objLangCap.EnumLangCap.Medical) & " Description"

        EventData.DataBind()
        BindPageList()
        PageNo = EventData.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & EventData.PageCount

        For intCnt = 0 To EventData.Items.Count - 1
            lbl = EventData.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPR.EnumMedicalStatus.Active
                        lbButton = EventData.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPR.EnumMedicalStatus.Deleted
                        lbButton = EventData.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
            End Select
        Next
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
        StatusList.Items.Add(New ListItem(objPR.mtdGetMedicalStatus(objPR.EnumMedicalStatus.Active), objPR.EnumMedicalStatus.Active))
        StatusList.Items.Add(New ListItem(objPR.mtdGetMedicalStatus(objPR.EnumMedicalStatus.Deleted), objPR.EnumMedicalStatus.Deleted))

    End Sub

    Sub BindSearchList()
        srchStatus.Items.Add(New ListItem(objPR.mtdGetMedicalStatus(objPR.EnumMedicalStatus.All), objPR.EnumMedicalStatus.All))
        srchStatus.Items.Add(New ListItem(objPR.mtdGetMedicalStatus(objPR.EnumMedicalStatus.Active), objPR.EnumMedicalStatus.Active))
        srchStatus.Items.Add(New ListItem(objPR.mtdGetMedicalStatus(objPR.EnumMedicalStatus.Deleted), objPR.EnumMedicalStatus.Deleted))
        srchStatus.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet

        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String
        Dim intCnt As Integer


        SearchStr = " AND M.Status like '" & IIf(srchStatus.SelectedItem.Value <> objPR.EnumMedicalStatus.All, srchStatus.SelectedItem.Value, "%") & "' "

        If Not srchMedicalCode.Text = "" Then
            SearchStr = SearchStr & " AND M.MedicalCode like '" & srchMedicalCode.Text & "%'"
        End If
        If Not srchDescription.Text = "" Then
            SearchStr = SearchStr & " AND M.MedicalDesc like '" & srchDescription.Text & "%'"
        End If
        If Not srchUpdBy.Text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & srchUpdBy.Text & "%'"
        End If

        sortItem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objPR.mtdGetMasterList(strOppCd_GET, strParam, objPR.EnumPayrollMasterType.Medical, objDataSet)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_MEDICAL_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_MedicalList.aspx")
        End Try
        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("MedicalCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("MedicalCode"))
                objDataSet.Tables(0).Rows(intCnt).Item("MedicalDesc") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("MedicalDesc"))
                objDataSet.Tables(0).Rows(intCnt).Item("MedicalType") = IIf(Trim(objDataSet.Tables(0).Rows(intCnt).Item("MedicalType")) = "1", objPR.mtdGetMedicalType(objPR.EnumMedicalType.Medicine),objPR.mtdGetMedicalType(objPR.EnumMedicalType.CheckUp))
                objDataSet.Tables(0).Rows(intCnt).Item("Status") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Status"))
                objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If
        Return objDataSet
    End Function

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



    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(Sender As Object, e As DataGridCommandEventArgs)
        Dim strOpCd As String = "PR_CLSSETUP_MEDICAL_STATUS_UPD"
        Dim strParam As String = ""
        Dim CPCell As TableCell = e.Item.Cells(0)
        Dim strSelectedMedicalCode As String 
        Dim intErrNo As Integer

        strSelectedMedicalCode = CPCell.Text
        strParam = strSelectedMedicalCode & "|" & objPR.EnumMedicalStatus.Deleted
        Try
            intErrNo = objPR.mtdUpdMedical(strOpCd, _
                                         strCompany, _
                                         strLocation, _
                                         strUserId, _
                                         strParam)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_MEDICAL_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/PR_Setup_MedicalList.aspx")
        End Try
      
        EventData.EditItemIndex = -1
        BindGrid() 

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_Setup_MedicalDet.aspx")
    End Sub

End Class
