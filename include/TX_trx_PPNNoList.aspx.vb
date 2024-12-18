
Imports System
Imports System.Data
Imports System.Collections 
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.Page
Imports System.Web.UI.Control
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.Strings
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Interaction
Imports agri.GlobalHdl.clsGlobalHdl
Imports agri.PWSystem.clsLangCap
 

Public Class TX_trx_PPNNoList : Inherits Page

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
    Protected WithEvents lblErrMessageNo As Label
    Protected WithEvents lstAccMonth As DropDownList
    Protected WithEvents lstAccYear As DropDownList
    Protected WithEvents intRec As HtmlInputHidden
    Protected WithEvents PrintDoc As ImageButton
    Protected WithEvents hidStatus As HtmlInputHidden

    Protected WithEvents ddlVATType As DropDownList

    Protected WithEvents ddlTaxMonth As DropDownList
    Protected WithEvents ddlTaxYear As DropDownList
    Protected WithEvents txtFPDate As TextBox
    Protected WithEvents lblDate As Label
    Protected WithEvents lblFmt As Label
    Protected WithEvents txtTaxYear As TextBox
    Protected WithEvents ddlTaxTrx As DropDownList
    Protected WithEvents ddlTaxStatus As DropDownList
    Protected WithEvents txtTaxBranch As TextBox
    Protected WithEvents ddlTaxBranch As DropDownList
    Protected WithEvents txtTaxNoFrom As TextBox
    Protected WithEvents txtTaxNoTo As TextBox
    Protected WithEvents dgLineGen As DataGrid
    Protected WithEvents lblErrTaxYear As Label
    Protected WithEvents lblErrSequence As Label

    Protected WithEvents ddlTaxType As DropDownList
    Protected WithEvents ddlTaxTypeLn As DropDownList
    Protected WithEvents hidTaxType As HtmlInputHidden
    Protected WithEvents hidTaxTypeLn As HtmlInputHidden
    

    Protected WithEvents GenerateBtn As ImageButton
    Protected WithEvents PostingBtn As ImageButton
    Protected WithEvents RollbackBtn As ImageButton
    Protected WithEvents GenerateNoBtn As ImageButton
    Protected WithEvents PostingNoBtn As ImageButton
    Protected WithEvents RollbackNoBtn As ImageButton
    Protected WithEvents DownloadBtn As ImageButton

    Protected WithEvents LinkDownload As HyperLink

    Protected WithEvents TrxPeriod As HtmlTableRow
    Protected WithEvents TrxCode As HtmlTableRow
    Protected WithEvents TrxStatus As HtmlTableRow

    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Protected objGLTrx As New agri.GL.ClsTrx()
    Protected objGLRpt As New agri.GL.clsReport()
    Protected objCTSetup As New agri.CT.clsSetup()
    Protected objCBTrx As New agri.CB.clsTrx()


    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objSysCfg As New agri.PWSystem.clsConfig()
    Dim objLangCap As New agri.PWSystem.clsLangCap()
    Dim objAdmin As New agri.Admin.clsShare()
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

            'to avoid double click, on aspx add this : UseSubmitBehavior="false"
            GenerateBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(GenerateBtn).ToString())
            PostingBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PostingBtn).ToString())
            RollbackBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(RollbackBtn).ToString())
            GenerateNoBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(GenerateNoBtn).ToString())
            PostingNoBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(PostingNoBtn).ToString())
            RollbackNoBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(RollbackNoBtn).ToString())
            DownloadBtn.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(DownloadBtn).ToString())

            lblErrMesage.Visible = False
            lblErrMessage.Visible = False
            lblErrMessageNo.Visible = False
            lblDate.Visible = False
            lblFmt.Visible = False
            lblErrSequence.Visible = False
            TrxPeriod.Visible = False
            TrxCode.Visible = False
            TrxStatus.Visible = False

            If Not Page.IsPostBack Then
                txtFPDate.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), Now)
                txtTaxYear.Text = Right(strAccYear, 2)

                If Session("SS_FILTERPERIOD") = "0" Then
                    lstAccMonth.SelectedValue = strAccMonth
                    ddlTaxMonth.SelectedValue = strAccMonth
                    BindAccYear(strAccYear)
                Else
                    lstAccMonth.SelectedValue = strSelAccMonth
                    ddlTaxMonth.SelectedValue = strSelAccMonth
                    BindAccYear(strSelAccYear)
                End If

                BindGrid(False, 0)
                ''BindPageList()
            End If

            PostingBtn.Attributes("onclick") = "javascript:return ConfirmAction('post this tax number now');"
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dgLine.CurrentPageIndex = 0
        dgLine.EditItemIndex = -1
        BindGrid(False, 0)
        BindPageList()
    End Sub

    Sub BindGrid(ByVal pEditRow As Boolean, ByVal pIndex As Integer)
        Dim intCnt As Integer
        Dim lbButton As LinkButton
        Dim lbl As Label
        Dim chk As CheckBox
        Dim ddl As DropDownList
        Dim lblDocID As Label
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim xCnt As Integer

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
        dgLine.Dispose()

        For intCnt = 0 To dsData.Tables(0).Rows.Count - 1
            xCnt = intCnt
            BindTaxType(dsData.Tables(0).Rows(intCnt).Item("TaxType"), xCnt)
            BindTaxTypeLn(dsData.Tables(0).Rows(intCnt).Item("TaxType"), dsData.Tables(0).Rows(intCnt).Item("TaxTypeLn"), xCnt)
        Next intCnt


        BindPageList()

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblSupplierName")
            If Trim(lbl.Text) = "TOTAL" Then
                lbl.Font.Bold = True
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDDocDate")
                lbl.Visible = False
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDDPPAmount")
                lbl.Font.Bold = True
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDTaxAmount")
                lbl.Font.Bold = True
                chk = dgLine.Items.Item(intCnt).FindControl("chkSelect")
                chk.Visible = False
                lbl = dgLine.Items.Item(intCnt).FindControl("lblPeriod")
                lbl.Visible = False
                ddl = dgLine.Items.Item(intCnt).FindControl("ddlTaxType")
                ddl.Visible = False
                ddl = dgLine.Items.Item(intCnt).FindControl("ddlTaxTypeLn")
                ddl.Visible = False
            End If

            lbl = dgLine.Items.Item(intCnt).FindControl("lblRcpID")
            If Trim(lbl.Text) = "" Then
                lbl = dgLine.Items.Item(intCnt).FindControl("lblRcpDate")
                lbl.Visible = False
            End If

            lbl = dgLine.Items.Item(intCnt).FindControl("lblFPNo")
            If Trim(lbl.Text) = "" Then
                lbl = dgLine.Items.Item(intCnt).FindControl("lblFPDate")
                lbl.Visible = False
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
		Dim prevAccYear As Integer
		Dim prevAccMonth As Integer

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If
        strAccYear = lstAccYear.SelectedItem.Value
		
		If strAccMonth = "1" Then
			prevAccMonth = 12
			prevAccYear = Convert.ToInt32(strAccYear) - 1
		Else
			prevAccMonth = Convert.ToInt32(strAccMonth) - 1
			prevAccYear = Convert.ToInt32(strAccYear) 
		End if
		

        strOpCd_GET = "TX_CLSTRX_TAXASSIGNMENT_GET_INVOICE"
        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & _
						" AND CAST(A.AccYear AS INT) >= 2018 AND (CAST(A.AccYear AS INT) * 100) + CAST(A.AccMonth AS INT) <= " & (Convert.ToInt32(strAccYear) * 100) + Convert.ToInt32(strAccMonth)
						'"AND (CAST(A.AccYear AS INT) * 100) + CAST(A.AccMonth AS INT) BETWEEN " & ((prevAccYear * 100) + prevAccMonth) & " AND " & (Convert.ToInt32(strAccYear) * 100) + Convert.ToInt32(strAccMonth) 
                        '" AND (CAST(A.AccYear AS INT) * 100) + CAST(A.AccMonth AS INT) < " & (Convert.ToInt32(strAccYear) * 100) + Convert.ToInt32(strAccMonth) & _
						'" AND (CAST(A.AccYear AS INT) * 100) + CAST(A.AccMonth AS INT) >= " & (prevAccYear * 100) + prevAccMonth
						
                        '" AND A.AccMonth IN ('" & strAccMonth & "') AND A.AccYear = '" & strAccYear & "' "


        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        'For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
        '    xCnt = intCnt
        '    response.write(intCnt)
        '    BindTaxType(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxType"), xCnt)
        '    'BindTaxTypeLn(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxType"), ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxTypeLn"), intCnt)
        'Next intCnt

        intRec.Value = 0

        Return ObjTaxDs
    End Function

    Sub BindTaxType(ByVal p_TaxType As String, ByVal p_TaxIndex As Integer)
        ddlTaxType = dgLine.Items.Item(p_TaxIndex).FindControl("ddlTaxType")

        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer
        Dim objTaxType As New Object
        Dim dr As DataRow

        Dim strOpCd As String = "TX_CLSSETUP_TAXVAT_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "STRSEARCH"
        strParamValue = "AND VatType=2"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objTaxType)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STP_PDO_HEADER_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        For intCnt = 0 To objTaxType.Tables(0).Rows.Count - 1
            objTaxType.Tables(0).Rows(intCnt).Item("TaxType") = Trim(objTaxType.Tables(0).Rows(intCnt).Item("TaxType"))
            objTaxType.Tables(0).Rows(intCnt).Item("Description") = Trim(objTaxType.Tables(0).Rows(intCnt).Item("TaxType"))

            If objTaxType.Tables(0).Rows(intCnt).Item("TaxType") = Trim(p_TaxType) Then
                intSelectedIndex = intCnt + 1
                'Exit For
            End If
        Next

        dr = objTaxType.Tables(0).NewRow()
        dr("TaxType") = ""
        dr("Description") = "Select Tax Type"
        objTaxType.Tables(0).Rows.InsertAt(dr, 0)

        ddlTaxType = dgLine.Items.Item(p_TaxIndex).FindControl("ddlTaxType")
        ddlTaxType.DataSource = objTaxType.Tables(0)
        ddlTaxType.DataValueField = "TaxType"
        ddlTaxType.DataTextField = "Description"
        ddlTaxType.DataBind()
        ddlTaxType.SelectedIndex = intSelectedIndex

        If Not objTaxType Is Nothing Then
            objTaxType = Nothing
        End If

    End Sub

    Protected Sub Onchanged_TaxType(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ddllist As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddllist.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        Dim content As String = item.Cells(0).Text

        Dim ddlTT As DropDownList = CType(item.Cells(5).FindControl("ddlTaxType"), DropDownList)
        'Dim ddlTTLn As DropDownList = CType(item.Cells(8).FindControl("ddlTaxTypeLn"), DropDownList)

        hidTaxType.Value = Trim(ddlTT.SelectedItem.Value)
        BindTaxTypeLn(ddlTT.SelectedItem.Value, "", item.ItemIndex.ToString())
    End Sub

    Sub BindTaxTypeLn(ByVal p_TaxType As String, ByVal p_TaxTypeLn As String, ByVal p_TaxIndex As Integer)
        ddlTaxTypeLn = dgLine.Items.Item(p_TaxIndex).FindControl("ddlTaxTypeLn")

        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim intSelectedIndex As Integer = 0
        Dim objTaxType As New Object
        Dim dr As DataRow

        Dim strOpCd As String = "TX_CLSSETUP_TAXVATLN_LIST_GET"
        Dim strParamName As String
        Dim strParamValue As String

        strParamName = "STRSEARCH"
        strParamValue = "AND VatType=2 AND A.TaxType='" & Trim(p_TaxType) & "'"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, strParamName, strParamValue, objTaxType)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GL_STP_PDO_HEADER_LIST&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try


        If objTaxType.Tables(0).Rows.Count > 0 Then
            For intCnt = 0 To objTaxType.Tables(0).Rows.Count - 1
                objTaxType.Tables(0).Rows(intCnt).Item("TaxTypeLn") = objTaxType.Tables(0).Rows(intCnt).Item("TaxTypeLn")
                objTaxType.Tables(0).Rows(intCnt).Item("Description") = Trim(objTaxType.Tables(0).Rows(intCnt).Item("TaxTypeLn")) & ". " & Trim(objTaxType.Tables(0).Rows(intCnt).Item("TaxObject"))

                If Trim(objTaxType.Tables(0).Rows(intCnt).Item("TaxTypeLn")) = Trim(p_TaxTypeLn) Then
                    intSelectedIndex = intCnt + 1
                    'Exit For
                End If
            Next
        End If

        dr = objTaxType.Tables(0).NewRow()
        dr("TaxTypeLn") = "0"
        dr("Description") = "Select Tax Type"
        objTaxType.Tables(0).Rows.InsertAt(dr, 0)

        ddlTaxTypeLn = dgLine.Items.Item(p_TaxIndex).FindControl("ddlTaxTypeLn")
        ddlTaxTypeLn.DataSource = objTaxType.Tables(0)
        ddlTaxTypeLn.DataValueField = "TaxTypeLn"
        ddlTaxTypeLn.DataTextField = "Description"
        ddlTaxTypeLn.DataBind()
        ddlTaxTypeLn.SelectedIndex = intSelectedIndex


        If Not objTaxType Is Nothing Then
            objTaxType = Nothing
        End If

    End Sub


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
        BindGrid(False, 0)
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgLine.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid(False, 0)
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgLine.CurrentPageIndex = e.NewPageIndex
        BindGrid(False, 0)
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgLine.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid(False, 0)
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
        BindGrid(False, 0)
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

        ddlTaxYear.DataSource = objAccYearDs.Tables(0)
        ddlTaxYear.DataValueField = "AccYear"
        ddlTaxYear.DataTextField = "UserName"
        ddlTaxYear.DataBind()
        ddlTaxYear.SelectedIndex = intSelectedIndex - 1
    End Sub

    Function Date_Validation(ByVal pv_strInputDate As String, ByVal pv_blnIsShortDate As Boolean) As String
        Dim objSysCfgDs As New Object
        Dim objActualDate As New Object
        Dim strDateFormat As String
        Dim strParam As String
        Dim intErrNo As Integer

        strParam = "PWSYSTEM_CLSCONFIG_CONFIG_DATEFMT_GET"
        Try
            intErrNo = objSysCfg.mtdGetConfigInfo(strParam, _
                                                  strCompany, _
                                                  strLocation, _
                                                  strUserId, _
                                                  objSysCfgDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=INVOICERCV_GET_CONFIG&errmesg=" & lblErrMessage.Text & "&redirect=pu/trx/pu_polist.aspx")
        End Try

        strDateFormat = objSysCfg.mtdGetDateFormat(Trim(objSysCfgDs.Tables(0).Rows(0).Item("Datefmt")))

        If pv_blnIsShortDate Then
            Date_Validation = objGlobal.GetShortDate(strDateFormat, pv_strInputDate)
        Else
            If objGlobal.mtdValidInputDate(strDateFormat, _
                                            pv_strInputDate, _
                                            strAcceptDateFormat, _
                                            objActualDate) = True Then
                Date_Validation = objActualDate
            Else
                Date_Validation = ""
            End If
        End If
    End Function

    Sub RefreshNoBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_GET As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim xCnt As Integer = 0
        Dim strFPDate As String = Date_Validation(txtFPDate.Text, False)
        Dim lbl As Label

        If ddlTaxMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
        Else
            strAccMonth = ddlTaxMonth.SelectedItem.Value
        End If
        strAccYear = ddlTaxYear.SelectedItem.Value

        strOpCd_GET = "TX_CLSTRX_TAXASSIGNMENT_GET"
        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & _
                        " AND A.EffDate = '" & strFPDate & "'"

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd_GET, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineGen.DataSource = ObjTaxDs
        dgLineGen.DataBind()

        For intCnt = 0 To dgLineGen.Items.Count - 1
            lbl = dgLineGen.Items.Item(intCnt).FindControl("lblDocID")
            If Trim(lbl.Text) = "" Then
                lbl = dgLineGen.Items.Item(intCnt).FindControl("lblDocDate")
                lbl.Visible = False
            End If

            lbl = dgLineGen.Items.Item(intCnt).FindControl("lblFPNo")
            If Trim(lbl.Text) = "" Then
                lbl = dgLineGen.Items.Item(intCnt).FindControl("lblFPDate")
                lbl.Visible = False
            End If
        Next

        If ObjTaxDs.Tables(0).Rows.Count > 0 Then
            hidStatus.Value = 1
        End If
    End Sub

    Sub GenerateNoBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim objTax As New Object
        Dim strFPDate As String = Date_Validation(txtFPDate.Text, False)
        Dim strOpCd As String
        Dim NoAwal As Integer
        Dim NoAkhir As Integer
        Dim NoTotal As Integer
        Dim lbl As Label

        'If ddlTaxMonth.SelectedItem.Value = "0" Then
        '    lblErrMessageNo.Text = "Please select only 1 account period to proceed."
        '    lblErrMessageNo.Visible = True
        '    Exit Sub
        'End If
        'If Month(strFPDate) <> Trim(ddlTaxMonth.SelectedItem.Value) Then
        '    lblDate.Text = "Period and month of effective date not equal."
        '    lblDate.Visible = True
        '    Exit Sub
        'End If
		If txtTaxBranch.Text = "" Or txtTaxBranch.Text = "-" Then
            lblErrMessageNo.Text = "Branch cannot be empty"
            lblErrMessageNo.Visible = True
            Exit Sub
        End If
		If txtTaxYear.Text = "" Or txtTaxYear.Text = "-" Then
            lblErrMessageNo.Text = "Year cannot be empty"
            lblErrMessageNo.Visible = True
            Exit Sub
        End If
        If txtTaxNoFrom.Text = "" Or txtTaxNoTo.Text = "" Then
            lblErrSequence.Text = "Sequence no. cannot be empty"
            lblErrSequence.Visible = True
            Exit Sub
        End If
        If Len(txtTaxNoFrom.Text) < 8 Or Len(txtTaxNoTo.Text) < 8 Then
            lblErrSequence.Text = "Sequence no. cannot less that 8 digit"
            lblErrSequence.Visible = True
            Exit Sub
        End If
        If txtTaxNoFrom.Text <> "" Or txtTaxNoTo.Text <> "" Then
            NoAwal = CInt(txtTaxNoFrom.Text)
            NoAkhir = CInt(txtTaxNoTo.Text)
            NoTotal = NoAkhir - NoAwal
            If NoTotal < 0 Then
                lblErrSequence.Text = "First number cannot be less than second number"
                lblErrSequence.Visible = True
                Exit Sub
            End If
        End If

        'cek effective date
        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_GET"
        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & _
                        " AND EffDate = '" & strFPDate & "' AND FPNo LIKE '" & Trim(ddlTaxTrx.SelectedItem.Value) + Trim(ddlTaxStatus.SelectedItem.Value) + "." + Trim(txtTaxBranch.Text) + "." + Trim(txtTaxYear.Text) & "%' "

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If ObjTaxDs.Tables(0).Rows.Count > 0 Then
            lblDate.Text = "Effective date with tax format already exist, please use refresh button to review"
            lblDate.Visible = True
            Exit Sub
        End If

        strAccMonth = ddlTaxMonth.SelectedItem.Value
        strAccYear = ddlTaxYear.SelectedItem.Value
        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_GENERATE_DUMMY"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|TAXTRX|TAXSTATUS|TAXBRANCH|TAXYEAR|NOFROM|NOTO|FPDATE|FPNO"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId & "|" & _
                        ddlTaxTrx.SelectedItem.Value & "|" & _
                        ddlTaxStatus.SelectedItem.Value & "|" & _
                        txtTaxBranch.Text & "|" & _
                        txtTaxYear.Text & "|" & _
                        txtTaxNoFrom.Text & "|" & txtTaxNoTo.Text & "|" & _
                        strFPDate & "|" & ""

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLineGen.DataSource = ObjTaxDs
        dgLineGen.DataBind()
        hidStatus.Value = 0

        For intCnt = 0 To dgLineGen.Items.Count - 1
            lbl = dgLineGen.Items.Item(intCnt).FindControl("lblDocID")
            If Trim(lbl.Text) = "" Then
                lbl = dgLineGen.Items.Item(intCnt).FindControl("lblDocDate")
                lbl.Visible = False
            End If

            lbl = dgLineGen.Items.Item(intCnt).FindControl("lblFPNo")
            If Trim(lbl.Text) = "" Then
                lbl = dgLineGen.Items.Item(intCnt).FindControl("lblFPDate")
                lbl.Visible = False
            End If
        Next
    End Sub

    Sub PostingNoBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim ObjTaxDs As New Object
        Dim strFPDate As String = Date_Validation(txtFPDate.Text, False)
        Dim strOpCd As String
        Dim NoAwal As Integer
        Dim NoAkhir As Integer
        Dim NoTotal As Integer

        'If ddlTaxMonth.SelectedItem.Value = "0" Then
        '    lblErrMessageNo.Text = "Please select only 1 account period to proceed."
        '    lblErrMessageNo.Visible = True
        '    Exit Sub
        'End If
        'If Month(strFPDate) <> Trim(ddlTaxMonth.SelectedItem.Value) Then
        '    lblDate.Text = "Period and month of effective date not equal."
        '    lblDate.Visible = True
        '    Exit Sub
        'End If
		If txtTaxBranch.Text = "" Or txtTaxBranch.Text = "-" Then
            lblErrMessageNo.Text = "Branch cannot be empty"
            lblErrMessageNo.Visible = True
            Exit Sub
        End If
		If txtTaxYear.Text = "" Or txtTaxYear.Text = "-" Then
            lblErrMessageNo.Text = "Year cannot be empty"
            lblErrMessageNo.Visible = True
            Exit Sub
        End If
        If txtTaxNoFrom.Text = "" Or txtTaxNoTo.Text = "" Then
            lblErrSequence.Text = "Sequence no. cannot be empty"
            lblErrSequence.Visible = True
            Exit Sub
        End If
        If Len(txtTaxNoFrom.Text) < 8 Or Len(txtTaxNoTo.Text) < 8 Then
            lblErrSequence.Text = "Sequence no. cannot less that 8 digit"
            lblErrSequence.Visible = True
            Exit Sub
        End If
        If txtTaxNoFrom.Text <> "" Or txtTaxNoTo.Text <> "" Then
            NoAwal = CInt(txtTaxNoFrom.Text)
            NoAkhir = CInt(txtTaxNoTo.Text)
            NoTotal = NoAkhir - NoAwal
            If NoTotal < 0 Then
                lblErrSequence.Text = "First number cannot be less than second number"
                lblErrSequence.Visible = True
                Exit Sub
            End If
        End If

        'cek effective date
        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_GET"
        strParamName = "LOCCODE|STRSEARCH"
        strParamValue = strLocation & "|" & _
                        " AND FPDate = '" & strFPDate & "' AND FPNo LIKE '" & Trim(ddlTaxTrx.SelectedItem.Value) + Trim(ddlTaxStatus.SelectedItem.Value) + "." + Trim(txtTaxBranch.Text) + "." + Trim(txtTaxYear.Text) & "%' "

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=TX_Setup_TaxObjectList_GET&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        If ObjTaxDs.Tables(0).Rows.Count > 0 Then
            lblDate.Text = "Effective date with tax format already exist, please use refresh button to review"
            lblDate.Visible = True
            Exit Sub
        End If

        strAccMonth = ddlTaxMonth.SelectedItem.Value
        strAccYear = ddlTaxYear.SelectedItem.Value
        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_GENERATE"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|TAXTRX|TAXSTATUS|TAXBRANCH|TAXYEAR|NOFROM|NOTO|FPDATE|FPNO"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId & "|" & _
                        ddlTaxTrx.SelectedItem.Value & "|" & _
                        ddlTaxStatus.SelectedItem.Value & "|" & _
                        txtTaxBranch.Text & "|" & _
                        txtTaxYear.Text & "|" & _
                        txtTaxNoFrom.Text & "|" & txtTaxNoTo.Text & "|" & _
                        strFPDate & "|" & ""

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        lblErrMessageNo.Visible = True
        lblErrMessageNo.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))
        RefreshNoBtn_Click(Sender, E)

        'dgLineGen.DataSource = ObjTaxDs
        'dgLineGen.DataBind()
        hidStatus.Value = 1
    End Sub

    Sub RollbackNoBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim ObjTaxDs As New Object
        Dim strFPDate As String = Date_Validation(txtFPDate.Text, False)
        Dim strOpCd As String
        Dim NoAwal As Integer
        Dim NoAkhir As Integer
        Dim NoTotal As Integer

        If ddlTaxMonth.SelectedItem.Value = "0" Then
            lblErrMessageNo.Text = "Please select only 1 account period to proceed."
            lblErrMessageNo.Visible = True
            Exit Sub
        End If
        If Month(strFPDate) <> Trim(ddlTaxMonth.SelectedItem.Value) Then
            lblDate.Text = "Period and month of effective date not equal."
            lblDate.Visible = True
            Exit Sub
        End If

        strAccMonth = ddlTaxMonth.SelectedItem.Value
        strAccYear = ddlTaxYear.SelectedItem.Value
        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_ROLLBACK"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID|TAXTRX|TAXSTATUS|TAXBRANCH|TAXYEAR|NOFROM|NOTO|FPDATE|FPNO"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId & "|" & _
                        ddlTaxTrx.SelectedItem.Value & "|" & _
                        ddlTaxStatus.SelectedItem.Value & "|" & _
                        txtTaxBranch.Text & "|" & _
                        txtTaxYear.Text & "|" & _
                        txtTaxNoFrom.Text & "|" & txtTaxNoTo.Text & "|" & _
                        strFPDate & "|" & ""

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        lblErrMessageNo.Visible = True
        lblErrMessageNo.Text = Trim(ObjTaxDs.Tables(0).Rows(0).Item("Msg"))
        RefreshNoBtn_Click(Sender, E)

        'dgLineGen.DataSource = ObjTaxDs
        'dgLineGen.DataBind()
        hidStatus.Value = 0
    End Sub

    Sub RefreshBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        BindGrid(False, 0)
    End Sub

    Sub GenerateBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim objTax As New Object
        Dim strOpCd As String
        Dim lbl As Label
        Dim chk As CheckBox
        Dim ddl As DropDownList
        Dim xCnt As Integer

        If lstAccMonth.SelectedItem.Value = "0" Then
            lblErrMessage.Text = "Please select only 1 account period to proceed."
            lblErrMessage.Visible = True
            Exit Sub
        End If

        strAccMonth = lstAccMonth.SelectedItem.Value
        strAccYear = lstAccYear.SelectedItem.Value
        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_NUMBER_GENERATE_DUMMY"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId 

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLine.DataSource = ObjTaxDs
        dgLine.DataBind()

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            xCnt = intCnt
            BindTaxType(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxType"), xCnt)
            BindTaxTypeLn(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxType"), ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxTypeLn"), xCnt)
        Next intCnt

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblSupplierName")
            If Trim(lbl.Text) = "TOTAL" Then
                lbl.Font.Bold = True
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDDocDate")
                lbl.Visible = False
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDDPPAmount")
                lbl.Font.Bold = True
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDTaxAmount")
                lbl.Font.Bold = True
                chk = dgLine.Items.Item(intCnt).FindControl("chkSelect")
                chk.Visible = False
                lbl = dgLine.Items.Item(intCnt).FindControl("lblPeriod")
                lbl.Visible = False
                ddl = dgLine.Items.Item(intCnt).FindControl("ddlTaxType")
                ddl.Visible = False
                ddl = dgLine.Items.Item(intCnt).FindControl("ddlTaxTypeLn")
                ddl.Visible = False
            End If

            lbl = dgLine.Items.Item(intCnt).FindControl("lblRcpID")
            If Trim(lbl.Text) = "" Then
                lbl = dgLine.Items.Item(intCnt).FindControl("lblRcpDate")
                lbl.Visible = False
            End If

            lbl = dgLine.Items.Item(intCnt).FindControl("lblFPNo")
            If Trim(lbl.Text) = "" Then
                lbl = dgLine.Items.Item(intCnt).FindControl("lblFPDate")
                lbl.Visible = False
            End If
        Next
    End Sub

    Sub PostingBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim objTax As New Object
        Dim strOpCd As String
        Dim lbl As Label
        Dim chk As CheckBox
        Dim ddl As DropDownList
        Dim intCheck As Integer = 0
        Dim xCnt As Integer

        If lstAccMonth.SelectedItem.Value = "0" Then
            lblErrMessage.Text = "Please select only 1 account period to proceed."
            lblErrMessage.Visible = True
            Exit Sub
        End If

        For Each dgLineItem In dgLine.Items
            Dim myCheckbox As CheckBox = CType(dgLineItem.Cells(7).Controls(1), CheckBox)
            If myCheckbox.Checked = True Then
                intCheck = intCheck + 1
            Else
                intCheck = intCheck + 0
            End If
        Next

        If intCheck = 0 Then
            lblErrMessage.Text = "Please checked document(s) to posting."
            lblErrMessage.Visible = True
            Exit Sub
        End If

        strAccMonth = lstAccMonth.SelectedItem.Value
        strAccYear = lstAccYear.SelectedItem.Value
        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_NUMBER_GENERATE"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLine.DataSource = ObjTaxDs
        dgLine.DataBind()

        For intCnt = 0 To ObjTaxDs.Tables(0).Rows.Count - 1
            xCnt = intCnt
            BindTaxType(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxType"), xCnt)
            BindTaxTypeLn(ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxType"), ObjTaxDs.Tables(0).Rows(intCnt).Item("TaxTypeLn"), xCnt)
        Next intCnt

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblSupplierName")
            If Trim(lbl.Text) = "TOTAL" Then
                lbl.Font.Bold = True
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDDocDate")
                lbl.Visible = False
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDDPPAmount")
                lbl.Font.Bold = True
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDTaxAmount")
                lbl.Font.Bold = True
                chk = dgLine.Items.Item(intCnt).FindControl("chkSelect")
                chk.Visible = False
                lbl = dgLine.Items.Item(intCnt).FindControl("lblPeriod")
                lbl.Visible = False
                ddl = dgLine.Items.Item(intCnt).FindControl("ddlTaxType")
                ddl.Visible = False
                ddl = dgLine.Items.Item(intCnt).FindControl("ddlTaxTypeLn")
                ddl.Visible = False
            End If

            lbl = dgLine.Items.Item(intCnt).FindControl("lblRcpID")
            If Trim(lbl.Text) = "" Then
                lbl = dgLine.Items.Item(intCnt).FindControl("lblRcpDate")
                lbl.Visible = False
            End If

            lbl = dgLine.Items.Item(intCnt).FindControl("lblFPNo")
            If Trim(lbl.Text) = "" Then
                lbl = dgLine.Items.Item(intCnt).FindControl("lblFPDate")
                lbl.Visible = False
            End If
        Next
    End Sub

    Sub RollbackBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim objTax As New Object
        Dim strOpCd As String
        Dim lbl As Label
        Dim chk As CheckBox
        Dim ddl As DropDownList

        If lstAccMonth.SelectedItem.Value = "0" Then
            lblErrMessage.Text = "Please select only 1 account period to proceed."
            lblErrMessage.Visible = True
            Exit Sub
        End If
        strAccMonth = lstAccMonth.SelectedItem.Value
        strAccYear = lstAccYear.SelectedItem.Value
        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_NUMBER_ROLLBACK"

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & strUserId 

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                ObjTaxDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & lblErrMessage.Text & "&redirect=")
        End Try

        dgLine.DataSource = ObjTaxDs
        dgLine.DataBind()

        For intCnt = 0 To dgLine.Items.Count - 1
            lbl = dgLine.Items.Item(intCnt).FindControl("lblSupplierName")
            If Trim(lbl.Text) = "TOTAL" Then
                lbl.Font.Bold = True
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDDocDate")
                lbl.Visible = False
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDDPPAmount")
                lbl.Font.Bold = True
                lbl = dgLine.Items.Item(intCnt).FindControl("lblIDTaxAmount")
                lbl.Font.Bold = True
                chk = dgLine.Items.Item(intCnt).FindControl("chkSelect")
                chk.Visible = False
                lbl = dgLine.Items.Item(intCnt).FindControl("lblPeriod")
                lbl.Visible = False
            End If
        Next
    End Sub

    Sub chkSelected_Changed(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOpCd As String = "TX_CLSTRX_TAXASSIGNMENT_NUMBER_CHECKED"
        Dim dgLineItem As DataGridItem
        Dim blnCheck As Integer
        Dim strDocID As String
        Dim strTaxType As String
        Dim strTaxTypeLn As String
        Dim strFPNo As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
            lblErrMesage.Text = "Please select only 1 account period to proceed."
            lblErrMesage.Visible = True
            Exit Sub
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        For Each dgLineItem In dgLine.Items
            Dim myCheckbox As CheckBox = CType(dgLineItem.Cells(7).Controls(1), CheckBox)
            If myCheckbox.Checked = True Then
                blnCheck = 1
            Else
                blnCheck = 0
            End If

            Dim myLabel As Label
            Dim myDDL As DropDownList

            myLabel = CType(dgLineItem.Cells(0).Controls(1), Label)
            strDocID = myLabel.Text
            myDDL = CType(dgLineItem.Cells(5).Controls(1), DropDownList)
            strTaxType = myDDL.SelectedItem.Value
            myDDL = CType(dgLineItem.Cells(6).Controls(1), DropDownList)
            strTaxTypeLn = myDDL.SelectedItem.Value
            myLabel = CType(dgLineItem.Cells(8).Controls(1), Label)
            strFPNo = myLabel.Text


            strParamName = "LOCCODE|DOCID|ISCHECK|TAXTYPE|TAXTYPELN|FPNO"
            strParamValue = strLocation & "|" & Trim(strDocID) & "|" & blnCheck & "|" & strTaxType & "|" & strTaxTypeLn & "|" & strFPNo

            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd, _
                                                      strParamName, _
                                                      strParamValue)


            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CP_CLSTRX_REKONSILEDTL_ADD&errmesg=" & lblErrMessage.Text & "&redirect=CB/trx/CB_trx_Rekonsilelist")
            End Try
        Next

    End Sub

    Sub chkReserved_Changed(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strOpCd As String = "TX_CLSTRX_TAXASSIGNMENT_RESERVED"
        Dim dgLineItem As DataGridItem
        Dim blnCheck As Integer
        Dim strDocID As String
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String

        If lstAccMonth.SelectedItem.Value = "0" Then
            strAccMonth = "1','2','3','4','5','6','7','8','9','10','11','12"
            lblErrMesage.Text = "Please select only 1 account period to proceed."
            lblErrMesage.Visible = True
            Exit Sub
        Else
            strAccMonth = lstAccMonth.SelectedItem.Value
        End If

        If hidStatus.Value = 0 Then
            lblErrMessageNo.Visible = True
            lblErrMessageNo.Text = "Reservation only for existing data. Please post tax number before reservation."
            Exit Sub
        End If

        For Each dgLineItem In dgLineGen.Items
            Dim myCheckbox As CheckBox = CType(dgLineItem.Cells(4).Controls(1), CheckBox)
            If myCheckbox.Checked = True Then
                blnCheck = 1
            Else
                blnCheck = 0
            End If

            Dim myLabel As Label = CType(dgLineItem.Cells(2).Controls(1), Label)
            strDocID = myLabel.Text


            strParamName = "LOCCODE|FPNO|ISRESERVED"
            strParamValue = strLocation & "|" & Trim(strDocID) & "|" & blnCheck
            Try
                intErrNo = objGLTrx.mtdInsertDataCommon(strOpCd, _
                                                      strParamName, _
                                                      strParamValue)


            Catch Exp As System.Exception
                Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=CP_CLSTRX_REKONSILEDTL_ADD&errmesg=" & lblErrMessage.Text & "&redirect=CB/trx/CB_trx_Rekonsilelist")
            End Try
        Next

    End Sub

    Sub DownloadBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strPeriod As String = IIf(Len(strAccMonth) = 1, "0" & Trim(strAccMonth), Trim(strAccMonth)) & Trim(strAccYear)
        Dim strOpCd As String
        Dim strSearch As String
        Dim objMapPath As String
        Dim objFTPFolder As String
        Dim objRptDs As New DataSet()
        Dim intCnt As Integer
        Dim intErrNo As Integer
        Dim strParamName As String
        Dim strParamValue As String
        Dim strUrl As String
		Dim intUrut As Integer
		Dim NoUrut As String

        strOpCd = "TX_CLSTRX_TAXASSIGNMENT_NUMBER_CSV"
        strSearch = ""
        'strSearch = IIf(Trim(srchFPNo.Text) = "", "", "FPNo Like '%" & Trim(srchFPNo.Text) & "%' AND ")
        'strSearch = strSearch + IIf(Trim(srchDocID.Text) = "", "", "DocID LIKE '%" & Trim(srchDocID.Text) & "%' AND ")
        'strSearch = strSearch + IIf(Trim(srchSupplier.Text) = "", "", "(SupplierCode LIKE '%" & Trim(srchSupplier.Text) & "%' OR SupplierName LIKE '%" & Trim(srchSupplier.Text) & "%') AND ")

        If (Trim(strSearch) <> "") Then
            strSearch = " AND " & Mid(strSearch, 1, Len(strSearch) - 4) & " "
        End If

        strParamName = "LOCCODE|POSTACCMONTH|POSTACCYEAR|STATUS|STRSEARCH|ORDERBY"
        strParamValue = strLocation & "|" & strAccMonth & "|" & strAccYear & "|" & 2 & "|" & strSearch & "|" & "ORDER BY FPDate, FPNo ASC"

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

        Dim MyCSVFileK As String = objFTPFolder & "VATOut-" & Trim(strCompany) & Trim(strPeriod) & ".csv"
        If My.Computer.FileSystem.FileExists(MyCSVFileK) = True Then
            My.Computer.FileSystem.DeleteFile(MyCSVFileK)
        End If
        Dim dataToWriteK As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(MyCSVFileK, True)

        dataToWriteK.WriteLine("FK;KD_JENIS_TRANSAKSI;FG_PENGGANTI;NOMOR_FAKTUR;MASA_PAJAK;TAHUN_PAJAK;TANGGAL_FAKTUR;NPWP;NAMA;ALAMAT_LENGKAP;JUMLAH_DPP;JUMLAH_PPN;JUMLAH_PPNBM;ID_KETERANGAN_TAMBAHAN;FG_UANG_MUKA;UANG_MUKA_DPP;UANG_MUKA_PPN;UANG_MUKA_PPNBM;REFERENSI")
        dataToWriteK.WriteLine("LT;NPWP;NAMA;JALAN;BLOK;NOMOR;RT;RW;KECAMATAN;KELURAHAN;KABUPATEN;PROPINSI;KODE_POS;NOMOR_TELEPON")
        dataToWriteK.WriteLine("OF;KODE_OBJEK;NAMA;HARGA_SATUAN;JUMLAH_BARANG;HARGA_TOTAL;DISKON;DPP;PPN;TARIF_PPNBM;PPNBM")

        intUrut = 0
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

				dataToWriteK.WriteLine(
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kd_FK")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("JnsTrx")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPPengganti")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPNo")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("MasaPajak")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("TahunPajak")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPDate")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("CustNPWP")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("CustName")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("CustAddress")) & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPDPPAmount")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("FPAmount")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("PPNBM")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("AddNote")) & ";" & 
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("DPInit")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("DPNetAmount")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("DPPPNAmount")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("DPPPNBM")) & ";" & _
					Trim(NoUrut))
				dataToWriteK.WriteLine(	
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kd_LT")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("CompName")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("Jalan")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Blok")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Nomor")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("RT")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("RW")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kecamatan")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kelurahan")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kabupaten")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Propinsi")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("KodePos")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("NoTelp")))
				dataToWriteK.WriteLine(	
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("Kd_OF")) & ";" & Trim(NoUrut) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Description")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("Cost")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Unit")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("Amount")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("DiscAmount")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("NetAmount")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("PPNAmount")) & ";" & _
					Trim(objRptDs.Tables(0).Rows(intCnt).Item("TarifPPNBM")) & ";" & Trim(objRptDs.Tables(0).Rows(intCnt).Item("PPNBM")))
            Next

            'lblErrMessage.Visible = True
            'lblErrMessage.Text = "InputVAT File created in " & Trim(MyCSVFileK)

            LinkDownload.Visible = True
            LinkDownload.Text = "Download file VATOut" & Trim(strCompany) & Trim(strPeriod) & ".csv"
            LinkDownload.NavigateUrl = "../../../" & strUrl & "VATOut-" & Trim(strCompany) & Trim(strPeriod) & ".csv"

        End If
        dataToWriteK.Close()

    End Sub
End Class
