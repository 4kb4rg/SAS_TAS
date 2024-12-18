
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
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap


Public Class TX_trx_PPNKeluaranList : Inherits Page

    Protected WithEvents lblErrMesage As Label
    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchFPNo As TextBox
    Protected WithEvents srchDocID As TextBox
    Protected WithEvents srchSupplier As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents intRec As HtmlInputHidden
    Protected WithEvents PostingBtn As ImageButton
    Protected WithEvents PrintDoc As ImageButton
    Protected WithEvents CancelBtn As ImageButton
    Protected WithEvents hidTrxStatus As HtmlInputHidden
    Protected WithEvents cbExcel As CheckBox
    Protected WithEvents cbCSV As CheckBox
    Protected WithEvents lblTtlDPPAmt As Label
    Protected WithEvents lblTtlPPNCreditedAmt As Label
    Protected WithEvents lblTtlPPNCreditedNotAmt As Label

    Protected WithEvents LinkDownload As HyperLink


    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objCTSetup As New agri.CT.clsSetup()
    Protected objCBTrx As New agri.CB.clsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()
    Dim objGLRpt As New agri.GL.clsReport()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strLangCode As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim intCTAR As Integer

    Dim ObjTaxDs As New Object()
    Dim objConfigDs As New Object()
    Dim objLangCapDs As New Object()

    Dim strTXDescr As String
    Dim strDocID As String
    Dim strSupplierCode As String
    Dim strAccCode As String

    Dim strTaxObjectCodeTag As String
    Dim strDescTag As String
    Dim strActCodeTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String
    Dim strAcceptDateFormat As String

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strLangCode = Session("SS_LANGCODE")
        strAccMonth = Session("SS_GLACCMONTH")
        strAccYear = Session("SS_GLACCYEAR")
        intCTAR = Session("SS_CTAR")
        strLocType = Session("SS_LOCTYPE")
        strSelAccMonth = Session("SS_SELACCMONTH")
        strSelAccYear = Session("SS_SELACCYEAR")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumCTAccessRights.CTMaster), intCTAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = ""
            End If

            PostingBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PostingBtn).ToString())
            CancelBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(CancelBtn).ToString())

            If Not Page.IsPostBack Then
                lblErrMesage.Visible = False

                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                GetPPNPosted()
                BindGrid()
                BindPageList()

                PostingBtn.Attributes("onclick") = "javascript:return ConfirmAction('posting now');"
                CancelBtn.Attributes("onclick") = "javascript:return ConfirmAction('cancel this posting period');"
            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        GetPPNPosted()
        BindGrid()
        BindPageList()
    End Sub

    Sub BindGrid()
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim lblDocID As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim chkBox As CheckBox
        Dim TtlDPPAmt As Double
        Dim TtlPPNCreditedAmt As Double
        Dim TtlPPNCreditedNotAmt As Double

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
            chkBox = dgLine.Items.Item(intCnt).FindControl("chkSelect")
            If UCase(Trim(lbl.Text)) = "POSTED" Then
                chkBox.Enabled = False
            End If
        Next

        For intCnt = 0 To dsData.Tables(0).Rows.Count - 1
            If dsData.Tables(0).Rows(intCnt).Item("InitStatus") <> 1 Then
                TtlDPPAmt += dsData.Tables(0).Rows(intCnt).Item("FPDPPAmount")
                TtlPPNCreditedAmt += dsData.Tables(0).Rows(intCnt).Item("FPAmountCredited")
                TtlPPNCreditedNotAmt += dsData.Tables(0).Rows(intCnt).Item("FPAmountCreditedNot")
            End If
        Next
        lblTtlDPPAmt.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TtlDPPAmt, 2), 2)
        lblTtlPPNCreditedAmt.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TtlPPNCreditedAmt, 2), 2)
        lblTtlPPNCreditedNotAmt.Text = objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(TtlPPNCreditedNotAmt, 2), 2)
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
        Dim strOpCd_GET As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer
        Dim xCnt As Integer = 0
        Dim strOrderBy As String

        strAccMonth = lstAccMonth.SelectedItem.Value
        strAccYear = lstAccYear.SelectedItem.Value
        strSearch = IIf(Trim(srchFPNo.Text) = "", "", "FPNo Like '%" & Trim(srchFPNo.Text) & "%' AND ")
        strSearch = strSearch + IIf(Trim(srchDocID.Text) = "", "", "DocID LIKE '%" & Trim(srchDocID.Text) & "%' AND ")
        strSearch = strSearch + IIf(Trim(srchSupplier.Text) = "", "", "(SupplierCode LIKE '%" & Trim(srchSupplier.Text) & "%' OR SupplierName LIKE '%" & Trim(srchSupplier.Text) & "%') AND ")

        If (Trim(strSearch) <> "") Then
            strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If

        If hidTrxStatus.Value <> objCBTrx.EnumTaxStatus.Closed Then
            strOrderBy = IIf(Trim(SortExpression.Text) = "", "ORDER BY (cast(YEAR(B.FPDate) AS int)*100) + (cast(MONTH(B.FPDate) AS int)), A.DocID, FPNo ASC", "Order By " & Trim(SortExpression.Text) & " ")
            strOrderBy = strOrderBy + IIf(Trim(SortExpression.Text) = "", "", IIf(Trim(SortCol.Text) = "", "ASC ", Trim(SortCol.Text)))

            strOpCd_GET = "TX_CLSTRX_PPNKELUARAN_LIST"

            PostingBtn.Visible = True
            CancelBtn.Visible = False
        Else
            strOrderBy = IIf(Trim(SortExpression.Text) = "", "ORDER BY (cast(YEAR(B.FPDate) AS int)*100) + (cast(MONTH(B.FPDate) AS int)), A.DocID, FPNo ASC", "Order By " & Trim(SortExpression.Text) & " ")
            strOrderBy = strOrderBy + IIf(Trim(SortExpression.Text) = "", "", IIf(Trim(SortCol.Text) = "", "ASC ", Trim(SortCol.Text)))

            strOpCd_GET = "TX_CLSTRX_PPNKELUARAN_POSTED_LIST"

            PostingBtn.Visible = False
            CancelBtn.Visible = True
        End If

        strParamName = "LOCCODE|POSTACCYEAR|POSTACCMONTH|STRSEARCH|ORDERBY"
        strParamValue = strLocation & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "|" & _
                        strSearch & "|" & _
                        strOrderBy

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        intRec.Value = 0

        Return ObjTaxDs
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
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_FPDetail(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strSelDocID As String
        Dim strSelDocDate As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strSelDescr As String
        Dim strSelAccCode As String
        Dim strSelAmount As String
        Dim strSelSplCode As String
        Dim strSelSplName As String
        Dim strSelTrxID As String

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocID")
        strSelDocID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocDate")
        strSelDocDate = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccMonth")
        strSelAccMonth = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccYear")
        strSelAccYear = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDescription")
        strSelDescr = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccCode")
        strSelAccCode = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocAmount")
        strSelAmount = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblSupplierCode")
        strSelSplCode = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblSupplierName")
        strSelSplName = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTrxID")
        strSelTrxID = lblDelText.Text


        Response.Write("<Script Language=""JavaScript"">window.open(""TX_trx_FPEntryDet.aspx?DocID=" & Trim(strSelDocID) & _
                    "&DocDate=" & strSelDocDate & _
                    "&DocAmount=" & Trim(strSelAmount) & _
                    "&AccCode=" & strSelAccCode & _
                    "&AccMonth=" & Trim(strSelAccMonth) & _
                    "&AccYear=" & Trim(strSelAccYear) & _
                    "&Descr=" & Trim(strSelDescr) & _
                    "&SplCode=" & Trim(strSelSplCode) & _
                    "&SplName=" & Trim(strSelSplName) & _
                    "&TrxID=" & Trim(strSelTrxID) & _
                    """,null ,""status=yes, height=655, width=1000, top=60, left=120, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        BindGrid()
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
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
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

    Sub GetPPNPosted()
        Dim strOpCd_GET As String = "TX_CLSTRX_PPNKELUARAN_POSTED_LIST"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        strAccMonth = lstAccMonth.SelectedItem.Value
        strAccYear = lstAccYear.SelectedItem.Value

        strParamName = "LOCCODE|POSTACCYEAR|POSTACCMONTH|STRSEARCH|ORDERBY"
        strParamValue = strLocation & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "||"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If ObjTaxDs.Tables(0).Rows.Count > 0 Then
            hidTrxStatus.Value = objCBTrx.EnumTaxStatus.Closed
        Else
            hidTrxStatus.Value = objCBTrx.EnumTaxStatus.Verified
        End If
    End Sub

    Sub PostingBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdGen As String = "TX_CLSTRX_PPNKELUARAN_POSTED"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim postAccMonth As String
        Dim postAccYear As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            postAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
            lblErrMesage.Text = "Please select only 1 account period to proceed this posting."
            lblErrMesage.Visible = True
            Exit Sub
        Else
            postAccMonth = lstAccMonth.SelectedItem.Value
        End If

        postAccYear = lstAccYear.SelectedItem.Value

        Dim intInputPeriod As Integer = (CInt(postAccYear) * 100) + CInt(postAccMonth)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intSelPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "Invalid posting period."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "Invalid posting period."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "This period already locked."
                Exit Sub
            End If
        End If

        Dim dgLineDeItem As DataGridItem
        Dim intCheck As Integer = 0

        For Each dgLineDeItem In dgLine.Items
            Dim myCheckbox As CheckBox = CType(dgLineDeItem.Cells(8).Controls(1), CheckBox)
            If myCheckbox.Checked = True Then
                intCheck = intCheck + 1
            End If
        Next

        If intCheck > 0 Then
            strParamName = "POSTACCMONTH|POSTACCYEAR|LOCCODE|UPDATEID"
            strParamValue = postAccMonth & "|" & postAccYear & "|" & strLocation & "|" & strUserId

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCdGen, _
                                                   strParamName, _
                                                   strParamValue, _
                                                   ObjTaxDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PERIOD POSTING&errmesg=" & Exp.ToString() & "&redirect=pu/trx/AP_trx_InvoiceRcvList")
            End Try

            lblErrMesage.Visible = True
            lblErrMesage.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))
        Else
            lblErrMesage.Visible = True
            lblErrMesage.Text = "Cannot found any data to posted!"
            Exit Sub
        End If

        GetPPNPosted()
        BindGrid()
    End Sub

    Sub PrintDoc_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim dgLineDeItem As DataGridItem
        Dim blnCheck As Integer
        Dim strTrxID As String
        Dim strTrxLnID As String
        Dim strDocID As String
        Dim strFPNo As String
        Dim strPostAccMonth As String
        Dim strPostAccYear As String
        Dim strDocStatus As String
        Dim strPrevDocID As String = ""
        Dim strPrevCheck As String = 0
        Dim strOpCd_GET As String = "TX_CLSTRX_PPNKELUARAN_UPDATE"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strExportToExcel As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strPostAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
            lblErrMesage.Text = "Please select only 1 account period to proceed this ppn printing."
            lblErrMesage.Visible = True
            Exit Sub
        Else
            strPostAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strPostAccYear = lstAccYear.SelectedItem.Value

        If hidTrxStatus.Value <> objCBTrx.EnumTaxStatus.Closed Then
            'For Each dgLineDeItem In dgLine.Items
            '    strPrevCheck = blnCheck
            '    strPrevDocID = strDocID

            '    Dim myCheckbox As CheckBox = CType(dgLineDeItem.Cells(8).Controls(1), CheckBox)
            '    If myCheckbox.Checked = False Then
            '        blnCheck = 1
            '    Else
            '        blnCheck = 2
            '    End If

            '    Dim myLabel As Label = CType(dgLineDeItem.Cells(9).Controls(1), Label)
            '    strTrxID = myLabel.Text
            '    Dim myLabel1 As Label = CType(dgLineDeItem.Cells(10).Controls(1), Label)
            '    strTrxLnID = myLabel1.Text
            '    Dim myLabel2 As Label = CType(dgLineDeItem.Cells(0).Controls(1), Label)
            '    strDocID = myLabel2.Text
            '    Dim myLabel3 As Label = CType(dgLineDeItem.Cells(4).Controls(1), Label)
            '    strFPNo = myLabel3.Text

            '    'If strPrevDocID = strDocID Then
            '    '    If strPrevCheck = 1 Then
            '    '        blnCheck = 1
            '    '    Else
            '    '        blnCheck = 2
            '    '    End If
            '    'End If

            '    strParamName = "LOCCODE|TRXID|TRXLNID|DOCID|FPNO|ISCHECK|UPDATEID|POSTACCMONTH|POSTACCYEAR"
            '    strParamValue = strLocation & "|" & Trim(strTrxID) & "|" & Trim(strTrxLnID) & "|" & Trim(strDocID) & "|" & Trim(strFPNo) & "|" & _
            '                    blnCheck & "|" & Trim(strUserId) & "|" & _
            '                    IIf(blnCheck = 1, "", strPostAccMonth) & "|" & IIf(blnCheck = 1, "", strPostAccYear)

            '    Try
            '        intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_GET, _
            '                                              strParamName, _
            '                                              strParamValue)


            '    Catch Exp As System.Exception
            '        Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CP_CLSTRX_REKONSILEDTL_ADD&errmesg=" & lblErrMessage.Text & "&redirect=CB/trx/CB_trx_Rekonsilelist")
            '    End Try
            'Next
        End If

        If hidTrxStatus.Value = objCBTrx.EnumTaxStatus.Closed Then
            strDocStatus = "3"
        Else
            strDocStatus = "2"
        End If

        strExportToExcel = IIf(cbExcel.Checked = True, "1", "0")

        If cbCSV.Checked = False Then
            Response.Write("<Script Language=""JavaScript"">window.open(""TX_trx_PrintPPN.aspx?PostAccMonth=" & strPostAccMonth & _
                                        "&PostAccYear=" & strPostAccYear & _
                                        "&FPNo=" & srchFPNo.Text & _
                                        "&DocID=" & srchDocID.Text & _
                                        "&Supplier=" & srchSupplier.Text & _
                                        "&PPNInit=" & "KELUARAN" & _
                                        "&DocStatus=" & strDocStatus & _
                                        "&ExportToExcel=" & strExportToExcel & _
                                        """,null ,""status=yes, height=500, width=800, top=100, left=220, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        Else
            Dim strPeriod As String = IIf(Len(strPostAccMonth) = 1, "0" & Trim(strPostAccMonth), Trim(strPostAccMonth)) & Trim(strPostAccYear)
            Dim strOpCd As String
            Dim strSearch As String
            Dim objMapPath As String
            Dim objFTPFolder As String
            Dim objRptDs As New DataSet()
            Dim intCnt As Integer
            Dim strUrl As String
			Dim intUrut As Integer
            Dim NoUrut As String
			
            strOpCd = "TX_STDRPT_GET_CSV_PPNKELUARAN"
            strSearch = IIf(Trim(srchFPNo.Text) = "", "", "FPNo Like '%" & Trim(srchFPNo.Text) & "%' AND ")
            strSearch = strSearch + IIf(Trim(srchDocID.Text) = "", "", "DocID LIKE '%" & Trim(srchDocID.Text) & "%' AND ")
            strSearch = strSearch + IIf(Trim(srchSupplier.Text) = "", "", "(SupplierCode LIKE '%" & Trim(srchSupplier.Text) & "%' OR SupplierName LIKE '%" & Trim(srchSupplier.Text) & "%') AND ")

            If (Trim(strSearch) <> "") Then
                strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
            End If

            strParamName = "LOCCODE|POSTACCMONTH|POSTACCYEAR|STATUS|STRSEARCH|ORDERBY"
            strParamValue = strLocation & "|" & strPostAccMonth & "|" & strPostAccYear & "|" & strDocStatus & "|" & strSearch & "|" & "ORDER BY FPDate, FPNo ASC"

            Try
                intErrNo = objGLRpt.mtdGetReportDataCommon(strOpCd, strParamName, strParamValue, objRptDs, objMapPath, objFTPFolder)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            Try
                intErrNo = objAdmin.mtdGetBasePath(objMapPath)
            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=&errmesg=" & lblErrMessage.Text & "&redirect=")
            End Try

            strUrl = objFTPFolder.Substring(InStr(objFTPFolder, "ftp") - 1, Len(objFTPFolder) - InStr(objFTPFolder, "ftp") + 1)
            strUrl = Replace(strUrl, "\", "/")

            Dim MyCSVFileK As String = objFTPFolder & "VAT_OUT-" & Trim(strCompany) & Trim(strPeriod) & ".csv"
            If My.Computer.FileSystem.FileExists(MyCSVFileK) = True Then
                My.Computer.FileSystem.DeleteFile(MyCSVFileK)
            End If
            Dim dataToWriteK As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(MyCSVFileK, True)

		    dataToWriteK.WriteLine("FK;KD_JENIS_TRANSAKSI;FG_PENGGANTI;NOMOR_FAKTUR;MASA_PAJAK;TAHUN_PAJAK;TANGGAL_FAKTUR;NPWP;NAMA;ALAMAT_LENGKAP;JUMLAH_DPP;JUMLAH_PPN;JUMLAH_PPNBM;ID_KETERANGAN_TAMBAHAN;FG_UANG_MUKA;UANG_MUKA_DPP;UANG_MUKA_PPN;UANG_MUKA_PPNBM;REFERENSI")
            dataToWriteK.WriteLine("LT;NPWP;NAMA;JALAN;BLOK;NOMOR;RT;RW;KECAMATAN;KELURAHAN;KABUPATEN;PROPINSI;KODE_POS;NOMOR_TELEPON")
            dataToWriteK.WriteLine("OF;KODE_OBJEK;NAMA;HARGA_SATUAN;JUMLAH_BARANG;HARGA_TOTAL;DISKON;DPP;PPN;TARIF_PPNBM;PPNBM")

            If objRptDs.Tables(0).Rows.Count > 0 Then
                For intCnt = 0 To objRptDs.Tables(0).Rows.Count - 1
                    intUrut = intUrut + 1
                    If Len(Trim(intUrut)) = 1 Then
                        NoUrut = "00" & Trim(intUrut)
                    ElseIf Len(Trim(intUrut)) = 2 Then
                        NoUrut = "0" & Trim(intUrut)
                    Else
                        NoUrut = Trim(intUrut)
                    End If

                    dataToWriteK.WriteLine("""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kd_FK")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("JnsTrx")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPPengganti")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPNo")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("MasaPajak")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("TahunPajak")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPDate")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("CustNPWP")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("CustName")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("CustAddress")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPDPPAmount")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPAmount")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("PPNBM")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("AddNote")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("DPInit")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("DPNetAmount")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("DPPPNAmount")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("DPPPNBM")) & """;""" & _
                         Trim(NoUrut) & """")
                    dataToWriteK.WriteLine("""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kd_LT")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("CompName")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("Jalan")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Blok")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Nomor")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("RT")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("RW")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kecamatan")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kelurahan")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kabupaten")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Propinsi")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("KodePos")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("NoTelp")) & """")
                    dataToWriteK.WriteLine("""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kd_OF")) & """;""" & Trim(NoUrut) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Description")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("Cost")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Unit")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Amount")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("DiscAmount")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("NetAmount")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("PPNAmount")) & """;""" & _
                         Trim(objRptDs.Tables(0).Rows(intCnt).Item("TarifPPNBM")) & """;""" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("PPNBM")) & """")
                Next

                'lblErrMessage.Visible = True
                'lblErrMessage.Text = "OutputVAT File created in " & Trim(MyCSVFileK)

                LinkDownload.Visible = True
                LinkDownload.Text = "Download file VAT_OUT-" & Trim(strCompany) & Trim(strPeriod) & ".csv"
                LinkDownload.NavigateUrl = "../../../" & strUrl & "VAT_OUT-" & Trim(strCompany) & Trim(strPeriod) & ".csv"
            End If
            dataToWriteK.Close()
        End If

        'BindGrid()
    End Sub

    Sub CancelBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdGen As String = "TX_CLSTRX_PPNKELUARAN_UNPOSTED"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim postAccMonth As String
        Dim postAccYear As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            postAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
            lblErrMesage.Text = "Please select only 1 account period to proceed this cancellation."
            lblErrMesage.Visible = True
            Exit Sub
        Else
            postAccMonth = lstAccMonth.SelectedItem.Value
        End If

        postAccYear = lstAccYear.SelectedItem.Value

        Dim intInputPeriod As Integer = (CInt(postAccYear) * 100) + CInt(postAccMonth)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intSelPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "Invalid posting period."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "Invalid posting period."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "This period already locked."
                Exit Sub
            End If
        End If

        Dim dgLineDeItem As DataGridItem
        Dim intCheck As Integer = 0

        For Each dgLineDeItem In dgLine.Items
            Dim myCheckbox As CheckBox = CType(dgLineDeItem.Cells(8).Controls(1), CheckBox)
            If myCheckbox.Checked = True Then
                intCheck = intCheck + 1
            End If
        Next

        If intCheck > 0 Then
            strParamName = "POSTACCMONTH|POSTACCYEAR|LOCCODE|UPDATEID"
            strParamValue = postAccMonth & "|" & postAccYear & "|" & strLocation & "|" & strUserId

            Try
                intErrNo = objGLTrx.mtdGetDataCommon(strOpCdGen, _
                                                   strParamName, _
                                                   strParamValue, _
                                                   ObjTaxDs)

            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PERIOD POSTING&errmesg=" & Exp.ToString() & "&redirect=pu/trx/AP_trx_InvoiceRcvList")
            End Try

            lblErrMesage.Visible = True
            lblErrMesage.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))
        Else
            lblErrMesage.Visible = True
            lblErrMesage.Text = "Cannot found any data to cancelled!"
            Exit Sub
        End If

        GetPPNPosted()
        BindGrid()
    End Sub

    Sub chkSelected_Changed(ByVal Sender As Object, ByVal E As EventArgs)
        Dim dgLineDeItem As DataGridItem
        Dim TtlDPPAmt As Double
        Dim TtlPPNCreditedAmt As Double
        Dim TtlPPNCreditedNotAmt As Double

        Dim strTrxID As String
        Dim strTrxLnID As String
        Dim strDocID As String
        Dim strFPNo As String
        Dim blnCheck As Integer
        Dim strPostAccMonth As String
        Dim strPostAccYear As String
        Dim strOpCd_GET As String = "TX_CLSTRX_PPNKELUARAN_UPDATE"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strPostAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
            lblErrMesage.Text = "Please select only 1 account period to proceed this ppn printing."
            lblErrMesage.Visible = True
            Exit Sub
        Else
            strPostAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strPostAccYear = lstAccYear.SelectedItem.Value

        For Each dgLineDeItem In dgLine.Items
            Dim myCheckbox As CheckBox = CType(dgLineDeItem.Cells(8).Controls(1), CheckBox)
            If myCheckbox.Checked = False Then
                blnCheck = 1
            Else
                blnCheck = 2
            End If

            Dim myLabel3 As Label = CType(dgLineDeItem.Cells(9).Controls(1), Label)
            strTrxID = myLabel3.Text
            Dim myLabel4 As Label = CType(dgLineDeItem.Cells(10).Controls(1), Label)
            strTrxLnID = myLabel4.Text
            Dim myLabel5 As Label = CType(dgLineDeItem.Cells(0).Controls(1), Label)
            strDocID = myLabel5.Text
            Dim myLabel6 As Label = CType(dgLineDeItem.Cells(4).Controls(1), Label)
            strFPNo = myLabel6.Text

            strParamName = "LOCCODE|TRXID|TRXLNID|DOCID|FPNO|ISCHECK|UPDATEID|POSTACCMONTH|POSTACCYEAR"
            strParamValue = strLocation & "|" & Trim(strTrxID) & "|" & Trim(strTrxLnID) & "|" & Trim(strDocID) & "|" & Trim(strFPNo) & "|" & _
                            blnCheck & "|" & Trim(strUserId) & "|" & _
                            IIf(blnCheck = 1, "", strPostAccMonth) & "|" & IIf(blnCheck = 1, "", strPostAccYear)

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_GET, _
                                                      strParamName, _
                                                      strParamValue)


            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CP_CLSTRX_REKONSILEDTL_ADD&errmesg=" & lblErrMessage.Text & "&redirect=CB/trx/CB_trx_Rekonsilelist")
            End Try
        Next

        BindGrid()
    End Sub
End Class
