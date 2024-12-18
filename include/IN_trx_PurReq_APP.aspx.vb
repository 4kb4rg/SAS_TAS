

Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic


Public Class IN_PurReq_APP : Inherits Page

    Protected WithEvents dgPRListing As DataGrid
    Protected WithEvents dgPrSent As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchPRTypeList As DropDownList
    Protected WithEvents srchPRLevelList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchPRID As TextBox
    Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents TotalAmount As Label
    Protected WithEvents Stock As ImageButton
    Protected WithEvents DC As ImageButton
    Protected WithEvents WS As ImageButton
    Protected WithEvents FA As ImageButton
    Protected WithEvents NU As ImageButton
    Protected WithEvents ibPrint As ImageButton
    Protected WithEvents srchStatusLnList As DropDownList
    Protected WithEvents srchApprovedBy As DropDownList
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents lblSearch As Label
    Protected WithEvents ddlLocation As DropDownList
    Protected WithEvents SrchddlType As DropDownList
    Protected WithEvents srchItem As TextBox


    Protected objIN As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Dim objPU As New agri.PU.clsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strOppCd_GET_PRList As String = "IN_CLSTRX_PURREQ_LIST_GET"
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intINAR As Integer

    Dim objDataSet As New DataSet()
    Dim intCnt As Integer
    Dim intErrNo As Integer
    Dim intPRCount As Integer
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLocLevel As String
    Dim intLevel As Integer
    Dim strSelAccMonth As String
    Dim strSelAccYear As String


    Dim BtnApproved As Button
    Dim BtnRetrieved As Button

    Dim BtnCancel As Button
    Dim APPButton As LinkButton
    Dim UpdButton As LinkButton
    Dim CancelButton As LinkButton
    Dim SaveButton As LinkButton

