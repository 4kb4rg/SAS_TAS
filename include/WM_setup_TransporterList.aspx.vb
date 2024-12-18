
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

Public Class WM_TransporterList : Inherits Page

    Protected WithEvents dgTransporterList As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblErrMessage As Label

    Protected WithEvents srchTransCode As TextBox
    Protected WithEvents srchName As TextBox
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lstDropList As DropDownList

    Protected objWM As New agri.WM.clsSetup()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intWMAR As Integer
    Dim objTransDs As New DataSet()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMTransporterSetup), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TransporterCode"
            End If

            If Not Page.IsPostBack Then
                BindSearchList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindSearchList()

        srchStatusList.Items.Add(New ListItem(objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.All), objWM.EnumTransporterStatus.All))
        srchStatusList.Items.Add(New ListItem(objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Active), objWM.EnumTransporterStatus.Active))
        srchStatusList.Items.Add(New ListItem(objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Deleted), objWM.EnumTransporterStatus.Deleted))

        srchStatusList.SelectedIndex = 1
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgTransporterList.CurrentPageIndex = 0
        dgTransporterList.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String


        Dim PageNo As Integer 
        Dim PageCount As Integer
        Dim dsData As DataSet
        
        dsData = LoadData
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTransporterList.PageSize)
        
        dgTransporterList.DataSource = dsData
        If dgTransporterList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTransporterList.CurrentPageIndex = 0
            Else
                dgTransporterList.CurrentPageIndex = PageCount - 1
            End If
        End If
        
        dgTransporterList.DataBind()
        BindPageList()
        PageNo = dgTransporterList.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgTransporterList.PageCount

        For intCnt = 0 To dgTransporterList.Items.Count - 1
            Status = dgTransporterList.Items.Item(intCnt).FindControl("lblStatus")
            strStatus = Status.Text

            Select Case strStatus
                Case objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Active)
                    lbButton = dgTransporterList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objWM.mtdGetTransporterStatus(objWM.EnumTransporterStatus.Deleted)
                    lbButton = dgTransporterList.Items.Item(intCnt).FindControl("Delete")
                    lbButton.Visible = False
            End Select
        Next

    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgTransporterList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgTransporterList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "WM_CLSSETUP_TRANSPORTER_GET"
        Dim strSrchTransCode As String
        Dim strSrchName As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchTransCode = IIf(srchTransCode.Text = "", "", srchTransCode.Text)
        strSrchName = IIf(srchName.Text = "", "", srchName.Text)
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objWM.EnumTransporterStatus.All, "", srchStatusList.SelectedItem.Value)
        strSrchLastUpdate = IIf(srchUpdateBy.Text = "", "", srchUpdateBy.Text)

        strParam = strSrchTransCode & "|" & _
                   strSrchName & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "||" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text

        Try
            intErrNo = objWM.mtdGetTransporter(strOppCode_Get, strParam, objTransDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_LIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=WM/Setup/WM_setup_TransporterList.aspx")
        End Try

        For intCnt = 0 To objTransDs.Tables(0).Rows.Count - 1
            objTransDs.Tables(0).Rows(intCnt).Item("TransporterCode") = Trim(objTransDs.Tables(0).Rows(intCnt).Item("TransporterCode"))
            objTransDs.Tables(0).Rows(intCnt).Item("Name") = Trim(objTransDs.Tables(0).Rows(intCnt).Item("Name"))
            objTransDs.Tables(0).Rows(intCnt).Item("UpdateDate") = Trim(objTransDs.Tables(0).Rows(intCnt).Item("UpdateDate"))
            objTransDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objTransDs.Tables(0).Rows(intCnt).Item("Status"))
            objTransDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objTransDs.Tables(0).Rows(intCnt).Item("UserName"))
        Next

        Return objTransDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgTransporterList.CurrentPageIndex = 0
            Case "prev"
                dgTransporterList.CurrentPageIndex = _
                Math.Max(0, dgTransporterList.CurrentPageIndex - 1)
            Case "next"
                dgTransporterList.CurrentPageIndex = _
                Math.Min(dgTransporterList.PageCount - 1, dgTransporterList.CurrentPageIndex + 1)
            Case "last"
                dgTransporterList.CurrentPageIndex = dgTransporterList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgTransporterList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgTransporterList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgTransporterList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgTransporterList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd_Transporter_Upd As String = "WM_CLSSETUP_TRANSPORTER_UPD"
        Dim strOpCd_Transporter_Add As String = ""
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strSelectedTrans As String
        Dim lblTransCode As Label
        Dim arrParam As Array
        Dim strTType As String

        dgTransporterList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTransCode = dgTransporterList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTransCode")
        arrParam = Split(Trim(lblTransCode.Text), "|")
        strSelectedTrans = arrParam(0)
        strTType = arrParam(1)
        
        strParam = strSelectedTrans & "||||" & objWM.EnumTransporterStatus.Deleted & "|" & strTType
        Try
            intErrNo = objWM.mtdUpdTransporter(strOpCd_Transporter_Add, _
                                                strOpCd_Transporter_Upd, _
                                                strCompany, _
                                                strLocation, _
                                                strUserId, _
                                                strParam, _
                                                True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=WM_SETUP_TRANSPORTER_LIST_DEL&errmesg=" & lblErrMessage.Text & "&redirect=WM/Setup/WM_setup_TransporterList.aspx")
        End Try

        dgTransporterList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTransBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WM_setup_TransporterDet.aspx")
    End Sub



End Class
