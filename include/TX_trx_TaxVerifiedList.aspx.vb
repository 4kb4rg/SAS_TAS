
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


Public Class TX_trx_TaxVerifiedList : Inherits Page

    Protected WithEvents lblErrMesage As Label
    Protected WithEvents dgLine As DataGrid
    Protected WithEvents lbDelete As LinkButton
    Protected WithEvents lblTracker As Label
    Protected WithEvents ddlTaxObjectGrp As DropDownList
    Protected WithEvents lstDropList As DropDownList
    'Protected WithEvents srchTaxObjectCode As TextBox
    Protected WithEvents srchDocID As TextBox
    Protected WithEvents srchSupplier As TextBox
    Protected WithEvents srchActCode As TextBox
    Protected WithEvents txtLastUpdate As TextBox
    Protected WithEvents SortExpression As Label
    Protected WithEvents SortCol As Label
    Protected WithEvents lblErrMessage As Label
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents intRec As HtmlInputHidden
    Protected WithEvents lstKPPInit As DropDownList
    Protected WithEvents Generate As ImageButton
    Protected WithEvents PrintDoc As ImageButton
    Protected WithEvents ddlKPP As DropDownList
    Protected WithEvents hidTaxStatus As HtmlInputHidden

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

            Generate.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(Generate).ToString())

            If Not Page.IsPostBack Then
                lblErrMesage.Visible = False

                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                GetVerifiedPosted()
                If hidTaxStatus.Value <> objCBTrx.EnumTaxStatus.Closed Then
                    GenerateDummy()
                End If

                BindCompKPP()
                BindAccCodeDropList("")
                BindGrid()
                BindPageList()

                Generate.Visible = True
                Generate.Attributes("onclick") = "javascript:return ConfirmAction('generate auto number now');"
            End If
        End If
    End Sub
	
	Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
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

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblTrxID")
            If Trim(lbl.Text) = "" Then
                lbButton = dgLine.Items.Item(intCnt).FindControl("lbPrint")
                lbButton.Visible = False
            ElseIf Right(Trim(lbl.Text), 4) = "XXXX" Then
                lbButton = dgLine.Items.Item(intCnt).FindControl("lbPrint")
                lbButton.Visible = False
            Else
                lbButton = dgLine.Items.Item(intCnt).FindControl("lbPrint")
                lbButton.Visible = True
            End If

            'Select Case intRec.Value
            '    Case 0
            '        lbButton = dgLine.Items.Item(intCnt).FindControl("lbEdit")
            '        lbButton.Visible = True
            '        lbButton = dgLine.Items.Item(intCnt).FindControl("lbUpdate")
            '        lbButton.Visible = False
            '        lbButton = dgLine.Items.Item(intCnt).FindControl("lbCancel")
            '        lbButton.Visible = False
            '    Case 1
            '        lbButton = dgLine.Items.Item(intCnt).FindControl("lbEdit")
            '        lbButton.Visible = False
            '        lbButton = dgLine.Items.Item(intCnt).FindControl("lbUpdate")
            '        lbButton.Visible = True
            '        lbButton = dgLine.Items.Item(intCnt).FindControl("lbCancel")
            '        lbButton.Visible = True
            '    Case 2
            '        intRec.Value = 0
            'End Select
        Next
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
        strSearch = IIf(Trim(ddlKPP.SelectedItem.Value) = "", "", "KPPInit Like '%" & Trim(ddlKPP.SelectedItem.Value) & "%' AND ")
        strSearch = strSearch + IIf(ddlTaxObjectGrp.SelectedItem.Value = "", "", "AccCode = '" & Trim(ddlTaxObjectGrp.SelectedItem.Value) & "' AND ")
        strSearch = strSearch + IIf(Trim(srchDocID.Text) = "", "", "A.DocID LIKE '%" & Trim(srchDocID.Text) & "%' AND ")
        strSearch = strSearch + IIf(Trim(srchSupplier.Text) = "", "", "(A.SupplierCode LIKE '%" & Trim(srchSupplier.Text) & "%' OR A.FromTo LIKE '%" & Trim(srchSupplier.Text) & "%') AND ")

        If (Trim(strSearch) <> "") Then
            strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If

        If hidTaxStatus.Value <> objCBTrx.EnumTaxStatus.Closed Then
            strOrderBy = IIf(Trim(SortExpression.Text) = "", "ORDER BY A.TaxInit, A.KPPInit, A.DocDate, A.DocID ASC", "Order By " & Trim(SortExpression.Text) & " ")
            strOrderBy = strOrderBy + IIf(Trim(SortExpression.Text) = "", "", IIf(Trim(SortCol.Text) = "", "ASC ", Trim(SortCol.Text)))

            strOpCd_GET = "TX_CLSTRX_TAXVERIFIEDCONFIRMED_LIST_GET"
        Else
            strOrderBy = IIf(Trim(SortExpression.Text) = "", "ORDER BY A.TaxInit, A.KPPInit, A.DocDate, A.DocID ASC", "Order By " & Trim(SortExpression.Text) & " ")
            strOrderBy = strOrderBy + IIf(Trim(SortExpression.Text) = "", "", IIf(Trim(SortCol.Text) = "", "ASC ", Trim(SortCol.Text)))

            strOpCd_GET = "TX_CLSTRX_TAXVERIFIEDPOSTED_LIST_GET"
        End If

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

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            If Right(Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("TrxID")), 4) = "XXXX" Then
                xCnt = xCnt + 1
            End If
        Next

        If xCnt > 0 Then
            PrintDoc.Enabled = False
            lblErrMesage.Visible = True
            lblErrMesage.Text = "Please generate auto number first before print."
        Else
            PrintDoc.Enabled = True
        End If
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
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocLnID")
        strSelDocLnID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxStatus")
        strSelTaxStatus = lblDelText.Text

        Response.Write("<Script Language=""JavaScript"">window.open(""TX_trx_TaxVerificationDet.aspx?DocID=" & Trim(strSelDocID) & _
                    "&TaxInit=" & strSelTaxInit & _
                    "&TaxStatus=" & strSelTaxStatus & _
                    "&DocLnID=" & strSelDocLnID & _
                    "&AccMonth=" & Trim(strSelAccMonth) & _
                    "&AccYear=" & Trim(strSelAccYear) & _
                    """,null ,""status=yes, height=575, width=1000, top=60, left=120, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        BindGrid()

        'Dim lblDelText As Label
        'Dim strSelTaxID As String
        'Dim strSelStatus As String
        'Dim strSelOriDoc As String
        'Dim strSelDocType As String
        'Dim strReportName As String

        'dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        'lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocID")
        'strSelTaxID = lblDelText.Text
        'lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxStatus")
        'strSelStatus = lblDelText.Text
        'lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblOriDoc")
        'strSelOriDoc = lblDelText.Text
        'lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocType")
        'strSelDocType = lblDelText.Text


        'strReportName = "CB_RPT_CashBankPrint"
        'If UCase(Trim(strSelOriDoc)) = "CASHBANK" Then
        '    Response.Write("<Script Language=""JavaScript"">window.open(""../../CB/reports/CB_Rpt_CashBankPrint.aspx?strId=" & strSelTaxID & _
        '              "&strPrintDate=" & Now() & _
        '              "&strStatus=" & strSelStatus & _
        '              "&CBType=" & strSelDocType & _
        '              "&strSortLine=" & "" & _
        '              "&strAccountTag=" & "" & _
        '              "&strVehTag=" & "" & _
        '              "&strVehExpTag=" & "" & _
        '              "&strBlockTag=" & "" & _
        '              "&strReportName=" & strReportName & _
        '              """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        'ElseIf UCase(Trim(strSelOriDoc)) = "PAYMENT" Then
        '    Response.Write("<Script Language=""JavaScript"">window.open(""../../CB/reports/CB_Rpt_PaymentDet.aspx?strPayId=" & strSelTaxID & _
        '               "&strPrintDate=" & Now() & _
        '               "&strStatus=" & strSelStatus & _
        '               "&strSortLine=" & "" & _
        '               "&strAccountTag=" & "" & _
        '               """, null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        'Else
        '    Response.Write("<Script Language=""JavaScript"">window.open(""../../GL/reports/GL_Rpt_Journal_Details.aspx?Type=Print&CompName=" & strCompany & _
        '       "&Location=" & strLocation & _
        '       "&TRXID=" & strSelTaxID & _
        '       """,null ,""status=yes, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        'End If
    End Sub

    Sub BindAccCodeDropList(Optional ByVal pv_strAccCode As String = "")
        Dim strOpCd As String = "TX_CLSSETUP_TAXOBJECTRATE_GROUP_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet
        Dim intCnt As Integer = 0

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

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            If Trim(ObjTaxDs.Tables(0).Rows(intCnt).Item("AccCode")) = Trim(pv_strAccCode) Then
                intSelectedIndex = intCnt + 1
            End If
        Next

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

    Sub OnCommand(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strCmdArgs As String = E.CommandArgument
        Dim strInit As String = strCmdArgs
        Dim lblDelText As Label
        Dim strSelAccCode As String
        Dim strSelTrxID As String
        Dim strSelDocID As String
        Dim strSelDocDate As String
        Dim strSelTaxID As String
        Dim strSelTaxInit As String
        Dim strSelSupplierCode As String
        Dim strSelSupplierName As String
        Dim strSelDPPAmount As String
        Dim strSelTaxAmount As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String

        dgLine.EditItemIndex = CInt(E.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAccCode")
        strSelAccCode = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTrxID")
        strSelTrxID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblDocID")
        strSelDocID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblDocDate")
        strSelDocDate = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTaxID")
        strSelTaxID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTaxInit")
        strSelTaxInit = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblSupplierCode")
        strSelSupplierCode = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblSupplierName")
        strSelSupplierName = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblDPPAmount")
        strSelDPPAmount = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTaxAmount")
        strSelTaxAmount = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAccMonth")
        strSelAccMonth = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblAccYear")
        strSelAccYear = lblDelText.Text

        If strInit = "Print" Then
            Response.Write("<Script Language=""JavaScript"">window.open(""TX_trx_PrintDocs.aspx?TrxID=" & strSelTrxID & _
                      "&DocID=" & strSelDocID & _
                      "&DocDate=" & strSelDocDate & _
                      "&AccMonth=" & strSelAccMonth & _
                      "&AccYear=" & strSelAccYear & _
                      "&TaxID=" & strSelTaxID & _
                      "&TaxInit=" & strSelTaxInit & _
                      "&SupplierCode=" & strSelSupplierCode & _
                      "&SupplierName=" & strSelSupplierName & _
                      "&DPPAmount=" & strSelDPPAmount & _
                      "&TaxAmount=" & strSelTaxAmount & _
                      "&AccCode=" & strSelAccCode & _
                      "&CompName=" & strCompany & _
                      "&PrintInit=" & "2" & _
                      """,null ,""status=yes, height=500, width=800, top=100, left=220, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
        End If

    End Sub

    Sub DEDR_Edit(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim btn As LinkButton
        Dim lstKPP As DropDownList
        Dim lblDelText As Label

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        BindGrid()
        If CInt(e.Item.ItemIndex) >= dgLine.Items.Count Then
            dgLine.EditItemIndex = -1
            Exit Sub
        End If

        btn = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbEdit")
        btn.Visible = False
        btn = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbUpdate")
        btn.Visible = True
        btn = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbCancel")
        btn.Visible = True
        btn = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lbPrint")
        btn.Visible = False
        lstKPP = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lstKPPInit")
        lstKPP.Visible = True
        BindCompTax(dgLine.EditItemIndex)

        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTrxID")
        lblDelText.Visible = False
        'lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblPrintedBy")
        'lblDelText.Visible = False
    End Sub

    Sub DEDR_Update(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim strOpCd As String = "TX_CLSTRX_TAXVERIFIED_UPDATE"
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim ObjTaxDs As DataSet
        Dim lblDelText As Label
        Dim strSelAccCode As String
        Dim strSelTrxID As String
        Dim strSelDocID As String
        Dim strSelDocLnID As String
        Dim strSelDocDate As String
        Dim strSelTaxID As String
        Dim strSelTaxLnID As String
        Dim strSelTaxInit As String
        Dim strSelSupplierCode As String
        Dim strSelSupplierName As String
        Dim strSelDPPAmount As String
        Dim strSelTaxAmount As String
        Dim strSelAccMonth As String
        Dim strSelAccYear As String
        Dim strSelKPPCode As String
        Dim lstKPP As DropDownList
        Dim strRate As String

        dgLine.EditItemIndex = CInt(e.Item.ItemIndex)
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccCode")
        strSelAccCode = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTrxID")
        strSelTrxID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocID")
        strSelDocID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocLnID")
        strSelDocLnID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDocDate")
        strSelDocDate = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxID")
        strSelTaxID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxLnID")
        strSelTaxLnID = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxInit")
        strSelTaxInit = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblSupplierCode")
        strSelSupplierCode = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblSupplierName")
        strSelSupplierName = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblDPPAmount")
        strSelDPPAmount = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblTaxAmount")
        strSelTaxAmount = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccMonth")
        strSelAccMonth = lblDelText.Text
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblAccYear")
        strSelAccYear = lblDelText.Text
        lstKPP = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lstKPPInit")
        strSelKPPCode = lstKPP.SelectedItem.Value
        lblDelText = dgLine.Items.Item(CInt(e.Item.ItemIndex)).FindControl("lblRate")
        strRate = lblDelText.Text

        strParamName = "LOCCODE|TRXID|DOCID|DOCLNID|DOCDATE|ACCMONTH|ACCYEAR|TAXID|TAXLNID|TAXINIT|KPPCODE|DPPAMOUNT|TAXAMOUNT|SUPPLIERCODE|SUPPLIERNAME|USERID|RATE"
        strParamValue = strLocation & "|" & Trim(strSelTrxID) & "|" & Trim(strSelDocID) & "|" & Trim(strSelDocLnID) & "|" & strSelDocDate & "|" & _
                        strSelAccMonth & "|" & strSelAccYear & "|" & _
                        Trim(strSelTaxID) & "|" & Trim(strSelTaxLnID) & "|" & Trim(strSelTaxInit) & "|" & Trim(strSelKPPCode) & "|" & _
                        strSelDPPAmount & "|" & strSelTaxAmount & "|" & Trim(strSelSupplierCode) & "|" & Trim(strSelSupplierName) & "|" & strUserId & "|" & strRate

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        BindGrid()
    End Sub

    Sub DEDR_Cancel(ByVal Sender As Object, ByVal e As DataGridCommandEventArgs)
        BindGrid()
    End Sub

    Sub BindCompTax(ByVal index As Integer)
        Dim strOpCd As String = "TX_CLSSETUP_COMPTAX_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet

        strParamName = "COMPCODE"
        strParamValue = strCompany

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("KPPCode") = ""
        dr("KPPDescr") = "Please select KPP location"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        lstKPPInit = dgLine.Items.Item(index).FindControl("lstKPPInit")
        lstKPPInit.DataSource = ObjTaxDs.Tables(0)
        lstKPPInit.DataValueField = "KPPCode"
        lstKPPInit.DataTextField = "KPPDescr"
        lstKPPInit.DataBind()
        lstKPPInit.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
        End If
    End Sub

    Sub BindCompKPP()
        Dim strOpCd As String = "TX_CLSSETUP_COMPTAX_GET"
        Dim dr As DataRow
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim ObjTaxDs As DataSet

        strParamName = "COMPCODE"
        strParamValue = strCompany

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectDet&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dr = ObjTaxDs.Tables(0).NewRow()
        dr("KPPInit") = ""
        dr("KPPDescr") = "Please select KPP location"
        ObjTaxDs.Tables(0).Rows.InsertAt(dr, 0)

        ddlKPP.DataSource = ObjTaxDs.Tables(0)
        ddlKPP.DataValueField = "KPPInit"
        ddlKPP.DataTextField = "KPPDescr"
        ddlKPP.DataBind()
        ddlKPP.SelectedIndex = intSelectedIndex

        If Not ObjTaxDs Is Nothing Then
            ObjTaxDs = Nothing
        End If
    End Sub

    Sub GetVerifiedPosted()
        Dim strOpCd_GET As String = "TX_CLSTRX_TAXVERIFIEDPOSTED_LIST_GET"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        strAccMonth = lstAccMonth.SelectedItem.Value
        strAccYear = lstAccYear.SelectedItem.Value

        strParamName = "LOCCODE|ACCYEAR|ACCMONTH|STRSEARCH|ORDERBY"
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
            hidTaxStatus.Value = objCBTrx.EnumTaxStatus.Closed
        End If
    End Sub

    Sub GenerateDummy()
        Dim strOpCd_UPD As String = "TX_CLSTRX_TAXVERIFICATION_GENERATE_DUMMY_TRXID"
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
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

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|USERID"
        strParamValue = strLocation & "|" & _
                        genAccMonth & "|" & _
                        gentAccYear & "|" & _
                        strUserId

        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd_UPD, _
                                                    strParamName, _
                                                    strParamValue)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_DEL&errmesg=" & lblErrMessage.Text & "&redirect=gl/setup/TX_Setup_TaxObjectList.aspx")
        End Try
    End Sub

    Sub GenerateNumber_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
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


        GetVerifiedPosted()
        If hidTaxStatus.Value <> objCBTrx.EnumTaxStatus.Closed Then
            'Generate auto number, run dummy first
            GenerateDummy()
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
        Else
            lblErrMesage.Visible = True
            lblErrMesage.Text = "Data Already Posted."
            Exit Sub
        End If
        
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

    Sub PrintDoc_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Write("<Script Language=""JavaScript"">window.open(""TX_trx_PrintDocs.aspx?TrxID=" & "" & _
                            "&DocID=" & "" & _
                            "&DocDate=" & "" & _
                            "&AccMonth=" & lstAccMonth.SelectedItem.Value & _
                            "&AccYear=" & lstAccYear.SelectedItem.Value & _
                            "&TaxID=" & "" & _
                            "&TaxInit=" & "" & _
                            "&SupplierCode=" & "" & _
                            "&SupplierName=" & "" & _
                            "&DPPAmount=" & "" & _
                            "&TaxAmount=" & "" & _
                            "&AccCode=" & "" & _
                            "&CompName=" & strCompany & _
                            "&PrintInit=" & "1" & _
                            """,null ,""status=yes, height=530, width=900, top=90, left=200, resizable=yes, scrollbars=yes, toolbar=no, location=no"");</Script>")
    End Sub

	Sub SetNoBukti_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		Dim strOpCd As String = "TX_CLSTRX_TAXVERIFIED_UPDATE_NOPOT"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strDOCID As String = ""
		Dim strDOCIDLN As String = ""
		Dim strKPP  As String = ""
		Dim strLoc  As String = ""
        Dim bUpdate As Boolean = False
		Dim NewNobukti As String = ""
		Dim genAccMonth As String
        Dim gentAccYear As String

		Dim btn As Button = CType(sender, Button)
		Dim txt As TextBox 
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)
		
		genAccMonth = lstAccMonth.SelectedItem.Value
        gentAccYear = lstAccYear.SelectedItem.Value

		Dim intInputPeriod As Integer = (CInt(gentAccYear) * 100) + CInt(genAccMonth)
        Dim intCurPeriod As Integer = (CInt(strAccYear) * 100) + CInt(strAccMonth)
        Dim intSelPeriod As Integer = (CInt(strSelAccYear) * 100) + CInt(strSelAccMonth)

        'If Session("SS_FILTERPERIOD") = "0" Then
        '    If intCurPeriod < intSelPeriod Then
        '        lblErrMesage.Visible = True
        '        lblErrMesage.Text = "Invalid generate period."
        '        Exit Sub
        '    End If
        'Else
        '    If intSelPeriod <> intInputPeriod Then
        '        lblErrMesage.Visible = True
        '        lblErrMesage.Text = "Invalid generate period."
        '        Exit Sub
        '    End If
        '    If intSelPeriod < intCurPeriod Then
        '        lblErrMesage.Visible = True
        '        lblErrMesage.Text = "This period already locked."
        '        Exit Sub
        '    End If
        'End If
		
        strDOCID = CType(dgItem.Cells(0).FindControl("lblDocID"), Label).Text
		strDOCIDLN = CType(dgItem.Cells(0).FindControl("lblDocLnID"), Label).Text
		NewNobukti = CType(dgItem.Cells(0).FindControl("txtTrxID"), TextBox ).Text
        strKPP = CType(dgItem.Cells(0).FindControl("lblKPPInit"), Label).Text
	    strLoc = CType(dgItem.Cells(0).FindControl("lblLocCode"), Label).Text
	    
	   
        strParamName = "NBPOT|DOCID|DOCLNID|UI|LOC|KPP"
        strParamValue = trim(NewNobukti) & "|" & Trim(strDOCID) & "|" & Trim(strDOCIDLN) & "|" & strUserId  & "|" & Trim(strLoc) & "|" &  Trim(strKPP)

       
        Try
            intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
            UserMsgBox(Me, "Set No.Bukti : " & trim(NewNobukti) & " DocID:" & trim(strDOCID) & " sukses ")
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text )
        End Try

				
        BindGrid()
	End Sub
	
	Sub ConfNoBukti_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		Dim strOpCd As String = "WM_WM_CLSTRX_TICKET_FFB_UPD_STATUS"
        Dim intErrNo As Integer
        Dim strParamName As String = ""
        Dim strParamValue As String = ""
        Dim strSPB As String = ""
        Dim bUpdate As Boolean = False

		Dim btn As Button = CType(sender, Button)
        Dim dgItem As DataGridItem = CType(btn.NamingContainer, DataGridItem)

        strSPB = CType(dgItem.Cells(0).FindControl("lblvticket"), Label).Text
       
        'strParamName = "ST|UI|SEARCH"
        'strParamValue = "2" & "|" & strUserId & "|SPBCode='" & Trim(strSPB) & "'"  

        'bUpdate = False
        'Try
        '    intErrNo = objOk.mtdInsertDataCommon(strOpCd, strParamName, strParamValue)
        '    bUpdate = True
        'Catch Exp As System.Exception
        '    Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=ADD_DETAIL&errmesg=" & lblErrMessage.Text & "&redirect=IN/trx/IN_PurReq.aspx")
        'End Try

		'If bUpdate = True Then
        '    UserMsgBox(Me, strParamValue)
        'End IF
		
        'BindGrid()
	End Sub
	
	
	
	End Class
