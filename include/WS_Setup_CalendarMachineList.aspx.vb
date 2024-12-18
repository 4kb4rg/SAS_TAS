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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap



Public Class WS_Setup_CalendarMachineList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchBlockCode As TextBox
    Protected WithEvents srchSubBlockCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblBlkCode As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrLicSize As Label
    Protected WithEvents NewTBBtn As ImageButton 

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objWS As New agri.WS.clsSetup()
    
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intWSAR As Integer

    Dim objWSDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strSubBlockCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim strStartAreaTag As String
    Dim strTotalAreaTag As String
    Dim strBlockTag As String
    Dim strStdPerAreaTag As String
    Dim strHarvestStartDateTag As String
    Dim strBlockCodeTag As String
	
	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String
	
    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intWSAR = Session("SS_WSAR")
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWSAccessRights.WSCalMachine), intWSAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrLicSize.Visible = False

            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "ws.SubBlkCode"
            End If
            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If
        End If
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
        
        dgLine.DataBind()
        BindPageList()
        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgLine.PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objWS.EnumCalendarizedMachineStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objWS.EnumCalendarizedMachineStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next

        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text = "Page " & pageno & " of " & dgLine.PageCount
        
    End Sub 

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblSubBlkCode.text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.text
        lblBlkCode.text = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.text
        NewTBBtn.AlternateText = GetCaption(objLangCap.EnumLangCap.SubBlock)

        dgLine.Columns(0).HeaderText = lblBlkCode.text
        dgLine.Columns(1).HeaderText = lblSubBlkCode.text
        dgLine.Columns(2).HeaderText = "Month"

        strSubBlockCodeTag = lblSubBlkCode.text
        strBlockCodeTag = lblBlkCode.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_subblock.aspx")
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
        Dim strOpCode_GetList As String = "WS_CLSSETUP_CALENDARMACHINE_LIST_GET"    
        Dim strSrchBlockCode As String
        Dim strSrchSubBlockCode As String
        Dim strSrchDesc As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchBlockCode = IIf(srchBlockCode.Text = "", "", " And ws.BlkCode = '" & srchBlockCode.Text & "' ") 
        strSrchSubBlockCode = IIf(srchSubBlockCode.Text = "", "", " And ws.SubBlkCode = '" & srchSubBlockCode.Text & "' ") 
        strSrchStatus = IIf(srchStatus.SelectedItem.Value = "", "", IIF(srchStatus.SelectedItem.Value = "0", " And ws.Status in ('" & objWS.EnumCalendarizedMachineStatus.Active & "','" & objWS.EnumCalendarizedMachineStatus.Deleted & "') ", " And ws.Status = '" & srchStatus.SelectedItem.Value & "' "))
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", " And ws.UpdateID = '" & txtLastUpdate.Text & "' ") 
        
        strSearch = strSrchBlockCode & strSrchSubBlockCode & strSrchStatus & strSrchLastUpdate
        strParam = " And ws.LocCode = '" & strLocation & "' And ws.AccYear = '" & strAccYear & "' " & _
                   strSearch & "|" & _
                   " Order By " & SortExpression.Text
        Try
            intErrNo = objWS.mtdGetCalendarizedMachine(strOpCode_GetList, strParam, objWSDs)
        
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_CALENDARMACHINE_LIST_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objWSDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objWSDs.Tables(0).Rows.Count - 1
                objWSDs.Tables(0).Rows(intCnt).Item("BlkCode") = Trim(objWSDs.Tables(0).Rows(intCnt).Item("BlkCode"))
                objWSDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objWSDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
                objWSDs.Tables(0).Rows(intCnt).Item("SelMonth") = Trim(objWSDs.Tables(0).Rows(intCnt).Item("SelMonth"))
                objWSDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objWSDs.Tables(0).Rows(intCnt).Item("Status"))
                objWSDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objWSDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objWSDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objWSDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objWSDs
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
        SortCol.Text = IIF(SortCol.Text = "asc", "desc", "asc")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub

    Sub DEDR_Delete(Sender As Object, e As DataGridCommandEventArgs)
        Dim objTrxDs As New Dataset()
        Dim strOpCd As String = "WS_CLSSETUP_CALENDARMACHINE_LIST_UPD"
        Dim strOpCode_GetList As String = "WS_CLSSETUP_CALENDARMACHINE_LIST_GET"
        Dim strParam As String = ""
        Dim lblSubBlkCode As Label 
        Dim lblBlockCode As Label
        Dim lblSelMonth As Label
        Dim strSubBlkCode As String
        Dim strBlkCode As String
        Dim strselMonth As String
        Dim intErrNo As Integer
        Dim CPCell As TableCell = e.Item.Cells(0)
            
        
        dgLine.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblSubBlkCode = dgLine.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("lblSubBlkCode")
        strSubBlkCode = lblSubBlkCode.Text
        lblBlockCode = dgLine.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("lblBlkCode")
        strBlkCode = lblBlockCode.Text
        lblSelMonth = dgLine.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("lblSelMonth")
        strselMonth = lblSelMonth.Text
        
        strParam = strBlkCode & "|" & _
                   strSubBlkCode & "|" & _
                   strselMonth & "|" & _
                   "|||||" & _
                   objWS.EnumCalendarizedMachineStatus.Deleted
       
        Try
            intErrNo = objWS.mtdUpdCalendarizedMachine(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strAccYear, _
                                           strUserId, _
                                           strParam, _
                                           True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WS_CLSSETUP_CALENDARMACHINE_LIST_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/IN_Trx_ItemToMachine_list.aspx")
        End Try
      


        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewTBBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("WS_CalendarMachineDet.aspx")
    End Sub


End Class
