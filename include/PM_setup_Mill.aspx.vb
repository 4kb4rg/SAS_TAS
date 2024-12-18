Imports System
Imports System.Data
Imports System.Text
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


Public Class PM_setup_Mill : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents curStatus As Label
    Protected WithEvents lblOper As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents srchMillCode As TextBox
    Protected WithEvents srchMillDesc As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents lblTitle As Label
    Protected WithEvents lblMillCode As Label
    Protected WithEvents lblMillDesc As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblPleaseEnter As Label
    Protected WithEvents lblList As Label
    Protected WithEvents MillType As DropDownList

    Protected objPMSetup As New agri.PM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strOppCd_GET As String = "PM_CLSSETUP_MILL_LIST_GET"
    Dim strOppCd_ADD As String = "PM_CLSSETUP_MILL_LIST_ADD"
    Dim strOppCd_UPD As String = "PM_CLSSETUP_MILL_LIST_UPD"
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
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim DocTitleTag As String
    Dim MillCodeTag As String
    Dim MillDescTag As String
    Dim MillTypeTag As String

    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim intConfigSetting As Integer
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPMAR = Session("SS_PMAR")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        intConfigSetting = Session("SS_CONFIGSETTING")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPMAccessRights.PMMill), intPMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "MillCode"
                SortCol.Text = "ASC"
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
        lblTitle.Text = "MILL"
        lblMillCode.Text = "Mill" & lblCode.Text
        lblMillDesc.Text = "Description"

        DocTitleTag = lblTitle.Text & lblList.Text
        MillCodeTag = "Mill Code"
        MillDescTag = "Description"
        MillTypeTag = "Type"

        strValidateCode = lblPleaseEnter.Text & lblMillCode.Text & "."
        strValidateDesc = lblPleaseEnter.Text & lblMillDesc.Text & "."

        EventData.Columns(0).HeaderText = lblMillCode.Text
        EventData.Columns(1).HeaderText = lblMillDesc.Text
        EventData.Columns(2).HeaderText = MillTypeTag
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PM_SETUP_MILL_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_setup_Mill.aspx")
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
        StatusList.Items.Add(New ListItem(objPMSetup.mtdGetMillStatus(objPMSetup.EnumMillStatus.All), objPMSetup.EnumMillStatus.All))
        StatusList.Items.Add(New ListItem(objPMSetup.mtdGetMillStatus(objPMSetup.EnumMillStatus.Active), objPMSetup.EnumMillStatus.Active))
        StatusList.Items.Add(New ListItem(objPMSetup.mtdGetMillStatus(objPMSetup.EnumMillStatus.Delete), objPMSetup.EnumMillStatus.Delete))
    End Sub

    Sub BindMillType(ByVal index As Integer)
        MillType = EventData.Items.Item(index).FindControl("MillType")
        MillType.Items.Add(New ListItem(objPMSetup.mtdGetMillType(objPMSetup.EnumMillType.Internal), objPMSetup.EnumMillType.Internal))
        MillType.Items.Add(New ListItem(objPMSetup.mtdGetMillType(objPMSetup.EnumMillType.External), objPMSetup.EnumMillType.External))
        MillType.Items.Add(New ListItem(objPMSetup.mtdGetMillType(objPMSetup.EnumMillType.Affiliate), objPMSetup.EnumMillType.Affiliate))
    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objPMSetup.mtdGetMillStatus(objPMSetup.EnumMillStatus.All), objPMSetup.EnumMillStatus.All))
        srchStatusList.Items.Add(New ListItem(objPMSetup.mtdGetMillStatus(objPMSetup.EnumMillStatus.Active), objPMSetup.EnumMillStatus.Active))
        srchStatusList.Items.Add(New ListItem(objPMSetup.mtdGetMillStatus(objPMSetup.EnumMillStatus.Delete), objPMSetup.EnumMillStatus.Delete))

        srchStatusList.SelectedIndex = 1
    End Sub

    Protected Function LoadData() As DataSet
        Dim Period As String

        strParam =  srchMillCode.Text & "|" & _
                    srchMillDesc.Text & "|" & _
                    srchStatusList.SelectedItem.Value & "|" & _
                    srchUpdateBy.Text & "|" & _
                    SortExpression.Text & " " & SortCol.Text

        Try
            intErrNo = objPMSetup.mtdGetMillCode(strOppCd_GET, strParam, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("../include/mesg/ErrorMessage.aspx?errcode=PM_SETUP_MILL_GET&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_setup_Mill.aspx")
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


    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim LabelText As Label
        Dim EditList As DropDownList
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim intSelectedAcc As Integer

        lblOper.Text = objPMSetup.EnumOperation.Update
        EventData.EditItemIndex = CInt(E.Item.ItemIndex)

        BindGrid()
        If CInt(E.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If
        BindStatusList(EventData.EditItemIndex)
        BindMillType(EventData.EditItemIndex)

        EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(EditText.Text) = objPMSetup.EnumMillStatus.Active
            Case True
                StatusList.SelectedIndex = 0
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("MillCode")
                EditText.ReadOnly = True
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("MillType")
                EditList.SelectedIndex = CInt(Trim(objDataSet.Tables(0).Rows(EventData.EditItemIndex).Item("MillType"))) - 1
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Delete"
                Updbutton.Attributes("onclick") = "javascript:return ConfirmAction('Delete');"
            Case False
                StatusList.SelectedIndex = 1
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("MillCode")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("MillDesc")
                EditText.Enabled = False
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("MillType")
                EditList.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UpdateDate")
                EditText.Enabled = False
                EditText = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("UserName")
                EditText.Enabled = False
                EditList = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("StatusList")
                EditList.Enabled = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
                Updbutton.Visible = False
                Updbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
                Updbutton.Text = "Undelete"
        End Select
        validateCode = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateCode")
        validateDesc = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ValidateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim EditText As TextBox
        Dim EditList As DropDownList
        Dim strMillCode As String
        Dim strMillDesc As String
        Dim strMillType As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim intError As Integer

        EditText = E.Item.FindControl("MillCode")
        strMillCode = EditText.Text
        EditText = E.Item.FindControl("MillDesc")
        strMillDesc = EditText.Text
        EditList = E.Item.FindControl("MillType")
        strMillType = EditList.SelectedItem.Value
        EditText = E.Item.FindControl("Status")
        strStatus = EditText.Text
        EditText = E.Item.FindControl("CreateDate")
        strCreateDate = EditText.Text

        strParam = strMillCode & "|" & _
                    strMillDesc & "|" & _
                    strMillType & "|" & _
                    strStatus

        Try
            intErrNo = objPMSetup.mtdUpdMill(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    lblOper.Text, _
                                                    intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=UPDATE_MILL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_setup_Mill.aspx")
        End Try

        If intError = objPMSetup.EnumErrorType.DuplicateKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
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
        Dim EditText As TextBox
        Dim EditList As DropDownList
        Dim strMillCode As String
        Dim strMillDesc As String
        Dim strMillType As String
        Dim strStatus As String
        Dim strCreateDate As String
        Dim blnDupKey As Boolean = False
        Dim lblMsg As Label
        Dim intError As Integer

        EditText = E.Item.FindControl("MillCode")
        strMillCode = EditText.Text
        EditText = E.Item.FindControl("MillDesc")
        strMillDesc = EditText.Text
        EditList = E.Item.FindControl("MillType")
        strMillType = EditList.SelectedItem.Value
        EditText = E.Item.FindControl("Status")
        strStatus = IIf(EditText.Text = objPMSetup.EnumMillStatus.Active, _
                        objPMSetup.EnumMillStatus.Delete, _
                        objPMSetup.EnumMillStatus.Active)
        EditText = E.Item.FindControl("CreateDate")
        strCreateDate = EditText.Text

        strParam = strMillCode & "|" & _
                    strMillDesc & "|" & _
                    strMillType & "|" & _
                    strStatus

        Try
            intErrNo = objPMSetup.mtdUpdMill(strOppCd_ADD, _
                                                    strOppCd_UPD, _
                                                    strOppCd_GET, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strParam, _
                                                    lblOper.Text, _
                                                    intError)

        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=DELETE_MILL&errmesg=" & lblErrMessage.Text & "&redirect=PM/Setup/PM_setup_Mill.aspx")
        End Try

        If intError = objPMSetup.EnumErrorType.DuplicateKey Then
            lblMsg = E.Item.FindControl("lblDupMsg")
            lblMsg.Visible = True
        Else
            EventData.EditItemIndex = -1
            BindGrid()
        End If
    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As dataSet = LoadData()
        Dim newRow As DataRow
        Dim Updbutton As LinkButton
        Dim validateCode As RequiredFieldValidator
        Dim validateDesc As RequiredFieldValidator
        Dim PageCount As Integer

        lblOper.Text = objPMSetup.EnumOperation.Add
        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("MillCode") = ""
        newRow.Item("MillDesc") = ""
        newRow.Item("MillType") = "0"
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
        BindMillType(EventData.EditItemIndex)

        Updbutton = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("Delete")
        Updbutton.Visible = False

        validateCode = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateCode")
        validateDesc = EventData.Items.Item(CInt(EventData.EditItemIndex)).FindControl("validateDesc")

        validateCode.ErrorMessage = strValidateCode
        validateDesc.ErrorMessage = strValidateDesc

    End Sub


End Class
