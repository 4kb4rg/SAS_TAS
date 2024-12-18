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
Imports agri.HR.clsSetup
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap
Imports agri.GL

Public Class HR_setup_Deptlist : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtDivID As TextBox
    Protected WithEvents txtDivHead As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCode As Label
    Protected WithEvents lblDepartment As Label
    Protected WithEvents lblTitle As Label

    Protected objHRSetup As New agri.HR.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim ObjOK As New agri.GL.ClsTrx
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intHRAR As Long

    Dim objDeptDs As New Object()
    Dim strLocType As String
    Dim ParamNama As String
    Dim ParamValue As String

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        intHRAR = Session("SS_HRAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumHRAccessRights.HRCompany), intHRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "DivID"
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
                Case objHRSetup.EnumDeptStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Text = "Delete"
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objHRSetup.EnumDeptStatus.Deleted
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Text = "Undelete"
                    lbButton.Visible = True
            End Select
        Next

    End Sub 

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
        Dim strOpCd_GET As String = "HR_HR_STP_DIVISI_GET"
        Dim strSearch As String
        Dim sortitem As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSearch = " AND A.LocCode='" & strLocation &"' AND A.Status like '" & IIf(Not ddlStatus.SelectedItem.Value = "0", _
                       ddlStatus.SelectedItem.Value, "%") & "' "
        If Not txtDivID.Text = "" Then
            strSearch = strSearch & " AND A.DivID like '%" & txtDivID.Text & "%' "
        End If
        If Not txtDescription.Text = "" Then
            strSearch = strSearch & " AND Description like '%" & txtDescription.Text & "%' "
        End If
        If Not txtDivHead.Text = "" Then
            strSearch = strSearch & " AND DeptHead like '%" & _
                        txtDivHead.Text & "%' "
        End If
        If Not txtLastUpdate.Text = "" Then
            strSearch = strSearch & " AND B.UserName like '%" & _
                        txtLastUpdate.Text & "%' "
        End If

        
        sortitem = "ORDER BY " & sortexpression.text & " " & sortcol.text
        ParamNama = "SEARCH|SORT"
        ParamValue = strSearch & "|" & sortitem

       
        Try
            intErrNo = ObjOK.mtdGetDataCommon(strOpCd_GET, ParamNama, ParamValue, objDeptDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objDeptDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objDeptDs.Tables(0).Rows.Count - 1
                objDeptDs.Tables(0).Rows(intCnt).Item("DivID") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("DivID"))
                objDeptDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("Status"))
                objDeptDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objDeptDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objDeptDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objDeptDs
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
        SortCol.Text = IIF(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub

    Sub DEDR_Delete(Sender As Object, e As DataGridCommandEventArgs)
        Dim strOpCd_UPD As String = "HR_HR_STP_DIVISI_UPD"
        Dim int As Integer = e.Item.ItemIndex
        Dim DivCell As TableCell = e.Item.Cells(0)
        Dim strDivID As String
        Dim strCodeDiv As String
        Dim strDeptHead As String
        Dim strCreateDate As String
        Dim strStatus As String
        Dim intErrNo As Integer
        Dim EditText As Label

        strDivID = DivCell.Text
        EditText = dgLine.Items.Item(int).FindControl("CodeDiv")
        strCodeDiv = EditText.Text
        EditText = dgLine.Items.Item(int).FindControl("DeptHead")
        strDeptHead = EditText.Text
        EditText = dgLine.Items.Item(int).FindControl("CreateDate")
        strCreateDate = EditText.Text
        EditText = dgLine.Items.Item(int).FindControl("Status")
        strStatus = IIf(Trim(EditText.Text) = "1", "2", "1")
        ParamNama = "DID|CID|Loc|DH|ST|CD|UD|UI"
        ParamValue = strDivID & "|" & strCodeDiv & "|" & strLocation & "|" & strDeptHead & "|" & strStatus & "|" & _
                      strCreateDate & "|" & DateTime.Now & "|" & strUserId

        Try
            intErrNo = ObjOK.mtdInsertDataCommon(strOpCd_UPD, ParamNama, ParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=HR_SETUP_DEPT_DEL&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewDeptBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("HR_setup_Divdet_Estate.aspx")
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        'lblTitle.text = UCase(GetCaption(objLangCap.EnumLangCap.Department))
        'lblDepartment.text = GetCaption(objLangCap.EnumLangCap.Department)

        'dgLine.Columns(1).HeaderText = lblDepartment.text & lblCode.text
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LANGCAP&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                If strLocType = objAdminLoc.EnumLocType.Mill then
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTermMW"))
                else
                    Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                end if
                Exit For
            End If
        Next
    End Function



End Class
