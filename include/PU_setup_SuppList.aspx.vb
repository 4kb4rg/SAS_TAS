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


Public Class PU_SuppList : Inherits Page

    Protected WithEvents dgSuppList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents ddlSuppType As DropDownList
    Protected WithEvents NewSuppBtn As ImageButton
    Protected WithEvents cbExcel As CheckBox



    Protected objPU As New agri.PU.clsSetup()
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


    Sub Page_Load(Sender As Object, E As EventArgs)
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

    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
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
        
        dsData = LoadData
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
        lblTracker.Text="Page " & pageno & " of " & dgSuppList.PageCount

        For intCnt = 0 To dgSuppList.Items.Count - 1
            lblStatus = dgSuppList.Items.Item(intCnt).FindControl("lblStatus")
            Select Case Trim(lblStatus.Text)
                Case objPU.EnumSuppStatus.Active
                    
                    lbButton = dgSuppList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"


                Case objPU.EnumSuppStatus.Deleted
                    lbButton = dgSuppList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next
    
    End Sub 

    Sub BindPageList() 
        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgSuppList.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgSuppList.CurrentPageIndex
    End Sub 

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PU_CLSSETUP_SUPPLIER_GET"
        Dim strSrchSuppCode as string
        Dim strSrchName as string
        Dim strSrchStatus as string
        Dim strSrchLastUpdate as string
        Dim strSearch as string
        Dim strParam as string
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strSrchSuppType As String

        strSrchSuppCode = IIf(txtSuppCode.Text = "", "", txtSuppCode.Text)
        strSrchName = IIf(txtName.Text = "", "", txtName.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strSrchSuppType = IIf(ddlSuppType.SelectedItem.Value = "0", "", ddlSuppType.SelectedItem.Value)

        strParam = strSrchSuppCode & "|" & _
                   strSrchName & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "||" & _
                   IIf(Session("SS_COACENTRALIZED") = "1", "", " A.AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & Trim(strLocation) & "') ") & "|" & _
                   strSrchSuppType

        Try
            intErrNo = objPU.mtdGetSupplier(strOpCd_Get, strParam, objSuppDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_SUPPLIERLIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objSuppDs.Tables(0).Rows.Count - 1
            objSuppDs.Tables(0).Rows(intCnt).Item(0) = Trim(objSuppDs.Tables(0).Rows(intCnt).Item(0))
            objSuppDs.Tables(0).Rows(intCnt).Item(1) = Trim(objSuppDs.Tables(0).Rows(intCnt).Item(1))
            objSuppDs.Tables(0).Rows(intCnt).Item(2) = Trim(objSuppDs.Tables(0).Rows(intCnt).Item(2))
            objSuppDs.Tables(0).Rows(intCnt).Item(3) = Trim(objSuppDs.Tables(0).Rows(intCnt).Item(3))
            objSuppDs.Tables(0).Rows(intCnt).Item(4) = Trim(objSuppDs.Tables(0).Rows(intCnt).Item(4))
            objSuppDs.Tables(0).Rows(intCnt).Item(5) = Trim(objSuppDs.Tables(0).Rows(intCnt).Item(5))
            objSuppDs.Tables(0).Rows(intCnt).Item(6) = Trim(objSuppDs.Tables(0).Rows(intCnt).Item(6))
            objSuppDs.Tables(0).Rows(intCnt).Item(7) = Trim(objSuppDs.Tables(0).Rows(intCnt).Item(7))
        Next                

        Return objSuppDs
    End Function


    Sub btnPreview_Click (sender As Object, e As ImageClickEventArgs)
            Dim strStatus As String
            Dim strSupplierCode As String
            Dim strSupplierName As String
            Dim strUpdateBy As String
            Dim strSortExp As String
            Dim strSortCol As String
        Dim IsExportExcel As String = "0"

            strSupplierCodeTag = "Supplier Code"
            strDescTag = "Name"
            strTitleTag = "Supplier List"

            strStatus = IIF(Not ddlStatus.selectedItem.Value = objPU.EnumSuppStatus.All, ddlStatus.selectedItem.Value, "")
            strSupplierCode = txtSuppCode.text
            strSupplierName = txtName.text
            strUpdateBy =  txtLastUpdate.text
            strSortExp = sortexpression.text
        strSortCol = SortCol.Text

        If cbExcel.Checked = True Then
            IsExportExcel = "1"
        Else
            IsExportExcel = "0"
        End If

        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/PU_Rpt_SupplierList.aspx?strSupplierCodeTag=" & strSupplierCodeTag & _
                    "&strDescTag=" & strDescTag & "&strTitleTag=" & strTitleTag & _
                    "&strStatus=" & strStatus & _
                    "&ExportToExcel=" & IsExportExcel & _
                    "&strSupplierCode=" & strSupplierCode & "&strSupplierName=" & strSupplierName & _
                    "&strUpdateBy=" & strUpdateBy & "&strSortExp=" & strSortExp & _
                    "&strSortCol=" & strSortCol & """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
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

    Sub PagingIndexChanged (sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            dgSuppList.CurrentPageIndex = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        dgSuppList.CurrentPageIndex=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgSuppList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub
 
    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim strOpCd_Add As String = ""
        Dim strOpCd_Upd As String = "PU_CLSSETUP_SUPPLIER_UPD"
        Dim strParam As String = ""
        Dim SuppCodeCell As TableCell = E.Item.Cells(0)
        Dim intErrNo As Integer

        strSelectedSupp = SuppCodeCell.Text
        strParam = strSelectedSupp & "|||||||||||||||||||" & objPU.EnumSuppStatus.Deleted & "||||||||||||||||||"

        Try
            intErrNo = objPU.mtdUpdSupplier(strOpCd_Add, _
                                            strOpCd_Upd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strParam, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_DEL_SUPPLIER&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        Supplier_Synchronized(strSelectedSupp)
        dgSuppList.EditItemIndex = -1
        BindGrid()
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

    Sub NewSuppBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PU_setup_SuppDet.aspx")
    End Sub

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
