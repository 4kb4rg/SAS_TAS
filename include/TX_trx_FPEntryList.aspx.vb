
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


Public Class TX_trx_FPEntryList : Inherits Page

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
    Protected WithEvents PrintDoc As ImageButton
    Protected WithEvents hidTaxStatus As HtmlInputHidden

    Protected WithEvents ddlVATType As DropDownList

    Protected WithEvents GenerateBtn As ImageButton
    Protected WithEvents PostingBtn As ImageButton

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objCTSetup As New agri.CT.clsSetup()
    Protected objCBTrx As New agri.CB.clsTrx()

    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
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

            If Not Page.IsPostBack Then
                lblErrMesage.Visible = False

                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindGrid()
                BindPageList()
            End If

            If ddlVATType.SelectedItem.Value = "2" Then
                GenerateBtn.Visible = False
                PostingBtn.Visible = False
            Else
                GenerateBtn.Visible = False
                PostingBtn.Visible = False
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

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblFPNo")
            If Trim(lbl.Text) = "" Then
                lbl = dgLine.Items.Item(intCnt).FindControl("lblFPDate")
                lbl.Visible = False
            End If
            lbl = dgLine.Items.Item(intCnt).FindControl("lblStatus")
            If Trim(lbl.Text) = "1" Then
                lbButton = dgLine.Items.Item(intCnt).FindControl("lbUpdate")
                lbButton.Visible = True
            End If
        Next

        PageNo = dgLine.CurrentPageIndex + 1
        lblTracker.Text = "Page " & PageNo & " of " & dgLine.PageCount
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

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        strAccYear = lstAccYear.SelectedItem.Value
        strSearch = IIf(Trim(srchFPNo.Text) = "", "", "B.FPNo Like '%" & Trim(srchFPNo.Text) & "%' AND ")
        strSearch = strSearch + IIf(Trim(srchDocID.Text) = "", "", "A.DocID LIKE '%" & Trim(srchDocID.Text) & "%' AND ")
        strSearch = strSearch + IIf(Trim(srchSupplier.Text) = "", "", "(B.SupplierCode LIKE '%" & Trim(srchSupplier.Text) & "%' OR B.SupplierName LIKE '%" & Trim(srchSupplier.Text) & "%') AND ")
        strSearch = strSearch + IIf(Trim(ddlVATType.SelectedItem.Value) = "", "", "B.VATType = '" & Trim(ddlVATType.SelectedItem.Value) & "' AND ")


        If (Trim(strSearch) <> "") Then
            strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If

        strOrderBy = IIf(Trim(SortExpression.Text) = "", "ORDER BY OriDoc, A.DocID ASC", "Order By " & Trim(SortExpression.Text) & " ")
        strOrderBy = strOrderBy + IIf(Trim(SortExpression.Text) = "", "", IIf(Trim(SortCol.Text) = "", "ASC ", Trim(SortCol.Text)))

        strOpCd_GET = "TX_CLSTRX_VAT_LIST"
        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|UPDATEID|STRSEARCH|ORDERBY"
        strParamValue = strLocation & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "|" & _
                        strUserId & "|" & _
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
        Dim strSelDocLnID As String
        Dim strVATType As String

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
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocLnID")
        strSelDocLnID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblVATType")
        strVATType = lblDelText.Text

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
                    "&DocLnID=" & Trim(strSelDocLnID) & _
                    "&VATType=" & Trim(strVATType) & _
                    """,null ,""status=yes, height=635, width=1000, top=60, left=120, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        BindGrid()
    End Sub

    Sub DEDR_FPUpdate(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
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
        Dim strSelDocLnID As String
        Dim strVATType As String
        Dim strOpCd_GET As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim intCnt As Integer
        Dim xCnt As Integer = 0
        Dim strOrderBy As String

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
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocLnID")
        strSelDocLnID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblVATType")
        strVATType = lblDelText.Text

        strOpCd_GET = "TX_CLSTRX_VAT_UPDATE"
        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|UPDATEID|STATUS|TRXID"
        strParamValue = strLocation & "|" & _
                        strAccYear & "|" & _
                        strAccMonth & "|" & _
                        strUserId & "|" & _
                        objCBTrx.EnumTaxStatus.Verified & "|" & _
                        strSelTrxID

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        intRec.Value = 0
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

    Sub GenerateTaxNumber_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdGen As String = "TX_CLSTRX_TAXVERIFICATION_GENERATE_AUTO_TRXID"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet
        Dim genAccMonth As String
        Dim gentAccYear As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            genAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
            lblErrMesage.Text = "Please select only 1 account period to proceed this posting."
            lblErrMesage.Visible = True
            Exit Sub
        Else
            genAccMonth = lstAccMonth.SelectedItem.Value
        End If

        gentAccYear = lstAccYear.SelectedItem.Value

        Dim intInputPeriod As Integer = (CInt(gentAccYear) * 100) + CInt(genAccMonth)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        If Session("SS_FILTERPERIOD") = "0" Then
            If intCurPeriod < intSelPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "Invalid generate period."
                Exit Sub
            End If
        Else
            If intSelPeriod <> intInputPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "Invalid generate period."
                Exit Sub
            End If
            If intSelPeriod < intCurPeriod Then
                lblErrMesage.Visible = True
                lblErrMesage.Text = "This period already locked."
                Exit Sub
            End If
        End If

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID"
        strParamValue = strLocation & "|" & genAccMonth & "|" & gentAccYear & "|" & strUserId

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCdGen, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BindGrid()
        lblErrMesage.Visible = True
        lblErrMesage.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))

    End Sub

    Sub PostingBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCdGen As String = "TX_CLSTRX_TAXVERIFICATION_GENERATE_POSTING"
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
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCdGen, _
                                               strParamName, _
                                               strParamValue, _
                                               ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PERIOD POSTING&errmesg=" & Exp.ToString() & "&redirect=pu/trx/AP_trx_InvoiceRcvList")
        End Try

        lblErrMesage.Visible = True
        lblErrMesage.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))

        BindGrid()
    End Sub

End Class
