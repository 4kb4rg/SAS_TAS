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

Imports agri.PR
Imports agri.GlobalHdl.clsGlobalHdl


Public Class PR_Trx_WPList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton    
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents txtWPTrxId As TextBox
    Protected WithEvents txtDescription As TextBox
    Protected WithEvents txtGangCode As TextBox
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblCurrentIndex As Label
    Protected WithEvents lblPageCount As Label

    Protected objPRTrx As New agri.PR.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intPRAR As Long

    Dim objWPDs As New Object()

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PRACCMONTH")
        strAccYear = Session("SS_PRACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intPRAR = Session("SS_PRAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPRAccessRights.PRTrxWorkPerformance), intPRAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
           If SortExpression.Text = "" Then
                SortExpression.Text = "WP.WPTrxId"
            End If
            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        lblCurrentIndex.Text = 0
        dgLine.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 

    Sub BindGrid() 
        Dim PageNo As Integer 
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim dsTemp As New DataSet()
        Dim lbl As Label


        Dim PageCount As Integer
        
        dsTemp = LoadData
        PageCount = objGlobal.mtdGetPageCount(Session("SS_PAGINGCOUNT"), dgLine.PageSize)
        If PageCount < 1 Then
            PageCount = 1
        End If
        If lblCurrentIndex.Text >= PageCount Then
            If PageCount = 0 Then
                lblCurrentIndex.Text = 0
            Else
                lblCurrentIndex.Text = PageCount - 1
                dsTemp = LoadData
            End If
        End If
        
        dgLine.DataSource = dsTemp
        dgLine.DataBind()
        lblPageCount.Text = PageCount
        BindPageList()
        PageNo = lblCurrentIndex.Text + 1
        lblTracker.Text="Page " & pageno & " of " & PageCount

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus") 
           	Select Case CInt(Trim(lbl.Text)) 
                Case objPRTrx.EnumWPTrxStatus.Active
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objPRTrx.EnumWPTrxStatus.Deleted, objPRTrx.EnumWPTrxStatus.Closed
                    lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next

    End Sub 

    Sub BindPageList() 
        Dim count As Integer = 1   
        Dim arrDList As New ArrayList()

        While not count = lblPageCount.Text + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = lblCurrentIndex.Text
    End Sub 

    Protected Function LoadData() As DataSet
        Dim strOpCd_GET As String = "PR_CLSTRX_WPTRX_SEARCH"
        Dim strSrchWPTrxId As String
        Dim strSrchDesc As String
        Dim strSrchGang As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer

        Session("SS_PAGING") = lblCurrentIndex.Text 

        strSrchWPTrxId = IIF(txtWPTrxId.Text = "", "", txtWPTrxId.Text)
        strSrchDesc = IIF(txtDescription.Text = "", "", txtDescription.Text)
        strSrchGang = IIF(txtGangCode.Text = "", "", txtGangCode.Text)
        strSrchStatus = IIF(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIF(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchWPTrxId & "|" & _
                   strSrchDesc & "|" & _
                   strSrchGang & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text & "|"
        Try
            intErrNo = objPRTrx.mtdGetWPTrx(strOpCd_GET, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objWPDs, _
                                            False)

        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If objWPDs.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objWPDs.Tables(0).Rows.Count - 1
                objWPDs.Tables(0).Rows(intCnt).Item("WPTrxId") = Trim(objWPDs.Tables(0).Rows(intCnt).Item("WPTrxId"))
                objWPDs.Tables(0).Rows(intCnt).Item("Description") = Trim(objWPDs.Tables(0).Rows(intCnt).Item("Description"))
                objWPDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objWPDs.Tables(0).Rows(intCnt).Item("Status"))
                objWPDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objWPDs.Tables(0).Rows(intCnt).Item("UpdateID"))
                objWPDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objWPDs.Tables(0).Rows(intCnt).Item("UserName"))
            Next
        End If

        Return objWPDs
    End Function


    Sub btnPrevNext_Click (sender As Object, e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).commandargument
        Select Case direction
            Case "first"
                lblCurrentIndex.Text = 0
            Case "prev"
                lblCurrentIndex.Text = _
                    Math.Max(0, lblCurrentIndex.Text - 1)
            Case "next"
                lblCurrentIndex.Text = _
                    Math.Min(lblPageCount.Text - 1, lblCurrentIndex.Text + 1)
            Case "last"
                lblCurrentIndex.Text = lblPageCount.Text - 1
        End Select
        lstDropList.SelectedIndex = lblCurrentIndex.Text
        BindGrid()
    End Sub

    Sub PagingIndexChanged(sender As Object, e As EventArgs)
        If Page.IsPostBack Then
            lblCurrentIndex.Text = lstDropList.SelectedIndex 
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(sender As Object, e As DataGridPageChangedEventArgs)
        lblCurrentIndex.Text = e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIF(SortCol.Text = "ASC", "DESC", "ASC")
        lblCurrentIndex.Text = lstDropList.SelectedIndex
        BindGrid() 
    End Sub

    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim objWPTrxLnDs As New Dataset()
        Dim strOpCd As String = "PR_CLSTRX_WPTRX_STATUS_UPD"
        Dim strOpCode_UpdLine As String = "PR_CLSTRX_WPTRX_LINE_UPD"
        Dim strOpCode_GetLine As String = "PR_CLSTRX_WPTRX_LINE_GET"
        Dim strParam As String = ""
        Dim strSelectedWPTrxId As String 
        Dim intErrNo As Integer
        Dim objWPTrxId As String
        Dim CPCell As TableCell = e.Item.Cells(0)
        Dim strSubActCode As String 
        Dim strBlock As String 

        strSelectedWPTrxId = CPCell.Text
        strParam = strSelectedWPTrxId & "|" & objPRTrx.EnumWPTrxStatus.Deleted
        
        Try
            intErrNo = objPRTrx.mtdUpdWPTrx(strOpCd, _
                                           strCompany, _
                                           strLocation, _
                                           strUserId, _
                                           strAccMonth, _
                                           strAccYear, _
                                           strParam, _
                                           True, _
                                           objWPTrxId)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_SETUP_WPTRX_CANCEL&errmesg=" & lblErrMessage.Text & "&redirect=pr/trx/PR_trx_WPList.aspx")
        End Try
      
        strParam = strSelectedWPTrxId
        Try
            intErrNo = objPRTrx.mtdGetWPTrx(strOpCode_GetLine, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objWPTrxLnDs, _
                                            True)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRXLINE_GET&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        If objWPTrxLnDs.Tables(0).Rows.Count > 0 
            strSubActCode = objWPTrxLnDs.Tables(0).Rows(0).Item("SUBACTCODE")
            strBlock = objWPTrxLnDs.Tables(0).Rows(0).Item("BLKCODE")
    
            strParam = strSelectedWPTrxId & "|" & _
                                strSubActCode & "|" & _
                                strBlock & "|" & _
                                Convert.ToString(objPRTrx.EnumWPTrxLnStatus.Active)              
            Try
                intErrNo = objPRTrx.mtdUpdWPTrxLine(strOpCd, _
                                                        strOpCode_UpdLine, _
                                                        strCompany, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strParam, _
                                                        True, _
                                                        objWPTrxLnDs)
            Catch Exp As Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_TRX_WPTRX_LINE_UPD&errmesg=" & Exp.ToString() & "&redirect=pr/trx/pr_clstrx_WPdet.aspx?WPTrxID=" & strSelectedWPTrxId)
            End Try
        End If
      

        dgLine.EditItemIndex = -1
        BindGrid() 
    End Sub

    Sub NewWPBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("PR_trx_WPDet.aspx")
    End Sub



End Class
