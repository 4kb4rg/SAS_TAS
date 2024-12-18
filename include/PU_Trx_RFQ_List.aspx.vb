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


Imports agri.PU.clsSetup
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PU_Trx_RFQ_List : Inherits Page

    Protected WithEvents dgSuppList As DataGrid

    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents TxtUserPo As TextBox
    Protected WithEvents txtRfqID As TextBox
    Protected WithEvents txtPRID As TextBox

    Protected WithEvents lblTracker As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList

    Protected WithEvents NewSuppBtn As ImageButton

    Dim ObjOk As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim strAccMonth As String
    Dim strAccYear As String

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim objSuppDs As New Object()

    Dim strSupplierCodeTag As String
    Dim strSelectedSupp As String
    Dim strDescTag As String
    Dim strTitleTag As String
    Dim intLevel As Integer
    Dim intLocLevel As Integer

#Region "TOOLS & COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intLevel = Session("SS_USRLEVEL")
        intLocLevel = Session("SS_LOCLEVEL")
        strAccMonth = Session("SS_SELACCMONTH")
        strAccYear = Session("SS_PUACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PURPH), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "SupplierCode"
            End If

            If Not Page.IsPostBack Then
                lstAccMonth.SelectedValue = strAccMonth
                lstAccYear.SelectedValue = strAccYear
                BindAccYear(strAccYear)
                BindGrid()
                BindPageList()
            End If

            'If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
            '    If intLevel < 1 Then
            '        NewSuppBtn.Visible = False
            '    Else
            '        NewSuppBtn.Visible = True
            '    End If
            'End If

        End If
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgSuppList.CurrentPageIndex = 0
            Case "prev"
                dgSuppList.CurrentPageIndex = _
                Math.Max(0, dgSuppList.CurrentPageIndex - 1)
            Case "next"
                dgSuppList.CurrentPageIndex = _
                Math.Min(dgSuppList.PageCount - 1, dgSuppList.CurrentPageIndex + 1)
            Case "last"
                dgSuppList.CurrentPageIndex = dgSuppList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgSuppList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgSuppList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgSuppList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgSuppList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
    End Sub

    Sub Supplier_Synchronized(ByVal pv_strSplCode As String)
        Dim strOpCd_Upd As String = "PU_CLSSETUP_SUPPLIER_SYNCHRONIZED"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim objEmp As New Object()

        If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
            strSelectedSupp = Trim(pv_strSplCode)
            strParamName = "SUPPLIERCODE|ORICOMPCODE"
            strParamValue = strSelectedSupp & "|" & Trim(strCompany)

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_Upd, strParamName, strParamValue)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

        End If
    End Sub

    Sub NewSuppBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PU_Trx_RFQ_Detail.aspx")
    End Sub

#End Region

#Region "local & procedure"
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

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgSuppList.CurrentPageIndex = 0
        dgSuppList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

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

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim lblStatus As Label

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgSuppList.PageSize)

        dgSuppList.DataSource = dsData
        If dgSuppList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgSuppList.CurrentPageIndex = 0
            Else
                dgSuppList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgSuppList.DataBind()
        BindPageList()
        PageNo = dgSuppList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgSuppList.PageCount

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgSuppList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgSuppList.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCd_GET As String = "PU_CLSTRX_RFQ_LIST_GET"
        Dim strParam As String
        Dim SearchStr As String
        Dim objTransDs As New Object()
        Dim ssQLKriteria As String = ""
        Dim intErrNo As Integer
        Dim IntAccMonth As Integer

        If intLevel <= 1 Then
            ssQLKriteria = "AND r.UserPO='" & strUserId & "'"
        Else
            ssQLKriteria = ""
        End If


        IntAccMonth = lstAccMonth.SelectedItem.Value

        If TxtUserPo.Text <> "" Then
            ssQLKriteria = ssQLKriteria & "AND r.UserPO LIKE '%" & TxtUserPo.Text & "%' OR u.UserName LIKE '" & TxtUserPo.Text & "'"
        End If

        If txtRfqID.Text <> "" Then
            ssQLKriteria = ssQLKriteria & "AND r.RfqID LIKE '%" & txtRfqID.Text & "%'"
        End If

        If txtPRID.Text <> "" Then
            ssQLKriteria = ssQLKriteria & "AND  r.PRID LIKE '%" & txtPRID.Text & "%'"
        End If

        If ddlStatus.SelectedIndex > 0 Then
            ssQLKriteria = ssQLKriteria & "AND  r.Status LIKE '%" & ddlStatus.SelectedItem.Value & "'"
        End If

        If IntAccMonth > 0 Then
            ssQLKriteria = ssQLKriteria & "AND  r.AccMonth='" & lstAccMonth.SelectedValue & "' "
        End If

        ssQLKriteria = ssQLKriteria & "AND r.AccYear='" & lstAccYear.SelectedItem.Value & "'"

        strParam = "LOC|STRSEARCH"
        SearchStr = strLocation & "|" & ssQLKriteria

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOppCd_GET, strParam, SearchStr, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objTransDs
    End Function

#End Region
End Class
