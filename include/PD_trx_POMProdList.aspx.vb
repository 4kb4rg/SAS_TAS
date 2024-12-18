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
Imports agri.PD.clsTrx
Imports agri.GlobalHdl.clsGlobalHdl

Public Class PD_trx_POMProdList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtProduction As TextBox
    Protected WithEvents txtType As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label

    Protected objPDTrx As New agri.PD.clsTRx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPDAR As Integer

    Dim objPOMProdDs As New Object()

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PDACCMONTH")
        strAccYear = Session("SS_PDACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPDAR = Session("SS_PDAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPDAccessRights.PDPOMProduction), intPDAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
           If SortExpression.Text = "" Then
                SortExpression.Text = "POM.POMProdID"
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
                Case objPDTrx.EnumPOMYieldStatus.Confirmed 
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPDTrx.EnumPOMYieldStatus.Deleted
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
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
        Dim strOpCd_GET As String = "PD_CLSTRX_POMYIELD_SEARCH"
        Dim strSrchProd As String
        Dim strSrchType As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchProd = IIF(txtProduction.Text = "", "", txtProduction.Text)
        strSrchType = IIF(txtType.Text = "", "", txtType.Text)
        strSrchStatus = IIF(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIF(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchProd & "|" & _
                   strSrchType & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"

        Try
            intErrNo = objPDTrx.mtdGetPOMYield(strOpCd_GET, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objPOMProdDs, _
                                            False)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objPOMProdDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objPOMProdDs.Tables(0).Rows.Count - 1
                objPOMProdDs.Tables(0).Rows(intCnt).Item("POMProdID") = Trim(objPOMProdDs.Tables(0).Rows(intCnt).Item("POMProdID"))
                objPOMProdDs.Tables(0).Rows(intCnt).Item("POMProdNameCode") = Trim(objPOMProdDs.Tables(0).Rows(intCnt).Item("POMProdNameCode"))
                objPOMProdDs.Tables(0).Rows(intCnt).Item("POMTypeCode") = Trim(objPOMProdDs.Tables(0).Rows(intCnt).Item("POMTypeCode"))
                objPOMProdDs.Tables(0).Rows(intCnt).Item("Weight") = Trim(objPOMProdDs.Tables(0).Rows(intCnt).Item("Weight"))
                objPOMProdDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objPOMProdDs.Tables(0).Rows(intCnt).Item("Status"))
                objPOMProdDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objPOMProdDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objPOMProdDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objPOMProdDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objPOMProdDs
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
        Dim strOpCd As String = "PD_CLSTRX_POMYIELD_STATUS_UPD"
        Dim objYieldID As String
        Dim strParam As String = ""
        Dim strSelectedPOMProd As String 
        Dim strWeight As String
        Dim strType As String
        Dim strTank As String
        Dim intErrNo As Integer
        Dim lblTemp As Label

        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)

        lblTemp = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblId")
        strSelectedPOMProd = lblTemp.Text

        lblTemp = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblWeight")
        strWeight = lblTemp.Text

        lblTemp = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblType")
        strType = lblTemp.Text

        lblTemp = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTank")
        strTank = lblTemp.Text

        strParam = strSelectedPOMProd & "|" & objPDTrx.EnumPOMYieldStatus.Deleted & "|" & _
                   strWeight & "|" & strType & "|" & strTank
        
        Try
            intErrNo = objPDTrx.mtdUpdPOMYield("", _
                                            strOpCd, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            True, _
                                            objYieldID)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PD_TRX_POMPROD_DEL&errmesg=" & lblErrMessage.Text & "&redirect=pd/trx/PD_trx_POMProdList.aspx")
        End Try

        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewPOMBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PD_trx_POMProdDet.aspx")
    End Sub

End Class
