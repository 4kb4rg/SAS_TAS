
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


Public Class TX_Setup_TaxObjectRateList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents ddlTaxObjectGrp As DropDownList
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchTaxObjectCode As TextBox
    Protected WithEvents srchDescription As TextBox
    Protected WithEvents srchActCode As TextBox
    Protected WithEvents srchStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents NewBtn As ImageButton
    Protected WithEvents btnSyncData As ImageButton


    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objCTSetup As New agri.CT.clsSetup()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intCTAR As Integer

    Dim ObjTaxDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strTaxObjectCodeTag As String
    Dim strDescTag As String
    Dim strActCodeTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intCTAR = Session("SS_CTAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intCTAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TOB.TaxID"
            End If
            If Not Page.IsPostBack Then
                BindAccCodeDropList("")
                BindGrid()
                BindPageList()
            End If

            'If strCompany <> "KAS" Then
            '    NewBtn.Visible = False
            '    btnSyncData.Visible = False
            'Else
                NewBtn.Visible = True
                btnSyncData.Visible = True
            'End If

        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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

        dsData = LoadData()
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
        lblTracker.Text = "Page " & PageNo & " of " & dgLine.PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objCTSetup.EnumStockItemStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objCTSetup.EnumStockItemStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next


    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "TX_CLSSETUP_TAXOBJECTRATE_LIST_GET"
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        strSearch = IIf(Trim(srchTaxObjectCode.Text) = "", "", "TOBLn.TaxObject Like '%" & Trim(srchTaxObjectCode.Text) & "%' AND ")
        strSearch = strSearch + IIf(ddlTaxObjectGrp.SelectedItem.Value = "", "", "TOB.AccCode = '" & ddlTaxObjectGrp.SelectedItem.Value & "' AND ")
        strSearch = strSearch + IIf(Trim(srchDescription.Text) = "", "", "TOB.Description LIKE '%" & Trim(srchDescription.Text) & "%' AND ")
        strSearch = strSearch + IIf(srchStatus.SelectedItem.Value = "0", "", "TOB.Status = '" & Trim(srchStatus.SelectedItem.Value) & "' AND ")
        strSearch = strSearch + IIf(Trim(txtLastUpdate.Text) = "", "", "USR.UserName LIKE '" & Trim(txtLastUpdate.Text) & "%' AND ")
        strSearch = strSearch + IIf(Session("SS_COACENTRALIZED") = "1", "", " GL.LocCode = '" & strLocation & "'  AND ")

        If (Trim(strSearch) <> "") Then
            strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If

        strSearch = strSearch + IIf(Trim(SortExpression.Text) = "", "", "Order By " & Trim(SortExpression.Text) & " ")
        strSearch = strSearch + IIf(Trim(SortCol.Text) = "", "ASC ", Trim(SortCol.Text))

        strParamName = "STRSEARCH"
        strParamValue = strSearch

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Return ObjTaxDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
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

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strOpCd_UPD As String = "TX_CLSSETUP_TAXOBJECTRATE_LIST_UPDATE"
        Dim strParam As String = ""
        Dim strSelTaxID As String
        Dim strSelStatus As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxID")
        strSelTaxID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblStatus")
        strSelStatus = lblDelText.Text

        strParamName = "STATUS|UPDATEID|TaxID"
        strParamValue = IIf(strSelStatus = objCTSetup.EnumStockItemStatus.Active, objCTSetup.EnumStockItemStatus.Deleted, objCTSetup.EnumStockItemStatus.Active) & "|" & _
                        strUserId & "|" & _
                        Trim(strSelTaxID)

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_UPD, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/TX_Setup_TaxObjectList.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("TX_Setup_TaxObjectRateDet.aspx")
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATE_GROUP_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet

        strParamName = "STRSEARCH"
        strParamValue = "WHERE AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & strLocation & "')"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Please select tax group"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTaxObjectGrp.DataSource = ObjTaxDs.Tables(0)
        ddlTaxObjectGrp.DataValueField = "AccCode"
        ddlTaxObjectGrp.DataTextField = "Description"
        ddlTaxObjectGrp.DataBind()
        ddlTaxObjectGrp.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
        End If
    End Sub

    Sub btnSyncData_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATE_SYNCHRONIZED"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "ORICOMPCODE"
        strParamValue = strCompany
      
        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try
        BindGrid()
    End Sub
End Class