#Region "COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intINAR = Session("SS_INAR")
        strAccMonth = Session("SS_INACCMONTH")
        strAccYear = Session("SS_INACCYEAR")
        strLocLevel = Session("SS_LOCLEVEL")
        intLevel = Session("SS_USRLEVEL")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        Stock.CommandName = objIN.EnumPurReqDocType.StockPR
        DC.CommandName = objIN.EnumPurReqDocType.DirectChargePR
        WS.CommandName = objIN.EnumPurReqDocType.WorkshopPR
        FA.CommandName = objIN.EnumPurReqDocType.FixedAssetPR

        NU.CommandName = objIN.EnumPurReqDocType.NurseryPR

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumINAccessRights.INPurchaseRequest), intINAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            If SortExpression.Text = "" Then
                SortExpression.Text = "PR.PRID"
                sortcol.Text = "ASC"
            End If

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            Stock.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Stock).ToString())
            DC.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(DC).ToString())
            WS.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(WS).ToString())
            FA.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(FA).ToString())
            NU.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(NU).ToString())
            'ibPrint.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(ibPrint).ToString())

            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    If lblSearch.Text = "" Then
                        lstAccMonth.SelectedValue = 0
                    Else
                        lstAccMonth.SelectedValue = strAccMonth
                    End If

                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = 0
                    BindAccYear(strSelAccYear)
                End If

                lblSearch.Text = ""
                srchApprovedBy.SelectedIndex = IIf(intLevel = 0, 1, intLevel)
                BindSearchList()
                BindPRTypeList()
                BindPRLevelList()

                If strLocLevel = "2" Or strLocLevel = "3" Then
                    BindLocation("")
                Else
                    BindLocation(strLocation)
                End If

                If lblSearch.Text = "" Then
                    BindGrid("")
                Else
                    BindSearch()
                End If
                BindPageList()

                'CheckStatus()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPrSent.CurrentPageIndex = 0
        dgPrSent.EditItemIndex = -1


        If SrchddlType.SelectedIndex = 0 Then
            BindGrid("SEARCH")
            BindSearch()
        ElseIf SrchddlType.SelectedIndex = 1 Then
            BindGrid("SEARCH")
        ElseIf SrchddlType.SelectedIndex = 2 Then
            BindSearch()
        End If

        CheckStatus()
        'BindPageList()

        lblSearch.Text = "CARI"
    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgPRListing.CurrentPageIndex = 0
            Case "prev"
                dgPRListing.CurrentPageIndex = _
                    Math.Max(0, dgPRListing.CurrentPageIndex - 1)
            Case "next"
                dgPRListing.CurrentPageIndex = _
                    Math.Min(dgPRListing.PageCount - 1, dgPRListing.CurrentPageIndex + 1)
            Case "last"
                dgPRListing.CurrentPageIndex = dgPRListing.PageCount - 1
        End Select
        lstDropList.SelectedIndex = dgPRListing.CurrentPageIndex

        If lblSearch.Text = "" Then
            BindGrid("")
        Else
            BindSearch()
        End If

        CheckStatus()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex
            If lblSearch.Text = "" Then
                BindGrid("")
            Else
                BindSearch()
            End If
            CheckStatus()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        Try
            dgPRListing.CurrentPageIndex = e.NewPageIndex

            If lblSearch.Text = "" Then
                BindGrid("")
            Else
                BindSearch()
            End If

            CheckStatus()
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex
        If lblSearch.Text = "" Then
            BindGrid("")
        Else
            BindSearch()
        End If
        CheckStatus()
    End Sub

    Sub CheckStatus()
        'Dim strOppCd_GET_PRLnList As String = "IN_CLSTRX_PURREQLN_LIST_GET"

        'Dim strParamTemp2 As String
        'Dim strParamTemp3 As String
        'Dim strPRStatus As String
        'Dim objPRDs As DataSet
        'Dim objPRLnDs As DataSet
        'Dim DelButton As LinkButton
        'Dim lblPRID As Label
        'Dim strPRID As String
        'Dim strPRIDTemp As String
        'Dim strQtyRcv As String

        'For intCnt = 0 To dgPRListing.Items.Count - 1
        '    lblPRID = dgPRListing.Items.Item(intCnt).FindControl("PRID")
        '    strPRID = lblPRID.Text
        '    strParamTemp2 = "And PR.PRID = '" & Trim(strPRID) & "'|" & " "
        '    Try
        '        intErrNo = objIN.mtdGetPurchaseRequest(strOppCd_GET_PRList, _
        '                                               strParamTemp2, _
        '                                               objIN.EnumPurReqDocType.StockPR, _
        '                                               strAccMonth, _
        '                                               strAccYear, _
        '                                               Trim(strLocation), _
        '                                               objPRDs)
        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQ_LIST_GET_CHECK_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        '    End Try

        '    If objPRDs.Tables(0).Rows.Count > 0 Then
        '        strPRIDTemp = Trim(objPRDs.Tables(0).Rows(0).Item("PRID"))
        '        strPRStatus = Trim(objPRDs.Tables(0).Rows(0).Item("Status"))
        '    End If

        '    strParamTemp3 = strPRIDTemp & "|" & "PRLn.PRID"
        '    Try
        '        intErrNo = objIN.mtdGetPRLnList(strOppCd_GET_PRLnList, _
        '                                        strCompany, _
        '                                        Trim(strLocation), _
        '                                        strUserId, _
        '                                        strAccMonth, _
        '                                        strAccYear, _
        '                                        strParamTemp3, _
        '                                        objPRLnDs)
        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PURREQLN_LIST_GET_CHECK_STATUS&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        '    End Try

        '    If objPRLnDs.Tables(0).Rows.Count > 0 Then
        '        strQtyRcv = objPRLnDs.Tables(0).Rows(0).Item("QtyRcv")
        '        If strPRStatus = objIN.EnumPurReqStatus.Active And strQtyRcv <> 0 Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Active And strQtyRcv = "" Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Confirmed Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Deleted Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '            If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
        '                DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
        '                DelButton.Visible = True
        '            Else
        '                DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
        '                DelButton.Visible = False
        '            End If
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Cancelled Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        Else
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = True
        '            DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '        End If
        '    Else
        '        If strPRStatus = objIN.EnumPurReqStatus.Active Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = True
        '            DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Confirmed Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Deleted Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '            If lstAccMonth.SelectedItem.Value >= Session("SS_INACCMONTH") Then
        '                DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
        '                DelButton.Visible = True
        '            Else
        '                DelButton = dgPRListing.Items.Item(intCnt).FindControl("Undelete")
        '                DelButton.Visible = False
        '            End If
        '        ElseIf strPRStatus = objIN.EnumPurReqStatus.Cancelled Then
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = False
        '        Else
        '            DelButton = dgPRListing.Items.Item(intCnt).FindControl("Delete")
        '            DelButton.Visible = True
        '            DelButton.Attributes("onclick") = "javascript:return ConfirmAction('delete');"
        '        End If
        '    End If
        'Next intCnt
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_PurReqLn_UPD As String = "IN_CLSTRX_PURREQLN_LIST_UPD"
        Dim objPRID As Object
        Dim CancelText As Label
        Dim Updbutton As LinkButton

        'Dim strRemarks As String = Request.Form("txtRemarks").Trim
        'Dim strPRID As String = lblPurReqID.Text.Trim
        Dim strParam As String
        Dim strItemCode As String
        Dim strQtyReq As String
        Dim strQtyRcv As String
        Dim strQtyOutstanding As String
        Dim strCost As String
        Dim strStatus As String

        'dgPRListing.EditItemIndex = CInt(E.Item.ItemIndex)
        'CancelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ItemCode")
        'strItemCode = CancelText.Text
        'CancelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("hidStatus")
        'strStatus = CancelText.Text
        'CancelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyApp")
        'strQtyReq = CancelText.Text
        'CancelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyRcv")
        'strQtyRcv = CancelText.Text
        'CancelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyOutstanding")
        'strQtyOutstanding = CancelText.Text
        'CancelText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblUnitCost")
        'strCost = CancelText.Text

        ''dgPRListing = Nothing
        dgPRListing.DataSource = Nothing
        dgPRListing.DataBind()
        If lblSearch.Text = "" Then
            BindGrid("")
        Else
            BindSearch()
        End If
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_PurReqLn_DEL As String = "IN_CLSTRX_PURREQLN_DEL"
        'Dim strPRID As String = lblPurReqID.Text.Trim
        Dim strStatusLn As String
        Dim EditStatusText As Label
        Dim EditStatus As DropDownList
        Dim EditStatusDescln As Label
        Dim strItemCode As String
        Dim ItemText As Label
        Dim QtyDispText As Label
        Dim EditQtyText As Label
        Dim EditQty As TextBox


        Dim EditAddNoteText As Label
        Dim EditAddNote As TextBox
        Dim indDate As String = ""


        'Dim intInputPeriod As Integer = Year(strDate) * 100 + Month(strDate)
        'Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        'Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)


        dgPRListing.EditItemIndex = CInt(E.Item.ItemIndex)
        'ItemText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("ItemCode")
        'strItemCode = ItemText.Text

        QtyDispText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyAppDisplay")
        QtyDispText.Visible = False
        EditQtyText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblQtyApp")
        EditQty = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstQtyApp")
        EditQty.Text = EditQtyText.Text
        EditQty.Visible = True

        EditStatusDescln = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblStatusDescln")
        EditStatusDescln.Visible = False
        EditStatusText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblStatusln")
        strStatusLn = EditStatusText.Text
        EditStatus = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstStatusLn")
        EditStatus.Visible = True

        'EditAddNoteText = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAddNote")
        'EditAddNoteText.Visible = False
        'EditAddNote = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lstAddNote")
        'EditAddNote.Text = EditAddNoteText.Text
        'EditAddNote.Visible = True

        EditStatus.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Active), objIN.EnumPurReqLnStatus.Active))
        EditStatus.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel), objIN.EnumPurReqLnStatus.Cancel))
        '' EditStatus.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Approved), objIN.EnumPurReqLnStatus.Approved))
        EditStatus.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Hold), objIN.EnumPurReqLnStatus.Hold))

        EditStatus.SelectedIndex = EditStatus.Items.IndexOf(EditStatus.Items.FindByValue(Trim(strStatusLn)))

        UpdButton = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Edit")
        UpdButton.Visible = False
        SaveButton = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        SaveButton.Visible = True
        CancelButton = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Cancel")
        CancelButton.Visible = True

        APPButton = dgPRListing.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Approved")
        APPButton.Visible = False


    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

        Dim strOpCd As String = "IN_CLSTRX_PURREQLN_UPD_APPROVED"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strItemCode As String
        Dim strQtyApp As String
        Dim strStatusLn As String
        Dim strAddNote As String
        Dim EditItem As Label
        Dim EditText As TextBox
        Dim EditStatus As DropDownList

        Dim indDate As String = ""
        Dim sTrPRID As String = ""
        Dim sTrPrLnID As String = ""
        Dim bUpdate As Boolean = False

        Dim intLevel_Edit As Integer

        EditText = E.Item.FindControl("lstQtyApp")
        strQtyApp = EditText.Text
        'EditText = E.Item.FindControl("lstAddNote")
        'strAddNote = EditText.Text
        EditStatus = E.Item.FindControl("lstStatusLn")
        strStatusLn = EditStatus.SelectedItem.Value


        EditItem = E.Item.FindControl("LnID") 'dgPRListing.Items.Item(E.Item.ItemIndex).Cells(0).Text
        sTrPrLnID = trim(EditItem.text)
        EditItem = E.Item.FindControl("lblPRID") 'dgPRListing.Items.Item(E.Item.ItemIndex).Cells(1).Text
        sTrPRID = trim(EditItem.text)
        EditItem = E.Item.FindControl("ItemCode") 'dgPRListing.Items.Item(E.Item.ItemIndex).Cells(2).Text
        strItemCode = trim(EditItem.text)

        If strStatusLn = 1 Then '' jika active
            intLevel_Edit = 0 ''intLevel - 1
        ElseIf strStatusLn = 2 Then '' jika cancel
            intLevel_Edit = 0
        ElseIf strStatusLn = 4 Then '' jika hold
            intLevel_Edit = intLevel - 1
        End If

        strParamName = "PRID|ITEMCODE|QTYAPP|QTYOUTSTANDING|STATUSLN|ADDITIONALNOTE|APPROVEDBY|PRLNID|USRID"
        strParamValue = sTrPRID & "|" & strItemCode & "|" & strQtyApp & "|" & strQtyApp & "|" & _
                        strStatusLn & "|" & strAddNote & "|" & intLevel_Edit & "|" & sTrPrLnID & "|" & strUserId



        bUpdate = False
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
            bUpdate = True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If bUpdate = True Then
            UserMsgBox(Me, "Update Sucsess...!!!")
        End If

        If lblSearch.Text = "" Then
            BindGrid("")
        Else
            BindSearch()
        End If
    End Sub

    Sub btnNewStPR_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim Issuetype As String = CType(sender, ImageButton).CommandName
        Response.Redirect("IN_PurReq_Details.aspx?prqtype=" & Issuetype)
    End Sub

    Sub BtnApproved_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strOpCd As String = "IN_CLSTRX_PURREQLN_ADD_APPROVED"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim StrPRID As String = ""
        Dim StrPRLNID As String = ""
        Dim strItemCode As String = ""
        Dim IntAppLevel As Integer
        Dim strConfirmed As String = ""
        Dim nQtyAPP As Double = 0
        Dim nUnitCost As Double

        Dim indDate As String = ""

        Dim bUpdate As Boolean = False

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        StrPRLNID = dgItem.Cells(0).Text
        StrPRID = dgItem.Cells(1).Text  ''CType(dgItem.Cells(0).FindControl("lblNodoc"), Label).Text
        strItemCode = dgItem.Cells(2).Text

        nQtyAPP = lCDbl(CType(dgItem.Cells(9).FindControl("lstQtyApp"), TextBox).Text)
        nUnitCost = lCDbl(CType(dgItem.Cells(10).FindControl("lblAmount"), Label).Text)
        IntAppLevel = CType(dgItem.Cells(13).FindControl("lblAppLevel"), Label).Text

        strParamName = "BOR|INTLEVEL|ITEM|PRLNID|USRID|APPSTATUS|QTYAPP|COST|LOC"
        strParamValue = StrPRID & "|" & intLevel & "|" & strItemCode & "|" & StrPRLNID & "|" & strUserId & "|" & 3 & "|" & nQtyAPP & "|" & nUnitCost & "|" & strLocation


        bUpdate = False
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
            bUpdate = True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try
         
        BindGrid("")
         

        If bUpdate = True Then
            UserMsgBox(Me, "Approved Sucsess...!!!" & CType(dgItem.Cells(10).FindControl("lblUnitCost"), Label).Text)
        End If

    End Sub


    Sub BtnRetrieved_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strOpCd As String = "IN_CLSTRX_PURREQLN_ADD_RETRIEVED"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim StrPRID As String = ""
        Dim StrPRLNID As String = ""
        Dim strItemCode As String = ""
        Dim IntAppLevel As Integer
        Dim strConfirmed As String = ""
        Dim nQtyAPP As Double = 0
        Dim nUnitCost As Double

        Dim indDate As String = ""

        Dim bUpdate As Boolean = False

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        StrPRLNID = dgItem.Cells(0).Text
        StrPRID = dgItem.Cells(1).Text  ''CType(dgItem.Cells(0).FindControl("lblNodoc"), Label).Text
        strItemCode = dgItem.Cells(2).Text
        IntAppLevel = CType(dgItem.Cells(11).FindControl("lblAppLevel"), Label).Text

        strParamName = "BOR|ITEM|PRLNID|USRID"
        strParamValue = StrPRID & "|" & strItemCode & "|" & StrPRLNID & "|" & strUserId
         
        bUpdate = False

        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
            bUpdate = True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        BindSearch()
        BindGrid("")

        If bUpdate = True Then
            UserMsgBox(Me, "Retrieved Sucsess...!!!")
        End If

    End Sub

    Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strOpCd As String = "IN_CLSTRX_PURREQLN_CANCEL_APPROVED"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim StrPRID As String = ""
        Dim StrPRLNID As String = ""
        Dim strItemCode As String = ""
        Dim IntAppLevel As Integer
        Dim strConfirmed As String = ""

        Dim indDate As String = ""

        Dim bUpdate As Boolean = False

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        StrPRLNID = dgItem.Cells(0).Text
        StrPRID = dgItem.Cells(1).Text  ''CType(dgItem.Cells(0).FindControl("lblNodoc"), Label).Text
        strItemCode = dgItem.Cells(2).Text
        IntAppLevel = CType(dgItem.Cells(11).FindControl("lblAppLevel"), Label).Text

        strParamName = "BOR|STATUS|ITEM|PRLNID|USRID"
        strParamValue = StrPRID & "|" & 1 & "|" & strItemCode & "|" & StrPRLNID & "|" & strUserId


        bUpdate = False
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
            bUpdate = True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        If bUpdate = True Then
            UserMsgBox(Me, "Cancel Approved Sucsess...!!!")
        End If


        If lblSearch.Text = "" Then
            BindGrid("")
        Else
            BindSearch()
        End If

    End Sub

    Sub Btnrefresh_onClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        BindGrid("")
        lblSearch.Text = ""
    End Sub
