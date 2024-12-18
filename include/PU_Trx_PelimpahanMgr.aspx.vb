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

Public Class PU_Trx_PelimpahanMgr : Inherits Page

    Protected WithEvents dgPRListing As DataGrid
    Protected WithEvents dgPRSend As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblTracker_sent As Label

    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lstDropList_Sent As DropDownList
    Protected WithEvents lstOutstanding As DropDownList
    Protected WithEvents lstDisposTo As DropDownList


    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchPRTypeList As DropDownList
    Protected WithEvents srchPRLevelList As DropDownList
    Protected WithEvents srchGroupItem As DropDownList

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
    Protected WithEvents srchApprovedBy As DropDownList

    Protected WithEvents lstFrLocCode As DropDownList
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents lblSearch As Label

    Protected WithEvents srchItem As TextBox
    Protected WithEvents srchPRID_Sent As TextBox
    Protected WithEvents srchItem_Sent As TextBox

    Protected objIN As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim strOppCd_GET_PRList As String = "IN_CLSTRX_PURREQ_LIST_GET"
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String

    Dim intPUAR As Integer

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


    'Dim BtnApproved As Button
    Dim BtnCancel As Button
    Dim APPButton As LinkButton
    Dim UpdButton As LinkButton
    Dim CancelButton As LinkButton
    Dim SaveButton As LinkButton

    Dim nCol_NoUrut As Byte = 0
    Dim nCol_PRILND As Byte = 1
    Dim nCol_PRID As Byte = 2
    Dim nCol_PRIDShow As Byte = 3
    Dim nCol_ItemCode As Byte = 4

    Dim nCol_DDLToLocCode As Byte = 9
    Dim nCol_lblLocCode As Byte = 9

