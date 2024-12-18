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

Public Class IN_ItemToMachineList : Inherits Page

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
    Protected WithEvents lblCode As Label
    Protected WithEvents lblErrLicSize As Label
    Protected WithEvents NewTBBtn As ImageButton 

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objINTrx As New agri.IN.clsTrx()
    
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intINAR As Integer

    Dim objINDs As New Object()
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
        intINAR = Session("SS_INAR")
	    
        strLocType = Session("SS_LOCTYPE")
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INItemToMachine), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrLicSize.Visible = False

            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "ITM.SubBlkCode"
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
                Case objINTrx.EnumItemToMachineStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objINTrx.EnumItemToMachineStatus.Deleted
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
        lblDescription.text = GetCaption(objLangCap.EnumLangCap.SubBlockDesc)
        NewTBBtn.AlternateText = GetCaption(objLangCap.EnumLangCap.SubBlock)

        dgLine.Columns(0).HeaderText = lblSubBlkCode.text
        dgLine.Columns(1).HeaderText = lblDescription.text

        strSubBlockCodeTag = lblSubBlkCode.text
        strDescTag = lblDescription.text
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
        Dim strOpCd_GET As String = "IN_CLSTRX_ITEMTOMACHINE_LIST_GET"    
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
            intErrNo = objINTrx.mtdGetSubBlock(strOpCd_GET, strLocation, strParam, objINDs, False)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEMTOMACHINE_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objINDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objINDs.Tables(0).Rows.Count - 1
                objINDs.Tables(0).Rows(intCnt).Item("SubBlkCode") = Trim(objINDs.Tables(0).Rows(intCnt).Item("SubBlkCode"))
                objINDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objINDs.Tables(0).Rows(intCnt).Item("Description"))
                objINDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objINDs.Tables(0).Rows(intCnt).Item("Status"))
                objINDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objINDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objINDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objINDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objINDs
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
        Dim strOpCd As String = "IN_CLSTRX_ITEMTOMACHINE_STATUS_UPD"
        Dim strOpCode_GetLine As String = "IN_CLSTRX_ITEMTOMACHINE_GET"
        Dim strParam As String = ""
        Dim lblSubBlkCode As Label 
        Dim lblBlockCode As Label
        Dim strSubBlkCode As String
        Dim strBlkCode As String
        Dim intErrNo As Integer
        Dim CPCell As TableCell = e.Item.Cells(0)
            
        
        dgLine.EditItemIndex = Convert.ToInt32(E.Item.ItemIndex)
        lblSubBlkCode = dgLine.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("lblSubBlkCode")
        strSubBlkCode = lblSubBlkCode.Text
        lblBlockCode = dgLine.Items.Item(Convert.ToInt32(E.Item.ItemIndex)).FindControl("BlkCode")
        strBlkCode = lblBlockCode.Text
        
        strParam = strBlkCode & "|" & strSubBlkCode & "|" & objINTrx.EnumItemToMachineStatus.Deleted
       
        Try
            intErrNo = objINTrx.mtdUpdItemToMachineTrx(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strParam, _
                                           True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINE_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/IN_Trx_ItemToMachine_list.aspx")
        End Try
      
        strParam = strSubBlkCode
        Try
            intErrNo = objINTrx.mtdGetItemToMachineTrx(strOpCode_GetLine, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            objTrxDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_ITEMTOMACHINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("IN_Trx_ItemToMachine_details.aspx")
    End Sub


End Class
