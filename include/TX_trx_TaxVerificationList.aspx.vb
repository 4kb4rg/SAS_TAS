
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


Public Class TX_trx_TaxVerificationList : Inherits Page

    Protected WithEvents lblErrMesage As Label
    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents ddlTaxObjectGrp As DropDownList
    Protected WithEvents lstDropList As DropDownList
    Protected WithEvents srchTaxObjectCode As TextBox
    Protected WithEvents srchDocID As TextBox
    Protected WithEvents srchSupplier As TextBox
    Protected WithEvents srchActCode As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents ddlPrintOpt As DropDownList
    Protected WithEvents PrintOpt As HtmlTableRow
    Protected WithEvents VerifiedBtn As ImageButton
    Protected WithEvents PrintDoc As ImageButton
    Protected WithEvents ddlTaxStatus As DropDownList

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objCTSetup As New agri.CT.clsSetup()
    Protected objCBTrx As New agri.CB.clsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSys As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
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

    Dim strTaxObjectCodeTag As String
    Dim strDescTag As String
    Dim strActCodeTag As String
    Dim strTitleTag As String
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim strLocType As String
    Dim strSelAccMonth As String
    Dim strSelAccYear As String


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

            VerifiedBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(VerifiedBtn).ToString())

            lblErrMesage.Visible = False

            If Not Page.IsPostBack Then
                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindAccCodeDropList("")
                BindGrid()
                BindPageList()
                VerifiedBtn.Attributes("onclick") = "javascript:return ConfirmAction('verified transactions on this period (" & Trim(lstAccMonth.SelectedValue) & "/" & Trim(lstAccYear.SelectedValue) & ") ');"
            End If
        End If
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
        Dim lblDocID As Label
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

        'For intCnt = 0 To dgLine.Items.Count - 1
        '    lbl = dgLine.Items.Item(intCnt).FindControl("lblTaxStatus")
        '    Select Case CInt(Trim(lbl.Text))
        '        Case 1
        '            lbButton = dgLine.Items.Item(intCnt).FindControl("lbVerified")
        '            lbButton.Visible = True
        '            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('verified');"
        '            lbButton = dgLine.Items.Item(intCnt).FindControl("lbUnverified")
        '            lbButton.Visible = False
        '        Case 2
        '            lbButton = dgLine.Items.Item(intCnt).FindControl("lbVerified")
        '            lbButton.Visible = False
        '            lbButton = dgLine.Items.Item(intCnt).FindControl("lbUnverified")
        '            lbButton.Visible = True
        '            lbButton.Attributes("onclick") = "javascript:return ConfirmAction('cancel verification');"
        '    End Select

        '    lblDocID = dgLine.Items.Item(intCnt).FindControl("lblDocID")
        '    If Left(Trim(lblDocID.Text), 3) <> "XXX" Then
        '        lbButton = dgLine.Items.Item(intCnt).FindControl("lbVerified")
        '        lbButton.Visible = False
        '        lbButton = dgLine.Items.Item(intCnt).FindControl("lbUnverified")
        '        lbButton.Visible = False
        '    End If
        'Next
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
        Dim strOrderBy As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strSearch = IIf(Trim(srchTaxObjectCode.Text) = "", "", "TaxObject Like '%" & Trim(srchTaxObjectCode.Text) & "%' AND ")
        strSearch = strSearch + IIf(ddlTaxObjectGrp.SelectedItem.Value = "", "", "AccCode = '" & Trim(ddlTaxObjectGrp.SelectedItem.Value) & "' AND ")
        strSearch = strSearch + IIf(Trim(srchDocID.Text) = "", "", "DocID LIKE '%" & Trim(srchDocID.Text) & "%' AND ")
        strSearch = strSearch + IIf(Trim(srchSupplier.Text) = "", "", "(SupplierCode LIKE '%" & Trim(srchSupplier.Text) & "%' OR FromTo LIKE '%" & Trim(srchSupplier.Text) & "%') AND ")
        strSearch = strSearch + "TaxStatus = '" & Trim(ddlTaxStatus.SelectedItem.Value) & "' AND "

        If (Trim(strSearch) <> "") Then
            strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If

        strOrderBy = IIf(Trim(SortExpression.Text) = "", "", "Order By " & Trim(SortExpression.Text) & " ")
        strOrderBy = strOrderBy + IIf(Trim(SortExpression.Text) = "", "", IIf(Trim(SortCol.Text) = "", "ASC ", Trim(SortCol.Text)))

        strOpCd_GET = "TX_CLSTRX_TAXVERIFIEDNEED_LIST_GET"
        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|STRSEARCH|ORDERBY"
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

    Sub PrintPreview(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strSelTaxID As String
        Dim strSelStatus As String
        Dim strSelOriDoc As String
        Dim strSelDocType As String
        Dim strReportName As String

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocID")
        strSelTaxID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxStatus")
        strSelStatus = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblOriDoc")
        strSelOriDoc = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocType")
        strSelDocType = lblDelText.Text


        strReportName = "CB_RPT_CashBankPrint"
        If UCase(Trim(strSelOriDoc)) = "CASHBANK" Then
            Response.Write("<Script Language=""JavaScript"">window.open(""../../CB/reports/CB_Rpt_CashBankPrint.aspx?strId=" & strSelTaxID & _
                      "&strPrintDate=" & Now() & _
                      "&strStatus=" & strSelStatus & _
                      "&CBType=" & strSelDocType & _
                      "&strSortLine=" & "" & _
                      "&strAccountTag=" & "" & _
                      "&strVehTag=" & "" & _
                      "&strVehExpTag=" & "" & _
                      "&strBlockTag=" & "" & _
                      "&strReportName=" & strReportName & _
                      """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        ElseIf UCase(Trim(strSelOriDoc)) = "PAYMENT" Then
            Response.Write("<Script Language=""JavaScript"">window.open(""../../CB/reports/CB_Rpt_PaymentDet.aspx?strPayId=" & strSelTaxID & _
                       "&strPrintDate=" & Now() & _
                       "&strStatus=" & strSelStatus & _
                       "&strSortLine=" & "" & _
                       "&strAccountTag=" & "" & _
                       """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strOpCd_UPD As String = "TX_CLSTRX_TAXVERIFIEDNEED_LIST_UPDATE"
        Dim strSelTaxID As String
        Dim strSelStatus As String
        Dim strSelOriDoc As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocID")
        strSelTaxID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxStatus")
        strSelStatus = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblOriDoc")
        strSelOriDoc = lblDelText.Text

        strParamName = "LOCCODE|DOCID|ORIDOC|TAXSTATUS|UPDATEID"
        strParamValue = strLocation & "|" & Trim(strSelTaxID) & "|" & _
                        UCase(Trim(strSelOriDoc)) & "|" & _
                        objCBTrx.EnumTaxStatus.Unverified & "|" & _
                        strUserId
        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_UPD, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/TX_Setup_TaxObjectList.aspx")
        End Try

        BindGrid()
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strOpCd_UPD As String = "TX_CLSTRX_TAXVERIFIEDNEED_LIST_UPDATE"
        Dim strSelDocID As String
        Dim strSelTaxStatus As String
        Dim strSelOriDoc As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocID")
        strSelDocID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxStatus")
        strSelTaxStatus = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblOriDoc")
        strSelOriDoc = lblDelText.Text

        strParamName = "LOCCODE|DOCID|ORIDOC|TAXSTATUS|UPDATEID"
        strParamValue = strLocation & "|" & Trim(strSelDocID) & "|" & _
                        UCase(Trim(strSelOriDoc)) & "|" & _
                        objCBTrx.EnumTaxStatus.Verified & "|" & _
                        strUserId

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_UPD, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/TX_Setup_TaxObjectList.aspx")
        End Try

        BindGrid()
    End Sub

    Sub DEDR_Preview(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblDelText As Label
        Dim strSelDocID As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strSelTaxInit As String
        Dim strSelDocLnID As String
        Dim strSelTaxStatus As String

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocID")
        strSelDocID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccMonth")
        strSelAccMonth = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccYear")
        strSelAccYear = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxInit")
        strSelTaxInit = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxStatus")
        strSelTaxStatus = lblDelText.Text
        strSelDocLnID = ""

        Response.Write("<Script Language=""JavaScript"">window.open(""TX_trx_TaxVerificationDet.aspx?DocID=" & Trim(strSelDocID) & _
                    "&TaxInit=" & strSelTaxInit & _
                    "&TaxStatus=" & strSelTaxStatus & _
                    "&DocLnID=" & strSelDocLnID & _
                    "&AccMonth=" & Trim(strSelAccMonth) & _
                    "&AccYear=" & Trim(strSelAccYear) & _
                    """,null ,""status=yes, height=575, width=1000, top=60, left=120, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")

        BindGrid()
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATE_GROUP_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet

        strParamName = "STRSEARCH"
        strParamValue = "WHERE AccCode in (SELECT AccCode FROM GL_Account WHERE LocCode='" & strLocation & "')"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("AccCode") = ""
        dr("Description") = "Please select tax group"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlTaxObjectGrp.DataSource = ObjTaxDs.Tables(0)
        ddlTaxObjectGrp.DataValueField = "AccCode"
        ddlTaxObjectGrp.DataTextField = "Description"
        ddlTaxObjectGrp.DataBind()
        ddlTaxObjectGrp.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
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

    Sub VerifiedBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd As String = "TX_CLSTRX_TAXVERIFIEDNEED_PERIOD_POSTING"
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

        strParamName = "ACCMONTH|ACCYEAR|LOCCODE|USERID"
        strParamValue = postAccMonth & "|" & postAccYear & "|" & strLocation & "|" & strUserId

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PERIOD POSTING&errmesg=" & Exp.ToString() & "&redirect=pu/trx/AP_trx_InvoiceRcvList")
        End Try

        BindGrid()
    End Sub

    Sub PrintDoc_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Write("<Script Language=""JavaScript"">window.open(""../reports/TX_Rpt_TaxReports.aspx?strTrxID=" & "XXX" & _
               "&strDocID=" & "" & _
               "&strSupplierCode=" & "" & _
               "&strCreateDate=" & Now() & _
               "&strAccMonth=" & lstAccMonth.SelectedItem.Value & _
               "&strAccYear=" & lstAccYear.SelectedItem.Value & _
               "&strPrintOpt=" & "4" & _
               "&strAccCode=" & "" & _
               "&strSPTRev=" & "" & _
               "&strQtySPP1=" & "" & _
               "&strQtySPP2=" & "" & _
               "&strQtySPP3=" & "" & _
               "&strQtyBuktiPtg=" & "" & _
               "&strCompNPWPNo=" & "" & _
               "&strCompNPWPLoc=" & "" & _
               "&strTaxInit=" & "" & _
               "&strPelapor=" & "" & _
               "&strPelaporNPWP=" & "" & _
               "&strPelapor2=" & "" & _
               """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub
End Class