#Region "COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        intPUAR = Session("SS_PUAR")
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
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumPUAccessRights.PUPurchaseOrder), intPUAR) = False Or intLevel < 2 Then
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
                    BindAccYear(strAccYear)
                Else
                    BindAccYear(strSelAccYear)
                End If


                srchApprovedBy.SelectedIndex = IIf(intLevel = 0, 1, intLevel)
                lstAccMonth.Text = strSelAccMonth
                lSetComboLocation("")
                BindSearchList()
                BindPRTypeList()
                BindPRLevelList()
                BindItemGroup()

                BindGrid()
                BindFromLocation("")
                BindToUserPO_SentItem()
                BindGrid_SendItem()

                BindPageList()
                BindPageList_Sent()

                CheckStatus()
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPRListing.CurrentPageIndex = 0
        dgPRListing.EditItemIndex = -1
        BindGrid()
        CheckStatus()
        BindPageList()
    End Sub

    Sub srchBtnSent_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPRSend.CurrentPageIndex = 0
        dgPRSend.EditItemIndex = -1
        BindGrid_SendItem()
        CheckStatus()
        BindPageList()
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

        BindGrid()
        CheckStatus()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
            CheckStatus()
        End If
    End Sub

    Sub PagingIndexChanged_Sent(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgPRSend.CurrentPageIndex = lstDropList_Sent.SelectedIndex
            BindGrid_SendItem()
            CheckStatus()
        End If
    End Sub

    Sub OnPageChanged_Sent(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        Try
            dgPRSend.CurrentPageIndex = e.NewPageIndex
            BindGrid_SendItem()
            CheckStatus()
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        Try
            dgPRListing.CurrentPageIndex = e.NewPageIndex
            BindGrid()

            CheckStatus()
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        sortcol.Text = IIf(sortcol.Text = "ASC", "DESC", "ASC")
        dgPRListing.CurrentPageIndex = lstDropList.SelectedIndex

        BindGrid()
        CheckStatus()
    End Sub

    Sub CheckStatus()
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
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)

    End Sub

    Sub btnNewStPR_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim Issuetype As String = CType(sender, ImageButton).CommandName
        Response.Redirect("IN_PurReq_Details.aspx?prqtype=" & Issuetype)
    End Sub

    Sub BtnSendPR_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCd As String = "PU_CLSTRX_PLMPH_TOUSER_SHUSER_ADD"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim StrPRID As String = ""
        Dim StrPRLNID As String = ""
        Dim strItemCode As String = ""
        Dim strSelectedUser As String = ""
        Dim bUpdate As Boolean = False

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        StrPRID = dgItem.Cells(nCol_PRID).Text
        strSelectedUser = CType(dgItem.Cells(7).FindControl("ddlToUser"), DropDownList).SelectedItem.Value

        If CType(dgItem.Cells(7).FindControl("ddlToUser"), DropDownList).SelectedIndex = 0 Then
            UserMsgBox(Me, "Please Select User Name...!!!")
            Exit Sub
        End If

        For intCnt = 0 To dgPRListing.Items.Count - 1
            If Len(Trim(dgPRListing.Items(intCnt).Cells(nCol_PRILND).Text)) > 0 And dgPRListing.Items(intCnt).Cells(nCol_PRID).Text = StrPRID Then
                strParamName = "PRID|PRLNID|ITEM|SID|STATUS|TOLOCCODE|CDATE|UDATE|UPID"
                strParamValue = StrPRID & "|" & _
                                dgPRListing.Items(intCnt).Cells(nCol_PRILND).Text & _
                                "|" & dgPRListing.Items(intCnt).Cells(nCol_ItemCode).Text & _
                                "|" & strSelectedUser & "|" & "1" & "|" & _
                                strLocation & "|" & _
                                Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "|" & _
                                Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "|" & _
                                strUserId

                bUpdate = False
                Try
                    intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                            strParamName, _
                                                            strParamValue)
                    bUpdate = True
                Catch Exp As System.Exception
                    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                End Try
            End If

        Next

        'If bUpdate = True Then
        '    UserMsgBox(Me, "Send Sucsess...!!!")
        'End If

        If bUpdate = True Then
            lblErrMessage.Text = ""
            lblErrMessage.Text = "Dispotition To " & strSelectedUser & " Sucsess...!!!"
            lblErrMessage.Visible = True
            lblErrMessage.BackColor = Drawing.Color.LightGreen
        End If


        BindGrid()
        BindGrid_SendItem()

    End Sub

    Sub BtnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCd As String = "PU_CLSTRX_PLMPH_TOUSER_SHUSER_ADD"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim StrPRID As String = ""
        Dim StrPRLNID As String = ""
        Dim strItemCode As String = ""
        Dim strSelectedUser As String = ""
        Dim bUpdate As Boolean = False

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        StrPRLNID = dgItem.Cells(nCol_PRILND).Text
        StrPRID = dgItem.Cells(nCol_PRID).Text
        strItemCode = dgItem.Cells(nCol_ItemCode).Text
        strSelectedUser = CType(dgItem.Cells(7).FindControl("ddlToUser"), DropDownList).SelectedItem.Value

        If CType(dgItem.Cells(7).FindControl("ddlToUser"), DropDownList).SelectedIndex = 0 Then
            UserMsgBox(Me, "Please Select User Name...!!!")
            Exit Sub
        End If

        strParamName = "PRID|PRLNID|ITEM|SID|STATUS|TOLOCCODE|CDATE|UDATE|UPID"
        strParamValue = StrPRID & "|" & _
                        StrPRLNID & "|" & _
                        strItemCode & "|" & _
                        strSelectedUser & "|" & "1" & "|" & _
                        strLocation & "|" & _
                        Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "|" & _
                        Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "|" & _
                        strUserId

        bUpdate = False
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)
            bUpdate = True
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        'If bUpdate = True Then
        '    UserMsgBox(Me, "Send Sucsess...!!!")
        'End If

        If bUpdate = True Then
            lblErrMessage.Text = ""
            lblErrMessage.Text = "Dispotition To " & strSelectedUser & " Sucsess...!!!"
            lblErrMessage.Visible = True
            lblErrMessage.BackColor = Drawing.Color.LightGreen
        End If

        BindGrid()
        BindGrid_SendItem()

    End Sub

    Sub BtnProcess_onClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        'Dim strOpCd As String = "PU_CLSTRX_PLMPH_TOUSER_SHUSER_ADD"
        'Dim strParamName As String = ""
        'Dim strParamValue As String = ""
        'Dim StrPRID As String = ""
        'Dim StrPRLNID As String = ""
        'Dim strItemCode As String = ""
        'Dim strSelectedLoc As String = ""
        'Dim strToUser As String = ""
        'Dim bUpdate As Boolean = False
        'Dim strMgr As String = ""
        'Dim strSelectedUser As String = ""
        'Dim bExist As Boolean = False

        'bExist = False
        'Try

        '    Dim btn As Button = CType(sender, Button)
        '    Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        '    For intCnt = 0 To dgPRListing.Items.Count - 1

        '        If CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).SelectedIndex > 0 Or CType(dgPRListing.Items(intCnt).FindControl("ddlToUser"), DropDownList).SelectedIndex > 0 Then
        '            bExist = True

        '            StrPRLNID = dgPRListing.Items(intCnt).Cells(0).Text 'dgItem.Cells(0).Text
        '            StrPRID = dgPRListing.Items(intCnt).Cells(1).Text 'dgItem.Cells(1).Text
        '            strItemCode = dgPRListing.Items(intCnt).Cells(2).Text 'dgItem.Cells(2).Text
        '            strSelectedLoc = CType(dgPRListing.Items(intCnt).FindControl("lblToLocCode"), Label).Text

        '            If CType(dgPRListing.Items(intCnt).FindControl("ddlToUser"), DropDownList).SelectedIndex > 0 Then
        '                strToUser = CType(dgPRListing.Items(intCnt).FindControl("ddlToUser"), DropDownList).SelectedItem.Value
        '            Else
        '                strToUser = ""
        '            End If


        '            strSelectedUser = CType(dgItem.Cells(7).FindControl("ddlToUser"), DropDownList).SelectedItem.Value

        '            strParamName = "PRID|PRLNID|ITEM|SID|STATUS|TOLOCCODE|CDATE|UDATE|UPID"
        '            strParamValue = StrPRID & "|" & _
        '                            StrPRLNID & "|" & _
        '                            strItemCode & "|" & _
        '                            strSelectedUser & "|" & "1" & "|" & _
        '                            strLocation & "|" & _
        '                            Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "|" & _
        '                            Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "|" & _
        '                            strUserId

        '            bUpdate = False
        '            Try
        '                intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
        '                                                        strParamName, _
        '                                                        strParamValue)
        '                bUpdate = True
        '            Catch Exp As System.Exception
        '                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        '            End Try

        '        End If
        '    Next

        '    If bExist = False Then
        '        UserMsgBox(Me, "Please Select Location or Purchasing User..!!!")
        '        Exit Sub
        '    End If


        '    If bUpdate = True Then
        '        UserMsgBox(Me, "Send Sucsess...!!!")

        '        BindGrid()
        '        BindGrid_SendItem()
        '    End If

        'Catch ex As Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try
    End Sub

    Sub BtnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCd As String = "PU_CLSTRX_PLMPH_DISP_TOLOC_SHLOC_ADD"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim StrPRID As String = ""
        Dim StrPRLNID As String = ""
        Dim strItemCode As String = ""
        Dim strSelectedLoc As String = ""
        Dim bUpdate As Boolean = False
        Dim StrMgr As String = ""

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        StrPRLNID = dgItem.Cells(nCol_PRILND).Text
        StrPRID = dgItem.Cells(nCol_PRID).Text
        strItemCode = dgItem.Cells(nCol_ItemCode).Text
        strSelectedLoc = CType(dgItem.Cells(10).FindControl("lblToLocCode"), Label).Text
        strMgr = CType(dgItem.Cells(10).FindControl("ddlToLocCode"), DropDownList).SelectedItem.Value

        If CType(dgItem.Cells(10).FindControl("ddlToLocCode"), DropDownList).SelectedIndex = 0 Then
            UserMsgBox(Me, "Please Select Purchase Location...!!!")
            Exit Sub
        End If

        strParamName = "PRID|PRLNID|ITEM|STATUS|DPLOCCODE|TOLOCCODE|UDATE|SID|RID|UPID|MGRID"
        strParamValue = StrPRID & _
                        "|" & StrPRLNID & _
                        "|" & strItemCode & _
                        "|" & "3" & _
                        "|" & strLocation & _
                        "|" & strSelectedLoc & _
                        "|" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & _
                        "|" & strUserId & _
                        "|" & "-" & _
                        "|" & strUserId & _
                        "|" & StrMgr

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
            UserMsgBox(Me, "Send Sucsess...!!!")
        End If

        BindGrid()
        BindGrid_SendItem()

    End Sub

    Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strOpCd As String = "PU_CLSTRX_PLMPH_TOUSER_SHUSER_DEL"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim StrPRID As String = ""
        Dim StrPRLNID As String = ""
        Dim strItemCode As String = ""
        Dim strSelectedLoc As String = ""
        Dim bUpdate As Boolean = False

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        StrPRLNID = dgItem.Cells(nCol_PRILND).Text
        StrPRID = dgItem.Cells(nCol_PRID).Text
        strItemCode = dgItem.Cells(nCol_ItemCode).Text

        strParamName = "PRID|PRLN|ITEM|LOC"
        strParamValue = RTrim(StrPRID) & "|" & RTrim(StrPRLNID) & "|" & RTrim(strItemCode) & "|" & strLocation

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
            UserMsgBox(Me, "Cancel Sucsess...!!!")
        End If

        BindGrid()
        BindGrid_SendItem()
    End Sub

    Sub BtnCancelPR_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strOpCd As String = "PU_CLSTRX_PLMPH_TOUSER_SHUSER_DEL"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim StrPRID As String = ""
        Dim StrPRLNID As String = ""
        Dim strItemCode As String = ""
        Dim strSelectedLoc As String = ""
        Dim bUpdate As Boolean = False

        Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        StrPRLNID = dgItem.Cells(nCol_PRILND).Text
        StrPRID = dgItem.Cells(nCol_PRID).Text
        strItemCode = dgItem.Cells(nCol_ItemCode).Text

        With dgPRSend
            For intCnt = 0 To .Items.Count - 1
                If Len(Trim(.Items(intCnt).Cells(nCol_PRILND).Text)) > 0 And .Items(intCnt).Cells(nCol_PRID).Text = StrPRID Then
                    strParamName = "PRID|PRLN|ITEM|LOC"
                    strParamValue = RTrim(StrPRID) & "|" & RTrim(.Items(intCnt).Cells(nCol_PRILND).Text) & "|" & RTrim(.Items(intCnt).Cells(nCol_ItemCode).Text) & "|" & strLocation

                    bUpdate = False

                    Try
                        intErrNo = objGLtrx.mtdInsertDataCommon(strOpCd, _
                                                                strParamName, _
                                                                strParamValue)
                        bUpdate = True
                    Catch Exp As System.Exception
                        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
                    End Try
                End If
            Next
        End With

        If bUpdate = True Then
            UserMsgBox(Me, "Cancel Sucsess...!!!")
        End If

        BindGrid()
        BindGrid_SendItem()
    End Sub

    Sub Btnrefresh_onClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        BindGrid()
    End Sub
 
