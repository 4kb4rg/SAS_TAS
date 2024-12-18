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


Public Class WM_WeighBridgeTicketList : Inherits Page


    Protected objWMTrx As New agri.WM.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objGLTrx As New agri.GL.clsTrx()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objOk As New agri.GL.ClsTrx()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWMAR As Integer
    Dim strDateFormat As String
    Dim strParamName As String
    Dim strParamValue As String

    Dim objPPHDs As New DataSet()
    Dim objTicketDs As New DataSet()
    Dim objDisbunDs As New DataSet()

#REGION "TOOLS & COMPONENT"

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")
        strDateFormat = Session("SS_DATEFMT")

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            lblErrDateInMsg.Visible = false
            lblErrDateIn.Visible = False
            lblErrMessage.Visible = False
            lblErrRefresh.Visible = False
            lblErrGenerate.Visible = False

            If SortExpression.Text = "" Then
                SortExpression.Text = "TicketNo"
            End If

            btnGenerate.Attributes.Add("onclick", "this.disabled=true;" + GetPostBackEventReference(btnGenerate).ToString())
            btnGenerate.Attributes("onclick") = "javascript:return confirm('Generate journal ?');"

            If Not Page.IsPostBack Then
                
                SearchBtn.Attributes("onclick") = "javascript:return ConfirmAction('Generated');"
                ddlMonth.SelectedIndex = CInt(Session("SS_SELACCMONTH"))
                
                BindAccYear(Session("SS_SELACCYEAR"))

                srchDateIn.SelectedDate=Date.Now
                srchDateInTo.SelectedDate=Date.Now

                BindSubLocation()                
                BindSupplier("")
                'BindAccount("", "", "", "", "", "", "")
                'LoadCOASetting()

                lstDropList.Visible = False
                btnPrev.Visible = False
                btnNext.Visible = False

            End If
        End If
    End Sub

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        IF Trim(srchlocation.SelectedItem.Value)="" Then
            UserMsgBox(Me,"Please Select Location")
            srchlocation.Focus()
            Exit sub
        End IF

        if (Month(srchDateIn.SelectedDate) <> Trim(ddlMonth.Selectedvalue)) OR (Month(srchDateInTo.SelectedDate) <> Trim(ddlMonth.Selectedvalue)) Then
            UserMsgBox(Me,"Invalid Selected Month - Date From And Period Not Equal")
            ddlMonth.Focus()
            Exit sub
        End If

        if (Year(srchDateIn.SelectedDate) <> Trim(ddlyear.SelectedItem.Value)) OR (Year(srchDateInTo.SelectedDate) <> Trim(ddlyear.SelectedItem.Value))   Then
            UserMsgBox(Me,"Invalid Selected Year - Date To And Period Not Equal")
            ddlyear.Focus()
            Exit sub
        End If

        dgTicketList.CurrentPageIndex = 0
        dgTicketList.EditItemIndex = -1
        BindGrid()

        dgPPHList.CurrentPageIndex = 0
        dgPPHList.EditItemIndex = -1
        BindGridPPH()

        dgFFBPriceList.CurrentPageIndex = 0
        dgFFBPriceList.EditItemIndex = -1
        BindGridFFBPrice()
        BindGridFFBPricedDetail()
        BindGridFFBPriceBongkar()
        LoadDataEmpty()

        UserMsgBox(Me, "Generated Sucsess !!!")
        Exit Sub
    End Sub

    Sub btnSaveInv_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        IF lSaveIsInvoice=True Then
            UserMsgBox(Me,"Save Completed")
        End IF
    End Sub

    Sub btnGenerate_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim strOpCd_DKtr As String = "WM_WM_CLSTRX_TICKET_GENERATE_JOURNAL"
        Dim objDataSet As New Object()
        Dim intErrNo As Integer
        Dim strMn As String
        Dim strYr As String

        strMn = ddlMonth.SelectedItem.Value.Trim
        strYr = ddlyear.SelectedItem.Value.Trim

        strParamName = "LOCCODE|ACCMONTH|ACCYEAR|UPDATEID"
        strParamValue = strLocation & "|" & strMn & "|" & strYr & "|" & strUserId

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd_DKtr, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=PR_MthEnd_PPH21_Estate&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objPPHDs.Tables(0).Rows.Count > 0 Then
            lblErrGenerate.Visible = True
            lblErrGenerate.Text = objPPHDs.Tables(0).Rows(0).Item("Msg")
        End If

        'LoadData()
        LoadDataPPH()

    End Sub

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgTicketList.CurrentPageIndex = 0
            Case "prev"
                dgTicketList.CurrentPageIndex = _
                Math.Max(0, dgTicketList.CurrentPageIndex - 1)
            Case "next"
                dgTicketList.CurrentPageIndex = _
                Math.Min(dgTicketList.PageCount - 1, dgTicketList.CurrentPageIndex + 1)
            Case "last"
                dgTicketList.CurrentPageIndex = dgTicketList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgTicketList.CurrentPageIndex
        BindGrid()
    End Sub

    Sub ddlVerified_OnSelectedIndexChanged()
        BindGridPPH()
    End Sub

    Sub PagingIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Page.IsPostBack Then
            dgTicketList.CurrentPageIndex = lstDropList.SelectedIndex
            BindGrid()
        End If
    End Sub

    Sub OnPageChanged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgTicketList.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
        dgTicketList.CurrentPageIndex = lstDropList.SelectedIndex
        BindGrid()
    End Sub

    Sub DEDR_Delete(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim strOppCd_WeighBridge_Ticket_GET As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_GET"
        Dim strOppCd_WeighBridge_Ticket_ADD As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_ADD"
        Dim strOppCd_WeighBridge_Ticket_UPD As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_UPD"
        Dim strParam As String = ""
        Dim intErrNo As Integer
        Dim strTicketNo As String
        Dim lblTicketNo As Label

        dgTicketList.EditItemIndex = CInt(E.Item.ItemIndex)
        lblTicketNo = dgTicketList.Items.Item(CInt(E.Item.ItemIndex)).FindControl("lblTicketNo")
        strTicketNo = lblTicketNo.Text

        strParam = strTicketNo & "|||||||||||||||||||||||" & objWMTrx.EnumWeighBridgeTicketStatus.Deleted
        Try
            strParam = strTicketNo & "|" & objWMTrx.EnumWeighBridgeTicketStatus.Deleted
            intErrNo = objWMTrx.mtdDelWeighBridgeTicket(strOppCd_WeighBridge_Ticket_UPD, _
                                                        strLocation, _
                                                        strUserId, _
                                                        strAccMonth, _
                                                        strAccYear, _
                                                        strParam)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=WM_CLSTRX_WEIGHBRIDGE_TICKET_LIST_DELETE&errmesg=" & lblErrMessage.Text & "&redirect=WM/trx/WM_trx_WeighBridgeTicketDet.aspx")
        End Try

        dgTicketList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub btnPPHPrintPrev_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlyear.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=TBSPPH-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgPPHList.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub

    Sub btnTicketPrintPrev_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlyear.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        strAccYear = ddlyear.SelectedItem.Value
         
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=TBSTICKET-" & Trim(strLocation) & "-" & Trim(cAccMonth) & Trim(strAccYear) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lbl As Label

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTicketList.PageSize)

        dgTicketList.DataSource = dsData
        If dgTicketList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTicketList.CurrentPageIndex = 0
            Else
                dgTicketList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgTicketList.DataBind()
        'BindPageList()

        'PageNo = dgTicketList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgTicketList.PageCount

        'CType(dgTicketList.Items(intCnt).FindControl("lblNoKontrakWB"), Label).BackColor =
      
        For intCnt = 0 To dgTicketList.Items.Count - 1

            CType(dgTicketList.Items.Item(intCnt).FindControl("lblVerifyInfo"), Label).Visible = True
            If CType(dgTicketList.Items.Item(intCnt).FindControl("chkisInvoice"), CheckBox).Checked = True Then
                CType(dgTicketList.Items.Item(intCnt).FindControl("lblVerifyInfo"), Label).Text = "Yes"
            Else
                CType(dgTicketList.Items.Item(intCnt).FindControl("lblVerifyInfo"), Label).Text = "Noaaa"
            End If

            CType(dgTicketList.Items.Item(intCnt).FindControl("chkisInvoice"), CheckBox).Visible = False
        Next

        dgTicketList.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
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

    Private Sub dgTicketList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTicketList.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)
            
        
            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = ""
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUPPLIER"
            dgCell.HorizontalAlign = HorizontalAlign.Center
  
            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PRODUK"
            dgCell.HorizontalAlign = HorizontalAlign.Center
 
            dgCell = New TableCell()
            dgCell.ColumnSpan = 11
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DATA TIMBANGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            ' ' ' ''---end colum data timbangan 

            ' ' ' ''-- start berat timbangan

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "BERAT TIMBANG (KG)"
            dgCell.HorizontalAlign = HorizontalAlign.Center
            ' ''-end Berat Timbang

            ' ' ''--start potongan            
            dgCell = New TableCell()
            dgCell.ColumnSpan = 5
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "POTONGAN "
            dgCell.HorizontalAlign = HorizontalAlign.Center


            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "QTY JANJANG"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "IS BUAH INAP"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUB TOTAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TAMBAHAN <BR> HARGA (Rp)"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "DASAR PENGENAAN PAJAK (DPP)"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "RUPIAH <br> DIBAYAR (Rp)"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS BONGKAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS LAPANGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "INVOICE ID"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "REALISASI PEMBAYARAN TBS"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgTicketList.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgTicketList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTicketList.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem

             dgItem = New DataGridItem(0, 0, ListItemType.Header)
                e.Item.Cells(0).Visible = False
                e.Item.Cells(1).Visible = False
                e.Item.Cells(2).Visible = False
                e.Item.Cells(22).Visible = False
                e.Item.Cells(23).Visible = False
                e.Item.Cells(27).Visible = False
                e.Item.Cells(31).Visible = False
                e.Item.Cells(38).Visible = False
                e.Item.Cells(39).Visible = False              
        End If
    End Sub

    Sub dgPPHList_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")
            End If
        End If
    End Sub

    Private Sub dgPPHList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPPHList.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then    
            Dim dgItem As DataGridItem
            Dim dgCell As TableCell
            dgItem = New DataGridItem(0, 0, ListItemType.Header)

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TANGGAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SUPPLIER/CUSTOMER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "KG <BR>PKS"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TONASE DIBAYAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "TAMBAHAN <BR>HARGA (Rp)"
            dgCell.HorizontalAlign = HorizontalAlign.Center
 

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "PPH"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "RUPIAH <BR> DIBAYAR (Rp)"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS BONGKAR"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "ONGKOS LAPANGAN"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.RowSpan = 2
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "NO. JURNAL"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgItem.Font.Bold = True
            dgPPHList.Controls(0).Controls.AddAt(0, dgItem)
        End If
    End Sub

    Private Sub dgPPHList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPPHList.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dgItem As DataGridItem
            dgItem = New DataGridItem(0, 0, ListItemType.Header)
            e.Item.Cells(0).Visible = False
            e.Item.Cells(1).Visible = False
            e.Item.Cells(5).Visible = False
            e.Item.Cells(8).Visible = False            
            e.Item.Cells(12).Visible = False
            e.Item.Cells(19).Visible = False            
        End If
    End Sub

    Sub dgFFBPriceList_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

    Sub dgFFFBOB_BindGrid(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='DarkSeaGreen'")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='e9e9e9'")
            Else
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='f2f2f2'")

            End If
        End If
    End Sub

