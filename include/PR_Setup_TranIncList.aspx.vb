Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic

Imports agri.PR
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PR_Setup_TranIncList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtTranIncCode As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents txtEmpCategory As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblTitle2 As Label

    Protected objPRSetup As New agri.PR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim intPRAR As Long
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objLangCapDs As New Object()
    Dim strLangCode As New Object()
   
    Dim ObjDataSet As New Object()
    Dim strLocType as String


    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")
        strLocType = Session("SS_LOCTYPE")
    
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTransportIncentive), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
           If SortExpression.Text = "" Then
                SortExpression.Text = "TranIncCode"
            End If
            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
            End If
        End If
        onload_GetLangCap()
       
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()     
        lblTitle.Text = Ucase(GetCaption(objLangCap.EnumLangCap.TransportIncentive)) & " "
        lblTitle2.Text = GetCaption(objLangCap.EnumLangCap.TransportIncentive) & " "
    End Sub

        Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = strLangCode
        Try
            intErrNo = objLangCap.mtdGetBussTerm(strOpCode_BussTerm, _
                                                 "", _
                                                 "", _
                                                 strUserId, _
                                                 "", _
                                                 "", _
                                                 objLangCapDs, _
                                                 strParam)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=MENU_PRSETUP_LANGCAP&errmesg=&redirect=")
        End Try
    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function
    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                dgLine.CurrentPageIndex = 0
            Case "prev"
                dgLine.CurrentPageIndex = _
                    Math.Max(0, dgLine.CurrentPageIndex - 1)
            Case "next"
                dgLine.CurrentPageIndex = _
                    Math.Min(dgLine.PageCount - 1, dgLine.CurrentPageIndex + 1)
            Case "last"
                dgLine.CurrentPageIndex = dgLine.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIF(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub

    Sub DEDR_Delete(Sender As Object, e As DataGridCommandEventArgs)
        Dim strOpCd As String = "PR_CLSSETUP_TRANINCENTIVE_STATUS_UPD"
        Dim strParam As String = ""
        Dim CPCell As TableCell = e.Item.Cells(0)
        Dim strSelectedTranIncCode As String 
        Dim intErrNo As Integer


       strSelectedTranIncCode = CPCell.Text       
       strParam =  strSelectedTranIncCode & "|" & objPRSetup.EnumTranIncentiveStatus.Deleted & "||"
     
        Try
            intErrNo = objPRSetup.mtdUpdTranIncentive(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strParam)
                                           
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_TranInc_DEL&errmesg=" & lblErrMessage.Text & "&redirect=hr/setup/PR_Setup_TranIncList.aspx")
        End Try
      
        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewADBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_Setup_TranIncDet.aspx")
    End Sub

    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
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
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgLine.PageSize)
        
        dgLine.DataSource = dsData
        If dgLine.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgLine.CurrentPageIndex = 0
            Else
                dgLine.CurrentPageIndex = PageCount - 1
            End If
        End If

        GetEntireLangCap()  

        Dim col as HyperLinkColumn
        col = dgLine.Columns(1)
        col.HeaderText = GetCaption(objLangCap.EnumLangCap.TransportIncentive) & " Code"

        dgLine.DataBind()
        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgLine.PageCount
        
        
        For intCnt = 0 To dgLine.Items.Count - 1
           
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objPRSetup.EnumTranIncentiveStatus.Active
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPRSetup.EnumTranIncentiveStatus.Deleted
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
            End Select
        Next           

    End Sub 

    Sub BindPageList() 
        Dim count As Integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub 

    Protected Function LoadData() As DataSet
        Dim strOppCd_GET as String = "PR_CLSSETUP_TRANINCENTIVE_GET"
        Dim strParam As String
        Dim SearchStr As String
        Dim sortItem As String
        Dim intCnt As Integer
        Dim intErrNo As Integer


        SearchStr = " AND TI.Status like '" & IIf(ddlStatus.SelectedItem.Value <> objPRSetup.EnumTranIncentiveStatus.All, ddlStatus.SelectedItem.Value, "%") & "' "

        If Not txtTranIncCode.Text = "" Then
            SearchStr = SearchStr & " AND TI.TranIncCode like '" & txtTranIncCode.Text & "%'"
        End If
        If Not txtDescription.Text = "" Then
            SearchStr = SearchStr & " AND TI.Description like '" & txtDescription.Text & "%'"
        End If
        If Not txtLastUpdate.Text = "" Then
            SearchStr = SearchStr & " AND Usr.UserName like '" & txtLastUpdate.Text & "%'"
        End If

        sortItem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = sortItem & "|" & SearchStr

        Try
            intErrNo = objPRSetup.mtdGetMasterList(strOppCd_GET, strParam, objPRSetup.EnumPayrollMasterType.TranIncentive, objDataSet)
        Catch Exp As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=PR_CLSSETUP_TranInc_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=PR/Setup/PR_Setup_TranIncList.aspx")
        End Try
        If objDataSet.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDataSet.Tables(0).Rows.Count - 1
                objDataSet.Tables(0).Rows(intCnt).Item("TranIncCode") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("TranIncCode"))
                objDataSet.Tables(0).Rows(intCnt).Item("Description") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Description"))
                objDataSet.Tables(0).Rows(intCnt).Item("Status") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("Status"))
                objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UpdateDate"))
                objDataSet.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDataSet.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If
        Return objDataSet
    End Function


    
End Class