#End Region

#Region "PROCEDURE"

    Sub lSetComboLocation(ByVal pv_LocCode As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objToLocation As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_TOLOC_SHLOC_GET"

        strParamName = "SEARCHSTR|SORTEXP"
        strParamValue = "" & "||Order By LocCode"


        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocation)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objToLocation.Tables(0).Rows.Count - 1
            objToLocation.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objToLocation.Tables(0).Rows(intCnt).Item("LocCode"))
            objToLocation.Tables(0).Rows(intCnt).Item("Description") = Trim(objToLocation.Tables(0).Rows(intCnt).Item("Description"))
            If objToLocation.Tables(0).Rows(intCnt).Item("LocCode") = pv_LocCode Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        dr = objToLocation.Tables(0).NewRow()
        dr("LocCode") = ""
        dr("Description") = "Select Location"
        objToLocation.Tables(0).Rows.InsertAt(dr, 0)

        lstFrLocCode.DataSource = objToLocation.Tables(0)
        lstFrLocCode.DataValueField = "LocCode"
        lstFrLocCode.DataTextField = "Description"
        lstFrLocCode.DataBind()
        lstFrLocCode.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub BindFromLocation(ByVal pv_LocCode As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim introw As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objToLocation As New Object
        Dim dr As DataRow
        'Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_TOLOC_SHLOC_GET"
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_TOLOC_MANAGER_GET"

        For intCnt = 0 To dgPRListing.Items.Count - 1

            strParamName = "STRSEARCH"
            strParamValue = "Where sh.UserID<>'" & Trim(strUserId) & "' "

            'strParamValue = "Where LocLevel IN('3','2') And LocType='4'  " & _
            '                " And sh.UserID<>'" & trim(strUserId) & "' "
            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objToLocation)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            For introw = 0 To objToLocation.Tables(0).Rows.Count - 1
                objToLocation.Tables(0).Rows(introw).Item("UserID") = Trim(objToLocation.Tables(0).Rows(introw).Item("UserID"))
                objToLocation.Tables(0).Rows(introw).Item("UserName") = Trim(objToLocation.Tables(0).Rows(introw).Item("UserName")) & "-" & "(" & Trim(objToLocation.Tables(0).Rows(introw).Item("description")) & ")"
                If objToLocation.Tables(0).Rows(introw).Item("LocCode") = pv_LocCode Then
                    intSelectedIndex = introw + 1
                End If
            Next introw

            dr = objToLocation.Tables(0).NewRow()
            dr("UserID") = ""
            dr("UserName") = "Select Location"
            objToLocation.Tables(0).Rows.InsertAt(dr, 0)

            CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataSource = objToLocation.Tables(0)
            CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataValueField = "UserID"
            CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataTextField = "UserName"
            CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataBind()
            CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).SelectedIndex = intSelectedIndex - 1
        Next

        

        'Dim strParamName As String
        'Dim strParamValue As String
        'Dim intErrNo As Integer
        'Dim intCnt As Integer = 0
        'Dim intSelectedIndex As Integer
        'Dim objToLocation As New Object
        'Dim dr As DataRow
        'Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_TOLOC_SHLOC_GET"

        'strParamName = "SEARCHSTR|SORTEXP"
        'strParamValue = "" & "||Order By LocCode"

        'Try
        '    intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
        '                                        strParamName, _
        '                                        strParamValue, _
        '                                        objToLocation)

        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        'End Try

        'For intCnt = 0 To objToLocation.Tables(0).Rows.Count - 1
        '    objToLocation.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objToLocation.Tables(0).Rows(intCnt).Item("LocCode"))
        '    objToLocation.Tables(0).Rows(intCnt).Item("Description") = Trim(objToLocation.Tables(0).Rows(intCnt).Item("Description"))
        '    If objToLocation.Tables(0).Rows(intCnt).Item("LocCode") = pv_LocCode Then
        '        intSelectedIndex = intCnt + 1
        '    End If
        'Next intCnt

        'dr = objToLocation.Tables(0).NewRow()
        'dr("LocCode") = ""
        'dr("Description") = "Select Location"
        'objToLocation.Tables(0).Rows.InsertAt(dr, 0)

        'lstFrLocCode.DataSource = objToLocation.Tables(0)
        'lstFrLocCode.DataValueField = "LocCode"
        'lstFrLocCode.DataTextField = "Description"
        'lstFrLocCode.DataBind()
        'lstFrLocCode.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub BindToLocation(ByVal pv_LocCode As String)
        'Dim strParamName As String
        'Dim strParamValue As String
        'Dim intErrNo As Integer
        'Dim intCnt As Integer = 0
        'Dim intSelectedIndex As Integer
        'Dim objToLocation As New Object
        'Dim dr As DataRow
        'Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_TOLOC_SHLOC_GET"

        'For intCnt = 0 To dgPRListing.Items.Count - 1

        '    strParamName = "SEARCHSTR|SORTEXP"
        '    strParamValue = "Where LocLevel IN('3','2') OR LocCode='" & CType(dgPRListing.Items(intCnt).FindControl("lblOrderLoc"), Label).Text & "' " & "||Order By LocCode"

        '    Try
        '        intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
        '                                            strParamName, _
        '                                            strParamValue, _
        '                                            objToLocation)

        '    Catch Exp As System.Exception
        '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        '    End Try

        '    'For intCnt = 0 To objToLocation.Tables(0).Rows.Count - 1
        '    '    objToLocation.Tables(0).Rows(intCnt).Item("LocCode") = Trim(objToLocation.Tables(0).Rows(intCnt).Item("LocCode"))
        '    '    objToLocation.Tables(0).Rows(intCnt).Item("Description") = Trim(objToLocation.Tables(0).Rows(intCnt).Item("Description"))
        '    '    If objToLocation.Tables(0).Rows(intCnt).Item("LocCode") = pv_LocCode Then
        '    '        intSelectedIndex = intCnt + 1
        '    '    End If
        '    'Next intCnt


        '    dr = objToLocation.Tables(0).NewRow()
        '    dr("LocCode") = ""
        '    dr("Description") = "Select Location"
        '    objToLocation.Tables(0).Rows.InsertAt(dr, 0)

        '    CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataSource = objToLocation.Tables(0)
        '    CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataValueField = "LocCode"
        '    CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataTextField = "Description"
        '    CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).DataBind()
        '    CType(dgPRListing.Items(intCnt).FindControl("ddlToLocCode"), DropDownList).SelectedIndex = intSelectedIndex - 1
        'Next
    End Sub

    Sub BindToUser(ByVal pv_User As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer        
        Dim objToUser As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_USER_GET"

        For intCnt = 0 To dgPRListing.Items.Count - 1

            strParamName = "SEARCHSTR"
            strParamValue = "Where sloc.LocCode='" & strLocation & "' " & _
                                " And ((p.ProdTypeCode='" & CType(dgPRListing.Items(intCnt).FindControl("lblGroupItem"), Label).Text.Trim & "') OR ('" & CType(dgPRListing.Items(intCnt).FindControl("lblGroupItem"), Label).Text.Trim & "'='')) " & _
                                " and sh.UsrLevel<=6 "

            Try
                intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue, _
                                                    objToUser)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            dr = objToUser.Tables(0).NewRow()
            dr("UserID") = ""
            dr("UserDispLay") = "Select User Name"
            objToUser.Tables(0).Rows.InsertAt(dr, 0)

            CType(dgPRListing.Items(intCnt).FindControl("ddlToUser"), DropDownList).DataSource = objToUser.Tables(0)
            CType(dgPRListing.Items(intCnt).FindControl("ddlToUser"), DropDownList).DataValueField = "UserID"
            CType(dgPRListing.Items(intCnt).FindControl("ddlToUser"), DropDownList).DataTextField = "UserDispLay"
            CType(dgPRListing.Items(intCnt).FindControl("ddlToUser"), DropDownList).DataBind()
            CType(dgPRListing.Items(intCnt).FindControl("ddlToUser"), DropDownList).SelectedIndex = intSelectedIndex - 1
        Next
    End Sub

    Sub BindToUserPO_SentItem()
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objToUserPO As New Object
        Dim dr As DataRow
        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_USER_GET"

        strParamName = "SEARCHSTR"
        strParamValue = "Where sloc.LocCode='" & strLocation & "' and sh.UsrLevel<=1 "

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objToUserPO)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objToUserPO.Tables(0).Rows.Count - 1
            objToUserPO.Tables(0).Rows(intCnt).Item("UserDispLay") = Trim(objToUserPO.Tables(0).Rows(intCnt).Item("UserDispLay"))
        Next intCnt

        dr = objToUserPO.Tables(0).NewRow()
        dr("UserID") = ""
        dr("UserDispLay") = "Select User Name"
        objToUserPO.Tables(0).Rows.InsertAt(dr, 0)

        lstDisposTo.DataSource = objToUserPO.Tables(0)
        lstDisposTo.DataValueField = "UserID"
        lstDisposTo.DataTextField = "UserDispLay"
        lstDisposTo.DataBind()
        lstDisposTo.SelectedIndex = intSelectedIndex - 1

    End Sub

    Sub BindItemGroup()
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objItemGR As New Object
        Dim strOpCd As String = "IN_CLSSETUP_PRODTYPE_LIST_GET"

        strParamName = "SEARCHSTR|SORTEXP"

        strParamValue = "" & "|" & ""

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemGR)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objItemGR.Tables(0).Rows.Count - 1
            objItemGR.Tables(0).Rows(intCnt).Item("ProdTypeCode") = Trim(objItemGR.Tables(0).Rows(intCnt).Item("ProdTypeCode"))
            objItemGR.Tables(0).Rows(intCnt).Item("Description") = Trim(objItemGR.Tables(0).Rows(intCnt).Item("Description"))
        Next intCnt

        dr = objItemGR.Tables(0).NewRow()
        dr("ProdTypeCode") = ""
        dr("Description") = "Please Select Item Group"
        objItemGR.Tables(0).Rows.InsertAt(dr, 0)

        srchGroupItem.DataSource = objItemGR.Tables(0)
        srchGroupItem.DataValueField = "ProdTypeCode"
        srchGroupItem.DataTextField = "Description"
        srchGroupItem.DataBind()
        srchGroupItem.SelectedIndex = intSelectedIndex - 1
    End Sub

    Sub BindToMgrUser(ByVal sender As Object, ByVal e As EventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim objToUser As New Object
        Dim objToLocation As New Object

        Dim strOpCd As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_USER_GET"
        Dim strOpCd_Loc As String = "PU_CLSSETUP_SUPPLIER_PELIMPAHAN_TOLOC_MANAGER_GET"
        Dim nLocCode As String = ""

        Dim DDL As DropDownList = CType(sender, DropDownList)
        Dim dgItem As DataGridItem = CType(DDL.NamingContainer, DataGridItem)

        strParamName = "STRSEARCH"
        strParamValue = "WHERE p.UserId='" & CType(dgItem.Cells(nCol_DDLToLocCode).FindControl("ddlToLocCode"), DropDownList).SelectedItem.Value & "'"

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Loc, _
                                                strParamName, _
                                                strParamValue, _
                                                objToLocation)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        CType(dgItem.Cells(nCol_lblLocCode).FindControl("lblToLocCode"), Label).Text = ""
        If objToLocation.Tables(0).Rows.Count > 0 Then
            nLocCode = Trim(objToLocation.Tables(0).Rows(intCnt).Item("LocCode"))
            CType(dgItem.Cells(nCol_lblLocCode).FindControl("lblToLocCode"), Label).Text = nLocCode
        End If

    End Sub

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

    Sub PageConTrol(ByVal pTipe As String)
        Dim nNourut As Integer

        If pTipe = "INBOX" Then
            nNourut = 1
            For intCnt = 0 To dgPRListing.Items.Count - 1
                If CType(dgPRListing.Items(intCnt).FindControl("lblPLstatus"), Label).Text = "2" Then
                    dgPRListing.Items(intCnt).BackColor = Drawing.Color.Yellow
                End If

                If Len(dgPRListing.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim) = 0 Or _
                        dgPRListing.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim = "&nbsp;" Then
                    CType(dgPRListing.Items(intCnt).FindControl("BtnSendPR"), Button).Visible = False
                End If

                If Len(dgPRListing.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim) > 0 And dgPRListing.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim <> "&nbsp;" Then
                    dgPRListing.Items(intCnt).Cells(nCol_NoUrut).Text = nNourut
                    nNourut = nNourut + 1
                End If
            Next
        End If

        If pTipe = "SENT" Then
            nNourut = 1
            For intCnt = 0 To dgPRSend.Items.Count - 1
                'CType(dgPRSend.Items(intCnt).FindControl("lblPLStatusDesc"), Label).Text = CType(dgPRSend.Items(intCnt).FindControl("lblplStatus_Sent"), Label).Text
                If CType(dgPRSend.Items(intCnt).FindControl("lblplStatus_Sent"), Label).Text.Trim = "1" Then
                    CType(dgPRSend.Items(intCnt).FindControl("lblPLStatusDesc"), Label).Text = "Sending"
                End If

                If Len(CType(dgPRSend.Items(intCnt).FindControl("lblPOIDLast"), Label).Text.Trim) > 0 Then
                    CType(dgPRSend.Items(intCnt).FindControl("lblPLStatusDesc"), Label).Text = "PO"
                    CType(dgPRSend.Items(intCnt).FindControl("BtnCancel"), Button).Visible = False
                End If

                If Len(dgPRSend.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim) = 0 Or _
                        dgPRSend.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim = "&nbsp;" Then
                    CType(dgPRSend.Items(intCnt).FindControl("BtnCancelPR"), Button).Visible = False
                End If

                If Len(dgPRSend.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim) > 0 Or _
                        dgPRSend.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim <> "&nbsp;" Then
                    If Len(CType(dgPRSend.Items(intCnt).FindControl("lblPOIDLast"), Label).Text.Trim) > 0 Then
                        CType(dgPRSend.Items(intCnt).FindControl("BtnCancelPR"), Button).Visible = False
                    End If
                End If

                If Len(dgPRSend.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim) > 0 And dgPRSend.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim <> "&nbsp;" Then
                    dgPRSend.Items(intCnt).Cells(nCol_NoUrut).Text = nNourut
                    nNourut = nNourut + 1
                End If

            Next
        End If
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

    Sub BindPageList_Sent()

        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgPRSend.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList_Sent.DataSource = arrDList
        lstDropList_Sent.DataBind()
        lstDropList_Sent.SelectedIndex = dgPRSend.CurrentPageIndex

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

    Sub BindGrid()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadData()
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
        PageConTrol("INBOX")
        BindToUser("")
        BindToLocation("")
    End Sub

    Sub BindGrid_SendItem()

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadSendItem()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPRSend.PageSize)

        dgPRSend.DataSource = dsData
        If dgPRSend.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPRSend.CurrentPageIndex = 0
            Else
                dgPRSend.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPRSend.DataBind()
        BindPageList_Sent()
        PageNo = dgPRSend.CurrentPageIndex + 1
        lblTracker_sent.Text = "Page " & PageNo & " of " & dgPRSend.PageCount
        PageConTrol("SENT")
    End Sub

    Protected Function LoadData() As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "PU_CLSTRX_PLMPH_PURREQLN_LIST_TOUSER_SHLOC_GET"

        Dim strSearchLocLevel As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
        Dim StatusLn As String = ""
        Dim PrLevel As String = ""
        Dim sSQLKriteria As String = ""

        If srchPRID.Text.Trim <> "" Then
            sSQLKriteria = "AND PRLn.PRID LIKE '%" & srchPRID.Text & "%'"            
        ElseIf srchItem.Text.Trim <> "" Then
            sSQLKriteria = "AND ((prln.ItemCode LIKE '%" & srchItem.Text & "%') OR (Itm.Description LIKE '%" & srchItem.Text & "%'))"
        ElseIf lstFrLocCode.SelectedIndex > 0 Then
            sSQLKriteria = "AND PR.PRLocCode LIKE '" & lstFrLocCode.SelectedItem.Value & "'"
        ElseIf srchGroupItem.SelectedIndex > 0 Then
            sSQLKriteria = "AND itm.ProdTypeCode LIKE '" & srchGroupItem.SelectedItem.Value & "'"
        End If


        sSQLKriteria = sSQLKriteria & "AND pl.ToLocCode='" & strLocation & "'"

        StatusLn = "3"
        PrLevel = "6"

        strParamName = "LOCCODE|STATUS|PRLEVEL|STRSEARCH|MGRID"
        strParamValue = strLocation & "|" & StatusLn & "|" & PrLevel & "|" & sSQLKriteria & "|" & strUserId

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

    Protected Function LoadSendItem() As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "PU_CLSTRX_PLMPH_PURREQLN_SEND_TOUSER_SHLOC_GET"

        Dim strSearchLocLevel As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
        Dim PrLevel As String = ""
        Dim StatusLn As String = ""
        Dim sSQLKriteria As String = ""
        Dim sSQLKriteria2 As String = ""

        If srchPRID_Sent.Text.Trim <> "" Then
            sSQLKriteria = "AND prln.PrID LIKE '%" & srchPRID_Sent.Text & "%'"
        Else
            sSQLKriteria = "AND ((prln.ItemCode LIKE '%" & srchItem_Sent.Text & "%') OR (Itm.Description LIKE '%" & srchItem_Sent.Text & "%'))"
        End If

        sSQLKriteria = sSQLKriteria & "AND ((pl.AccMonth='" & lstAccMonth.SelectedItem.Value & "') OR ('" & lstAccMonth.SelectedItem.Value & "'='0'))  And pl.AccYear='" & lstAccYear.SelectedItem.Value & "'"


        StatusLn = "3"
        PrLevel = "6"

        sSQLKriteria2 = "Where ((PROutstanding='" & lstOutstanding.SelectedItem.Value & "') OR ('" & lstOutstanding.SelectedItem.Value & "'='0')) And ((UserPO='" & lstDisposTo.SelectedItem.Value & "') or ('" & lstDisposTo.SelectedItem.Value & "'=''))"
        strParamName = "LOCCODE|STATUS|PRLEVEL|STRSEARCH|MGRID|STRSEARCH2"
        strParamValue = strLocation & "|" & StatusLn & "|" & PrLevel & "|" & sSQLKriteria & "|" & strUserId & "|" & sSQLKriteria2

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

    Sub dgPRListing_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
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

