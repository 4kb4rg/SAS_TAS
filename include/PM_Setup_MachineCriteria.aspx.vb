Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Information
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Web.Services

Public Class PM_Setup_MachineCriteria : Inherits Page

    Protected WithEvents lblErrMessage As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblList As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents ddlStation As DropDownList
    Protected WithEvents ddlMachine As DropDownList
    Protected WithEvents txtCriteriaField As TextBox
    Protected WithEvents ddlType As DropDownList
    Protected WithEvents ddlUsedFor As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SearchBtn As Button
    Protected WithEvents EventData As DataGrid
    Protected WithEvents btnPrev As ImageButton
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents btnNext As ImageButton
    Protected WithEvents ibNew As ImageButton

    Protected WithEvents ddlEdtStation As DropDownList
    Protected WithEvents ddlEdtMachine As DropDownList
    Protected WithEvents ddlEdtFieldType As DropDownList
    Protected WithEvents ddlEdtUsedFor As DropDownList
    Protected WithEvents StatusList As DropDownList
    
    Protected WithEvents lblEdtStation As Label
    Protected WithEvents lblEdtMachine As Label
    Protected WithEvents txtEdtCriteriaField As TextBox
    Protected WithEvents lblEdFieldType As Label
    Protected WithEvents lblEdtUsedFor As Label
    Protected WithEvents txtUpdateDate As TextBox
    Protected WithEvents txtCreateDate As TextBox
    Protected WithEvents txtEdtStatus As TextBox
    Protected WithEvents txtEdtUserName As TextBox
    Protected WithEvents lblCriteriaID As Label 
    
    Protected WithEvents lblMsg As Label

    
    Protected objPM As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()

    Dim strOppCd_GET As String = "PM_CLSSETUP_MACHINECRITERIA_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_MACHINECRITERIA_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_MACHINECRITERIA_UPD"
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPMAR As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strSearch As String = ""

	Dim objAdminLoc As New agri.Admin.clsLoc()
	Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMachineCriteria), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblMsg.Visible = False
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "CriteriaID"
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
    End Sub

    Sub GetEntireLangCap()
        Dim strOpCode_BussTerm As String = "PWSYSTEM_CLSLANGCAP_BUSSTERM_GET"
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_SETUP_MACHINE_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineMaster.aspx")
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
        StatusList.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaStatus(objPM.EnumMachineCriteriaStatus.Active), objPM.EnumMachineCriteriaStatus.Active))
        StatusList.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaStatus(objPM.EnumMachineCriteriaStatus.Deleted), objPM.EnumMachineCriteriaStatus.Deleted))
    End Sub

    Sub BindStationList(Byval index As Integer, Byval vstrValue As String)
        Dim strOpCd As String =  "PM_CLSSETUP_MACHINECRITERIA_STATION_GET"
        Dim dsList As DataSet
        Dim intCnt As Integer
        
                
        ddlEdtStation = EventData.Items.Item(index).FindControl("ddlEdtStation")

        strSearch = "WHERE Status = '1' AND LocCode = '" &  strLocation & "' AND ProcessCtrl = '1'"
        strParam = strSearch
        Try
            intErrNo = objPM.mtdGetMachineCriteriaStation(strOpCd, strParam, dsList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_MACHINECRITERIA_STATIONLIST&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineCriteria.aspx")
        End Try
        ddlEdtStation.Items.Add(New ListItem("Please Select", ""))
        If dsList.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                ddlEdtStation.Items.Add(New ListItem(dsList.Tables(0).Rows(intCnt).Item("BlkCode"), dsList.Tables(0).Rows(intCnt).Item("BlkCode")))
            Next
        End If
        ddlEdtStation.SelectedValue = vstrValue
    End Sub

    Sub BindMachineList(Byval index As Integer, Byval vstrValue As String, Optional Byval vstrStation As String = "")
        Dim strOpCd As String =  "PM_CLSSETUP_MACHINECRITERIA_MACHINE_GET"
        Dim dsList As DataSet
        Dim intCnt As Integer
                
        ddlEdtMachine = EventData.Items.Item(index).FindControl("ddlEdtMachine")
        ddlEdtMachine.Items.Clear
            
        ddlEdtMachine.Items.Add(New ListItem("Please Select", ""))

        If vstrStation <> "" Then

            strSearch = "WHERE Status = '1' AND LocCode = '" &  strLocation & "' AND ProcessCtrl = '1'"

            If vstrStation <> "" Then
                strSearch = strSearch & " AND BlkCode = '" & vstrStation & "'"
             End If

            strParam = strSearch
            Try
                intErrNo = objPM.mtdGetMachineCriteriaStation(strOpCd, strParam, dsList)
            Catch Exp As System.Exception
                Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_MACHINECRITERIA_MACHINELIST&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineCriteria.aspx")
            End Try
        
            If dsList.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                    ddlEdtMachine.Items.Add(New ListItem(dsList.Tables(0).Rows(intCnt).Item("_Descr"), dsList.Tables(0).Rows(intCnt).Item("SubBlkCode")))
                Next
            End If

            Try
                ddlEdtMachine.SelectedValue = vstrValue
            Catch Exp As System.Exception
                ddlEdtMachine.SelectedValue = ""
            End try

        Else
             ddlEdtMachine.SelectedValue = ""
        End If
    End Sub
    
    Sub BindFieldTypeList(Byval index As Integer, Byval vstrValue As String)
        ddlEdtFieldType = EventData.Items.Item(index).FindControl("ddlEdtFieldType")
        ddlEdtFieldType.Items.Add(New ListItem("Please Select", ""))
        ddlEdtFieldType.Items.Add(New ListItem(objPM.mtdGetFieldType(objPM.EnumFieldType.KeyIn), objPM.EnumFieldType.KeyIn))
        ddlEdtFieldType.Items.Add(New ListItem(objPM.mtdGetFieldType(objPM.EnumFieldType.Header), objPM.EnumFieldType.Header))
        ddlEdtFieldType.SelectedValue = vstrValue
    End Sub
    
    Sub BindUsedForList(Byval index As Integer, Byval vstrValue As String)
        ddlEdtUsedFor = EventData.Items.Item(index).FindControl("ddlEdtUsedFor")
        ddlEdtUsedFor.Items.Add(New ListItem("Please Select", ""))
        ddlEdtUsedFor.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaFor(objPM.EnumMachineCriteriaFor.OilLoss), objPM.EnumMachineCriteriaFor.OilLoss))
        ddlEdtUsedFor.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaFor(objPM.EnumMachineCriteriaFor.KernelLoss), objPM.EnumMachineCriteriaFor.KernelLoss))
        ddlEdtUsedFor.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaFor(objPM.EnumMachineCriteriaFor.WaterQuality), objPM.EnumMachineCriteriaFor.WaterQuality))
        ddlEdtUsedFor.SelectedValue = vstrValue
    End Sub

    Sub BindSearchList()
        Dim strOpCd As String = ""
        Dim dsList As DataSet
        Dim intCnt As Integer

        ddlStatus.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaStatus(objPM.EnumMachineCriteriaStatus.All), objPM.EnumMachineCriteriaStatus.All))
        ddlStatus.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaStatus(objPM.EnumMachineCriteriaStatus.Active), objPM.EnumMachineCriteriaStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaStatus(objPM.EnumMachineCriteriaStatus.Deleted), objPM.EnumMachineCriteriaStatus.Deleted))
        ddlStatus.SelectedIndex = 1
       
        strOpCd = "PM_CLSSETUP_MACHINECRITERIA_STATION_GET"
        strSearch = "WHERE Status = '1' AND LocCode = '" &  strLocation & "' AND ProcessCtrl = '1'"
        strParam = strSearch
        Try
            intErrNo = objPM.mtdGetMachineCriteriaStation(strOpCd, strParam, dsList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_MACHINECRITERIA_STATIONLIST&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineCriteria.aspx")
        End Try
        ddlStation.Items.Add(New ListItem("All", ""))
        If dsList.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                ddlStation.Items.Add(New ListItem(dsList.Tables(0).Rows(intCnt).Item("BlkCode"), dsList.Tables(0).Rows(intCnt).Item("BlkCode")))
            Next
        End If
        ddlStation.SelectedIndex = 0
       
        strOpCd = "PM_CLSSETUP_MACHINECRITERIA_MACHINE_GET"
        strSearch = "WHERE Status = '1' AND LocCode = '" & strLocation & "' AND ProcessCtrl = '1'"
        strParam = strSearch
        Try
            intErrNo = objPM.mtdGetMachineCriteriaStation(strOpCd, strParam, dsList)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_MACHINECRITERIA_MACHINELIST&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineCriteria.aspx")
        End Try
        ddlMachine.Items.Add(New ListItem("All", ""))
        If dsList.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To dsList.Tables(0).Rows.Count - 1
                ddlMachine.Items.Add(New ListItem(dsList.Tables(0).Rows(intCnt).Item("_Descr"), dsList.Tables(0).Rows(intCnt).Item("SubBlkCode")))
            Next
        End If
        ddlMachine.SelectedIndex = 0

        ddlType.Items.Add(New ListItem(objPM.mtdGetFieldType(objPM.EnumFieldType.All), objPM.EnumFieldType.All))
        ddlType.Items.Add(New ListItem(objPM.mtdGetFieldType(objPM.EnumFieldType.KeyIn), objPM.EnumFieldType.KeyIn))
        ddlType.Items.Add(New ListItem(objPM.mtdGetFieldType(objPM.EnumFieldType.Header), objPM.EnumFieldType.Header))
        ddlType.SelectedIndex = 0

        ddlUsedFor.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaFor(objPM.EnumMachineCriteriaFor.All), objPM.EnumMachineCriteriaFor.All))
        ddlUsedFor.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaFor(objPM.EnumMachineCriteriaFor.OilLoss), objPM.EnumMachineCriteriaFor.OilLoss))
        ddlUsedFor.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaFor(objPM.EnumMachineCriteriaFor.KernelLoss), objPM.EnumMachineCriteriaFor.KernelLoss))
        ddlUsedFor.Items.Add(New ListItem(objPM.mtdGetMachineCriteriaFor(objPM.EnumMachineCriteriaFor.WaterQuality), objPM.EnumMachineCriteriaFor.WaterQuality))
        ddlUsedFor.SelectedIndex = 0

    End Sub

    Protected Function LoadData() As DataSet
        Dim strParam As String
        Dim SearchStr As String
        Dim SortItem As String
        Dim intCnt As Integer


        SearchStr = " AND M.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = objPM.EnumMachineCriteriaStatus.All, _
                       ddlStatus.SelectedItem.Value, "%") & "' AND M.LocCode='" & strLocation & "'"

        If Not ddlStation.SelectedItem.Value = "" Then
            SearchStr = SearchStr & " AND M.Station = '" & ddlStation.SelectedItem.Value & "'"
        End If 
        If Not ddlMachine.SelectedItem.Value = "" Then
            SearchStr = SearchStr & " AND M.Machine = '" & ddlMachine.SelectedItem.Value & "'"
        End If 
        If Not Trim(txtCriteriaField.Text) = "" Then
            SearchStr = SearchStr & " AND M.CriteriaField like '" & txtCriteriaField.Text & "%' "
        End If
        If Not ddlType.SelectedItem.Value = "0" Then
            SearchStr = SearchStr & " AND M.FieldType = '" & ddlType.SelectedItem.Value & "'"
        End If 
        If Not ddlUsedFor.SelectedItem.Value = "0" Then
            SearchStr = SearchStr & " AND M.UsedFor = '" & ddlUsedFor.SelectedItem.Value & "'"
        End If
        If Not txtLastUpdate.Text = "" Then
            SearchStr = SearchStr & " AND usr.Username like '" & txtLastUpdate.Text & "%' "
        End If

        SortItem = "ORDER BY " & SortExpression.Text & " " & sortcol.Text
        strParam = SortItem & "|" & SearchStr

        Try
            intErrNo = objPM.mtdGetMachineCriteria(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=GET_MACHINECRITERIA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineCriteria.aspx")
        End Try

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

        EventData.EditItemIndex = -1
        BindGrid()
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            EventData.CurrentPageIndex = lstDropList.SelectedIndex
            EventData.EditItemIndex = -1
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        EventData.CurrentPageIndex = e.NewPageIndex
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        EventData.CurrentPageIndex = lstDropList.SelectedIndex
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim Lbl As Label
        Dim Updbutton As LinkButton
        Dim strStation As String

        blnUpdate.Text = True
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        
        BindStatusList(EventData.EditItemIndex)

        Lbl = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEdtStation")
        BindStationList(EventData.EditItemIndex, Trim(Lbl.Text))
        strStation = Trim(Lbl.Text)
        Lbl = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEdtMachine")
        BindMachineList(EventData.EditItemIndex, Trim(Lbl.Text), strStation)
        Lbl = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEdFieldType")
        BindFieldTypeList(EventData.EditItemIndex, Trim(Lbl.Text))
        Lbl = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblEdtUsedFor")
        BindUsedForList(EventData.EditItemIndex, Trim(Lbl.Text))


        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("txtEdtStatus")
        Select Case CInt(EditText.Text) = objPM.EnumMachineCriteriaStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            Case False
                StatusList.SelectedIndex = 1
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim List As DropDownList
        Dim lbl As Label
        Dim blnDupKey As Boolean = False
        
        Dim strStation As String
        Dim strMachine As String
        Dim strCriteriaField As String
        Dim strFieldType As String
        Dim strUsedFor As String
        Dim strStatus As String
        Dim strCriteriaID As String

         
        lbl = E.Item.FindControl("lblCriteriaID")
        strCriteriaID = lbl.Text  

        list = E.Item.FindControl("ddlEdtStation")
        strStation = list.SelectedItem.Value

        list = E.Item.FindControl("ddlEdtMachine")
        strMachine = list.SelectedItem.Value

        EditText = E.Item.FindControl("txtEdtCriteriaField")
        strCriteriaField = EditText.Text        
    
        list = E.Item.FindControl("ddlEdtFieldType")
        strFieldType = list.SelectedItem.Value
        
        list = E.Item.FindControl("ddlEdtUsedFor")
        strUsedFor = list.SelectedItem.Value        

        list = E.Item.FindControl("StatusList")
        strStatus = list.SelectedItem.Value

       
        strParam = strCriteriaID & "|" & strStation & "|" & _
                    strMachine & "|" & _
                    strCriteriaField & "|" & _
                    strFieldType & "|" & _
                    strUsedFor & "|" & _
                    strStatus 

        Try
            intErrNo = objPM.mtdUpdMachineCriteria(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    blnDupKey, _
                                                    blnUpdate.Text)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_MACHINECRITERIA&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineCriteria.aspx")
        End Try

        If blnDupKey Then
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If

    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd As String = "PM_CLSSETUP_MACHINECRITERIA_STATUS_UPD"
        Dim EditText As TextBox
        Dim lbl As Label
        
        Dim strCriteriaID As String
        Dim strStatus As String

        
        lbl = E.Item.FindControl("lblCriteriaID")
        strCriteriaID = lbl.Text  
      
        EditText = E.Item.FindControl("txtEdtStatus")
        strStatus = IIf(EditText.Text = objPM.EnumMachineCriteriaStatus.Active, _
                        objPM.EnumMachineCriteriaStatus.Deleted, _
                        objPM.EnumMachineCriteriaStatus.Active)    
       
        strParam = strCriteriaID & "|"  & strStatus 

        Try
            intErrNo = objPM.mtdUpdMachineCriteriaStatus(strOpCd, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_MACHINECRITERIA_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_Setup_MachineCriteria.aspx")
        End Try
    
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer

        blnUpdate.Text = False
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("Station") = ""
        newRow.Item("Machine") = ""
        newRow.Item("CriteriaField") = ""
        newRow.Item("FieldType") = ""
        newRow.Item("UsedFor") = ""
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UserName") = ""
        dataSet.Tables(0).Rows.Add(newRow)

        EventData.DataSource = dataSet
        PageCount = objGlobal.mtdGetPageCount(dataSet.Tables(0).Rows.Count, EventData.PageSize)
        If EventData.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                EventData.CurrentPageIndex = 0
            Else
                EventData.CurrentPageIndex = PageCount - 1
            End If
        End If
        EventData.DataBind()
        BindPageList()

        EventData.CurrentPageIndex = EventData.PageCount - 1
        lblTracker.Text = "Page " & (EventData.CurrentPageIndex + 1) & " of " & EventData.PageCount
        lstDropList.SelectedIndex = EventData.CurrentPageIndex
        EventData.DataBind()
        EventData.EditItemIndex = EventData.Items.Count - 1
        EventData.DataBind()

        BindStatusList(EventData.EditItemIndex)
        BindStationList(EventData.EditItemIndex, "")
        BindMachineList(EventData.EditItemIndex, "", "")
        BindFieldTypeList(EventData.EditItemIndex, "")
        BindUsedForList(EventData.EditItemIndex, "")

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

    End Sub

    Sub ddlEdtStation_Select(ByVal Sender As Object, ByVal E As EventArgs)
        Dim List As DropDownList
        List = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("ddlEdtStation")
        BindMachineList(CInt(EventData.EditItemIndex), "", Trim(List.SelectedItem.Value))

    End Sub



End Class
