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


Public Class PD_trx_YearOfPlantYieldList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtYearOfPlant As TextBox
    Protected WithEvents txtGroupRef As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objPDTrx As New agri.PD.clsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objYearPlantDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intPDAR As Integer

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        intPDAR = Session("SS_PDAR")

        If strUserId = "" Then
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDYearOfPlantYield), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "yld.Year"
            End If
            If Not Page.IsPostBack Then
                BindStatusList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub BindStatusList()
        ddlStatus.Items.Add(New ListItem("All", ""))
        ddlStatus.Items.Add(New ListItem(objPDTrx.mtdGetYearOfPlantYieldStatus(objPDTrx.EnumYearOfPlantYieldStatus.Active), objPDTrx.EnumYearOfPlantYieldStatus.Active))
        ddlStatus.Items.Add(New ListItem(objPDTrx.mtdGetYearOfPlantYieldStatus(objPDTrx.EnumYearOfPlantYieldStatus.Deleted), objPDTrx.EnumYearOfPlantYieldStatus.Deleted))
        ddlStatus.SelectedIndex = 1
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
                Case objPDTrx.EnumMPOBPriceStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPDTrx.EnumMPOBPriceStatus.Deleted
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
        Dim strOpCdGet As String = "PD_CLSTRX_YEAR_OF_PLANT_YIELD_GET"
        Dim strSearch As String = ""
        Dim strSort As String = ""
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSearch = strSearch & "and yld.LocCode = '" & Trim(strLocation) & "' " & _
                                "and yld.AccMonth = '" & Trim(strAccMonth) & "' " & _
                                "and yld.AccYear = '" & Trim(strAccYear) & "' "

        If Trim(txtYearOfPlant.Text) <> "" Then
            strSearch = strSearch & "and yld.Year like '" & Trim(txtYearOfPlant.Text) & "%' "
        End If

        If Trim(txtGroupRef.Text) <> "" Then
            strSearch = strSearch & "and yld.GroupRef like '" & Trim(txtGroupRef.Text) & "%' "
        End If

        If ddlStatus.SelectedItem.Value <> "" Then
            strSearch = strSearch & "and yld.Status = '" & ddlStatus.SelectedItem.Value & "' "
        End If

        If Trim(txtLastUpdate.Text) <> "" Then
            strSearch = strSearch & "and usr.UserName like '" & Trim(txtLastUpdate.Text) & "%' "
        End If

        strSort = "order by " & Trim(SortExpression.Text) & " " & SortCol.Text
        strParam = strSearch & "|" & strSort

        Try
            intErrNo = objPDTrx.mtdGetYearOfPlantYield(strOpCdGet, strParam, objYearPlantDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_YEAROFPLANTYIELDLIST_GET&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_yearofplantyieldlist.aspx")
        End Try


        Return objYearPlantDs

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
        SortCol.Text = IIf(SortCol.Text = "asc", "desc", "asc")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim strOpCd_GET As String = ""
        Dim strOpCd_ADD As String = ""
        Dim strOpCd_UPD As String = "PD_CLSTRX_YEAR_OF_PLANT_YIELD_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strCompositKey As String
        Dim strYearOfPlant As String
        Dim strGroupRef As String
        Dim strRefDate As String
        
        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblCompositKey")
        strCompositKey = lbl.Text

        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblYearOfPlant")
        strYearOfPlant = lbl.Text

        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblGroupRef")
        strGroupRef = lbl.Text

        lbl = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblRefDate")
        strRefDate = lbl.Text

        strParam = strYearOfPlant & Chr(9) & _
                   strGroupRef & Chr(9) & _
                   strRefDate & Chr(9) & Chr(9) & Chr(9) & _
                   objPDTrx.EnumYearOfPlantYieldStatus.Deleted

        Try
            intErrNo = objPDTrx.mtdUpdYearOfPlantYield(strOpCd_GET, _
                                                        strOpCd_ADD, _
                                                        strOpCd_UPD, _
                                                        "", _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam, _
                                                        "", _
                                                        False, _
                                                        0, _
                                                        True)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_YEAROFPLANTYIELDLIST_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/pd_trx_yearofplantyieldlist.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub NewTBBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("PD_trx_YearOfPlantYieldDet.aspx")
    End Sub





End Class
