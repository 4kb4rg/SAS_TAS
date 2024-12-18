 
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class PU_trx_PO_MTR : Inherits Page

    Protected WithEvents dgPRListing As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchPRTypeList As DropDownList
    Protected WithEvents srchPRLevelList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchPOID As TextBox

    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents srchStatusLnList As DropDownList
    Protected WithEvents srchApprovedBy As DropDownList
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents lblSearch As Label

    Protected WithEvents srchItem As TextBox
    Protected WithEvents srchReqSatus As DropDownList

    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents ddlDelLocation As DropDownList
    Protected WithEvents ddlPOUser As DropDownList

    Protected WithEvents srchSupplier As TextBox

    Protected objIN As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objPU As New agri.PU.clsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strOppCd_GET_PRList As String = "IN_CLSTRX_PURREQ_LIST_GET"
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer

    Dim objDataSet As New DataSet()
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim intPRCount As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLocLevel As String
    Dim intLevel As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String


    Dim BtnApproved As Button
    Dim BtnCancel As Button
    Dim APPButton As LinkButton
    Dim UpdButton As LinkButton
    Dim CancelButton As LinkButton
    Dim SaveButton As LinkButton

#Region "COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLocLevel = Session("SS_LOCLEVEL")
        intLevel = Session("SS_USRLEVEL")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        'ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = False Then
         '   Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            If SortExpression.Text = "" Then
                SortExpression.Text = "PO.POID"
                sortcol.Text = "ASC"
            End If

            If Not Page.IsPostBack Then
                'If Session("SS_FILTERPERIOD") = "0" Then
                '    lstAccMonth.SelectedValue = strAccMonth
                '    BindAccYear(strAccYear)
                'Else
                '    lstAccMonth.SelectedValue = 0
                '    BindAccYear(strSelAccYear)
                'End If

                lstAccMonth.SelectedValue = strSelAccMonth
                BindAccYear(strAccYear)

                If strLocLevel = "2" Or strLocLevel = "3" Then
                    BindLocation("")
                Else
                    BindLocation(strLocation)
                End If

                BindPOUser()
                BindPOLnStatus()
                BindGrid()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPRListing.CurrentPageIndex = 0
        dgPRListing.EditItemIndex = -1
        BindSearch()
        'CheckStatus()
        'BindPageList()
        lblSearch.Text = "CARI"
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex
        If lblSearch.Text = "" Then
            BindGrid()
        Else
            BindSearch()
        End If
    End Sub

#End Region

