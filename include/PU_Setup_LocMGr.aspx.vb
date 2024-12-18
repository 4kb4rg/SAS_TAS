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


Public Class PU_Setup_LocMGr : Inherits Page

    Protected WithEvents EventData As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents blnUpdate As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblDupMsg As Label
    Protected WithEvents sortcol As Label

    Protected WithEvents lblTitle As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents IsPowerSupply As CheckBox

    Protected objGLSetup As New agri.GL.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim strOppCd_ADD As String = "PU_CLSSETUP_SUPPLIER_LOCMGR_ADD"
    Dim strOppCd_DEL As String = "PU_CLSSETUP_SUPPLIER_LOCMGR_DEL"


    Dim ObjOk As New agri.GL.ClsTrx()
    Dim objDataSet As New Object()
    Dim objLangCapDs As New Object()
    Dim intErrNo As Integer
    Dim strParam As String = ""
    Dim strValidateCode As String
    Dim strValidateDesc As String
    Dim strValidateUrut As String

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intGLAR As Integer
    Dim strVehTypeCodeTag As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Dim nCol_LocCOde As Byte = 0
    Dim nCol_DDLToUser As Byte = 1

#Region "TOOLS & COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
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
            'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumGLAccessRights.GLVehicleType), intGLAR) = False Then
            '    Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "PdoGrpID"
                sortcol.Text = "ASC"
            End If
            If Not Page.IsPostBack Then                
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindToUser(ByVal sender As Object, ByVal e As EventArgs)
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_USER_GET"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim objToUser As New Object
        Dim objToLocation As New Object
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        Dim DDL As DropDownList = CType(sender, DropDownList)
        Dim dgItem As DataGridItem = CType(DDL.NamingContainer, DataGridItem)

        strParamName = "SEARCHSTR"
        strParamValue = "WHERE sh.UsrLevel >=2 And sl.LocCOde='" & CType(dgItem.Cells(nCol_LocCOde).FindControl("ddlToLocCode"), DropDownList).SelectedItem.Value & "'"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToUser)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objToUser.Tables(0).NewRow()
        dr("UserID") = ""
        dr("UserName") = "Select User Name"
        objToUser.Tables(0).Rows.InsertAt(dr, 0)

        CType(dgItem.Cells(nCol_DDLToUser).FindControl("ddlToUser"), DropDownList).DataSource = objToUser.Tables(0)
        CType(dgItem.Cells(nCol_DDLToUser).FindControl("ddlToUser"), DropDownList).DataValueField = "UserID"
        CType(dgItem.Cells(nCol_DDLToUser).FindControl("ddlToUser"), DropDownList).DataTextField = "UserName"
        CType(dgItem.Cells(nCol_DDLToUser).FindControl("ddlToUser"), DropDownList).DataBind()
        CType(dgItem.Cells(nCol_DDLToUser).FindControl("ddlToUser"), DropDownList).SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)

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

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim DDLEditText As DropDownList
        Dim txtEditText As TextBox
        Dim lbUpdbutton As LinkButton

        EventData.EditItemIndex = CInt(e.Item.ItemIndex)


        BindGrid()
        BindToLocation("")
        'BindToUser("")

        If CInt(e.Item.ItemIndex) >= EventData.Items.Count Then
            EventData.EditItemIndex = -1
            Exit Sub
        End If

        txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Status")
        Select Case CInt(txtEditText.Text) = objGLSetup.EnumVehTypeStatus.Active
            Case True

                DDLEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlToLocCode")
                DDLEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlToUser")
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

            Case False
                StatusList.SelectedIndex = 1
                DDLEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("ddlToLocCode")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateDate")
                txtEditText.Enabled = False
                txtEditText = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("UpdateID")
                txtEditText.Enabled = False
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Update")
                lbUpdbutton.Visible = False
                lbUpdbutton = EventData.Items.Item(CInt(e.Item.ItemIndex)).FindControl("Delete")
                lbUpdbutton.Text = "Undelete"

        End Select

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim txtEditText As DropDownList
        Dim strMgrID As String
        Dim StrLocMGr As String
        Dim blnDupKey As Boolean = False

        Dim ParamNama As String
        Dim ParamValue As String

        txtEditText = E.Item.FindControl("ddlToLocCode")
        StrLocMGr = txtEditText.Text
        txtEditText = E.Item.FindControl("ddlToUser")
        strMgrID = txtEditText.Text

        ParamNama = "LOC|UID|CD|UD|UPDATEID|ST"
        ParamValue = StrLocMGr & "|" & _
                     strMgrID & "|" & _
                     Format(Date.Now, "yyyy-MM-dd") & "|" & _
                     Format(Date.Now, "yyyy-MM-dd") & "|" & _
                     strUserId & "|" & _
                     "1"
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOppCd_ADD, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        EventData.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbUpdbutton As LinkButton
        Dim lblEdit As Label
        Dim strLoc As String
        Dim strUser As String
        Dim ParamNama As String
        Dim ParamValue As String

        'lbUpdbutton = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        'lbUpdbutton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"

        lblEdit = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblLocCode")
        strLoc = lblEdit.Text.Trim
        lblEdit = EventData.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblUserID")
        strUser = lblEdit.Text.Trim

        ParamNama = "LOC|UID"
        ParamValue = strLoc & "|" & _
                     strUser
        Try
            intErrNo = ObjOk.mtdInsertDataCommon(strOppCd_DEL, ParamNama, ParamValue)
        Catch ex As Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=ADD_ADDHOURLINE&errmesg=" & lblErrMessage.Text & "&redirect=IN/Trx/IN_Trx_Stocktransfer_List.aspx")
        End Try

        EventData.EditItemIndex = -1
        BindGrid()

    End Sub

    Sub DEDR_Add(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dataSet As DataSet = LoadData()
        Dim newRow As DataRow
        Dim lbUpdbutton As LinkButton
        Dim PageCount As Integer

        Dim strNewIDFormat As String = ""
        Dim StrNoTransF As String = ""

        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objToLocation As New Object
        Dim objToUser As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_TOLOC_SHLOC_GET"
        Dim strOpCd_Usr As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_USER_GET"

        blnUpdate.Text = False

        newRow = dataSet.Tables(0).NewRow()
        newRow.Item("LocCode") = ""
        newRow.Item("UserID") = ""
        newRow.Item("Status") = 1
        newRow.Item("CreateDate") = DateTime.Now()
        newRow.Item("UpdateDate") = DateTime.Now()
        newRow.Item("UpdateID") = ""
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

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = "" & "|Order By LocCode"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocation)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objToLocation.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "Select Location"
        objToLocation.Tables(0).Rows.InsertAt(dr, 0)

        CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToLocCode"), DropDownList).DataSource = objToLocation.Tables(0)
        CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToLocCode"), DropDownList).DataValueField = "LocCode"
        CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToLocCode"), DropDownList).DataTextField = "Description"
        CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToLocCode"), DropDownList).DataBind()
        CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToLocCode"), DropDownList).SelectedIndex = intSelectedIndex - 1

        'strParamName = "SEARCHSTR|SORTEXP"
        'strParamValue = "WHERE sh.UsrLevel >=2 "


        'Try
        '    intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Usr, _
        '                                        strParamName, _
        '                                        strParamValue, _
        '                                        objToUser)

        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        'End Try

        'dr = objToUser.Tables(0).NewRow()
        'dr("UserID") = ""
        'dr("UserName") = "Select User Name"
        'objToUser.Tables(0).Rows.InsertAt(dr, 0)

        'CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToUser"), DropDownList).DataSource = objToUser.Tables(0)
        'CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToUser"), DropDownList).DataValueField = "UserID"
        'CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToUser"), DropDownList).DataTextField = "UserName"
        'CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToUser"), DropDownList).DataBind()
        'CType(EventData.Items(CInt(EventData.EditItemIndex)).FindControl("ddlToUser"), DropDownList).SelectedIndex = intSelectedIndex - 1

    End Sub

