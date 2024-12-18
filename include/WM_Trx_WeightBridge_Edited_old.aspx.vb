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

Public Class WM_Trx_WeightBridge_Edited : Inherits Page

    Protected WithEvents dgPRListing As DataGrid
    Protected WithEvents dgPRSend As DataGrid
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lblTracker As Label
    Protected WithEvents lblTracker_sent As Label

    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents lstDropList_Sent As DropDownList


    Protected WithEvents StatusList As DropDownList
    Protected WithEvents srchStatusList As DropDownList
    Protected WithEvents srchPRTypeList As DropDownList
    Protected WithEvents srchPRLevelList As DropDownList
    Protected WithEvents SortExpression As Label
    Protected WithEvents sortcol As Label
    Protected WithEvents srchTicketNo As TextBox
    Protected WithEvents srchContractNo As TextBox
    Protected WithEvents srchDONo As TextBox
    Protected WithEvents srchCust As TextBox
    Protected WithEvents srchProductList As DropDownList

    'Protected WithEvents srchUpdateBy As TextBox
    Protected WithEvents TotalAmount As Label
    Protected WithEvents Stock As ImageButton
    Protected WithEvents DC As ImageButton
    Protected WithEvents WS As ImageButton
    Protected WithEvents FA As ImageButton
    Protected WithEvents NU As ImageButton
    Protected WithEvents ibPrint As ImageButton
    'Protected WithEvents srchStatusLnList As DropDownList
    'Protected WithEvents srchApprovedBy As DropDownList
    Protected WithEvents lstAccMonth As DropDownList
    'Protected WithEvents lstFrLocCode As DropDownList
    Protected WithEvents lstToLocCode_Sent As DropDownList

    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents lblSearch As Label
    Protected WithEvents lblErrHour As Label
    Protected WithEvents cbExcel As CheckBox

    'Protected WithEvents srchItem As TextBox
    'Protected WithEvents srchPRID_Sent As TextBox
    'Protected WithEvents srchItem_Sent As TextBox

    Protected objIN As New agri.IN.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLtrx As New agri.GL.ClsTrx()
    Protected objWMTrx As New agri.WM.clsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()

    Dim strOppCd_GET_PRList As String = "IN_CLSTRX_PURREQ_LIST_GET"
    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim intPUAR As Integer
    Dim intWMAR As Integer

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
    Dim nCol_DDLToUser As Byte = 10

    Dim strDateFMt As String


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
        strDateFMt = Session("SS_DATEFMT")
        intWMAR = Session("SS_WMAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = False Then
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


                'srchApprovedBy.SelectedIndex = IIf(intLevel = 0, 1, intLevel)
                lstAccMonth.Text = strSelAccMonth

                BindSearchProductList()
                BindGrid()
            End If
        End If
    End Sub

    Sub BindSearchProductList()

        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.All), objWMTrx.EnumWeighBridgeTicketProduct.All))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.CPO), objWMTrx.EnumWeighBridgeTicketProduct.CPO))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.PK), objWMTrx.EnumWeighBridgeTicketProduct.PK))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.EFB), objWMTrx.EnumWeighBridgeTicketProduct.EFB))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Shell), objWMTrx.EnumWeighBridgeTicketProduct.Shell))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.AbuJanjang), objWMTrx.EnumWeighBridgeTicketProduct.AbuJanjang))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Fiber), objWMTrx.EnumWeighBridgeTicketProduct.Fiber))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Brondolan), objWMTrx.EnumWeighBridgeTicketProduct.Brondolan))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Solid), objWMTrx.EnumWeighBridgeTicketProduct.Solid))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.MinyakLimbah), objWMTrx.EnumWeighBridgeTicketProduct.MinyakLimbah))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.FFB), objWMTrx.EnumWeighBridgeTicketProduct.FFB))
        srchProductList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketProduct(objWMTrx.EnumWeighBridgeTicketProduct.Others), objWMTrx.EnumWeighBridgeTicketProduct.Others))

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgPRListing.CurrentPageIndex = 0
        dgPRListing.EditItemIndex = -1
        BindGrid()
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
        dgPRListing.DataSource = Nothing
        dgPRListing.DataBind()
        BindGrid()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim lbl As Label
        Dim txt As TextBox
        Dim btn As LinkButton

        lbl = E.Item.FindControl("lblDateReceived")
        lbl.Visible = False
        txt = E.Item.FindControl("TxtDateReceived")
        txt.Visible = True
        txt.Text = objGlobal.GetShortDate(strDateFMt, lbl.Text.Trim)

        lbl = E.Item.FindControl("lblHourReceived")
        lbl.Visible = False
        txt = E.Item.FindControl("TxtHourReceived")
        txt.Visible = True
        txt.Text = FormatDateTime(lbl.Text.Trim, DateFormat.ShortTime)

        lbl = E.Item.FindControl("lblBuyerFirst")
        lbl.Visible = False
        txt = E.Item.FindControl("TxtBuyerFirst")
        txt.Visible = True
        txt.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", "")

        lbl = E.Item.FindControl("lblBuyerSecond")
        lbl.Visible = False
        txt = E.Item.FindControl("TxtBuyerSecond")
        txt.Visible = True
        txt.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", "")


        lbl = E.Item.FindControl("lblBuyerNet")
        lbl.Visible = False
        txt = E.Item.FindControl("TxtBuyerNet")
        txt.Visible = True
        txt.Text = Replace(Replace(lbl.Text.Trim, ".", ""), ",", "")

        ''CType(dgItem.Cells(nCol_DDLToUser).FindControl("ddlToUser"), DropDownList).SelectedItem.Value()

        btn = E.Item.FindControl("Edit")
        btn.Visible = False

        btn = E.Item.FindControl("Cancel")
        btn.Visible = True

        btn = E.Item.FindControl("Update")
        btn.Visible = True

    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim StrOpCode As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_EDIT_UPDATE_GET"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim lblTicketNo As Label
        Dim TxtBuyerFirst As TextBox
        Dim TxtBuyerSec As TextBox
        Dim TxtBuyerNet As TextBox
        Dim TxtDateReceived As TextBox
        Dim TxtHourReceived As TextBox
        Dim lblDONo As Label
        Dim lblContractNo As Label
        Dim btn As LinkButton

        lblTicketNo = E.Item.FindControl("lblTicketNo")
        TxtBuyerFirst = E.Item.FindControl("TxtBuyerFirst")
        TxtBuyerSec = E.Item.FindControl("TxtBuyerSecond")
        TxtBuyerNet = E.Item.FindControl("TxtBuyerNet")
        TxtDateReceived = E.Item.FindControl("TxtDateReceived")
        TxtHourReceived = E.Item.FindControl("TxtHourReceived")
        lblDONo = E.Item.FindControl("lblDONo")
        lblContractNo = E.Item.FindControl("lblContractNo")

        If Not IsTime(Trim(TxtHourReceived.Text)) Then
            lblErrHour.Text = "input format jam (hh:mm) !"
            lblErrHour.Visible = True
            Exit Sub
        End If

        If TxtBuyerFirst.Text = "" Then
            TxtBuyerFirst.Text = 0
        End If
        If TxtBuyerSec.Text = "" Then
            TxtBuyerSec.Text = 0
        End If
        If TxtBuyerNet.Text = "" Then
            TxtBuyerNet.Text = 0
        End If
        If CDbl(TxtBuyerNet.Text) = 0 Then
            TxtBuyerNet.Text = CDbl(TxtBuyerFirst.Text) - CDbl(TxtBuyerSec.Text)
        End If

        strParamName = "STRSEARCH|TICKETNO|DONO|HOURRCV"
        strParamValue = "SET BuyerFirstWeight=" & TxtBuyerFirst.Text & _
                        ",BuyerSecondWeight=" & TxtBuyerSec.Text & _
                        ",BuyerNetWeight=" & TxtBuyerNet.Text & _
                        ",DateReceived='" & Date_Validation(TxtDateReceived.Text, False) & "'" & _
                        " Where TicketNo='" & Trim(lblTicketNo.Text) & "'" & "|" & _
                        Trim(lblTicketNo.Text) & "|" & _
                        Trim(lblDONo.Text) & "|" & _
                        TxtHourReceived.Text
        Try
            intErrNo = objGLtrx.mtdInsertDataCommon(StrOpCode, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=UPD_ITEM&errmesg=" & Exp.ToString() & "&redirect=pu/trx/pu_trx_GRList")
        End Try

        TxtBuyerFirst.Visible = False
        TxtBuyerSec.Visible = False
        TxtBuyerNet.Visible = False

        btn = E.Item.FindControl("Edit")
        btn.Visible = True

        btn = E.Item.FindControl("Cancel")
        btn.Visible = False

        btn = E.Item.FindControl("Update")
        btn.Visible = False

        BindGrid()
    End Sub

    Function IsTime(ByVal str As String)
        Dim r As Regex = New Regex("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$")
        Dim m As Match = r.Match(str)
        If (m.Success) Then
            Return True
        End If
        Return False
    End Function

    Sub btnNewStPR_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim Issuetype As String = CType(sender, ImageButton).CommandName
        Response.Redirect("IN_PurReq_Details.aspx?prqtype=" & Issuetype)
    End Sub

    Sub Btnrefresh_onClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        BindGrid()
    End Sub

    Sub btnPreview_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If cbExcel.Checked = True Then
            Dim btn As LinkButton
            Dim lbl As Label

            For intCnt = 0 To dgPRListing.Items.Count - 1
                btn = dgPRListing.Items.Item(intCnt).FindControl("Edit")
                btn.Visible = False
            Next


            Dim cAccMonth As String = IIf(Len(lstAccMonth.SelectedItem.Value) = 1, "0" & Trim(lstAccYear.SelectedItem.Value), Trim(lstAccMonth.SelectedItem.Value))
            strAccYear = lstAccYear.SelectedItem.Value

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=EDIT.TICKET-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.xls"

            Dim stringWrite = New System.IO.StringWriter()
            Dim htmlWrite = New HtmlTextWriter(stringWrite)

            dgPRListing.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.End()
        Else
            Response.Write("<Script Language=""JavaScript"">window.open(""../reports/WM_Rpt_WeighBridge_Transport.aspx?Type=Print&CompName=" & strCompany & _
                                    "&Location=" & strLocation & _
                                    "&strSearchMonth=" & lstAccMonth.SelectedItem.Value & _
                                    "&strSearchYear=" & lstAccYear.SelectedItem.Value & _
                                    "&srchTicketNo=" & srchTicketNo.Text & _
                                    "&srchContractNo=" & srchContractNo.Text & _
                                    "&srchDONo=" & srchDONo.Text & _
                                    "&srchCust=" & srchCust.Text & _
                                    "&srchProductList=" & srchProductList.SelectedItem.Value & _
                                    """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

#End Region

#Region "PROCEDURE"
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

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim strAcceptFormat As String

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"

        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                           pv_strInputDate, _
                                           strAcceptFormat, _
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub PageConTrol(ByVal pTipe As String)
        Dim nNourut As Integer

        'nNourut = 1
        'If pTipe = "INBOX" Then
        '    For intCnt = 0 To dgPRListing.Items.Count - 1
        '        If intLevel >= 2 Then
        '            CType(dgPRListing.Items(intCnt).FindControl("BtnSend"), Button).Enabled = True
        '        Else
        '            CType(dgPRListing.Items(intCnt).FindControl("BtnSend"), Button).Enabled = False
        '        End If

        '        If CType(dgPRListing.Items(intCnt).FindControl("lblPLstatus"), Label).Text = "2" Then
        '            dgPRListing.Items(intCnt).BackColor = Drawing.Color.Yellow
        '        ElseIf CType(dgPRListing.Items(intCnt).FindControl("lblPLstatus"), Label).Text = "3" Then
        '            dgPRListing.Items(intCnt).BackColor = Drawing.Color.LightGreen
        '        End If

        '        If Len(dgPRListing.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim) = 0 Or _
        '                dgPRListing.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim = "&nbsp;" Then
        '            CType(dgPRListing.Items(intCnt).FindControl("BtnSendPR"), Button).Visible = False
        '        End If

        '        If Len(dgPRListing.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim) > 0 And dgPRListing.Items(intCnt).Cells(nCol_PRIDShow).Text.Trim <> "&nbsp;" Then
        '            dgPRListing.Items(intCnt).Cells(nCol_NoUrut).Text = nNourut
        '            nNourut = nNourut + 1
        '        End If
        '    Next
        'End If

        If pTipe = "SENT" Then
            nNourut = 1
            For intCnt = 0 To dgPRSend.Items.Count - 1
                CType(dgPRSend.Items(intCnt).FindControl("BtnCancel"), Button).Visible = True

                If intLevel >= 2 Then
                    CType(dgPRSend.Items(intCnt).FindControl("BtnCancel"), Button).Enabled = True
                Else
                    CType(dgPRSend.Items(intCnt).FindControl("BtnCancel"), Button).Enabled = False
                End If

                If CType(dgPRSend.Items(intCnt).FindControl("lblplStatus_Sent"), Label).Text.Trim = "1" Then
                    CType(dgPRSend.Items(intCnt).FindControl("lblPLStatusDesc"), Label).Text = "Sending"
                End If

                If Len(CType(dgPRSend.Items(intCnt).FindControl("lblDthID"), Label).Text.Trim) > 0 Then
                    CType(dgPRSend.Items(intCnt).FindControl("lblPLStatusDesc"), Label).Text = "DTH"
                    CType(dgPRSend.Items(intCnt).FindControl("BtnCancel"), Button).Visible = False
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
                    If Len(CType(dgPRSend.Items(intCnt).FindControl("lblPOIDLast"), Label).Text.Trim) > 0 Or Len(CType(dgPRSend.Items(intCnt).FindControl("lblDthID"), Label).Text.Trim) > 0 Then
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

        'Dim count As Integer = 1
        'Dim arrDList As New ArrayList()

        'While Not count = dgPRListing.PageCount + 1
        '    arrDList.Add("Page " & count)
        '    count = count + 1
        'End While
        'lstDropList.DataSource = arrDList
        'lstDropList.DataBind()
        'lstDropList.SelectedIndex = dgPRListing.CurrentPageIndex

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
        Dim lbl As Label
        Dim lb As LinkButton

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

        For intCnt = 0 To dgPRListing.Items.Count - 1
            lbl = dgPRListing.Items.Item(intCnt).FindControl("lblTicketNo")
            If Trim(lbl.Text) = "TOTAL" Then
                lbl.Font.Bold = True
                lb = dgPRListing.Items.Item(intCnt).FindControl("Edit")
                lb.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblIndate")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblProductCode")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblSWeight")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblFWeight")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblNetWeight")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblDateReceived")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblHourReceived")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblBuyerFirst")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblBuyerSecond")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblBuyerNet")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblSelisih")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblOA")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblAmount")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblType")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblMin")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblMax")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblClaimed")
                lbl.Font.Bold = True
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblCtrPrice")
                lbl.Visible = False
                lbl = dgPRListing.Items.Item(intCnt).FindControl("lblClaimAmount")
                lbl.Font.Bold = True
            End If
        Next

        'BindPageList()
        'PageNo = dgPRListing.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgPRListing.PageCount
        'PageConTrol("INBOX")
        'BindToLocation("")
        'BindToUser()
    End Sub

    Protected Function LoadData() As DataSet
        Dim objItemDs As New Object()
        Dim strOpCode As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_EDIT_LIST_GET"

        Dim strSearchLocLevel As String = ""
        Dim strParamName As String
        Dim strParamValue As String
        Dim objData As New Object
        Dim StatusLn As String = ""
        Dim PrLevel As String = ""
        Dim sSQLKriteria As String = ""

        If lstAccMonth.SelectedItem.Value = 0 Then
            sSQLKriteria = "AND r.AccYear<='" & lstAccYear.SelectedItem.Value & "' "
        Else
            sSQLKriteria = "AND r.AccYear<='" & lstAccYear.SelectedItem.Value & "' And r.AccMonth<='" & lstAccMonth.SelectedItem.Value & "'"
        End If

        If srchTicketNo.Text <> "" Then
            sSQLKriteria = sSQLKriteria & "AND k.TicketNo LIKE '%" & srchTicketNo.Text & "%'"
        End If
        If srchContractNo.Text <> "" Then
            sSQLKriteria = sSQLKriteria & "AND k.ContractNo LIKE '%" & srchContractNo.Text & "%'"
        End If
        If srchDONo.Text <> "" Then
            sSQLKriteria = sSQLKriteria & "AND k.DONo LIKE '%" & srchDONo.Text & "%'"
        End If
        If srchCust.Text <> "" Then
            sSQLKriteria = sSQLKriteria & "AND b.Name LIKE '%" & srchCust.Text & "%'"
        End If
        If srchProductList.SelectedItem.Value <> 0 Then
            sSQLKriteria = sSQLKriteria & "AND r.ProductCode = '" & srchProductList.SelectedItem.Value & "'"
        End If

        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCode, _
                                                strParamName, _
                                                strParamValue, _
                                                objItemDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=IN_ITEM_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        intPRCount = objItemDs.Tables(0).Rows.Count
        If objItemDs.Tables(0).Rows.Count > 0 Then
            cbExcel.Visible = True
            ibPrint.Visible = True
        Else
            cbExcel.Visible = False
            ibPrint.Visible = False
        End If

        Return objItemDs
    End Function

    Sub dgPRListing_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

    Private Sub dgPRListing_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPRListing.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TICKET"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DATE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PRODUCT"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "CUSTOMER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TRANSPORTER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "VEHICLE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SELLER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 5
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "CUSTOMER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DIFF"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = ""
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "OA"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "AMOUNT"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TOLERANCE"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "CLAIMED"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgPRListing.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgPRListing_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPRListing.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(2).Visible = False
            e.Item.Cells(3).Visible = False
            e.Item.Cells(4).Visible = False
            e.Item.Cells(5).Visible = False
            e.Item.Cells(14).Visible = False
            e.Item.Cells(15).Visible = False
            e.Item.Cells(16).Visible = False
            e.Item.Cells(17).Visible = False
        End If
    End Sub

#End Region
End Class

