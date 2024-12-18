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


Public Class PU_setup_UserGroupList : Inherits Page

    Protected WithEvents dgSuppList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtUserCode As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtTypeProduk As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents NewSuppBtn As ImageButton

    Dim ObjOk As New agri.GL.ClsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objAdminLoc As New agri.Admin.clsLoc()

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


    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
        intLevel = Session("SS_USRLEVEL")
        intLocLevel = Session("SS_LOCLEVEL")


        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUSupplier), intPUAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "SupplierCode"
            End If

            If Not Page.IsPostBack Then
                BindGrid()
                BindPageList()
            End If

            If intLocLevel = objAdminLoc.EnumLocLevel.HQ Then
                If intLevel < 1 Then
                    NewSuppBtn.Visible = False
                Else
                    NewSuppBtn.Visible = True
                End If
            End If

        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgSuppList.CurrentPageIndex = 0
        dgSuppList.EditItemIndex = -1
        BindGrid()
        BindPageList()
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
        Dim strOppCd_GET As String = "PU_CLSSETUP_SUPPLIER_USER_LIST_GET"
        Dim strParam As String
        Dim SearchStr As String
        Dim objTransDs As New Object()
        Dim ssQLKriteria As String = ""
        Dim intErrNo As Integer

        If txtUserCode.Text <> "" Then
            ssQLKriteria = "Where UserID LIKE '%" & txtUserCode.Text & "%'"
        ElseIf txtName.Text <> "" Then
            ssQLKriteria = "Where UserName LIKE '%" & txtName.Text & "%'"
        ElseIf txtTypeProduk.Text <> "" Then
            ssQLKriteria = "Where ProdTypeCode LIKE '%" & txtTypeProduk.Text & "%' OR TypeName LIKE '%" & txtTypeProduk.Text & "%'"
        End If

        strParam = "SEARCHSTR"
        SearchStr = ssQLKriteria

        Try
            intErrNo = ObjOk.mtdGetDataCommon(strOppCd_GET, strParam, SearchStr, objTransDs)
        Catch Exp As System.Exception
            Response.Write("/include/mesg/ErrorMessage.aspx?errcode=IN_CLSTRX_STOCKTRANSFERDIV_DISPLAYLINE_GET&errmesg=" & Exp.Message & "&redirect=HR/trx/HR_trx_EmployeeList_Estate.aspx")
        End Try

        Return objTransDs
    End Function

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
        Response.Redirect("PU_setup_UserGroup.aspx")
    End Sub

End Class
