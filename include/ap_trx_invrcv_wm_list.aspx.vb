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


Public Class ap_trx_invrcv_wm_list : Inherits Page



    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objAPTrx As New agri.AP.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objGLSetup As New agri.GL.clsSetup()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intAPAR As Integer
    Dim strDateFormat As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim intLevel As Integer
    Dim strParamName As String
    Dim strParamValue As String

    Dim BtnConfirm As Button
    Dim BtnCancel As Button

    Dim objTicketDs As New DataSet()

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)

        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intAPAR = Session("SS_APAR")
        strDateFormat = Session("SS_DATEFMT")
        intLevel = Session("SS_USRLEVEL")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumAPAccessRights.APInvoiceReceive), intAPAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else

            lblErrGenerate.Visible = False
            lblErrGenInv.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "A.TrxID"
            End If

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            GenInvBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(GenInvBtn).ToString())
            btnGenerate.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnGenerate).ToString())


            If Not Page.IsPostBack Then
                'srchDate.Text = "1/" & strSelAccMonth & "/" & strSelAccYear
                'srchDateTo.Text = DateAdd(DateInterval.Month, 1, CDate(strSelAccMonth & "/1/" & strSelAccYear))
                'srchDateTo.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), DateAdd(DateInterval.Day, -1, CDate(srchDateTo.Text)))
                srchDate.SelectedDate = Date.Now()
                srchDateTo.SelectedDate = Date.Now()
                BindSearchStatusList()
                BindSubLocation()
                BindGrid()
                BindGridSUM()
                BindPageList()
                'BindAccount("", "", "", "", "", "")
                'LoadCOASetting()

            End If
        End If
    End Sub

    Sub BindSearchStatusList()
        srchStatusList.Items.Add(New ListItem(objAPTrx.mtdGetInvoiceRcvStatus(objAPTrx.EnumInvoiceRcvStatus.All), objAPTrx.EnumInvoiceRcvStatus.All))
        srchStatusList.Items.Add(New ListItem(objAPTrx.mtdGetInvoiceRcvStatus(objAPTrx.EnumInvoiceRcvStatus.Active), objAPTrx.EnumInvoiceRcvStatus.Active))
        srchStatusList.Items.Add(New ListItem(objAPTrx.mtdGetInvoiceRcvStatus(objAPTrx.EnumInvoiceRcvStatus.Confirmed), objAPTrx.EnumInvoiceRcvStatus.Confirmed))
        srchStatusList.Items.Add(New ListItem(objAPTrx.mtdGetInvoiceRcvStatus(objAPTrx.EnumInvoiceRcvStatus.Deleted), objAPTrx.EnumInvoiceRcvStatus.Deleted))
        srchStatusList.Items.Add(New ListItem(objAPTrx.mtdGetInvoiceRcvStatus(objAPTrx.EnumInvoiceRcvStatus.Cancelled), objAPTrx.EnumInvoiceRcvStatus.Cancelled))
        srchStatusList.SelectedIndex = 1
    End Sub

    Sub BindSubLocation()
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer

        sSQLKriteria = "SELECT SubLocCode,SubDescription From SH_LOCATIONSUB "
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Get,
                                         strParamName,
                                         strParamValue,
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try

        dr = objdsST.Tables(0).NewRow()
        dr("SubLocCode") = ""
        dr("SubDescription") = "Select Location"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        srchlocation.DataSource = objdsST.Tables(0)
        srchlocation.DataValueField = "SubLocCode"
        srchlocation.DataTextField = "SubDescription"
        srchlocation.DataBind()
        srchlocation.SelectedIndex = intSelectedIndex

    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If srchlocation.SelectedItem.Value = "" Then
            UserMsgBox(Me, "Please Select Location")
            srchlocation.Focus()
            Exit Sub
        End If

        dgList.CurrentPageIndex = 0
        dgList.EditItemIndex = -1
        BindGrid()
        BindGridSUM()
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

        Dim UpdButton As LinkButton
        Dim DelButton As LinkButton
        Dim EdtButton As LinkButton
        Dim CanButton As LinkButton

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgList.PageSize)

        dgList.DataSource = dsData
        If dgList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgList.CurrentPageIndex = 0
            Else
                dgList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgList.DataBind()
        BindPageList()
        PageNo = dgList.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgList.PageCount


        For intCnt = 0 To dgList.Items.Count - 1
            Status = dgList.Items.Item(intCnt).FindControl("lblStatus")
            strStatus = Status.Text

            BtnConfirm = dgList.Items.Item(intCnt).FindControl("BtnConfirm")
            BtnCancel = dgList.Items.Item(intCnt).FindControl("BtnCancel")
            BtnConfirm.Attributes("onclick") = "javascript:return ConfirmAction('confirm');"
            BtnCancel.Attributes("onclick") = "javascript:return ConfirmAction('cancel');"

            EdtButton = dgList.Items.Item(intCnt).FindControl("Edit")
            DelButton = dgList.Items.Item(intCnt).FindControl("Delete")
            UpdButton = dgList.Items.Item(intCnt).FindControl("Update")
            CanButton = dgList.Items.Item(intCnt).FindControl("Cancel")

            Select Case strStatus
                Case objAPTrx.mtdGetInvoiceRcvStatus(objAPTrx.EnumInvoiceRcvStatus.Active)
                    BtnConfirm.Visible = True
                    BtnCancel.Visible = False

                    EdtButton.Visible = False
                    DelButton.Visible = False
                    UpdButton.Visible = False
                    CanButton.Visible = False

                Case objAPTrx.mtdGetInvoiceRcvStatus(objAPTrx.EnumInvoiceRcvStatus.Deleted)
                    BtnConfirm.Visible = False
                    BtnCancel.Visible = False

                    EdtButton.Visible = False
                    DelButton.Visible = False
                    UpdButton.Visible = False
                    CanButton.Visible = False

                Case objAPTrx.mtdGetInvoiceRcvStatus(objAPTrx.EnumInvoiceRcvStatus.Confirmed)
                    BtnConfirm.Visible = False
                    BtnCancel.Visible = True

                    EdtButton.Visible = False
                    DelButton.Visible = False
                    UpdButton.Visible = False
                    CanButton.Visible = False

                Case Else
                    BtnConfirm.Visible = False
                    BtnCancel.Visible = False

                    EdtButton.Visible = False
                    DelButton.Visible = False
                    UpdButton.Visible = False
                    CanButton.Visible = False
            End Select
        Next

    End Sub

    Sub BindGridSUM()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lbl As Label

        dsData = LoadDataSUM()

        dgListSUM.DataBind()
        dgListPay.DataBind()

        For intCnt = 0 To dgListSUM.Items.Count - 1
            lbl = dgListSUM.Items.Item(intCnt).FindControl("lblNoUrut")
            If Trim(lbl.Text) = "999" Then
                lbl.Visible = False
            End If
        Next

        For intCnt = 0 To dgListPay.Items.Count - 1
            lbl = dgListPay.Items.Item(intCnt).FindControl("lblNoUrut")
            If Trim(lbl.Text) = "999" Then
                lbl.Visible = False
            End If
        Next
    End Sub

    Sub BindPageList()
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While

        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgList.CurrentPageIndex

    End Sub

    Protected Function LoadData() As DataSet

        Dim strOpCd As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_SEARCH"

        Dim dsResult As New Object

        Dim strSrchTrxID As String
        Dim strSrchRefNo As String
        Dim strSrchDate As String = ""
        Dim strSrchDateTo As String = ""
        Dim strSrchSupplier As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String



        strSrchTrxID = IIf(Trim(srchTrxID.Text) = "", "", " AND  A.TRXID LIKE '%" & srchTrxID.Text & "%'")
        strSrchRefNo = IIf(Trim(srchRefNo.Text) = "", "", " AND  RefNo LIKE '" & srchRefNo.Text & "'")
        strSrchDate = " AND  RefDate BETWEEN '" & Format(srchDate.SelectedDate, "yyyy-MM-dd") & "' AND '" & Format(srchDateTo.SelectedDate, "yyyy-MM-dd") & "'"
        strSrchSupplier = IIf(srchSupplier.Text = "", "", " AND (A.SupplierCode LIKE '" & srchSupplier.Text & "' OR E.Name LIKE '%" & srchSupplier.Text & "%') ")
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objAPTrx.EnumInvoiceRcvStatus.All, " AND  A.Status NOT IN ('3','4') ", " AND  A.Status = '" & srchStatusList.SelectedItem.Value & "'")
        strSrchLastUpdate = IIf(srchUpdateBy.Text = "", "", " AND  A.UpdateID LIKE '" & srchUpdateBy.Text & "'")


        strSearch = strSrchTrxID & strSrchRefNo & strSrchDate & strSrchStatus & strSrchSupplier & strSrchLastUpdate &
                    " AND A.LOCCODE = '" & strLocation & "' AND A.SubLocCode='" & Trim(srchlocation.SelectedItem.Value) & "'"

        strSearch = " WHERE " & Mid(Trim(strSearch), 6)

        strSearch = strSearch
        hidSearch.Value = strSearch

        strParamName = "STRSEARCH"
        strParamValue = strSearch

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd,
                                                 strParamName,
                                                 strParamValue,
                                                 dsResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_LOAD&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try



        'For intCnt = 0 To dsResult.Tables(0).Rows.Count - 1
        '    dsResult.Tables(0).Rows(intCnt).Item("TrxID") = Trim(dsResult.Tables(0).Rows(intCnt).Item("TrxID"))
        '    dsResult.Tables(0).Rows(intCnt).Item("RefNo") = Trim(dsResult.Tables(0).Rows(intCnt).Item("RefNo"))
        '    dsResult.Tables(0).Rows(intCnt).Item("RefDate") = Trim(dsResult.Tables(0).Rows(intCnt).Item("RefDate"))
        '    dsResult.Tables(0).Rows(intCnt).Item("SupplierCode") = Trim(dsResult.Tables(0).Rows(intCnt).Item("SupplierCode"))
        '    dsResult.Tables(0).Rows(intCnt).Item("AccCode") = Trim(dsResult.Tables(0).Rows(intCnt).Item("AccCode"))
        '    dsResult.Tables(0).Rows(intCnt).Item("Status") = Trim(dsResult.Tables(0).Rows(intCnt).Item("Status"))
        '    dsResult.Tables(0).Rows(intCnt).Item("UserName") = Trim(dsResult.Tables(0).Rows(intCnt).Item("UserName"))
        'Next

        Return dsResult

    End Function

    Protected Function LoadDataSUM() As DataSet
        Dim strOpCd As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICESUM_SEARCH"
        Dim dsResult As New Object

        Dim strSrchTrxID As String
        Dim strSrchRefNo As String
        Dim strSrchDate As String = ""
        Dim strSrchDateTo As String = ""
        Dim strSrchSupplier As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSearch As String
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim BegDate As String
        Dim EndDate As String



        strSrchDate = " AND  RefDate BETWEEN '" & Format(srchDate.SelectedDate, "yyyy-MM-dd") & "' AND '" & Format(srchDateTo.SelectedDate, "yyyy-MM-dd") & "'"
        strSrchSupplier = IIf(srchSupplier.Text = "", "", " AND (A.SupplierCode LIKE '" & srchSupplier.Text & "' OR C.Name LIKE '%" & srchSupplier.Text & "%') ")
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = objAPTrx.EnumInvoiceRcvStatus.All, "", " AND  A.Status = '" & srchStatusList.SelectedItem.Value & "'")
        strSrchLastUpdate = IIf(srchUpdateBy.Text = "", "", " AND  A.UpdateID LIKE '" & srchUpdateBy.Text & "'")

        strSearch = strSrchDate & strSrchStatus & strSrchSupplier & strSrchLastUpdate &
                    " AND A.LOCCODE = '" & strLocation & "' AND A.SubLocCode='" & Trim(srchlocation.SelectedItem.Value) & "'"

        strSearch = " WHERE " & Mid(Trim(strSearch), 6)

        strSearch = strSearch

        strParamName = "STRSEARCH"
        strParamValue = strSearch

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd,
                                                 strParamName,
                                                 strParamValue,
                                                 dsResult)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_LOAD&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        dgListSUM.DataSource = dsResult
        dgListSUM.DataBind()

        dgListPay.DataSource = dsResult
        dgListPay.DataBind()

        If dsResult.Tables(0).Rows.Count > 0 Then
            cbExcel.Visible = True
            PrintPrev.Visible = True
            cbExcelPay.Visible = True
            PrintPrevPay.Visible = True
            btnGenerate.Visible = True 'generate berdasarkan data tiket, bukan invoice --> regenerate to accomodate adjustment (revisi/bonus)

            BegDate = "1/" & Month(srchDateTo.SelectedDate) & "/" & Year(srchDateTo.SelectedDate)
            EndDate = DateAdd(DateInterval.Month, 1, CDate(Month(srchDateTo.SelectedDate) & "/1/" & Year(srchDateTo.SelectedDate)))
            EndDate = objGlobal.GetShortDate(Session("SS_DATEFMT"), DateAdd(DateInterval.Day, -1, CDate(EndDate)))

            If Month(srchDate.SelectedDate) <> Month(srchDateTo.SelectedDate) Then
                btnGenerate.Visible = False
            ElseIf Day(srchDate.SelectedDate) <> 1 Then
                btnGenerate.Visible = False
            ElseIf Day(srchDateTo.SelectedDate) <> Day(Date_Validation(EndDate, False)) Then
                btnGenerate.Visible = False
            End If
        End If

        Return dsResult

    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgList.CurrentPageIndex = 0
            Case "prev"
                dgList.CurrentPageIndex =
                Math.Max(0, dgList.CurrentPageIndex - 1)
            Case "next"
                dgList.CurrentPageIndex =
                Math.Min(dgList.PageCount - 1, dgList.CurrentPageIndex + 1)
            Case "last"
                dgList.CurrentPageIndex = dgList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_DEL"

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        Dim intErrNo As Integer
        Dim strTrxID As String
        Dim lblTrxID As Label

        dgList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTrxID = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTrxID")
        strTrxID = lblTrxID.Text

        strParamName = "TRXID|STATUS|USERID"
        strParamValue = strTrxID & "|" & objAPTrx.EnumInvoiceRcvStatus.Deleted & "|" & strUserId

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                    strParamName,
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        dgList.EditItemIndex = -1
        BindGrid()
        BindGridSUM()
    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim UpdButton As LinkButton
        Dim HrgDispText As Label
        Dim EditHrgText As Label
        Dim EditHrg As TextBox

        HrgDispText = E.Item.FindControl("lblHargaFinalDisplay")
        HrgDispText.Visible = False
        EditHrgText = E.Item.FindControl("lblHargaFinal")
        EditHrg = E.Item.FindControl("lstHargaFinal")
        EditHrg.Text = EditHrgText.Text
        EditHrg.Visible = True
        EditHrg.Focus()

        UpdButton = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Edit")
        UpdButton.Visible = False
        UpdButton = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Update")
        UpdButton.Visible = True
        UpdButton = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Cancel")
        UpdButton.Visible = True
        UpdButton = dgList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("Delete")
        UpdButton.Visible = False
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOpCd As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_UPD_PRICE"
        Dim objItemDs As New Object()
        Dim intCnt As Integer = 0
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0

        Dim EditLabel As Label
        Dim EditText As TextBox
        Dim strTrxID As String
        Dim strHargaFinal As String

        EditLabel = E.Item.FindControl("lblTrxID")
        strTrxID = EditLabel.Text
        EditText = E.Item.FindControl("lstHargaFinal")
        strHargaFinal = EditText.Text

        strParamName = "TRXID|HARGA|USERID"
        strParamValue = strTrxID & "|" & strHargaFinal & "|" & strUserId

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                    strParamName,
                                                    strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        End Try

        BindGrid()
        BindGridSUM()
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        BindGrid()
        BindGridSUM()
    End Sub

    Sub BtnGenInv_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_GET_TICKET"
        Dim strOpCdPPN As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_GET_TICKET_PPNAMOUNT"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        Dim intErrNo As Integer
        Dim strAccMonth As String
        Dim strAccYear As String

        ' Dim strDateFrom As String = Date_Validation(srchDate.Text, False)
        'Dim strDateTo As String = Date_Validation(srchDateTo.Text, False)

        strAccMonth = strSelAccMonth
        strAccYear = strSelAccYear
        If srchlocation.SelectedItem.Value = "" Then
            UserMsgBox(Me, "Please Select Location")
            srchlocation.Focus()
            Exit Sub
        End If

        If Month(srchDate.SelectedDate) <> Month(srchDateTo.SelectedDate) Then
            UserMsgBox(Me, "Only 1 Month period to generate.")
            srchDate.Focus()
            Exit Sub
        End If

        strParamName = "LOCCODE|SUBLOC|ACCMONTH|ACCYEAR|USERID|DATEFROM|DATETO"
        strParamValue = strLocation & "|" & Trim(srchlocation.SelectedItem.Value) & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId & "|" & Format(srchDate.SelectedDate, "yyyy-MM-dd") & "|" & Format(srchDateTo.SelectedDate, "yyyy-MM-dd")

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                    strParamName,
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        'generate ppn pembelian tbs
        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCdPPN, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        BindGrid()
        BindGridSUM()

        CheckTBSPriceNotFound()

        'langsung generate tbs journal
        'btnGenerate_Click(Sender, E)
    End Sub

    Protected Sub CheckTBSPriceNotFound()
        Dim strOpCd As String = "AP_CLSTRX_WEIGHBRIDGE_FFBPRICE_NOTFOUND"

        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        Dim intErrNo As Integer
        Dim strAccMonth As String
        Dim strAccYear As String
        'Dim strDateFrom As String = Date_Validation(srchDate.Text, False)
        'Dim strDateTo As String = Date_Validation(srchDateTo.Text, False)
        Dim dsResult As New Object

        strAccMonth = strSelAccMonth
        strAccYear = strSelAccYear

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID|DATEFROM|DATETO"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId & "|" & Format(srchDate.SelectedDate, "yyyy-MM-dd") & "|" & Format(srchDateTo.SelectedDate, "yyyy-MM-dd")

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd,
                                                strParamName,
                                                strParamValue,
                                                dsResult)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        If dsResult.Tables(0).Rows.Count > 0 Then
            lblErrGenInv.Visible = True
            lblErrGenInv.Text = dsResult.Tables(0).Rows(0).Item("Msg")
        End If
    End Sub

    Sub NewBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("ap_trx_invrcv_wm_det.aspx")
    End Sub

    Sub BtnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCd As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_CONFIRM"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        Dim intErrNo As Integer
        Dim strAccMonth As String
        Dim strAccYear As String
        Dim strTrxID As String
        Dim cblHrgFinal As Double = 0

        Dim btn As Button = CType(sender, Button)
        Dim dgList As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        strTrxID = CType(dgList.Cells(10).FindControl("lblTrxID"), Label).Text
        'cblHrgFinal = CType(dgList.Cells(5).FindControl("lblHargaFinal"), Label).Text

        strAccMonth = strSelAccMonth
        strAccYear = strSelAccYear

        'If cblHrgFinal = 0 Then
        '    UserMsgBox(Me, "Confirm failed, Harga akhir have to greater than 0...!!!")
        '    Exit Sub
        'End If

        strParamName = "TRXID|STATUS|USERID|LOCCODE|ACCMONTH|ACCYEAR"
        strParamValue = strTrxID & "|" & objAPTrx.EnumInvoiceRcvStatus.Confirmed &
                        "|" & strUserId & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                    strParamName,
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        BindGrid()
        BindGridSUM()
    End Sub

    Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strOpCd As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_CONFIRM"
        Dim strParamName As String = ""
        Dim strParamValue As String = ""

        Dim intErrNo As Integer
        Dim strAccMonth As String
        Dim strAccYear As String
        Dim strTrxID As String

        Dim btn As Button = CType(sender, Button)
        Dim dgList As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        strTrxID = CType(dgList.Cells(10).FindControl("lblTrxID"), Label).Text

        strAccMonth = strSelAccMonth
        strAccYear = strSelAccYear

        strParamName = "TRXID|STATUS|USERID|LOCCODE|ACCMONTH|ACCYEAR"
        strParamValue = strTrxID & "|" & objAPTrx.EnumInvoiceRcvStatus.Cancelled &
                        "|" & strUserId & "|" & strLocation & "|" & strAccMonth & "|" & strAccYear

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd,
                                                    strParamName,
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_DELETED&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list")
        End Try

        BindGrid()
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
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam,
                                                  strCompany,
                                                  strLocation,
                                                  strUserId,
                                                  objSysCfgDs)
        Catch Exp As Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CB_DEPOSIT_GET_CONFIG&errmesg=" & Exp.Message & "&redirect=CB/trx/cb_trx_DepositList.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt").Trim())

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat,
                                           pv_strInputDate,
                                           strAcceptFormat,
                                           objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub btnPreview_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim intCnt As Integer
        Dim strTRXID As String

        strTRXID = Trim(hidSearch.Value)
        If strTRXID = "" Then
            Exit Sub
        End If


        Try

            If cbExcelList.Checked = False And cbDetailByInvoice.Checked = True Then
                Response.Write("<Script Language=""JavaScript"">window.open(""../reports/AP_Rpt_InvRcv_WM_List.aspx?Type=Print&CompName=" & strCompany &
                        "&Location=" & strLocation &
                        "&strSearch=" & strTRXID &
                        "&strSearchDateFrom=" & srchDate.SelectedDate &
                        "&strSearchDateTo=" & srchDateTo.SelectedDate &
                        "&strRptInvoiceType=" & IIf(cbDetailByInvoice.Checked, "1", "0") &
                        "&strRptType=" & IIf(cbExcelListRekap.Checked, "R", "D") &
                        "&strExportToExcel=" & IIf(cbExcelList.Checked, "1", "0") &
                        """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

            ElseIf cbExcelListRekap.Checked = True Then

                For intCnt = 0 To dgList.Items.Count - 1
                    CType(dgList.Items.Item(intCnt).FindControl("Edit"), LinkButton).Visible = False
                    CType(dgList.Items.Item(intCnt).FindControl("Update"), LinkButton).Visible = False
                    CType(dgList.Items.Item(intCnt).FindControl("Delete"), LinkButton).Visible = False
                    CType(dgList.Items.Item(intCnt).FindControl("Cancel"), LinkButton).Visible = False
                    CType(dgList.Items.Item(intCnt).FindControl("BtnConfirm"), Button).Visible = False
                    CType(dgList.Items.Item(intCnt).FindControl("BtnCancel"), Button).Visible = False

                Next

                Response.Clear()
                Response.AddHeader("content-disposition", "attachment;filename=INVREKAP-" & Trim(strLocation) & ".xls")
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.xls"

                Dim stringWrite = New System.IO.StringWriter()
                Dim htmlWrite = New HtmlTextWriter(stringWrite)

                dgList.RenderControl(htmlWrite)
                Response.Write(stringWrite.ToString())
                Response.End()
            End If

        Catch ex As Exception
            srchTrxID.Text = Err.Description
        End Try
    End Sub

    Sub btnPrintPrev_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=INVREKAP-" & Trim(strLocation) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgListSUM.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub btnPrintPrevPay_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=INVREKAPPAY-" & Trim(strLocation) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgListPay.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub btnSaveSetting_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_DKtr As String = "WM_CLSTRX_TICKET_COASETTING_BUY_UPDATE"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String

        'strMn = ddlMonth.SelectedItem.Value.Trim
        'strYr = ddlyear.SelectedItem.Value.Trim

        strParamName = "COATBSPEMILIK|COATBSAGEN|COAPPN|COAONGKOSBONGKAR|COAONGKOSLAPANGAN|COAPPH"
        strParamValue = Trim(radTbsPemilik.SelectedValue) & "|" & Trim(radTbsAgen.SelectedValue) & "|" & Trim(radTbsPPN.SelectedValue) & "|" &
                        Trim(radTBSOBongkar.SelectedValue) & "|" & Trim(radTbsOLapangan.SelectedValue) & "|" & Trim(radTbsPPH.SelectedValue)

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_DKtr, strParamName, strParamValue)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        LoadCOASetting()
    End Sub

    Protected Function LoadCOASetting() As DataSet
        Dim strOpCd_DKtr As String = "WM_CLSTRX_TICKET_COASETTING_BUY_GET"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String

        'strMn = ddlMonth.SelectedItem.Value.Trim
        'strYr = ddlyear.SelectedItem.Value.Trim

        strParamName = "STRSEARCH"
        strParamValue = ""

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_DKtr, strParamName, strParamValue, objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objTicketDs.Tables(0).Rows.Count > 0 Then
            radTbsPemilik.SelectedValue = RTrim(objTicketDs.Tables(0).Rows(0).Item("COATBSPemilik"))
            radTbsAgen.SelectedValue = RTrim(objTicketDs.Tables(0).Rows(0).Item("COATBSAgen"))
            radTbsPPN.SelectedValue = RTrim(objTicketDs.Tables(0).Rows(0).Item("COAPPN"))
            radTbsPPH.SelectedValue = RTrim(objTicketDs.Tables(0).Rows(0).Item("COAPPH"))
            radTBSOBongkar.SelectedValue = RTrim(objTicketDs.Tables(0).Rows(0).Item("COAOngkosBongkar"))
            radTbsOLapangan.SelectedValue = RTrim(objTicketDs.Tables(0).Rows(0).Item("COAOngkosLapangan"))

        End If
    End Function

    Sub BindAccount(ByVal pv_strCOATBSPemilik As String, ByVal pv_strCOATBSAgen As String, ByVal pv_strCOAPPN As String, ByVal pv_strCOAOB As String, ByVal pv_strCOAOL As String, ByVal pv_strCOAPPH As String)
        Dim strOpCd As String = "GL_CLSSETUP_ACCOUNTCODE_LIST_GET"
        Dim strParam As String = "Order By ACC.AccCode|And ACC.Status = '" & objGLSetup.EnumAccountCodeStatus.Active & "'"
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim intSelectedIndexCOATBSPemilik As Integer = 0
        Dim intSelectedIndexCOATBSAgen As Integer = 0
        Dim intSelectedIndexCOAPPN As Integer = 0
        Dim intSelectedIndexCOAOB As Integer = 0
        Dim intSelectedIndexCOAOL As Integer = 0
        Dim intSelectedIndexCOAOBBiaya As Integer = 0
        Dim intSelectedIndexCOAOLBiaya As Integer = 0
        Dim objAccDs As New Object

        strParam = strParam & IIf(Session("SS_COACENTRALIZED") = "1", "", " AND LocCode = '" & Trim(strLocation) & "' ")

        Try
            intErrNo = objGLSetup.mtdGetMasterList(strOpCd,
                                                   strParam,
                                                   objGLSetup.EnumGLMasterType.AccountCode,
                                                   objAccDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PU_TRX_GR_ACCCODE_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = objAccDs.Tables(0).NewRow()
        dr("AccCode") = ""
        ' dr("_Description") = "Please select account code"
        objAccDs.Tables(0).Rows.InsertAt(dr, 0)

        radTbsPemilik.DataSource = objAccDs.Tables(0)
        radTbsPemilik.DataValueField = "AccCode"
        radTbsPemilik.DataTextField = "_Description"
        radTbsPemilik.DataBind()


        radTbsAgen.DataSource = objAccDs.Tables(0)
        radTbsAgen.DataValueField = "AccCode"
        radTbsAgen.DataTextField = "_Description"
        radTbsAgen.DataBind()


        radTbsPPN.DataSource = objAccDs.Tables(0)
        radTbsPPN.DataValueField = "AccCode"
        radTbsPPN.DataTextField = "_Description"
        radTbsPPN.DataBind()


        radTbsPPH.DataSource = objAccDs.Tables(0)
        radTbsPPH.DataValueField = "AccCode"
        radTbsPPH.DataTextField = "_Description"
        radTbsPPH.DataBind()


        radTBSOBongkar.DataSource = objAccDs.Tables(0)
        radTBSOBongkar.DataValueField = "AccCode"
        radTBSOBongkar.DataTextField = "_Description"
        radTBSOBongkar.DataBind()


        radTbsOLapangan.DataSource = objAccDs.Tables(0)
        radTbsOLapangan.DataValueField = "AccCode"
        radTbsOLapangan.DataTextField = "_Description"
        radTbsOLapangan.DataBind()

    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_Jurnal As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_GENERATE_JOURNAL"
        Dim strOpCd_PPN As String = "AP_CLSTRX_WEIGHBRIDGE_INVOICE_GENERATE_PPNAMOUNT"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String
        Dim BegDate As String
        Dim EndDate As String

        BegDate = "1/" & Month(srchDateTo.SelectedDate) & "/" & Year(srchDateTo.SelectedDate)
        EndDate = DateAdd(DateInterval.Month, 1, CDate(Month(srchDateTo.SelectedDate) & "/1/" & Year(srchDateTo.SelectedDate)))
        EndDate = objGlobal.GetShortDate(Session("SS_DATEFMT"), DateAdd(DateInterval.Day, -1, CDate(EndDate)))

        If Month(srchDate.SelectedDate) <> Month(srchDateTo.SelectedDate) Then
            lblErrGenerate.Visible = True
            UserMsgBox(Me, "Only 1 Month period to generate.")
            Exit Sub
            ' ElseIf Day(srchDate.SelectedDate) <> 1 Then
            '     UserMsgBox(Me,"Please put day 1 as beginning.")            
            '     Exit Sub
            ' ElseIf Day(srchDateTo.SelectedDate) <> Day(Date_Validation(EndDate, False)) Then
            '     lblErrGenerate.Visible = True
            '     lblErrGenerate.Text = "Please put end of day of this period."
            '     Exit Sub
        End If

        strMn = Month(srchDate.SelectedDate)
        strYr = Year(srchDate.SelectedDate)

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID"
        strParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & strUserId

        'generate jurnal pembelian tbs
        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_Jurnal, strParamName, strParamValue, objDataSet)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objDataSet.Tables(0).Rows.Count > 0 Then
            lblErrGenerate.Visible = True
            lblErrGenerate.Text = objDataSet.Tables(0).Rows(0).Item("Msg")
        End If

        'generate ppn pembelian tbs
        'Try
        '    intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_PPN, strParamName, strParamValue, objDataSet)
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        'End Try

        'If objDataSet.Tables(0).Rows.Count > 0 Then
        '    lblErrGenerate.Visible = True
        '    lblErrGenerate.Text = lblErrGenerate.Text & "<br>" & objDataSet.Tables(0).Rows(0).Item("Msg")
        'End If

        'LoadData()
        'LoadDataPPH()
    End Sub

    Sub dgTicketList_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

End Class
