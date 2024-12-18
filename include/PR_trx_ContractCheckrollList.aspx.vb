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


Public Class PR_trx_ContractCheckrollList : Inherits Page

    Protected WithEvents dgContCheckList As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblErrFromAttdDate As Label
    Protected WithEvents lblErrToAttdDate As Label
    Protected WithEvents lblErrAttdDate As Label
    Protected WithEvents lblDateFormat As Label
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtSuppCode As TextBox
    Protected WithEvents txtName As TextBox
    Protected WithEvents txtFromAttdDate As TextBox
    Protected WithEvents txtToAttdDate As TextBox
    Protected WithEvents txtAttdCode As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox

    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPRAR As Long
    Dim strDateSetting As String

    Dim objContDs As New Object()
    Dim intErrNo As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strDateSetting = Session("SS_DATEFMT")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxContractorCheckroll), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrFromAttdDate.Visible = False
            lblErrToAttdDate.Visible = False
            lblDateFormat.Visible = False
            lblErrAttdDate.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "CA.SupplierCode, CA.AttDate, CA.AttCode"
            End If

            If Not Page.IsPostBack Then
                BindStatusList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindStatusList()

        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetContractCheckrollStatus(objPRTrx.EnumContractCheckroll.All), objPRTrx.EnumContractCheckroll.All))
        ddlStatus.Items.Add(New ListItem(objPRTrx.mtdGetContractCheckrollStatus(objPRTrx.EnumContractCheckroll.Active), objPRTrx.EnumContractCheckroll.Active))

        ddlStatus.SelectedIndex = 1
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgContCheckList.CurrentPageIndex = 0
        dgContCheckList.EditItemIndex = -1
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
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgContCheckList.PageSize)

        dgContCheckList.DataSource = dsData
        If dgContCheckList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgContCheckList.CurrentPageIndex = 0
            Else
                dgContCheckList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgContCheckList.DataBind()
        BindPageList()
        PageNo = dgContCheckList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgContCheckList.PageCount

        For intCnt = 0 To dgContCheckList.Items.Count - 1
            lblStatus = dgContCheckList.Items.Item(intCnt).FindControl("lblStatus")
            Select Case Trim(lblStatus.Text)
                Case objPRTrx.EnumContractCheckroll.Active
                    lbButton = dgContCheckList.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
            End Select
        Next

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgContCheckList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgContCheckList.CurrentPageIndex
    End Sub

    Protected Function LoadData() As DataSet
        Dim strOpCd_Get As String = "PR_CLSTRX_CONTRACTOR_CHECKROLL_GET"
        Dim strSrchSuppCode As String
        Dim strSrchName As String
        Dim strSrchFromAttdDate As String
        Dim strSrchToAttdDate As String
        Dim strSrchAttdCode As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim strValidDate As String
        Dim strDateFormat As String

        If txtFromAttdDate.Text <> "" Then
            If objGlobal.mtdValidInputDate(strDateSetting, txtFromAttdDate.Text, strDateFormat, strValidDate) = False Then
                lblDateFormat.Text = strDateFormat & "."
                lblDateFormat.Visible = True
                lblErrAttdDate.Visible = True
                strSrchFromAttdDate = ""
            Else
                strSrchFromAttdDate = strValidDate
            End If
        End If

        If txtToAttdDate.Text <> "" Then
            If objGlobal.mtdValidInputDate(strDateSetting, txtToAttdDate.Text, strDateFormat, strValidDate) = False Then
                lblDateFormat.Text = strDateFormat & "."
                lblDateFormat.Visible = True
                lblErrAttdDate.Visible = True
                strSrchToAttdDate = ""
            Else
                strSrchToAttdDate = strValidDate
            End If
        End If

        If txtFromAttdDate.Text <> "" And txtToAttdDate.Text = "" Then
            lblErrToAttdDate.Visible = True
            strSrchFromAttdDate = ""
        ElseIf txtFromAttdDate.Text = "" And txtToAttdDate.Text <> "" Then
            lblErrFromAttdDate.Visible = True
            strSrchToAttdDate = ""
        End If

        strSrchSuppCode = IIf(txtSuppCode.Text = "", "", txtSuppCode.Text)
        strSrchName = IIf(txtName.Text = "", "", txtName.Text)
        strSrchAttdCode = IIf(txtAttdCode.Text = "", "", txtAttdCode.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = objPRTrx.EnumContractCheckroll.All, "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchSuppCode & "|" & _
                   strSrchName & "|" & _
                   strSrchFromAttdDate & "|" & _
                   strSrchToAttdDate & "|" & _
                   strSrchAttdCode & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "||" & _
                   SortExpression.Text & "|" & SortCol.Text & "|"

        Try
            intErrNo = objPRTrx.mtdGetContractCheckroll(strOpCd_Get, _
                                                        strLocation, _
                                                        strParam, _
                                                        objContDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_CONTCHECKLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/PR_trx_ContractCheckrollList.aspx")
        End Try

        For intCnt = 0 To objContDs.Tables(0).Rows.Count - 1
            objContDs.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(objContDs.Tables(0).Rows(intCnt).Item("SupplierCode"))
            objContDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objContDs.Tables(0).Rows(intCnt).Item("Name"))
            objContDs.Tables(0).Rows(intCnt).Item("AttDate") = Trim(objContDs.Tables(0).Rows(intCnt).Item("AttDate"))
            objContDs.Tables(0).Rows(intCnt).Item("AttCode") = Trim(objContDs.Tables(0).Rows(intCnt).Item("AttCode"))
            objContDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objContDs.Tables(0).Rows(intCnt).Item("Status"))
            objContDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objContDs.Tables(0).Rows(intCnt).Item("UserName"))
        Next

        Return objContDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgContCheckList.CurrentPageIndex = 0
            Case "prev"
                dgContCheckList.CurrentPageIndex = _
                Math.Max(0, dgContCheckList.CurrentPageIndex - 1)
            Case "next"
                dgContCheckList.CurrentPageIndex = _
                Math.Min(dgContCheckList.PageCount - 1, dgContCheckList.CurrentPageIndex + 1)
            Case "last"
                dgContCheckList.CurrentPageIndex = dgContCheckList.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgContCheckList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgContCheckList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgContCheckList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgContCheckList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd_Del As String = "PR_CLSTRX_CONTRACTOR_CHECKROLL_DEL"
        Dim label As label
        Dim strAttdID As String

        label = e.Item.FindControl("lblAttdID")
        strAttdID = label.Text

        Try
            intErrNo = objPRTrx.mtdDelContractCheckroll(strOpCd_Del, _
                                                        strAttdID, _
                                                        strLocation)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_CLSTRX_CONTRACT_CHECKROLLLIST_DEL&errmesg=" & Exp.ToString() & "&redirect=pr/trx/PR_trx_ContractCheckrollList.aspx")
        End Try

        BindGrid()
    End Sub

    Sub NewSuppBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PR_trx_ContractCheckrollDet.aspx")
    End Sub


End Class
