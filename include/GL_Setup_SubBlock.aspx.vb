
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

'Imports System.Configuration.ConfigurationSettings
'Imports System.Diagnostics

Public Class GL_Setup_SubBlock : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchSubBlockCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblSubBlkCode As Label
    Protected WithEvents lblDescription As Label
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrLicSize As Label

    Protected WithEvents NewTBBtn As ImageButton 

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLSetup As New agri.GL.clsSetup()
    
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer

    Dim objGLSetupDs As New Object()
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
        intGLAR = Session("SS_GLAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLSubBlk), intGLAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrLicSize.Visible = False

            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "sub.SubBlkCode"
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
                Case objGLSetup.EnumSubBlockStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objGLSetup.EnumSubBlockStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next

        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text = "Page " & pageno & " of " & dgLine.PageCount
        
    End Sub 

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.SubBlock))
        lblSubBlkCode.text = GetCaption(objLangCap.EnumLangCap.SubBlock) & lblCode.text
        lblDescription.text = GetCaption(objLangCap.EnumLangCap.SubBlockDesc)

        NewTBBtn.AlternateText = GetCaption(objLangCap.EnumLangCap.SubBlock)

        dgLine.Columns(0).HeaderText = lblSubBlkCode.text
        dgLine.Columns(1).HeaderText = lblDescription.text

        strSubBlockCodeTag = lblSubBlkCode.text
        strDescTag = lblDescription.text
        strTitleTag = lblTitle.text
        strStartAreaTag = GetCaption(objLangCap.EnumLangCap.StartArea)
        strTotalAreaTag = GetCaption(objLangCap.EnumLangCap.TotalArea)
        strBlockTag = GetCaption(objLangCap.EnumLangCap.Block)
        strStdPerAreaTag = GetCaption(objLangCap.EnumLangCap.StdPerArea)
        strHarvestStartDateTag = GetCaption(objLangCap.EnumLangCap.HarvestStartDate)
        strBlockCodeTag = GetCaption(objLangCap.EnumLangCap.Block) & lblCode.text

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

        Dim strOpCd_GET As String = "GL_CLSSETUP_SUBBLOCK_LIST_GET"    
        Dim strSrchSubBlockCode As String
        Dim strSrchDesc As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchSubBlockCode = IIF(srchSubBlockCode.Text = "", "", srchSubBlockCode.Text)
        strSrchDesc = IIF(srchDescription.Text = "", "", srchDescription.Text)
        strSrchStatus = IIF(srchStatus.SelectedItem.Value = "0", "", srchStatus.SelectedItem.Value)
        strSrchLastUpdate = IIF(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchSubBlockCode & "|" & _
                   strSrchDesc & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"

        Try
            intErrNo = objGLSetup.mtdGetSubBlock(strOpCd_GET, strLocation, strParam, objGLSetupDs, False)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objGLSetupDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objGLSetupDs.Tables(0).Rows.Count - 1
                objGLSetupDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("Description"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("Status"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objGLSetupDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objGLSetupDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objGLSetupDs
    End Function


    Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
        Dim strStatus As String
        Dim strSubBlockCode As String
        Dim strDescription As String
        Dim strUpdateBy As String
        Dim strSortExp As String
        Dim strSortCol As String

        strStatus = IIF(Not srchStatus.selectedItem.Value = "0", srchStatus.selectedItem.Value, "")
        strSubBlockCode = srchSubBlockCode.text
        strDescription = srchDescription.text
        strUpdateBy =  txtLastUpdate.text
        strSortExp = SortExpression.text
        strSortCol = SortCol.text

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/GL_Rpt_SubBlock.aspx?strSubBlockCodeTag=" & strSubBlockCodeTag & _
                    "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & _
                    "&strStartAreaTag=" & strStartAreaTag & "&strTotalAreaTag=" & strTotalAreaTag & _
                    "&strBlockTag=" & strBlockTag & "&strStdPerAreaTag=" & strStdPerAreaTag & _
                    "&strHarvestStartDateTag=" & strHarvestStartDateTag & _
                    "&strBlockCodeTag=" & strBlockCodeTag & "&strStatus=" & strStatus & _
                    "&strSubBlockCode=" & strSubBlockCode & "&strDescription=" & strDescription & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub


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
        Dim lblDelText As Label
        Dim strOpCd_ADD As String = ""
        Dim strOpCd_UPD As String = "GL_CLSSETUP_SUBBLOCK_UPD"
        Dim strParam As String = ""
        Dim strSelBlkCode As String 
        Dim intErrNo As Integer
        Dim blnExceedLicSize As Boolean

        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLnId")
        strSelBlkCode = lblDelText.Text



        strParam = strSelBlkCode & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9) & _
                       objGLSetup.EnumSubBlockStatus.Deleted & Chr(9) & Chr(9) & Chr(9) & Chr(9) & Chr(9)  & _
                       Chr(9) & Chr(9) & Chr(9) & Chr(9)& Chr(9)& Chr(9)& Chr(9)& Chr(9)& Chr(9)& Chr(9)& Chr(9) & _
                       Chr(9) & Chr(9)

        Try
            intErrNo = objGLSetup.mtdUpdSubBlock(strOpCd_UPD, _
                                                 strCompany, _
                                                 strLocation, _
                                                 strUserId, _
                                                 strParam, _
                                                 True, _
                                                 blnExceedLicSize)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_SETUP_SUBBLOCK_LIST_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/gl_setup_subblock.aspx")
        End Try

        If blnExceedLicSize = True Then lblErrLicSize.Visible = True      

        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewTBBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("GL_Setup_SubBlockDet.aspx")
    End Sub

    'Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
    '    Dim VSKey As String 
    '    Debug.WriteLine(MyBase.Session.SessionID)

    '    VSKey = "VIEWSTATE_" & MyBase.Session.SessionID & "_" & Request.RawUrl & "_" & Date.Now.Ticks.ToString

    '    If UCase(AppSettings("ServerSideViewState")) = "TRUE" Then

    '        If UCase(AppSettings("ViewStateStore")) = "CACHE" Then
    '            Cache.Add(VSKey, viewState, Nothing, Date.Now.AddMinutes(Session.Timeout), _
    '             Cache.NoSlidingExpiration, Web.Caching.CacheItemPriority.Default, Nothing)

    '        Else
    '            Dim VsDataTable As DataTable
    '            Dim DbRow As DataRow

    '            If IsNothing(Session("__VSDataTable")) Then
    '                Dim PkColumn(1), DbColumn As DataColumn
    '                VsDataTable = New DataTable("VState") 

    '                DbColumn = New DataColumn("VSKey", GetType(String))
    '                VsDataTable.Columns.Add(DbColumn)
    '                PkColumn(0) = DbColumn
    '                VsDataTable.PrimaryKey = PkColumn

    '                DbColumn = New DataColumn("VSData", GetType(Object))
    '                VsDataTable.Columns.Add(DbColumn)

    '                DbColumn = New DataColumn("DateTime", GetType(Date))
    '                VsDataTable.Columns.Add(DbColumn)
    '            Else
    '                VsDataTable = Session("__VSDataTable")
    '            End If

    '            DbRow = VsDataTable.Rows.Find(VSKey)

    '            If Not IsNothing(DbRow) Then
    '                DbRow("VsData") = viewState
    '            Else
    '                DbRow = VsDataTable.NewRow
    '                DbRow("VSKey") = VSKey
    '                DbRow("VsData") = viewState
    '                DbRow("DateTime") = Date.Now
    '                VsDataTable.Rows.Add(DbRow)
    '            End If

    '            If Convert.ToInt16(AppSettings("ViewStateTableSize")) < VsDataTable.Rows.Count Then
    '                Debug.WriteLine("Deleting ViewState Created On " & DbRow(2) & ",ID " & DbRow(0))
    '                VsDataTable.Rows(0).Delete() 
    '            End If

    '            Session("__VSDataTable") = VsDataTable
    '        End If

    '        RegisterHiddenField("__VIEWSTATE_KEY", VSKey)
    '    Else
    '        MyBase.SavePageStateToPersistenceMedium(viewState)
    '    End If
    'End Sub

    'Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object
    '    If UCase(AppSettings("ServerSideViewState")) = "TRUE" Then

    '        Dim VSKey As String 
    '        VSKey = Request.Form("__VIEWSTATE_KEY") 

    '        If Not VSKey.StartsWith("VIEWSTATE_") Then
    '            Throw New Exception("Invalid VIEWSTATE Key: " & VSKey)
    '        End If

    '        If UCase(AppSettings("ViewStateStore")) = "CACHE" Then
    '            Return Cache(VSKey)
    '        Else
    '            Dim VsDataTable As DataTable
    '            Dim DbRow As DataRow
    '            VsDataTable = Session("__VSDataTable")
    '            If IsNothing(VsDataTable) Then
    '                Response.Redirect("/SessionExpire.aspx")
    '            End If

    '            DbRow = VsDataTable.Rows.Find(VSKey)

    '            If IsNothing(DbRow) Then
    '                Throw New Exception("VIEWStateKey not Found. Consider increasing the ViewStateTableSize parameter on Web.Config file.")
    '            End If

    '            Return DbRow("VsData")
    '        End If
    '    Else
    '        Return MyBase.LoadPageStateFromPersistenceMedium()
    '    End If
    'End Function


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
