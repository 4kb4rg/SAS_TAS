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
        If strUserId = "" Then
            Response.Redirect("/SessionExpire.aspx")
        ElseIf objAR.mtdHasAccessRights(objAR.mtdGetAccessRights(objAR.EnumWMAccessRights.WMWeighBridgeTicket), intWMAR) = False Then
            Response.Redirect("/" & strLangCode & "/include/mesg/AccessRights.aspx")
        Else
            If SortExpression.Text = "" Then
                SortExpression.Text = "TicketNo"
            End If

            If Not Page.IsPostBack Then
                
                srchDateIn.SelectedDate=Date.Now()
                srchDateTo.SelectedDate=Date.Now()

                'srchDateTo.Text = DateAdd(DateInterval.Month, 1, CDate(Month(Now()) & "/1/" & Year(Now())))
                'srchDateTo.Text = objGlobal.GetShortDate(Session("SS_DATEFMT"), DateAdd(DateInterval.Day, -1, CDate(srchDateTo.Text)))

                BindSearchStatusList()
                BindSearchProductList()
                BindGrid()                
            End If

            If intLocLevel = objAdminLoc.EnumLocLevel.Estate And strLocType = objAdminLoc.EnumLocType.Mill Then
                NewTicketBtn.Visible = True
            Else
                NewTicketBtn.Visible = False
            End If
        End If
    End Sub

    Sub BindSearchStatusList()

        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.All), objWMTrx.EnumWeighBridgeTicketStatus.All))
        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.Active), objWMTrx.EnumWeighBridgeTicketStatus.Active))
        srchStatusList.Items.Add(New ListItem(objWMTrx.mtdGetWeighBridgeTicketStatus(objWMTrx.EnumWeighBridgeTicketStatus.Deleted), objWMTrx.EnumWeighBridgeTicketStatus.Deleted))

        srchStatusList.SelectedIndex = 1
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
        dgTicketList.CurrentPageIndex = 0
        dgTicketList.EditItemIndex = -1
        BindGrid()
    End Sub

    Sub Sort_Grid(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        SortExpression.Text = e.SortExpression.ToString()
        SortCol.Text = IIf(SortCol.Text = "ASC", "DESC", "ASC")
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

        Dim nTotSellerFirsWeight As Double = 0
        Dim nTotSellerSecWeight As Double = 0
        Dim nTotSellerNetto As Double = 0

        Dim nTotCustFirsWeight As Double = 0
        Dim nTotCustSecWeight As Double = 0
        Dim nTotCustNetto As Double = 0
        Dim nTotSelisih As Double = 0

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


        For intCnt = 0 To dsData.Tables(0).Rows.Count - 1
            nTotSellerFirsWeight = nTotSellerFirsWeight + lCDbl(dsData.Tables(0).Rows(intCnt).Item("FirstWeight"))
            nTotSellerSecWeight = nTotSellerSecWeight + lCDbl(dsData.Tables(0).Rows(intCnt).Item("SecondWeight"))
            nTotSellerNetto = nTotSellerNetto + lCDbl(dsData.Tables(0).Rows(intCnt).Item("NetWeight"))

            nTotCustFirsWeight = nTotCustFirsWeight + lCDbl(dsData.Tables(0).Rows(intCnt).Item("BuyerFirstWeight"))
            nTotCustSecWeight = nTotCustSecWeight + lCDbl(dsData.Tables(0).Rows(intCnt).Item("BuyerSecondWeight"))
            nTotCustNetto = nTotCustNetto + lCDbl(dsData.Tables(0).Rows(intCnt).Item("BuyerNetWeight"))
            nTotSelisih = nTotSelisih + lCDbl(dsData.Tables(0).Rows(intCnt).Item("Selisih"))
        Next


        CType(getFooter(dgTicketList).FindControl("lblTotSellerFirstWeight"), Label).Text = Format(nTotSellerFirsWeight, fNom)
        CType(getFooter(dgTicketList).FindControl("lblTotSecondWeight"), Label).Text = Format(nTotSellerSecWeight, fNom)
        CType(getFooter(dgTicketList).FindControl("lblTotSellerNetWeight"), Label).Text = Format(nTotSellerNetto, fNom)

        CType(getFooter(dgTicketList).FindControl("lblTotBuyerFirstWeight"), Label).Text = Format(nTotCustFirsWeight, fNom)
        CType(getFooter(dgTicketList).FindControl("lblTotBuyerSecondWeight"), Label).Text = Format(nTotCustSecWeight, fNom)
        CType(getFooter(dgTicketList).FindControl("lblTotBuyerNetWeight"), Label).Text = Format(nTotCustNetto, fNom)
        CType(getFooter(dgTicketList).FindControl("lblTotSelisih"), Label).Text = Format(nTotSelisih, fNom)
    End Sub
    Function getFooter(ByVal grid As DataGrid) As DataGridItem
        For Each ctrl As WebControl In grid.Controls(0).Controls
            'loop DataGridTable
            If TypeOf ctrl Is System.Web.UI.WebControls.DataGridItem Then
                Dim item As DataGridItem = DirectCast(ctrl, DataGridItem)
                If item.ItemType = ListItemType.Footer Then Return item
            End If
        Next
    End Function

    Function lCDbl(ByVal pcVal As String) As Double
        If IsNumeric(pcVal) Then lCDbl = CDbl(pcVal) Else lCDbl = 0
    End Function

    Protected Function LoadData() As DataSet
        Dim strOppCode_Get As String = "WM_CLSTRX_WEIGHBRIDGE_TICKET_GET"
        Dim strSrchTicketNo As String
        Dim strsrchContractNo As String
        Dim strSrchDeliveryNo As String
        Dim strSrchDateIn As String
        Dim strSrchCustomer As String
        Dim strSrchProduct As String
        Dim strSrchStatus As String
        Dim strSrchLastUpdate As String
        Dim strSrchDate As String = ""
        Dim strSearch As String
        Dim strParam As String
        Dim intErrNo As Integer
        Dim intCnt As Integer
        Dim objFormatDate As String
        Dim objActualDate As String


        strSrchTicketNo = IIf(srchTicketNo.Text = "", "", srchTicketNo.Text)
        strsrchContractNo = IIf(srchContractNo.Text = "", "", srchContractNo.Text)
        strSrchDeliveryNo = IIf(srchDeliveryNo.Text = "", "", srchDeliveryNo.Text)

        strSrchDate = " AND TIC.InDate BETWEEN '" & Format(srchDateIn.SelectedDate,"yyyy-MM-dd") & "' AND '" & Format(srchDateTo.SelectedDate,"yyyy-MM-dd") & "' "
 
        strSrchTicketNo = IIf(srchTicketNo.Text = "", "", "AND k.TicketNo LIKE '" & Trim(srchTicketNo.Text) & "%' ")
        strsrchContractNo = IIf(srchContractNo.Text = "", "", "AND k.ContractNo LIKE '" & Trim(srchContractNo.Text) & "%' ")
        strSrchDeliveryNo = IIf(srchDeliveryNo.Text = "", "", "AND TIC.DeliveryNoteNo LIKE '" & Trim(srchDeliveryNo.Text) & "%' ")
        strSrchCustomer = IIf(srchCustomer.Text = "", "", "AND (TIC.CustomerCode LIKE '" & Trim(srchCustomer.Text) & "%' or SP.Name LIKE '%" & Trim(srchCustomer.Text) & "%') ")
        strSrchProduct = IIf(srchProductList.SelectedItem.Value = objWMTrx.EnumWeighBridgeTicketProduct.All, "", " AND TIC.ProductCode='" & srchProductList.SelectedItem.Value & "' ")
        strSrchStatus = IIf(srchStatusList.SelectedItem.Value = "", "", " AND TIC.Status = '" & Trim(srchStatusList.SelectedItem.Value) & "' ")     
        strSearch = strSrchDate & strSrchTicketNo & strsrchContractNo & strSrchDeliveryNo & strSrchStatus & strSrchCustomer & strSrchProduct & strSrchLastUpdate

        strSearch = " WHERE " & Mid(Trim(strSearch), 5)

        strSearch = strSearch


        strParamName = "SEARCHSTR"
        strParamValue = strSearch

        Try
            intErrNo = objGLTrx.mtdGetDataCommon(strOppCode_Get, _
                                                 strParamName, _
                                                 strParamValue, _
                                                 objTicketDs)
        Catch Exp As System.Exception
            Response.Redirect("/include/mesg/ErrorMessage.aspx?errcode=GENERATEINV_LOAD&errmesg=" & lblErrMessage.Text & "&redirect=ap/trx/ap_trx_invrcv_wm_list.aspx")
        End Try

        If objTicketDs.Tables(0).Rows.Count > 0 Then
            cbExcelTicket.Visible = True
            TicketPrintPrev.Visible = True
        Else
            cbExcelTicket.Visible = False
            TicketPrintPrev.Visible = False
        End If

        Return objTicketDs
    End Function


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

    Private Sub dgTicketList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTicketList.ItemCreated
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
            dgCell.Text = "TrxCPOER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 3
            dgItem.Cells.Add(dgCell)
            dgCell.Text = "SELLER"
            dgCell.HorizontalAlign = HorizontalAlign.Center

            dgCell = New TableCell()
            dgCell.ColumnSpan = 4
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
            e.Item.Cells(3).Visible = False
            e.Item.Cells(4).Visible = False
            e.Item.Cells(12).Visible = False
            e.Item.Cells(13).Visible = False
        End If
    End Sub

End Class
