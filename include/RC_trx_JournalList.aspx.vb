Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic.Interaction

Imports agri.RC.clsTrx
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap

Public Class RC_trx_JournalList : Inherits Page

    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtJournalID As TextBox
    Protected WithEvents txtJournalRefID As TextBox
    Protected WithEvents txtLocCode As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMesage As Label
    Protected WithEvents lblTo As Label
    Protected WithEvents lblLocation AS Label

    Protected objRCTrx As New agri.RC.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objRCDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intADAR As Integer

    Sub Page_Load(Sender As Object, E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intADAR = Session("SS_ADAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumADAccessRights.RCJournal), intADAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "JRN.JournalID"
            End If

            If Not Page.IsPostBack Then
                BindGrid() 
                BindPageList()
            End If
        End If
    End Sub


    Sub onload_GetLangCap()
        GetEntireLangCap()

        lblLocation.text = GetCaption(objLangCap.EnumLangCap.Location)
        dgLine.columns(3).HeaderText = lblTo.text & lblLocation.text

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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_TRX_DALIST_GET_LANGCAP&errmesg=" & lblErrMesage.Text & "&redirect=rc/trx/RC_trx_DAList.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
        Dim count As Integer
        
        For count=0 To objLangCapDs.Tables(0).Rows.Count - 1
            If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode"))
                Return Trim(objLangCapDs.Tables(0).Rows(count).Item("BusinessTerm"))
                Exit For
            End If
        Next
    End Function



    Sub srchBtn_Click(sender As Object, e As System.EventArgs) 
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid() 
        BindPageList()
    End Sub 


    Sub BindGrid() 
        Dim PageNo as Integer 
        Dim strOpCode_GetJrnList As String = "RC_CLSTRX_JOURNAL_LIST_GET"
        Dim lbButton As LinkButton
        Dim strSrchJrnID as string
        Dim strSrchJrnRefNo as string
        Dim strSrchLocCode as string
        Dim strSrchStatus as string
        Dim strSrchLastUpdate as string
        Dim strSearch as string
        Dim strParam as string
        Dim intErrNo As Integer
        Dim intCnt As Integer

        strSrchJrnID = IIf(txtJournalID.Text = "", "", txtJournalID.Text)
        strSrchJrnRefNo = IIf(txtJournalRefID.Text = "", "", txtJournalRefID.Text)
        strSrchLocCode = IIf(txtLocCode.Text = "", "", txtLocCode.Text)
        strSrchStatus = IIf(ddlStatus.SelectedItem.Value = "0", "", ddlStatus.SelectedItem.Value)
        strSrchLastUpdate = IIf(txtLastUpdate.Text = "", "", txtLastUpdate.Text)
        strParam = strSrchJrnID & "|" & _
                   strSrchJrnRefNo & "|" & _
                   strSrchLocCode & "|" & _
                   strSrchStatus & "|" & _
                   strSrchLastUpdate & "|" & _
                   SortExpression.Text & "|" & _
                   SortCol.Text

        Try
            intErrNo = objRCTrx.mtdGetJournal(strOpCode_GetJrnList, _
                                            strCompany, _
                                            strLocation, _
                                            strUserId, _
                                            strAccMonth, _
                                            strAccYear, _
                                            strParam, _
                                            objRCDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_JOURNALLIST_GET_LIST&errmesg=" & Exp.ToString() & "&redirect=")
        End Try

        For intCnt = 0 To objRCDs.Tables(0).Rows.Count - 1
            objRCDs.Tables(0).Rows(intCnt).Item("JournalID") = Trim(objRCDs.Tables(0).Rows(intCnt).Item("JournalID"))
            objRCDs.Tables(0).Rows(intCnt).Item("DocRefNo") = Trim(objRCDs.Tables(0).Rows(intCnt).Item("DocRefNo"))
            objRCDs.Tables(0).Rows(intCnt).Item("ToLocCode") = Trim(objRCDs.Tables(0).Rows(intCnt).Item("ToLocCode"))
            objRCDs.Tables(0).Rows(intCnt).Item("Status") = Trim(objRCDs.Tables(0).Rows(intCnt).Item("Status"))
            objRCDs.Tables(0).Rows(intCnt).Item("UpdateID") = Trim(objRCDs.Tables(0).Rows(intCnt).Item("UpdateID"))
        Next                

        dgLine.DataSource = objRCDs
        dgLine.DataBind()
        
        For intCnt = 0 To dgLine.Items.Count - 1
            Select Case CInt(objRCDs.Tables(0).Rows(intCnt).Item("Status"))
                Case objRCTrx.EnumJournalStatus.Active
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = True
                        lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objRCTrx.EnumJournalStatus.Confirmed, objRCTrx.EnumJournalStatus.Cancelled
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
                Case objRCTrx.EnumJournalStatus.Deleted
                        lbButton = dgLine.Items.Item(intCnt).FindControl("lbDelete")
                        lbButton.Visible = False
            End Select
        Next

        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text="Page " & pageno & " of " & dgLine.PageCount
    End Sub 

    Sub BindPageList() 
        Dim count as integer = 1   
        Dim arrDList As New ArrayList()

        While not count = dgLine.PageCount + 1
            arrDList.Add("Page " & count)
            Count = Count +1
        End While 
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgLine.CurrentPageIndex
    End Sub 


    Sub Update_Status(ByVal pv_strDAID As String, _
                      ByVal pv_intDASts As Integer)

        Dim strOpCode_Jrn As String = "RC_CLSTRX_JOURNAL_STATUS_UPD"
        Dim strParam As String = pv_strDAID & "|" & pv_intDASts
        Dim intCnt As Integer
        Dim intErrNo As Integer

        Try
            intErrNo = objRCTrx.mtdUpdJournalStatus(strOpCode_Jrn, _
                                                    strCompany, _
                                                    strLocation, _
                                                    strUserId, _
                                                    strAccMonth, _
                                                    strAccYear, _
                                                    strParam)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=RC_JOURNALLIST_UPD_STATUS&errmesg=" & Exp.ToString() & "&redirect=RC/trx/RC_trx_JournalList.aspx")
        End Try
    End Sub


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
        dgLine.CurrentPageIndex=e.NewPageIndex
        BindGrid() 
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid() 
    End Sub
 
    Sub DEDR_Delete(Sender As Object, E As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strJournalID As String

        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("idJrnId")
        strJournalID = lblDelText.Text

        Update_Status(strJournalID, objRCTrx.EnumJournalStatus.Deleted)

        BindGrid() 
        BindPageList()
    End Sub

    Sub NewJrnBtn_Click(Sender As Object, E As ImageClickEventArgs)
        Response.Redirect("RC_trx_JournalDet.aspx")
    End Sub
End Class
