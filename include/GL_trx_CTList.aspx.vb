

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


Public Class gl_trx_CTList : Inherits Page

    Protected WithEvents dgCreditNote As DataGrid
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents ddlStatus As DropDownList
    Protected WithEvents txtWOID As TextBox
    Protected WithEvents txtSupplier As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMesage As Label

    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objAPTrx As New agri.AP.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAdminLoc As New agri.Admin.clsLoc()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    
    Dim objLangCapDs As New Object()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intAPAR As Integer
    Dim objCreditNoteDs As New Object()
    Dim strLocType as String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_APACCMONTH")
        strAccYear = Session("SS_APACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intAPAR = Session("SS_APAR")
        strLocType = Session("SS_LOCTYPE")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APCreditNote), intAPAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            onload_GetLangCap()
            If SortExpression.Text = "" Then
                SortExpression.Text = "CN.WorkOrderID"
            End If

            If Not Page.IsPostBack Then
                BindStatusList()
                BindGrid()
                BindPageList()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgCreditNote.CurrentPageIndex = 0
        dgCreditNote.EditItemIndex = -1
        BindGrid()
        BindPageList()
    End Sub


    Sub BindStatusList()
        ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditNoteStatus(objAPTrx.EnumCreditNoteStatus.All), objAPTrx.EnumCreditNoteStatus.All))
        ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditNoteStatus(objAPTrx.EnumCreditNoteStatus.Active), objAPTrx.EnumCreditNoteStatus.Active))
        'ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditNoteStatus(objAPTrx.EnumCreditNoteStatus.Closed), objAPTrx.EnumCreditNoteStatus.Closed))
        ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditNoteStatus(objAPTrx.EnumCreditNoteStatus.Confirmed), objAPTrx.EnumCreditNoteStatus.Confirmed))
        ddlStatus.Items.Add(New ListItem(objAPTrx.mtdGetCreditNoteStatus(objAPTrx.EnumCreditNoteStatus.Deleted), objAPTrx.EnumCreditNoteStatus.Deleted))

        ddlStatus.SelectedIndex = 1

    End Sub

    Sub BindGrid()
        Dim strOpCd As String = "GL_CLSTRX_WORKORDER_LIST_GET"
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim lbButton As LinkButton
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim lbl As Label

        strParamValue = IIf(Trim(txtWOID.Text) = "", "", " AND CN.WorkOrderID = '" & Replace(txtWOID.Text, "'", "''") & "'")
        strParamValue = strParamValue & IIf(Trim(txtSupplier.Text) = "", "", " AND SUPP.SupplierCode = '" & Replace(txtSupplier.Text, "'", "''") & "'")
        strParamValue = strParamValue & IIf(ddlStatus.SelectedItem.Value = "0", "", " AND CN.Status = " & ddlStatus.SelectedItem.Value)
        strParamValue = strParamValue & IIf(txtLastUpdate.Text = "", "", " AND CN.Status = '" & Replace(txtLastUpdate.Text, "'", "''") & "'")

        If Len(strParamValue) > 0 Then
            strParamValue = " AND CN.LOCCODE = '" & strLocation & "' " & strParamValue & " ORDER BY " & SortExpression.Text & " " & SortCol.Text
        End If

        strParamName = "STRSEARCH"
                
        Try

            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objCreditNoteDs)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CREDITNOTELIST_GET_LIST&errmesg=" & lblErrMesage.Text & "&redirect=AP/trx/ap_trx_CNList.aspx")
        End Try


        PageCount = objGlobal.mtdGetPageCount(objCreditNoteDs.Tables(0).Rows.Count, dgCreditNote.PageSize)
        dgCreditNote.DataSource = objCreditNoteDs
        If dgCreditNote.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgCreditNote.CurrentPageIndex = 0
            Else
                dgCreditNote.CurrentPageIndex = PageCount - 1
            End If
        End If
        dgCreditNote.DataBind()
        BindPageList()

        For intCnt = 0 To dgCreditNote.Items.Count - 1
            lbl = dgCreditNote.Items.Item(intCnt).FindControl("lblStatus")
            Select Case CInt(Trim(lbl.Text))
                Case objAPTrx.EnumCreditNoteStatus.Active
                    lbButton = dgCreditNote.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = True
                    lbButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
                Case objAPTrx.EnumCreditNoteStatus.Deleted
                    lbButton = dgCreditNote.Items.Item(intCnt).FindControl("lbDelete")
                    lbButton.Visible = False
            End Select
        Next

        PageNo = dgCreditNote.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgCreditNote.PageCount
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgCreditNote.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgCreditNote.CurrentPageIndex
    End Sub


    Sub Update_Status(ByVal pv_strCreditNoteId As String, _
                      ByVal pv_intCreditNoteSts As Integer)

        Dim strOpCode As String = "GL_CLSTRX_WORKORDER_STATUS_UPD"
        Dim strParamName As String = "[WORKORDERID]|[STATUS]"
        Dim strParamValue As String = pv_strCreditNoteId & "|" & pv_intCreditNoteSts
        Dim intErrNo As Integer

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCode, _
                                                      strParamName, _
                                                      strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=CREDITNOTELIST_UPD_STATUS&errmesg=" & lblErrMesage.Text & "&redirect=ap/trx/ap_trx_cnlist.aspx")
        End Try
    End Sub


    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgCreditNote.CurrentPageIndex = 0
            Case "prev"
                dgCreditNote.CurrentPageIndex = _
                Math.Max(0, dgCreditNote.CurrentPageIndex - 1)
            Case "next"
                dgCreditNote.CurrentPageIndex = _
                Math.Min(dgCreditNote.PageCount - 1, dgCreditNote.CurrentPageIndex + 1)
            Case "last"
                dgCreditNote.CurrentPageIndex = dgCreditNote.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgCreditNote.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgCreditNote.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgCreditNote.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgCreditNote.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strCreditNoteId As String

        dgCreditNote.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgCreditNote.Items.Item(CInt(E.Item.ItemIndex)).FindControl("idCNId")
        strCreditNoteId = lblDelText.Text

        Update_Status(strCreditNoteId, objAPTrx.EnumCreditNoteStatus.Deleted)

        BindGrid()
        BindPageList()
    End Sub


    Sub NewCNBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("gl_trx_CTDet.aspx")
    End Sub

    Sub onload_GetLangCap()
        GetEntireLangCap()
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
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=AP_TRX_DNLIST_GET_LANGCAP&errmesg=" & lblErrMesage.Text & "&redirect=ap/setup/AP_trx_CNList.aspx")
        End Try

    End Sub

    Function GetCaption(ByVal pv_TermCode) As String
            Dim count As Integer

            For count = 0 To objLangCapDs.Tables(0).Rows.Count - 1
                If Trim(pv_TermCode) = Trim(objLangCapDs.Tables(0).Rows(count).Item("TermCode")) Then
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