#End Region

    Sub BindToLocation(ByVal pv_LocCode As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objToLocation As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_TOLOC_SHLOC_GET"


        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = "" & "|Order By LocCode"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocation)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To EventData.Items.Count - 1

            dr = objToLocation.Tables(0).NewRow()
            dr("LocCode") = ""
            dr("Description") = "Select Location"
            objToLocation.Tables(0).Rows.InsertAt(dr, 0)

            CType(EventData.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataSource = objToLocation.Tables(0)
            CType(EventData.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataValueField = "LocCode"
            CType(EventData.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataTextField = "Description"
            CType(EventData.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataBind()
            CType(EventData.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).SelectedIndex = intSelectedIndex - 1

        Next
    End Sub

#Region "LOCAL PROCEDURE"

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

    Sub onload_GetLangCap()
        GetEntireLangCap()
        lblTitle.Text = "SETUP PURCHASING MANAGER"

        EventData.Columns(0).HeaderText = "Location"
        EventData.Columns(1).HeaderText = "Manager"

        strTitleTag = lblTitle.Text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=GL/Setup/GL_Setup_VehicleType.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer

        For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
                If strLocType = objAdminLoc.EnumLocType.Mill Then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                Else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                End If
                Exit For
            End If
        Next
    End Function

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
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetVehTypeStatus(objGLSetup.EnumVehTypeStatus.Active), objGLSetup.EnumVehTypeStatus.Active))
        StatusList.Items.Add(New ListItem(objGLSetup.mtdGetVehTypeStatus(objGLSetup.EnumVehTypeStatus.Deleted), objGLSetup.EnumVehTypeStatus.Deleted))
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCd_GET As String = "PU_CLSSETUP_SUPPLIER_LOCMGR_GET"
        Dim strParam As String
        Dim SearchStr As String
        Dim objTransDs As New Object()

        strParam = ""
        SearchStr = strLocation

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOppCd_GET, strParam, SearchStr, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objTransDs
    End Function


#End Region


End Class