#End Region

#Region "PROCEDURE"

    Private Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        lstAccYear.DataSource = objAccYearDs.Tables(0)
        lstAccYear.DataValueField = "AccYear"
        lstAccYear.DataTextField = "UserName"
        lstAccYear.DataBind()
        lstAccYear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Sub PageConTrol()
        For intCnt = 0 To dgPRListing.Items.Count - 1
            ''-- SET DEFAULT
            BtnApproved = dgPRListing.Items.Item(intCnt).FindControl("BtnApproved")
            BtnCancel = dgPRListing.Items.Item(intCnt).FindControl("BtnCancel")
            BtnApproved.Attributes("onclick") = "javascript:return ConfirmAction('approved');"
            BtnCancel.Attributes("onclick") = "javascript:return ConfirmAction('cancel');"
            CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).Text = "Approved"
            CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).Enabled = True

            CType(dgPRListing.Items.Item(intCnt).FindControl("lblUnitCost"), Label).Text = FormatNumber(lCDbl(CType(dgPRListing.Items.Item(intCnt).FindControl("lblUnitCost"), Label).Text), 2)
            CType(dgPRListing.Items.Item(intCnt).FindControl("lblAmount"), Label).Text = FormatNumber(lCDbl(CType(dgPRListing.Items.Item(intCnt).FindControl("lblAmount"), Label).Text), 2)
            ''-- END SET DEFAULT
             
            '''==control Linked edit jika Level Approved Lebih tinggi dari level yang login & by passed approval
            If (intLevel <= CType(dgPRListing.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text) _
                    Or CType(dgPRListing.Items(intCnt).FindControl("lblStatusln"), Label).Text = 3 Then
                CType(dgPRListing.Items(intCnt).FindControl("Edit"), LinkButton).Enabled = False
            Else
                CType(dgPRListing.Items(intCnt).FindControl("Edit"), LinkButton).Enabled = True
                CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).Enabled = True
            End If
             
            '' control warna dan tombol cancel hanya di level 6 dan jika sudah approved level 6 akan tampil

            If CType(dgPRListing.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text = 0 Then
                CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).BackColor = Drawing.Color.Yellow   'menunggu app ktu
            ElseIf CType(dgPRListing.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text = 2 And CType(dgPRListing.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text <= 6 Then
                CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).BackColor = Drawing.Color.Blue   'menunggu app Manager 
            ElseIf CType(dgPRListing.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text = 3 And CType(dgPRListing.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text <= 6 Then
                CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).BackColor = Drawing.Color.Red   'menunggu app Direktur
            ElseIf CType(dgPRListing.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text >= 6 Then  ''done
                CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).Text = "Completed"
                CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).BorderStyle = BorderStyle.None
                CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).BackColor = Drawing.Color.Transparent
            End If


            If CType(dgPRListing.Items(intCnt).FindControl("lblStatusln"), Label).Text = 2 Then
                dgPRListing.Items(intCnt).BackColor = Drawing.Color.SlateGray
                dgPRListing.Items(intCnt).Font.Italic = True
            End If

            ''' Khusus Dir Ops Keatas
            If intLevel < 6 Then
                If CType(dgPRListing.Items(intCnt).FindControl("lblOrderLoc"), Label).Text.Trim <> strLocation Then
                    CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).Visible = False
                Else
                    CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).Visible = True
                End If
            End If

            If CType(dgPRListing.Items(intCnt).FindControl("lblPOIDLast"), Label).Text <> "" Then
                CType(dgPRListing.Items(intCnt).FindControl("BtnApproved"), Button).Visible = False
            End If

        Next
    End Sub

    Sub PageConTrol_History()
        For intCnt = 0 To dgPrSent.Items.Count - 1
            ''-- SET DEFAULT
            BtnRetrieved = dgPrSent.Items.Item(intCnt).FindControl("BtnRetrieved")
            BtnCancel = dgPrSent.Items.Item(intCnt).FindControl("BtnCancel")
            BtnRetrieved.Attributes("onclick") = "javascript:return ConfirmAction('Retrieved');"


            CType(dgPrSent.Items(intCnt).FindControl("BtnRetrieved"), Button).Text = "Retrieved"
            CType(dgPrSent.Items(intCnt).FindControl("BtnRetrieved"), Button).Enabled = True
            CType(dgPrSent.Items(intCnt).FindControl("BtnRetrieved"), Button).Visible = False

            If CType(dgPrSent.Items(intCnt).FindControl("lblPOIDLast"), Label).Text.Trim = "" Then
                If (intLevel > lCDbl(CType(dgPrSent.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text)) And CType(dgPrSent.Items(intCnt).FindControl("lblStatusln"), Label).Text.Trim = "1" Then
                    CType(dgPrSent.Items(intCnt).FindControl("BtnRetrieved"), Button).Visible = True
                End If

                If intLevel = lCDbl(CType(dgPrSent.Items(intCnt).FindControl("lblAppBy_Level"), Label).Text) Then
                    CType(dgPrSent.Items(intCnt).FindControl("BtnRetrieved"), Button).Visible = True
                End If
            End If
        Next
    End Sub

    Sub BindPageList()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgPRListing.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgPRListing.CurrentPageIndex

    End Sub

    Sub BindSearchList()
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.All), objIN.EnumPurReqStatus.All))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Active), objIN.EnumPurReqStatus.Active))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Cancelled), objIN.EnumPurReqStatus.Cancelled))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Confirmed), objIN.EnumPurReqStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objIN.mtdGetPurReqStatus(objIN.EnumPurReqStatus.Deleted), objIN.EnumPurReqStatus.Deleted))
        srchStatusList.Items.Add(New ListItem("Closed", objIN.EnumPurReqStatus.Fulfilled))

        If intLevel = 0 Then
            srchStatusList.SelectedIndex = 1
        Else
            srchStatusList.SelectedIndex = 6
        End If

        srchStatusLnList.Items.Add(New ListItem("All", "1,2,3,4"))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Active), objIN.EnumPurReqLnStatus.Active))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Cancel), objIN.EnumPurReqLnStatus.Cancel))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Approved), objIN.EnumPurReqLnStatus.Approved))
        srchStatusLnList.Items.Add(New ListItem(objIN.mtdGetPurReqLnStatus(objIN.EnumPurReqLnStatus.Hold), objIN.EnumPurReqLnStatus.Hold))
        srchStatusLnList.SelectedIndex = 0

    End Sub

    Sub BindPRTypeList()
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.All), objIN.EnumPurReqDocType.All))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.DirectChargePR), objIN.EnumPurReqDocType.DirectChargePR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.StockPR), objIN.EnumPurReqDocType.StockPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.WorkshopPR), objIN.EnumPurReqDocType.WorkshopPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.FixedAssetPR), objIN.EnumPurReqDocType.FixedAssetPR))
        srchPRTypeList.Items.Add(New ListItem(objIN.mtdGetPRType(objIN.EnumPurReqDocType.NurseryPR), objIN.EnumPurReqDocType.NurseryPR))

    End Sub

    Sub BindPRLevelList()
        srchPRLevelList.Items.Clear()
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.All), objAdminLoc.EnumLocLevel.All))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Estate), objAdminLoc.EnumLocLevel.Estate))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Perwakilan), objAdminLoc.EnumLocLevel.Perwakilan))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.HQ), objAdminLoc.EnumLocLevel.HQ))
        srchPRLevelList.Items.Add(New ListItem(objAdminLoc.mtdGetLocLevel(objAdminLoc.EnumLocLevel.Mill), objAdminLoc.EnumLocLevel.Mill))
    End Sub

    Sub BindGrid(ByVal pKriteria As String)

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData(pKriteria)
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPRListing.PageSize)

        dgPRListing.DataSource = dsData
        If dgPRListing.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPRListing.CurrentPageIndex = 0
            Else
                dgPRListing.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPRListing.DataBind()
        BindPageList()
        PageNo = dgPRListing.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgPRListing.PageCount
        PageConTrol()
    End Sub

    Sub BindSearch()
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadSrch()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPRListing.PageSize)

        dgPrSent.DataSource = dsData
        If dgPrSent.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPrSent.CurrentPageIndex = 0
            Else
                dgPrSent.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPrSent.DataBind()
        'BindPageList()
        PageNo = dgPrSent.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgPrSent.PageCount
        PageConTrol_History()

    End Sub

    Protected Function LoadData(pKriteria As String) As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "IN_CLSTRX_PURREQLN_LIST_APPROVED"

        Dim strSearchLocLevel As String = ""
        Dim strLastAccMonth As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
        Dim StatusLn As String = ""
        Dim sSQLKriteria As String

        strAccYear = Year(Now())
        lstAccMonth.SelectedValue = 0

        sSQLKriteria = ""
        If pKriteria = "SEARCH" Then
            sSQLKriteria = "Where NextApprovedBy_Level = " & intLevel & ""

            If srchPRID.Text.Trim <> "" Then
                sSQLKriteria = sSQLKriteria & " And PRID LIKE '%" & srchPRID.Text.Trim & "%'"
            ElseIf srchItem.Text.Trim <> "" Then
                sSQLKriteria = sSQLKriteria & " And ((ItemCode like '%" & srchItem.Text & "%') OR (ItemDesc like '%" & srchItem.Text & "%'))"
            End If
        Else

            'If intLevel = 7 Then
            '    sSQLKriteria = "Where NextApprovedBy_Level <= " & intLevel & " And ApprovedBy_Level >=2"
            'Else
            sSQLKriteria = "Where NextApprovedBy_Level = " & intLevel & ""
            'End If

        End If

        StatusLn = "1,4"
        strParamName = "LOCCODE|STATUS|PRLEVEL|FROMLOCCODE|USERID|STRSEARCH"
        strParamValue = strLocation & "|" & StatusLn & "|" & intLevel & "|" & ddlLocation.SelectedItem.Value & "|" & strUserId & "|" & sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        intPRCount = objItemDs.Tables(0).Rows.Count

        Return objItemDs
    End Function

    Protected Function LoadSrch() As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "IN_CLSTRX_PURREQLN_SEARCH_APPROVED"

        Dim strSearchLocLevel As String = ""
        Dim strLastAccMonth As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object

        strAccYear = lstAccYear.SelectedItem.Value
        'strAccMonth = Month(Now())
        'strLastAccMonth = Val(strAccMonth) - 1

        'If strLocLevel = "1" Then
        '    strSearchLocLevel = " AND (LTRIM(RTRIM(PR.PRID)) LIKE '%" & Trim(strLocation) & "%') "
        'ElseIf strLocLevel = "2" Then
        '    strSearchLocLevel = " AND (LTRIM(RTRIM(PR.PRID)) LIKE '%" & Trim(strLocation) & "%' OR (PR.LocLevel IN ('1') AND PR.Status = '2') OR (PR.LocCode LIKE '%" & Trim(strLocation) & "%')) "
        'ElseIf strLocLevel = "3" Then
        '    strSearchLocLevel = " AND (LTRIM(RTRIM(PR.PRID)) LIKE '%" & Trim(strLocation) & "%' OR (PR.LocLevel IN ('1','2') AND PR.Status = '2') OR (PR.LocCode LIKE '%" & Trim(strLocation) & "%')) "
        'End If

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If


        strParamName = "LOCCODE|STATUS|PRLEVEL|ACCMONTH|ACCYEAR|PRID|STATUSLN|ITEMSEARCH|FROMLOCCODE|USERID"
        strParamValue = strLocation & "|" & 1 & "|" & intLevel & "|" & strAccMonth & "|" & strAccYear & "|" & srchPRID.Text & "|" & srchStatusLnList.SelectedItem.Value & "|" & _
                        "AND (Itm.ItemCode LIKE '%" & Trim(srchItem.Text) & "%' OR Itm.Description LIKE '%" & Trim(srchItem.Text) & "%' OR PRLn.OtherName LIKE '%" & Trim(srchItem.Text) & "%')" & "|" & _
                        ddlLocation.SelectedItem.Value & "|" & strUserId

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        intPRCount = objItemDs.Tables(0).Rows.Count

        Return objItemDs
    End Function

    Sub BindLocation(ByVal pv_strLocCode As String)
        Dim strOpCd As String = "ADMIN_CLSLOC_LOCATION_LIST_GET"
        Dim objLocDs As New Object()
        Dim strPRRefLocCode As String
        Dim strParam As String = ""
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim dr As DataRow
        Dim intSelectedIndex As Integer

        strPRRefLocCode = IIf(pv_strLocCode = "", "", pv_strLocCode)
        strParam = strPRRefLocCode & "|" & objAdminLoc.EnumLocStatus.Active & "|LocCode|"

        Try
            intErrNo = objPU.mtdGetLoc(strOpCd, strParam, objLocDs, "")

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_GET_LOC&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_trx_polist.aspx")
        End Try

        For intCnt = 0 To objLocDs.Tables(0).Rows.Count - 1
            With objLocDs.Tables(0).Rows(intCnt)
                .Item("LocCode") = Trim(.Item("LocCode"))
                .Item("Description") = Trim(.Item("LocCode")) & " (" & Trim(.Item("Description")) & ")"
                If .Item("LocCode") = Trim(strPRRefLocCode) Then intSelectedIndex = intCnt + 1
            End With
        Next intCnt

        dr = objLocDs.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "-All-"
        objLocDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlLocation.DataSource = objLocDs.Tables(0)
        ddlLocation.DataValueField = "LocCode"
        ddlLocation.DataTextField = "Description"
        ddlLocation.DataBind()
        ddlLocation.SelectedIndex = intSelectedIndex
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

#End Region
End Class

