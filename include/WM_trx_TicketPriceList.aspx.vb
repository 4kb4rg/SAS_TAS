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


Public Class WM_trx_TicketPriceList : Inherits Page

    Protected objWMTrx As New agri.WM.clsTrx()
    Protected objGlobal As New agri.GlobalHdl.clsGlobalHdl()
    Dim objAR As New agri.GlobalHdl.clsAccessRights()
    Dim objAdminLoc As New agri.Admin.clsLoc()
    Dim objGLSetup As New agri.GL.clsSetup()
    Dim objGLTrx As New agri.GL.ClsTrx()
    Dim objSysCfg As New agri.PWSystem.clsConfig()

    Dim strCompany As String
    Dim strLocation As String
    Dim strUserId As String
    Dim strAccMonth As String
    Dim strAccYear As String
    Dim strLangCode As String
    Dim intWMAR As Integer
    Dim strDateFormat As String
    Dim intLevel As Integer
    Dim intLocLevel As Integer
    Dim strLocType As String
    Dim strParamName As String
    Dim strParamValue As String
    Dim vAcsessRight As Boolean = False
    Dim objTicketDs As New DataSet()

    Dim fNom As String = "#,###."

    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        strCompany = Session("SS_COMPANY")
        strLocation = Session("SS_LOCATION")
        strUserId = Session("SS_USERID")
        strAccMonth = Session("SS_PMACCMONTH")
        strAccYear = Session("SS_PMACCYEAR")
        strLangCode = Session("SS_LANGCODE")
        intWMAR = Session("SS_WMAR")
        strDateFormat = Session("SS_DATEFMT")
        intLevel = Session("SS_USRLEVEL")
        intLocLevel = Session("SS_LOCLEVEL")
        strLocType = Session("SS_LOCTYPE")

        If (intLocLevel = 3 And strLocType = 4) Then
            vAcsessRight = True
        Else
            vAcsessRight = False
        End If

        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = False Or vAcsessRight = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TicketNo"
            End If

            If Not Page.IsPostBack Then
                BindSubLocation()
                BindSupplier("")
                BindTransType()
                BindSearchProductList()
                dtDateFr.SelectedDate = Date.Now
                dtDateTo.SelectedDate = Date.Now
                BindGrid()
            End If
        End If

    End Sub

    Sub BindSearchProductList()
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer

        sSQLKriteria = "SELECT ProductCode,ProductName From WM_STP_PRODUCT  Where Status='1'"
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
        dr("ProductCode") = ""
        dr("ProductName") = "Select Product"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        srchProductList.DataSource = objdsST.Tables(0)
        srchProductList.DataValueField = "ProductCode"
        srchProductList.DataTextField = "ProductName"
        srchProductList.DataBind()
        srchProductList.SelectedIndex = 0
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
        srchlocation.SelectedIndex = 0

    End Sub

    Sub BindTransType()
        Dim strParamName As String
        Dim strParamValue As String
        Dim strOpCd_Get As String = "IN_CLSTRX_GLOBAL_UPD"

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim dr As DataRow
        Dim sSQLKriteria As String = ""
        Dim objdsST As New DataSet
        Dim intSelectedIndex As Integer

        sSQLKriteria = "SELECT TransType,TransName From WM_STP_TRANSTYPE "
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
        dr("TransType") = ""
        dr("TransName") = "Select Transaction"
        objdsST.Tables(0).Rows.InsertAt(dr, 0)

        srchTransType.DataSource = objdsST.Tables(0)
        srchTransType.DataValueField = "TransType"
        srchTransType.DataTextField = "TransName"
        srchTransType.DataBind()
        srchTransType.SelectedIndex = 0

    End Sub

    Sub BindSupplier(ByVal pv_strCode As String)
        Dim strOpCd As String = "IN_CLSTRX_GLOBAL_UPD"
        Dim strParamName As String
        Dim strParamValue As String
        Dim sSQLKriteria As String
        Dim objdsST As New DataSet
        Dim dr As DataRow

        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim intSelectedIndex As Integer = 0

        sSQLKriteria = "SELECT RTRIM(RelasiCode) AS SupplierCode,RTRIM(RelasiName) AS SuppName From V_Relasi "
        strParamName = "STRSEARCH"
        strParamValue = sSQLKriteria

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOpCd,
                                         strParamName,
                                         strParamValue,
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

    Sub srchBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'dgTicketList.CurrentPageIndex = 0
        'dgTicketList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub BindGrid()
        Dim lbButton As LinkButton
        Dim intCnt As Integer
        Dim Status As Label
        Dim strStatus As String
        Dim PageNo As Integer
        Dim PageCount As Integer
        Dim dsData As DataSet

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


        Dim nNettoWeight As Double = 0
        Dim nTotalNominal As Double = 0

        For intCnt = 0 To dsData.Tables(0).Rows.Count - 1
            nNettoWeight = nNettoWeight + lCDbl(dsData.Tables(0).Rows(intCnt).Item("NetWeightFinal"))
            nTotalNominal = nTotalNominal + lCDbl(dsData.Tables(0).Rows(intCnt).Item("TotalAmount"))
        Next intCnt

        CType(getFooter(dgTicketList).FindControl("lblTotalNetWeight"), Label).Text = Format(nNettoWeight, fNom)
        CType(getFooter(dgTicketList).FindControl("lblTotalAmount"), Label).Text = Format(nTotalNominal, fNom)

        BindPageList()
        ''BindGridFooter()

    End Sub

    Sub BindGridFooter()
        Dim nNettoWeight As Double = 0
        Dim nTotalNominal As Double = 0
         
        For intCnt = 0 To dgTicketList.Items.Count - 1
            nNettoWeight = nNettoWeight + lCDbl(CType(dgTicketList.Items(intCnt).FindControl("lblNetWeight"), Label).Text)
            nTotalNominal = nTotalNominal + lCDbl(CType(dgTicketList.Items(intCnt).FindControl("lblAmount"), Label).Text)
        Next

        CType(getFooter(dgTicketList).FindControl("lblTotalNetWeight"), Label).Text = Format(nNettoWeight, fNom)
        CType(getFooter(dgTicketList).FindControl("lblTotalAmount"), Label).Text = Format(nTotalNominal, fNom)

    End Sub

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Function getFooter(ByVal grid As DataGrid) As DataGridItem
        For Each ctrl As WebControl In grid.Controls(0).Controls
            'loop DataGridTable
            If TypeOf ctrl Is System.Web.UI.WebControls.DataGridItem Then
                Dim item As DataGridItem = DirectCast(ctrl, DataGridItem)
                If item.ItemType = ListItemType.Footer Then Return item
            End If
        Next
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

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_PRICE_LIST"
        Dim strParamName As String
        Dim strParamValue As String

        Dim intErrNo As Integer


        strParamName = "FR|TO|TICKETNO|CONTRACTNO|DONO|SUPP|PRODUCT|SUBLOC|TRTYPE"
        strParamValue = Format(dtDateFr.SelectedDate, "yyyy-MM-dd") &
                        "|" & Format(dtDateTo.SelectedDate, "yyyy-MM-dd") &
                        "|" & srchTicketNo.Text &
                        "|" & srchContractNo.Text &
                        "|" & srchDeliveryNo.Text &
                        "|" & radcmbCust.SelectedValue &
                        "|" & srchProductList.SelectedItem.Value &
                        "|" & srchlocation.SelectedItem.Value &
                        "|" & srchTransType.SelectedItem.Value

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOppCode_Get,
                                                 strParamName,
                                                 strParamValue,
                                                 objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_LOAD&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        Return objTicketDs
    End Function

    Sub btnPrevNext_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim direction As String = CType(sender, ImageButton).CommandArgument
        Select Case direction
            Case "first"
                dgTicketList.CurrentPageIndex = 0
            Case "prev"
                dgTicketList.CurrentPageIndex =
                Math.Max(0, dgTicketList.CurrentPageIndex - 1)
            Case "next"
                dgTicketList.CurrentPageIndex =
                Math.Min(dgTicketList.PageCount - 1, dgTicketList.CurrentPageIndex + 1)
            Case "last"
                dgTicketList.CurrentPageIndex = dgTicketList.PageCount - 1
        End Select

        lstDropList.SelectedIndex = dgTicketList.CurrentPageIndex
        BindGrid()
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

    Sub btnTicketPrintPrev_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        'Dim cAccMonth As String = IIf(Len(ddlMonth.SelectedItem.Value) = 1, "0" & Trim(ddlyear.SelectedItem.Value), Trim(ddlMonth.SelectedItem.Value))
        'strAccYear = ddlyear.SelectedItem.Value

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=WBTICKET-" & Trim(strLocation) & ".xls")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"

        Dim stringWrite = New System.IO.StringWriter()
        Dim htmlWrite = New HtmlTextWriter(stringWrite)

        dgTicketList.AllowPaging = False
        BindGrid()
        dgTicketList.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
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

    Sub NewTicketBtn_Click(ByVal Sender As Object, ByVal E As ImageClickEventArgs)
        Response.Redirect("WM_trx_WeighBridgeTicketDet.aspx")
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