#Region "PROCEDURE"

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        lstAccYear.DataSource = objAccYearDs.Tables(0)
        lstAccYear.DataValueField = "AccYear"
        lstAccYear.DataTextField = "UserName"
        lstAccYear.DataBind()
        lstAccYear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Sub dgPRListing_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

    Private Sub dgPRListing_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPRListing.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.ColumnSpan = 11
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Purchase Order"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Goods Receive"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Dispatch Advise"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Stock Receive (Perwakilan)"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Stock Transfer"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "Stock Receive"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgPRListing.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgPRListing_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPRListing.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            'e.Item.Cells(0).Visible = False
        End If

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.All), objIN.EnumPurReqStatus.All))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active), objIN.EnumPurReqStatus.Active))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Cancelled), objIN.EnumPurReqStatus.Cancelled))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed), objIN.EnumPurReqStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Deleted), objIN.EnumPurReqStatus.Deleted))
        srchStatusList.Items.Add(New ListItem("Closed", objIN.EnumPurReqStatus.Fulfilled))

        If intLevel = 0 Then
            srchStatusList.SelectedIndex = 1
        Else
            srchStatusList.SelectedIndex = 6
        End If

        srchStatusLnList.Items.Add(New ListItem("All", "1','2','3','4"))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Active), objIN.EnumPurReqLnStatus.Active))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel), objIN.EnumPurReqLnStatus.Cancel))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Approved), objIN.EnumPurReqLnStatus.Approved))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Hold), objIN.EnumPurReqLnStatus.Hold))
        srchStatusLnList.SelectedIndex = 0

    End Sub

    Sub BindPOLnStatus()
        srchStatusLnList.Items.Clear()
        srchStatusLnList.Items.Add(New ListItem(objPU.mtdGetPOLnStatus(objPU.EnumPOLnStatus.All), objPU.EnumPOLnStatus.All))
        srchStatusLnList.Items.Add(New ListItem(objPU.mtdGetPOLnStatus(objPU.EnumPOLnStatus.Active), objPU.EnumPOLnStatus.Active))
        srchStatusLnList.Items.Add(New ListItem(objPU.mtdGetPOLnStatus(objPU.EnumPOLnStatus.Edited), objPU.EnumPOLnStatus.Edited))
        srchStatusLnList.Items.Add(New ListItem(objPU.mtdGetPOLnStatus(objPU.EnumPOLnStatus.Cancelled), objPU.EnumPOLnStatus.Cancelled))
        srchStatusLnList.Items.Add(New ListItem(objPU.mtdGetPOLnStatus(objPU.EnumPOLnStatus.DeliveryComplete), objPU.EnumPOLnStatus.DeliveryComplete))
    End Sub

    Sub BindLocation(ByVal pv_strLocCode As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strPRRefLocCode = IIf(pv_strLocCode = "", "", pv_strLocCode)
        strParam = strPRRefLocCode & "|" & objAdminLoc.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            With objLocDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = Trim(strPRRefLocCode) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "-All-"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocation.DataSource = objLocDs.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex

        ddlDelLocation.DataSource = objLocDs.Tables(0)
        ddlDelLocation.DataValueField = "LocCode"
        ddlDelLocation.DataTextField = "Description"
        ddlDelLocation.DataBind()
        ddlDelLocation.SelectedIndex = intSelectedIndex
    End Sub

    Sub BindPOUser()
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim objToUser As New Object
        Dim objToLocation As New Object
        Dim dr As DataRow
        Dim intSelectedIndex As Integer
        Dim introw As Integer = 0

        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_USER_GET"

        'For intCnt = 0 To dgPRListing.Items.Count - 1
        strParamName = "SEARCHSTR"
        strParamValue = "Where sloc.LocLevel='3' And sloc.LocType='4' and sh.UsrLevel<=1 "

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToUser)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        For introw = 0 To objToUser.Tables(0).Rows.Count - 1
            objToUser.Tables(0).Rows(introw).Item("UserID") = Trim(objToUser.Tables(0).Rows(introw).Item("UserID"))
            objToUser.Tables(0).Rows(introw).Item("UserName") = Trim(objToUser.Tables(0).Rows(introw).Item("UserName")) & "(" & Trim(objToUser.Tables(0).Rows(introw).Item("description")) & ")"
            'If objToLocation.Tables(0).Rows(introw).Item("LocCode") = pv_LocCode Then
            '    intSelectedIndex = introw + 1
            'End If
        Next introw

        dr = objToUser.Tables(0).NewRow()
        dr("UserID") = ""
        dr("UserName") = "Select User Name"
        objToUser.Tables(0).Rows.InsertAt(dr, 0)

        ddlPOUser.DataSource = objToUser.Tables(0)
        ddlPOUser.DataValueField = "UserID"
        ddlPOUser.DataTextField = "UserName"
        ddlPOUser.DataBind()
        ddlPOUser.SelectedIndex = intSelectedIndex - 1
        'Next
    End Sub


    Sub BindGrid()
        Dim dsData As DataSet

        dsData = LoadData()
        dgPRListing.DataSource = dsData
        dgPRListing.DataBind()
        PageConTrol()
    End Sub

    Sub BindSearch()
        Dim dsData As DataSet

        dsData = LoadSrch()
        dgPRListing.DataSource = dsData
        dgPRListing.DataBind()
        PageConTrol()
    End Sub

    Protected Function LoadData() As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "PU_CLSTRX_PO_MONITOR"

        Dim strSearchLocLevel As String = ""
        Dim strLastAccMonth As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
        Dim StatusLn As String = ""
        Dim SearchStr As String

        SearchStr = " "
        strAccYear = lstAccYear.SelectedItem.Value.Trim()
        strAccMonth = Month(Now())
        strLastAccMonth = Val(strAccMonth) - 1

        SearchStr = SearchStr & " AND PO.AccYear = '" & strAccYear & "' "

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        SearchStr = SearchStr & " AND PO.AccMonth IN ('" & strAccMonth & "') "

        If Not SearchStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If
        End If

        strParamName = "LOCCODE|SEARCHSTR"
        strParamValue = strLocation & "|" & SearchStr

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        intPRCount = objItemDs.Tables(0).Rows.Count

        Return objItemDs
    End Function

    Protected Function LoadSrch() As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "PU_CLSTRX_PO_MONITOR"

        Dim strSearchLocLevel As String = ""
        Dim strLastAccMonth As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
        Dim intCnt As Integer = 0

        'strAccYear = Year(Now())
        strAccYear = lstAccYear.SelectedItem.Value.Trim()
        strAccMonth = Month(Now())
        strLastAccMonth = Val(strAccMonth) - 1

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        Dim SearchStr As String

        SearchStr = " "
        strAccYear = lstAccYear.SelectedItem.Value.Trim()
        strAccMonth = Month(Now())
        strLastAccMonth = Val(strAccMonth) - 1

        SearchStr = SearchStr & " AND PO.AccYear = '" & strAccYear & "' "

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        SearchStr = SearchStr & " AND PO.AccMonth IN ('" & strAccMonth & "') "

        Select Case srchReqSatus.SelectedItem.Value
            Case 0
                SearchStr = SearchStr & ""
            Case 1
                SearchStr = SearchStr & " AND COALESCE(POLN.QtyOrder,0)-COALESCE(GR.GRQTY,0)-COALESCE(GRN.GRNQTY,0) > 0 "
            Case 2
                SearchStr = SearchStr & " AND COALESCE(POLN.QtyOrder,0)-COALESCE(GR.GRQTY,0)-COALESCE(GRN.GRNQTY,0) = 0 "
        End Select

        If srchPOID.Text <> "" Then
            SearchStr = SearchStr & " AND PO.POID LIKE '%" & Trim(srchPOID.Text) & "%' "
        End If

        If srchItem.Text <> "" Then
            SearchStr = SearchStr & " AND (ITM.ItemCode LIKE '%" & Trim(srchItem.Text) & "%' or ITM.Description LIKE '%" & Trim(srchItem.Text) & "%')"
        End If

        If srchSupplier.Text <> "" Then
            SearchStr = SearchStr & " AND (SUP.SupplierCode LIKE '%" & Trim(srchSupplier.Text) & "%' or SUP.Name LIKE '%" & Trim(srchSupplier.Text) & "%')"
        End If

        Select Case srchStatusLnList.SelectedItem.Value
            Case 0
                SearchStr = SearchStr & ""
            Case Else
                SearchStr = SearchStr & " AND POLN.Status = '" & Trim(srchStatusLnList.SelectedItem.Value) & "' "
        End Select

        If ddlPOUser.SelectedItem.Value <> "" Then
            SearchStr = SearchStr & " AND PO.UserPO = '" & Trim(ddlPOUser.SelectedItem.Value) & "' "
        End If

        If ddlLocation.SelectedItem.Value <> "" Then
            SearchStr = SearchStr & " AND POLN.PRRefLocCode = '" & Trim(ddlLocation.SelectedItem.Value) & "' "
        End If

        If ddlDelLocation.SelectedItem.Value <> "" Then
            SearchStr = SearchStr & " AND PO.LocPenyerahan = '" & Trim(ddlDelLocation.SelectedItem.Value) & "' "
        End If

        If Not SearchStr = "" Then
            If Right(SearchStr, 4) = "AND " Then
                SearchStr = Left(SearchStr, Len(SearchStr) - 4)
            End If
        End If

        strParamName = "LOCCODE|SEARCHSTR"
        strParamValue = strLocation & "|" & SearchStr

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return objItemDs
    End Function

    Sub PageConTrol()
        Dim lbl As Label
        Dim btn As Button

        For intCnt = 0 To dgPRListing.Items.Count - 1
            lbl = dgPRListing.Items.Item(intCnt).FindControl("lblPOLNStatus")
            btn = dgPRListing.Items.Item(intCnt).FindControl("BtnCompleted")
            btn.Attributes("onclick") = "javascript:return ConfirmAction('delivery complete');"

            Select Case Trim(lbl.Text)
                Case 1
                    lbl = dgPRListing.Items.Item(intCnt).FindControl("lblQtyOutstanding")
                    If CDbl(lbl.Text) = 0 Then
                        CType(dgPRListing.Items(intCnt).FindControl("BtnCompleted"), Button).Enabled = False
                        btn.Text = "fully received"
                    Else
                        CType(dgPRListing.Items(intCnt).FindControl("BtnCompleted"), Button).Enabled = True
                    End If
                Case 2
                    CType(dgPRListing.Items(intCnt).FindControl("BtnCompleted"), Button).Enabled = False
                    btn.Text = "cancelled"
                Case 8
                    CType(dgPRListing.Items(intCnt).FindControl("BtnCompleted"), Button).Enabled = False
                    btn.Text = "completed"
            End Select

            lbl = dgPRListing.Items.Item(intCnt).FindControl("lblGRID")
            If Trim(lbl.Text) = "" Then
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblGRDate")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblGRQty")
                lbl.Visible = False
            End If
            lbl = dgPRListing.Items.Item(intCnt).FindControl("lblDAID")
            If Trim(lbl.Text) = "" Then
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblDADate")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblDAQty")
                lbl.Visible = False
            End If
            lbl = dgPRListing.Items.Item(intCnt).FindControl("lblSRID")
            If Trim(lbl.Text) = "" Then
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblSRDate")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblSRQty")
                lbl.Visible = False
            End If

        Next
    End Sub

    Sub BtnCompleted_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCd As String = "PU_CLSTRX_PO_LINE_UPD"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strPOID As String = ""
        Dim strPOLnID As String = ""
        Dim strItemCode As String = ""
        Dim indDate As String = ""
        Dim bUpdate As Boolean = False

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        strPOID = CType(dgItem.Cells(0).FindControl("lblPOID"), Label).Text
        strPOLnID = CType(dgItem.Cells(0).FindControl("lblPOLNID"), Label).Text
        strItemCode = CType(dgItem.Cells(2).FindControl("lblItemCode"), Label).Text
      
        strParamName = "STRUPDATE"
        strParamValue = "SET Status = '" & objPU.EnumPOLnStatus.DeliveryComplete & "' " & _
                        "WHERE POID = '" & Trim(strPOID) & "' AND POLNID = '" & Trim(strPOLnID) & "' AND ITEMCODE = '" & Trim(strItemCode) & "' "

        bUpdate = False
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
            bUpdate = True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If lblSearch.Text = "" Then
            BindGrid()
        Else
            BindSearch()
        End If

        If bUpdate = True Then
            UserMsgBox(Me, "Delivery completed sucsess...!!!")
        End If

    End Sub

#End Region
End Class