#End Region

#REGION "LOCAL & PROCEDURE"    
    Sub BindSupplier(ByVal pv_strCode As String)
        Dim strOpCd As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim sSQLKriteria AS String
        Dim objdsST AS New DataSet
        Dim dr As DataRow
 
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        sSQLKriteria="SELECT RTRIM(Suppliercode) AS SupplierCode,RTRIM(Name) AS SuppName From PU_Supplier  Where Status='1' And Supptype='5' "
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd, _
                                         strParamName, _
                                         strParamValue, _
                                         objdsST)
        Catch Exp As System.Exception
            Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
        End Try

        dr = objdsST.Tables(0).NewRow()
        dr("SupplierCode") = ""
        ''dr("Name") = lblPleaseSelect.Text & lblBillPartyTag.Text
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        radcmbCust.DataSource = objdsST.Tables(0)
        radcmbCust.DataValueField = "SupplierCode"
        radcmbCust.DataTextField = "SuppName"
        radcmbCust.DataBind()
        radcmbCust.SelectedIndex = 0
 
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
 
        sSQLKriteria="SELECT SubLocCode,SubDescription From SH_LOCATIONSUB  Where LocCode='" & strlocation & "'"
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                         strParamName, _
                                         strParamValue, _
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

    Private Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lbl As Label

        dsData = LoadData()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTicketList.PageSize)

        dgTicketList.DataSource = dsData
        If dgTicketList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgTicketList.CurrentPageIndex = 0
            Else
                dgTicketList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgTicketList.DataBind()
        'BindPageList()

        'PageNo = dgTicketList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgTicketList.PageCount

        'CType(dgTicketList.Items(intCnt).FindControl("lblNoKontrakWB"), Label).BackColor =
        Dim lblNoKontrak As Label
        Dim lblNoKontrakWB As Label
        Dim lblPotDiakui As Label
        Dim lblPotWB As Label
        Dim lblIsInap As Label
        Dim lblDescInap As Label
        Dim lblIsverify As Label
        Dim lblInvoiceNo As Label

        For intCnt = 0 To dgTicketList.Items.Count - 1
            lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKodeSpl")
            lblIsInap = dgTicketList.Items.Item(intCnt).FindControl("lblIsBuahInap")
            lblDescInap = dgTicketList.Items.Item(intCnt).FindControl("lblDescBuahInap")
            lblIsverify = dgTicketList.Items.Item(intCnt).FindControl("lblIsVerify")
            lblInvoiceNo = dgTicketList.Items.Item(intCnt).FindControl("lblInvoiceID")

          
            CType(dgTicketList.Items.Item(intCnt).FindControl("lblNoUrut"), Label).Text = intCnt + 1
            
            If lblIsverify.text.Trim="1" OR lblIsverify.text.Trim="2" OR lblIsverify.text.Trim="3" Then 
                CType(dgTicketList.Items.Item(intCnt).FindControl("chkisInvoice"), CheckBox).Checked = True
                CType(dgTicketList.Items.Item(intCnt).FindControl("lblVerifyInfo"), Label).Text = "Yes"
            Else
                CType(dgTicketList.Items.Item(intCnt).FindControl("chkisInvoice"), CheckBox).Checked = False
                CType(dgTicketList.Items.Item(intCnt).FindControl("lblVerifyInfo"), Label).Text = "No"
            End IF

            IF lblInvoiceNo.text.Trim = "" Then
                CType(dgTicketList.Items.Item(intCnt).FindControl("chkisInvoice"), CheckBox).Enabled=True
            Else
                CType(dgTicketList.Items.Item(intCnt).FindControl("chkisInvoice"), CheckBox).Enabled=False
            END IF

            If Trim(lbl.Text) <> "ZZZZZZ" Then
                lblNoKontrak = dgTicketList.Items.Item(intCnt).FindControl("lblNoKontrakWB")
                lblNoKontrakWB = dgTicketList.Items.Item(intCnt).FindControl("lblNoKontrak")
 
                lblPotWB = dgTicketList.Items.Item(intCnt).FindControl("lblPotWB")

                If lblNoKontrak.Text.Trim <> lblNoKontrakWB.Text.Trim Then
                    dgTicketList.Items.Item(intCnt).ForeColor = Drawing.Color.Red
                End If

            End If

            If Trim(lblIsInap.Text) = "1" Then
                lblDescInap.text = "Yes"
                lblDescInap.ForeColor = Drawing.Color.Red

            Else
                lblDescInap.text = "No"
            End If

            If Trim(lbl.Text) = "ZZZZZZ" Then
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblNoUrut")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblProd")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblTglMsuk")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblJamMasuk")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblJamKeluar")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblNamaSpl")
                lbl.Text = Replace(lbl.Text, "ZZZ", "")
                lbl.Font.Bold = True
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblPotTotal")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblRate")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKGOB")
                lbl.Visible = False
                lbl = dgTicketList.Items.Item(intCnt).FindControl("lblKGOL")
                lbl.Visible = False
            End If
        Next
    End Sub

    Sub CheckAll()
        Dim lblInvoiceNo As Label  
        Dim lblIsverify As Label

        For intCnt = 0 To dgTicketList.Items.Count - 1
            lblIsverify = dgTicketList.Items.Item(intCnt).FindControl("lblIsVerify")
            lblInvoiceNo = dgTicketList.Items.Item(intCnt).FindControl("lblInvoiceID")           
            IF lblIsverify.text.Trim="0" And chkisAll.Checked=True Then 
                CType(dgTicketList.Items.Item(intCnt).FindControl("chkisInvoice"), CheckBox).Checked=True            
            END IF

            IF lblIsverify.text.Trim="0"  And chkisAll.Checked=False  Then
                CType(dgTicketList.Items.Item(intCnt).FindControl("chkisInvoice"), CheckBox).Checked=False
            END IF

        Next
    END Sub

    Sub BindGridPPH()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String

        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet
        Dim lbl As Label

        dsData = LoadDataPPH()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgPPHList.PageSize)

        dgPPHList.DataSource = dsData
        If dgPPHList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgPPHList.CurrentPageIndex = 0
            Else
                dgPPHList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgPPHList.DataBind()

        For intCnt = 0 To dgPPHList.Items.Count - 1
            lbl = dgPPHList.Items.Item(intCnt).FindControl("lblKodeSpl")
            If Trim(lbl.Text) = "ZZZZZZ" Then
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblNoUrut")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblTglMasuk")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblNamaSpl")
                lbl.Text = Replace(lbl.Text, "ZZZ", "")
                lbl.Font.Bold = True
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblNPWP")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblDPPAmountPPH")
                lbl.Font.Bold = True
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblPPHAmountPPH")
                lbl.Font.Bold = True
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblRatePPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOBKGPPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOBTripPPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOBTotalPPH")
                lbl.Font.Bold = True
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOLKGPPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOLTripPPH")
                lbl.Visible = False
                lbl = dgPPHList.Items.Item(intCnt).FindControl("lblOLTotalPPH")
                lbl.Font.Bold = True
            End If
        Next
    End Sub

    Sub BindGridFFBPrice()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadDataFFBPrice()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTicketList.PageSize)

        dgFFBPriceList.DataSource = dsData
        If dgFFBPriceList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgFFBPriceList.CurrentPageIndex = 0
            Else
                dgFFBPriceList.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgFFBPriceList.DataBind()
        'BindPageList()

        'PageNo = dgTicketList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgTicketList.PageCount
    End Sub

    Sub BindGridFFBPricedDetail()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadDataFFBPriceDetail()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgTicketList.PageSize)

        dgFFBPriceListDet.DataSource = dsData
        If dgFFBPriceList.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgFFBPriceListDet.CurrentPageIndex = 0
            Else
                dgFFBPriceListDet.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgFFBPriceListDet.DataBind()
        'BindPageList()

        'PageNo = dgTicketList.CurrentPageIndex + 1
        'lblTracker.Text = "Page " & PageNo & " of " & dgTicketList.PageCount
    End Sub

    Sub BindGridFFBPriceBongkar()
        Dim PageCount As Integer
        Dim dsData As DataSet

        dsData = LoadDataFFBPrice_Bongkar()
        PageCount = objGlobal.mtdGetPageCount(dsData.Tables(0).Rows.Count, dgFFFBOB.PageSize)

        dgFFFBOB.DataSource = dsData
        If dgFFFBOB.CurrentPageIndex >= PageCount Then
            If PageCount = 0 Then
                dgFFFBOB.CurrentPageIndex = 0
            Else
                dgFFFBOB.CurrentPageIndex = PageCount - 1
            End If
        End If

        dgFFFBOB.DataBind()

    End Sub

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_TICKET_GET"       
        Dim strSrchStatus As String
        Dim strSearch As String
         Dim strSrchDateIn As String
         Dim strSrchDateTo As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String

        
        strParamName = "DTFR|DTTO|SUPCODE|SUBBSN|STS"
        strParamValue = Format(srchDateIn.SelectedDate,"yyyy-MM-dd") & "|" & Format(srchDateInTo.SelectedDate,"yyyy-MM-dd") & "|" & radcmbCust.Selectedvalue  & "|" & Trim(srchlocation.SelectedItem.Value)   & "|" &  Trim(ddlStatusGen.SelectedValue)

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgTicketList.DataSource = objTicketDs
        dgTicketList.DataBind()

        If objTicketDs.Tables(0).Rows.Count > 0 Then
            cbExcelTicket.Visible = True
            TicketPrintPrev.Visible = True

            lstDropList.Visible = False
            btnPrev.Visible = False
            btnNext.Visible = False
        Else
            cbExcelTicket.Visible = False
            TicketPrintPrev.Visible = False

            lstDropList.Visible = False
            btnPrev.Visible = False
            btnNext.Visible = False
        End If

        Return objTicketDs

    End Function
 
    Protected Function LoadDataPPH() As DataSet
        Dim strOppCode_Get As String  = "WM_WM_CLSTRX_TICKET_GET_SUMMARY"
        Dim strSrchTicketNo As String
        Dim strSrchDeliveryNo As String
        Dim strSrchDateIn As String
        Dim strSrchDateTo As String
        Dim strSrchCustomer As String
        Dim strSrchProduct As String
        Dim strSrchStatus As String
        Dim strSearch As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String
        Dim lbl As Label


        strParamName = "DTFR|DTTO|SUPCODE|SUBBSN|VRF"
        strParamValue =  Format(srchDateIn.SelectedDate,"yyyy-MM-dd") & "|" & Format(srchDateInTo.SelectedDate,"yyyy-MM-dd")  & _
                            "|" & radcmbCust.Selectedvalue & "|" & Trim(srchlocation.SelectedItem.Value) & "|" & ddlStatusGen.SelectedValue

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgPPHList.DataSource = objPPHDs
        dgPPHList.DataBind()

        If objPPHDs.Tables(0).Rows.Count > 0 Then
            btnGenerate.Visible = True
            cbExcelPPH.Visible = True
            PPHPrintPrev.Visible = True
        Else
            btnGenerate.Visible = False
            cbExcelPPH.Visible = False
            PPHPrintPrev.Visible = False
        End If

        Return objPPHDs
    End Function

    Protected Function LoadDataFFBPrice() As DataSet
        Dim strOppCode_Get As String ="WM_WM_CLSTRX_FFB_PRICE_GET"
        Dim intErrNo As Integer
        Dim strSrchDateIn As String
        Dim strSrchDateTo As String
         Dim objFormatDate As String
        Dim objActualDate As String

        strParamName = "DTFR|DTTO"
        strParamValue = Format(srchDateIn.SelectedDate,"yyyy-MM-dd")  & "|" & Format(srchDateInTo.SelectedDate,"yyyy-MM-dd") 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgFFBPriceList.DataSource = objPPHDs
        dgFFBPriceList.DataBind()

        Return objPPHDs
    End Function

    Protected Function LoadDataFFBPrice_Bongkar() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_FFB_ONGKOS_BONGKAR_GET"
        Dim intErrNo As Integer 
        Dim strSrchDateIn As String
        Dim strSrchDateTo As String
        Dim objFormatDate As String
        Dim objActualDate As String

        
        strParamName = "DTFR|DTTO"
        strParamValue = Format(srchDateIn.SelectedDate,"yyyy-MM-dd")  & "|" & Format(srchDateInTo.SelectedDate,"yyyy-MM-dd") 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgFFFBOB.DataSource = objPPHDs
        dgFFFBOB.DataBind()

        Return objPPHDs
    End Function

    Protected Function LoadDataFFBPriceDetail() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_FFB_PRICE_DETAIL_GET"
        Dim intErrNo As Integer
        Dim strSrchDateIn As String
        Dim strSrchDateTo As String
         Dim objFormatDate As String
        Dim objActualDate As String
        
        strParamName = "DTFR|DTTO"
        strParamValue = Format(srchDateIn.SelectedDate,"yyyy-MM-dd")  & "|" & Format(srchDateInTo.SelectedDate,"yyyy-MM-dd") 

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        dgFFBPriceListDet.DataSource = objPPHDs
        dgFFBPriceListDet.DataBind()

        Return objPPHDs
    End Function

    Protected Function LoadDataEmpty() As DataSet
        Dim strOppCode_Get As String = "WM_WM_CLSTRX_TICKET_GET_EMPTY"
        Dim intErrNo As Integer
        Dim strSrchDateIn As String
        Dim strSrchDateTo As String
        Dim objFormatDate As String
        Dim objActualDate As String

        
        strParamName = "DTFR|DTTO|SUBLOC"
        strParamValue = Format(srchDateIn.SelectedDate,"yyyy-MM-dd")  & "|" & Format(srchDateInTo.SelectedDate,"yyyy-MM-dd")  & "|" & srchlocation.SelectedItem.Value

        Try
            intErrNo = objOk.mtdGetDataCommon(strOppCode_Get, strParamName, strParamValue, objPPHDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=wm_ticket&errmesg=" & Exp.Message & "&redirect=")
        End Try

        If objPPHDs.Tables(0).Rows.Count > 0 Then
            lblErrRefresh.Visible = True
            lblErrRefresh.Text = objPPHDs.Tables(0).Rows(0).Item("Msg")
        End If

        Return objPPHDs
    End Function

   Sub BindPageList()
      
        Dim count As Integer = 1
        Dim arrDList As New ArrayList()

        While Not count = dgTicketList.PageCount + 1
            arrDList.Add("Page " & count)
            count = count + 1
        End While
        lstDropList.DataSource = arrDList
        lstDropList.DataBind()
        lstDropList.SelectedIndex = dgTicketList.CurrentPageIndex

    End Sub

    Sub BindAccYear(ByVal pv_strAccYear As String)
        Dim strParamName As String
        Dim strParamValue As String
        Dim intErrNo As Integer
        Dim intCnt As Integer = 0
        Dim intSelectedIndex As Integer
        Dim objAccYearDs As New Object
        Dim strOpCd As String = "ADMIN_CLSACCPERIOD_CONFIG_GET"

        strParamName = "LOCCODE|SEARCHSTR|SORTEXP"
        strParamValue = strLocation & "||Order By HD.AccYear"

        Try
            intErrNo = objOk.mtdGetDataCommon(strOpCd, _
                                                strParamName, _
                                                strParamValue, _
                                                objAccYearDs)

        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GET_ACCYEAR&errmesg=" & Exp.Message & "&redirect=")
        End Try

        For intCnt = 0 To objAccYearDs.Tables(0).Rows.Count - 1
            objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            objAccYearDs.Tables(0).Rows(intCnt).Item("UserName") = Trim(objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear"))
            If objAccYearDs.Tables(0).Rows(intCnt).Item("AccYear") = pv_strAccYear Then
                intSelectedIndex = intCnt + 1
            End If
        Next intCnt

        ddlyear.DataSource = objAccYearDs.Tables(0)
        ddlyear.DataValueField = "AccYear"
        ddlyear.DataTextField = "UserName"
        ddlyear.DataBind()
        ddlyear.SelectedIndex = intSelectedIndex - 1
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
 
    Function lSaveIsInvoice()  AS Boolean
        Dim IsInvoice AS String="0"
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim strParamName AS String
        Dim strParamValue AS String
        Dim strTicketNO AS String
        Dim sSQLKriteria AS String
        Dim intErrNo As Integer        
        Dim objdsST As New DataSet
        Dim bSave AS Boolean=false
        

        For intCnt = 0 To dgTicketList.Items.Count - 1
            strTicketNo= CType(dgTicketList.Items(intCnt).FindControl("lblTicketNo"), Label).text

            If CType(dgTicketList.Items(intCnt).FindControl("chkisInvoice"), CheckBox).Checked = True Then
                Try

                    sSQLKriteria="Update WM_TRX_TICKET Set IsVerify='1' Where KodeSlipTimbangan='" & strTicketNo & "'"
                    strParamName = "STRSEARCH"
                    strParamValue = sSQLKriteria
                    
                    intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                            strParamName, _
                                            strParamValue, _
                                            objdsST)
                    bSave=True
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
                End Try
            Else
                Try
                    sSQLKriteria="Update WM_TRX_TICKET Set IsVerify='0' Where KodeSlipTimbangan='" & strTicketNo & "'"
                    strParamName = "STRSEARCH"
                    strParamValue = sSQLKriteria
                    
                    intErrNo = objGLtrx.mtdGetDataCommon(strOpCd_Get, _
                                            strParamName, _
                                            strParamValue, _
                                            objdsST)
                    bSave=True
                Catch Exp As System.Exception
                    Response.Redirect("../../../include/mesg/ErrorMessage.aspx?errcode=STOCK_ADJUSTMENT_UPDATE_FAILED&errmesg=&redirect=IN/Trx/IN_trx_StockAdj_list.aspx")
                End Try
            End If
        Next

        Return bSave
    End Function
#End Region
End Class
