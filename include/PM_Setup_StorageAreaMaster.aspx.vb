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

Public Class PM_Setup_StorageAreaMaster : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label
    
    Protected WithEvents srchStorageAreaCode As TextBox
    Protected WithEvents srchStorageType As TextBox
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList

    Protected objPM As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim objDataSet As New DataSet()
    Dim strStorageAreaCode As String
    Dim strDescription As String
    Dim TitleTag As String
    Dim StorageAreaCodeTag As String
    Dim DescriptionTag As String
    
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMStorageAreaMaster), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            TitleTag = "STORAGE AREA MASTER LIST"
            StorageAreaCodeTag = "Storage Area Code"
            DescriptionTag = "Storage Type"
            If SortExpression.Text = "" Then
                SortExpression.Text = "StorageAreaCode"
            End If

            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objPM.mtdGetStorageAreaStatus(objPM.EnumStorageAreaStatus.All), objPM.EnumstorageAreaStatus.All))
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetStorageAreaStatus(objPM.EnumStorageAreaStatus.Active), objPM.EnumstorageAreaStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPM.mtdGetStorageAreaStatus(objPM.EnumStorageAreaStatus.Deleted), objPM.EnumstorageAreaStatus.Deleted))
        srchStatusList.SelectedIndex = 1
    End Sub

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
        
        dsData = LoadData
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
        lblTracker.Text="Page " & pageno & " of " & EventData.PageCount
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

    Protected Function LoadData() As DataSet
        Dim strOppCd_GET As String = "PM_CLSSETUP_STORAGEAREA_GET"
        Dim SearchStr As String
        Dim strParam As String
        Dim sortitem as string
        Dim intErrNo As Integer
        Dim intCnt As Integer
        
        SearchStr =  " AND SA.Status like '" & IIF(Not srchStatusList.selectedItem.Value = objPM.EnumStorageAreaStatus.All, _
                       srchStatusList.selectedItem.Value, "%" ) & "' AND SA.LocCode='" & strLocation & "'"
        
        SearchStr =  SearchStr & " AND SA.LocCode = '" & strLocation & "' "

        If NOT srchStorageAreaCode.text = "" Then
            SearchStr =  SearchStr & " AND SA.StorageAreaCode like '" & srchStorageAreaCode.text & "%' "
        End If
        
        If NOT srchStorageType.text = "" Then
            SearchStr =  SearchStr & " AND ST.Description like '" & srchStorageType.text & "%' "
        End If

        If NOT srchUpdateBy.text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & srchUpdateBy.text & "%' "
        End If

        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text 
        strParam =  sortitem & "|" & SearchStr

        Try
            intErrNo = objPM.mtdGetStorageArea(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_STORAGEAREALIST&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_StorageAreaMaster.aspx")
        End Try

        For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
            objDataSet.Tables(0).Rows(intCnt).Item("StorageAreaCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("StorageAreaCode"))
            objDataSet.Tables(0).Rows(intCnt).Item("Description") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Description"))
            objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objDataSet.Tables(0).Rows(intCnt).Item("Status") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Status"))
            objDataSet.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UserName"))
        Next

        Return objDataSet
    End Function
    
Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
    Dim strStatus As String
    Dim strStorageAreaCode As String
    Dim strDescription As String
    Dim strUpdateBy As String
    Dim strSortExp As String
    Dim strSortCol As String

    strStatus = IIF(Not srchStatusList.selectedItem.Value = objPM.EnumStorageAreaStatus.All, srchStatusList.selectedItem.Value, "")
    strStorageAreaCode = srchStorageAreaCode.text
    strDescription = srchStorageType.text
    strUpdateBy =  srchUpdateBy.text
    strSortExp = sortexpression.text
    strSortCol = sortcol.text

    Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PM_Rpt_StorageAreaMasterDetail.aspx?strStatus=" & strStatus & _
                   "&strStorageAreaCode=" & strStorageAreaCode & _
                   "&strDescription=" & strDescription & _
                   "&strUpdateBy=" & strUpdateBy & _
                   "&strSortExp=" & strSortExp & _
                   "&strSortCol=" & strSortCol & _
                   "&TitleTag=" & TitleTag & _
                   "&StorageAreaCodeTag=" & StorageAreaCodeTag & _
                   "&DescriptionTag=" & DescriptionTag & _
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
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)



        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PM_Setup_StorageAreaMaster_Det.aspx")
    End Sub


End Class
